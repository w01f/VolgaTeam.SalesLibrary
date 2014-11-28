using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.Cloner
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		private FormProgressSyncSource _formSyncSource = null;
		private FormProgressSyncFilesRegular _formSyncRegular = null;
		private FormProgressSyncFilesIPad _formSyncIpad = null;

		public string SourceFilesPath
		{
			get
			{
				return buttonEditSource.EditValue != null ? buttonEditSource.EditValue.ToString() : string.Empty;
			}
		}

		public string DestinationFilesPath
		{
			get
			{
				return buttonEditDestination.EditValue != null ? buttonEditDestination.EditValue.ToString() : string.Empty;
			}
		}

		public string DestinationRegularSync
		{
			get
			{
				return buttonEditDestinationRegularSync.EditValue != null ? buttonEditDestinationRegularSync.EditValue.ToString() : string.Empty;
			}
		}

		public string DestinationIPadSync
		{
			get
			{
				return buttonEditDestinationIPadSync.EditValue != null ? buttonEditDestinationIPadSync.EditValue.ToString() : string.Empty;
			}
		}

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		private FormMain()
		{
			InitializeComponent();
		}

		#region Methods
		private void Convert()
		{
			if (!string.IsNullOrEmpty(this.SourceFilesPath) && Directory.Exists(this.SourceFilesPath) && !string.IsNullOrEmpty(this.DestinationFilesPath) && Directory.Exists(this.DestinationFilesPath))
			{
				var thread = new System.Threading.Thread(delegate()
				{
					Globals.ThreadActive = true;
					Globals.ThreadAborted = false;

					this.Invoke((MethodInvoker)ShowSyncProgressSource);

					var sourceFolder = new DirectoryInfo(this.SourceFilesPath);
					var destinationFolder = new DirectoryInfo(this.DestinationFilesPath);
					var sourceLibrary = new Library(sourceFolder.Name, sourceFolder);
					sourceLibrary.Init();
					var destinationLibrary = sourceLibrary.Clone(destinationFolder.Name, destinationFolder) as Library;
					destinationLibrary.IPadManager.SyncDestinationPath = this.DestinationIPadSync;

					CopySourceFiles();

					destinationLibrary.Save();
					destinationLibrary.SaveLight();
					this.Invoke((MethodInvoker)HideSyncProgressSource);

					if (!String.IsNullOrEmpty(destinationLibrary.IPadManager.SyncDestinationPath) && ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
					{
						this.Invoke((MethodInvoker)ShowSyncProgressIpad);
						SynchronizeForIpad(destinationLibrary);
						this.Invoke((MethodInvoker)HideSyncProgressIpad);
					}

					Globals.ThreadActive = false;
					Globals.ThreadAborted = false;
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();
			}
			else
			{
				MessageBox.Show("Source or Destination folder are wrong", "SD Cloner", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		#region Sync Methods
		private void CopySourceFiles()
		{
			SyncManager.DeleteFolder(new DirectoryInfo(this.DestinationFilesPath));
			Directory.CreateDirectory(this.DestinationFilesPath);

			string[] serviceFolders = { "!Attachments", "!QV", "!WV" };
			string[] serviceFiles = { Constants.StorageFileName, Constants.StorageLightFileName, Constants.LibrariesJsonFileName, Constants.ReferencesJsonFileName };
			SyncManager syncManager = new SyncManager();
			foreach (var subfolder in new DirectoryInfo(this.SourceFilesPath).GetDirectories())
			{
				if (!serviceFolders.Contains(subfolder.Name))
				{
					string destinationPath = Path.Combine(this.DestinationFilesPath, subfolder.Name);
					if (!Directory.Exists(destinationPath))
						Directory.CreateDirectory(destinationPath);
					syncManager.SynchronizeFolders(subfolder, new DirectoryInfo(destinationPath), new HashSet<string>());
				}
			}
			foreach (var file in new DirectoryInfo(this.SourceFilesPath).GetFiles())
			{
				if (!serviceFiles.Contains(file.Name))
					file.CopyTo(Path.Combine(this.DestinationFilesPath, file.Name), true);
			}
		}

		private void SynchronizeForIpad(Library library)
		{
			if (!String.IsNullOrEmpty(library.IPadManager.SyncDestinationPath) && Directory.Exists(library.IPadManager.SyncDestinationPath))
			{
				var filesWhiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				var existedLibraryFolderNames = new List<string>();

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					library.PrepareForIPadSynchronize();

				var syncManager = new SyncManager();
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					library.PrepareForRegularSynchronize();

				string salesDepotFolderName = library.Folder.FullName.Equals(library.Folder.Root.FullName) ? Constants.WholeDriveFilesStorage : library.Folder.Name;
				existedLibraryFolderNames.Add(salesDepotFolderName);

				var destinationFolder = new DirectoryInfo(library.IPadManager.SyncDestinationPath);
				if (!destinationFolder.Exists)
				{
					destinationFolder.Create();
				}
				filesWhiteList.Clear();

				AddFolderForSync(new DirectoryInfo(Path.Combine(library.Folder.FullName, Constants.LibraryLogoFolder)), filesWhiteList);
				filesWhiteList.Add(new FileInfo(Path.Combine(library.Folder.FullName, Constants.StorageLightFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(library.Folder.FullName, Constants.LibrariesJsonFileName)).FullName);
				filesWhiteList.Add(new FileInfo(Path.Combine(library.Folder.FullName, Constants.ReferencesJsonFileName)).FullName);

				var sourceSubFolders = new List<DirectoryInfo>();
				var destinationSubFolders = new List<DirectoryInfo>();

				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					foreach (LibraryPage page in library.Pages)
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
											(file as LibraryFolderLink).AllFiles.OfType<LibraryLink>().Where(x => (x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.Presentation) && x.PreviewContainer != null).ToList().ForEach(x => AddFolderForSync(new DirectoryInfo(x.PreviewContainer.ContainerPath), filesWhiteList));
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
					syncManager.SynchronizeFolders(library.Folder, destinationFolder, filesWhiteList, false);
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					sourceSubFolders.Clear();
					sourceSubFolders.AddRange(library.Folder.GetDirectories().Where(x => filesWhiteList.Any(y => Path.GetDirectoryName(y).Contains(x.FullName))));
					foreach (var subFolder in sourceSubFolders)
					{
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						{
							string destinationSubFolderPath = Path.Combine(destinationFolder.FullName, subFolder.Name);
							if (!Directory.Exists(destinationSubFolderPath))
							{
								Directory.CreateDirectory(destinationSubFolderPath);
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
					if (library.ExtraFolders.Count > 0)
					{
						string extraFoldersDestinationRootPath = Path.Combine(destinationFolder.FullName, Constants.ExtraFoldersRootFolderName);
						if (!Directory.Exists(extraFoldersDestinationRootPath))
						{
							Directory.CreateDirectory(extraFoldersDestinationRootPath);
						}
						var extraFoldersDestinationRoot = new DirectoryInfo(extraFoldersDestinationRootPath);
						destinationSubFolders.Add(extraFoldersDestinationRoot);
						var extraFolderDestinations = new List<DirectoryInfo>();
						foreach (var extraRootFolder in library.ExtraFolders)
						{
							if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
							sourceSubFolders.Clear();
							sourceSubFolders.AddRange(extraRootFolder.Folder.GetDirectories().Where(x => filesWhiteList.Any(y => Path.GetDirectoryName(y).Contains(x.FullName))));
							var extraFolderDestinationPath = Path.Combine(extraFoldersDestinationRoot.FullName, extraRootFolder.RootId.ToString());
							if (!Directory.Exists(extraFolderDestinationPath))
								Directory.CreateDirectory(extraFolderDestinationPath);
							var extraFolderDestination = new DirectoryInfo(extraFolderDestinationPath);
							syncManager.SynchronizeFolders(extraRootFolder.Folder, extraFolderDestination, filesWhiteList);
							if (extraFolderDestination.GetFiles().Length > 0 || extraFolderDestination.GetDirectories().Length > 0)
								extraFolderDestinations.Add(extraFolderDestination);
						}
						foreach (DirectoryInfo subFolder in extraFoldersDestinationRoot.GetDirectories().Where(x => !extraFolderDestinations.Select(y => y.FullName).Contains(x.FullName)))
						{
							SyncManager.DeleteFolder(subFolder);
						}
					}
				}
				#endregion

				#region Sync Preview Containers
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					string previewSourceFolderPath = Path.Combine(library.Folder.FullName, Constants.FtpPreviewContainersRootFolderName);
					var previewSourceFolder = new DirectoryInfo(previewSourceFolderPath);
					if (previewSourceFolder.Exists)
					{
						string previewDestinationFolderPath = Path.Combine(destinationFolder.FullName, Constants.FtpPreviewContainersRootFolderName);
						if (!Directory.Exists(previewDestinationFolderPath))
						{
							Directory.CreateDirectory(previewDestinationFolderPath);
						}
						var previewDestinationFolder = new DirectoryInfo(previewDestinationFolderPath);
						destinationSubFolders.Add(previewDestinationFolder);
						syncManager.SynchronizeFolders(previewSourceFolder, previewDestinationFolder, new HashSet<string>());
					}
				}
				#endregion
			}
			else
			{
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

		public void ShowSyncProgressSource()
		{
			_formSyncSource = new FormProgressSyncSource();
			_formSyncSource.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
			{
				Globals.ThreadAborted = true;
			});
			_formSyncSource.Show();
		}

		public void HideSyncProgressSource()
		{
			_formSyncSource.Close();
			_formSyncSource = null;
		}

		public void ShowSyncProgressRegular()
		{
			_formSyncRegular = new FormProgressSyncFilesRegular();
			_formSyncRegular.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
			{
				Globals.ThreadAborted = true;
			});
			_formSyncRegular.Show();
		}

		public void HideSyncProgressRegular()
		{
			_formSyncRegular.Close();
			_formSyncRegular = null;
		}

		public void ShowSyncProgressIpad()
		{
			_formSyncIpad = new FormProgressSyncFilesIPad();
			_formSyncIpad.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
			{
				Globals.ThreadAborted = true;
			});
			_formSyncIpad.Show();
		}

		public void HideSyncProgressIpad()
		{
			_formSyncIpad.Close();
			_formSyncIpad = null;
		}
		#endregion

		private void simpleButtonSplit_Click(object sender, EventArgs e)
		{
			this.Enabled = false;
			Convert();
			this.Enabled = true;
		}

		private void buttonEditSource_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditSource.EditValue != null)
					dialog.SelectedPath = buttonEditSource.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditSource.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void buttonEditDestination_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditDestination.EditValue != null)
					dialog.SelectedPath = buttonEditDestination.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditDestination.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void buttonEditDestinationRegularSync_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditDestinationRegularSync.EditValue != null)
					dialog.SelectedPath = buttonEditDestinationRegularSync.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditDestinationRegularSync.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void buttonEditDestinationIPadSync_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditDestinationIPadSync.EditValue != null)
					dialog.SelectedPath = buttonEditDestinationIPadSync.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditDestinationIPadSync.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void simpleButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
