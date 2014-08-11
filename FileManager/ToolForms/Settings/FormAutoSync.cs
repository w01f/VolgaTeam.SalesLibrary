using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormAutoSync : MetroForm
	{
		private FormAutoSyncEdit _formEdit;

		public FormAutoSync()
		{
			InitializeComponent();
		}

		private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				MainController.Instance.ActiveDecorator.Library.EnableAutoSync = buttonXEnable.Checked;
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			buttonXEnable.Checked = MainController.Instance.ActiveDecorator.Library.EnableAutoSync;
			buttonXDisable.Checked = !MainController.Instance.ActiveDecorator.Library.EnableAutoSync;
			gridControlSyncSchedule.DataSource = new BindingList<SyncScheduleRecord>(MainController.Instance.ActiveDecorator.Library.SyncScheduleRecords);
		}

		private void buttonXAddSyncTime_Click(object sender, EventArgs e)
		{
			var syncScheduleRecord = new SyncScheduleRecord();
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
			if (_formEdit.ShowDialog() == DialogResult.OK)
			{
				MainController.Instance.ActiveDecorator.Library.SyncScheduleRecords.Add(syncScheduleRecord);
				gridControlSyncSchedule.DataSource = new BindingList<SyncScheduleRecord>(MainController.Instance.ActiveDecorator.Library.SyncScheduleRecords);
				if (gridViewSyncSchedule.RowCount > 0)
					gridViewSyncSchedule.FocusedRowHandle = gridViewSyncSchedule.RowCount - 1;
			}
		}

		private void buttonXEnable_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
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

		private void gridViewSyncSchedule_RowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
			{
				SyncScheduleRecord syncScheduleRecord = MainController.Instance.ActiveDecorator.Library.SyncScheduleRecords[gridViewSyncSchedule.GetDataSourceRowIndex(e.RowHandle)];
				_formEdit = new FormAutoSyncEdit(syncScheduleRecord);
				if (_formEdit.ShowDialog() == DialogResult.OK)
					gridControlSyncSchedule.DataSource = new BindingList<SyncScheduleRecord>(MainController.Instance.ActiveDecorator.Library.SyncScheduleRecords);
			}
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			MainController.Instance.ActiveDecorator.Library.SyncScheduleRecords.RemoveAt(gridViewSyncSchedule.GetDataSourceRowIndex(gridViewSyncSchedule.FocusedRowHandle));
			gridControlSyncSchedule.DataSource = new BindingList<SyncScheduleRecord>(MainController.Instance.ActiveDecorator.Library.SyncScheduleRecords);
		}
	}
}