using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	public sealed partial class DataSourceTreeViewControl
	{
		private async Task RefreshExternalFiles(bool expandAll = false)
		{
			treeListExternalFiles.Enabled = false;
			treeListExternalFiles.SuspendLayout();
			treeListExternalFiles.Nodes.Clear();

			var defaultDataSource = _dataSources.First();
			await Task.Run(() => Invoke(new MethodInvoker(() =>
			{
				var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (Directory.Exists(desktopPath))
				{
					var specialPathNode = treeListExternalFiles.AppendNode(
						new[] { "Desktop" },
						null,
						new FolderLink { RootId = defaultDataSource.DataSourceId, Path = desktopPath });
					specialPathNode.StateImageIndex = 0;
				}

				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				if (Directory.Exists(desktopPath))
				{
					var specialPathNode = treeListExternalFiles.AppendNode(
						new[] { "Documents" },
						null,
						new FolderLink { RootId = defaultDataSource.DataSourceId, Path = documentsPath });
					specialPathNode.StateImageIndex = 0;
				}

				foreach (var rootNode in DriveInfo.GetDrives().Select(driveInfo =>
					treeListExternalFiles.AppendNode(new[] { driveInfo.Name }, null, new FolderLink { RootId = defaultDataSource.DataSourceId, Path = driveInfo.Name })))
				{
					rootNode.StateImageIndex = 0;
				}
			})));

			treeListExternalFiles.ResumeLayout();
			treeListExternalFiles.Enabled = true;
		}
	}
}
