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
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.CompactWallbin
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
			var treeList = sender as TreeList;
			if (treeList == null) return;
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeList.CalcHitInfo(hitPoint);
			_contextMenuTargetNode = hitInfo.Node;
			var wallbinItem = (WallbinItem)_contextMenuTargetNode?.Tag;
			if (wallbinItem?.Type != WallbinItemType.Link) return;
			var sourceLink = wallbinItem.Source as BaseLibraryLink;
			if (sourceLink == null) return;

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
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFileLocation.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesThumbnail.Visibility = BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Line Break Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete this Line Break";
				barSubItemSingleLinkPropertiesImages.Caption = "Line Break ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Line Break";
			}
			else if (sourceLink is LinkBundleLink)
			{
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFileLocation.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
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
				barButtonItemSingleLinkPropertiesOpenLink.Visibility = !isLinkInaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFileLocation.Visibility = !isLinkInaccessable && sourceLink is LibraryFileLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesFolderLinkSettings.Visibility = !isLinkInaccessable && sourceLink is LibraryFolderLink &&
																		 ((LibraryFolderLink)sourceLink).AllLinks.Any(l => l.Type == FileTypes.Excel || l.Type == FileTypes.Pdf)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesFolderLinkPdfSettings.Enabled = !isLinkInaccessable &&
																				 sourceLink is LibraryFolderLink &&
																				 ((LibraryFolderLink)sourceLink).AllLinks.Any(
																					 l => l.Type == FileTypes.Pdf);
				barButtonItemSingleLinkPropertiesFolderLinkExcelSettings.Enabled = !isLinkInaccessable &&
																				 sourceLink is LibraryFolderLink &&
																				 ((LibraryFolderLink)sourceLink).AllLinks.Any(
																					 l => l.Type == FileTypes.Excel);
				barButtonItemSingleLinkPropertiesRefreshPreview.Visibility = !isLinkInaccessable && sourceLink is IPreviewableLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesTags.Visibility = !isLinkInaccessable && MainController.Instance.Settings.EditorSettings.EnableTagsEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesExpirationDate.Visibility = !isLinkInaccessable
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barSubItemSingleLinkPropertiesAdminSettings.Visibility = !isLinkInaccessable && (sourceLink is PdfLink ||
																							 sourceLink is ExcelLink)
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemSingleLinkPropertiesThumbnail.Visibility = !isLinkInaccessable && sourceLink is IThumbnailSettingsHolder
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

				barButtonItemSingleLinkPropertiesLinkSettings.Caption = "Link Settings";
				barButtonItemSingleLinkPropertiesDelete.Caption = "Delete this Link";
				barSubItemSingleLinkPropertiesImages.Caption = "Link ART";
				barButtonItemSingleLinkPropertiesResetSettings.Caption = "Reset this Link";
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

		private void barButtonItemSingleLinkPropertiesImageSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditImageSettings(_contextMenuTargetNode);
		}

		private void barButtonItemSingleLinkPropertiesOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			OpenLink(_contextMenuTargetNode);
		}

		private void barButtonItemSingleLinkPropertiesDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteSingleLink(_contextMenuTargetNode);
		}

		private void barButtonItemSingleLinkPropertiesLinkSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Notes);
		}

		private void barButtonItemSingleLinkPropertiesTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Tags);
		}

		private void barButtonItemSingleLinkPropertiesWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Widget);
		}

		private void barButtonItemSingleLinkPropertiesBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Banner);
		}

		private void barButtonItemSingleLinkPropertiesThumbnail_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Thumbnail);
		}

		private void barButtonItemSingleLinkPropertiesResetSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetSingleLinkSettings(_contextMenuTargetNode);
		}

		private void barButtonItemSingleLinkPropertiesFileLocation_ItemClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkLocation(_contextMenuTargetNode);
		}

		private void barButtonItemSingleLinkPropertiesRefreshPreview_ItemClick(object sender, ItemClickEventArgs e)
		{
			RefreshLinkPreviewFiles(_contextMenuTargetNode);
		}

		private void barButtonItemSingleLinkPropertiesExpirationDate_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.ExpirationDate);
		}

		private void barButtonItemSingleLinkPropertiesSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.Security);
		}

		private void barButtonItemSingleLinkPropertiesFolderLinkExcelSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.AdminSettings, FileTypes.Excel);
		}

		private void barButtonItemSingleLinkPropertiesFolderLinkPdfSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditSingleLinkSettings(_contextMenuTargetNode, LinkSettingsType.AdminSettings, FileTypes.Pdf);
		}
	}
}
