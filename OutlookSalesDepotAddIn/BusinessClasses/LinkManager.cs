using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Outlook;
using OutlookSalesDepotAddIn.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class LinkManager
	{
		private static LinkManager _instance;

		public List<int> PreviousPreviewHandles { get; private set; }

		private LinkManager()
		{
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

		public void OpenLink(LibraryLink link, bool specialOptions = false)
		{
			if (link.Type != FileTypes.LineBreak && link.Type != FileTypes.Folder && link.Type != FileTypes.Url && link.Type != FileTypes.Network)
			{
				var sourceFile = RequestFile(link);
				if (sourceFile == null)
				{
					Utils.ShowWarning("File or Link is Not Active");
					return;
				}
			}
			switch (link.Type)
			{
				case FileTypes.BuggyPresentation:
				case FileTypes.FriendlyPresentation:
				case FileTypes.Presentation:
				case FileTypes.Word:
				case FileTypes.Excel:
				case FileTypes.PDF:
				case FileTypes.MediaPlayerVideo:
				case FileTypes.QuickTimeVideo:
				case FileTypes.Folder:
					PreviewFile(link);
					break;
				case FileTypes.Other:
				case FileTypes.Url:
				case FileTypes.Network:
					Utils.ShowInfo("Can't attach this link");
					break;
				case FileTypes.LineBreak:
					if (!string.IsNullOrEmpty(link.LineBreakProperties.Note))
						Utils.ShowInfo(link.LineBreakProperties.Note);
					break;
			}
		}

		private void PreviewFile(LibraryLink link)
		{
			if (!link.LinkAvailable) return;
			using (var form = new FormLinkPreview())
			{
				form.SelectedFile = link;
				form.ShowDialog();
			}
		}

		public void AttachFile(LibraryLink link)
		{
			var currentMailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as MailItem;
			if (currentMailItem == null) return;

			try
			{
				var newFile = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(link.LocalPath));
				File.Copy(link.LocalPath, newFile, true);
				currentMailItem.Attachments.Add(newFile, OlAttachmentType.olByValue, 1, "Attachment");
			}
			catch
			{
				Utils.ShowWarning(string.Format("Could not create copy of {0} in a temp folder.", Path.GetFileName(link.LocalPath)));
			}
		}

		public void AttachFiles(IEnumerable<LibraryLink> links)
		{
			var currentMailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as MailItem;
			if (currentMailItem == null) return;
			foreach (var libraryLink in links.Where(l => l.LinkAvailable))
			{
				try
				{
					var newFile = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(libraryLink.LocalPath));
					File.Copy(libraryLink.LocalPath, newFile, true);
					currentMailItem.Attachments.Add(newFile, OlAttachmentType.olByValue, 1, "Attachment");
				}
				catch
				{
					Utils.ShowWarning(string.Format("Could not create copy of {0} in a temp folder.", Path.GetFileName(libraryLink.LocalPath)));
					break;
				}
			}
		}

		public FileInfo RequestFile(LibraryLink link)
		{
			FileInfo sourceFile;
			if (!link.LinkAvailable) return null;
			try
			{
				sourceFile = new FileInfo(link.LocalPath);
			}
			catch
			{
				sourceFile = null;
			}
			return sourceFile;
		}
	}
}