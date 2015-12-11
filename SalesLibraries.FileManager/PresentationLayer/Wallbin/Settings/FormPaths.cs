using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormPaths : MetroForm
	{
		public string BackupPath
		{
			get { return buttonEditBackupFolder.EditValue as String; }
			set { buttonEditBackupFolder.EditValue = value; }
		}

		public string LocalSyncPath
		{
			get
			{
				return checkEditLocalSyncPath.Checked ? 
					buttonEditLocalSyncPath.EditValue as String : 
					null;
			}
			set
			{
				buttonEditLocalSyncPath.EditValue = value;
				checkEditLocalSyncPath.Checked = !String.IsNullOrEmpty(value);
			}
		}

		public string WebSyncPath
		{
			get { return buttonEditWebSyncPath.EditValue as String; }
			set { buttonEditWebSyncPath.EditValue = value; }
		}

		public FormPaths()
		{
			InitializeComponent();
		}

		private void buttonEditFolderSelector_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var folderSelector = sender as ButtonEdit;
			if (folderSelector == null) return;
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = folderSelector.EditValue as String;
				if (dialog.ShowDialog() != DialogResult.OK) return;
				folderSelector.EditValue = dialog.SelectedPath;
			}
		}

		private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			e.Cancel = true;
			if (String.IsNullOrEmpty(BackupPath) || !Directory.Exists(BackupPath))
			{
				MainController.Instance.PopupMessages.ShowWarning("Primary Back Root is Incorrect");
				return;
			}
			if (checkEditLocalSyncPath.Checked && (String.IsNullOrEmpty(LocalSyncPath) || !Directory.Exists(LocalSyncPath)))
			{
				MainController.Instance.PopupMessages.ShowWarning("Local Sync Folder is Incorrect");
				return;
			}
			if (String.IsNullOrEmpty(WebSyncPath) || !Directory.Exists(WebSyncPath))
			{
				MainController.Instance.PopupMessages.ShowWarning("Web Sync Folder is Incorrect");
				return;
			}
			e.Cancel = false;
		}

		private void checkEditLocalSyncPath_CheckedChanged(object sender, EventArgs e)
		{
			laLocalSyncPath.Enabled = checkEditLocalSyncPath.Checked;
			laLocalSyncDesription.Enabled = checkEditLocalSyncPath.Checked;
			buttonEditLocalSyncPath.Enabled = checkEditLocalSyncPath.Checked;
			if (!checkEditLocalSyncPath.Checked)
				buttonEditLocalSyncPath.EditValue = null;
		}
	}
}