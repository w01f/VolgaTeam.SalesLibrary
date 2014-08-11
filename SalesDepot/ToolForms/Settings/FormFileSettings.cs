using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.ToolForms.Settings
{
	public partial class FormFileSettings : MetroForm
	{
		public FormFileSettings()
		{
			InitializeComponent();
		}

		private void buttonXResetToDefault_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.OpenFilePath = null;
			SettingsManager.Instance.SaveFilePath = null;
			SettingsManager.Instance.SaveSettings();
			buttonEditOpenFile.EditValue = SettingsManager.Instance.OpenFilePath;
			buttonEditSaveFile.EditValue = SettingsManager.Instance.SaveFilePath;
		}

		private void FormFileSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				SettingsManager.Instance.OpenFilePath = buttonEditOpenFile.EditValue != null ? buttonEditOpenFile.EditValue.ToString() : null;
				SettingsManager.Instance.SaveFilePath = buttonEditSaveFile.EditValue != null ? buttonEditSaveFile.EditValue.ToString() : null;
				SettingsManager.Instance.SaveSettings();
			}
		}

		private void FormFileSettings_Load(object sender, EventArgs e)
		{
			buttonEditOpenFile.EditValue = SettingsManager.Instance.OpenFilePath;
			buttonEditSaveFile.EditValue = SettingsManager.Instance.SaveFilePath;
		}

		private void buttonEditOpenFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = SettingsManager.Instance.OpenFilePath;
				if (dialog.ShowDialog() == DialogResult.OK)
					buttonEditOpenFile.EditValue = dialog.SelectedPath;
			}
		}

		private void buttonEditSaveFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = SettingsManager.Instance.SaveFilePath;
				if (dialog.ShowDialog() == DialogResult.OK)
					buttonEditSaveFile.EditValue = dialog.SelectedPath;
			}
		}
	}
}
