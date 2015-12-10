using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.SalesDepot.Business.Services;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors;

namespace SalesLibraries.SalesDepot.Business.LinkViewers
{
	static class LinkManager
	{
		public static List<int> PreviousPreviewHandles { get; private set; }

		static LinkManager()
		{
			PreviousPreviewHandles = new List<int>();
		}

		public static void OpenLink(LibraryObjectLink link, bool specialOptions = false)
		{
			MainController.Instance.ActivityManager.AddLinkAccessActivity("Link Access", link);
			if (specialOptions)
			{
				using (var formViewOptions = new FormLinkSpecialOptions())
				{
					formViewOptions.Text = String.Format(formViewOptions.Text, link.Name);
					formViewOptions.buttonXEmailBin.Enabled = link is LibraryFileLink;
					if (formViewOptions.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
					{
						if (formViewOptions.SelectedOption == ViewOptions.EmailBinAdd)
						{
							MainController.Instance.EmailBin.AddLink((LibraryFileLink)link);
						}
					}
				}
			}
			else
				LinkProcessorFactory.Create(link).Open();
		}

		public static void PreviewLink(LibraryObjectLink link)
		{
			MainController.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", link);
			using (var form = new FormLinkPreview())
			{
				form.Link = link;
				form.ShowDialog(MainController.Instance.MainForm);
			}
			var lastHandle = PreviousPreviewHandles.LastOrDefault();
			RegistryHelper.SalesDepotHandle = new IntPtr(lastHandle);
			PreviousPreviewHandles.Remove(lastHandle);
			RegistryHelper.MaximizeSalesDepot = RegistryHelper.SalesDepotHandle == MainController.Instance.MainForm.Handle;
		}

		public static void EmailLink(LibraryFileLink fileLink)
		{
			MainController.Instance.ActivityManager.AddLinkAccessActivity("Email Link", fileLink);
			EmailFile(fileLink.FullPath);
		}

		public static void EmailFile(string filePath)
		{
			EmailFiles(new[] { filePath });
		}

		public static void EmailFiles(string[] filePaths)
		{
			if (OutlookHelper.Instance.Connect())
			{
				OutlookHelper.Instance.CreateMessage(string.Empty, filePaths);
				OutlookHelper.Instance.Disconnect();
			}
			else
				MainController.Instance.PopupMessages.ShowWarning("Cannot open Outlook");
		}

		public static void OpenCopyOfFile(LibraryFileLink fileLink)
		{
			try
			{
				var newFile = Path.Combine(MainController.Instance.Settings.OpenFilePath, @"Copy of " + Path.GetFileName(fileLink.FullPath));
				File.Copy(fileLink.FullPath, newFile, true);
				Utils.OpenFile(newFile);
				MainController.Instance.ActivityManager.AddLinkAccessActivity("Open Link", fileLink);
			}
			catch
			{
				MainController.Instance.PopupMessages.ShowWarning(string.Format("Could not create copy of {0} in a temp folder.", Path.GetFileName(fileLink.FullPath)));
			}
		}

		public static void OpenVideo(VideoLink videoLink)
		{
			var newFile = Path.Combine(MainController.Instance.Settings.OpenFilePath, videoLink.NameWithExtension);
			File.Copy(videoLink.FullPath, newFile, true);
			var videoPlay = new ProcessStartInfo(newFile);
			try
			{
				var process = new Process();
				process.StartInfo = videoPlay;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
				process.Start();
				MainController.Instance.ActivityManager.AddLinkAccessActivity("Open Video", videoLink);
			}
			catch
			{
				MainController.Instance.PopupMessages.ShowWarning("Couldn’t find player associated with video file");
			}
		}

		public static void OpenFolderLink(LibraryFolderLink folderLink)
		{
			if (!folderLink.CheckIfDead())
			{
				MainController.Instance.ActivityManager.AddLinkAccessActivity("Open Folder", folderLink);
				Utils.OpenFile(folderLink.FullPath);
			}
			else
				MainController.Instance.PopupMessages.ShowWarning("Folder is Not Active");
		}

		public static void SaveLink(string dialogTitle, LibraryFileLink fileLink)
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = dialogTitle;
				dialog.InitialDirectory = MainController.Instance.Settings.SaveFilePath;
				dialog.FileName = fileLink.Name;
				dialog.OverwritePrompt = true;
				dialog.Filter = fileLink.Extension.Substring(1).ToUpper() + " Files|*" + fileLink.Extension;
				if (dialog.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
				{
					MainController.Instance.ActivityManager.AddLinkAccessActivity("Save Link", fileLink);

					var newFile = dialog.FileName;
					File.Copy(fileLink.FullPath, newFile, true);
					if (File.Exists(newFile))
					{
						if (MainController.Instance.PopupMessages.ShowWarningQuestion(
							String.Format("The file has been saved as\n{0}\nDo you want to open it?",
								newFile)
							) == DialogResult.Yes)
							Utils.OpenFile(newFile);
					}
					else
						MainController.Instance.PopupMessages.ShowWarning("File has not been saved successfully.");
				}
			}
		}

