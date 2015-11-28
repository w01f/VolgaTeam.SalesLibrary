using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
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
			if (e.Button != MouseButtons.Right) return;
			var treeList = sender as TreeList;
			if (treeList == null) return;
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
			if (hitInfo.Node == null) return;
			if (hitInfo.Node.Tag == null) return;
			if (hitInfo.Node.Tag.GetType() != typeof(FileLink)) return;
			treeListAllFiles.Selection.Clear();
			hitInfo.Node.Selected = true;
			contextMenuStrip.Show(treeListAllFiles, hitPoint);
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
				if (hitInfo.Node.Tag.GetType() == typeof(FolderLink))
					FillNode(hitInfo.Node, false);
				else if (hitInfo.Node.Tag.GetType() == typeof(FileLink))
					ViewItem(hitInfo.Node.Tag as FileLink);
			}
			treeList.ResumeLayout();
		}

		private void tmiOpen_Click(object sender, EventArgs e)
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

		private void FillNode(TreeListNode node, bool showSubItems)
		{
			if (node.Tag == null) return;
			var folderLink = node.Tag as FolderLink;
			if (folderLink == null || node.Nodes.Count != 0) return;
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
				catch { }
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
							childNode = treeListAllFiles.AppendNode(new[] { String.Format("{0} ({1})", fileLink.Name, File.GetLastWriteTime(file).ToString("MM/dd/yy hh:mm tt")) }, node, fileLink);
							childNode.StateImageIndex = GetImageindex(file);
						}
						Application.DoEvents();
					}
				}
				catch { }
				node.StateImageIndex = 1;
				node.Expanded = true;
			}
			catch { }
		}

		private int GetImageindex(string filePath)
		{
			switch (Path.GetExtension(filePath).ToUpper())
			{
				case ".XLS":
				case ".XLSX":
				case ".XLT":
				case ".XLTX":
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
				case ".URL":
					return 8;
				case ".DOC":
				case ".DOCX":
					return 9;
				case ".KEY":
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
					treeList.DoDragDrop(
						treeList.Selection
							.OfType<TreeListNode>()
							.Select(node => node.Tag)
							.OfType<SourceLink>()
							.ToArray()
						, DragDropEffects.Copy);
					return;
				}
			}
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
			treeList.Cursor = hitInfo.Node != null ? Cursors.Hand : Cursors.Default;
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

		private void ViewItem(FileLink file)
		{
			try
			{
				Process.Start(file.Path);
			}
			catch { }
		}
		#endregion
	}
}