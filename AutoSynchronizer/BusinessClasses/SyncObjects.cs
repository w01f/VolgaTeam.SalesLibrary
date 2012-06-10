using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoSynchronizer.BusinessClasses
{
    public class LibrarySynchronizer
    {
        private System.Threading.Timer _timer = null;

        public LibraryWrapper Manager { get; private set; }

        public bool SyncInProgress { get; private set; }

        public LibrarySynchronizer(LibraryWrapper manager)
        {
            this.Manager = manager;
            ScheduleNextSync();
        }

        #region Schedule Methods
        private DateTime[] GetSyncDatesFromTimePoints(DateTime now)
        {
            List<DateTime> result = new List<DateTime>();

            foreach (TimePoint sheduledTime in this.Manager.Library.SyncTimes)
            {
                DateTime syncDate = new DateTime(now.Year, now.Month, now.Day, sheduledTime.Time.Hour, sheduledTime.Time.Minute, sheduledTime.Time.Second);

                while (syncDate.DayOfWeek != sheduledTime.Day)
                    syncDate = syncDate.AddDays(1);
                if (syncDate < now)
                    syncDate = syncDate.AddDays(7);
                result.Add(syncDate);
            }

            return result.ToArray();
        }

        private long GetMillisecondsForNextSync()
        {
            DateTime nowTime = DateTime.Now;
            DateTime nextTime = GetSyncDatesFromTimePoints(nowTime).Min();
            TimeSpan difference = nextTime.Subtract(nowTime);
            long totalMilliseconds = (long)difference.TotalMilliseconds;
            return totalMilliseconds;
        }

        public void StopBackgroundSync()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        private void ScheduleNextSync()
        {
            StopBackgroundSync();
            if (this.Manager.Library.EnableAutoSync && this.Manager.Library.SyncTimes.Count > 0)
            {
                _timer = new System.Threading.Timer(delegate(object state)
                {
                    if (!AppManager.Instance.FileManagerActive())
                        Synchronize();
                    this.Manager.InitServiceObjects();
                }, null, GetMillisecondsForNextSync(), System.Threading.Timeout.Infinite);
            }
        }
        #endregion

        #region Sync Methods
        public void Synchronize()
        {
            try
            {
                this.SyncInProgress = true;
                if (this.Manager.Library.IsConfigured && !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.BackupPath) && Directory.Exists(ConfigurationClasses.SettingsManager.Instance.BackupPath) && !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.NetworkPath) && Directory.Exists(ConfigurationClasses.SettingsManager.Instance.NetworkPath))
                {
                    HashSet<string> filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    List<string> existedLibraryFolderNames = new List<string>();
                    this.Manager.Library.PrepareForSynchronize();

                    string libraryFolderName = this.Manager.Library.Folder.FullName.Equals(this.Manager.Library.Folder.Root.FullName) ? ConfigurationClasses.SettingsManager.WholeDriveFilesStorage : this.Manager.Library.Folder.Name;
                    existedLibraryFolderNames.Add(libraryFolderName);

                    DirectoryInfo destinationFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.NetworkPath, libraryFolderName));
                    if (!destinationFolder.Exists)
                        destinationFolder.Create();
                    filesWhiteList.Clear();

                    AddFolderForSync(new DirectoryInfo(Path.Combine(this.Manager.Library.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName)), filesWhiteList);
                    AddFolderForSync(new DirectoryInfo(Path.Combine(this.Manager.Library.Folder.FullName, ConfigurationClasses.SettingsManager.LibraryLogoFolder)), filesWhiteList);

                    foreach (LibraryPage page in this.Manager.Library.Pages)
                        foreach (LibraryFolder folder in page.Folders)
                            foreach (LibraryFile file in folder.Files)
                            {
                                switch (file.Type)
                                {
                                    case FileTypes.Folder:
                                        AddFolderForSync(new DirectoryInfo(file.FullPath), filesWhiteList);
                                        break;
                                    case FileTypes.BuggyPresentation:
                                    case FileTypes.FriendlyPresentation:
                                    case FileTypes.OtherPresentation:
                                    case FileTypes.MediaPlayerVideo:
                                    case FileTypes.QuickTimeVideo:
                                    case FileTypes.Other:
                                        if (File.Exists(file.FullPath))
                                        {
                                            if (!filesWhiteList.Contains(file.FullPath))
                                                filesWhiteList.Add(file.FullPath);
                                        }
                                        break;
                                    case FileTypes.LineBreak:
                                        break;
                                }
                            }

                    FileInfo cacheFile = new FileInfo(Path.Combine(this.Manager.Library.Folder.FullName, ConfigurationClasses.SettingsManager.StorageFileName));
                    if (cacheFile.Exists)
                        cacheFile.CopyTo(Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.StorageFileName), true);

                    List<DirectoryInfo> sourceSubFolders = new List<DirectoryInfo>();
                    List<DirectoryInfo> destinationSubFolders = new List<DirectoryInfo>();

                    #region Sync Primary Root
                    sourceSubFolders.Clear();
                    sourceSubFolders.AddRange(this.Manager.Library.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
                    foreach (DirectoryInfo subFolder in sourceSubFolders)
                    {
                        string destinationSubFolderPath = Path.Combine(destinationFolder.FullName, subFolder.Name);
                        if (!Directory.Exists(destinationSubFolderPath))
                            Directory.CreateDirectory(destinationSubFolderPath);
                        DirectoryInfo destinationSubFolder = new DirectoryInfo(destinationSubFolderPath);
                        destinationSubFolders.Add(destinationSubFolder);
                        ToolClasses.SyncManager.Instance.SynchronizeFolders(subFolder, destinationSubFolder, filesWhiteList);
                    }
                    #endregion

                    #region Sync Extra Roots
                    if (this.Manager.Library.ExtraFolders.Count > 0)
                    {
                        string extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.ExtraFoldersRootFolderName);
                        if (!Directory.Exists(extraFoldersDestinationRootPath))
                            Directory.CreateDirectory(extraFoldersDestinationRootPath);
                        DirectoryInfo extraFoldersDestinationRoot = new DirectoryInfo(extraFoldersDestinationRootPath);
                        destinationSubFolders.Add(extraFoldersDestinationRoot);
                        List<DirectoryInfo> extraFolderDestinations = new List<DirectoryInfo>();
                        foreach (RootFolder extraRootFolder in this.Manager.Library.ExtraFolders)
                        {
                            sourceSubFolders.Clear();
                            sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
                            if (sourceSubFolders.Count > 0)
                            {
                                string extraFolderDestinationPath = Path.Combine(extraFoldersDestinationRoot.FullName, extraRootFolder.RootId.ToString());
                                if (!Directory.Exists(extraFolderDestinationPath))
                                    Directory.CreateDirectory(extraFolderDestinationPath);
                                DirectoryInfo extraFolderDestination = new DirectoryInfo(extraFolderDestinationPath);
                                extraFolderDestinations.Add(extraFolderDestination);
                                ToolClasses.SyncManager.Instance.SynchronizeFolders(extraRootFolder.Folder, extraFolderDestination, filesWhiteList);
                            }
                        }
                        foreach (DirectoryInfo subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
                            ToolClasses.SyncManager.Instance.DeleteFolder(subFolder);
                    }
                    #endregion

                    #region Sync Overnights Calendar
                    if (this.Manager.Library.OvernightsCalendar.Enabled)
                    {
                        string overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.OvernightsCalendarRootFolderName);
                        if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
                            Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
                        DirectoryInfo overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
                        destinationSubFolders.Add(overnightsCalendarDestinationFolder);
                        ToolClasses.SyncManager.Instance.SynchronizeFolders(this.Manager.Library.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
                    }
                    #endregion

                    foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName)))
                        ToolClasses.SyncManager.Instance.DeleteFolder(subFolder);
                }
            }
            finally
            {
                this.SyncInProgress = false;
            }
        }

        private void AddFolderForSync(DirectoryInfo folder, HashSet<string> filesWhiteList)
        {
            if (folder.Exists)
            {
                foreach (DirectoryInfo subFolder in folder.GetDirectories())
                    AddFolderForSync(subFolder, filesWhiteList);
                foreach (FileInfo file in folder.GetFiles())
                    if (!file.Name.ToLower().Equals("thumbs.db"))
                        if (!filesWhiteList.Contains(file.FullName))
                            filesWhiteList.Add(file.FullName);
            }
        }
        #endregion
    }

    public class OvernightsEmailGrabber
    {
        private System.Threading.Timer _timer = null;
        private InteropClasses.OutlookHelper _outlook = null;

        public DateTime NextGrabTime { get; set; }
        public LibraryWrapper Manager { get; private set; }

        public OvernightsEmailGrabber(LibraryWrapper manager)
        {
            this.NextGrabTime = DateTime.MinValue;
            this.Manager = manager;
            _outlook = new InteropClasses.OutlookHelper(this.Manager.Library.OvernightsCalendar);
        }

        #region Schedule Methods
        private long GetMillisecondsForNextGrab()
        {
            DateTime nowTime = DateTime.Now;
            DateTime nextTime = this.NextGrabTime.Equals(DateTime.MinValue) ? nowTime : this.NextGrabTime;
            TimeSpan difference = nextTime.Subtract(nowTime);
            long totalMilliseconds = (long)difference.TotalMilliseconds;
            return totalMilliseconds;
        }

        public void StopBackgroundGrab()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        public void ScheduleNextGrab()
        {
            StopBackgroundGrab();
            if (this.Manager.Library.OvernightsCalendar.EnableEmailGrabber)
            {
                _timer = new System.Threading.Timer(delegate(object state)
                {
                    GrabEmail();

                    this.NextGrabTime = DateTime.Now.AddMinutes(this.Manager.Library.OvernightsCalendar.EmailGrabInterval);
                    ScheduleNextGrab();
                }, null, GetMillisecondsForNextGrab(), System.Threading.Timeout.Infinite);
            }
        }
        #endregion

        #region Grab Methods
        public void GrabEmail()
        {
            lock (AppManager.Locker)
            {
                _outlook.GrabOvernightsEmail();
            }
        }
        #endregion
    }
}
