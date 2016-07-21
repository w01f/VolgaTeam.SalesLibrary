using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public sealed partial class KeywordsEditor : UserControl, IGroupSettingsEditor
	{
		private readonly List<SearchTag> _keywords = new List<SearchTag>();

		private SelectionManager Selection => MainController.Instance.WallbinViews.Selection;

		public KeywordsEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			repositoryItemButtonEditKeyword.Enter += EditorHelper.EditorEnter;
			repositoryItemButtonEditKeyword.MouseUp += EditorHelper.EditorMouseUp;
			repositoryItemButtonEditKeyword.MouseDown += EditorHelper.EditorMouseUp;

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
			pnButtons.Enabled = false;
			pnData.Enabled = false;
			gridControl.DataSource = null;
			_keywords.Clear();
			Enabled = false;

			var defaultLink = Selection.SelectedFiles.FirstOrDefault(link => link.Tags.HasKeywords) ?? Selection.SelectedFiles.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = Selection.SelectedFiles.All(link => link.Tags.Keywords.Any());
			var sameData = Selection.SelectedFiles.All(link => link.Tags.Keywords.Compare(defaultLink.Tags.Keywords));

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
				_keywords.AddRange(defaultLink.Tags.Keywords);

			gridControl.DataSource = _keywords;
		}

		public void ApplyData()
		{
			gridView.CloseEditor();
			_keywords.RemoveAll(tag => String.IsNullOrEmpty(tag.Name));
			Selection.SelectedFiles.ApplyKeywords(_keywords.Where(tag => !String.IsNullOrEmpty(tag.Name)).ToArray());
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL KEYWORD TAGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedFiles.ApplyKeywords(new SearchTag[] { });
			EditorChanged?.Invoke(this, new EventArgs());
			UpdateData();
		}
		#endregion

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			ResetData();
		}

		private void repositoryItemButtonEditKeyword_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridView.CloseEditor();
			if (gridView.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			_keywords.RemoveAt(gridView.GetDataSourceRowIndex(gridView.FocusedRowHandle));
			gridView.RefreshData();
			ApplyData();
		}

		private void buttonXAdd_Click(object sender, EventArgs e)
		{
			gridView.CloseEditor();
			_keywords.RemoveAll(tag => string.IsNullOrEmpty(tag.Name));
			_keywords.Add(new SearchTag());
			gridView.RefreshData();
			if (gridView.RowCount > 0)
			{
				gridView.FocusedRowHandle = gridView.RowCount - 1;
				gridView.MakeRowVisible(gridView.FocusedRowHandle, true);
			}
		}

		private void gridView_HiddenEditor(object sender, EventArgs e)
		{
			ApplyData();
		}
	}
}