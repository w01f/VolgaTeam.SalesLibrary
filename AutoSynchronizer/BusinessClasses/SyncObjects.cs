using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoSynchronizer.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;
using Timer = System.Threading.Timer;

namespace AutoSynchronizer.BusinessClasses
{
	public class LibrarySynchronizer
	{
		private Timer _timer;

		public LibrarySynchronizer(LibraryWrapper manager)
		{
			Manager = manager;
			ScheduleNextSync();
		}

		#region Schedule Methods
		private DateTime[] GetSyncDatesFromTimePoints(DateTime now)
		{
			var result = new List<DateTime>();

			foreach (SyncScheduleRecord sheduleRecord in Manager.Library.SyncScheduleRecords)
			{
				var syncDate = new DateTime(now.Year, now.Month, now.Day, sheduleRecord.Time.Hour, sheduleRecord.Time.Minute, 0);

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
			var totalMilliseconds = (long)difference.TotalMilliseconds;
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
			if (Manager.Library.EnableAutoSync && Manager.Library.SyncScheduleRecords.Count > 0)
			{
				_timer = new Timer(delegate
				{
					if (!AppManager.Instance.FileManagerActive())
					{
						Globals.ThreadActive = true;
						Globals.ThreadAborted = false;
						SyncInProgress = true;

						FormHidden.Instance.Invoke((MethodInvoker)delegate { FormHidden.Instance.ShowSyncProgressRegular(); });
						SynchronizeRegular();
						FormHidden.Instance.Invoke((MethodInvoker)delegate { FormHidden.Instance.HideSyncProgressRegular(); });

						if (!String.IsNullOrEmpty(Manager.Library.IPadManager.SyncDestinationPath) && ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
						{
							FormHidden.Instance.Invoke((MethodInvoker)delegate { FormHidden.Instance.ShowSyncProgressIpad(); });
							SynchronizeForIpad();
							FormHidden.Instance.Invoke((MethodInvoker)delegate { FormHidden.Instance.HideSyncProgressIpad(); });
						}

						SyncInProgress = false;
						Globals.ThreadActive = false;
						Globals.ThreadAborted = false;
					}
					Manager.InitServiceObjects();
				}, null, GetMillisecondsForNextSync(), Timeout.Infinite);
			}
		}
		#endregion

		#region Sync Methods
		public void SynchronizeRegular()
		{
			string foldera = SettingsManager.Instance.BackupPath;
			string folderb = SettingsManager.Instance.NetworkPath;
			if (Manager.Library.IsConfigured && !String.IsNullOrEmpty(foldera) && Directory.Exists(foldera) && !String.IsNullOrEmpty(folderb) && Directory.Exists(folderb))
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
				syncLog.AppendLine(string.Format("Sync {0}", new[] { Manager.Library.Name }));

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					Manager.Library.PrepareForRegularSynchronize();

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					syncLog.AppendLine(string.Format("Sync {0}", new[] { Manager.Library.Name }));

					string salesDepotFolderName = Manager.Library.Folder.FullName.Equals(Manager.Library.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : Manager.Library.Folder.Name;
					existedLibraryFolderNames.Add(salesDepotFolderName);

					var destinationFolder = new DirectoryInfo(Path.Combine(folderb, salesDepotFolderName));
					if (!destinationFolder.Exists)
					{
						destinationFolder.Create();
						syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationFolder.FullName }));
						foldersCreated++;
					}
					filesWhiteList.Clear();

					AddFolderForSync(new DirectoryInfo(Path.Combine(Manager.Library.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
					filesWhiteList.Add(new FileInfo(Path.Combine(Manager.Library.Folder.FullName, Constants.StorageFileName)).FullName);

					var sourceSubFolders = new List<DirectoryInfo>();
					var destinationSubFolders = new List<DirectoryInfo>();

					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					{
						if (!Manager.Library.UseDirectAccess)
						{
							foreach (LibraryPage page in Manager.Library.Pages)
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
										}
									}
								}
							}

							#region Sync Primary Root
							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
								syncManager.SynchronizeFolders(Manager.Library.Folder, destinationFolder, filesWhiteList, false);
							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							{
								sourceSubFolders.Clear();
								sourceSubFolders.AddRange(Manager.Library.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
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
								}
							}
							#endregion

							#region Sync Extra Roots
							if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							{
								if (Manager.Library.ExtraFolders.Count > 0)
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
									foreach (RootFolder extraRootFolder in Manager.Library.ExtraFolders)
									{
										if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
										sourceSubFolders.Clear();
										sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
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
							syncManager.SynchronizeFolders(Manager.Library.Folder, destinationFolder, filesWhiteList, false);
						}

						#region Sync Overnights Calendar
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							if (Manager.Library.OvernightsCalendar.Enabled)
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
								syncManager.SynchronizeFolders(Manager.Library.OvernightsCalendar.RootFolder, overnightsCalendarDestinationFolder, new HashSet<string>());
							}
						}
						#endregion

						#region Sync Program Manager
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							if (Manager.Library.EnableProgramManagerSync && !string.IsNullOrEmpty(Manager.Library.ProgramManagerLocation) && Directory.Exists(Manager.Library.ProgramManagerLocation))
							{
								string programManagerDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.ProgramManagerRootFolderName);
								if (!Directory.Exists(programManagerDestinationFolderPath))
								{
									Directory.CreateDirectory(programManagerDestinationFolderPath);
									syncLog.AppendLine(string.Format("Folder created: {0}", new[] { programManagerDestinationFolderPath }));
									foldersCreated++;
								}
								var programManagerSourceFolder = new DirectoryInfo(Manager.Library.ProgramManagerLocation);
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
						string logPath = Path.Combine(SettingsManager.Instance.LogRootPath, string.Format("Library Sync Auto at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
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
			else
			{
				MessageBox.Show("Sales Libarary is not configured");
				Globals.ThreadAborted = true;
			}
		}

		public void SynchronizeForIpad()
		{
			if (!String.IsNullOrEmpty(Manager.Library.IPadManager.SyncDestinationPath) && Directory.Exists(Manager.Library.IPadManager.SyncDestinationPath))
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

				syncLog.AppendLine(string.Format("Sync {0}", new[] { Manager.Library.Name }));

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					Manager.Library.PrepareForIPadSynchronize();

				string salesDepotFolderName = Manager.Library.Folder.FullName.Equals(Manager.Library.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : Manager.Library.Folder.Name;
				existedLibraryFolderNames.Add(salesDepotFolderName);

				var destinationFolder = new DirectoryInfo(Manager.Library.IPadManager.SyncDestinationPath);
				if (!destinationFolder.Exists)
				{
					destinationFolder.Create();
					syncLog.AppendLine(string.Format("Folder created: {0}", new[] { destinationFolder.FullName }));
					foldersCreated++;
				}
				filesWhiteList.Clear();

				AddFolderForSync(new DirectoryInfo(Path.Combine(Manager.Library.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
				filesWhiteList.Add(new FileInfo(Path.Combine(Manager.Library.Folder.FullName, Constants.StorageLightFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(Manager.Library.Folder.FullName, Constants.LibrariesJsonFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(Manager.Library.Folder.FullName, Constants.ReferencesJsonFileName)).FullName);

				var sourceSubFolders = new List<DirectoryInfo>();
				var destinationSubFolders = new List<DirectoryInfo>();

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					foreach (LibraryPage page in Manager.Library.Pages)
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
								}
							}
						}
					}
				}

				#region Sync Primary Root
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					syncManager.SynchronizeFolders(Manager.Library.Folder, destinationFolder, filesWhiteList, false);
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					sourceSubFolders.Clear();
					sourceSubFolders.AddRange(Manager.Library.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
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
					if (Manager.Library.ExtraFolders.Count > 0)
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
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							foreach (RootFolder extraRootFolder in Manager.Library.ExtraFolders)
							{
								if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
								sourceSubFolders.Clear();
								sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0));
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
					string previewSourceFolderPath = Path.Combine(Manager.Library.Folder.FullName, Constants.FtpPreviewContainersRootFolderName);
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
					syncLog.AppendLine(string.Format("Sync completed: {0}", new[] { DateTime.Now.ToString("MM/dd/yy h:mm tt") }));
					syncLog.AppendLine(string.Format("Total files created: {0}", new[] { filesCreated.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total files updated: {0}", new[] { filesUpdated.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total files deleted: {0}", new[] { filesDeleted.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total files declined: {0}", new[] { filesDeclined.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total folders created: {0}", new[] { foldersCreated.ToString("#,##0") }));
					syncLog.AppendLine(string.Format("Total folders deleted: {0}", new[] { foldersDeleted.ToString("#,##0") }));

					try
					{
						string logPath = Path.Combine(SettingsManager.Instance.LogRootPath, string.Format("iPad Sync Auto at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
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
			else
			{
				MessageBox.Show("Sales Libarary is not configured");
				Globals.ThreadAborted = true;
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

		public LibraryWrapper Manager { get; private set; }

		public bool SyncInProgress { get; private set; }
	}
}