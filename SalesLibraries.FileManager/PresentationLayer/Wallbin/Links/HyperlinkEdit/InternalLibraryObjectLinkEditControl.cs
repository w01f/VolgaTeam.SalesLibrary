using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Business.Models.ExternalLibraryLinks;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalLibraryObjectLinkEditControl : BaseInternalLibraryContentEditControl, IInternalLinkEditControl
	{
		public InternalLibraryObjectLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public override void InitControl()
		{
			if (IsListsLoaded) return;
			base.InitControl();

			comboBoxEditLibraryName.Properties.Items.Clear();
			comboBoxEditLibraryName.Properties.Items.AddRange(MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.OrderBy(l => l.Name)
				.Select(l => l.Name)
				.ToArray());
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalLibraryObjectLinkInfo)PrepareHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.LibraryName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target library before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.PageName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target page before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.WindowName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target window before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.LinkName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target link before saving");
				return false;
			}
			return true;
		}

		public InternalLinkInfo GetFinalHyperLinkInfo()
		{
			var linkInfo = (InternalLibraryObjectLinkInfo)PrepareHyperLinkInfo();

			MainController.Instance.ProcessManager.Run(
				"Loading Link Thumbnails...",
				(cancelationToken, formProgess) =>
				{
					linkInfo.ThumbnailUrls.AddRange(MainController.Instance.Lists.ExternalLibraryLinks.GetLinkThumbnails(linkInfo.LinkId));
				});

			return linkInfo;
		}

		public InternalLinkInfo PrepareHyperLinkInfo()
		{
			var selectedLink = (LibraryLink)comboBoxEditLibraryLinkName.EditValue;

			var linkInfo = new InternalLibraryObjectLinkInfo
			{
				LibraryName = comboBoxEditLibraryName.EditValue as String,
				PageName = comboBoxEditPageName.EditValue as String,
				WindowName = comboBoxEditWindowName.EditValue as String,
				LinkName = selectedLink.ToString(),
				LinkId = selectedLink.Id
			};

			return linkInfo;
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo)
		{
			if (templateInfo is InternalLibraryPageLinkInfo)
			{
				comboBoxEditLibraryName.EditValue = ((InternalLibraryPageLinkInfo)templateInfo).LibraryName;
				comboBoxEditPageName.EditValue = ((InternalLibraryPageLinkInfo)templateInfo).PageName;
			}
			if (templateInfo is InternalLibraryFolderLinkInfo)
			{
				comboBoxEditLibraryName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).LibraryName;
				comboBoxEditPageName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).PageName;
				comboBoxEditWindowName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).WindowName;
			}
		}

		private void OnLibraryChanged(object sender, EventArgs e)
		{
			comboBoxEditPageName.Properties.Items.Clear();
			var libraryName = comboBoxEditLibraryName.EditValue as String;
			var library = MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.FirstOrDefault(l => String.Compare(l.Name, libraryName, StringComparison.OrdinalIgnoreCase) == 0);
			if (library != null)
				comboBoxEditPageName.Properties.Items.AddRange(library.Pages
					.OrderBy(p => p.Order)
					.Select(p => p.Name)
					.ToArray());
			OnLibraryPageChanged(sender, e);
			OnLibraryFolderChanged(sender, e);
		}

		private void OnLibraryPageChanged(object sender, EventArgs e)
		{
			comboBoxEditWindowName.Properties.Items.Clear();
			var libraryName = comboBoxEditLibraryName.EditValue as String;
			var libraryPageName = comboBoxEditPageName.EditValue as String;
			var libraryPage = MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.Where(l => String.Compare(l.Name, libraryName, StringComparison.OrdinalIgnoreCase) == 0)
				.SelectMany(l => l.Pages)
				.FirstOrDefault(p => String.Compare(p.Name, libraryPageName, StringComparison.OrdinalIgnoreCase) == 0);
			if (libraryPage != null)
				comboBoxEditWindowName.Properties.Items.AddRange(libraryPage.Folders
					.OrderBy(f => f.Order)
					.Select(f => f.Name)
					.ToArray());
			OnLibraryFolderChanged(sender, e);
		}

		private void OnLibraryFolderChanged(object sender, EventArgs e)
		{
			comboBoxEditLibraryLinkName.Properties.Items.Clear();
			var libraryName = comboBoxEditLibraryName.EditValue as String;
			var libraryPageName = comboBoxEditPageName.EditValue as String;
			var libraryFolderName = comboBoxEditWindowName.EditValue as String;
			var libraryFolder = MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.Where(l => String.Compare(l.Name, libraryName, StringComparison.OrdinalIgnoreCase) == 0)
				.SelectMany(l => l.Pages)
				.Where(p => String.Compare(p.Name, libraryPageName, StringComparison.OrdinalIgnoreCase) == 0)
				.SelectMany(p => p.Folders)
				.FirstOrDefault(f => String.Compare(f.Name, libraryFolderName, StringComparison.OrdinalIgnoreCase) == 0);
			if (libraryFolder != null)
				comboBoxEditLibraryLinkName.Properties.Items.AddRange(libraryFolder.Links
					.OrderBy(link => link.Order)
					.ToArray());
		}
	}
}
