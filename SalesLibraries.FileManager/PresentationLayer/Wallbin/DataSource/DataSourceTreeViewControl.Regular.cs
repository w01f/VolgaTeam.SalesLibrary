using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	public sealed partial class DataSourceTreeViewControl
	{
		private readonly List<IDataSource> _dataSources = new List<IDataSource>();

		private async Task RefreshRegularFiles(bool expandAll = false)
		{
			treeListRegularFiles.Enabled = false;
			treeListRegularFiles.SuspendLayout();
			treeListRegularFiles.Nodes.Clear();
			var expandNode = treeListRegularFiles.AppendNode(new object[] { "Expand All" }, null);
			expandNode.StateImageIndex = 0;

			await Task.Run(() => Invoke(new MethodInvoker(() =>
			{
				foreach (var rootNode in _dataSources.Select(rootFolder =>
					treeListRegularFiles.AppendNode(new[] { rootFolder.Name }, null, new FolderLink { RootId = rootFolder.DataSourceId, Path = rootFolder.Path })))
				{
					rootNode.StateImageIndex = 0;
					FillNode(rootNode, expandAll);
				}
			})));
			treeListRegularFiles.ResumeLayout();
			treeListRegularFiles.Enabled = true;
		}

		public void ShowFileInTree(string filePath)
		{
			xtraTabControlFiles.SelectedTabPage = xtraTabPageRegular;
			treeListRegularFiles.Selection.Clear();

			treeListRegularFiles.SuspendLayout();
			laTreeViewProgressLabel.Text = "Searching Item...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			var thread = new Thread(() => Invoke(new MethodInvoker(() =>
			{
				foreach (var rootNode in treeListRegularFiles.Nodes.Skip(1).ToList())
					if (FindNodeByPath(rootNode, filePath))
						break;
			})));
			thread.Start();
			while (thread.IsAlive)
			{
				Thread.Sleep(500);
				Application.DoEvents();
			}

			xtraTabControlFiles.Enabled = true;
			circularProgressTreeView.IsRunning = false;
			pnTreeViewProgress.Visible = false;
			treeListRegularFiles.ResumeLayout();
			treeListRegularFiles.Enabled = true;
		}
	}
}
