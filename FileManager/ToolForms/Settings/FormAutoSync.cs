using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormAutoSync : Form
    {
        public FormAutoSync()
        {
            InitializeComponent();
        }

        private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EnableAutoSync = buttonXEnable.Checked;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            buttonXEnable.Checked = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EnableAutoSync;
            buttonXDisable.Checked = !PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EnableAutoSync;
            gridControlSyncTimes.DataSource = new BindingList<BusinessClasses.TimePoint>(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncTimes);
        }

        private void buttonXAddSyncTime_Click(object sender, EventArgs e)
        {
            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncTimes.Add(new BusinessClasses.TimePoint());
            gridControlSyncTimes.DataSource = new BindingList<BusinessClasses.TimePoint>(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncTimes);
            if (gridViewSyncTimes.RowCount > 0)
                gridViewSyncTimes.FocusedRowHandle = gridViewSyncTimes.RowCount - 1;
        }

        private void repositoryItemTimeEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncTimes.RemoveAt(gridViewSyncTimes.GetDataSourceRowIndex(gridViewSyncTimes.FocusedRowHandle));
                gridControlSyncTimes.DataSource = new BindingList<BusinessClasses.TimePoint>(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncTimes);
            }
        }

        private void buttonXEnable_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && !button.Checked)
            {
                buttonXEnable.Checked = false;
                buttonXDisable.Checked = false;
                button.Checked = true;
            }
        }

        private void buttonXEnable_CheckedChanged(object sender, EventArgs e)
        {
            buttonXAddSyncTime.Enabled = buttonXEnable.Checked;
            gridControlSyncTimes.Enabled = buttonXEnable.Checked;
        }
    }
}
