using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FileManager.ConfigurationClasses;
using FileManager.ToolClasses;
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
			if (!rootFolder.Exists) return;
			LibraryCollection.Clear();
			if (rootFolder.Root.FullName.Equals(rootFolder.FullName) || SettingsManager.Instance.UseDirectAccessToFiles)
				LibraryCollection.Add(new Library(Constants.WholeDriveFilesStorage, rootFolder, SettingsManager.Instance.UseDirectAccessToFiles, SettingsManager.Instance.DirectAccessFileAgeLimit));
			else
			{
				foreach (DirectoryInfo subFolder in rootFolder.GetDirectories())
					LibraryCollection.Add(new Library(subFolder.Name, subFolder, false, SettingsManager.Instance.DirectAccessFileAgeLimit));
			}
		}

		public void Synchronize(Library activeLibrary, bool forseFullSync = false)
		{
			var files = new List<string>();

			var syncLogText = String.Empty;
			RegularSynchronizeLibrary(activeLibrary, out syncLogText);
			try
			{
				var logPath = Path.Combine(SettingsManager.Instance.LogRootPath, string.Format("Library Sync Manual at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
				using (var sw = new StreamWriter(logPath, false))
				{
					sw.Write(syncLogText);
					sw.Flush();
					sw.Close();
				}
				files.Add(logPath);
			}
			catch { }

			if (activeLibrary.FullSync || forseFullSync)
			{
				activeLibrary.PrepareForIPadSynchronize();
				SynchronizeLibraryForIpad(activeLibrary, out syncLogText);
				if (!String.IsNullOrEmpty(syncLogText))
				{
					try
					{
						var logPath = Path.Combine(SettingsManager.Instance.LogRootPath, string.Format("iPad Sync Manual at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
						using (var sw = new StreamWriter(logPath, false))
						{
							sw.Write(syncLogText);
							sw.Flush();
							sw.Close();
						}
						files.Add(logPath);
					}
					catch { }
				}
				files.AddRange(activeLibrary.Folder.GetFiles("*.json").Select(f => f.FullName));
			}

			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
			{
				files.AddRange(activeLibrary.Folder.GetFiles("*.xml").Select(f => f.FullName));
				files.Add(SettingsManager.Instance.SettingsFilePath);
				ArchiveHelper.Instance.ArchiveFiles(files);
			}
		}

		private void RegularSynchronizeLibrary(Library activeLibrary, out string syncLogText)
		{
			syncLogText = null;
			var foldera = SettingsManager.Instance.BackupPath;
			var folderb = SettingsManager.Instance.NetworkPath;
			if (!String.IsNullOrEmpty(foldera) && Directory.Exists(foldera) && !String.IsNullOrEmpty(folderb) && Directory.Exists(folderb))
			{
				var filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				var existedLibraryFolderNames = new List<string>();

				var syncLog = new StringBuilder();
				var filesCreated = 0;
				var filesUpdated = 0;
				var filesDeleted = 0;
				var filesDeclined = 0;
				var foldersCreated = 0;
				var foldersDeleted = 0;
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

				if (activeLibrary.IsConfigured)
				{
					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						activeLibrary.PrepareForRegularSynchronize();

					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					{
						syncLog.AppendLine(string.Format("Sync {0}", new[] { activeLibrary.Name }));

						string salesDepotFolderName = activeLibrary.Folder.FullName.Equals(activeLibrary.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : activeLibrary.Folder.Name;
						existedLibraryFolderNames.Add(salesDepotFolderName);

						var destinationFolder = new DirectoryInfo(Path.Combine(folderb, salesDepotFolderName));
						if (!destinationFolder.Exists)
						{
							destinationFolder.Create();
							syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationFolder.FullName }));
							foldersCreated++;
						}
						filesWhiteList.Clear();

						AddFolderForSync(new DirectoryInfo(Path.Combine(activeLibrary.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
						filesWhiteList.Add(new FileInfo(Path.Combine(activeLibrary.Folder.FullName, Constants.StorageFileName)).FullName);

						var sourceSubFolders = new List<DirectoryInfo>();
						var destinationSubFolders = new List<DirectoryInfo>();

						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							if (!activeLibrary.UseDirectAccess)
							{
								foreach (var page in activeLibrary.Pages)
								{
									foreach (var folder in page.Folders)
									{
										foreach (LibraryLink file in folder.Files)
										{
											if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
											{
												switch (file.Type)
												{
													case FileTypes.Folder:
														AddFolderForSync(new DirectoryInfo(file.OriginalPath), filesWhiteList);
														var libraryFolderLink = file as LibraryFolderLink;
														if (libraryFolderLink != null)
															libraryFolderLink.AllFiles
																.OfType<LibraryLink>()
																.Where(f => (f.Type == FileTypes.FriendlyPresentation ||
																		f.Type == FileTypes.BuggyPresentation ||
																		f.Type == FileTypes.Presentation) &&
																	f.PreviewContainer != null)
																.ToList()
																.ForEach(x => AddFolderForSync(new DirectoryInfo(x.PreviewContainer.ContainerPath), filesWhiteList));
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
										if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
											break;
									}
									if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
										break;
								}

								#region Sync Primary Root
								if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
									syncManager.SynchronizeFolders(activeLibrary.Folder, destinationFolder, filesWhiteList, false);
								if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
								{
									sourceSubFolders.Clear();
									sourceSubFolders.AddRange(activeLibrary.Folder.GetDirectories().Where(x => filesWhiteList.Any(y => Path.GetDirectoryName(y).Contains(x.FullName))));
									foreach (var subFolder in sourceSubFolders)
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
									if (activeLibrary.ExtraFolders.Count > 0)
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
										foreach (RootFolder extraRootFolder in activeLibrary.ExtraFolders)
										{
											if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
											sourceSubFolders.Clear();
											sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Any(y => Path.GetDirectoryName(y).Contains(x.FullName))));
											string extraFolderDestinationPath = Path.Combine(extraFoldersDestinationRoot.FullName, extraRootFolder.RootId.ToString());
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
								syncManager.SynchronizeFolders(activeLibrary.Folder, destinationFolder, filesWhiteList, false);
							}

							#region Sync Overnights Calendar
							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							{
								if (activeLibrary.OvernightsCalendar.Enabled && SettingsManager.Instance.EnableOvernightsCalendarTab)
								{
									var overnightsCalendarDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.OvernightsCalendarRootFolderName);
									if (!Directory.Exists(overnightsCalendarDestinationFolderPath))
									{
										Directory.CreateDirectory(overnightsCalendarDestinationFolderPath);
										syncLog.AppendLine(string.Format("Folder created: {0}", new[] { overnightsCalendarDestinationFolderPath }));
										foldersCreated++;
									}
									var overnightsCalendarDestinationFolder = new DirectoryInfo(overnightsCalendarDestinationFolderPath);
									destinationSubFolders.Add(overnightsCalendarDestinationFolder);
									syncManager.SynchronizeFolders(activeLibrary.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
								}
							}
							#endregion

							#region Sync Program Manager
							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							{
								if (activeLibrary.EnableProgramManagerSync && !string.IsNullOrEmpty(activeLibrary.ProgramManagerLocation) && Directory.Exists(activeLibrary.ProgramManagerLocation))
								{
									var programManagerDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.ProgramManagerRootFolderName);
									if (!Directory.Exists(programManagerDestinationFolderPath))
									{
										Directory.CreateDirectory(programManagerDestinationFolderPath);
										syncLog.AppendLine(string.Format("Folder created: {0}", new[] { programManagerDestinationFolderPath }));
										foldersCreated++;
									}
									var programManagerSourceFolder = new DirectoryInfo(activeLibrary.ProgramManagerLocation);
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
				}
				syncLogText = syncLog.ToString();
			}
		}

		private void SynchronizeLibraryForIpad(Library activeLibrary, out string syncLogText)
		{
			syncLogText = null;
			var filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			var existedLibraryFolderNames = new List<string>();

			var syncLog = new StringBuilder();
			var filesCreated = 0;
			var filesUpdated = 0;
			var filesDeleted = 0;
			var filesDeclined = 0;
			var foldersCreated = 0;
			var foldersDeleted = 0;
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

			if (activeLibrary.IsConfigured)
			{
				syncLog.AppendLine(string.Format("Sync {0}", new[] { activeLibrary.Name }));

				string salesDepotFolderName = activeLibrary.Folder.FullName.Equals(activeLibrary.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : activeLibrary.Folder.Name;
				existedLibraryFolderNames.Add(salesDepotFolderName);

				var destinationFolder = new DirectoryInfo(activeLibrary.IPadManager.SyncDestinationPath);
				if (!destinationFolder.Exists)
				{
					destinationFolder.Create();
					syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationFolder.FullName }));
					foldersCreated++;
				}
				filesWhiteList.Clear();

				AddFolderForSync(new DirectoryInfo(Path.Combine(activeLibrary.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
				filesWhiteList.Add(new FileInfo(Path.Combine(activeLibrary.Folder.FullName, Constants.StorageLightFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(activeLibrary.Folder.FullName, Constants.LibrariesJsonFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(activeLibrary.Folder.FullName, Constants.ReferencesJsonFileName)).FullName);

				var sourceSubFolders = new List<DirectoryInfo>();
				var destinationSubFolders = new List<DirectoryInfo>();

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					foreach (LibraryPage page in activeLibrary.Pages)
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
											var libraryFolderLink = file as LibraryFolderLink;
											if (libraryFolderLink != null)
												libraryFolderLink.AllFiles
													.OfType<LibraryLink>()
													.Where(x => (x.Type == FileTypes.FriendlyPresentation ||
															x.Type == FileTypes.BuggyPresentation ||
															x.Type == FileTypes.Presentation)
														&& x.PreviewContainer != null)
														.ToList()
														.ForEach(x => AddFolderForSync(new DirectoryInfo(x.PreviewContainer.ContainerPath), filesWhiteList));
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
					syncManager.SynchronizeFolders(activeLibrary.Folder, destinationFolder, filesWhiteList, false);

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					sourceSubFolders.Clear();
					sourceSubFolders.AddRange(activeLibrary.Folder.GetDirectories().Where(x => filesWhiteList.Any(y => Path.GetDirectoryName(y).Contains(x.FullName))));
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
					if (activeLibrary.ExtraFolders.Count > 0)
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
						syncManager.SynchronizeFolders(activeLibrary.Folder, destinationFolder, filesWhiteList, false);
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							foreach (RootFolder extraRootFolder in activeLibrary.ExtraFolders)
							{
								if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
								sourceSubFolders.Clear();
								sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Any()));
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
							foreach (DirectoryInfo subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
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
					string previewSourceFolderPath = Path.Combine(activeLibrary.Folder.FullName, Constants.FtpPreviewContainersRootFolderName);
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
			}

			syncLogText = syncLog.ToString();
		}

		private void AddFolderForSync(DirectoryInfo folder, HashSet<string> filesWhiteList)
		{
			if (!folder.Exists) return;
			foreach (DirectoryInfo subFolder in folder.GetDirectories())
				AddFolderForSync(subFolder, filesWhiteList);
			foreach (FileInfo file in folder.GetFiles())
				if (!file.Name.ToLower().Equals("thumbs.db"))
					if (!filesWhiteList.Contains(file.FullName))
						filesWhiteList.Add(file.FullName);
		}
	}
}