using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormAutoSync : Form
    {
        private FormAutoSyncEdit _formEdit;

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
            gridControlSyncSchedule.DataSource = new BindingList<BusinessClasses.SyncScheduleRecord>(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncScheduleRecords);
        }

        private void buttonXAddSyncTime_Click(object sender, EventArgs e)
        {
            BusinessClasses.SyncScheduleRecord syncScheduleRecord = new BusinessClasses.SyncScheduleRecord();
            syncScheduleRecord.Time = DateTime.Now;
            switch (syncScheduleRecord.Time.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    syncScheduleRecord.Monday = true;
                    break;
                case DayOfWeek.Tuesday:
                    syncScheduleRecord.Tuesday = true;
                    break;
                case DayOfWeek.Wednesday:
                    syncScheduleRecord.Wednesday = true;
                    break;
                case DayOfWeek.Thursday:
                    syncScheduleRecord.Thursday = true;
                    break;
                case DayOfWeek.Friday:
                    syncScheduleRecord.Friday = true;
                    break;
                case DayOfWeek.Saturday:
                    syncScheduleRecord.Saturday = true;
                    break;
                case DayOfWeek.Sunday:
                    syncScheduleRecord.Sunday = true;
                    break;
            }
            _formEdit = new FormAutoSyncEdit(syncScheduleRecord);
            if (_formEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncScheduleRecords.Add(syncScheduleRecord);
                gridControlSyncSchedule.DataSource = new BindingList<BusinessClasses.SyncScheduleRecord>(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncScheduleRecords);
                if (gridViewSyncSchedule.RowCount > 0)
                    gridViewSyncSchedule.FocusedRowHandle = gridViewSyncSchedule.RowCount - 1;
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
            gridControlSyncSchedule.Enabled = buttonXEnable.Checked;
            laHint.Enabled = buttonXEnable.Checked;
        }

        private void gridViewSyncSchedule_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                BusinessClasses.SyncScheduleRecord syncScheduleRecord = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncScheduleRecords[gridViewSyncSchedule.GetDataSourceRowIndex(e.RowHandle)];
                _formEdit = new FormAutoSyncEdit(syncScheduleRecord);
                if (_formEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    gridControlSyncSchedule.DataSource = new BindingList<BusinessClasses.SyncScheduleRecord>(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncScheduleRecords);
            }
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncScheduleRecords.RemoveAt(gridViewSyncSchedule.GetDataSourceRowIndex(gridViewSyncSchedule.FocusedRowHandle));
            gridControlSyncSchedule.DataSource = new BindingList<BusinessClasses.SyncScheduleRecord>(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SyncScheduleRecords);
        }
    }
}
