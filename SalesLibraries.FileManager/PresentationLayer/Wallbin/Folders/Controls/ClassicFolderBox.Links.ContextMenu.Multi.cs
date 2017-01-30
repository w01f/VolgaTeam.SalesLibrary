using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		private readonly List<BaseContextMenuEditor> _multiLinksContextMenuEditors = new List<BaseContextMenuEditor>();
		private LinkRow _defaultMultuLinkRow;

		private void InitMultiLinksContextMenuEditors()
		{
			var libraryObjectNotesEditor = new LibraryObjectNotesEditor(barSubItemMultiLinkPropertiesLinkNotes);
			libraryObjectNotesEditor.EditValueChanged += OnMultiLinksContextEditorValueChanged;
			_multiLinksContextMenuEditors.Add(libraryObjectNotesEditor);

			var lineBreakNotesEditor = new LineBreakNotesEditor(barSubItemMultiLinkPropertiesLineBreakNotes);
			lineBreakNotesEditor.EditValueChanged += OnMultiLinksContextEditorValueChanged;
			_multiLinksContextMenuEditors.Add(lineBreakNotesEditor);

			var libraryObjectFormatEditor = new LibraryObjectTextFormatEditor(barSubItemMultiLinkPropertiesFormatLink);
			libraryObjectFormatEditor.EditValueChanged += OnMultiLinksContextEditorValueChanged;
			_multiLinksContextMenuEditors.Add(libraryObjectFormatEditor);

			var lineBreakFormatEditor = new LineBreakTextFormatEditor(barSubItemMultiLinkPropertiesFormatLineBreak);
			lineBreakFormatEditor.EditValueChanged += OnMultiLinksContextEditorValueChanged;
			_multiLinksContextMenuEditors.Add(lineBreakFormatEditor);

			popupMenuMultiLinkProperties.CloseUp += OnMultiLinkPropertiesMenuCloseUp;
		}

		private void LoadMultiLinkContextMenuEditors(IList<BaseLibraryLink> links)
		{
			_multiLinksContextMenuEditors.ForEach(e => e.LoadLinks(links));
		}

		private void ApplyMultiLinksContextMenuEditorChanges()
		{
			_multiLinksContextMenuEditors.ForEach(e => e.ApplyChanges());
		}

		private void ProcessMultiLinksContextMenu(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (e.Button != MouseButtons.Right) return;
			if (SelectionManager.SelectedLinks.Count <= 1) return;

			_defaultMultuLinkRow = (LinkRow)grFiles.Rows[e.RowIndex];

			barButtonItemMultiLinkPropertiesSecurity.Visibility = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemMultiLinkPropertiesTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit && SelectionManager.SelectedLinks.OfType<LibraryObjectLink>().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemMultiLinkPropertiesExpirationDate.Visibility = SelectionManager.SelectedLinks.OfType<LibraryObjectLink>().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemMultiLinkPropertiesRefreshPreviewFiles.Visibility = SelectionManager.SelectedLinks.OfType<PreviewableLink>().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

			LoadMultiLinkContextMenuEditors(SelectionManager.SelectedLinks.ToList());
			popupMenuMultiLinkProperties.ShowPopup(Cursor.Position);
		}

		private void OnMultiLinksContextEditorValueChanged(object sender, EventArgs e)
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
			MultiLinksSettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnMultiLinkPropertiesMenuCloseUp(object sender, EventArgs e)
		{
			ApplyMultiLinksContextMenuEditorChanges();
			_defaultMultuLinkRow = null;
		}

		private void barButtonItemMultiLinkPropertiesResetSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetMultiLinkSettings();
		}

		private void barButtonItemMultiLinkPropertiesDeleteLinks_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteMultiLinks(SelectionManager.SelectedLinks.ToList());
		}

		private void barButtonItemMultiLinkPropertiesWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			var linkGroup = new[] { _defaultMultuLinkRow.Source }
				.Union(SelectionManager.SelectedLinks.Where(link => link.ExtId != _defaultMultuLinkRow.Source.ExtId))
				.ToMultiLinkSet();
			EditLinksGroupSettings(linkGroup, LinkSettingsType.Widget);
		}

		private void barButtonItemMultiLinkPropertiesBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			var linkGroup = new[] { _defaultMultuLinkRow.Source }
				.Union(SelectionManager.SelectedLinks.Where(link => link.ExtId != _defaultMultuLinkRow.Source.ExtId))
				.ToMultiLinkSet();
			EditLinksGroupSettings(linkGroup, LinkSettingsType.Banner);
		}

		private void barButtonItemMultiLinkPropertiesTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!(_defaultMultuLinkRow.Source is LibraryObjectLink)) return;
			var linkGroup = new[] { _defaultMultuLinkRow.Source }
				.Union(SelectionManager.SelectedLinks.OfType<LibraryObjectLink>().Where(link => link.ExtId != _defaultMultuLinkRow.Source.ExtId))
				.ToMultiLinkSet();
			EditLinksGroupSettings(linkGroup, LinkSettingsType.Tags);
		}

		private void barButtonItemMultiLinkPropertiesSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			var linkGroup = new[] { _defaultMultuLinkRow.Source }
				.Union(SelectionManager.SelectedLinks.Where(link => link.ExtId != _defaultMultuLinkRow.Source.ExtId))
				.ToMultiLinkSet();
			EditLinksGroupSettings(linkGroup, LinkSettingsType.Security);
		}

		private void barButtonItemMultiLinkPropertiesExpirationDate_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!(_defaultMultuLinkRow.Source is LibraryObjectLink)) return;
			var linkGroup = new[] { _defaultMultuLinkRow.Source }
				.Union(SelectionManager.SelectedLinks.OfType<LibraryObjectLink>().Where(link => link.ExtId != _defaultMultuLinkRow.Source.ExtId))
				.ToMultiLinkSet();
			EditLinksGroupSettings(linkGroup, LinkSettingsType.ExpirationDate);
		}

		private void barButtonItemMultiLinkPropertiesRefreshPreviewFiles_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to refresh the server files for selected links?") != DialogResult.Yes) return;
			RefreshPreviewFiles(SelectionManager.SelectedLinks.OfType<PreviewableLink>().ToList());
			MainController.Instance.PopupMessages.ShowInfo("Links are now updated for the server!");
		}
	}
}
