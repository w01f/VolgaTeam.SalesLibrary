using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.Floater;
using SalesDepot.InteropClasses;
using SalesDepot.ToolForms;
using SalesDepot.ToolForms.QBuilderForms;
using SalesDepot.ToolForms.WallBin;

namespace SalesDepot.BusinessClasses
{
	public class LinkManager
	{
		private static LinkManager _instance;

		private readonly FormPowerPointQuickView _formPowerPointQuickView;
		private readonly FormPowerPointQuickViewOld _formPowerPointQuickViewOld;
		private readonly FormAddLink _formQuickSiteAddLink;
		private readonly FormAddFolder _formQuickSiteAddFolder;
		private readonly FormEmailWebLink _formQuickSiteEmail;

		public List<int> PreviousPreviewHandles { get; private set; }

		private LinkManager()
		{
			_formPowerPointQuickView = new FormPowerPointQuickView();
			_formPowerPointQuickViewOld = new FormPowerPointQuickViewOld();
			_formQuickSiteAddLink = new FormAddLink();
			_formQuickSiteAddFolder = new FormAddFolder();
			_formQuickSiteEmail = new FormEmailWebLink();
			PreviousPreviewHandles = new List<int>();
		}

		public static LinkManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new LinkManager();
				return _instance;
			}
		}

		public void OpenLibraryFolder(LibraryFolder folder)
		{
			if (SettingsManager.Instance.QBuilderSettings.AvailableHosts.Count > 0)
				using (var formViewOptions = new FormFolderSpecialOptions())
				{
					formViewOptions.Text = string.Format(formViewOptions.Text, folder.Name);
					if (formViewOptions.ShowDialog() == DialogResult.OK)
					{
						if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteAdd)
							AddFolderToQuickSite(folder);
					}
				}
		}

		public void OpenLink(LibraryLink link, bool specialOptions = false)
		{
			FileInfo sourceFile = null;
			if (link.Type != FileTypes.LineBreak && link.Type != FileTypes.Folder && link.Type != FileTypes.Url && link.Type != FileTypes.Network)
			{
				sourceFile = RequestFile(link);
				if (sourceFile == null)
				{
					AppManager.Instance.ShowWarning("File or Link is Not Active");
					AppManager.Instance.ActivityManager.AddLinkAccessActivity("Link not active", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
					return;
				}
			}

			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Link Access", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
			if (specialOptions)
			{
				using (var formViewOptions = new FormLinkSpecialOptions())
				{
					formViewOptions.Text = string.Format(formViewOptions.Text, link.Type != FileTypes.LineBreak ? link.Name : "LineBreak");
					formViewOptions.buttonXQuickSiteAdd.Enabled = SettingsManager.Instance.QBuilderSettings.AvailableHosts.Count > 0;
					formViewOptions.buttonXQuickSiteEmail.Enabled = SettingsManager.Instance.QBuilderSettings.AvailableHosts.Count > 0 && (link.Type != FileTypes.LineBreak);
					formViewOptions.buttonXEmailBin.Enabled = (link.Type != FileTypes.Folder && link.Type != FileTypes.LineBreak);
					if (formViewOptions.ShowDialog() == DialogResult.OK)
					{
						if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteEmail)
						{
							EmailLinkToQuickSite(link);
						}
						else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteAdd)
						{
							AddLinkToQuickSite(link);
						}
						else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.EmailBinAdd)
						{
							FormMain.Instance.TabHome.AddToEmailBin(link);
						}
					}
				}
			}
			else
			{
				switch (link.Type)
				{
					case FileTypes.BuggyPresentation:
					case FileTypes.FriendlyPresentation:
					case FileTypes.Presentation:
						switch (SettingsManager.Instance.PowerPointLaunchOptions)
						{
							case LinkLaunchOptions.Menu:
								using (var formViewOptions = new FormViewOptions())
								{
									formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
									if (formViewOptions.ShowDialog() == DialogResult.OK)
									{
										if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Open)
										{
											OpenCopyOfFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Save)
										{
											SaveFile("Save copy of the file as", link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Print)
										{
											PrintFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Email)
										{
											PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(link.LocalPath));
											using (var form = new FormEmailPresentation())
											{
												form.SelectedFile = link;
												form.ActiveSlide = 1;
												form.rbActiveSlide.Visible = false;
												form.ShowDialog();
											}
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteEmail)
										{
											EmailLinkToQuickSite(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteAdd)
										{
											AddLinkToQuickSite(link);
										}
									}
								}
								break;
							case LinkLaunchOptions.Launch:
								OpenCopyOfFile(link);
								return;
							case LinkLaunchOptions.Viewer:
								if (link.PreviewContainer != null && link.PreviewContainer.CheckPreviewImages() && !SettingsManager.Instance.OldStyleQuickView)
								{
									ViewPresentation(link);
									link.PreviewContainer.ReleasePreviewImages();
								}
								else
									ViewPresentationOld(link);
								break;
						}
						break;
					case FileTypes.PDF:
						switch (SettingsManager.Instance.PDFLaunchOptions)
						{
							case LinkLaunchOptions.Viewer:
								PreviewFile(link);
								return;
							case LinkLaunchOptions.Launch:
								OpenCopyOfFile(link);
								return;
							case LinkLaunchOptions.Menu:
								using (var formViewOptions = new FormViewOptions())
								{
									formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
									if (formViewOptions.ShowDialog() == DialogResult.OK)
									{
										if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Open)
										{
											OpenCopyOfFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Save)
										{
											SaveFile("Save copy of the file as", link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Print)
										{
											PrintFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Email)
											using (var form = new FormEmailLink())
											{
												form.link = link;
												form.ShowDialog();
											}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteEmail)
										{
											EmailLinkToQuickSite(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteAdd)
										{
											AddLinkToQuickSite(link);
										}
									}
								}
								break;
						}
						break;
					case FileTypes.Word:
						switch (SettingsManager.Instance.WordLaunchOptions)
						{
							case LinkLaunchOptions.Viewer:
								PreviewFile(link);
								return;
							case LinkLaunchOptions.Launch:
								OpenCopyOfFile(link);
								return;
							case LinkLaunchOptions.Menu:
								using (var formViewOptions = new FormViewOptions())
								{
									formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
									if (formViewOptions.ShowDialog() == DialogResult.OK)
									{
										if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Open)
										{
											OpenCopyOfFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Save)
										{
											SaveFile("Save copy of the file as", link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Print)
										{
											PrintFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Email)
											using (var form = new FormEmailLink())
											{
												form.link = link;
												form.ShowDialog();
											}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteEmail)
										{
											EmailLinkToQuickSite(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteAdd)
										{
											AddLinkToQuickSite(link);
										}
									}
								}
								break;
						}
						break;
					case FileTypes.Excel:
						switch (SettingsManager.Instance.ExcelLaunchOptions)
						{
							case LinkLaunchOptions.Viewer:
								PreviewFile(link);
								return;
							case LinkLaunchOptions.Launch:
								OpenCopyOfFile(link);
								return;
							case LinkLaunchOptions.Menu:
								using (var formViewOptions = new FormViewOptions())
								{
									formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
									if (formViewOptions.ShowDialog() == DialogResult.OK)
									{
										if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Open)
										{
											OpenCopyOfFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Save)
										{
											SaveFile("Save copy of the file as", link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Print)
										{
											PrintFile(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Email)
											using (var form = new FormEmailLink())
											{
												form.link = link;
												form.ShowDialog();
											}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteEmail)
										{
											EmailLinkToQuickSite(link);
										}
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteAdd)
										{
											AddLinkToQuickSite(link);
										}
									}
								}
								break;
						}
						break;
					case FileTypes.MediaPlayerVideo:
						switch (SettingsManager.Instance.VideoLaunchOptions)
						{
							case LinkLaunchOptions.Viewer:
								PreviewFile(link);
								return;
							case LinkLaunchOptions.Launch:
								OpenVideo(link);
								break;
							case LinkLaunchOptions.Menu:
								using (var formVideoOptions = new FormVideoViewOptions())
								{
									formVideoOptions.Text = string.Format(formVideoOptions.Text, sourceFile.Name);
									if (formVideoOptions.ShowDialog() == DialogResult.OK)
									{
										if (formVideoOptions.SelectedOption == FormVideoViewOptions.VideoViewOptions.Add)
										{
											AddVideoIntoPresentation(link);
										}
										else if (formVideoOptions.SelectedOption == FormVideoViewOptions.VideoViewOptions.Open)
										{
											OpenVideo(link);
										}
										else if (formVideoOptions.SelectedOption == FormVideoViewOptions.VideoViewOptions.QuickSiteEmail)
										{
											EmailLinkToQuickSite(link);
										}
										else if (formVideoOptions.SelectedOption == FormVideoViewOptions.VideoViewOptions.QuickSiteAdd)
										{
											AddLinkToQuickSite(link);
										}
									}
								}
								break;
						}
						break;
					case FileTypes.QuickTimeVideo:
						OpenVideo(link);
						break;
					case FileTypes.Other:
						OpenCopyOfFile(link);
						break;
					case FileTypes.Folder:
						switch (SettingsManager.Instance.FolderLaunchOptions)
						{
							case LinkLaunchOptions.Viewer:
								PreviewFile(link);
								return;
							case LinkLaunchOptions.Launch:
								OpenFolder(link);
								break;
							case LinkLaunchOptions.Menu:
								using (var formViewOptions = new FormFolderViewOptions())
								{
									formViewOptions.Text = string.Format(formViewOptions.Text, link.Name);
									if (formViewOptions.ShowDialog() == DialogResult.OK)
									{
										if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.Open)
											OpenFolder(link);
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteEmail)
											EmailLinkToQuickSite(link);
										else if (formViewOptions.SelectedOption == FormViewOptions.ViewOptions.QuickSiteAdd)
											AddLinkToQuickSite(link);
									}
								}
								break;
						}
						break;
					case FileTypes.Url:
						StartProcess(link);
						break;
					case FileTypes.Network:
						StartProcess(link);
						break;
					case FileTypes.OvernightsLink:
						StartProcess(link);
						break;
					case FileTypes.LineBreak:
						if (!string.IsNullOrEmpty(link.LineBreakProperties.Note))
							AppManager.Instance.ShowInfo(link.LineBreakProperties.Note);
						break;
				}
			}
		}

		private void PreviewFile(LibraryLink link)
		{
			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
			if (link.LinkAvailable)
			{
				using (var form = new FormLinkPreview())
				{
					form.SelectedFile = link;
					form.ShowDialog();
				}
			}
			var lastHandle = new IntPtr(PreviousPreviewHandles.LastOrDefault());
			RegistryHelper.SalesDepotHandle = lastHandle;
			PreviousPreviewHandles.Remove(PreviousPreviewHandles.LastOrDefault());
			RegistryHelper.MaximizeSalesDepot = lastHandle != FormMain.Instance.Handle ? false : true;
		}

		public void OpenCopyOfFile(LibraryLink link)
		{
			try
			{
				string newFile = Path.Combine(SettingsManager.Instance.OpenFilePath, @"Copy of " + Path.GetFileName(link.LocalPath));
				File.Copy(link.LocalPath, newFile, true);
				OpenFile(newFile);

				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
			}
			catch
			{
				AppManager.Instance.ShowWarning(string.Format("Could not create copy of {0} in a temp folder.", Path.GetFileName(link.LocalPath)));
			}
		}

		public void OpenCopyOfFile(FileInfo file)
		{

			try
			{
				file = file.CopyTo(Path.Combine(SettingsManager.Instance.OpenFilePath, @"Copy of " + file.Name), true);
				OpenFile(file.FullName);
			}
			catch
			{
				AppManager.Instance.ShowWarning(string.Format("Could not create copy of {0} in a temp folder.", file.Name));
			}
		}

		private void OpenFile(string filePath)
		{
			try
			{
				Process.Start(filePath);
			}
			catch
			{
				AppManager.Instance.ShowWarning("Couldn't open the file");
			}
		}

		public void OpenFolder(LibraryLink link)
		{
			if (Directory.Exists(link.LocalPath))
			{
				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Folder", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
				Process.Start(link.LocalPath);
			}
			else
				AppManager.Instance.ShowWarning("Folder is Not Active");
		}

		public void StartProcess(LibraryLink link)
		{
			try
			{
				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
				Process.Start(link.LocalPath);
			}
			catch
			{
				AppManager.Instance.ShowWarning("This Link is not active");
			}
		}

		public void SaveFile(string dialogTitle, LibraryLink link, bool isCopy = true)
		{
			var dialog = new SaveFileDialog();
			dialog.Title = dialogTitle;
			dialog.InitialDirectory = SettingsManager.Instance.SaveFilePath;
			dialog.FileName = (isCopy ? "Copy of " : string.Empty) + Path.GetFileName(link.LocalPath);
			dialog.OverwritePrompt = true;
			dialog.Filter = (Path.GetExtension(link.LocalPath).Substring(1)).ToUpper() + " Files|*" + Path.GetExtension(link.LocalPath);
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Save Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

				string newFile = dialog.FileName;
				File.Copy(link.LocalPath, newFile, true);
				if (File.Exists(newFile))
				{
					if (AppManager.Instance.ShowInfoQuestion(string.Format("The {0} file has been saved as\n{1}\nDo you want to open it?", new object[] { isCopy ? "copy of the" : string.Empty, newFile })) == DialogResult.Yes)
						OpenCopyOfFile(new FileInfo(newFile));
				}
				else
					AppManager.Instance.ShowWarning("File has not been saved successfully.");
			}
		}

		public void SaveFile(string dialogTitle, FileInfo file, bool isCopy = true)
		{
			var dialog = new SaveFileDialog();
			dialog.Title = dialogTitle;
			dialog.InitialDirectory = SettingsManager.Instance.SaveFilePath;
			dialog.FileName = (isCopy ? "Copy of " : string.Empty) + file.Name;
			dialog.OverwritePrompt = true;
			dialog.Filter = (file.Extension.Substring(1)).ToUpper() + " Files|*" + file.Extension;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				file = file.CopyTo(dialog.FileName, true);
				if (file.Exists)
				{
					if (AppManager.Instance.ShowInfoQuestion(string.Format("The {0} file has been saved as\n{1}\nDo you want to open it?", new object[] { isCopy ? "copy of the" : string.Empty, file.FullName })) == DialogResult.Yes)
						OpenCopyOfFile(file);
				}
				else
					AppManager.Instance.ShowWarning("File has not been saved successfully.");
			}
		}

		public void PrintFile(LibraryLink link)
		{
			var printProcess = new Process();
			string newFile = Path.Combine(SettingsManager.Instance.OpenFilePath, @"Copy of " + Path.GetFileName(link.LocalPath));
			File.Copy(link.LocalPath, newFile, true);
			switch (Path.GetExtension(link.LocalPath).Substring(1).ToUpper())
			{
				case "PPT":
				case "PPTX":
					break;
				case "DOC":
				case "DOCX":
					try
					{
						printProcess.StartInfo.FileName = "winword.exe";
						printProcess.StartInfo.Arguments = '"' + newFile + '"' + " /mFilePrint";
						printProcess.Start();
					}
					catch
					{
						AppManager.Instance.ShowWarning("AcroRd32.exe has not been found");
					}
					break;
				case "XLS":
				case "XLSX":
					if (ExcelHelper.Instance.Connect())
					{
						ExcelHelper.Instance.Print(new FileInfo(newFile));
						ExcelHelper.Instance.Disconnect();
					}
					break;
				case "PDF":
					try
					{
						printProcess.StartInfo.FileName = "AcroRd32.exe";
						printProcess.StartInfo.Arguments = " /p " + '"' + newFile + '"';
						printProcess.Start();
					}
					catch
					{
						AppManager.Instance.ShowWarning("AcroRd32.exe has not been found");
					}
					break;
				default:
					AppManager.Instance.ShowWarning("Cannot print files of this type");
					break;
			}

			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Print Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
		}

		public void EmailFile(LibraryLink link)
		{
			if (OutlookHelper.Instance.Open())
			{
				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Email Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
				OutlookHelper.Instance.CreateMessage(string.Empty, link.LocalPath);
				OutlookHelper.Instance.Close();
			}
			else
				AppManager.Instance.ShowWarning("Cannot open Outlook");
		}

		public void EmailFile(string filePath)
		{
			if (OutlookHelper.Instance.Open())
			{
				OutlookHelper.Instance.CreateMessage(string.Empty, filePath);
				OutlookHelper.Instance.Close();
			}
			else
				AppManager.Instance.ShowWarning("Cannot open Outlook");
		}

		public void EmailFile(string[] filePaths)
		{
			if (OutlookHelper.Instance.Open())
			{
				OutlookHelper.Instance.CreateMessage(string.Empty, filePaths);
				OutlookHelper.Instance.Close();
			}
			else
				AppManager.Instance.ShowWarning("Cannot open Outlook");
		}

		public FileInfo RequestFile(LibraryLink link)
		{
			FileInfo sourceFile = null;
			if (link.LinkAvailable)
			{
				if (SettingsManager.Instance.UseRemoteConnection)
				{
					using (var form = new FormProgress())
					{
						form.laProgress.Text = string.Format("Downloading {0}...", link.NameWithExtension);
						form.TopMost = true;
						FormMain.Instance.ribbonControl.Enabled = false;
						form.Show();
						Application.DoEvents();
						try
						{
							sourceFile = new FileInfo(link.LocalPath);
						}
						catch
						{
							sourceFile = null;
						}
						FormMain.Instance.UseWaitCursor = false;
						FormMain.Instance.ribbonControl.Enabled = true;
						form.Close();
						Application.DoEvents();
					}
				}
				else
				{
					try
					{
						sourceFile = new FileInfo(link.LocalPath);
					}
					catch
					{
						sourceFile = null;
					}
				}
			}
			return sourceFile;
		}

		public void ViewPresentation(LibraryLink link)
		{
			string presentationFile = link.LocalPath;
			if (!PowerPointHelper.Instance.IsLinkedWithApplication)
			{
				AppManager.Instance.RunPowerPointLoader();
				AppManager.Instance.ActivatePowerPoint();
				AppManager.Instance.ActivateMainForm();
			}
			var file = new FileInfo(presentationFile);
			if (file.Extension.ToLower().Equals(".pptx") && PowerPointHelper.Instance.Is2003)
			{
				if (!RegistryHelper.Office2007CompatibilityPackInstalled)
				{
					AppManager.Instance.ShowWarning("This File was created in a Newer Version of Microsoft Office." + Environment.NewLine + Environment.NewLine + "In the FUTURE, if you want to open this file, ASK your I.T. Manager to INSTALL the Office 2007 Compatibility Pack.");
					return;
				}
				if (SettingsManager.Instance.PowerPointLaunchOptions == LinkLaunchOptions.Viewer)
				{
					if (AppManager.Instance.ShowWarningQuestion("This file is built in a newer version of PowerPoint." + Environment.NewLine + "Do you still want to open the file?") == DialogResult.Yes)
						OpenCopyOfFile(link);
					return;
				}
			}
			if (file.Exists)
			{
				_formPowerPointQuickView.SelectedFile = link;
				int temp = link.PreviewContainer.SelectedIndex;

				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

				_formPowerPointQuickView.ShowDialog(FormMain.Instance);
				link.PreviewContainer.SelectedIndex = temp;
			}
			RegistryHelper.SalesDepotHandle = FormMain.Instance.Handle;
			RegistryHelper.MaximizeSalesDepot = true;
		}

		public void ViewPresentationOld(LibraryLink link)
		{
			if (!PowerPointHelper.Instance.IsLinkedWithApplication)
			{
				AppManager.Instance.RunPowerPointLoader();
				AppManager.Instance.ActivatePowerPoint();
				AppManager.Instance.ActivateMainForm();
			}
			var file = new FileInfo(link.LocalPath);
			if (file.Extension.ToLower().Equals(".pptx") && PowerPointHelper.Instance.Is2003)
			{
				if (!RegistryHelper.Office2007CompatibilityPackInstalled)
				{
					AppManager.Instance.ShowWarning("This File was created in a Newer Version of Microsoft Office." + Environment.NewLine + Environment.NewLine + "In the FUTURE, if you want to open this file, ASK your I.T. Manager to INSTALL the Office 2007 Compatibility Pack.");
					return;
				}
				if (SettingsManager.Instance.PowerPointLaunchOptions == LinkLaunchOptions.Viewer)
				{
					if (AppManager.Instance.ShowWarningQuestion("This file is built in a newer version of PowerPoint." + Environment.NewLine + "Do you still want to open the file?") == DialogResult.Yes)
						OpenCopyOfFile(link);
					return;
				}
			}
			if (file.Exists)
			{
				_formPowerPointQuickViewOld.SelectedFile = link;

				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

				_formPowerPointQuickViewOld.ShowDialog(FormMain.Instance);
			}
			RegistryHelper.SalesDepotHandle = FormMain.Instance.Handle;
			RegistryHelper.MaximizeSalesDepot = true;
		}

		public void AddVideoIntoPresentation(LibraryLink link)
		{
			if (File.Exists(PowerPointHelper.Instance.ActivePresentation.FullName))
			{
				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Insert video", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

				AppManager.Instance.ActivatePowerPoint();
				AppManager.Instance.ActivateMainForm();
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Inserting the video...";
					FloaterManager.Instance.ShowFloater(FormMain.Instance, () =>
					{
						form.TopMost = true;
						bool result = false;
						var thread = new Thread(delegate() { result = PowerPointHelper.Instance.InsertVideoIntoActivePresentation(link.LocalPath, 100, 100, 400, 400); });
						thread.Start();
						form.Show();
						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					});
				}
			}
			else
			{
				AppManager.Instance.ShowWarning("The presentation is not saved. Please, save it and try again");
			}
		}

		public void OpenVideo(LibraryLink link)
		{
			string newFile = Path.Combine(SettingsManager.Instance.OpenFilePath, Path.GetFileName(link.LocalPath));
			File.Copy(link.LocalPath, newFile, true);
			var videoPlay = new ProcessStartInfo(newFile);
			try
			{
				var process = new Process();
				process.StartInfo = videoPlay;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
				process.Start();

				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Video", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
			}
			catch
			{
				AppManager.Instance.ShowWarning("Couldn’t find player associated with video file");
			}
		}

		public void EmailLinkToQuickSite(LibraryLink link)
		{
			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Email as Web Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
			if (link.LinkAvailable)
			{
				_formQuickSiteEmail.Init(link);
				_formQuickSiteEmail.ShowDialog();
			}
		}

		public void AddLinkToQuickSite(LibraryLink link)
		{
			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Add to quickSITE", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
			if (link.LinkAvailable)
			{
				_formQuickSiteAddLink.Init(link);
				if (_formQuickSiteAddLink.ShowDialog() == DialogResult.OK)
					AppManager.Instance.ShowInfo(link.Type != FileTypes.LineBreak ?
						"Link successfully added to Site Link Cart" :
						"LineBreak successfully added to Site Link Cart");
			}
		}

		public void AddFolderToQuickSite(LibraryFolder folder)
		{
			_formQuickSiteAddFolder.Init(folder);
			if (_formQuickSiteAddFolder.ShowDialog() == DialogResult.OK)
				AppManager.Instance.ShowInfo("Links successfully added to Site Link Cart");
		}
	}
}