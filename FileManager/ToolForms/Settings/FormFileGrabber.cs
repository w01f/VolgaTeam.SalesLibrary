using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using FileManager.Controllers;

namespace FileManager.ToolForms.Settings
{
	public partial class FormFileGrabber : Form
	{
		public FormFileGrabber()
		{
			InitializeComponent();
			spinEditGrabInterval.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditGrabInterval.MouseDown += FormMain.Instance.EditorMouseDown;
			spinEditGrabInterval.Enter += FormMain.Instance.EditorEnter;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laSourceFolder.Font = new Font(laSourceFolder.Font.FontFamily, laSourceFolder.Font.Size - 2, laSourceFolder.Font.Style);
			}
		}

		private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber = buttonXEnable.Checked;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabInterval = (int)spinEditGrabInterval.Value;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabSourceFolder = buttonEditSouceFolder.EditValue != null ? buttonEditSouceFolder.EditValue.ToString() : string.Empty;
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			buttonXEnable.Checked = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber;
			buttonXDisable.Checked = !MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber;
			spinEditGrabInterval.Value = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabInterval;
			buttonEditSouceFolder.EditValue = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabSourceFolder;
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

			laSourceFolder.Enabled = buttonXEnable.Checked;
			buttonEditSouceFolder.Enabled = buttonXEnable.Checked;
		}

		private void buttonEditSouceFolder_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditSouceFolder.EditValue != null)
					dialog.SelectedPath = buttonEditSouceFolder.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditSouceFolder.EditValue = dialog.SelectedPath;
				}
			}
		}
	}
}