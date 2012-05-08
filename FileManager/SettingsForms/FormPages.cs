using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.SettingsForms
{
    public partial class FormPages : Form
    {
        public BusinessClasses.Library Library { get; set; }

        public FormPages()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadPages();
        }

        private void LoadPages()
        {
            gridControlPages.DataSource = new BindingList<BusinessClasses.LibraryPage>(this.Library.Pages);
        }

        private void FormPages_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                gridViewPages.CloseEditor();
                if(this.Library.Pages.Where(x=>string.IsNullOrEmpty(x.Name.Trim())).Count()>0)
                {
                    AppManager.Instance.ShowWarning("You need to name all Pages before closing.");
                    e.Cancel = true;
                }
            }
        }

        private void FormPages_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (this.Library.Pages.Count > 0)
                {
                    this.Library.IsConfigured = true;
                    this.Library.Save();
                }
                else
                    AppManager.Instance.ShowWarning("You need to have at least 1 Page in Library.");
            }
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    this.Library.UpPage(gridViewPages.GetDataSourceRowIndex(gridViewPages.FocusedRowHandle));
                    if (gridViewPages.FocusedRowHandle > 0)
                        gridViewPages.FocusedRowHandle--;
                    break;
                case 1:
                    this.Library.DownPage(gridViewPages.GetDataSourceRowIndex(gridViewPages.FocusedRowHandle));
                    if (gridViewPages.FocusedRowHandle < gridViewPages.RowCount - 1)
                        gridViewPages.FocusedRowHandle++;
                    break;
            }
        }

        private void buttonXAdd_Click(object sender, EventArgs e)
        {
            this.Library.AddPage();
            ((BindingList<BusinessClasses.LibraryPage>)gridControlPages.DataSource).ResetBindings();
            gridViewPages.FocusedRowHandle = gridViewPages.RowCount - 1;
        }

        private void buttonXRemove_Click(object sender, EventArgs e)
        {
            if (gridViewPages.FocusedRowHandle >= 0)
            {
                if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete selected page?") == DialogResult.Yes)
                {
                    gridViewPages.DeleteSelectedRows();
                    this.Library.RebuildPagesIndexes();
                }
            }
        }
    }
}
