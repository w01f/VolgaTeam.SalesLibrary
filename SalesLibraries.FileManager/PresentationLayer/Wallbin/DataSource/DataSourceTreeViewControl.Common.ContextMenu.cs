using System;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	public sealed partial class DataSourceTreeViewControl
	{
		private void OnMenuItemFileOpenClick(object sender, EventArgs e)
		{
			var fileLink = SelectedTreeList.Selection.Count > 0 ?
				SelectedTreeList.Selection[0].Tag as FileLink :
				null;
			if (fileLink != null)
				ViewItem(fileLink);
		}

		private void OnMenuItemFileDeleteClick(object sender, EventArgs e)
		{
			var fileNode = SelectedTreeList.Selection[0];
			var fileLink = fileNode.Tag as FileLink;
			if (fileLink == null) return;
			using (var form = new FormCustomDialog(
					String.Format("{0}{1}",
						"<size=+4>Are you SURE you want to DELETE this file from your Site Source Directory?</size>",
						"<br><br>* This Link will also be removed from your Site…<br>*This file is saved in z_archive…"
					),
					new[]
					{
						new CustomDialogButtonInfo {Title = "DELETE",DialogResult = DialogResult.OK,Width = 100},
						new CustomDialogButtonInfo {Title = "CANCEL",DialogResult = DialogResult.Cancel,Width = 100}
					}
				))
			{
				form.Width = 500;
				form.Height = 170;
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				if (Utils.MoveFileToArchive(fileLink.Path))
					fileNode.ParentNode.Nodes.Remove(fileNode);
				else
					MainController.Instance.PopupMessages.ShowWarning("Couldn't delete file. It might be busy by another process");
			}
		}

		private void OnMenuItemFolderCreateClick(object sender, EventArgs e)
		{
			var folderNode = SelectedTreeList.Selection[0];
			var folderLink = folderNode.Tag as FolderLink;
			if (folderLink == null) return;
			using (var form = new FormCreateFolder())
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var subFolderPath = Path.Combine(folderLink.Path, form.FolderName);
				if (Directory.Exists(subFolderPath)) return;
				Directory.CreateDirectory(subFolderPath);

				var subFolderLink = new FolderLink();
				subFolderLink.RootId = folderLink.RootId;
				subFolderLink.Path = subFolderPath;
				var childNode = SelectedTreeList.AppendNode(new[] { subFolderLink.Name }, folderNode, subFolderLink);
				childNode.StateImageIndex = 0;
			}
		}
	}
}
