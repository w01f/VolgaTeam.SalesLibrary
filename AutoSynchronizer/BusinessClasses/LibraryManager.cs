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

        public Library Library { get; private set; }
        public LibrarySynchronizer Syncer { get; private set; }
        public OvernightsEmailGrabber EmailGrabber { get; private set; }
        public OvernightsFileGrabber FileGrabber { get; private set; }

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
            else
                this.EmailGrabber = new OvernightsEmailGrabber(this);
            this.EmailGrabber.ScheduleNextGrab();

            if (this.FileGrabber != null)
                this.FileGrabber.StopBackgroundGrab();
            else
                this.FileGrabber = new OvernightsFileGrabber(this);
            this.FileGrabber.ScheduleNextGrab();
        }
    }
}