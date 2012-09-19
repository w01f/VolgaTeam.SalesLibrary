using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SalesDepot.CoreObjects;

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
                    this.LibraryCollection.Add(new Library(SalesDepot.CoreObjects.Constants.WholeDriveFilesStorage, rootFolder, ConfigurationClasses.SettingsManager.Instance.UseDirectAccessToFiles, ConfigurationClasses.SettingsManager.Instance.DirectAccessFileAgeLimit));
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
            string foldera = ConfigurationClasses.SettingsManager.Instance.BackupPath;
            string folderb = ConfigurationClasses.SettingsManager.Instance.NetworkPath;
            if (!String.IsNullOrEmpty(foldera) && Directory.Exists(foldera) && !String.IsNullOrEmpty(folderb) && Directory.Exists(folderb))
            {
                HashSet<string> filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                List<string> existedLibraryFolderNames = new List<string>();

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

                foreach (Library salesDepot in this.LibraryCollection)
                {
                    if (salesDepot.IsConfigured)
                    {
                        if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                            salesDepot.PrepareForRegularSynchronize();

                        if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                        {
                            syncLog.AppendLine(string.Format("Sync {0}", new string[] { salesDepot.Name }));

                            string salesDepotFolderName = salesDepot.Folder.FullName.Equals(salesDepot.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : salesDepot.Folder.Name;
                            existedLibraryFolderNames.Add(salesDepotFolderName);

                            DirectoryInfo destinationFolder = new DirectoryInfo(Path.Combine(folderb, salesDepotFolderName));
                            if (!destinationFolder.Exists)
                            {
                                destinationFolder.Create();
                                syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { destinationFolder.FullName }));
                                foldersCreated++;
                            }
                            filesWhiteList.Clear();

                            AddFolderForSync(new DirectoryInfo(Path.Combine(salesDepot.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
                            filesWhiteList.Add(new FileInfo(Path.Combine(salesDepot.Folder.FullName, Constants.StorageFileName)).FullName);

                            List<DirectoryInfo> sourceSubFolders = new List<DirectoryInfo>();
                            List<DirectoryInfo> destinationSubFolders = new List<DirectoryInfo>();

                            if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                            {
                                if (!salesDepot.UseDirectAccess)
                                {
                                    foreach (LibraryPage page in salesDepot.Pages)
                                    {
                                        foreach (LibraryFolder folder in page.Folders)
                                        {
                                            foreach (LibraryFile file in folder.Files)
                                            {
                                                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                                {
                                                    switch (file.Type)
                                                    {
                                                        case FileTypes.Folder:
                                                            AddFolderForSync(new DirectoryInfo(file.OriginalPath), filesWhiteList);
                                                            break;
                                                        case FileTypes.BuggyPresentation:
                                                        case FileTypes.FriendlyPresentation:
                                                        case FileTypes.Presentation:
                                                            if (File.Exists(file.OriginalPath))
                                                            {
                                                                if (!filesWhiteList.Contains(file.OriginalPath))
                                                                    filesWhiteList.Add(file.OriginalPath);
                                                                if (file.PreviewContainer != null)
                                                                    AddFolderForSync(new DirectoryInfo(file.PreviewContainer.ContainerPath), filesWhiteList);
                                                            }
                                                            break;
                                                        case FileTypes.MediaPlayerVideo:
                                                        case FileTypes.QuickTimeVideo:
                                                        case FileTypes.Other:
                                                            if (File.Exists(file.OriginalPath))
                                                            {
                                                                if (!filesWhiteList.Contains(file.OriginalPath))
                                                                    filesWhiteList.Add(file.OriginalPath);
                                                            }
                                                            break;
                                                        case FileTypes.LineBreak:
                                                            break;
                                                    }
                                                }
                                                else
                                                    break;
                                            }
                                            if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
                                                break;
                                        }
                                        if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
                                            break;
                                    }
                                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                        syncManager.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);

                                    #region Sync Primary Root
                                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                    {
                                        sourceSubFolders.Clear();
                                        sourceSubFolders.AddRange(salesDepot.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
                                        foreach (DirectoryInfo subFolder in sourceSubFolders)
                                        {
                                            if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
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
                                            else
                                                break;
                                        }
                                    }
                                    #endregion

                                    #region Sync Extra Roots
                                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                    {
                                        if (salesDepot.ExtraFolders.Count > 0)
                                        {
                                            string extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, Constants.ExtraFoldersRootFolderName);
                                            if (!Directory.Exists(extraFoldersDestinationRootPath))
                                            {
                                                Directory.CreateDirectory(extraFoldersDestinationRootPath);
                                                syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { extraFoldersDestinationRootPath }));
                                                foldersCreated++;
                                            }
                                            DirectoryInfo extraFoldersDestinationRoot = new DirectoryInfo(extraFoldersDestinationRootPath);
                                            destinationSubFolders.Add(extraFoldersDestinationRoot);
                                            List<DirectoryInfo> extraFolderDestinations = new List<DirectoryInfo>();
                                            foreach (RootFolder extraRootFolder in salesDepot.ExtraFolders)
                                            {
                                                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
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
                                                else
                                                    break;
                                            }
                                            if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                            {
                                                foreach (DirectoryInfo subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
                                                {
                                                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                                    {
                                                        ToolClasses.SyncManager.DeleteFolder(subFolder);
                                                        syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { subFolder.FullName }));
                                                        foldersDeleted++;
                                                    }
                                                    else
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    syncManager.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);
                                }

                                #region Sync Overnights Calendar
                                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                {
                                    if (salesDepot.OvernightsCalendar.Enabled)
                                    {
                                        string overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.OvernightsCalendarRootFolderName);
                                        if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
                                        {
                                            Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
                                            syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { overnightsCalendarDestinationFolderPath }));
                                            foldersCreated++;
                                        }
                                        DirectoryInfo overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
                                        destinationSubFolders.Add(overnightsCalendarDestinationFolder);
                                        syncManager.SynchronizeFolders(salesDepot.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
                                    }
                                }
                                #endregion

                                #region Sync Program Manager
                                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                {
                                    if (salesDepot.EnableProgramManagerSync && !string.IsNullOrEmpty(salesDepot.ProgramManagerLocation) && Directory.Exists(salesDepot.ProgramManagerLocation))
                                    {
                                        string programManagerDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.ProgramManagerRootFolderName);
                                        if (!Directory.Exists(programManagerDestinationFolderPath))
                                        {
                                            Directory.CreateDirectory(programManagerDestinationFolderPath);
                                            syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { programManagerDestinationFolderPath }));
                                            foldersCreated++;
                                        }
                                        DirectoryInfo programManagerSourceFolder = new DirectoryInfo(salesDepot.ProgramManagerLocation);
                                        DirectoryInfo programManagerDestinationFolder = new DirectoryInfo(programManagerDestinationFolderPath);
                                        destinationSubFolders.Add(programManagerDestinationFolder);
                                        syncManager.SynchronizeFolders(programManagerSourceFolder, programManagerDestinationFolder, new HashSet<string>());
                                    }
                                }
                                #endregion

                                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                {
                                    foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName)))
                                    {
                                        ToolClasses.SyncManager.DeleteFolder(subFolder);
                                        syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { subFolder.FullName }));
                                        foldersDeleted++;
                                    }
                                }
                            }
                        }
                    }
                }

                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    DirectoryInfo networkFolder = new DirectoryInfo(folderb);
                    foreach (DirectoryInfo folder in networkFolder.GetDirectories())
                        if (!existedLibraryFolderNames.Contains(folder.Name))
                        {
                            ToolClasses.SyncManager.DeleteFolder(folder);
                            syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { folder.FullName }));
                            foldersDeleted++;
                        }

                    syncLog.AppendLine(string.Format("Sync completed: {0}", new string[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));
                    syncLog.AppendLine(string.Format("Total files created: {0}", new string[] { filesCreated.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total files updated: {0}", new string[] { filesUpdated.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total files deleted: {0}", new string[] { filesDeleted.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total files declined: {0}", new string[] { filesDeclined.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total folders created: {0}", new string[] { foldersCreated.ToString("#,##0") }));
                    syncLog.AppendLine(string.Format("Total folders deleted: {0}", new string[] { foldersDeleted.ToString("#,##0") }));

                    try
                    {
                        string logPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LogRootPath, string.Format("Manual Sync at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
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

            AppManager.Instance.RunAutoFM();
        }

        public void SynchronizeLibraryForIpad(Library salesDepot)
        {
            HashSet<string> filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            List<string> existedLibraryFolderNames = new List<string>();

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

            if (salesDepot.IsConfigured)
            {
                syncLog.AppendLine(string.Format("Sync {0}", new string[] { salesDepot.Name }));

                string salesDepotFolderName = salesDepot.Folder.FullName.Equals(salesDepot.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : salesDepot.Folder.Name;
                existedLibraryFolderNames.Add(salesDepotFolderName);

                DirectoryInfo destinationFolder = new DirectoryInfo(salesDepot.IPadManager.SyncDestinationPath);
                if (!destinationFolder.Exists)
                {
                    destinationFolder.Create();
                    syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { destinationFolder.FullName }));
                    foldersCreated++;
                }
                filesWhiteList.Clear();

                AddFolderForSync(new DirectoryInfo(Path.Combine(salesDepot.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
                filesWhiteList.Add(new FileInfo(Path.Combine(salesDepot.Folder.FullName, Constants.StorageLightFileName)).FullName);

                List<DirectoryInfo> sourceSubFolders = new List<DirectoryInfo>();
                List<DirectoryInfo> destinationSubFolders = new List<DirectoryInfo>();

                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    foreach (LibraryPage page in salesDepot.Pages)
                    {
                        foreach (LibraryFolder folder in page.Folders)
                        {
                            foreach (LibraryFile file in folder.Files)
                            {
                                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                                {
                                    switch (file.Type)
                                    {
                                        case FileTypes.Folder:
                                            AddFolderForSync(new DirectoryInfo(file.OriginalPath), filesWhiteList);
                                            break;
                                        case FileTypes.BuggyPresentation:
                                        case FileTypes.FriendlyPresentation:
                                        case FileTypes.Presentation:
                                            if (File.Exists(file.OriginalPath))
                                            {
                                                if (!filesWhiteList.Contains(file.OriginalPath))
                                                    filesWhiteList.Add(file.OriginalPath);
                                                if (file.UniversalPreviewContainer != null && !string.IsNullOrEmpty(file.UniversalPreviewContainer.ContainerPath))
                                                    AddFolderForSync(new DirectoryInfo(file.UniversalPreviewContainer.ContainerPath), filesWhiteList);
                                            }
                                            break;
                                        case FileTypes.MediaPlayerVideo:
                                        case FileTypes.QuickTimeVideo:
                                        case FileTypes.Other:
                                            if (File.Exists(file.OriginalPath))
                                            {
                                                if (!filesWhiteList.Contains(file.OriginalPath))
                                                    filesWhiteList.Add(file.OriginalPath);
                                                if (file.UniversalPreviewContainer != null && !string.IsNullOrEmpty(file.UniversalPreviewContainer.ContainerPath))
                                                    AddFolderForSync(new DirectoryInfo(file.UniversalPreviewContainer.ContainerPath), filesWhiteList);
                                            }
                                            break;
                                        case FileTypes.LineBreak:
                                            break;
                                    }
                                }
                                else
                                    break;
                            }
                            if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
                                break;
                        }
                        if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
                            break;
                    }
                }
                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                    syncManager.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);

                #region Sync Primary Root
                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    sourceSubFolders.Clear();
                    sourceSubFolders.AddRange(salesDepot.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
                    foreach (DirectoryInfo subFolder in sourceSubFolders)
                    {
                        if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
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
                        else
                            break;
                    }
                }
                #endregion

                #region Sync Extra Roots
                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    if (salesDepot.ExtraFolders.Count > 0)
                    {
                        string extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, Constants.ExtraFoldersRootFolderName);
                        if (!Directory.Exists(extraFoldersDestinationRootPath))
                        {
                            Directory.CreateDirectory(extraFoldersDestinationRootPath);
                            syncLog.AppendLine(string.Format("Folder created: {0}", new string[] { extraFoldersDestinationRootPath }));
                            foldersCreated++;
                        }
                        DirectoryInfo extraFoldersDestinationRoot = new DirectoryInfo(extraFoldersDestinationRootPath);
                        destinationSubFolders.Add(extraFoldersDestinationRoot);
                        List<DirectoryInfo> extraFolderDestinations = new List<DirectoryInfo>();
                        if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                        {
                            foreach (RootFolder extraRootFolder in salesDepot.ExtraFolders)
                            {
                                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
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
                                else
                                    break;
                            }
                        }
                        if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                        {
                            foreach (DirectoryInfo subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
                            {
                                ToolClasses.SyncManager.DeleteFolder(subFolder);
                                syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { subFolder.FullName }));
                                foldersDeleted++;
                            }
                        }
                    }
                }
                #endregion
                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName) && !x.FullName.Contains("_gsdata_")))
                    {
                        ToolClasses.SyncManager.DeleteFolder(subFolder);
                        syncLog.AppendLine(string.Format("Folder deleted: {0}", new string[] { subFolder.FullName }));
                        foldersDeleted++;
                    }
                }
            }


            if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
            {
                syncLog.AppendLine(string.Format("Sync completed: {0}", new string[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));
                syncLog.AppendLine(string.Format("Total files created: {0}", new string[] { filesCreated.ToString("#,##0") }));
                syncLog.AppendLine(string.Format("Total files updated: {0}", new string[] { filesUpdated.ToString("#,##0") }));
                syncLog.AppendLine(string.Format("Total files deleted: {0}", new string[] { filesDeleted.ToString("#,##0") }));
                syncLog.AppendLine(string.Format("Total files declined: {0}", new string[] { filesDeclined.ToString("#,##0") }));
                syncLog.AppendLine(string.Format("Total folders created: {0}", new string[] { foldersCreated.ToString("#,##0") }));
                syncLog.AppendLine(string.Format("Total folders deleted: {0}", new string[] { foldersDeleted.ToString("#,##0") }));

                try
                {
                    string logPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LogRootPath, string.Format("iPad Sync at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
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

        public static void MakeFolderAvailable(DirectoryInfo folder)
        {
            try
            {
                foreach (DirectoryInfo subFolder in folder.GetDirectories())
                    MakeFolderAvailable(subFolder);
                foreach (FileInfo file in folder.GetFiles())
                    if (File.Exists(file.FullName))
                        File.SetAttributes(file.FullName, FileAttributes.Normal);
            }
            catch
            {
            }
        }

        public static void DeleteFolder(DirectoryInfo folder, string filter = "")
        {
            try
            {
                MakeFolderAvailable(folder);
                foreach (DirectoryInfo subFolder in folder.GetDirectories())
                    DeleteFolder(subFolder, filter);
                foreach (FileInfo file in folder.GetFiles())
                {
                    try
                    {
                        if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                            File.Delete(file.FullName);
                    }
                    catch
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(100);
                            if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                                File.Delete(file.FullName);
                        }
                        catch
                        {
                        }
                    }
                }
                try
                {
                    if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                        Directory.Delete(folder.FullName, false);
                }
                catch
                {
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                            Directory.Delete(folder.FullName, false);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
    }
}