		public static void SaveFile(string dialogTitle, FileInfo file)
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = dialogTitle;
				dialog.InitialDirectory = MainController.Instance.Settings.SaveFilePath;
				dialog.FileName = file.Name;
				dialog.OverwritePrompt = true;
				dialog.Filter = (file.Extension.Substring(1)).ToUpper() + " Files|*" + file.Extension;
				if (dialog.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
				{
					file = file.CopyTo(dialog.FileName, true);
					if (file.Exists)
					{
						if (MainController.Instance.PopupMessages.ShowWarningQuestion(
							String.Format("The file has been saved as\n{0}\nDo you want to open it?",
								file.FullName)
							) == DialogResult.Yes)
							Utils.OpenFile(file.FullName);
					}
					else
						MainController.Instance.PopupMessages.ShowWarning("File has not been saved successfully.");
				}
			}
		}

		public static void PrintFile(LibraryFileLink fileLink)
		{
			var printProcess = new Process();
			var newFile = Path.Combine(MainController.Instance.Settings.OpenFilePath, @"Copy of " + Path.GetFileName(fileLink.FullPath));
			File.Copy(fileLink.FullPath, newFile, true);
			if (FileFormatHelper.IsWordFile(fileLink.FullPath))
			{
				try
				{
					printProcess.StartInfo.FileName = "winword.exe";
					printProcess.StartInfo.Arguments = '"' + newFile + '"' + " /mFilePrint";
					printProcess.Start();
				}
				catch { }
			}
			else if (FileFormatHelper.IsExcelFile(fileLink.FullPath))
			{
				if (ExcelHelper.Instance.Connect())
				{
					ExcelHelper.Instance.Print(new FileInfo(newFile));
					ExcelHelper.Instance.Disconnect();
				}
			}
			else if (FileFormatHelper.IsPdfFile(fileLink.FullPath))
			{
				try
				{
					printProcess.StartInfo.FileName = "AcroRd32.exe";
					printProcess.StartInfo.Arguments = " /p " + '"' + newFile + '"';
					printProcess.Start();
				}
				catch
				{
					MainController.Instance.PopupMessages.ShowWarning("AcroRd32.exe has not been found");
				}
			}
			MainController.Instance.ActivityManager.AddLinkAccessActivity("Print Link", fileLink);
		}

		public static void AddVideoIntoPresentation(VideoLink file)
		{
			if (!MainController.Instance.CheckPowerPointRunning(() => MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes))
				return;
			if (File.Exists(PowerPointSingleton.Instance.GetActivePresentation().FullName))
			{
				MainController.Instance.ActivityManager.AddLinkAccessActivity("Insert video", file);
				PowerPointManager.Instance.ActivatePowerPoint();
				MainController.Instance.ActivateApplication();
				FloaterManager.Instance.ShowFloater(
					MainController.Instance.MainForm,
					MainController.Instance.Settings.SalesDepotName,
					MainController.Instance.MainForm.FloaterLogo,
					() => MainController.Instance.ProcessManager.Run(
						"Inserting the video...",
						cancellationToken => PowerPointSingleton.Instance.InsertVideoIntoActivePresentation(file.FullPath))
					);
			}
			else
			{
				MainController.Instance.PopupMessages.ShowWarning("The presentation is not saved. Please, save it and try again");
			}
		}

		public static void ViewPresentation(PowerPointLink powerPointLink)
		{
			using (var form = new FormPowerPointQuickView())
			{
				form.PowerPointLink = powerPointLink;
				MainController.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", powerPointLink);
				form.ShowDialog(MainController.Instance.MainForm);
				if (form.AfterClose != null)
					form.AfterClose();
				RegistryHelper.SalesDepotHandle = MainController.Instance.MainForm.Handle;
				RegistryHelper.MaximizeSalesDepot = true;
			}
		}

		public static void ViewPresentationOld(PowerPointLink powerPointLink)
		{
			if (!MainController.Instance.CheckPowerPointRunning(() => MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes))
				return;
			using (var form = new FormPowerPointQuickViewOld())
			{
				form.PowerPointLink = powerPointLink;
				MainController.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", powerPointLink);
				form.ShowDialog(MainController.Instance.MainForm);
				RegistryHelper.SalesDepotHandle = MainController.Instance.MainForm.Handle;
				RegistryHelper.MaximizeSalesDepot = true;
			}
		}
	}
}
