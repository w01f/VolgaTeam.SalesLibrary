using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using FileManager.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormPaths : MetroForm
	{
		public FormPaths()
		{
			InitializeComponent();
		}

		private void buttonEditBackupFolder_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditBackupFolder.EditValue != null)
					dialog.SelectedPath = buttonEditBackupFolder.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditBackupFolder.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void buttonEditNetworkSyncFolder_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditNetworkSyncFolder.EditValue != null)
					dialog.SelectedPath = buttonEditNetworkSyncFolder.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditNetworkSyncFolder.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			if (DialogResult == DialogResult.OK)
			{
				e.Cancel = true;
				if (buttonEditBackupFolder.EditValue == null)
				{
					AppManager.Instance.ShowWarning("Primary Back Root is Incorrect");
					return;
				}
				if (!Directory.Exists(buttonEditBackupFolder.EditValue.ToString()))
				{
					AppManager.Instance.ShowWarning("Primary Back Root is Incorrect");
					return;
				}
				if (buttonEditNetworkSyncFolder.EditValue == null)
				{
					AppManager.Instance.ShowWarning("Network Sync Folder is Incorrect");
					return;
				}
				if (!Directory.Exists(buttonEditNetworkSyncFolder.EditValue.ToString()))
				{
					AppManager.Instance.ShowWarning("Network Sync Folder is Incorrect");
					return;
				}
				e.Cancel = false;
				SettingsManager.Instance.BackupPath = buttonEditBackupFolder.EditValue.ToString();
				SettingsManager.Instance.NetworkPath = buttonEditNetworkSyncFolder.EditValue != null ? buttonEditNetworkSyncFolder.EditValue.ToString() : string.Empty;
				SettingsManager.Instance.UseDirectAccessToFiles = checkEditDirectAccess.Checked;
				SettingsManager.Instance.DirectAccessFileAgeLimit = (int)spinEditFileAgeLimil.Value;
				SettingsManager.Instance.Save();
			}
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			buttonEditBackupFolder.EditValue = SettingsManager.Instance.BackupPath;
			buttonEditNetworkSyncFolder.EditValue = SettingsManager.Instance.NetworkPath;
			checkEditDirectAccess.Checked = SettingsManager.Instance.UseDirectAccessToFiles;
			checkEditFileAgeLimit.Checked = SettingsManager.Instance.DirectAccessFileAgeLimit > 0;
			spinEditFileAgeLimil.Value = SettingsManager.Instance.DirectAccessFileAgeLimit;
		}

		private void checkEditDirectAccess_CheckedChanged(object sender, EventArgs e)
		{
			checkEditFileAgeLimit.Enabled = checkEditDirectAccess.Checked;
			if (!checkEditDirectAccess.Checked)
				checkEditFileAgeLimit.Checked = false;
		}

		private void checkEditFileAgeLimit_CheckedChanged(object sender, EventArgs e)
		{
			spinEditFileAgeLimil.Enabled = checkEditFileAgeLimit.Checked;
			laFileAgeLimit.Enabled = checkEditFileAgeLimit.Checked;
			if (!checkEditFileAgeLimit.Checked)
				spinEditFileAgeLimil.Value = 0;
		}
	}
}