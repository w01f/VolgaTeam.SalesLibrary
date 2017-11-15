using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormColumns : MetroForm
	{
		private bool _allowToSave;
		private LibraryPage _currentPage;
		private bool _stateChanged;

		public Library Library { get; set; }

		public FormColumns()
		{
			InitializeComponent();

			layoutControlItemAdd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAdd.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAlignByColumns.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAlignByColumns.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAlignByColumns.MinSize = RectangleHelper.ScaleSize(layoutControlItemAlignByColumns.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAlignByRows.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAlignByRows.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAlignByRows.MinSize = RectangleHelper.ScaleSize(layoutControlItemAlignByRows.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));

			layoutControlItemSave.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSave.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSave.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		#region Base Methods
		private void GetColumnSettings()
		{
			_allowToSave = false;
			checkEditEnableColumnTitles.Checked = _currentPage.Settings.EnableColumnTitles;

			layoutControl.SuspendLayout();
			tabbedControlGroupWindows.BeginUpdate();
			foreach (var columnSettingsTab in tabbedControlGroupWindows.TabPages.OfType<ColumnSettings>().ToList())
			{
				columnSettingsTab.ReleaseControl();
				tabbedControlGroupWindows.RemoveTabPage(columnSettingsTab);
			}
			for (int i = 0; i < LibraryPage.ColumnsCount; i++)
			{
				var columnPage = new ColumnSettings(_currentPage, i);
				columnPage.FolderChanged += (o, e) => { _stateChanged = true; };
				columnPage.FolderMovedLeft += OnMoveFolderLeft;
				columnPage.FolderMovedRight += OnMoveFolderRight;
				tabbedControlGroupWindows.AddTabPage(columnPage);
			}
			tabbedControlGroupWindows.SelectedTabPage = tabbedControlGroupWindows.TabPages.OfType<ColumnSettings>().FirstOrDefault();
			tabbedControlGroupWindows.EndUpdate();
			layoutControl.ResumeLayout(true);

			foreach (var targetPage in tabbedControlGroupWindows.TabPages.OfType<ColumnSettings>())
				foreach (var columnPage in tabbedControlGroupWindows.TabPages.OfType<ColumnSettings>().Where(p => p != targetPage))
				{
					targetPage.FolderCopied += (o, e) =>
					{
						var page = o as ColumnSettings;
						if (page != columnPage)
							columnPage.FolderInBuffer = e.SourceFolder;
					};
					targetPage.FolderPasted += (o, e) =>
					{
						columnPage.FolderInBuffer = null;
						if (columnPage.ColumnOrder == e.OldColumnOrder)
							columnPage.LoadData();
					};
				}
			_allowToSave = true;
		}

		private void SaveColumnSettings()
		{
			if (_currentPage == null) return;
			foreach (var columnPage in tabbedControlGroupWindows.TabPages.OfType<ColumnSettings>())
				columnPage.SaveData();
			_currentPage.Settings.EnableColumnTitles = checkEditEnableColumnTitles.Checked;
		}

		private void PopulatePagesList()
		{
			comboBoxEditPages.Properties.Items.Clear();
			comboBoxEditPages.Properties.Items.AddRange(Library.Pages.ToList());
			comboBoxEditPages.EditValue = Library.Pages.FirstOrDefault();
			layoutControlItemPages.Visibility = Library.Pages.Count > 1 ? LayoutVisibility.Always : LayoutVisibility.Never;
		}
		#endregion

		#region Base GUI
		private void Form_Load(object sender, EventArgs e)
		{
			_currentPage = null;
			PopulatePagesList();
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (!_stateChanged) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Before you EXIT, do you want to save the changes you made?") == DialogResult.Yes)
			{
				e.Cancel = false;
				return;
			}
			if (MessageBox.Show("You are about to lose your changes.\nThe changes will be LOST FOREVER & EVER & EVER!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
				e.Cancel = true;
		}

		private void FormColumns_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveColumnSettings();
		}
		#endregion

		#region Columns Tab GUI
		private void comboBoxEditPages_EditValueChanged(object sender, EventArgs e)
		{
			SaveColumnSettings();
			if (comboBoxEditPages.EditValue == null) return;
			_currentPage = comboBoxEditPages.EditValue as LibraryPage;
			GetColumnSettings();
		}

		private void btAddWindow_Click(object sender, EventArgs e)
		{
			var columnPage = tabbedControlGroupWindows.SelectedTabPage as ColumnSettings;
			if (columnPage == null) return;
			columnPage.AddFolder();
			_stateChanged = true;
		}

		private void OnMoveFolderRight(object sender, EventArgs e)
		{
			var columnPage = sender as ColumnSettings;
			if (columnPage == null) return;
			if (columnPage.ColumnOrder == 2) return;
			var nextPage = tabbedControlGroupWindows.TabPages[columnPage.ColumnOrder + 1] as ColumnSettings;
			columnPage.CopyFolder();
			nextPage.PasteFolder();
			_stateChanged = true;
		}

		private void OnMoveFolderLeft(object sender, EventArgs e)
		{
			var columnPage = sender as ColumnSettings;
			if (columnPage == null) return;
			if (columnPage.ColumnOrder == 0) return;
			var prevPage = tabbedControlGroupWindows.TabPages[columnPage.ColumnOrder - 1] as ColumnSettings;
			columnPage.CopyFolder();
			prevPage.PasteFolder();
			_stateChanged = true;
		}

		private void OnAlignByColumnsClick(object sender, EventArgs e)
		{
			if (_currentPage == null) return;
			_currentPage.AlignFoldersByColumns();
			foreach (var columnPage in tabbedControlGroupWindows.TabPages.OfType<ColumnSettings>())
				columnPage.LoadData();
			_stateChanged = true;
		}

		private void OnAlignByRowsClick(object sender, EventArgs e)
		{
			if (_currentPage == null) return;
			_currentPage.AlignFoldersByRows();
			foreach (var columnPage in tabbedControlGroupWindows.TabPages.OfType<ColumnSettings>())
				columnPage.LoadData();
			_stateChanged = true;
		}

		private void ckEnableColumnTitles_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemColumnTitleSettings.Enabled = checkEditEnableColumnTitles.Checked;
			if (_allowToSave)
				_stateChanged = true;
		}

		private void buttonXColumnTitleSettings_Click(object sender, EventArgs e)
		{
			var columnPage = tabbedControlGroupWindows.SelectedTabPage as ColumnSettings;
			if (columnPage == null) return;
			var columnTitle = _currentPage.GetColumnTitles().ElementAtOrDefault(columnPage.ColumnOrder);
			using (var form = new FormColumnTitle(columnTitle))
			{
				if (form.ShowDialog(this) != DialogResult.OK) return;
				_stateChanged = true;
			}
		}
		#endregion
	}
}