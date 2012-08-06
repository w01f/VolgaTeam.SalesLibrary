using System;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormPaths : Form
    {
        public FormPaths()
        {
            InitializeComponent();
        }

        private void buttonEditBackupFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (buttonEditBackupFolder.EditValue != null)
                    dialog.SelectedPath = buttonEditBackupFolder.EditValue.ToString();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        buttonEditBackupFolder.EditValue = dialog.SelectedPath;
                }
            }
        }

        private void buttonEditNetworkSyncFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (buttonEditNetworkSyncFolder.EditValue != null)
                    dialog.SelectedPath = buttonEditNetworkSyncFolder.EditValue.ToString();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        buttonEditNetworkSyncFolder.EditValue = dialog.SelectedPath;
                }
            }
        }

        private void buttonEditFtpSyncFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (buttonEditFtpSyncFolder.EditValue != null)
                    dialog.SelectedPath = buttonEditFtpSyncFolder.EditValue.ToString();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        buttonEditFtpSyncFolder.EditValue = dialog.SelectedPath;
                }
            }
        }

        private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
                if (buttonEditBackupFolder.EditValue == null)
                {
                    AppManager.Instance.ShowWarning("Primary Back Root is Incorrect");
                    return;
                }
                else if (!System.IO.Directory.Exists(buttonEditBackupFolder.EditValue.ToString()))
                {
                    AppManager.Instance.ShowWarning("Primary Back Root is Incorrect");
                    return;
                }
                if (buttonEditNetworkSyncFolder.EditValue == null)
                {
                    AppManager.Instance.ShowWarning("Network Sync Folder is Incorrect");
                    return;
                }
                else if (!System.IO.Directory.Exists(buttonEditNetworkSyncFolder.EditValue.ToString()))
                {
                    AppManager.Instance.ShowWarning("Network Sync Folder is Incorrect");
                    return;
                }
                if (buttonEditFtpSyncFolder.EditValue == null)
                {
                    AppManager.Instance.ShowWarning("FTP Sync Folder is Incorrect");
                    return;
                }
                else if (!System.IO.Directory.Exists(buttonEditFtpSyncFolder.EditValue.ToString()))
                {
                    AppManager.Instance.ShowWarning("FTP Sync Folder is Incorrect");
                    return;
                }
                e.Cancel = false;
                ConfigurationClasses.SettingsManager.Instance.BackupPath = buttonEditBackupFolder.EditValue.ToString();
                ConfigurationClasses.SettingsManager.Instance.NetworkPath = buttonEditNetworkSyncFolder.EditValue != null ? buttonEditNetworkSyncFolder.EditValue.ToString() : string.Empty;
                ConfigurationClasses.SettingsManager.Instance.FtpPath = buttonEditFtpSyncFolder.EditValue != null ? buttonEditFtpSyncFolder.EditValue.ToString() : string.Empty;
                ConfigurationClasses.SettingsManager.Instance.UseDirectAccessToFiles = checkEditDirectAccess.Checked;
                ConfigurationClasses.SettingsManager.Instance.DirectAccessFileAgeLimit = (int)spinEditFileAgeLimil.Value;
                ConfigurationClasses.SettingsManager.Instance.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            buttonEditBackupFolder.EditValue = ConfigurationClasses.SettingsManager.Instance.BackupPath;
            buttonEditNetworkSyncFolder.EditValue = ConfigurationClasses.SettingsManager.Instance.NetworkPath;
            buttonEditFtpSyncFolder.EditValue = ConfigurationClasses.SettingsManager.Instance.FtpPath;
            checkEditDirectAccess.Checked = ConfigurationClasses.SettingsManager.Instance.UseDirectAccessToFiles;
            checkEditFileAgeLimit.Checked = ConfigurationClasses.SettingsManager.Instance.DirectAccessFileAgeLimit > 0;
            spinEditFileAgeLimil.Value = ConfigurationClasses.SettingsManager.Instance.DirectAccessFileAgeLimit;
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
