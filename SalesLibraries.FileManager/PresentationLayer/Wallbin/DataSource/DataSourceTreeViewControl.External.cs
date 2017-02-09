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
				foreach (var rootNode in DriveInfo.GetDrives().Select(driveInfo =>
					treeListExternalFiles.AppendNode(new[] { driveInfo.Name }, null, new FolderLink { RootId = defaultDataSource.DataSourceId, Path = driveInfo.Name })))
				{
					rootNode.StateImageIndex = 0;
					FillNode(rootNode, expandAll);
				}
			})));

			treeListExternalFiles.ResumeLayout();
			treeListExternalFiles.Enabled = true;
		}
	}
}
