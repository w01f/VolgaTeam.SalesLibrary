using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public sealed partial class KeywordsEditor : UserControl, IGroupSettingsEditor
	{
		private readonly List<KeywordModel> _keywords = new List<KeywordModel>();

		private SelectionManager Selection => MainController.Instance.WallbinViews.Selection;

		public KeywordsEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			repositoryItemButtonEditSharedKeyword.Enter += EditorHelper.EditorEnter;
			repositoryItemButtonEditSharedKeyword.MouseUp += EditorHelper.EditorMouseUp;
			repositoryItemButtonEditSharedKeyword.MouseDown += EditorHelper.EditorMouseUp;

			if (!((CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			buttonXAdd.Font = new Font(buttonXAdd.Font.FontFamily, buttonXAdd.Font.Size - 2, buttonXAdd.Font.Style);
			buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
		}

		#region IGroupSettingsEditor Members
		public string Title => "Manage Keywords";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			gridControl.DataSource = null;
			_keywords.Clear();
			Enabled = false;

			Enabled = Selection.SelectedLinks.Any();

			var commonKeywords = Selection.SelectedLinks.GetCommonKeywords().ToList();
			_keywords.AddRange(commonKeywords.Select(k => new KeywordModel { Name = k.Name, IsShared = true }));
			foreach (var link in Selection.SelectedLinks)
			{
				_keywords.AddRange(link.Tags.Keywords
					.Where(k => !commonKeywords.Any(commonKeyword => commonKeyword.Equals(k)))
						.Select(k => new KeywordModel { Name = k.Name, IsShared = false }));
			}

			gridControl.DataSource = _keywords;
		}

		public void ApplyData()
		{
			gridView.CloseEditor();
			_keywords.RemoveAll(tag => String.IsNullOrEmpty(tag.Name));
			Selection.SelectedLinks.ApplyKeywords(_keywords.ToArray());
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL KEYWORD TAGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedLinks.ApplyKeywords(new SearchTag[] { });
			EditorChanged?.Invoke(this, new EventArgs());
			UpdateData();
		}
		#endregion

		private void OnResetClick(object sender, EventArgs e)
		{
			ResetData();
		}

		private void OnAddClick(object sender, EventArgs e)
		{
			gridView.CloseEditor();
			_keywords.RemoveAll(tag => string.IsNullOrEmpty(tag.Name));
			_keywords.Add(new KeywordModel { IsShared = true });
			gridView.RefreshData();
			if (gridView.RowCount > 0)
			{
				gridView.FocusedRowHandle = gridView.RowCount - 1;
				gridView.MakeRowVisible(gridView.FocusedRowHandle, true);
			}
		}

		private void OnKeywordEditorButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridView.CloseEditor();
			if (gridView.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var keyword = (KeywordModel)gridView.GetFocusedRow();
			switch (e.Button.Tag as String)
			{
				case "Delete":
					_keywords.Remove(keyword);
					break;
				case "MakeShared":
					keyword.IsShared = true;
					break;
			}
			gridView.RefreshData();
			ApplyData();
		}

		private void OnHideEditor(object sender, EventArgs e)
		{
			ApplyData();
		}

		private void OnGridViewCustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
		{
			var keyword = (KeywordModel)gridView.GetRow(e.RowHandle);
			e.RepositoryItem = keyword.IsShared ?
				repositoryItemButtonEditSharedKeyword :
				repositoryItemButtonEditPartialKeyword;
		}

		private void OnGridViewRowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			var keyword = (KeywordModel)gridView.GetRow(e.RowHandle);
			e.Appearance.ForeColor = keyword.IsShared ?
				Color.Black :
				Color.Gray;
		}
	}
}