using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Common.Helpers;
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
			get
			{
				return checkEditWebSyncPath.Checked ?
					buttonEditWebSyncPath.EditValue as String :
					null;
			}
			set
			{
				buttonEditWebSyncPath.EditValue = value;
				checkEditWebSyncPath.Checked = !String.IsNullOrEmpty(value);
			}
		}

		public FormPaths()
		{
			InitializeComponent();
			Text = String.Format(Text, AppProfileManager.Instance.LibraryAlias);
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				laBackup.Font = new Font(laBackup.Font.FontFamily, laBackup.Font.Size - 2, laBackup.Font.Style);
				laBackupDescription.Font = new Font(laBackupDescription.Font.FontFamily, laBackupDescription.Font.Size - 2, laBackupDescription.Font.Style);
				laLocalSyncPath.Font = new Font(laLocalSyncPath.Font.FontFamily, laLocalSyncPath.Font.Size - 2, laLocalSyncPath.Font.Style);
				laLocalSyncDesription.Font = new Font(laLocalSyncDesription.Font.FontFamily, laLocalSyncDesription.Font.Size - 2, laLocalSyncDesription.Font.Style);
				laWebSyncPath.Font = new Font(laWebSyncPath.Font.FontFamily, laWebSyncPath.Font.Size - 2, laWebSyncPath.Font.Style);
				laWebSyncDescription.Font = new Font(laWebSyncDescription.Font.FontFamily, laWebSyncDescription.Font.Size - 2, laWebSyncDescription.Font.Style);

				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
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
			if (checkEditWebSyncPath.Checked && (String.IsNullOrEmpty(WebSyncPath) || !Directory.Exists(WebSyncPath)))
			{
				MainController.Instance.PopupMessages.ShowWarning("Web Sync Folder is Incorrect");
				return;
			}
			if (BackupPath == LocalSyncPath || BackupPath == WebSyncPath || (!String.IsNullOrEmpty(LocalSyncPath) && LocalSyncPath == WebSyncPath))
			{
				MainController.Instance.PopupMessages.ShowWarning(String.Format("DUDE! WTH?{0}Check your Paths…{0}Something is Really Screwed Up…", Environment.NewLine));
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

		private void checkEditWebSyncPath_CheckedChanged(object sender, EventArgs e)
		{
			laWebSyncPath.Enabled = checkEditWebSyncPath.Checked;
			laWebSyncDescription.Enabled = checkEditWebSyncPath.Checked;
			buttonEditWebSyncPath.Enabled = checkEditWebSyncPath.Checked;
			if (!checkEditWebSyncPath.Checked)
				buttonEditWebSyncPath.EditValue = null;
		}
	}
}