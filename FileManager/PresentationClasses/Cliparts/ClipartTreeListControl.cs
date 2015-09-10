using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using FileManager.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;

namespace FileManager.PresentationClasses.Cliparts
{
	public partial class ClipartTreeListControl : UserControl
	{
		private TreeListHitInfo dragStartHitInfo;

		public ClipartTreeListControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXRefresh.Font = new Font(buttonXRefresh.Font.FontFamily, buttonXRefresh.Font.Size - 2, buttonXRefresh.Font.Style);
			}
		}

		#region TreeView Data Methods
		private void Refresh_Click(object sender, EventArgs e)
		{
			Init();
		}

		private void treeListAllFiles_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Clicks != 2) return;
			var treeList = sender as TreeList;
			if (treeList == null) return;
			var hitPoint = new Point(e.X, e.Y);
			TreeListHitInfo hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
			if (hitInfo.Node == null) return;
			treeList.SuspendLayout();
			if (hitInfo.Node.Tag != null)
			{
				if (hitInfo.Node.Tag.GetType() == typeof(DirectoryInfo))
					FillNode(hitInfo.Node, true);
				else if (hitInfo.Node.Tag.GetType() == typeof(FileInfo))
					ViewItem(hitInfo.Node.Tag as FileInfo);
			}
			treeList.ResumeLayout();
		}

		private void FillNode(TreeListNode node, bool showFiles)
		{
			TreeListNode childNode;
			if (node.Tag == null) return;
			if (node.Tag.GetType() != typeof(DirectoryInfo)) return;
			if (node.Nodes.Count != 0) return;
			var folder = (DirectoryInfo)node.Tag;
			try
			{
				{
					var folders = new List<DirectoryInfo>();
					folders.AddRange(folder.GetDirectories());
					folders.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
					foreach (DirectoryInfo subFolder in folders)
					{
						if (!SettingsManager.Instance.HiddenObjects.Any(x => subFolder.FullName.ToLower().Contains(x.ToLower())))
						{
							childNode = treeListAllFiles.AppendNode(new object[] { subFolder.Name }, node, subFolder);
							childNode.StateImageIndex = 0;
						}
					}
				}
				if (showFiles)
				{
					var files = new List<FileInfo>();
					files.AddRange(folder.GetFiles());
					files.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
					foreach (FileInfo file in files)
					{
						switch (file.Extension.ToLower())
						{
							case ".png":
							case ".jpg":
							case ".bmp":
							case ".gif":
							case ".tif":
							case ".wmf":
								childNode = treeListAllFiles.AppendNode(new object[] { file.Name + " (" + file.LastWriteTime.ToShortDateString() + " " + file.LastWriteTime.ToShortTimeString() + ")" }, node, file);
								childNode.StateImageIndex = 2;
								break;
						}
					}
				}
				node.StateImageIndex = 1;
				node.Expanded = true;
			}
			catch { }
		}

		private void treeList_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && ModifierKeys == Keys.None)
			{
				var tl = sender as TreeList;
				dragStartHitInfo = tl.CalcHitInfo(e.Location);
			}
		}

		private void treeList_MouseMove(object sender, MouseEventArgs e)
		{
			var treeList = sender as TreeList;
			if (treeList == null) return;
			if (e.Button == MouseButtons.Left && dragStartHitInfo != null && dragStartHitInfo.Node != null)
			{
				Size dragSize = SystemInformation.DragSize;
				var dragRect = new Rectangle(new Point(dragStartHitInfo.MousePoint.X - dragSize.Width / 2,
					dragStartHitInfo.MousePoint.Y - dragSize.Height / 2), dragSize);
				if (!dragRect.Contains(e.Location))
				{
					var dragData = new List<object>();
					foreach (TreeListNode node in treeList.Selection)
						if (node.Tag.GetType() == typeof(FileInfo))
							dragData.Add(node.Tag);
					if (dragData.Count > 0)
						treeList.DoDragDrop(new DataObject(DataFormats.Serializable, dragData.ToArray()), DragDropEffects.Copy);
					return;
				}
			}
			var hitPoint = new Point(e.X, e.Y);
			TreeListHitInfo hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
			if (hitInfo.Node != null)
				treeList.Cursor = Cursors.Hand;
			else
				treeList.Cursor = Cursors.Default;
		}
		#endregion

		#region Common Methods
		public void Init()
		{
			treeListAllFiles.SuspendLayout();
			treeListAllFiles.Nodes.Clear();
			pnTreeViewProgress.Visible = true;
			var thread = new Thread(delegate()
										{
											foreach (string drive in Directory.GetLogicalDrives())
											{
												FormMain.Instance.Invoke((MethodInvoker)delegate
																							{
																								var rootFolder = new DirectoryInfo(drive);
																								Application.DoEvents();
																								TreeListNode rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Name }, null, rootFolder);
																								rootNode.StateImageIndex = 0;
																								Application.DoEvents();
																								FillNode(rootNode, true);
																								Application.DoEvents();
																							});
											}
										});
			thread.Start();

			while (thread.IsAlive)
				Application.DoEvents();

			pnTreeViewProgress.Visible = false;
			treeListAllFiles.ResumeLayout();
			treeListAllFiles.Enabled = true;
		}

		private void ViewItem(FileInfo file)
		{
			try
			{
				Process.Start(file.FullName);
			}
			catch { }
		}
		#endregion
	}
}