using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class KeywordsEditor : UserControl, ITagsEditor
	{
		public KeywordsEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			Keywords = new List<StringDataSourceWrapper>();

			repositoryItemButtonEditKeyword.Enter += FormMain.Instance.EditorEnter;
			repositoryItemButtonEditKeyword.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditKeyword.MouseDown += FormMain.Instance.EditorMouseUp;

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

		private bool _needToApply;
		public bool NeedToApply
		{
			get { return _needToApply; }
			set
			{
				_needToApply = value;
				var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
				if (activePage != null) activePage.Parent.StateChanged = true;
			}
		}
		public List<StringDataSourceWrapper> Keywords { get; private set; }

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			pnButtons.Enabled = false;
			pnData.Enabled = false;
			gridControl.DataSource = null;
			Keywords.Clear();
			Enabled = false;

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = activePage.SelectedLinks.All(x => x.CustomKeywords.Tags.Count == 0);
			var sameData = defaultLink != null && activePage.SelectedLinks.All(x => x.CustomKeywords.Compare(defaultLink.CustomKeywords));

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
				Keywords.AddRange(defaultLink.CustomKeywords.Tags.Select(x => new StringDataSourceWrapper(x.Name)));

			gridControl.DataSource = Keywords;
		}

		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			gridView.CloseEditor();
			Keywords.RemoveAll(x => string.IsNullOrEmpty(x.Value));

			foreach (var link in activePage.SelectedLinks)
			{
				link.CustomKeywords.Tags.Clear();
				link.CustomKeywords.Tags.AddRange(Keywords.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => new SearchTag(link.CustomKeywords.Name) { Name = x.Value }));
			}

			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}
		#endregion

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure You want to DELETE ALL KEYWORD TAGS for the selected files?") != DialogResult.Yes) return;
			foreach (var link in activePage.SelectedLinks)
				link.CustomKeywords.Tags.Clear();
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

			UpdateData();
		}

		private void repositoryItemButtonEditKeyword_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridView.CloseEditor();
			if (gridView.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				Keywords.RemoveAt(gridView.GetDataSourceRowIndex(gridView.FocusedRowHandle));
				gridView.RefreshData();
				NeedToApply = true;
			}
		}

		private void buttonXAdd_Click(object sender, EventArgs e)
		{
			gridView.CloseEditor();
			Keywords.RemoveAll(x => string.IsNullOrEmpty(x.Value));
			Keywords.Add(new StringDataSourceWrapper());
			gridView.RefreshData();
			if (gridView.RowCount > 0)
			{
				gridView.FocusedRowHandle = gridView.RowCount - 1;
				gridView.MakeRowVisible(gridView.FocusedRowHandle, true);
			}
			NeedToApply = true;
		}
	}
}