using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.Cliparts
{
    public partial class ClipartTreeListControl : UserControl
    {
        private DevExpress.XtraTreeList.TreeListHitInfo dragStartHitInfo;

        public ClipartTreeListControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTreeViewProgressLable.Font = new Font(laTreeViewProgressLable.Font.FontFamily, laTreeViewProgressLable.Font.Size - 3, laTreeViewProgressLable.Font.Style);
                btRefresh.Font = new Font(btRefresh.Font.FontFamily, btRefresh.Font.Size - 2, btRefresh.Font.Style);
            }
        }

        #region TreeView Data Methods
        private void Refresh_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void treeListAllFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
                if (treeList != null)
                {
                    Point hitPoint = new Point(e.X, e.Y);
                    DevExpress.XtraTreeList.TreeListHitInfo hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
                    if (hitInfo.Node != null)
                    {
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
                }
            }
        }

        private void FillNode(DevExpress.XtraTreeList.Nodes.TreeListNode node, bool showFiles)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode childNode;
            if (node.Tag != null)
            {
                if (node.Tag.GetType() == typeof(DirectoryInfo))
                {
                    if (node.Nodes.Count == 0)
                    {
                        DirectoryInfo folder = (DirectoryInfo)node.Tag;
                        try
                        {
                            {
                                List<DirectoryInfo> folders = new List<DirectoryInfo>();
                                folders.AddRange(folder.GetDirectories());
                                folders.Sort((x, y) => SalesDepot.CoreObjects.InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                                foreach (DirectoryInfo subFolder in folders)
                                {
                                    if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
                                    {
                                        childNode = treeListAllFiles.AppendNode(new object[] { subFolder.Name }, node, subFolder);
                                        childNode.StateImageIndex = 0;
                                    }
                                }
                            }
                            if (showFiles)
                            {
                                List<FileInfo> files = new List<FileInfo>();
                                files.AddRange(folder.GetFiles());
                                files.Sort((x, y) => SalesDepot.CoreObjects.InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
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
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void treeList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.None)
            {
                DevExpress.XtraTreeList.TreeList tl = sender as DevExpress.XtraTreeList.TreeList;
                dragStartHitInfo = tl.CalcHitInfo(e.Location);
            }
        }

        private void treeList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList != null)
            {
                if (e.Button == MouseButtons.Left && dragStartHitInfo != null && dragStartHitInfo.Node != null)
                {
                    Size dragSize = SystemInformation.DragSize;
                    Rectangle dragRect = new Rectangle(new Point(dragStartHitInfo.MousePoint.X - dragSize.Width / 2,
                        dragStartHitInfo.MousePoint.Y - dragSize.Height / 2), dragSize);
                    if (!dragRect.Contains(e.Location))
                    {
                        List<object> dragData = new List<object>();
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in treeList.Selection)
                            if (node.Tag.GetType() == typeof(FileInfo))
                                dragData.Add(node.Tag);
                        if (dragData.Count > 0)
                            treeList.DoDragDrop(new DataObject(DataFormats.Serializable, (object)dragData.ToArray()), DragDropEffects.Copy);
                        return;
                    }
                }
                Point hitPoint = new Point(e.X, e.Y);
                DevExpress.XtraTreeList.TreeListHitInfo hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
                if (hitInfo.Node != null)
                    treeList.Cursor = Cursors.Hand;
                else
                    treeList.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Common Methods
        public void Init()
        {
            treeListAllFiles.SuspendLayout();
            treeListAllFiles.Nodes.Clear();
            laTreeViewProgressLable.Text = "Loading Tree View...";
            pnTreeViewProgress.Visible = true;
            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                foreach (string drive in Directory.GetLogicalDrives())
                {

                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        DirectoryInfo rootFolder = new DirectoryInfo(drive);
                        Application.DoEvents();
                        DevExpress.XtraTreeList.Nodes.TreeListNode rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Name }, null, rootFolder);
                        rootNode.StateImageIndex = 0;
                        Application.DoEvents();
                        FillNode(rootNode, true);
                        Application.DoEvents();
                    });
                }
            }));
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
