using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin
{
	public partial class FormCompactWallbin
	{
		private readonly Pen _penBorder = new Pen(Color.Black) { DashStyle = DashStyle.Solid };
		private TreeListNode _dragOverNode;

		private void InsertLinks(IList<string> sourcePaths, TreeListNode targetNode)
		{
			TreeListNode parentNode;
			int positionToInsert;
			if (((WallbinItem) targetNode.Tag).Type == WallbinItemType.Folder)
			{
				parentNode = targetNode;
				positionToInsert = 0;
			}
			else
			{
				parentNode = targetNode.ParentNode;
				positionToInsert = parentNode.Nodes.IndexOf(targetNode) + 1;
			}

			var parentFolder = ((WallbinItem)parentNode.Tag).Source as LibraryFolder;
			if (parentFolder == null) return;

			var sourceLinks = new List<SourceLink>();
			foreach (var sourcePath in sourcePaths)
				sourceLinks.Add(SourceLink.FromExternalPath(sourcePath,parentFolder.Page.Library));

			if (!sourceLinks.Any()) return;

			var treeList = targetNode.TreeList;
			treeList.SuspendLayout();
			MainController.Instance.ProcessManager.Run(String.Format("Adding Link{0}...", sourceLinks.Count > 1 ? "s" : String.Empty),
				(cancelationToken, formProgess) =>
				{
					foreach (var sourceLink in sourceLinks)
					{
						var link = LibraryFileLink.Create(sourceLink, parentFolder);
						((List<BaseLibraryLink>)parentFolder.Links).InsertItem(link, positionToInsert);
					}

					MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(() =>
					{
						parentNode.Nodes.Clear();
						FillNode(parentNode);
					}));
				});
			treeList.ResumeLayout();

			RaiseDataChanged();
		}

		private void OnTreeViewDragOver(object sender, DragEventArgs e)
		{
			var treeList = sender as TreeList;
			if (treeList == null) return;

			var previuseDragOverNode = _dragOverNode;
			_dragOverNode = null;

			e.Effect = DragDropEffects.None;
			var p = treeList.PointToClient(new Point(e.X, e.Y));
			var targetNode = treeList.CalcHitInfo(p).Node;
			if (targetNode?.Tag is WallbinItem &&
				(((WallbinItem)targetNode.Tag).Type == WallbinItemType.Link ||
				 ((WallbinItem)targetNode.Tag).Type == WallbinItemType.Folder) &&
				e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				var droppedItemsPaths = e.Data.GetData(DataFormats.FileDrop) as String[];
				if (droppedItemsPaths != null)
				{
					e.Effect = DragDropEffects.Copy;
					_dragOverNode = targetNode;
				}
			}

			if (_dragOverNode != null)
				treeList.InvalidateNode(_dragOverNode);
			if (previuseDragOverNode != null && previuseDragOverNode != _dragOverNode)
				treeList.InvalidateNode(previuseDragOverNode);
		}

		private void OnTreeViewDragDrop(object sender, DragEventArgs e)
		{
			var treeList = sender as TreeList;
			if (treeList == null) return;

			var p = treeList.PointToClient(new Point(e.X, e.Y));
			var targetNode = treeList.CalcHitInfo(p).Node;
			var droppedItemsPaths = e.Data.GetData(DataFormats.FileDrop) as String[];

			InsertLinks(droppedItemsPaths,targetNode);

			if (_dragOverNode != null)
			{
				var currentDragOverNode = _dragOverNode;
				_dragOverNode = null;
				treeList.InvalidateNode(currentDragOverNode);
			}
		}
	}
}
