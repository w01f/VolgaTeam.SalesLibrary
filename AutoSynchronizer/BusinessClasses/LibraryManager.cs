using System.Collections.Generic;
using System.IO;
using AutoSynchronizer.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace AutoSynchronizer.BusinessClasses
{
	internal class LibraryManager
	{
		private static readonly LibraryManager _instance = new LibraryManager();

		private LibraryManager()
		{
			LibraryCollection = new List<LibraryWrapper>();
		}

		public List<LibraryWrapper> LibraryCollection { get; private set; }

		public static LibraryManager Instance
		{
			get { return _instance; }
		}

		public void LoadLibraries()
		{
			var rootFolder = new DirectoryInfo(SettingsManager.Instance.BackupPath);
			if (rootFolder.Exists)
			{
				LibraryCollection.Clear();
				if (rootFolder.Root.FullName.Equals(rootFolder.FullName) || SettingsManager.Instance.UseDirectAccessToFiles)
					LibraryCollection.Add(new LibraryWrapper(new Library(Constants.WholeDriveFilesStorage, rootFolder)));
				else
					foreach (var subFolder in rootFolder.GetDirectories())
						LibraryCollection.Add(new LibraryWrapper(new Library(subFolder.Name, subFolder)));
			}
		}
	}

	public class LibraryWrapper
	{
		private readonly FileSystemWatcher _libraryStorageWatcher = new FileSystemWatcher();

		public LibraryWrapper(Library library)
		{
			Library = library;
			Library.OvernightsCalendar.LoadParts();
			InitServiceObjects();

			_libraryStorageWatcher.Path = Library.Folder.FullName;
			_libraryStorageWatcher.Filter = Constants.StorageFileName;
			_libraryStorageWatcher.Changed += (sender, e) =>
			{
				if (e.ChangeType == WatcherChangeTypes.Changed)
				{
					try
					{
						if (!Syncer.SyncInProgress)
						{
							_libraryStorageWatcher.EnableRaisingEvents = false;
							lock (AppManager.Locker)
							{
								Library.Init();
								Library.OvernightsCalendar.LoadParts();
								InitServiceObjects();
							}
						}
					}
					finally
					{
						_libraryStorageWatcher.EnableRaisingEvents = true;
					}
				}
			};
			_libraryStorageWatcher.EnableRaisingEvents = true;
		}

		public Library Library { get; private set; }
		public LibrarySynchronizer Syncer { get; private set; }

		public void InitServiceObjects()
		{
			if (Syncer != null)
				Syncer.StopBackgroundSync();
			Syncer = new LibrarySynchronizer(this);
		}
	}
}