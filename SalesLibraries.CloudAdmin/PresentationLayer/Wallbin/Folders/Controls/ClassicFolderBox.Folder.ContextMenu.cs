using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		private readonly List<BaseContextMenuEditor> _folderLinksContextMenuEditors = new List<BaseContextMenuEditor>();

		private void InitFolderLinksContextMenuEditors()
		{
			var libraryObjectNotesEditor = new LibraryObjectNotesEditor(barSubItemFolderPropertiesMultiLinksNotesObjects);
			libraryObjectNotesEditor.EditValueChanged += OnFolderLinksContextEditorValueChanged;
			_folderLinksContextMenuEditors.Add(libraryObjectNotesEditor);

			var lineBreakNotesEditor = new LineBreakNotesEditor(barSubItemFolderPropertiesMultiLinksNotesLineBreaks);
			lineBreakNotesEditor.EditValueChanged += OnFolderLinksContextEditorValueChanged;
			_folderLinksContextMenuEditors.Add(lineBreakNotesEditor);

			var libraryObjectFormatEditor = new LibraryObjectTextFormatEditor(barSubItemFolderPropertiesMultiLinksFormatObjects);
			libraryObjectFormatEditor.EditValueChanged += OnFolderLinksContextEditorValueChanged;
			_folderLinksContextMenuEditors.Add(libraryObjectFormatEditor);

			var lineBreakFormatEditor = new LineBreakTextFormatEditor(barSubItemFolderPropertiesMultiLinksFormatLineBreaks);
			lineBreakFormatEditor.EditValueChanged += OnFolderLinksContextEditorValueChanged;
			_folderLinksContextMenuEditors.Add(lineBreakFormatEditor);

			popupMenuFolderProperties.CloseUp += OnFolderLinkPropertiesMenuCloseUp;
		}

		private void LoadFolderLinksContextMenuEditors(IList<BaseLibraryLink> links)
		{
			_folderLinksContextMenuEditors.ForEach(e => e.LoadLinks(links));
		}

		private void ApplyFolderLinksContextMenuEditorChanges()
		{
			_folderLinksContextMenuEditors.ForEach(e => e.ApplyChanges());
		}

		private void OnFolderLinksContextEditorValueChanged(object sender, EventArgs e)
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
			UpdateContent(true);
		}

		private void OnFolderLinkPropertiesMenuCloseUp(object sender, EventArgs e)
		{
			ApplyFolderLinksContextMenuEditorChanges();
		}

		private void barButtonItemFolderPropertiesFolderSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderSettings();
		}

		private void barButtonItemFolderPropertiesDeleteFolder_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteFolder();
		}

		private void barButtonItemFolderPropertiesFolderWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderWidget();
		}

		private void barButtonItemFolderPropertiesFolderBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderBanner();
		}

		private void barButtonItemFolderPropertiesImageSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderImageSettings();
		}

		private void barButtonItemFolderPropertiesDeleteLinkWidgets_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetWidgets();
		}

		private void barButtonItemFolderPropertiesDeleteLinkBanners_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetBanners();
		}

		private void barButtonItemFolderPropertiesDeleteLinkSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetSecurity();
		}

		private void barButtonItemFolderPropertiesDeleteLinkTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetTags();
		}

		private void barButtonItemFolderPropertiesSortLinks_ItemClick(object sender, ItemClickEventArgs e)
		{
			SortLinkByName();
		}

		private void barButtonItemFolderPropertiesSetLinkTextWordWrap_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetLinkTextWordWrap();
		}

		private void barButtonItemFolderPropertiesLinkAdminSetingsExcel_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditAllLinksInFolderSettings(LinkSettingsType.AdminSettings, FileTypes.Excel);
		}

		private void barButtonItemFolderPropertiesLinkAdminSetingsPdf_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditAllLinksInFolderSettings(LinkSettingsType.AdminSettings, FileTypes.Pdf);
		}

		private void barButtonItemFolderPropertiesMultiLinksResetSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetAllLinksInFolderSettings();
		}

		private void barButtonItemFolderPropertiesMultiLinksDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteFolderLinks();
		}

		private void barButtonItemFolderPropertiesMultiLinksWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditAllLinksInFolderSettings(LinkSettingsType.Widget);
		}

		private void barButtonItemFolderPropertiesMultiLinksBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditAllLinksInFolderSettings(LinkSettingsType.Banner);
		}

		private void barButtonItemFolderPropertiesMultiLinksTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditAllLinksInFolderSettings(LinkSettingsType.Tags);
		}

		private void barButtonItemFolderPropertiesMultiLinksSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditAllLinksInFolderSettings(LinkSettingsType.Security);
		}

		private void barButtonItemFolderPropertiesMultiLinksExpirationDate_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditAllLinksInFolderSettings(LinkSettingsType.ExpirationDate);
		}

		private void barButtonItemFolderPropertiesMultiLinksRefreshPreview_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to refresh the server files for links in this window?") != DialogResult.Yes) return;
			RefreshPreviewFiles(DataSource.AllGroupLinks.OfType<PreviewableLink>().ToList());
			MainController.Instance.PopupMessages.ShowInfo("Links are now updated for the server!");
		}

		private void barButtonItembarSubItemFolderPropertiesDeleteAllLinks_ItemClick(object sender, ItemClickEventArgs e)
		{
			barButtonItemFolderPropertiesMultiLinksDelete_ItemClick(sender, e);
		}
	}
}
