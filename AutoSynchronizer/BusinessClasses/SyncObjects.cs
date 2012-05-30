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
                    this.Manager.Library.PrepareForSynchronize();

                    string libraryFolderName = this.Manager.Library.Folder.FullName.Equals(this.Manager.Library.Folder.Root.FullName) ? ConfigurationClasses.SettingsManager.WholeDriveFilesStorage : this.Manager.Library.Folder.Name;

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

                    if (this.Manager.Library.OvernightsCalendar.Enabled)
                    {
                        string overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.OvernightsCalendarRootFolderName);
                        if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
                            Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
                        DirectoryInfo overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
                        destinationSubFolders.Add(overnightsCalendarDestinationFolder);
                        ToolClasses.SyncManager.Instance.SynchronizeFolders(this.Manager.Library.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
                    }

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
        private DateTime _nextGrabTime = DateTime.MinValue;
        private InteropClasses.OutlookHelper _outlook = null;

        public LibraryWrapper Manager { get; private set; }

        public OvernightsEmailGrabber(LibraryWrapper manager)
        {
            this.Manager = manager;
            _outlook = new InteropClasses.OutlookHelper(this.Manager.Library.OvernightsCalendar);
            ScheduleNextGrab();
        }

        #region Schedule Methods
        private long GetMillisecondsForNextGrab()
        {
            DateTime nowTime = DateTime.Now;
            DateTime nextTime = _nextGrabTime.Equals(DateTime.MinValue) ? nowTime : _nextGrabTime;
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

        private void ScheduleNextGrab()
        {
            StopBackgroundGrab();
            if (this.Manager.Library.OvernightsCalendar.EnableEmailGrabber)
            {
                _timer = new System.Threading.Timer(delegate(object state)
                {
                    GrabEmail();

                    _nextGrabTime = DateTime.Now.AddMinutes(this.Manager.Library.OvernightsCalendar.EmailGrabInterval);
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
