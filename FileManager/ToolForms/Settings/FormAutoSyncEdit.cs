using System;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormAutoSyncEdit : Form
	{
		private readonly SyncScheduleRecord _syncScheduleRecord;

		public FormAutoSyncEdit(SyncScheduleRecord syncScheduleRecord)
		{
			InitializeComponent();

			_syncScheduleRecord = syncScheduleRecord;
			if (_syncScheduleRecord == null)
			{
				_syncScheduleRecord = new SyncScheduleRecord();
				_syncScheduleRecord.Time = DateTime.Now;
			}

			timeEditTime.Time = _syncScheduleRecord.Time;
			checkBoxMonday.Checked = _syncScheduleRecord.Monday;
			checkBoxTuesday.Checked = _syncScheduleRecord.Tuesday;
			checkBoxWednesday.Checked = _syncScheduleRecord.Wednesday;
			checkBoxThursday.Checked = _syncScheduleRecord.Thursday;
			checkBoxFriday.Checked = _syncScheduleRecord.Friday;
			checkBoxSaturday.Checked = _syncScheduleRecord.Saturday;
			checkBoxSunday.Checked = _syncScheduleRecord.Sunday;

			timeEditTime.MouseUp += FormMain.Instance.EditorMouseUp;
			timeEditTime.MouseDown += FormMain.Instance.EditorMouseDown;
			timeEditTime.Enter += FormMain.Instance.EditorEnter;
		}

		private void FormAutoSyncEdit_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				_syncScheduleRecord.Time = timeEditTime.Time;
				_syncScheduleRecord.Monday = checkBoxMonday.Checked;
				_syncScheduleRecord.Tuesday = checkBoxTuesday.Checked;
				_syncScheduleRecord.Wednesday = checkBoxWednesday.Checked;
				_syncScheduleRecord.Thursday = checkBoxThursday.Checked;
				_syncScheduleRecord.Friday = checkBoxFriday.Checked;
				_syncScheduleRecord.Saturday = checkBoxSaturday.Checked;
				_syncScheduleRecord.Sunday = checkBoxSunday.Checked;
			}
		}
	}
}