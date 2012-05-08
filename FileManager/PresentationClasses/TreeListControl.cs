using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileManager.CustomControls
{
    public partial class TreeListControl : UserControl
    {
        private DirectoryInfo _treeViewFolder;
        private DevExpress.XtraTreeList.TreeListHitInfo dragStartHitInfo;

        public TreeListControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
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

            DirectoryInfo rootFolder = _treeViewFolder;
            DevExpress.XtraTreeList.Nodes.TreeListNode rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Name }, null, rootFolder);
            rootNode.StateImageIndex = 0;
            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                FormMain.Instance.Invoke((MethodInvoker)delegate()
                {
                    FillNode(rootNode, true);
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
                            if (hitInfo.Node.Tag.GetType() == typeof(FileInfo))
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
                            treeList.Nodes.RemoveAt(1);
                            laTreeViewProgressLable.Text = "Loading Tree View...";
                            pnTreeViewProgress.Visible = true;
                            treeList.Enabled = false;

                            DirectoryInfo rootFolder = _treeViewFolder;
                            DevExpress.XtraTreeList.Nodes.TreeListNode rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Name }, null, rootFolder);
                            rootNode.StateImageIndex = 0;
                            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
                            {
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    FillNode(rootNode, true);
                                    ExpandAll(rootNode);
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

        private void tmiOpen_Click(object sender, EventArgs e)
        {
            switch (tcSalesDepotFiles.SelectedIndex)
            {
                case 0:
                    if (treeListAllFiles.Selection.Count > 0)
                    {
                        FileInfo file = treeListAllFiles.Selection[0].Tag as FileInfo;
                        if (file != null)
                            ViewItem(file);
                    }
                    break;
                case 1:
                    if (treeListSearchFiles.Selection.Count > 0)
                    {
                        FileInfo file = treeListSearchFiles.Selection[0].Tag as FileInfo;
                        if (file != null)
                            ViewItem(file);
                    }
                    break;
            }
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
                if (node.Tag.GetType() == typeof(DirectoryInfo))
                {
                    if (node.Nodes.Count == 0)
                    {
                        DirectoryInfo folder = (DirectoryInfo)node.Tag;
                        try
                        {
                            foreach (DirectoryInfo subFolder in folder.GetDirectories())
                            {
                                if (!subFolder.FullName.Contains("!SD-Graphics") && !subFolder.FullName.Contains(ConfigurationClasses.SettingsManager.PreviewFolderPrefix) && !subFolder.FullName.Contains("!Old"))
                                {
                                    childNode = treeListAllFiles.AppendNode(new object[] { subFolder.Name }, node, subFolder);
                                    childNode.StateImageIndex = 0;
                                }
                            }
                            if (showFiles)
                                foreach (FileInfo file in folder.GetFiles())
                                {
                                    if (!file.Name.ToLower().Equals("thumbs.db"))
                                    {
                                        childNode = treeListAllFiles.AppendNode(new object[] { file.Name + " (" + file.LastWriteTime.ToShortDateString() + " " + file.LastWriteTime.ToShortTimeString() + ")" }, node, file);
                                        childNode.StateImageIndex = GetImageindex(file);
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
                                if (node.Tag.GetType() == typeof(FileInfo) || node.Tag.GetType() == typeof(DirectoryInfo))
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
        private void SearchFileInFolder(DirectoryInfo folder, string keyWord, List<FileInfo> files)
        {
            try
            {
                foreach (DirectoryInfo subFolder in folder.GetDirectories())
                    if (!subFolder.Name.Equals("!SD-Graphics"))
                        SearchFileInFolder(subFolder, keyWord, files);
                foreach (FileInfo file in folder.GetFiles("*" + keyWord + "*.*"))
                {
                    if ((file.LastWriteTime >= dtStartDate.Value && file.LastWriteTime <= dtEndDate.Value) || !ckDateRange.Checked)
                        files.Add(file);
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

            List<FileInfo> files = new List<FileInfo>();
            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                SearchFileInFolder(_treeViewFolder, edKeyWord.Text, files);
                if (files.Count > 0)
                {
                    files.Sort((x, y) => x.Name.CompareTo(y.Name));
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        foreach (FileInfo file in files)
                        {
                            DevExpress.XtraTreeList.Nodes.TreeListNode childNode = treeListSearchFiles.AppendNode(new object[] { file.Name + " (" + file.LastWriteTime.ToShortDateString() + " " + file.LastWriteTime.ToShortTimeString() + ")" }, null, file);
                            childNode.StateImageIndex = GetImageindex(file);
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
        public void Init(DirectoryInfo rootFolder)
        {
            _treeViewFolder = rootFolder;
            Refresh_Click(null, null);
            ckDateRange_CheckedChanged(null, null);
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
