using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormExtraRoots : MetroForm
	{
		public FormExtraRoots()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96) { }
		}

		public Library Library { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			LoadExtraRoots();
		}

		private void LoadExtraRoots()
		{
			gridControlFolders.DataSource = new BindingList<RootFolder>(Library.ExtraFolders);
		}

		private void FormPages_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			if (DialogResult == DialogResult.OK)
				gridViewFolders.CloseEditor();
		}

		private void FormPages_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				Library.Save();
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					Library.UpExtraRoot(gridViewFolders.GetDataSourceRowIndex(gridViewFolders.FocusedRowHandle));
					if (gridViewFolders.FocusedRowHandle > 0)
						gridViewFolders.FocusedRowHandle--;
					break;
				case 1:
					Library.DownExtraRoot(gridViewFolders.GetDataSourceRowIndex(gridViewFolders.FocusedRowHandle));
					if (gridViewFolders.FocusedRowHandle < gridViewFolders.RowCount - 1)
						gridViewFolders.FocusedRowHandle++;
					break;
			}
		}

		private void buttonXAdd_Click(object sender, EventArgs e)
		{
			Library.AddExtraRoot();
			((BindingList<RootFolder>)gridControlFolders.DataSource).ResetBindings();
			gridViewFolders.FocusedRowHandle = gridViewFolders.RowCount - 1;
		}

		private void repositoryItemButtonEditPath_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					using (var dialog = new FolderBrowserDialog())
					{
						string selectedPath = Library.ExtraFolders[gridViewFolders.GetDataSourceRowIndex(gridViewFolders.FocusedRowHandle)].Path;
						if (!string.IsNullOrEmpty(selectedPath))
							dialog.SelectedPath = selectedPath;
						if (dialog.ShowDialog() == DialogResult.OK)
						{
							if (Directory.Exists(dialog.SelectedPath))
								gridViewFolders.SetRowCellValue(gridViewFolders.FocusedRowHandle, gridColumnPath, dialog.SelectedPath);
						}
					}
					break;
				case 1:
					if (gridViewFolders.FocusedRowHandle >= 0)
					{
						if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete selected folder?") == DialogResult.Yes)
						{
							gridViewFolders.DeleteSelectedRows();
							Library.RebuildExtraFoldersOrder();
						}
					}
					break;
			}
		}
	}
}