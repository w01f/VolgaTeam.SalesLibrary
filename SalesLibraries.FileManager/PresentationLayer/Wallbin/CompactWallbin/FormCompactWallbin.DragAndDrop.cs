using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls;

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
			if (((WallbinItem)targetNode.Tag).Type == WallbinItemType.Folder)
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
				sourceLinks.Add(SourceLink.FromExternalPath(sourcePath, parentFolder.Page.Library));


			var existedPreviewContainerPairs = new Dictionary<SourceLink, List<BasePreviewContainer>>();
			foreach (var extrernalLink in sourceLinks.Where(link => link.IsExternal).OfType<FileLink>().ToList())
			{
				var existedPreviewContainers = parentFolder.Page.Library.PreviewContainers
					.Where(previewContainer => String.Equals(Path.GetFileName(previewContainer.SourcePath), extrernalLink.Name,
						StringComparison.OrdinalIgnoreCase))
					.ToList();
				if (existedPreviewContainers.Any())
					existedPreviewContainerPairs.Add(extrernalLink, existedPreviewContainers);
			}

			if (!sourceLinks.Any()) return;

			var confirmDrop = true;
			if (existedPreviewContainerPairs.Any())
			{
				using (var form = new FormUpdateFile())
				{
					var result = form.ShowDialog(MainController.Instance.MainForm);
					confirmDrop = result == DialogResult.Yes;
					if (result == DialogResult.No)
					{
						foreach (var previewContainerPair in existedPreviewContainerPairs)
						{
							foreach (var previewContainer in previewContainerPair.Value)
							{
								previewContainer.ClearContent();
								try
								{
									File.Copy(previewContainerPair.Key.Path, previewContainer.SourcePath, true);
								}
								catch { }
							}
						}
						MainController.Instance.PopupMessages.ShowInfo("File updated");
					}
				}
			}

			if (!confirmDrop) return;

			var treeList = targetNode.TreeList;
			treeList.SuspendLayout();
			MainController.Instance.ProcessManager.Run(
				String.Format("Adding Link{0}...", sourceLinks.Count > 1 ? "s" : String.Empty),
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

			InsertLinks(droppedItemsPaths, targetNode);

			if (_dragOverNode != null)
			{
				var currentDragOverNode = _dragOverNode;
				_dragOverNode = null;
				treeList.InvalidateNode(currentDragOverNode);
			}
		}
	}
}
