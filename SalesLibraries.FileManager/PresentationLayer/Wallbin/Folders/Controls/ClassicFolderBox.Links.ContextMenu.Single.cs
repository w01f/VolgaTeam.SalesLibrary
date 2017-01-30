using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		private readonly List<BaseContextMenuEditor> _singleLinkContextMenuEditors = new List<BaseContextMenuEditor>();

		private void InitSingleLinkContextMenuEditors()
		{
			var libraryObjectNotesEditor = new LibraryObjectNotesEditor(barSubItemSingleLinkPropertiesObjectNotes);
			libraryObjectNotesEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_singleLinkContextMenuEditors.Add(libraryObjectNotesEditor);

			var lineBreakNotesEditor = new LineBreakNotesEditor(barSubItemSingleLinkPropertiesLineBreakNotes);
			lineBreakNotesEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_singleLinkContextMenuEditors.Add(lineBreakNotesEditor);

			var libraryObjectFormatEditor = new LibraryObjectTextFormatEditor(barSubItemSingleLinkPropertiesObjectTextFormat);
			libraryObjectFormatEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_singleLinkContextMenuEditors.Add(libraryObjectFormatEditor);

			var lineBreakFormatEditor = new LineBreakTextFormatEditor(barSubItemSingleLinkPropertiesLineBreakTextFormat);
			lineBreakFormatEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_singleLinkContextMenuEditors.Add(lineBreakFormatEditor);

			var documentSettingsEditor = new DocumentSettingsEditor(barSubItemSingleLinkPropertiesAdminSettings);
			documentSettingsEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_singleLinkContextMenuEditors.Add(documentSettingsEditor);

			var excelSettingsEditor = new ExcelSettingsEditor(barSubItemSingleLinkPropertiesAdminSettings);
			excelSettingsEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_singleLinkContextMenuEditors.Add(excelSettingsEditor);

			popupMenuSingleLinkProperties.CloseUp += OnSingleLinkPropertiesMenuCloseUp;
		}

		private void LoadSingleLinkContextMenuEditors(BaseLibraryLink link)
		{
			_singleLinkContextMenuEditors.ForEach(e => e.LoadLinks(new[] { link }.ToList()));
		}

		private void ApplySingleLinkContextMenuEditorChanges()
		{
			_singleLinkContextMenuEditors.ForEach(e => e.ApplyChanges());
		}

		private void OnSingleLinkContextEditorValueChanged(object sender, EventArgs e)
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			selectedRow.Info.Recalc();
			grFiles.Refresh();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnGridMouseClick(object sender, MouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (e.Button != MouseButtons.Right) return;
			var hitTest = grFiles.HitTest(e.X, e.Y);
			if (hitTest.Type == DataGridViewHitTestType.None)
			{
				barButtonItemSingleLinkPropertiesPaste.Visibility = MainController.Instance.WallbinViews.LinksClipboard.LinkIds.Any() ?
					BarItemVisibility.Always :
					BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesCopy.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesCut.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesDelete.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesImageSettings.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesObjectNotes.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesImages.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesResetSettings.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdvanced.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesObjectTextFormat.Visibility = BarItemVisibility.Never;

				popupMenuSingleLinkProperties.ShowPopup(Cursor.Position);
			}
			else
			{
				barButtonItemSingleLinkPropertiesCopy.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesCut.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesPaste.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesDelete.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesLinkSettings.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesImageSettings.Visibility = BarItemVisibility.Always;
				barSubItemSingleLinkPropertiesObjectNotes.Visibility = BarItemVisibility.Always;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesTags.Visibility = BarItemVisibility.Always;
				barSubItemSingleLinkPropertiesImages.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesResetSettings.Visibility = BarItemVisibility.Always;
				barSubItemSingleLinkPropertiesAdvanced.Visibility = BarItemVisibility.Always;
				barSubItemSingleLinkPropertiesObjectTextFormat.Visibility = BarItemVisibility.Always;
			}
		}

		private void ProcessSingleLinkContextMenu(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (e.Button != MouseButtons.Right) return;

			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			if (SelectionManager.SelectedLinks.Count > 1 && linkRow.Selected) return;

			if (!linkRow.Selected)
				SelectSingleRow(linkRow);

			barButtonItemSingleLinkPropertiesCopy.Visibility =
				barButtonItemSingleLinkPropertiesCut.Visibility = !linkRow.Inaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemSingleLinkPropertiesPaste.Visibility =
				MainController.Instance.WallbinViews.LinksClipboard.IsPasteAvailable(linkRow.Source.ExtId)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barSubItemSingleLinkPropertiesAddHyperlink.Visibility = !linkRow.Inaccessable
				? BarItemVisibility.Always
				: BarItemVisibility.Never;
			barButtonItemSingleLinkPropertiesImageSettings.Visibility = !linkRow.Inaccessable && linkRow.AllowEditImageSettings
				? BarItemVisibility.Always
				: BarItemVisibility.Never;
			barButtonItemSingleLinkPropertiesSecurity.Visibility =
				!linkRow.Inaccessable && MainController.Instance.Settings.EditorSettings.EnableSecurityEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemSingleLinkPropertiesResetSettings.Visibility =
				!linkRow.Inaccessable && linkRow.Source.GetCustomizedSettigsGroups().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			if (linkRow.Source is LineBreak)
			{
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFileLocation.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Line Break Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete this Line Break";
				barSubItemSingleLinkPropertiesImages.Caption = "Line Break ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Line Break";
			}
			else if (linkRow.Source is LinkBundleLink)
			{
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFileLocation.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = !linkRow.Inaccessable &&
											 MainController.Instance.Settings.EditorSettings.EnableTagsEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = !linkRow.Inaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Link Bundle Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete this Link Bundle";
				barSubItemSingleLinkPropertiesImages.Caption = "Link Bundle ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Link Bundle";
			}
			else
			{
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = !linkRow.Inaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFileLocation.Visibility = !linkRow.Inaccessable && linkRow.Source is LibraryFileLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdvancedSettings.Visibility = !linkRow.Inaccessable &&
																		 linkRow.Source is LibraryFolderLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = !linkRow.Inaccessable && linkRow.Source is PreviewableLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = !linkRow.Inaccessable &&
															 MainController.Instance.Settings.EditorSettings.EnableTagsEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = !linkRow.Inaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = !linkRow.Inaccessable && (linkRow.Source is PdfLink ||
																							 linkRow.Source is ExcelLink)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Link Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete this Link";
				barSubItemSingleLinkPropertiesImages.Caption = "Link ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Link";
			}

			barButtonItemSingleLinkPropertiesLinkSettings.Visibility = !linkRow.Inaccessable
				? BarItemVisibility.Always
				: BarItemVisibility.Never;
			barSubItemSingleLinkPropertiesImages.Visibility =
				barSubItemSingleLinkPropertiesAdvanced.Visibility =
					barSubItemSingleLinkPropertiesObjectNotes.Visibility =
						barSubItemSingleLinkPropertiesLineBreakNotes.Visibility =
							barSubItemSingleLinkPropertiesObjectTextFormat.Visibility =
								barSubItemSingleLinkPropertiesLineBreakTextFormat.Visibility = !linkRow.Inaccessable
								? BarItemVisibility.Always
								: BarItemVisibility.Never;

			LoadSingleLinkContextMenuEditors(linkRow.Source);
			popupMenuSingleLinkProperties.ShowPopup(Cursor.Position);
		}

		private void OnSingleLinkPropertiesMenuCloseUp(object sender, EventArgs e)
		{
			ApplySingleLinkContextMenuEditorChanges();
		}

		private void barButtonItemLinkPropertiesCopy_ItemClick(object sender, ItemClickEventArgs e)
		{
			CopyLinks();
		}

		private void barButtonItemLinkPropertiesCut_ItemClick(object sender, ItemClickEventArgs e)
		{
			CutLinks();
		}

		private void barButtonItemLinkPropertiesPaste_ItemClick(object sender, ItemClickEventArgs e)
		{
			PasteLinks();
		}

		private void barButtonItemLinkPropertiesOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			OpenLink();
		}

		private void barButtonItemLinkPropertiesFileLocation_ItemClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkLocation();
		}

		private void barButtonItemLinkPropertiesDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteSingleLink();
		}

		private void barButtonItemLinkPropertiesRefreshPreview_ItemClick(object sender, ItemClickEventArgs e)
		{
			var sourceLink = SelectedLinkRow?.Source as PreviewableLink;
			if (sourceLink == null) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", sourceLink.NameWithExtension, Environment.NewLine)) != DialogResult.Yes) return;
			RefreshPreviewFiles(new[] { sourceLink });
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1}Is now updated for the server!", sourceLink.NameWithExtension, Environment.NewLine));
		}

		private void barButtonItemLinkPropertiesEditImageSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditImageSettings();
		}

		private void barButtonItemLinkPropertiesLinkSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Notes);
		}

		private void barButtonItemLinkPropertiesAdvancedSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.AdvancedSettings);
		}

		private void barButtonItemLinkPropertiesTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Tags);
		}

		private void barButtonItemLinkPropertiesExpirationDate_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.ExpirationDate);
		}

		private void barButtonItemLinkPropertiesSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Security);
		}

		private void barButtonItemLinkPropertiesWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Widget);
		}

		private void barButtonItemLinkPropertiesBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Banner);
		}

		private void barButtonItemLinkPropertiesResetSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetSingleLinkSettings();
		}

		private void barSubItemLinkPropertiesAddHyperlinkUrl_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<UrlLinkInfo>());
		}

		private void barSubItemLinkPropertiesAddHyperlinkYouTube_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<YouTubeLinkInfo>());
		}

		private void barSubItemLinkPropertiesAddHyperlinkVimeo_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<VimeoLinkInfo>());
		}

		private void barSubItemLinkPropertiesAddHyperlinkQuickSite_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<QuickSiteLinkInfo>());
		}

		private void barSubItemLinkPropertiesAddHyperlinkHtml5_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<Html5LinkInfo>());
		}

		private void barSubItemLinkPropertiesAddHyperlinkInternal_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<InternalWallbinLinkInfo>());
		}

		private void barSubItemLinkPropertiesAddHyperlinkLan_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<LanLinkInfo>());
		}

		private void barSubItemLinkPropertiesAddHyperlinkApp_ItemClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<AppLinkInfo>());
		}
	}
}
