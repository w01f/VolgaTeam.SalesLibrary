using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FileManager.Controllers;

namespace FileManager.ToolForms.Settings
{
	public partial class FormEmailGrabber : Form
	{
		public FormEmailGrabber()
		{
			InitializeComponent();
			spinEditGrabInterval.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditGrabInterval.MouseDown += FormMain.Instance.EditorMouseDown;
			spinEditGrabInterval.Enter += FormMain.Instance.EditorEnter;
			textEditInboxSubFolder.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditInboxSubFolder.MouseDown += FormMain.Instance.EditorMouseDown;
			textEditInboxSubFolder.Enter += FormMain.Instance.EditorEnter;

			if ((base.CreateGraphics()).DpiX > 96)
			{
				laGrabInterval.Font = new Font(laGrabInterval.Font.FontFamily, laGrabInterval.Font.Size - 2, laGrabInterval.Font.Style);
				laInboxSubFolder.Font = new Font(laInboxSubFolder.Font.FontFamily, laInboxSubFolder.Font.Size - 2, laInboxSubFolder.Font.Style);
			}
		}

		private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableEmailGrabber = buttonXEnable.Checked;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EmailGrabInterval = (int)spinEditGrabInterval.Value;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.InboxSubFolder = textEditInboxSubFolder.EditValue != null ? textEditInboxSubFolder.EditValue.ToString() : string.Empty;
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			buttonXEnable.Checked = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableEmailGrabber;
			buttonXDisable.Checked = !MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableEmailGrabber;
			spinEditGrabInterval.Value = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EmailGrabInterval;
			textEditInboxSubFolder.EditValue = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.InboxSubFolder;
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
			laGrabInterval.Enabled = buttonXEnable.Checked;
			spinEditGrabInterval.Enabled = buttonXEnable.Checked;

			laInboxSubFolder.Enabled = buttonXEnable.Checked;
			textEditInboxSubFolder.Enabled = buttonXEnable.Checked;
		}
	}
}