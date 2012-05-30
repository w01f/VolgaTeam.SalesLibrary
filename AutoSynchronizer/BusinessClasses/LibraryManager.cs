using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoSynchronizer.BusinessClasses
{
    class LibraryManager
    {
        private static LibraryManager _instance = new LibraryManager();

        public List<LibraryWrapper> LibraryCollection { get; private set; }

        public static LibraryManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private LibraryManager()
        {
            this.LibraryCollection = new List<LibraryWrapper>();
        }

        public void LoadLibraries()
        {
            DirectoryInfo rootFolder = new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.BackupPath);
            if (rootFolder.Exists)
            {
                this.LibraryCollection.Clear();
                if (rootFolder.Root.FullName.Equals(rootFolder.FullName))
                    this.LibraryCollection.Add(new LibraryWrapper(new Library(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage, rootFolder)));
                else
                    foreach (DirectoryInfo subFolder in rootFolder.GetDirectories())
                        this.LibraryCollection.Add(new LibraryWrapper(new Library(subFolder.Name, subFolder)));
            }
        }
    }

    public class LibraryWrapper
    {
        private FileSystemWatcher _libraryStorageWatcher = new FileSystemWatcher();
        private FileSystemWatcher _fileGrabberWatcher = null;

        public Library Library { get; private set; }
        public LibrarySynchronizer Syncer { get; private set; }
        public OvernightsEmailGrabber EmailGrabber { get; private set; }

        public LibraryWrapper(Library library)
        {
            this.Library = library;
            this.Library.OvernightsCalendar.LoadYears();
            InitServiceObjects();

            _libraryStorageWatcher.Path = this.Library.Folder.FullName;
            _libraryStorageWatcher.Filter = ConfigurationClasses.SettingsManager.StorageFileName;
            _libraryStorageWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _libraryStorageWatcher.Changed += new FileSystemEventHandler((sender, e) =>
            {
                try
                {
                    if (!this.Syncer.SyncInProgress)
                    {
                        _libraryStorageWatcher.EnableRaisingEvents = false;
                        lock (AppManager.Locker)
                        {
                            this.Library.Init();
                            InitServiceObjects();
                        }
                    }
                }
                finally
                {
                    _libraryStorageWatcher.EnableRaisingEvents = true;
                }
            });
            _libraryStorageWatcher.EnableRaisingEvents = true;
        }

        public void InitServiceObjects()
        {
            if (this.Syncer != null)
                this.Syncer.StopBackgroundSync();
            this.Syncer = new LibrarySynchronizer(this);

            if (this.EmailGrabber != null)
                this.EmailGrabber.StopBackgroundGrab();
            this.EmailGrabber = new OvernightsEmailGrabber(this);

            if (_fileGrabberWatcher != null)
            {
                _fileGrabberWatcher.EnableRaisingEvents = false;
                _fileGrabberWatcher = null;
            }
            if (this.Library.OvernightsCalendar.EnableFileGrabber && Directory.Exists(this.Library.OvernightsCalendar.FileGrabSourceFolder))
            {
                foreach (FileInfo file in GetLatestFiles(new DirectoryInfo(this.Library.OvernightsCalendar.FileGrabSourceFolder)))
                    GrabFile(file);

                _fileGrabberWatcher = new FileSystemWatcher();
                _fileGrabberWatcher.Path = this.Library.OvernightsCalendar.FileGrabSourceFolder;
                _fileGrabberWatcher.IncludeSubdirectories = true;
                _fileGrabberWatcher.NotifyFilter = NotifyFilters.LastWrite;
                _fileGrabberWatcher.Changed += new FileSystemEventHandler((sender, e) =>
                {
                    GrabFile(new FileInfo(e.FullPath));
                });
                _fileGrabberWatcher.EnableRaisingEvents = true;
            }
        }

        private void GrabFile(FileInfo file)
        {
            DateTime fileDate = file.LastWriteTime;
            string fileExtension = file.Extension;
            if (fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx"))
            {
                string tempFile = Path.GetTempFileName();
                file.CopyTo(tempFile, true);
                InteropClasses.ExcelHelper excelHelper = new InteropClasses.ExcelHelper();
                fileDate = excelHelper.GetOvernightsDate(tempFile);
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
            BusinessClasses.CalendarYear year = this.Library.OvernightsCalendar.Years.Where(x => x.Year.Equals(fileDate.Year)).FirstOrDefault();
            if (year != null && year.RootFolder.Exists)
            {
                string destinationPath = Path.Combine(year.RootFolder.FullName, string.Format("{0}f{1}", new string[] { fileDate.ToString("MMddyy"), fileExtension }));
                if (File.Exists(destinationPath))
                { 
                    if(File.GetLastWriteTime(destinationPath) < file.LastWriteTime)
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
    }
}