using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormExtraRoots : Form
    {
        public BusinessClasses.Library Library { get; set; }

        public FormExtraRoots()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadExtraRoots();
        }

        private void LoadExtraRoots()
        {
            gridControlFolders.DataSource = new BindingList<BusinessClasses.RootFolder>(this.Library.ExtraFolders);
        }

        private void FormPages_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                gridViewFolders.CloseEditor();
        }

        private void FormPages_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                this.Library.Save();
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    this.Library.UpExtraRoot(gridViewFolders.GetDataSourceRowIndex(gridViewFolders.FocusedRowHandle));
                    if (gridViewFolders.FocusedRowHandle > 0)
                        gridViewFolders.FocusedRowHandle--;
                    break;
                case 1:
                    this.Library.DownExtraRoot(gridViewFolders.GetDataSourceRowIndex(gridViewFolders.FocusedRowHandle));
                    if (gridViewFolders.FocusedRowHandle < gridViewFolders.RowCount - 1)
                        gridViewFolders.FocusedRowHandle++;
                    break;
            }
        }

        private void buttonXAdd_Click(object sender, EventArgs e)
        {
            this.Library.AddExtraRoot();
            ((BindingList<BusinessClasses.RootFolder>)gridControlFolders.DataSource).ResetBindings();
            gridViewFolders.FocusedRowHandle = gridViewFolders.RowCount - 1;
        }

        private void repositoryItemButtonEditPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                    {
                        string selectedPath = this.Library.ExtraFolders[gridViewFolders.GetDataSourceRowIndex(gridViewFolders.FocusedRowHandle)].Path;
                        if (!string.IsNullOrEmpty(selectedPath))
                            dialog.SelectedPath = selectedPath;
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (System.IO.Directory.Exists(dialog.SelectedPath))
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
                            this.Library.RebuildExtraFoldersOrder();
                        }
                    }
                    break;
            }
        }
    }
}
