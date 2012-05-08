using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileManager.BusinessClasses
{
    class LibraryManager
    {
        private static LibraryManager _instance = new LibraryManager();

        public List<Library> LibraryCollection { get; set; }
        public Library SelectedLibrary { get; set; }
        public bool OldStyleProceed { get; set; }

        public static LibraryManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private LibraryManager()
        {
            this.LibraryCollection = new List<Library>();
        }


        public void LoadLibraries(DirectoryInfo rootFolder)
        {
            if (rootFolder.Exists)
            {
                this.LibraryCollection.Clear();
                if (rootFolder.Root.FullName.Equals(rootFolder.FullName))
                {
                    this.LibraryCollection.Add(new Library(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage, rootFolder));
                    this.SelectedLibrary = this.LibraryCollection[0];
                }
                else
                {
                    foreach (DirectoryInfo subFolder in rootFolder.GetDirectories())
                        this.LibraryCollection.Add(new Library(subFolder.Name, subFolder));
                    this.SelectedLibrary = this.LibraryCollection.Where(x => x.Name.Equals(ConfigurationClasses.SettingsManager.Instance.SelectedLibrary)).FirstOrDefault();
                    if (this.SelectedLibrary == null && this.LibraryCollection.Count > 0)
                        this.SelectedLibrary = this.LibraryCollection[0];
                }
            }
        }

        public void SynchronizeLibraries()
        {
            HashSet<string> filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string foldera = ConfigurationClasses.SettingsManager.Instance.BackupPath;
            string folderb = ConfigurationClasses.SettingsManager.Instance.NetworkPath;
            if (!String.IsNullOrEmpty(foldera) && Directory.Exists(foldera) && !String.IsNullOrEmpty(folderb) && Directory.Exists(folderb))
            {
                foreach (Library salesDepot in this.LibraryCollection)
                    if (salesDepot.IsConfigured)
                    {
                        salesDepot.PrepareForSynchronize();

                        string salesDepotFoldername = salesDepot.Folder.FullName.Equals(salesDepot.Folder.Root.FullName) ? ConfigurationClasses.SettingsManager.WholeDriveFilesStorage : salesDepotFoldername = salesDepot.Folder.Name;

                        DirectoryInfo destinationFolder = new DirectoryInfo(Path.Combine(folderb, salesDepotFoldername));
                        if (!destinationFolder.Exists)
                            destinationFolder.Create();
                        filesWhiteList.Clear();

                        AddFolderForSync(new DirectoryInfo(Path.Combine(salesDepot.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName)), filesWhiteList);
                        AddFolderForSync(new DirectoryInfo(Path.Combine(salesDepot.Folder.FullName, ConfigurationClasses.SettingsManager.LibraryLogoFolder)), filesWhiteList);

                        foreach (LibraryPage page in salesDepot.Pages)
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

                        FileInfo cacheFile = new FileInfo(Path.Combine(salesDepot.Folder.FullName, ConfigurationClasses.SettingsManager.StorageFileName));
                        if (cacheFile.Exists)
                            cacheFile.CopyTo(Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.StorageFileName), true);

                        List<DirectoryInfo> sourceSubFolders = new List<DirectoryInfo>();
                        List<DirectoryInfo> destinationSubFolders = new List<DirectoryInfo>();
                        sourceSubFolders.AddRange(salesDepot.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
                        foreach (DirectoryInfo subFolder in sourceSubFolders)
                        {
                            string destinationSubFolderPath = Path.Combine(destinationFolder.FullName, subFolder.Name);
                            if (!Directory.Exists(destinationSubFolderPath))
                                Directory.CreateDirectory(destinationSubFolderPath);
                            DirectoryInfo destinationSubFolder = new DirectoryInfo(destinationSubFolderPath);
                            destinationSubFolders.Add(destinationSubFolder);
                            ToolClasses.SyncManager.Instance.SynchronizeFolders(subFolder, destinationSubFolder, filesWhiteList);
                        }

                        if (salesDepot.OvernightsCalendar.Enabled)
                        {
                            string overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.OvernightsCalendarRootFolderName);
                            if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
                                Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
                            DirectoryInfo overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
                            destinationSubFolders.Add(overnightsCalendarDestinationFolder);
                            ToolClasses.SyncManager.Instance.SynchronizeFolders(salesDepot.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
                        }

                        foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName)))
                            ToolClasses.SyncManager.Instance.DeleteFolder(subFolder);
                    }
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
                    {
                        if (!filesWhiteList.Contains(file.FullName))
                            filesWhiteList.Add(file.FullName);
                    }
            }
        }
    }
}