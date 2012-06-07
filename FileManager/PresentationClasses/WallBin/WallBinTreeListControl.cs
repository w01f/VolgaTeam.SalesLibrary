using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.WallBin
{
    public partial class WallBinTreeListControl : UserControl
    {
        private List<BusinessClasses.FolderLink> _rootFolders = new List<BusinessClasses.FolderLink>();
        private DevExpress.XtraTreeList.TreeListHitInfo dragStartHitInfo;

        public WallBinTreeListControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laDoubleClick.Font = new Font(laDoubleClick.Font.FontFamily, laDoubleClick.Font.Size - 3, laDoubleClick.Font.Style);
                laEndDate.Font = new Font(laEndDate.Font.FontFamily, laEndDate.Font.Size - 2, laEndDate.Font.Style);
                laStartDate.Font = new Font(laStartDate.Font.FontFamily, laStartDate.Font.Size - 2, laStartDate.Font.Style);
                laTreeViewProgressLable.Font = new Font(laTreeViewProgressLable.Font.FontFamily, laTreeViewProgressLable.Font.Size - 3, laTreeViewProgressLable.Font.Style);
                ckDateRange.Font = new Font(ckDateRange.Font.FontFamily, ckDateRange.Font.Size - 2, ckDateRange.Font.Style);
                btRefresh.Font = new Font(btRefresh.Font.FontFamily, btRefresh.Font.Size - 2, btRefresh.Font.Style);
                btSearch.Font = new Font(btSearch.Font.FontFamily, btSearch.Font.Size - 2, btSearch.Font.Style);
            }
        }

        #region TreeView Data Methods
        private void Refresh_Click(object sender, EventArgs e)
        {
            treeListAllFiles.SuspendLayout();
            laTreeViewProgressLable.Text = "Loading Tree View...";
            pnTreeViewProgress.Visible = true;
            tcSalesDepotFiles.Enabled = false;

            treeListAllFiles.Nodes.Clear();
            DevExpress.XtraTreeList.Nodes.TreeListNode expandNode = treeListAllFiles.AppendNode(new object[] { "Expand All" }, null);
            expandNode.StateImageIndex = 0;

            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                FormMain.Instance.Invoke((MethodInvoker)delegate()
                {
                    foreach (BusinessClasses.FolderLink rootFolder in _rootFolders)
                    {
                        DevExpress.XtraTreeList.Nodes.TreeListNode rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Folder.Name }, null, rootFolder);
                        rootNode.StateImageIndex = 0;
                        FillNode(rootNode, true);
                        Application.DoEvents();
                    }
                });
            }));
            thread.Start();

            while (thread.IsAlive)
                Application.DoEvents();

            tcSalesDepotFiles.Enabled = true;
            pnTreeViewProgress.Visible = false;
            treeListAllFiles.ResumeLayout();
            treeListAllFiles.Enabled = true;
        }

        private void treeListAllFiles_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
                if (treeList != null)
                {
                    Point hitPoint = new Point(e.X, e.Y);
                    DevExpress.XtraTreeList.TreeListHitInfo hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
                    if (hitInfo.Node != null)
                        if (hitInfo.Node.Tag != null)
                            if (hitInfo.Node.Tag.GetType() == typeof(BusinessClasses.FileLink))
                            {
                                treeListAllFiles.Selection.Clear();
                                hitInfo.Node.Selected = true;
                                contextMenuStrip.Show(treeListAllFiles, hitPoint);
                            }
                }
            }
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
                        if (hitInfo.Node.GetValue(treeListColumnName).Equals("Expand All"))
                        {
                            List<DevExpress.XtraTreeList.Nodes.TreeListNode> nodesToDelete = new List<DevExpress.XtraTreeList.Nodes.TreeListNode>();
                            for (int i = 1; i < treeList.Nodes.Count; i++)
                                nodesToDelete.Add(treeList.Nodes[i]);
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in nodesToDelete)
                                treeList.Nodes.Remove(node);

                            laTreeViewProgressLable.Text = "Loading Tree View...";
                            pnTreeViewProgress.Visible = true;
                            treeList.Enabled = false;

                            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
                            {
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    foreach (BusinessClasses.FolderLink rootFolder in _rootFolders)
                                    {
                                        DevExpress.XtraTreeList.Nodes.TreeListNode rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Folder.Name }, null, rootFolder);
                                        rootNode.StateImageIndex = 0;
                                        FillNode(rootNode, true);
                                        Application.DoEvents();
                                    }
                                });
                            }));
                            thread.Start();

                            while (thread.IsAlive)
                                Application.DoEvents();
                            pnTreeViewProgress.Visible = false;
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
                            if (hitInfo.Node.Tag.GetType() == typeof(BusinessClasses.FolderLink))
                                FillNode(hitInfo.Node, true);
                            else if (hitInfo.Node.Tag.GetType() == typeof(BusinessClasses.FileLink))
                                ViewItem(hitInfo.Node.Tag as BusinessClasses.FileLink);
                        }
                        treeList.ResumeLayout();
                    }
                }
            }
        }

        private void tmiOpen_Click(object sender, EventArgs e)
        {
            BusinessClasses.FileLink fileLink = null;
            switch (tcSalesDepotFiles.SelectedIndex)
            {
                case 0:
                    if (treeListAllFiles.Selection.Count > 0)
                        fileLink = treeListAllFiles.Selection[0].Tag as BusinessClasses.FileLink;
                    break;
                case 1:
                    if (treeListSearchFiles.Selection.Count > 0)
                        fileLink = treeListSearchFiles.Selection[0].Tag as BusinessClasses.FileLink;
                    break;
            }
            if (fileLink != null)
                ViewItem(fileLink);
        }

        private void ExpandAll(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in node.Nodes)
            {
                if (childNode.Nodes.Count > 0)
                {
                    Application.DoEvents();
                    ExpandAll(childNode);
                    Application.DoEvents();
                    childNode.Expanded = true;
                }
                else if (!node.GetValue(treeListColumnName).Equals("Expand All") && !node.GetValue(treeListColumnName).Equals("Collapse All"))
                {
                    FillNode(childNode, true);
                    if (node.Nodes.Count > 0)
                    {
                        Application.DoEvents();
                        ExpandAll(childNode);
                        Application.DoEvents();
                        childNode.Expanded = true;
                    }
                }
            }
        }

        private void Expand(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in node.Nodes)
            {
                if (childNode.Nodes.Count > 0)
                    childNode.Expanded = true;
                else if (!node.GetValue(treeListColumnName).Equals("Expand All") && !node.GetValue(treeListColumnName).Equals("Collapse All"))
                    FillNode(childNode, true);
            }
        }

        private void FillNode(DevExpress.XtraTreeList.Nodes.TreeListNode node, bool showFiles)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode childNode;
            if (node.Tag != null)
            {
                BusinessClasses.FolderLink folderLink = node.Tag as BusinessClasses.FolderLink;
                if (folderLink != null && node.Nodes.Count == 0)
                {
                    try
                    {
                        {
                            List<DirectoryInfo> folders = new List<DirectoryInfo>();
                            folders.AddRange(folderLink.Folder.GetDirectories());
                            folders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                            foreach (DirectoryInfo subFolder in folders)
                            {
                                if (ConfigurationClasses.SettingsManager.Instance.HiddenFolders.Where(x => subFolder.FullName.Contains(x)).Count() == 0)
                                {
                                    BusinessClasses.FolderLink subFolderLink = new BusinessClasses.FolderLink();
                                    subFolderLink.RootId = folderLink.RootId;
                                    subFolderLink.Folder = subFolder;
                                    childNode = treeListAllFiles.AppendNode(new object[] { subFolder.Name }, node, subFolderLink);
                                    childNode.StateImageIndex = 0;
                                }
                            }
                        }
                        if (showFiles)
                        {
                            List<FileInfo> files = new List<FileInfo>();
                            files.AddRange(folderLink.Folder.GetFiles());
                            files.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                            foreach (FileInfo file in files)
                            {
                                if (!file.Name.ToLower().Equals("thumbs.db"))
                                {
                                    BusinessClasses.FileLink fileLink = new BusinessClasses.FileLink();
                                    fileLink.RootId = folderLink.RootId;
                                    fileLink.File = file;
                                    childNode = treeListAllFiles.AppendNode(new object[] { file.Name + " (" + file.LastWriteTime.ToShortDateString() + " " + file.LastWriteTime.ToShortTimeString() + ")" }, node, fileLink);
                                    childNode.StateImageIndex = GetImageindex(file);
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
                default:
                    return 2;
            }
        }

        private string GetFileFormat(FileInfo file)
        {
            switch (file.Extension.ToUpper())
            {
                case ".XLS":
                case ".XLSX":
                case ".XLT":
                case ".XLTX":
                    return "Excel File";
                case ".BMP":
                case ".JPG":
                case ".JPEG":
                case ".PNG":
                case ".GIF":
                case ".TIF":
                case ".TIFF":
                case ".ICO":
                    return "Image File";
                case ".PDF":
                    return "Adobe PDF";
                case ".PPT":
                case ".PPTX":
                    return "PowerPoint File";
                case ".MPEG":
                case ".MPG":
                case ".WMV":
                case ".ASF":
                case ".AVI":
                case ".MOV":
                case ".MP4":
                case ".M4V":
                    return "Video Clip";
                case ".URL":
                    return "Website Link";
                case ".DOC":
                case ".DOCX":
                    return "Word Document";
                default:
                    return "Sales Depot File";
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
                            if (!node.GetValue(treeListColumnName).Equals("Expand All") && !node.GetValue(treeListColumnName).Equals("Collapse All"))
                                if (node.Tag.GetType() == typeof(BusinessClasses.FileLink) || node.Tag.GetType() == typeof(BusinessClasses.FolderLink))
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

        #region Kew Word Files Tree View
        private void SearchFileInFolder(BusinessClasses.FolderLink folderLink, string keyWord, List<BusinessClasses.FileLink> files)
        {
            try
            {
                foreach (DirectoryInfo subFolder in folderLink.Folder.GetDirectories())
                    if (ConfigurationClasses.SettingsManager.Instance.HiddenFolders.Where(x => subFolder.FullName.Contains(x)).Count() == 0)
                    {
                        BusinessClasses.FolderLink subFolderLink = new BusinessClasses.FolderLink();
                        subFolderLink.RootId = folderLink.RootId;
                        subFolderLink.Folder = subFolder;
                        SearchFileInFolder(subFolderLink, keyWord, files);
                    }
                foreach (FileInfo file in folderLink.Folder.GetFiles("*" + keyWord + "*.*"))
                {
                    if ((file.LastWriteTime >= dtStartDate.Value && file.LastWriteTime <= dtEndDate.Value) || !ckDateRange.Checked)
                    {
                        BusinessClasses.FileLink fileLink = new BusinessClasses.FileLink();
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
            laTreeViewProgressLable.Text = "Searching Files...";
            pnTreeViewProgress.Visible = true;
            tcSalesDepotFiles.Enabled = false;

            List<BusinessClasses.FileLink> files = new List<BusinessClasses.FileLink>();
            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                foreach (BusinessClasses.FolderLink folder in _rootFolders)
                    SearchFileInFolder(folder, edKeyWord.Text, files);
                if (files.Count > 0)
                {
                    files.Sort((x, y) => x.File.Name.CompareTo(y.File.Name));
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        foreach (BusinessClasses.FileLink file in files)
                        {
                            DevExpress.XtraTreeList.Nodes.TreeListNode childNode = treeListSearchFiles.AppendNode(new object[] { file.File.Name + " (" + file.File.LastWriteTime.ToShortDateString() + " " + file.File.LastWriteTime.ToShortTimeString() + ")" }, null, file);
                            childNode.StateImageIndex = GetImageindex(file.File);
                        }
                    });
                }
            }));
            thread.Start();

            while (thread.IsAlive)
                Application.DoEvents();

            pnTreeViewProgress.Visible = false;
            treeListSearchFiles.ResumeLayout();
            tcSalesDepotFiles.Enabled = true;
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
            if (ckDateRange.Checked)
                pnKeyWord.Height = gbDateRange.Bottom + 4;
            else
                pnKeyWord.Height = gbDateRange.Top + 4;
        }
        #endregion

        #region Other GUI Events
        private void tcSalesDepotFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcSalesDepotFiles.SelectedIndex)
            {
                case 0:
                    treeListSearchFiles.Selection.Clear();
                    break;
                case 1:
                    treeListAllFiles.Selection.Clear();
                    break;
            }
        }
        #endregion

        #region Common Methods
        public void Init(BusinessClasses.FolderLink[] rootFolders)
        {
            _rootFolders.Clear();
            _rootFolders.AddRange(rootFolders);
            Refresh_Click(null, null);
            ckDateRange_CheckedChanged(null, null);
        }

        private void ViewItem(BusinessClasses.FileLink file)
        {
            try
            {
                Process.Start(file.File.FullName);
            }
            catch { }
        }
        #endregion
    }
}
