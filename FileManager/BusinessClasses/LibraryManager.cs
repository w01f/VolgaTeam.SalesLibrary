using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.BusinessClasses
{
	internal class LibraryManager
	{
		private static readonly LibraryManager _instance = new LibraryManager();

		private LibraryManager()
		{
			LibraryCollection = new List<Library>();
		}

		public List<Library> LibraryCollection { get; set; }

		public static LibraryManager Instance
		{
			get { return _instance; }
		}


		public void LoadLibraries(DirectoryInfo rootFolder)
		{
			if (rootFolder.Exists)
			{
				LibraryCollection.Clear();
				if (rootFolder.Root.FullName.Equals(rootFolder.FullName) || SettingsManager.Instance.UseDirectAccessToFiles)
					LibraryCollection.Add(new Library(Constants.WholeDriveFilesStorage, rootFolder, SettingsManager.Instance.UseDirectAccessToFiles, SettingsManager.Instance.DirectAccessFileAgeLimit));
				else
				{
					foreach (DirectoryInfo subFolder in rootFolder.GetDirectories())
						LibraryCollection.Add(new Library(subFolder.Name, subFolder, false, SettingsManager.Instance.DirectAccessFileAgeLimit));
				}
			}
		}

		public void SynchronizeLibraries()
		{
			AppManager.Instance.KillAutoFM();
			string foldera = SettingsManager.Instance.BackupPath;
			string folderb = SettingsManager.Instance.NetworkPath;
			if (!String.IsNullOrEmpty(foldera) && Directory.Exists(foldera) && !String.IsNullOrEmpty(folderb) && Directory.Exists(folderb))
			{
				var filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				var existedLibraryFolderNames = new List<string>();

				var syncLog = new StringBuilder();
				int filesCreated = 0;
				int filesUpdated = 0;
				int filesDeleted = 0;
				int filesDeclined = 0;
				int foldersCreated = 0;
				int foldersDeleted = 0;
				var syncManager = new SyncManager();
				syncManager.FileCreated += (sender, e) =>
											   {
												   syncLog.AppendLine(string.Format("File created: {0}", new[] { e.Destination }));
												   filesCreated++;
											   };
				syncManager.FileUpdated += (sender, e) =>
											   {
												   syncLog.AppendLine(string.Format("File updated: {0}", new[] { e.Destination }));
												   filesUpdated++;
											   };
				syncManager.FileDeleted += (sender, e) =>
											   {
												   syncLog.AppendLine(string.Format("File deleted: {0}", new[] { e.Destination }));
												   filesDeleted++;
											   };
				syncManager.FileDeclined += (sender, e) =>
												{
													syncLog.AppendLine(string.Format("File declined: {0}", new[] { e.Destination }));
													filesDeclined++;
												};
				syncManager.FolderCreated += (sender, e) =>
												 {
													 syncLog.AppendLine(string.Format("Folder created: {0}", new[] { e.Destination }));
													 foldersCreated++;
												 };
				syncManager.FolderDeleted += (sender, e) =>
												 {
													 syncLog.AppendLine(string.Format("Folder deleted: {0}", new[] { e.Destination }));
													 foldersDeleted++;
												 };

				syncLog.AppendLine(string.Format("Sync started: {0}", new[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));

				foreach (var salesDepot in LibraryCollection)
				{
					if (!salesDepot.IsConfigured) continue;
					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						salesDepot.PrepareForRegularSynchronize();

					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					{
						syncLog.AppendLine(string.Format("Sync {0}", new[] { salesDepot.Name }));

						string salesDepotFolderName = salesDepot.Folder.FullName.Equals(salesDepot.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : salesDepot.Folder.Name;
						existedLibraryFolderNames.Add(salesDepotFolderName);

						var destinationFolder = new DirectoryInfo(Path.Combine(folderb, salesDepotFolderName));
						if (!destinationFolder.Exists)
						{
							destinationFolder.Create();
							syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationFolder.FullName }));
							foldersCreated++;
						}
						filesWhiteList.Clear();

						AddFolderForSync(new DirectoryInfo(Path.Combine(salesDepot.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
						filesWhiteList.Add(new FileInfo(Path.Combine(salesDepot.Folder.FullName, Constants.StorageFileName)).FullName);

						var sourceSubFolders = new List<DirectoryInfo>();
						var destinationSubFolders = new List<DirectoryInfo>();

						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							if (!salesDepot.UseDirectAccess)
							{
								foreach (LibraryPage page in salesDepot.Pages)
								{
									foreach (LibraryFolder folder in page.Folders)
									{
										foreach (LibraryLink file in folder.Files)
										{
											if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
											{
												switch (file.Type)
												{
													case FileTypes.Folder:
														AddFolderForSync(new DirectoryInfo(file.OriginalPath), filesWhiteList);
														(file as LibraryFolderLink).GetWholeContent().Where(x => (x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.Presentation) && x.PreviewContainer != null).ToList().ForEach(x => AddFolderForSync(new DirectoryInfo(x.PreviewContainer.ContainerPath), filesWhiteList));
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
												if (file.AttachmentProperties.Enable)
												{
													foreach (LinkAttachment attachment in file.AttachmentProperties.FilesAttachments)
													{
														if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
														{
															if (attachment.IsSourceAvailable)
																filesWhiteList.Add(attachment.DestinationPath);
														}
														else
															break;
													}
												}
											}
											else
												break;
										}
										if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
											break;
									}
									if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
										break;
								}

								#region Sync Primary Root
								if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
									syncManager.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);
								if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
								{
									sourceSubFolders.Clear();
									sourceSubFolders.AddRange(salesDepot.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
									foreach (DirectoryInfo subFolder in sourceSubFolders)
									{
										if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
										{
											string destinationSubFolderPath = Path.Combine(destinationFolder.FullName, subFolder.Name);
											if (!Directory.Exists(destinationSubFolderPath))
											{
												Directory.CreateDirectory(destinationSubFolderPath);
												syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationSubFolderPath }));
												foldersCreated++;
											}
											var destinationSubFolder = new DirectoryInfo(destinationSubFolderPath);
											destinationSubFolders.Add(destinationSubFolder);
											syncManager.SynchronizeFolders(subFolder, destinationSubFolder, filesWhiteList);
										}
										else
											break;
									}
								}
								#endregion

								#region Sync Extra Roots
								if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
								{
									if (salesDepot.ExtraFolders.Count > 0)
									{
										string extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, Constants.ExtraFoldersRootFolderName);
										if (!Directory.Exists(extraFoldersDestinationRootPath))
										{
											Directory.CreateDirectory(extraFoldersDestinationRootPath);
											syncLog.AppendLine(string.Format("Folder created: {0}", new[] { extraFoldersDestinationRootPath }));
											foldersCreated++;
										}
										var extraFoldersDestinationRoot = new DirectoryInfo(extraFoldersDestinationRootPath);
										destinationSubFolders.Add(extraFoldersDestinationRoot);
										var extraFolderDestinations = new List<DirectoryInfo>();
										foreach (var extraRootFolder in salesDepot.ExtraFolders)
										{
											if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
											sourceSubFolders.Clear();
											sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
											var extraFolderDestinationPath = Path.Combine(extraFoldersDestinationRoot.FullName, extraRootFolder.RootId.ToString());
											if (!Directory.Exists(extraFolderDestinationPath))
											{
												Directory.CreateDirectory(extraFolderDestinationPath);
												syncLog.AppendLine(string.Format("Folder created: {0}", new[] { extraFolderDestinationPath }));
												foldersCreated++;
											}
											var extraFolderDestination = new DirectoryInfo(extraFolderDestinationPath);
											syncManager.SynchronizeFolders(extraRootFolder.Folder, extraFolderDestination, filesWhiteList);
											if (extraFolderDestination.GetFiles().Length > 0 || extraFolderDestination.GetDirectories().Length > 0)
												extraFolderDestinations.Add(extraFolderDestination);
										}
										if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
										{
											foreach (DirectoryInfo subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
											{
												if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
												{
													SyncManager.DeleteFolder(subFolder);
													syncLog.AppendLine(string.Format("Folder deleted: {0}", new[] { subFolder.FullName }));
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
							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							{
								if (salesDepot.OvernightsCalendar.Enabled && SettingsManager.Instance.EnableOvernightsCalendarTab)
								{
									string overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.OvernightsCalendarRootFolderName);
									if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
									{
										Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
										syncLog.AppendLine(string.Format("Folder created: {0}", new[] { overnightsCalendarDestinationFolderPath }));
										foldersCreated++;
									}
									var overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
									destinationSubFolders.Add(overnightsCalendarDestinationFolder);
									syncManager.SynchronizeFolders(salesDepot.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
								}
							}
							#endregion

							#region Sync Program Manager
							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							{
								if (salesDepot.EnableProgramManagerSync && !string.IsNullOrEmpty(salesDepot.ProgramManagerLocation) && Directory.Exists(salesDepot.ProgramManagerLocation))
								{
									string programManagerDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.ProgramManagerRootFolderName);
									if (!Directory.Exists(programManagerDestinationFolderPath))
									{
										Directory.CreateDirectory(programManagerDestinationFolderPath);
										syncLog.AppendLine(string.Format("Folder created: {0}", new[] { programManagerDestinationFolderPath }));
										foldersCreated++;
									}
									var programManagerSourceFolder = new DirectoryInfo(salesDepot.ProgramManagerLocation);
									var programManagerDestinationFolder = new DirectoryInfo(programManagerDestinationFolderPath);
									destinationSubFolders.Add(programManagerDestinationFolder);
									syncManager.SynchronizeFolders(programManagerSourceFolder, programManagerDestinationFolder, new HashSet<string>());
								}
							}
							#endregion

							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							{
								foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName)))
								{
									SyncManager.DeleteFolder(subFolder);
									syncLog.AppendLine(string.Format("Folder deleted: {0}", new[] { subFolder.FullName }));
									foldersDeleted++;
								}
							}
						}
					}
				}

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var networkFolder = new DirectoryInfo(folderb);
					foreach (DirectoryInfo folder in networkFolder.GetDirectories())
						if (!existedLibraryFolderNames.Contains(folder.Name))
						{
							SyncManager.DeleteFolder(folder);
							syncLog.AppendLine(string.Format("Folder deleted: {0}", new[] { folder.FullName }));
							foldersDeleted++;
						}

					syncLog.AppendLine(string.Format("Sync completed: {0}", new[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));
					syncLog.AppendLine(string.Format("Total files created: {0}", new[] { filesCreated.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total files updated: {0}", new[] { filesUpdated.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total files deleted: {0}", new[] { filesDeleted.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total files declined: {0}", new[] { filesDeclined.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total folders created: {0}", new[] { foldersCreated.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total folders deleted: {0}", new[] { foldersDeleted.ToString("#,##0") }));

					try
					{
						string logPath = Path.Combine(SettingsManager.Instance.LogRootPath, string.Format("Library Sync Manual at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
						using (var sw = new StreamWriter(logPath, false))
						{
							sw.Write(syncLog.ToString());
							sw.Flush();
							sw.Close();
						}
					}
					catch { }
				}
			}

			AppManager.Instance.RunAutoFM();
		}

		public void SynchronizeLibraryForIpad(Library salesDepot)
		{
			var filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			var existedLibraryFolderNames = new List<string>();

			var syncLog = new StringBuilder();
			int filesCreated = 0;
			int filesUpdated = 0;
			int filesDeleted = 0;
			int filesDeclined = 0;
			int foldersCreated = 0;
			int foldersDeleted = 0;
			var syncManager = new SyncManager();
			syncManager.FileCreated += (sender, e) =>
										   {
											   syncLog.AppendLine(string.Format("File created: {0}", new[] { e.Destination }));
											   filesCreated++;
										   };
			syncManager.FileUpdated += (sender, e) =>
										   {
											   syncLog.AppendLine(string.Format("File updated: {0}", new[] { e.Destination }));
											   filesUpdated++;
										   };
			syncManager.FileDeleted += (sender, e) =>
										   {
											   syncLog.AppendLine(string.Format("File deleted: {0}", new[] { e.Destination }));
											   filesDeleted++;
										   };
			syncManager.FileDeclined += (sender, e) =>
											{
												syncLog.AppendLine(string.Format("File declined: {0}", new[] { e.Destination }));
												filesDeclined++;
											};
			syncManager.FolderCreated += (sender, e) =>
											 {
												 syncLog.AppendLine(string.Format("Folder created: {0}", new[] { e.Destination }));
												 foldersCreated++;
											 };
			syncManager.FolderDeleted += (sender, e) =>
											 {
												 syncLog.AppendLine(string.Format("Folder deleted: {0}", new[] { e.Destination }));
												 foldersDeleted++;
											 };

			syncLog.AppendLine(string.Format("Sync started: {0}", new[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));

			if (salesDepot.IsConfigured)
			{
				syncLog.AppendLine(string.Format("Sync {0}", new[] { salesDepot.Name }));

				string salesDepotFolderName = salesDepot.Folder.FullName.Equals(salesDepot.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : salesDepot.Folder.Name;
				existedLibraryFolderNames.Add(salesDepotFolderName);

				var destinationFolder = new DirectoryInfo(salesDepot.IPadManager.SyncDestinationPath);
				if (!destinationFolder.Exists)
				{
					destinationFolder.Create();
					syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationFolder.FullName }));
					foldersCreated++;
				}
				filesWhiteList.Clear();

				AddFolderForSync(new DirectoryInfo(Path.Combine(salesDepot.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
				filesWhiteList.Add(new FileInfo(Path.Combine(salesDepot.Folder.FullName, Constants.StorageLightFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(salesDepot.Folder.FullName, Constants.LibrariesJsonFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(salesDepot.Folder.FullName, Constants.ReferencesJsonFileName)).FullName);

				var sourceSubFolders = new List<DirectoryInfo>();
				var destinationSubFolders = new List<DirectoryInfo>();

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					foreach (LibraryPage page in salesDepot.Pages)
					{
						foreach (LibraryFolder folder in page.Folders)
						{
							foreach (LibraryLink file in folder.Files)
							{
								if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
								{
									switch (file.Type)
									{
										case FileTypes.Folder:
											AddFolderForSync(new DirectoryInfo(file.OriginalPath), filesWhiteList);
											(file as LibraryFolderLink).GetWholeContent().Where(x => (x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.Presentation) && x.PreviewContainer != null).ToList().ForEach(x => AddFolderForSync(new DirectoryInfo(x.PreviewContainer.ContainerPath), filesWhiteList));
											break;
										case FileTypes.BuggyPresentation:
										case FileTypes.FriendlyPresentation:
										case FileTypes.Presentation:
											if (File.Exists(file.OriginalPath))
											{
												if (!filesWhiteList.Contains(file.OriginalPath))
													filesWhiteList.Add(file.OriginalPath);
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
									if (file.AttachmentProperties.Enable)
									{
										foreach (LinkAttachment attachment in file.AttachmentProperties.FilesAttachments)
										{
											if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
											{
												if (attachment.IsSourceAvailable)
													filesWhiteList.Add(attachment.DestinationPath);
											}
											else
												break;
										}
									}
								}
								else
									break;
							}
							if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
								break;
						}
						if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
							break;
					}
				}

				#region Sync Primary Root
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					syncManager.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					sourceSubFolders.Clear();
					sourceSubFolders.AddRange(salesDepot.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
					foreach (DirectoryInfo subFolder in sourceSubFolders)
					{
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							string destinationSubFolderPath = Path.Combine(destinationFolder.FullName, subFolder.Name);
							if (!Directory.Exists(destinationSubFolderPath))
							{
								Directory.CreateDirectory(destinationSubFolderPath);
								syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationSubFolderPath }));
								foldersCreated++;
							}
							var destinationSubFolder = new DirectoryInfo(destinationSubFolderPath);
							destinationSubFolders.Add(destinationSubFolder);
							syncManager.SynchronizeFolders(subFolder, destinationSubFolder, filesWhiteList);
						}
						else
							break;
					}
				}
				#endregion

				#region Sync Extra Roots
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					if (salesDepot.ExtraFolders.Count > 0)
					{
						var extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, Constants.ExtraFoldersRootFolderName);
						if (!Directory.Exists(extraFoldersDestinationRootPath))
						{
							Directory.CreateDirectory(extraFoldersDestinationRootPath);
							syncLog.AppendLine(string.Format("Folder created: {0}", new[] { extraFoldersDestinationRootPath }));
							foldersCreated++;
						}
						var extraFoldersDestinationRoot = new DirectoryInfo(extraFoldersDestinationRootPath);
						destinationSubFolders.Add(extraFoldersDestinationRoot);
						var extraFolderDestinations = new List<DirectoryInfo>();
						syncManager.SynchronizeFolders(salesDepot.Folder, destinationFolder, filesWhiteList, false);
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							foreach (var extraRootFolder in salesDepot.ExtraFolders)
							{
								if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
								sourceSubFolders.Clear();
								sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
								var extraFolderDestinationPath = Path.Combine(extraFoldersDestinationRoot.FullName, extraRootFolder.RootId.ToString());
								if (!Directory.Exists(extraFolderDestinationPath))
								{
									Directory.CreateDirectory(extraFolderDestinationPath);
									syncLog.AppendLine(string.Format("Folder created: {0}", new[] { extraFolderDestinationPath }));
									foldersCreated++;
								}
								var extraFolderDestination = new DirectoryInfo(extraFolderDestinationPath);
								syncManager.SynchronizeFolders(extraRootFolder.Folder, extraFolderDestination, filesWhiteList);
								if (extraFolderDestination.GetFiles().Length > 0 || extraFolderDestination.GetDirectories().Length > 0)
									extraFolderDestinations.Add(extraFolderDestination);
							}
						}
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							foreach (var subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
							{
								SyncManager.DeleteFolder(subFolder);
								syncLog.AppendLine(string.Format("Folder deleted: {0}", new[] { subFolder.FullName }));
								foldersDeleted++;
							}
						}
					}
				}
				#endregion

				#region Sync Preview Containers
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					string previewSourceFolderPath = Path.Combine(salesDepot.Folder.FullName, Constants.FtpPreviewContainersRootFolderName);
					var previewSourceFolder = new DirectoryInfo(previewSourceFolderPath);
					if (previewSourceFolder.Exists)
					{
						string previewDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.FtpPreviewContainersRootFolderName);
						if (!Directory.Exists(previewDestinationFolderPath))
						{
							Directory.CreateDirectory(previewDestinationFolderPath);
							syncLog.AppendLine(string.Format("Folder created: {0}", new[] { previewDestinationFolderPath }));
							foldersCreated++;
						}
						var previewDestinationFolder = new DirectoryInfo(previewDestinationFolderPath);
						destinationSubFolders.Add(previewDestinationFolder);
						syncManager.SynchronizeFolders(previewSourceFolder, previewDestinationFolder, new HashSet<string>());
					}
				}
				#endregion

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					foreach (DirectoryInfo subFolder in destinationFolder.GetDirectories().Where(x => !destinationSubFolders.Select(y => y.FullName).Contains(x.FullName) && !x.FullName.Contains("_gsdata_")))
					{
						SyncManager.DeleteFolder(subFolder);
						syncLog.AppendLine(string.Format("Folder deleted: {0}", new[] { subFolder.FullName }));
						foldersDeleted++;
					}
				}
			}

			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
			{
				syncLog.AppendLine(string.Format("Sync completed: {0}", new[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));
				syncLog.AppendLine(string.Format("Total files created: {0}", new[] { filesCreated.ToString("#,##0") }));
				syncLog.AppendLine(string.Format("Total files updated: {0}", new[] { filesUpdated.ToString("#,##0") }));
				syncLog.AppendLine(string.Format("Total files deleted: {0}", new[] { filesDeleted.ToString("#,##0") }));
				syncLog.AppendLine(string.Format("Total files declined: {0}", new[] { filesDeclined.ToString("#,##0") }));
				syncLog.AppendLine(string.Format("Total folders created: {0}", new[] { foldersCreated.ToString("#,##0") }));
				syncLog.AppendLine(string.Format("Total folders deleted: {0}", new[] { foldersDeleted.ToString("#,##0") }));

				try
				{
					string logPath = Path.Combine(SettingsManager.Instance.LogRootPath, string.Format("iPad Sync Manual at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
					using (var sw = new StreamWriter(logPath, false))
					{
						sw.Write(syncLog.ToString());
						sw.Flush();
						sw.Close();
					}
				}
				catch { }
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
			catch { }
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
							Thread.Sleep(100);
							if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
								File.Delete(file.FullName);
						}
						catch { }
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
						Thread.Sleep(100);
						if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
							Directory.Delete(folder.FullName, false);
					}
					catch { }
				}
			}
			catch { }
		}
	}
}