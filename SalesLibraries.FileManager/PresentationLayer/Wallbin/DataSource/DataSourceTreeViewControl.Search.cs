using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Configuration;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	public sealed partial class DataSourceTreeViewControl
	{
		private IEnumerable<FileLink> SearchFileInFolder(FolderLink folderLink, string keyWord)
		{
			var fileList = new List<FileLink>();
			try
			{
				foreach (var subFolderPath in Directory.GetDirectories(folderLink.Path))
					if (!GlobalSettings.HiddenObjects.Any(item => subFolderPath.ToLower().Contains(item.ToLower())))
					{
						var subFolderLink = new FolderLink
						{
							RootId = folderLink.RootId,
							Path = subFolderPath
						};
						fileList.AddRange(SearchFileInFolder(subFolderLink, keyWord));
					}
			}
			catch { }
			try
			{
				foreach (var filePath in Directory.GetFiles(folderLink.Path, String.Format("*{0}*", keyWord)))
				{
					var lastWriteTime = File.GetLastWriteTime(filePath);
					if ((checkEditDateRange.Checked && (lastWriteTime < dateEditStartDate.DateTime || lastWriteTime > dateEditEndDate.DateTime)) ||
						GlobalSettings.HiddenObjects.Any(x => filePath.ToLower().Contains(x.ToLower())))
						continue;
					var fileLink = new FileLink
					{
						RootId = folderLink.RootId,
						Path = filePath
					};
					fileList.Add(fileLink);
				}
			}
			catch { }
			return fileList;
		}

		private async void OnSearchClick(object sender, EventArgs e)
		{
			treeListSearchFiles.SuspendLayout();
			treeListSearchFiles.Nodes.Clear();
			labelControlProgress.Text = "Searching Files...";
			layoutControlGroupProgress.Visibility = LayoutVisibility.Always;
			circularProgressTreeView.IsRunning = true;
			layoutControlGroupFiles.Enabled = false;

			var files = new List<FileLink>();
			await Task.Run(() =>
			{
				files.AddRange(_dataSources.SelectMany(ds => SearchFileInFolder(new FolderLink() { RootId = ds.DataSourceId, Path = ds.Path }, textEditKeyWord.EditValue as String)));
				if (files.Count <= 0) return;
				files.Sort((x, y) => x.Name.CompareTo(y.Name));
				MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					foreach (var file in files)
					{
						var childNode = treeListSearchFiles.AppendNode(new[] { String.Format("{0} ({1})", file.Name, File.GetLastWriteTime(file.Path).ToString("MM/dd/yy hh:mm tt")) }, null, file);
						childNode.StateImageIndex = GetImageindex(file.Path);
						Application.DoEvents();
					}
				}));
			});

			layoutControlGroupProgress.Visibility = LayoutVisibility.Never;
			circularProgressTreeView.IsRunning = false;
			treeListSearchFiles.ResumeLayout();
			layoutControlGroupFiles.Enabled = true;
			if (files.Count == 0)
				MainController.Instance.PopupMessages.ShowInfo("Files was not found");
		}

		private void OnKeywordEditKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OnSearchClick(null, null);
		}

		private void OnDateRangeCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupDateRangeWrapper.Visibility = checkEditDateRange.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}
	}
}
