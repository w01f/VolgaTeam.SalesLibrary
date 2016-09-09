using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	[ToolboxItem(false)]
	public sealed partial class DataSourceTreeViewControl : UserControl
	{
		private readonly List<IDataSource> _dataSources = new List<IDataSource>();
		private TreeListHitInfo _dragStartHitInfo;

		public DataSourceTreeViewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if (!((CreateGraphics()).DpiX > 96)) return;
			laDoubleClick.Font = new Font(laDoubleClick.Font.FontFamily, laDoubleClick.Font.Size - 3, laDoubleClick.Font.Style);
			laEndDate.Font = new Font(laEndDate.Font.FontFamily, laEndDate.Font.Size - 2, laEndDate.Font.Style);
			laStartDate.Font = new Font(laStartDate.Font.FontFamily, laStartDate.Font.Size - 2, laStartDate.Font.Style);
			laTreeViewProgressLabel.Font = new Font(laTreeViewProgressLabel.Font.FontFamily, laTreeViewProgressLabel.Font.Size - 3, laTreeViewProgressLabel.Font.Style);
			checkEditDateRange.Font = new Font(checkEditDateRange.Font.FontFamily, checkEditDateRange.Font.Size - 2, checkEditDateRange.Font.Style);
			buttonXRefresh.Font = new Font(buttonXRefresh.Font.FontFamily, buttonXRefresh.Font.Size - 2, buttonXRefresh.Font.Style);
			buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
		}

		#region TreeView Data Methods
		private async void Refresh_Click(object sender, EventArgs e)
		{
			treeListAllFiles.SuspendLayout();
			laTreeViewProgressLabel.Text = "Loading Tree View...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			treeListAllFiles.Nodes.Clear();
			var expandNode = treeListAllFiles.AppendNode(new object[] { "Expand All" }, null);
			expandNode.StateImageIndex = 0;

			await Task.Run(() => Invoke(new MethodInvoker(() =>
			{
				foreach (var rootNode in _dataSources.Select(rootFolder =>
					treeListAllFiles.AppendNode(new[] { rootFolder.Name }, null, new FolderLink { RootId = rootFolder.DataSourceId, Path = rootFolder.Path })))
				{
					rootNode.StateImageIndex = 0;
					FillNode(rootNode, false);
				}
			})));

			xtraTabControlFiles.Enabled = true;
			circularProgressTreeView.IsRunning = false;
			pnTreeViewProgress.Visible = false;
			treeListAllFiles.ResumeLayout();
			treeListAllFiles.Enabled = true;
		}

		private void treeListAllFiles_MouseClick(object sender, MouseEventArgs e)
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
				tmiFileDelete.Visible = treeList == treeListAllFiles;
				contextMenuStripFile.Show(treeList, hitPoint);
			}
			else if (hitInfo.Node.Tag is FolderLink && hitInfo.Node.StateImageIndex == 1)
			{
				contextMenuStripFolder.Show(treeList, hitPoint);
			}
		}

		private async void treeListAllFiles_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Clicks != 2) return;
			var treeList = sender as TreeList;
			if (treeList == null) return;
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeList.CalcHitInfo(hitPoint);
			if (hitInfo.Node == null) return;
			treeList.SuspendLayout();
			if (hitInfo.Node.GetValue(treeListColumnName).Equals("Expand All"))
			{
				foreach (var nodeToDelete in treeList.Nodes.Skip(1).ToList())
					treeList.Nodes.Remove(nodeToDelete);
				laTreeViewProgressLabel.Text = "Loading Tree View...";
				pnTreeViewProgress.Visible = true;
				circularProgressTreeView.IsRunning = true;
				treeList.Enabled = false;

				await Task.Run(() => Invoke(new MethodInvoker(() =>
				{
					foreach (var rootNode in _dataSources.Select(rootFolder =>
						treeList.AppendNode(new[] { rootFolder.Name }, null, new FolderLink { RootId = rootFolder.DataSourceId, Path = rootFolder.Path })))
					{
						rootNode.StateImageIndex = 0;
						FillNode(rootNode, true);
					}
				})));

				pnTreeViewProgress.Visible = false;
				circularProgressTreeView.IsRunning = false;
				treeList.ResumeLayout();
				treeList.Enabled = true;
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
			treeList.ResumeLayout();
		}

		private void tmiFileOpen_Click(object sender, EventArgs e)
		{
			FileLink fileLink = null;
			switch (xtraTabControlFiles.SelectedTabPageIndex)
			{
				case 0:
					if (treeListAllFiles.Selection.Count > 0)
						fileLink = treeListAllFiles.Selection[0].Tag as FileLink;
					break;
				case 1:
					if (treeListSearchFiles.Selection.Count > 0)
						fileLink = treeListSearchFiles.Selection[0].Tag as FileLink;
					break;
			}
			if (fileLink != null)
				ViewItem(fileLink);
		}

		private void tmiFileDelete_Click(object sender, EventArgs e)
		{
			var fileNode = treeListAllFiles.Selection[0];
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

		private void tmiFolderCreate_Click(object sender, EventArgs e)
		{
			var folderNode = treeListAllFiles.Selection[0];
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
				var childNode = treeListAllFiles.AppendNode(new[] { subFolderLink.Name }, folderNode, subFolderLink);
				childNode.StateImageIndex = 0;
			}
		}

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
								subFolderLink.Path = subFolder;
								childNode = treeListAllFiles.AppendNode(new[] { subFolderLink.Name }, node, subFolderLink);
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
						foreach (var file in Directory.GetFiles(folderLink.Path))
						{
							if (!GlobalSettings.HiddenObjects.Any(x => file.ToLower().Contains(x.ToLower())))
							{
								var fileLink = new FileLink();
								fileLink.RootId = folderLink.RootId;
								fileLink.Path = file;
								childNode =
									treeListAllFiles.AppendNode(
										new[] { String.Format("{0} ({1})", fileLink.Name, File.GetLastWriteTime(file).ToString("MM/dd/yy hh:mm tt")) },
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
				case ".XLS":
				case ".XLT":
				case ".XLTX":
				case ".XLSX":
					return 3;
				case ".BMP":
				case ".JPG":
				case ".JPEG":
				case ".PNG":
				case ".GIF":
				case ".TIF":
				case ".TIFF":
				case ".ICO":
					return 4;
				case ".PDF":
					return 5;
				case ".PPT":
				case ".PPTX":
				case ".PPS":
				case ".PPSX":
				case ".PPTM":
					return 6;
				case ".MPEG":
				case ".MPG":
				case ".WMV":
				case ".ASF":
				case ".AVI":
				case ".MOV":
				case ".MP4":
				case ".M4V":
					return 7;
				case ".DOC":
				case ".DOCX":
					return 8;
				case ".KEY":
					return 9;
				case ".MP3":
					return 10;
				default:
					return 2;
			}
		}

		private void treeList_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left || ModifierKeys != Keys.None) return;
			var tl = sender as TreeList;
			if (tl != null) _dragStartHitInfo = tl.CalcHitInfo(e.Location);
		}

		private void treeList_MouseMove(object sender, MouseEventArgs e)
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
			var hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
			treeList.Cursor = hitInfo.Node != null ? Cursors.Hand : Cursors.Default;
		}

		private void treeListAllFiles_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			var p = treeListAllFiles.PointToClient(new Point(e.X, e.Y));
			var targetNode = treeListAllFiles.CalcHitInfo(p).Node;
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

		private async void treeListAllFiles_DragDrop(object sender, DragEventArgs e)
		{
			var p = treeListAllFiles.PointToClient(new Point(e.X, e.Y));
			var targetNode = treeListAllFiles.CalcHitInfo(p).Node;
			if (!(targetNode?.Tag is SourceLink && e.Data.GetDataPresent(DataFormats.FileDrop))) return;
			var droppedItemsPaths = e.Data.GetData(DataFormats.FileDrop) as String[];
			if (droppedItemsPaths == null) return;
			if (targetNode.Tag is FileLink)
				targetNode = targetNode.ParentNode;
			var targetFolderLink = (FolderLink)targetNode.Tag;
			var targetFolderPath = targetFolderLink.Path;

			if (droppedItemsPaths.Any(path => String.Equals(targetFolderPath, Path.GetDirectoryName(path), StringComparison.OrdinalIgnoreCase)))
				return;

			treeListAllFiles.SuspendLayout();
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
			treeListAllFiles.ResumeLayout();
			treeListAllFiles.Enabled = true;
		}

		private void treeListAllFiles_AfterCollapse(object sender, NodeEventArgs e)
		{
			if (e.Node.GetValue(treeListColumnName).Equals("Collapse All") ||
				e.Node.GetValue(treeListColumnName).Equals("Expand All"))
				return;
			e.Node.StateImageIndex = 0;
		}

		private void treeListAllFiles_AfterExpand(object sender, NodeEventArgs e)
		{
			if (e.Node.GetValue(treeListColumnName).Equals("Collapse All") ||
				e.Node.GetValue(treeListColumnName).Equals("Expand All"))
				return;
			e.Node.StateImageIndex = 1;
		}
		#endregion

		#region Kew Word Files Tree View
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

		private async void btSearch_Click(object sender, EventArgs e)
		{
			treeListSearchFiles.SuspendLayout();
			treeListSearchFiles.Nodes.Clear();
			laTreeViewProgressLabel.Text = "Searching Files...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			var files = new List<FileLink>();
			await Task.Run(() =>
			{
				files.AddRange(_dataSources.SelectMany(ds => SearchFileInFolder(new FolderLink() { RootId = ds.DataSourceId, Path = ds.Path }, textEditKeyWord.EditValue as String)));
				if (files.Count <= 0) return;
				files.Sort((x, y) => x.Name.CompareTo(y.Name));
				Invoke(new MethodInvoker(() =>
				{
					foreach (var file in files)
					{
						var childNode = treeListSearchFiles.AppendNode(new[] { String.Format("{0} ({1})", file.Name, File.GetLastWriteTime(file.Path).ToString("MM/dd/yy hh:mm tt")) }, null, file);
						childNode.StateImageIndex = GetImageindex(file.Path);
						Application.DoEvents();
					}
				}));
			});

			pnTreeViewProgress.Visible = false;
			circularProgressTreeView.IsRunning = false;
			treeListSearchFiles.ResumeLayout();
			xtraTabControlFiles.Enabled = true;
			if (files.Count == 0)
				MainController.Instance.PopupMessages.ShowInfo("Files was not found");
		}

		private void edKeyWord_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				btSearch_Click(null, null);
		}

		private void ckDateRange_CheckedChanged(object sender, EventArgs e)
		{
			if (checkEditDateRange.Checked)
				pnKeyWord.Height = groupControlDateRange.Bottom + 4;
			else
				pnKeyWord.Height = groupControlDateRange.Top;
		}
		#endregion

		#region Other GUI Events
		private void xtraTabControlFiles_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			switch (xtraTabControlFiles.SelectedTabPageIndex)
			{
				case 0:
					treeListSearchFiles.Selection.Clear();
					break;
				case 1:
					treeListAllFiles.Selection.Clear();
					buttonXSearch.Refresh();
					break;
			}
		}
		#endregion

		#region Common Methods
		public void LoadData(IEnumerable<IDataSource> dataSources)
		{
			_dataSources.Clear();
			_dataSources.AddRange(dataSources.OrderBy(ds => ds.Order));
			Refresh_Click(buttonXRefresh, EventArgs.Empty);
			ckDateRange_CheckedChanged(checkEditDateRange, EventArgs.Empty);
		}

		public void ShowFileInTree(string filePath)
		{
			xtraTabControlFiles.SelectedTabPage = xtraTabPageRegular;
			treeListAllFiles.Selection.Clear();

			treeListAllFiles.SuspendLayout();
			laTreeViewProgressLabel.Text = "Searching Item...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			var thread = new Thread(() => Invoke(new MethodInvoker(() =>
			{
				foreach (var rootNode in treeListAllFiles.Nodes.Skip(1).ToList())
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
			treeListAllFiles.ResumeLayout();
			treeListAllFiles.Enabled = true;
		}

		private void ViewItem(FileLink file)
		{
			Utils.OpenFile(file.Path);
		}
		#endregion
	}
}