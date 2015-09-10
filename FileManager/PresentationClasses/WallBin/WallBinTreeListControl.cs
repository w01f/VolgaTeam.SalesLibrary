using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using FileManager.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;

namespace FileManager.PresentationClasses.WallBin
{
	[ToolboxItem(false)]
	public partial class WallBinTreeListControl : UserControl
	{
		private readonly List<FolderLink> _rootFolders = new List<FolderLink>();
		private TreeListHitInfo _dragStartHitInfo;
		private Library _parentLibrary;

		public WallBinTreeListControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laDoubleClick.Font = new Font(laDoubleClick.Font.FontFamily, laDoubleClick.Font.Size - 3, laDoubleClick.Font.Style);
				laEndDate.Font = new Font(laEndDate.Font.FontFamily, laEndDate.Font.Size - 2, laEndDate.Font.Style);
				laStartDate.Font = new Font(laStartDate.Font.FontFamily, laStartDate.Font.Size - 2, laStartDate.Font.Style);
				laTreeViewProgressLabel.Font = new Font(laTreeViewProgressLabel.Font.FontFamily, laTreeViewProgressLabel.Font.Size - 3, laTreeViewProgressLabel.Font.Style);
				checkEditDateRange.Font = new Font(checkEditDateRange.Font.FontFamily, checkEditDateRange.Font.Size - 2, checkEditDateRange.Font.Style);
				buttonXRefresh.Font = new Font(buttonXRefresh.Font.FontFamily, buttonXRefresh.Font.Size - 2, buttonXRefresh.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}
		}

