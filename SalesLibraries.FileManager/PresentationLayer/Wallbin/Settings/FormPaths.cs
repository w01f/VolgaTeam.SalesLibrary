using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormPaths : MetroForm
	{
		private readonly List<PathWrapper> _localPathList = new List<PathWrapper>();
		private readonly List<PathWrapper> _webPathList = new List<PathWrapper>();

		public string BackupPath
		{
			get { return buttonEditBackupFolder.EditValue as String; }
			set { buttonEditBackupFolder.EditValue = value; }
		}

		public FormPaths()
		{
			InitializeComponent();
			Text = String.Format(Text, AppProfileManager.Instance.LibraryAlias);

			repositoryItemButtonEditWebSyncPath.EnableSelectAll();
			repositoryItemButtonEditLocalSyncPath.EnableSelectAll();

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
				buttonXWebSyncPathAdd.Font = new Font(buttonXWebSyncPathAdd.Font.FontFamily, buttonXWebSyncPathAdd.Font.Size - 2, buttonXWebSyncPathAdd.Font.Style);
				buttonXLocalSyncPathAdd.Font = new Font(buttonXLocalSyncPathAdd.Font.FontFamily, buttonXLocalSyncPathAdd.Font.Size - 2, buttonXLocalSyncPathAdd.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		private void LoadLocalPaths()
		{
			gridControlLocalSyncPath.DataSource = null;
			gridControlLocalSyncPath.DataSource = _localPathList;
			gridViewLocalSyncPath.RefreshData();
			gridViewLocalSyncPath.UpdateCurrentRow();
		}

		private void LoadWebPaths()
		{
			gridControlWebSyncPath.DataSource = null;
			gridControlWebSyncPath.DataSource = _webPathList;
			gridViewWebSyncPath.RefreshData();
			gridViewWebSyncPath.UpdateCurrentRow();
		}

		private void FormPaths_Load(object sender, EventArgs e)
		{
			BackupPath = MainController.Instance.Settings.BackupPath;

			_localPathList.Clear();
			_localPathList.AddRange(MainController.Instance.Settings.NetworkPaths.Select(p => new PathWrapper { Name = p }));
			LoadLocalPaths();
			checkEditLocalSyncPath.Checked = MainController.Instance.Settings.EnableLocalSync;

			_webPathList.Clear();
			_webPathList.AddRange(MainController.Instance.Settings.WebPaths.Select(p => new PathWrapper { Name = p }));
			LoadWebPaths();
			checkEditWebSyncPath.Checked = MainController.Instance.Settings.EnableWebSync;
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
			if (checkEditLocalSyncPath.Checked && (!_localPathList.Any() || _localPathList.Any(p => !Directory.Exists(p.Name))))
			{
				MainController.Instance.PopupMessages.ShowWarning("Some of Local Sync Folders are Incorrect");
				return;
			}
			if (checkEditWebSyncPath.Checked && (!_webPathList.Any() || _webPathList.Any(p => !Directory.Exists(p.Name))))
			{
				MainController.Instance.PopupMessages.ShowWarning("Some of Web Sync Folders are Incorrect");
				return;
			}
			e.Cancel = false;
		}

		private void FormPaths_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			MainController.Instance.Settings.BackupPath = BackupPath;

			MainController.Instance.Settings.NetworkPaths.Clear();
			if (checkEditLocalSyncPath.Checked)
				MainController.Instance.Settings.NetworkPaths.AddRange(_localPathList.Select(p => p.Name));

			MainController.Instance.Settings.WebPaths.Clear(); if (checkEditWebSyncPath.Checked)
				MainController.Instance.Settings.WebPaths.AddRange(_webPathList.Select(p => p.Name));
		}

		private void buttonEditFolderSelector_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var folderSelector = sender as ButtonEdit;
			if (folderSelector == null) return;
			using (var dialog = new FolderBrowserDialogEx())
			{
				dialog.ShowEditBox = true;
				dialog.ShowFullPathInEditBox = true;
				dialog.SelectedPath = folderSelector.EditValue as String;
				if (dialog.ShowDialog() != DialogResult.OK) return;
				folderSelector.EditValue = dialog.SelectedPath;
			}
		}

		private void checkEditLocalSyncPath_CheckedChanged(object sender, EventArgs e)
		{
			buttonXLocalSyncPathAdd.Enabled = checkEditLocalSyncPath.Checked;
			gridControlLocalSyncPath.Enabled = checkEditLocalSyncPath.Checked;
		}

		private void checkEditWebSyncPath_CheckedChanged(object sender, EventArgs e)
		{
			buttonXWebSyncPathAdd.Enabled = checkEditWebSyncPath.Checked;
			gridControlWebSyncPath.Enabled = checkEditWebSyncPath.Checked;
		}

		private void buttonXWebSyncPathAdd_Click(object sender, EventArgs e)
		{
			_webPathList.Add(new PathWrapper { Name = String.Empty });
			LoadWebPaths();
		}

		private void buttonXLocalSyncPathAdd_Click(object sender, EventArgs e)
		{
			_localPathList.Add(new PathWrapper { Name = String.Empty });
			LoadLocalPaths();
		}

		private void repositoryItemButtonEditLocalSyncPath_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					var selectedPath = (PathWrapper)gridViewLocalSyncPath.GetFocusedRow();
					using (var dialog = new FolderBrowserDialogEx())
					{
						dialog.ShowEditBox = true;
						dialog.ShowFullPathInEditBox = true;
						dialog.SelectedPath = selectedPath.Name;
						if (dialog.ShowDialog() != DialogResult.OK) return;
						selectedPath.Name = dialog.SelectedPath;
						LoadLocalPaths();
					}
					break;
				case 1:
					if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure want to delete path?") == DialogResult.Yes)
					{
						_localPathList.Remove((PathWrapper)gridViewLocalSyncPath.GetFocusedRow());
						LoadLocalPaths();
					}
					break;
			}
		}

		private void repositoryItemButtonEditWebSyncPath_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					var selectedPath = (PathWrapper)gridViewWebSyncPath.GetFocusedRow();
					using (var dialog = new FolderBrowserDialogEx())
					{
						dialog.ShowEditBox = true;
						dialog.ShowFullPathInEditBox = true;
						dialog.SelectedPath = selectedPath.Name;
						if (dialog.ShowDialog() != DialogResult.OK) return;
						selectedPath.Name = dialog.SelectedPath;
						LoadWebPaths();
					}
					break;
				case 1:
					if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure want to delete path?") == DialogResult.Yes)
					{
						_webPathList.Remove((PathWrapper)gridViewWebSyncPath.GetFocusedRow());
						LoadWebPaths();
					}
					break;
			}
		}

		internal class PathWrapper
		{
			public string Name { get; set; }
		}
	}
}