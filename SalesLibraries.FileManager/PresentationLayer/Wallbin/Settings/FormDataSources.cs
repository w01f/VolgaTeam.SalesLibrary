using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormDataSources : MetroForm
	{
		public FormDataSources()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96) { }
		}

		public Library Library { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			LoadDataSources();
		}

		private void LoadDataSources()
		{
			gridControlFolders.DataSource = Library.DataSources;
			gridViewFolders.RefreshData();
			gridViewFolders.Focus();
		}

		private void FormPages_FormClosed(object sender, FormClosedEventArgs e)
		{
			gridViewFolders.CloseEditor();
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var dataSource = gridViewFolders.GetFocusedRow() as AdditionalDataSource;
			if (dataSource == null) return;
			var focusedRowHandle = gridViewFolders.FocusedRowHandle;
			switch (e.Button.Index)
			{
				case 0:
					focusedRowHandle = gridViewFolders.FocusedRowHandle > 0 ? gridViewFolders.FocusedRowHandle - 1 : 0;
					Library.DataSources.UpItem(dataSource);
					break;
				case 1:
					focusedRowHandle = gridViewFolders.FocusedRowHandle < gridViewFolders.RowCount - 1 ? gridViewFolders.FocusedRowHandle + 1 : gridViewFolders.RowCount - 1;
					Library.DataSources.DownItem(dataSource);
					break;
			}
			LoadDataSources();
			gridViewFolders.FocusedRowHandle = focusedRowHandle;
		}

		private void buttonXAdd_Click(object sender, EventArgs e)
		{
			Library.AddDataSource();
			LoadDataSources();
			gridViewFolders.FocusedRowHandle = gridViewFolders.RowCount - 1;
		}

		private void repositoryItemButtonEditPath_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var dataSource = gridViewFolders.GetFocusedRow() as AdditionalDataSource;
			if (dataSource == null) return;
			switch (e.Button.Index)
			{
				case 0:
					using (var dialog = new FolderBrowserDialog())
					{

						if (!String.IsNullOrEmpty(dataSource.Path))
							dialog.SelectedPath = dataSource.Path;
						if (dialog.ShowDialog() != DialogResult.OK) return;
						if (Directory.Exists(dialog.SelectedPath))
							dataSource.Path = dialog.SelectedPath;
						gridViewFolders.UpdateCurrentRow();
					}
					break;
				case 1:
					if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to delete selected folder?") != DialogResult.Yes) return;
					var focusedRowHandle = gridViewFolders.FocusedRowHandle;
					Library.DataSources.RemoveItem(dataSource);
					LoadDataSources();
					if (gridViewFolders.RowCount > 0 && gridViewFolders.RowCount > focusedRowHandle)
						gridViewFolders.FocusedRowHandle = focusedRowHandle;
					break;
			}
		}
	}
}