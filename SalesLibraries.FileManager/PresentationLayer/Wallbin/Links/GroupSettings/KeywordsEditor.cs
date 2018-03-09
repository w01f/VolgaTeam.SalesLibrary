using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
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

			repositoryItemButtonEditSharedKeyword.Enter += EditorHelper.OnEditorEnter;
			repositoryItemButtonEditSharedKeyword.MouseUp += EditorHelper.OnEditorMouseUp;
			repositoryItemButtonEditSharedKeyword.MouseDown += EditorHelper.OnEditorMouseUp;

			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAdd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAdd.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		#region IGroupSettingsEditor Members
		public string Title => "Manage Keywords";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			gridControl.DataSource = null;
			_keywords.Clear();

			Enabled = Selection.SelectedObjects.Any();

			var commonKeywords = Selection.SelectedObjects.GetCommonKeywords().ToList();
			_keywords.AddRange(commonKeywords.Select(k => new KeywordModel { Name = k.Name, IsShared = true }));
			foreach (var link in Selection.SelectedObjects)
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
			Selection.SelectedObjects.ApplyKeywords(_keywords.ToArray());
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL KEYWORD TAGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedObjects.ApplyKeywords(new SearchTag[] { });
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