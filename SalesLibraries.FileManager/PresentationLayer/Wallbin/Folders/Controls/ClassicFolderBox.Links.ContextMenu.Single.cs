using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Interfaces;
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
				barButtonItemSingleLinkPropertiesOpenLinkSourceFile.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesDelete.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesImageSettings.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesObjectNotes.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = BarItemVisibility.Never;
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
				barButtonItemSingleLinkPropertiesOpenLinkSourceFile.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesDelete.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesLinkSettings.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesImageSettings.Visibility = BarItemVisibility.Always;
				barSubItemSingleLinkPropertiesObjectNotes.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Always;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = BarItemVisibility.Always;
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
				barSubItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesOneDrive.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesThumbnail.Visibility = BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Line Break Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete this Line Break";
				barSubItemSingleLinkPropertiesImages.Caption = "Line Break ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Line Break";
			}
			else if (linkRow.Source is LinkBundleLink)
			{
				barSubItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesOneDrive.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesThumbnail.Visibility = BarItemVisibility.Always;
				barButtonItemSingleLinkPropertiesTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = BarItemVisibility.Always;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Link Bundle Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete this Link Bundle";
				barSubItemSingleLinkPropertiesImages.Caption = "Link Bundle ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Link Bundle";
			}
			else
			{
				var fileLink = linkRow.Source as LibraryFileLink;

				barSubItemSingleLinkPropertiesOpenLink.Visibility = !linkRow.Inaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkSourceFolder.Visibility = fileLink != null
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkSiteLink.Visibility = linkRow.Source?.ParentLibrary != null &&
																			   linkRow.Source != null &&
																			   linkRow.Source.ParentLibrary.SyncDate > linkRow.Source.AddDate
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkOneDriveLink.Visibility = MainController.Instance.Settings.OneDriveSettings.Enabled && !String.IsNullOrEmpty(fileLink?.OneDriveSettings.Url)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barSubItemSingleLinkPropertiesOneDrive.Visibility = MainController.Instance.Settings.OneDriveSettings.Enabled && fileLink != null
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOneDriveOpenUrl.Visibility =
				barButtonItemSingleLinkPropertiesOneDriveCopyUrl.Visibility = !String.IsNullOrEmpty(fileLink?.OneDriveSettings.Url)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = !linkRow.Inaccessable &&
																		 linkRow.Source is LibraryFolderLink &&
																		 ((LibraryFolderLink)linkRow.Source).AllLinks.Any(l => l.Type == LinkType.Excel || l.Type == LinkType.Pdf)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFolderLinkPdfSettings.Enabled = !linkRow.Inaccessable &&
																				 linkRow.Source is LibraryFolderLink &&
																				 ((LibraryFolderLink)linkRow.Source).AllLinks.Any(
																					 l => l.Type == LinkType.Pdf);
				barButtonItemSingleLinkPropertiesFolderLinkExcelSettings.Enabled = !linkRow.Inaccessable &&
																				 linkRow.Source is LibraryFolderLink &&
																				 ((LibraryFolderLink)linkRow.Source).AllLinks.Any(
																					 l => l.Type == LinkType.Excel);
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility =
					!linkRow.Inaccessable &&
						(linkRow.Source is IPreviewableLink || (linkRow.Source is LibraryFolderLink && ((LibraryFolderLink)linkRow.Source).AllLinks.OfType<PreviewableFileLink>().Any()))
						? BarItemVisibility.Always
						: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = !linkRow.Inaccessable &&
															 MainController.Instance.Settings.EditorSettings.EnableTagsEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = !linkRow.Inaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdminSettings.Visibility = !linkRow.Inaccessable && (linkRow.Source is PdfLink ||
																							 linkRow.Source is ExcelLink)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesThumbnail.Visibility = !linkRow.Inaccessable &&
																		 linkRow.Source is IThumbnailSettingsHolder
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Link Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete";
				barSubItemSingleLinkPropertiesImages.Caption = "Link ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Link";

				if (linkRow.Source is PdfLink)
					barButtonItemSingleLinkPropertiesAdminSettings.Caption = "PDF Settings";
				else if (linkRow.Source is ExcelLink)
					barButtonItemSingleLinkPropertiesAdminSettings.Caption = "Excel Settings";
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

		private void OnSingleLinkPropertiesCopyClick(object sender, ItemClickEventArgs e)
		{
			CopyLinks();
		}

		private void OnSingleLinkPropertiesCutClick(object sender, ItemClickEventArgs e)
		{
			CutLinks();
		}

		private void OnSingleLinkPropertiesPasteClick(object sender, ItemClickEventArgs e)
		{
			PasteLinks();
		}

		private void OnSingleLinkPropertiesOpenLinkSourceFileClick(object sender, ItemClickEventArgs e)
		{
			OpenLink();
		}

		private void OnSingleLinkPropertiesOpenLinkSourceFolderClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkLocation();
		}

		private void OnSingleLinkPropertiesOpenLinkSiteLinkClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkOnSite();
		}

		private void OnSingleLinkPropertiesOpenLinkOneDriveLinkClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkOnOneDrive();
		}

		private void OnSingleLinkPropertiesDeleteClick(object sender, ItemClickEventArgs e)
		{
			DeleteSingleLink();
		}

		private void OnSingleLinkPropertiesOneDriveOpenUrlClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkOnOneDrive();
		}

		private void OnSingleLinkPropertiesOneDriveCopyUrlClick(object sender, ItemClickEventArgs e)
		{
			CopyOneDriveUrl();
		}

		private void OnSingleLinkPropertiesOneDriveResetUrlClick(object sender, ItemClickEventArgs e)
		{
			ResetOneDriveUrl();
		}

		private void OnSingleLinkPropertiesRefreshPreviewClick(object sender, ItemClickEventArgs e)
		{
			var sourceLinks = new List<IPreviewableLink>();
			var linkTitle = String.Empty;
			if (SelectedLinkRow?.Source is IPreviewableLink)
			{
				linkTitle = ((IPreviewableLink)SelectedLinkRow?.Source).PreviewName;
				sourceLinks.Add((IPreviewableLink)SelectedLinkRow?.Source);
			}
			else if (SelectedLinkRow?.Source is LibraryFolderLink)
			{
				linkTitle = SelectedLinkRow?.Source.LinkInfoDisplayName;
				sourceLinks.AddRange(((LibraryFolderLink)SelectedLinkRow?.Source).AllLinks.OfType<PreviewableFileLink>());
			}

			if (!sourceLinks.Any()) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", linkTitle, Environment.NewLine)) != DialogResult.Yes) return;
			RefreshPreviewFiles(sourceLinks);
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1}Is now updated for the server!", linkTitle, Environment.NewLine));
		}

		private void OnSingleLinkPropertiesEditImageSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditImageSettings();
		}

		private void OnSingleLinkPropertiesLinkSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Notes);
		}

		private void OnSingleLinkPropertiesTagsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Tags);
		}

		private void OnSingleLinkPropertiesExpirationDateClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.ExpirationDate);
		}

		private void OnSingleLinkPropertiesSecurityClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Security);
		}

		private void OnSingleLinkPropertiesWidgetClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Widget);
		}

		private void OnSingleLinkPropertiesBannerClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Banner);
		}

		private void OnSingleLinkPropertiesThumbnailClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.Thumbnail);
		}

		private void OnSingleLinkPropertiesResetSettingsClick(object sender, ItemClickEventArgs e)
		{
			ResetSingleLinkSettings();
		}

		private void OnSingleLinkPropertiesAddHyperlinkUrlClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<UrlLinkInfo>());
		}

		private void OnSingleLinkPropertiesAddHyperlinkYouTubeClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<YouTubeLinkInfo>());
		}

		private void OnSingleLinkPropertiesAddHyperlinkVimeoClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<VimeoLinkInfo>());
		}

		private void OnSingleLinkPropertiesAddHyperlinkQuickSiteClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<QuickSiteLinkInfo>());
		}

		private void OnSingleLinkPropertiesAddHyperlinkHtml5Click(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<Html5LinkInfo>());
		}

		private void OnSingleLinkPropertiesAddHyperlinkInternalClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<InternalWallbinLinkInfo>());
		}

		private void OnSingleLinkPropertiesAddHyperlinkLanClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<LanLinkInfo>());
		}

		private void OnSingleLinkPropertiesAddHyperlinkAppClick(object sender, ItemClickEventArgs e)
		{
			AddHyperLink(BaseNetworkLinkInfo.GetDefault<AppLinkInfo>());
		}

		private void OnSingleLinkPropertiesFolderLinkPdfSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.AdminSettings, LinkType.Pdf);
		}

		private void OnSingleLinkPropertiesFolderLinkExcelSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(LinkSettingsType.AdminSettings, LinkType.Excel);
		}

		private void OnSingleLinkPropertiesAdminSettingsClick(object sender, ItemClickEventArgs e)
		{
			var sourceLink = SelectedLinkRow?.Source as IPreviewableLink;
			if (sourceLink is PdfLink)
				EditSingleLinkSettings(LinkSettingsType.AdminSettings, LinkType.Pdf);
			else if (sourceLink is ExcelLink)
				EditSingleLinkSettings(LinkSettingsType.AdminSettings, LinkType.Excel);
		}
	}
}