		#region TreeView Data Methods
		private void Refresh_Click(object sender, EventArgs e)
		{
			treeListAllFiles.SuspendLayout();
			laTreeViewProgressLabel.Text = "Loading Tree View...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			treeListAllFiles.Nodes.Clear();
			TreeListNode expandNode = treeListAllFiles.AppendNode(new object[] { "Expand All" }, null);
			expandNode.StateImageIndex = 0;

			var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate
			{
				foreach (FolderLink rootFolder in _rootFolders)
				{
					var rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Folder.Name }, null, rootFolder);
					rootNode.StateImageIndex = 0;
					FillNode(rootNode, false);
					Application.DoEvents();
				}
			}));
			thread.Start();

			while (thread.IsAlive)
				Application.DoEvents();

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

		private void treeListAllFiles_MouseDoubleClick(object sender, MouseEventArgs e)
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
				var nodesToDelete = new List<TreeListNode>();
				for (int i = 1; i < treeList.Nodes.Count; i++)
					nodesToDelete.Add(treeList.Nodes[i]);
				foreach (TreeListNode node in nodesToDelete)
					treeList.Nodes.Remove(node);

				laTreeViewProgressLabel.Text = "Loading Tree View...";
				pnTreeViewProgress.Visible = true;
				circularProgressTreeView.IsRunning = true;
				treeList.Enabled = false;

				var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate
				{
					foreach (FolderLink rootFolder in _rootFolders)
					{
						TreeListNode rootNode = treeList.AppendNode(new object[] { rootFolder.Folder.Name }, null, rootFolder);
						rootNode.StateImageIndex = 0;
						FillNode(rootNode, true);
						Application.DoEvents();
					}
				}));
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();
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
				treeList.Nodes[1].Expanded = true;
				hitInfo.Node.SetValue(treeListColumnName, "Expand All");
				hitInfo.Node.SetValue(treeListColumnPath, "Expand All");
				hitInfo.Node.StateImageIndex = 0;
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
					var folders = new List<DirectoryInfo>();
					folders.AddRange(folderLink.Folder.GetDirectories());
					folders.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
					foreach (var subFolder in folders)
					{
						if (!SettingsManager.Instance.HiddenObjects.Any(x => subFolder.FullName.ToLower().Contains(x.ToLower())))
						{
							var subFolderLink = new FolderLink();
							subFolderLink.RootId = folderLink.RootId;
							subFolderLink.Folder = subFolder;
							childNode = treeListAllFiles.AppendNode(new object[] { subFolder.Name }, node, subFolderLink);
							childNode.StateImageIndex = 0;

							Application.DoEvents();

							if (showSubItems)
								FillNode(childNode, showSubItems);
							if (showSubItems && childNode.Nodes.Count == 0)
								node.Nodes.Remove(childNode);
						}
						Application.DoEvents();
					}
				}
				catch { }
				try
				{
					var files = new List<FileInfo>();
					files.AddRange(folderLink.Folder.GetFiles());
					files.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
					foreach (var file in files)
					{
						if (!SettingsManager.Instance.HiddenObjects.Any(x => file.Name.ToLower().Contains(x.ToLower())) && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
						{
							var fileLink = new FileLink();
							fileLink.RootId = folderLink.RootId;
							fileLink.File = file;
							childNode = treeListAllFiles.AppendNode(new object[] { file.Name + " (" + file.LastWriteTime.ToString("MM/dd/yy hh:mm tt") + ")" }, node, fileLink);
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

		private int GetImageindex(FileInfo file)
		{
			switch (file.Extension.ToUpper())
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
			_dragStartHitInfo = tl.CalcHitInfo(e.Location);
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
					var dragData = new List<object>();
					foreach (TreeListNode node in treeList.Selection)
						if (!node.GetValue(treeListColumnName).Equals("Expand All") && !node.GetValue(treeListColumnName).Equals("Collapse All"))
							if (node.Tag.GetType() == typeof(FileLink) || node.Tag.GetType() == typeof(FolderLink))
								dragData.Add(node.Tag);
					if (dragData.Count > 0)
						treeList.DoDragDrop(new DataObject(DataFormats.Serializable, dragData.ToArray()), DragDropEffects.Copy);
					return;
				}
			}
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
			treeList.Cursor = hitInfo.Node != null ? Cursors.Hand : Cursors.Default;
		}
		#endregion

		#region Kew Word Files Tree View
		private void SearchFileInFolder(FolderLink folderLink, string keyWord, List<FileLink> files)
		{
			try
			{
				foreach (var subFolder in folderLink.Folder.GetDirectories())
					if (!SettingsManager.Instance.HiddenObjects.Any(x => subFolder.FullName.ToLower().Contains(x.ToLower())))
					{
						var subFolderLink = new FolderLink();
						subFolderLink.RootId = folderLink.RootId;
						subFolderLink.Folder = subFolder;
						SearchFileInFolder(subFolderLink, keyWord, files);
					}
			}
			catch { }
			try
			{
				foreach (FileInfo file in folderLink.Folder.GetFiles("*" + keyWord + "*.*"))
				{
					if (((file.LastWriteTime >= dateEditStartDate.DateTime && file.LastWriteTime <= dateEditEndDate.DateTime) || !checkEditDateRange.Checked) && SettingsManager.Instance.HiddenObjects.Where(x => file.FullName.ToLower().Contains(x.ToLower())).Count() == 0 && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
					{
						var fileLink = new FileLink();
						fileLink.RootId = folderLink.RootId;
						fileLink.File = file;
						files.Add(fileLink);
					}
				}
			}
			catch { }
		}

		private void btSearch_Click(object sender, EventArgs e)
		{
			treeListSearchFiles.SuspendLayout();
			treeListSearchFiles.Nodes.Clear();
			laTreeViewProgressLabel.Text = "Searching Files...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			var files = new List<FileLink>();
			var thread = new Thread(() =>
			{
				foreach (FolderLink folder in _rootFolders)
					SearchFileInFolder(folder, textEditKeyWord.EditValue != null ? textEditKeyWord.EditValue.ToString() : string.Empty, files);
				if (files.Count > 0)
				{
					files.Sort((x, y) => x.File.Name.CompareTo(y.File.Name));
					FormMain.Instance.Invoke((MethodInvoker)delegate
																{
																	foreach (FileLink file in files)
																	{
																		TreeListNode childNode = treeListSearchFiles.AppendNode(new object[] { file.File.Name + " (" + file.File.LastWriteTime.ToShortDateString() + " " + file.File.LastWriteTime.ToShortTimeString() + ")" }, null, file);
																		childNode.StateImageIndex = GetImageindex(file.File);
																		Application.DoEvents();
																	}
																});
				}
			});
			thread.Start();

			while (thread.IsAlive)
				Application.DoEvents();

			pnTreeViewProgress.Visible = false;
			circularProgressTreeView.IsRunning = false;
			treeListSearchFiles.ResumeLayout();
			xtraTabControlFiles.Enabled = true;
			if (files.Count == 0)
				AppManager.Instance.ShowInfo("Files was not found");
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
		public void Init(Library library)
		{
			_parentLibrary = library;

			splitContainerControl.PanelVisibility = _parentLibrary.UseDirectAccess ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1;

			_rootFolders.Clear();
			_rootFolders.AddRange(_parentLibrary.ExtraFolders);
			_rootFolders.Sort((x, y) => (x as RootFolder).Order.CompareTo((y as RootFolder).Order));
			_rootFolders.Insert(0, _parentLibrary.RootFolder);

			if (_parentLibrary.UseDirectAccess)
				UpdateStatistic();

			Refresh_Click(null, null);
			ckDateRange_CheckedChanged(null, null);
		}

		private void ViewItem(FileLink file)
		{
			try
			{
				Process.Start(file.File.FullName);
			}
			catch { }
		}

		private void UpdateStatistic()
		{
			var folders = new List<DirectoryInfo>();
			var files = new List<FileInfo>();

			labelControlTotalFolders.Text = string.Empty;
			labelControlFiles.Text = string.Empty;

			laStatisticProgressLable.Text = "Loading statistic...";
			pnStatisticProgress.Visible = true;
			circularProgressStatistic.IsRunning = true;
			var thread = new Thread(() =>
			{
				foreach (var folder in _rootFolders.Select(x => x.Folder))
				{
					var childFolder = GetFolders(folder);
					if (childFolder.Length > 0 || folder.GetFiles().Any(x => x.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate))
					{
						folders.AddRange(childFolder);
						folders.Add(folder);
					}
					files.AddRange(GetFiles(folder));
				}
				files.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Extension, y.Extension));
				var filesStatistic = new StringBuilder();
				foreach (string extension in files.Select(x => x.Extension.ToLower()).Distinct())
				{
					filesStatistic.AppendLine(string.Format("{0}: {1}", new[] { extension.Replace(".", string.Empty), files.Where(x => x.Extension.ToLower().Equals(extension)).Count().ToString("# ##0") }));
					filesStatistic.AppendLine(string.Empty);
				}
				FormMain.Instance.Invoke((MethodInvoker)delegate
				{
					labelControlTotalFolders.Text = string.Format("Total folders: {0}", folders.Count.ToString("# ##0"));
					labelControlFiles.Text = filesStatistic.ToString();
					circularProgressStatistic.IsRunning = false;
					pnStatisticProgress.Visible = false;
				});
			});
			thread.Start();
		}

		private IEnumerable<FileInfo> GetFiles(DirectoryInfo folder)
		{
			var files = new List<FileInfo>();
			try
			{
				foreach (var subFolder in folder.GetDirectories())
					if (!SettingsManager.Instance.HiddenObjects.Any(x => subFolder.FullName.ToLower().Contains(x.ToLower())))
						files.AddRange(GetFiles(subFolder));
			}
			catch { }
			try
			{
				files.AddRange(folder.GetFiles().Where(x => x.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate && !SettingsManager.Instance.HiddenObjects.Any(y => x.FullName.ToLower().Contains(y.ToLower()))));
			}
			catch { }
			return files.ToArray();
		}

		private DirectoryInfo[] GetFolders(DirectoryInfo folder)
		{
			var folders = new List<DirectoryInfo>();
			try
			{
				foreach (var subFolder in folder.GetDirectories())
				{
					if (SettingsManager.Instance.HiddenObjects.Any(x => subFolder.FullName.ToLower().Contains(x.ToLower()))) continue;
					var childFolder = GetFolders(subFolder);
					try
					{
						if (childFolder.Length > 0 || subFolder.GetFiles().Any(x => x.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate && SettingsManager.Instance.HiddenObjects.Where(y => x.FullName.ToLower().Contains(y.ToLower())).Count() == 0))
						{
							folders.AddRange(childFolder);
							folders.Add(subFolder);
						}
					}
					catch { }
				}
			}
			catch { }
			return folders.ToArray();
		}
		#endregion
	}
}