using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            foreach (SyncScheduleRecord sheduleRecord in this.Manager.Library.SyncScheduleRecords)
            {
                DateTime syncDate = new DateTime(now.Year, now.Month, now.Day, sheduleRecord.Time.Hour, sheduleRecord.Time.Minute, 0);

                while (!((syncDate.DayOfWeek == DayOfWeek.Monday & sheduleRecord.Monday) ||
                         (syncDate.DayOfWeek == DayOfWeek.Tuesday & sheduleRecord.Tuesday) ||
                         (syncDate.DayOfWeek == DayOfWeek.Wednesday & sheduleRecord.Wednesday) ||
                         (syncDate.DayOfWeek == DayOfWeek.Thursday & sheduleRecord.Thursday) ||
                         (syncDate.DayOfWeek == DayOfWeek.Friday & sheduleRecord.Friday) ||
                         (syncDate.DayOfWeek == DayOfWeek.Saturday & sheduleRecord.Saturday) ||
                         (syncDate.DayOfWeek == DayOfWeek.Sunday & sheduleRecord.Sunday)))
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
            if (this.Manager.Library.EnableAutoSync && this.Manager.Library.SyncScheduleRecords.Count > 0)
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

                    StringBuilder syncLog = new StringBuilder();
                    int filesCreated = 0;
                    int filesUpdated = 0;
                    int filesDeleted = 0;
                    int filesDeclined = 0;
                    int foldersCreated = 0;
                    int foldersDeleted = 0;
                    ToolClasses.SyncManager syncManager = new ToolClasses.SyncManager();
                    syncManager.FileCreated += new EventHandler<ToolClasses.SyncEventArgs>((sender, e) =>
                    {
                        syncLog.AppendLine(string.Format("File created: {0}", new string[] { e.Destination }));
                        filesCreated++;
                    });
                    syncManager.FileUpdated += new EventHandler<ToolClasses.SyncEventArgs>((sender, e) =>
                    {
                        syncLog.AppendLine(string.Format("File updated: {0}", new string[] { e.Destination }));
                        filesUpdated++;
                    });
                    syncManager.FileDeleted += new EventHandler<ToolClasses.SyncEventArgs>((sender, e) =>
                    {
                        syncLog.AppendLine(string.Format("File deleted: {0}", new string[] { e.Destination }));
                        filesDeleted++;
                    });
                    syncManager.FileDeclined += new EventHandler<ToolClasses.SyncEventArgs>((sender, e) =>
                    {
                        syncLog.AppendLine(string.Format("File declined: {0}", new string[] { e.Destination }));
                        filesDeclined++;
                    });
                    syncManager.FolderCreated += new EventHandler<ToolClasses.SyncEventArgs>((sender, e) =>
                    {
                        syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { e.Destination }));
                        foldersCreated++;
                    });
                    syncManager.FolderDeleted += new EventHandler<ToolClasses.SyncEventArgs>((sender, e) =>
                    {
                        syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { e.Destination }));
                        foldersDeleted++;
                    });


                    syncLog.AppendLine(string.Format("Sync started: {0}", new string[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));
                    syncLog.AppendLine(string.Format("Sync {0}", new string[] { this.Manager.Library.Name }));

                    HashSet<string> filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    List<string> existedLibraryFolderNames = new List<string>();
                    this.Manager.Library.PrepareForSynchronize();

                    string libraryFolderName = this.Manager.Library.Folder.FullName.Equals(this.Manager.Library.Folder.Root.FullName) ? ConfigurationClasses.SettingsManager.WholeDriveFilesStorage : this.Manager.Library.Folder.Name;
                    existedLibraryFolderNames.Add(libraryFolderName);

                    DirectoryInfo destinationFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.NetworkPath, libraryFolderName));
                    if (!destinationFolder.Exists)
                    {
                        destinationFolder.Create();
                        syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { destinationFolder.FullName }));
                        foldersCreated++;
                    }
                    filesWhiteList.Clear();

                    AddFolderForSync(new DirectoryInfo(Path.Combine(this.Manager.Library.Folder.FullName, ConfigurationClasses.SettingsManager.LibraryLogoFolder)), filesWhiteList);
                    filesWhiteList.Add(new FileInfo(Path.Combine(this.Manager.Library.Folder.FullName, ConfigurationClasses.SettingsManager.StorageFileName)).FullName);

                    List<DirectoryInfo> sourceSubFolders = new List<DirectoryInfo>();
                    List<DirectoryInfo> destinationSubFolders = new List<DirectoryInfo>();

                    if (!this.Manager.Library.UseDirectAccess)
                    {
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
                                            if (File.Exists(file.FullPath))
                                            {
                                                if (!filesWhiteList.Contains(file.FullPath))
                                                {
                                                    filesWhiteList.Add(file.FullPath);
                                                    if (file.PreviewContainer != null)
                                                        AddFolderForSync(new DirectoryInfo(file.PreviewContainer.ContainerPath), filesWhiteList);
                                                }
                                            }
                                            break;
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

                        syncManager.SynchronizeFolders(this.Manager.Library.Folder, destinationFolder, filesWhiteList, false);

                        #region Sync Primary Root
                        sourceSubFolders.Clear();
                        sourceSubFolders.AddRange(this.Manager.Library.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
                        foreach (DirectoryInfo subFolder in sourceSubFolders)
                        {
                            string destinationSubFolderPath = Path.Combine(destinationFolder.FullName, subFolder.Name);
                            if (!Directory.Exists(destinationSubFolderPath))
                            {
                                Directory.CreateDirectory(destinationSubFolderPath);
                                syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { destinationSubFolderPath }));
                                foldersCreated++;
                            }
                            DirectoryInfo destinationSubFolder = new DirectoryInfo(destinationSubFolderPath);
                            destinationSubFolders.Add(destinationSubFolder);
                            syncManager.SynchronizeFolders(subFolder, destinationSubFolder, filesWhiteList);
                        }
                        #endregion

                        #region Sync Extra Roots
                        if (this.Manager.Library.ExtraFolders.Count > 0)
                        {
                            string extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.ExtraFoldersRootFolderName);
                            if (!Directory.Exists(extraFoldersDestinationRootPath))
                            {
                                Directory.CreateDirectory(extraFoldersDestinationRootPath);
                                syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { extraFoldersDestinationRootPath }));
                                foldersCreated++;
                            }
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
                                    {
                                        Directory.CreateDirectory(extraFolderDestinationPath);
                                        syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { extraFolderDestinationPath }));
                                        foldersCreated++;
                                    }
                                    DirectoryInfo extraFolderDestination = new DirectoryInfo(extraFolderDestinationPath);
                                    extraFolderDestinations.Add(extraFolderDestination);
                                    syncManager.SynchronizeFolders(extraRootFolder.Folder, extraFolderDestination, filesWhiteList);
                                }
                            }
                            foreach (DirectoryInfo subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
                            {
                                ToolClasses.SyncManager.DeleteFolder(subFolder);
                                syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { subFolder.FullName }));
                                foldersDeleted++;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        syncManager.SynchronizeFolders(this.Manager.Library.Folder, destinationFolder, filesWhiteList, false);
                    }

                    #region Sync Overnights Calendar
                    if (this.Manager.Library.OvernightsCalendar.Enabled)
                    {
                        string overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.OvernightsCalendarRootFolderName);
                        if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
                        {
                            Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
                            syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { overnightsCalendarDestinationFolderPath }));
                            foldersCreated++;
                        }
                        DirectoryInfo overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
                        destinationSubFolders.Add(overnightsCalendarDestinationFolder);
                        syncManager.SynchronizeFolders(this.Manager.Library.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
                    }
                    #endregion

                    #region Sync Program Manager
                    if (this.Manager.Library.EnableProgramManagerSync && !string.IsNullOrEmpty(this.Manager.Library.ProgramManagerLocation) && Directory.Exists(this.Manager.Library.ProgramManagerLocation))
                    {
                        string programManagerDestinationFolderPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.ProgramManagerRootFolderName);
                        if (!Directory.Exists(programManagerDestinationFolderPath))
                        {
                            Directory.CreateDirectory(programManagerDestinationFolderPath);
                            syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { programManagerDestinationFolderPath }));
                            foldersCreated++;
                        }
                        DirectoryInfo programManagerSourceFolder = new DirectoryInfo(this.Manager.Library.ProgramManagerLocation);
                        DirectoryInfo programManagerDestinationFolder = new DirectoryInfo(programManagerDestinationFolderPath);
                        destinationSubFolders.Add(programManagerDestinationFolder);
                        syncManager.SynchronizeFolders(programManagerSourceFolder, programManagerDestinationFolder, new HashSet<string>());
                    }
                    #endregion

                    foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName)))
                    {
                        ToolClasses.SyncManager.DeleteFolder(subFolder);
                        syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { subFolder.FullName }));
                        foldersDeleted++;
                    }

                    syncLog.AppendLine(string.Format("Total files created: {0}", new string[] { filesCreated.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total files updated: {0}", new string[] { filesUpdated.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total files deleted: {0}", new string[] { filesDeleted.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total files declined: {0}", new string[] { filesDeclined.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total folders created: {0}", new string[] { foldersCreated.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total folders deleted: {0}", new string[] { foldersDeleted.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Sync completed: {0}", new string[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));

                    try
                    {
                        string logPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LogRootPath, string.Format("Auto Sync at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
                        using (StreamWriter sw = new StreamWriter(logPath, false))
                        {
                            sw.Write(syncLog.ToString());
                            sw.Flush();
                            sw.Close();
                        }
                    }
                    catch
                    {
                    }
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
            long totalMilliseconds = (long)difference.TotalMilliseconds > 0 ? (long)difference.TotalMilliseconds : 0;
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
                    if (!AppManager.Instance.FileManagerActive())
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

    public class OvernightsFileGrabber
    {
        private System.Threading.Timer _timer = null;
        private InteropClasses.ExcelHelper _excel = null;
        private List<FileInfo> _files = new List<FileInfo>();
        private GrabCacheManager _grabCacheManager;

        public DateTime NextGrabTime { get; set; }
        public LibraryWrapper Manager { get; private set; }

        public OvernightsFileGrabber(LibraryWrapper manager)
        {
            this.NextGrabTime = DateTime.MinValue;
            this.Manager = manager;
            _excel = new InteropClasses.ExcelHelper();
        }

        #region Schedule Methods
        private long GetMillisecondsForNextGrab()
        {
            DateTime nowTime = DateTime.Now;
            DateTime nextTime = this.NextGrabTime.Equals(DateTime.MinValue) ? nowTime : this.NextGrabTime;
            TimeSpan difference = nextTime.Subtract(nowTime);
            long totalMilliseconds = (long)difference.TotalMilliseconds > 0 ? (long)difference.TotalMilliseconds : 0;
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
            if (this.Manager.Library.OvernightsCalendar.EnableFileGrabber)
            {
                _timer = new System.Threading.Timer(delegate(object state)
                {
                    if (!AppManager.Instance.FileManagerActive())
                        GrabFiles();

                    this.NextGrabTime = DateTime.Now.AddMinutes(this.Manager.Library.OvernightsCalendar.FileGrabInterval);
                    ScheduleNextGrab();
                }, null, GetMillisecondsForNextGrab(), System.Threading.Timeout.Infinite);
            }
        }
        #endregion

        #region Grab Methods
        private void GrabFiles()
        {
            _grabCacheManager = new GrabCacheManager(this.Manager.Library.OvernightsCalendar.FileGrabSourceFolder);
            List<FileInfo> newFiles = new List<FileInfo>(GetLatestFiles(new DirectoryInfo(this.Manager.Library.OvernightsCalendar.FileGrabSourceFolder)));
            FileInfo[] changedFiles = newFiles.Where(x => !_files.Select(y => y.FullName).Contains(x.FullName) || _files.Where(y => y.FullName.Equals(x.FullName) && y.LastWriteTime < x.LastWriteTime).Count() > 0).ToArray();
            foreach (FileInfo file in changedFiles)
                if (!file.FullName.Equals(_grabCacheManager.GrabCacheFilePath))
                    GrabFile(file);
            _files.Clear();
            _files.AddRange(newFiles);
            _grabCacheManager.SaveCache();
        }

        private void GrabFile(FileInfo file)
        {
            DateTime fileDate = file.LastWriteTime;
            string fileExtension = file.Extension;
            if (fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx"))
            {
                GrabFileInfo cachedInfo = _grabCacheManager.GrabbedFiles.Where(x => x.FilePath.Equals(file.FullName)).FirstOrDefault();
                if (cachedInfo == null)
                {
                    cachedInfo = new GrabFileInfo();
                    cachedInfo.FilePath = file.FullName;
                    _grabCacheManager.GrabbedFiles.Add(cachedInfo);
                }

                DateTime fileLastWrite = new DateTime(file.LastWriteTime.Year, file.LastWriteTime.Month, file.LastWriteTime.Day, file.LastWriteTime.Hour, file.LastWriteTime.Minute, file.LastWriteTime.Second);
                DateTime cacheLastWrite = new DateTime(cachedInfo.LastModified.Year, cachedInfo.LastModified.Month, cachedInfo.LastModified.Day, cachedInfo.LastModified.Hour, cachedInfo.LastModified.Minute, cachedInfo.LastModified.Second);
                if (fileLastWrite > cacheLastWrite)
                {
                    string tempFile = Path.GetTempFileName();
                    file.CopyTo(tempFile, true);
                    InteropClasses.ExcelHelper excelHelper = new InteropClasses.ExcelHelper();
                    fileDate = excelHelper.GetOvernightsDate(tempFile);
                    if (File.Exists(tempFile))
                        File.Delete(tempFile);

                    cachedInfo.FileDate = fileDate;
                    cachedInfo.LastModified = file.LastWriteTime;
                }
                else
                    fileDate = cachedInfo.FileDate;
            }
            BusinessClasses.CalendarYear year = this.Manager.Library.OvernightsCalendar.Years.Where(x => x.Year.Equals(fileDate.Year)).FirstOrDefault();
            if (year != null && year.RootFolder.Exists)
            {
                string destinationPath = Path.Combine(year.RootFolder.FullName, string.Format("{0}f{1}", new string[] { fileDate.ToString("MMddyy"), fileExtension }));
                if (File.Exists(destinationPath))
                {
                    if (File.GetLastWriteTime(destinationPath) < file.LastWriteTime)
                        file.CopyTo(destinationPath, true);
                }
                else
                    file.CopyTo(destinationPath, true);
            }
        }

        private FileInfo[] GetLatestFiles(DirectoryInfo sourceFolder)
        {
            List<FileInfo> result = new List<FileInfo>();

            foreach (DirectoryInfo subFolder in sourceFolder.GetDirectories())
                result.AddRange(GetLatestFiles(subFolder));

            foreach (FileInfo file in sourceFolder.GetFiles())
                result.Add(file);

            return result.ToArray();
        }
        #endregion
    }
}

