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
                if (rootFolder.Root.FullName.Equals(rootFolder.FullName) || ConfigurationClasses.SettingsManager.Instance.UseDirectAccessToFiles)
                {
                    this.LibraryCollection.Add(new Library(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage, rootFolder, ConfigurationClasses.SettingsManager.Instance.UseDirectAccessToFiles, ConfigurationClasses.SettingsManager.Instance.DirectAccessFileAgeLimit));
                    this.SelectedLibrary = this.LibraryCollection[0];
                }
                else
                {
                    foreach (DirectoryInfo subFolder in rootFolder.GetDirectories())
                        this.LibraryCollection.Add(new Library(subFolder.Name, subFolder, ConfigurationClasses.SettingsManager.Instance.UseDirectAccessToFiles, ConfigurationClasses.SettingsManager.Instance.DirectAccessFileAgeLimit));
                    this.SelectedLibrary = this.LibraryCollection.Where(x => x.Name.Equals(ConfigurationClasses.SettingsManager.Instance.SelectedLibrary)).FirstOrDefault();
                    if (this.SelectedLibrary == null && this.LibraryCollection.Count > 0)
                        this.SelectedLibrary = this.LibraryCollection[0];
                }
            }
        }

        public void SynchronizeLibraries()
        {
            AppManager.Instance.KillAutoFM();


            HashSet<string> filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            List<string> existedLibraryFolderNames = new List<string>();
            string foldera = ConfigurationClasses.SettingsManager.Instance.BackupPath;
            string folderb = ConfigurationClasses.SettingsManager.Instance.NetworkPath;
            if (!String.IsNullOrEmpty(foldera) && Directory.Exists(foldera) && !String.IsNullOrEmpty(folderb) && Directory.Exists(folderb))
            {
                foreach (Library salesDepot in this.LibraryCollection)
                    if (salesDepot.IsConfigured)
                    {
                        salesDepot.PrepareForSynchronize();

                        string salesDepotFolderName = salesDepot.Folder.FullName.Equals(salesDepot.Folder.Root.FullName) ? ConfigurationClasses.SettingsManager.WholeDriveFilesStorage : salesDepot.Folder.Name;
                        existedLibraryFolderNames.Add(salesDepotFolderName);

                        DirectoryInfo destinationFolder = new DirectoryInfo(Path.Combine(folderb, salesDepotFolderName));
                        if (!destinationFolder.Exists)
                            destinationFolder.Create();
                        filesWhiteList.Clear();

                        AddFolderForSync(new DirectoryInfo(Path.Combine(salesDepot.Folder.FullName, ConfigurationClasses.SettingsManager.LibraryLogoFolder)), filesWhiteList);
                        filesWhiteList.Add(new FileInfo(Path.Combine(salesDepot.Folder.FullName, ConfigurationClasses.SettingsManager.StorageFileName)).FullName);

                        List<DirectoryInfo> sourceSubFolders = new List<DirectoryInfo>();
                        List<DirectoryInfo> destinationSubFolders = new List<DirectoryInfo>();

                        if (!salesDepot.UseDirectAccess)
                        {
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
                                                if (File.Exists(file.FullPath))
                                                {
                                                    if (!filesWhiteList.Contains(file.FullPath))
                                                    {
                                                        filesWhiteList.Add(file.FullPath);
                                                        if (file.PreviewContainer != null)
                                                            AddFolderForSync(new DirectoryInfo(file.PreviewContainer.PreviewStorageFolder), filesWhiteList);
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

                            ToolClasses.SyncManager.Instance.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);

                            #region Sync Primary Root
                            sourceSubFolders.Clear();
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
                            #endregion

                            #region Sync Extra Roots
                            if (salesDepot.ExtraFolders.Count > 0)
                            {
                                string extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.ExtraFoldersRootFolderName);
                                if (!Directory.Exists(extraFoldersDestinationRootPath))
                                    Directory.CreateDirectory(extraFoldersDestinationRootPath);
                                DirectoryInfo extraFoldersDestinationRoot = new DirectoryInfo(extraFoldersDestinationRootPath);
                                destinationSubFolders.Add(extraFoldersDestinationRoot);
                                List<DirectoryInfo> extraFolderDestinations = new List<DirectoryInfo>();
                                foreach (RootFolder extraRootFolder in salesDepot.ExtraFolders)
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
                        }
                        else
                        {
                            ToolClasses.SyncManager.Instance.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);
                        }

                        #region Sync Overnights Calendar
                        if (salesDepot.OvernightsCalendar.Enabled)
                        {
                            string overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, ConfigurationClasses.SettingsManager.OvernightsCalendarRootFolderName);
                            if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
                                Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
                            DirectoryInfo overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
                            destinationSubFolders.Add(overnightsCalendarDestinationFolder);
                            ToolClasses.SyncManager.Instance.SynchronizeFolders(salesDepot.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
                        }
                        #endregion

                        foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName)))
                            ToolClasses.SyncManager.Instance.DeleteFolder(subFolder);
                    }

                DirectoryInfo networkFolder = new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.NetworkPath);
                foreach (DirectoryInfo folder in networkFolder.GetDirectories())
                    if (!existedLibraryFolderNames.Contains(folder.Name))
                        ToolClasses.SyncManager.Instance.DeleteFolder(folder);
            }

            AppManager.Instance.RunAutoFM();
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
    }
}