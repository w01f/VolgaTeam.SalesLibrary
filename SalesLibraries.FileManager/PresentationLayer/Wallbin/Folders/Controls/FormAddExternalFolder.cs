using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class FormAddExternalFolder : MetroForm
	{
		private bool _loading;
		private readonly FolderLink _folderLink;

		public FormAddExternalFolder(FolderLink folderLink)
		{
			_folderLink = folderLink;

			InitializeComponent();

			layoutControlItemSelectAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemClearAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemClearAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void LoadData()
		{
			_loading = true;

			Text = _folderLink.Name;

			var files = _folderLink.GetAllFiles().ToList();
			files.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));

			checkedListBoxControl.Items.AddRange(files.Select(file => new CheckedListBoxItem(
					file,
					String.Format("{0} <color=gray>({1})</color>", file.Name, file.Length.ToFileSize()),
					CheckState.Checked
					))
				.ToArray());

			UpdateFolderInfo();

			_loading = false;
		}

		private void SaveData()
		{
			var selectedFiles = checkedListBoxControl.Items
				.OfType<CheckedListBoxItem>()
				.Where(item => item.CheckState == CheckState.Checked)
				.Select(item => item.Value)
				.OfType<FileInfo>()
				.ToList();

			_folderLink.SelectedFiles.Clear();
			_folderLink.SelectedFiles.AddRange(selectedFiles);
		}

		private void UpdateFolderInfo()
		{
			var selectedFiles = checkedListBoxControl.Items
				.OfType<CheckedListBoxItem>()
				.Where(item => item.CheckState == CheckState.Checked)
				.Select(item => item.Value)
				.OfType<FileInfo>()
				.ToList();

			var totalFiles = selectedFiles.Count;
			var totalFileSize = selectedFiles.Sum(file => file.Length).ToFileSize();

			labelControlTitle.Text = String.Format("<size=+3>Folder Size: {0}          Files: {1}</size>",
				totalFileSize, totalFiles);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			LoadData();
		}

		private void OnFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveData();
			if (_folderLink.SelectedFiles.Any()) return;
			MainController.Instance.PopupMessages.ShowWarning("You need to select at least one file");
			e.Cancel = true;
		}

		private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (_loading) return;
			UpdateFolderInfo();
		}

		private void buttonXSelectAll_Click(object sender, EventArgs e)
		{
			checkedListBoxControl.CheckAll();
		}

		private void buttonXClearAll_Click(object sender, EventArgs e)
		{
			checkedListBoxControl.UnCheckAll();
		}
	}
}
