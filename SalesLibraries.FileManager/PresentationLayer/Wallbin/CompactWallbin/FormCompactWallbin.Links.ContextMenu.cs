using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin
{
	public partial class FormCompactWallbin
	{
		private TreeListNode _contextMenuTargetNode;
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
			if (_contextMenuTargetNode == null) return;
			_contextMenuTargetNode.TreeList.InvalidateNode(_contextMenuTargetNode);
			RaiseDataChanged();
		}

		private void OnSingleLinkPropertiesMenuCloseUp(Object sender, EventArgs e)
		{
			ApplySingleLinkContextMenuEditorChanges();
		}

		private void OnTreeViewMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			if (!(sender is TreeList treeList)) return;
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeList.CalcHitInfo(hitPoint);
			_contextMenuTargetNode = hitInfo.Node;
			var wallbinItem = (WallbinItem)_contextMenuTargetNode?.Tag;
			if (wallbinItem?.Type != WallbinItemType.Link) return;
			if (!(wallbinItem.Source is BaseLibraryLink sourceLink)) return;

			_contextMenuTargetNode.Selected = true;

			var isLinkInaccessable = sourceLink is LibraryFileLink && ((LibraryFileLink)sourceLink).IsDead;

			barButtonItemSingleLinkPropertiesImageSettings.Visibility = !isLinkInaccessable && (sourceLink.Banner.Enable ||
				sourceLink.Widget.Enabled ||
				sourceLink.Widget.HasAutoWidget ||
				sourceLink.Thumbnail.Enable)
				? BarItemVisibility.Always
				: BarItemVisibility.Never;
			barButtonItemSingleLinkPropertiesSecurity.Visibility =
				!isLinkInaccessable && MainController.Instance.Settings.EditorSettings.EnableSecurityEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemSingleLinkPropertiesResetSettings.Visibility =
				!isLinkInaccessable && sourceLink.GetCustomizedSettigsGroups().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			if (sourceLink is LineBreak)
			{
				barSubItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkSourceFolder.Visibility = BarItemVisibility.Never;
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
			else if (sourceLink is LinkBundleLink)
			{
				barSubItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkSourceFolder.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesThumbnail.Visibility = BarItemVisibility.Never;
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
				barSubItemSingleLinkPropertiesOpenLink.Visibility = !isLinkInaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkSourceFolder.Visibility = sourceLink is LibraryFileLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkSiteLink.Visibility = sourceLink.ParentLibrary != null &&
																			   sourceLink.ParentLibrary.SyncDate > sourceLink.AddDate
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesOpenLinkOneDriveLink.Visibility = MainController.Instance.Settings.OneDriveSettings.Enabled &&
																				sourceLink is PreviewableFileLink fileLink &&
																				!String.IsNullOrEmpty(fileLink.OneDriveSettings.Url)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = !isLinkInaccessable && sourceLink is LibraryFolderLink &&
																		 ((LibraryFolderLink)sourceLink).AllLinks.Any(l => l.Type == LinkType.Excel || l.Type == LinkType.Pdf)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFolderLinkPdfSettings.Enabled = !isLinkInaccessable &&
																				 sourceLink is LibraryFolderLink &&
																				 ((LibraryFolderLink)sourceLink).AllLinks.Any(
																					 l => l.Type == LinkType.Pdf);
				barButtonItemSingleLinkPropertiesFolderLinkExcelSettings.Enabled = !isLinkInaccessable &&
																				 sourceLink is LibraryFolderLink &&
																				 ((LibraryFolderLink)sourceLink).AllLinks.Any(
																					 l => l.Type == LinkType.Excel);
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = !isLinkInaccessable && sourceLink is IPreviewableLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = !isLinkInaccessable && MainController.Instance.Settings.EditorSettings.EnableTagsEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = !isLinkInaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesAdminSettings.Visibility = !isLinkInaccessable && (sourceLink is PdfLink ||
																							 sourceLink is ExcelLink)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesThumbnail.Visibility = !isLinkInaccessable && sourceLink is IThumbnailSettingsHolder
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Link Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete";
				barSubItemSingleLinkPropertiesImages.Caption = "Link ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Link";

				if (sourceLink is PdfLink)
					barButtonItemSingleLinkPropertiesAdminSettings.Caption = "PDF Settings";
				else if (sourceLink is ExcelLink)
					barButtonItemSingleLinkPropertiesAdminSettings.Caption = "Excel Settings";
			}

			barButtonItemSingleLinkPropertiesLinkSettings.Visibility = !isLinkInaccessable
				? BarItemVisibility.Always
				: BarItemVisibility.Never;
			barSubItemSingleLinkPropertiesImages.Visibility =
				barSubItemSingleLinkPropertiesAdvanced.Visibility =
					barSubItemSingleLinkPropertiesObjectNotes.Visibility =
						barSubItemSingleLinkPropertiesLineBreakNotes.Visibility =
							barSubItemSingleLinkPropertiesObjectTextFormat.Visibility =
								barSubItemSingleLinkPropertiesLineBreakTextFormat.Visibility = !isLinkInaccessable
								? BarItemVisibility.Always
								: BarItemVisibility.Never;

			LoadSingleLinkContextMenuEditors(sourceLink);
			popupMenuSingleLinkProperties.ShowPopup(Cursor.Position);
		}

		private void OnSingleLinkPropertiesImageSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditImageSettings(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesOpenLinkSourceFileClick(object sender, ItemClickEventArgs e)
		{
			OpenLink(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesOpenLinkSourceFolderClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkLocation(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesOpenLinkOneDriveLinkClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			OpenLinkOnOneDrive(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesOpenLinkSiteLinkClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			OpenLinkOnSite(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesDeleteClick(object sender, ItemClickEventArgs e)
		{
			DeleteSingleLink(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesLinkSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Notes);
		}

		private void OnSingleLinkPropertiesTagsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Tags);
		}

		private void OnSingleLinkPropertiesWidgetClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Widget);
		}

		private void OnSingleLinkPropertiesBannerClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Banner);
		}

		private void OnSingleLinkPropertiesThumbnailClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Thumbnail);
		}

		private void OnSingleLinkPropertiesResetSettingsClick(object sender, ItemClickEventArgs e)
		{
			ResetSingleLinkSettings(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesRefreshPreviewClick(object sender, ItemClickEventArgs e)
		{
			RefreshLinkPreviewFiles(_contextMenuTargetNode);
		}

		private void OnSingleLinkPropertiesExpirationDateClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.ExpirationDate);
		}

		private void OnSingleLinkPropertiesSecurityClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Security);
		}

		private void OnSingleLinkPropertiesFolderLinkExcelSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.AdminSettings, LinkType.Excel);
		}

		private void OnSingleLinkPropertiesFolderLinkPdfSettingsClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.AdminSettings, LinkType.Pdf);
		}

		private void OnSingleLinkPropertiesAdminSettingsClick(object sender, ItemClickEventArgs e)
		{
			var wallbinItem = (WallbinItem)_contextMenuTargetNode?.Tag;
			if (wallbinItem?.Type != WallbinItemType.Link) return;
			var sourceLink = wallbinItem.Source as BaseLibraryLink;
			if (sourceLink is PdfLink)
				EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.AdminSettings, LinkType.Pdf);
			else if (sourceLink is ExcelLink)
				EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.AdminSettings, LinkType.Excel);
		}
	}
}
