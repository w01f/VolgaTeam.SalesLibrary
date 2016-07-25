using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
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
			if ((CreateGraphics()).DpiX > 96)
			{
				laPages.Font = new Font(laPages.Font.FontFamily, laPages.Font.Size - 2, laPages.Font.Style);
				ckEnableColumnTitles.Font = new Font(ckEnableColumnTitles.Font.FontFamily, ckEnableColumnTitles.Font.Size - 2, ckEnableColumnTitles.Font.Style);
				buttonXColumnTitleSettings.Font = new Font(buttonXColumnTitleSettings.Font.FontFamily, buttonXColumnTitleSettings.Font.Size - 2, buttonXColumnTitleSettings.Font.Style);
				buttonXSave.Font = new Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 2, buttonXSave.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
			}
		}

		#region Base Methods
		private void GetColumnSettings()
		{
			_allowToSave = false;
			ckEnableColumnTitles.Checked = _currentPage.Settings.EnableColumnTitles;
			xtraTabControlWindows.TabPages.Clear();
			for (int i = 0; i < LibraryPage.ColumnsCount; i++)
			{
				var columnPage = new ColumnSettings(_currentPage, i);
				columnPage.FolderChanged += (o, e) => { _stateChanged = true; };
				columnPage.FolderMovedLeft += OnMoveFolderLeft;
				columnPage.FolderMovedRight += OnMoveFolderRight;
				xtraTabControlWindows.TabPages.Add(columnPage);
			}
			foreach (var targetPage in xtraTabControlWindows.TabPages.OfType<ColumnSettings>())
				foreach (var columnPage in xtraTabControlWindows.TabPages.OfType<ColumnSettings>().Where(p => p != targetPage))
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
			foreach (var columnPage in xtraTabControlWindows.TabPages.OfType<ColumnSettings>())
				columnPage.SaveData();
			_currentPage.Settings.EnableColumnTitles = ckEnableColumnTitles.Checked;
		}

		private void PopulatePagesList()
		{
			comboBoxEditPages.Properties.Items.Clear();
			comboBoxEditPages.Properties.Items.AddRange(Library.Pages.ToList());
			comboBoxEditPages.EditValue = Library.Pages.FirstOrDefault();
			pnPages.Visible = Library.Pages.Count > 1;
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
			var columnPage = xtraTabControlWindows.SelectedTabPage as ColumnSettings;
			if (columnPage == null) return;
			columnPage.AddFolder();
			_stateChanged = true;
		}

		private void OnMoveFolderRight(object sender, EventArgs e)
		{
			var columnPage = sender as ColumnSettings;
			if (columnPage == null) return;
			if (columnPage.ColumnOrder == 2) return;
			var nextPage = xtraTabControlWindows.TabPages[columnPage.ColumnOrder + 1] as ColumnSettings;
			columnPage.CopyFolder();
			nextPage.PasteFolder();
			_stateChanged = true;
		}

		private void OnMoveFolderLeft(object sender, EventArgs e)
		{
			var columnPage = sender as ColumnSettings;
			if (columnPage == null) return;
			if (columnPage.ColumnOrder == 0) return;
			var prevPage = xtraTabControlWindows.TabPages[columnPage.ColumnOrder - 1] as ColumnSettings;
			columnPage.CopyFolder();
			prevPage.PasteFolder();
			_stateChanged = true;
		}

		private void OnAlignByColumnsClick(object sender, EventArgs e)
		{
			if (_currentPage == null) return;
			_currentPage.AlignFoldersByColumns();
			foreach (var columnPage in xtraTabControlWindows.TabPages.OfType<ColumnSettings>())
				columnPage.LoadData();
			_stateChanged = true;
		}

		private void OnAlignByRowsClick(object sender, EventArgs e)
		{
			if (_currentPage == null) return;
			_currentPage.AlignFoldersByRows();
			foreach (var columnPage in xtraTabControlWindows.TabPages.OfType<ColumnSettings>())
				columnPage.LoadData();
			_stateChanged = true;
		}

		private void ckEnableColumnTitles_CheckedChanged(object sender, EventArgs e)
		{
			buttonXColumnTitleSettings.Enabled = ckEnableColumnTitles.Checked;
			if (_allowToSave)
				_stateChanged = true;
		}

		private void buttonXColumnTitleSettings_Click(object sender, EventArgs e)
		{
			var columnPage = xtraTabControlWindows.SelectedTabPage as ColumnSettings;
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