using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	public sealed partial class DataSourceTreeViewControl
	{
		private TreeListHitInfo _dragStartHitInfo;

		private TreeList SelectedTreeList
		{
			get
			{
				switch (xtraTabControlFiles.SelectedTabPageIndex)
				{
					case 0:
						return treeListRegularFiles;
					case 1:
						return treeListExternalFiles;
					case 2:
						return treeListSearchFiles;
					default:
						throw new ArgumentOutOfRangeException("Undefined TreeList");
				}
			}
		}

		#region Methods
		private void FillNode(TreeListNode node, bool showSubItems)
		{
			var folderLink = node.Tag as FolderLink;
			if (folderLink == null) return;
			if (node.Nodes.Count == 0)
			{
				try
				{
					TreeListNode childNode;
					try
					{
						var folders = new List<string>();
						folders.AddRange(Directory.GetDirectories(folderLink.Path));
						folders.Sort(WinAPIHelper.StrCmpLogicalW);
						foreach (var subFolder in folders)
						{
							if (!GlobalSettings.HiddenObjects.Any(x => subFolder.ToLower().Contains(x.ToLower())))
							{
								var subFolderLink = new FolderLink();
								subFolderLink.RootId = folderLink.RootId;
								subFolderLink.IsExternal = !_dataSources.Any(ds => subFolder.Contains(ds.Path));
								subFolderLink.Path = subFolder;
								childNode = node.TreeList.AppendNode(new[] { subFolderLink.Name }, node, subFolderLink);
								childNode.StateImageIndex = 0;

								if (showSubItems)
									FillNode(childNode, showSubItems);
								if (showSubItems && childNode.Nodes.Count == 0)
									node.Nodes.Remove(childNode);
							}
						}
					}
					catch
					{
					}
					try
					{
						var files = new List<string>();
						files.AddRange(Directory.GetFiles(folderLink.Path));
						files.Sort(WinAPIHelper.StrCmpLogicalW);
						foreach (var file in files.Where(f => !f.ToLower().EndsWith(".ini")))
						{
							if (!GlobalSettings.HiddenObjects.Any(x => file.ToLower().Contains(x.ToLower())))
							{
								var fileLink = new FileLink();
								fileLink.RootId = folderLink.RootId;
								fileLink.IsExternal = !_dataSources.Any(ds => file.Contains(ds.Path));
								fileLink.Path = file;
								childNode = node.TreeList.AppendNode(
										new[]
										{
											String.Format("{0} ({1:MM/dd/yy hh:mm tt})",
											fileLink.Name,
											File.GetLastWriteTime(file))
										},
										node, fileLink);
								childNode.StateImageIndex = GetImageindex(file);
							}
							Application.DoEvents();
						}
					}
					catch
					{
					}
				}
				catch
				{
				}
			}
			node.StateImageIndex = 1;
			node.Expanded = true;
		}

		private bool FindNodeByPath(TreeListNode targetNode, string itemPath)
		{
			var nodeSourceLink = (SourceLink)targetNode.Tag;
			if (itemPath.Equals(nodeSourceLink.Path, StringComparison.OrdinalIgnoreCase))
			{
				targetNode.TreeList.MakeNodeVisible(targetNode);
				targetNode.Selected = true;
				return true;
			}
			if (itemPath.ToUpper().Contains(nodeSourceLink.Path.ToUpper()))
			{
				if (!targetNode.Nodes.Any())
					FillNode(targetNode, false);
				if (targetNode.Nodes.ToList().Any(childNode => FindNodeByPath(childNode, itemPath)))
					return true;
			}
			return false;
		}

		private int GetImageindex(string filePath)
		{
			switch (Path.GetExtension(filePath).ToUpper())
			{
				case ".7Z":
					return 3;
				case ".AEP":
					return 4;
				case ".AET":
					return 5;
				case ".AI":
					return 6;
				case ".AIT":
					return 7;
				case ".DOC":
					return 8;
				case ".DOCX":
					return 9;
				case ".EPS":
					return 10;
				case ".JPEG":
					return 11;
				case ".KEY":
					return 12;
				case ".MOV":
					return 13;
				case ".MP3":
					return 14;
				case ".MP4":
				case ".MPEG":
				case ".MPG":
				case ".WMV":
				case ".ASF":
				case ".AVI":
				case ".M4V":
					return 15;
				case ".PDD":
					return 16;
				case ".PDF":
					return 17;
				case ".PNG":
				case ".BMP":
				case ".JPG":
				case ".GIF":
				case ".TIF":
				case ".TIFF":
				case ".ICO":
					return 18;
				case ".PPT":
				case ".PPS":
				case ".PPSX":
				case ".PPTM":
					return 19;
				case ".PPTX":
					return 20;
				case ".PS":
					return 21;
				case ".PSD":
					return 22;
				case ".RAR":
					return 23;
				case ".SVG":
					return 24;
				case ".URL":
					return 25;
				case ".XLS":
				case ".XLT":
				case ".XLTX":
					return 26;
				case ".XLSX":
					return 27;
				case ".XML":
					return 28;
				case ".ZIP":
					return 29;
				default:
					return 2;
			}
		}

		private void ViewItem(FileLink file)
		{
			Utils.OpenFile(file.Path);
		}
		#endregion

		#region Event handlers
		private void OnFilesTreeViewMouseClick(object sender, MouseEventArgs e)
		{
			var treeList = sender as TreeList;
			if (treeList == null) return;
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeList.CalcHitInfo(hitPoint);
			if (!(hitInfo.Node?.Tag is SourceLink)) return;
			var ctrlSelect = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			var shiftSelect = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			if (!ctrlSelect && !shiftSelect)
			{
				treeList.Selection.Clear();
				hitInfo.Node.Selected = true;
			}
			if (e.Button != MouseButtons.Right) return;
			if (hitInfo.Node.Tag is FileLink)
			{
				tmiFileDelete.Visible = treeList != treeListSearchFiles;
				contextMenuStripFile.Show(treeList, hitPoint);
			}
			else if (hitInfo.Node.Tag is FolderLink && hitInfo.Node.StateImageIndex == 1)
			{
				contextMenuStripFolder.Show(treeList, hitPoint);
			}
		}

		private async void OnFilesTreeViewMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Clicks != 2) return;
			var treeList = sender as TreeList;
			if (treeList == null) return;
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeList.CalcHitInfo(hitPoint);
			if (hitInfo.Node == null) return;
			if (hitInfo.Node.GetValue(treeListColumnName).Equals("Expand All"))
			{
				foreach (var nodeToDelete in treeList.Nodes.Skip(1).ToList())
					treeList.Nodes.Remove(nodeToDelete);
				laTreeViewProgressLabel.Text = "Loading Tree View...";
				pnTreeViewProgress.Visible = true;
				circularProgressTreeView.IsRunning = true;

				switch (xtraTabControlFiles.SelectedTabPageIndex)
				{
					case 0:
						await RefreshRegularFiles(true);
						break;
					case 1:
						await RefreshExternalFiles(true);
						break;
				}

				pnTreeViewProgress.Visible = false;
				circularProgressTreeView.IsRunning = false;
				hitInfo.Node.SetValue(treeListColumnName, "Collapse All");
				hitInfo.Node.SetValue(treeListColumnPath, "Collapse All");
				hitInfo.Node.StateImageIndex = 1;
			}
			else if (hitInfo.Node.GetValue(treeListColumnName).Equals("Collapse All"))
			{
				treeList.SuspendLayout();
				treeList.Visible = false;
				treeList.CollapseAll();
				treeList.Nodes.ToList().ForEach(node => node.StateImageIndex = 0);
				hitInfo.Node.SetValue(treeListColumnName, "Expand All");
				hitInfo.Node.SetValue(treeListColumnPath, "Expand All");
				treeList.Visible = true;
				treeList.ResumeLayout();
			}
			else if (hitInfo.Node.Tag != null)
			{
				if (hitInfo.Node.Tag is FolderLink)
				{
					if (hitInfo.Node.Nodes.Count == 0 && hitInfo.Node.StateImageIndex == 0)
						FillNode(hitInfo.Node, false);
					else if (hitInfo.Node.Nodes.Count == 0 && hitInfo.Node.StateImageIndex == 1)
						hitInfo.Node.StateImageIndex = 0;
				}
				else if (hitInfo.Node.Tag is FileLink)
					ViewItem((FileLink)hitInfo.Node.Tag);
			}
		}

		private void OnFilesTreeViewMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left || ModifierKeys != Keys.None) return;
			var tl = sender as TreeList;
			if (tl != null) _dragStartHitInfo = tl.CalcHitInfo(e.Location);
		}

		private void OnFilesTreeViewMouseMove(object sender, MouseEventArgs e)
		{
			var treeList = sender as TreeList;
			if (treeList == null) return;
			if (e.Button == MouseButtons.Left && _dragStartHitInfo != null && _dragStartHitInfo.Node != null)
			{
				var dragSize = SystemInformation.DragSize;
				var dragRect = new Rectangle(new Point(_dragStartHitInfo.MousePoint.X - dragSize.Width / 2,
					_dragStartHitInfo.MousePoint.Y - dragSize.Height / 2), dragSize);
				if (!dragRect.Contains(e.Location))
				{
					var souceLinks = treeList.Selection
						.OfType<TreeListNode>()
						.Select(node => node.Tag)
						.OfType<SourceLink>()
						.ToArray();

					var data = new DataObject();
					data.SetData(DataFormats.FileDrop, souceLinks.Select(link => link.Path).ToArray());
					data.SetData(DataFormats.Serializable, souceLinks);

					treeList.DoDragDrop(data, DragDropEffects.Copy);
					return;
				}
			}
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeList.CalcHitInfo(hitPoint);
			treeList.Cursor = hitInfo.Node != null ? Cursors.Hand : Cursors.Default;
		}

		private void OnFilesTreeViewDragOver(object sender, DragEventArgs e)
		{
			var treeList = sender as TreeList;
			if (treeList == null) return;

			e.Effect = DragDropEffects.None;
			var p = treeList.PointToClient(new Point(e.X, e.Y));
			var targetNode = treeList.CalcHitInfo(p).Node;
			if (!(targetNode?.Tag is SourceLink) || !e.Data.GetDataPresent(DataFormats.FileDrop)) return;
			var droppedItemsPaths = e.Data.GetData(DataFormats.FileDrop) as String[];
			if (droppedItemsPaths == null) return;
			if (targetNode.Tag is FileLink)
				targetNode = targetNode.ParentNode;
			var targetFolderLink = (FolderLink)targetNode.Tag;
			var targetFolderPath = targetFolderLink.Path;
			if (!droppedItemsPaths.Any(path => path.Equals(targetFolderPath, StringComparison.OrdinalIgnoreCase)))
				e.Effect = DragDropEffects.Copy;
		}

		private async void OnFilesTreeViewDragDrop(object sender, DragEventArgs e)
		{
			var treeList = sender as TreeList;
			if (treeList == null) return;

			var p = treeList.PointToClient(new Point(e.X, e.Y));
			var targetNode = treeList.CalcHitInfo(p).Node;
			if (!(targetNode?.Tag is SourceLink && e.Data.GetDataPresent(DataFormats.FileDrop))) return;
			var droppedItemsPaths = e.Data.GetData(DataFormats.FileDrop) as String[];
			if (droppedItemsPaths == null) return;
			if (targetNode.Tag is FileLink)
				targetNode = targetNode.ParentNode;
			var targetFolderLink = (FolderLink)targetNode.Tag;
			var targetFolderPath = targetFolderLink.Path;

			if (droppedItemsPaths.Any(path => String.Equals(targetFolderPath, Path.GetDirectoryName(path), StringComparison.OrdinalIgnoreCase)))
				return;

			treeList.SuspendLayout();
			laTreeViewProgressLabel.Text = "Copying Files...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			await Task.Run(() =>
			{
				try
				{
					Utils.CopyToFolder(targetFolderPath, droppedItemsPaths);
				}
				catch
				{
				}
				Invoke(new MethodInvoker(() =>
				{
					targetNode.Nodes.Clear();
					FillNode(targetNode, false);
				}));
			});

			xtraTabControlFiles.Enabled = true;
			circularProgressTreeView.IsRunning = false;
			pnTreeViewProgress.Visible = false;
			treeList.ResumeLayout();
			treeList.Enabled = true;
		}

		private void OnFilesTreeViewAfterCollapse(object sender, NodeEventArgs e)
		{
			if (e.Node.GetValue(treeListColumnName).Equals("Collapse All") ||
				e.Node.GetValue(treeListColumnName).Equals("Expand All"))
				return;
			e.Node.StateImageIndex = 0;
		}

		private void OnFilesTreeViewAfterExpand(object sender, NodeEventArgs e)
		{
			if (e.Node.GetValue(treeListColumnName).Equals("Collapse All") ||
				e.Node.GetValue(treeListColumnName).Equals("Expand All"))
				return;
			e.Node.StateImageIndex = 1;
		}
		#endregion
	}
}
