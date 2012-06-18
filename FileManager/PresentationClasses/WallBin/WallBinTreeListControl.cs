using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class WallBinTreeListControl : UserControl
    {
        private BusinessClasses.Library _parentLibrary = null;
        private List<BusinessClasses.FolderLink> _rootFolders = new List<BusinessClasses.FolderLink>();
        private DevExpress.XtraTreeList.TreeListHitInfo _dragStartHitInfo;

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
                checkEditDateRange.Font = new Font(checkEditDateRange.Font.FontFamily, checkEditDateRange.Font.Size - 2, checkEditDateRange.Font.Style);
                simpleButtonRefresh.Font = new Font(simpleButtonRefresh.Font.FontFamily, simpleButtonRefresh.Font.Size - 2, simpleButtonRefresh.Font.Style);
                simpleButtonSearch.Font = new Font(simpleButtonSearch.Font.FontFamily, simpleButtonSearch.Font.Size - 2, simpleButtonSearch.Font.Style);
            }
        }

        #region TreeView Data Methods
        private void Refresh_Click(object sender, EventArgs e)
        {
            treeListAllFiles.SuspendLayout();
            laTreeViewProgressLable.Text = "Loading Tree View...";
            pnTreeViewProgress.Visible = true;
            circularProgress.IsRunning = true;
            xtraTabControlFiles.Enabled = false;

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
                        FillNode(rootNode, false);
                        Application.DoEvents();
                    }
                });
            }));
            thread.Start();

            while (thread.IsAlive)
                Application.DoEvents();

            xtraTabControlFiles.Enabled = true;
            circularProgress.IsRunning = false;
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
                    DevExpress.XtraTreeList.TreeListHitInfo hitInfo = treeList.CalcHitInfo(hitPoint);
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
                            circularProgress.IsRunning = true;
                            treeList.Enabled = false;

                            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
                            {
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    foreach (BusinessClasses.FolderLink rootFolder in _rootFolders)
                                    {
                                        DevExpress.XtraTreeList.Nodes.TreeListNode rootNode = treeList.AppendNode(new object[] { rootFolder.Folder.Name }, null, rootFolder);
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
                            circularProgress.IsRunning = false;
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
                                FillNode(hitInfo.Node, false);
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
            switch (xtraTabControlFiles.SelectedTabPageIndex)
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

        private void FillNode(DevExpress.XtraTreeList.Nodes.TreeListNode node, bool showSubItems)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode childNode;
            if (node.Tag != null)
            {
                BusinessClasses.FolderLink folderLink = node.Tag as BusinessClasses.FolderLink;
                if (folderLink != null && node.Nodes.Count == 0)
                {
                    try
                    {
                        List<DirectoryInfo> folders = new List<DirectoryInfo>();
                        folders.AddRange(folderLink.Folder.GetDirectories());
                        folders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                        foreach (DirectoryInfo subFolder in folders)
                        {
                            if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
                            {
                                BusinessClasses.FolderLink subFolderLink = new BusinessClasses.FolderLink();
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
                        List<FileInfo> files = new List<FileInfo>();
                        files.AddRange(folderLink.Folder.GetFiles());
                        files.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                        foreach (FileInfo file in files)
                        {
                            if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => file.Name.ToLower().Contains(x.ToLower())).Count() == 0 && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
                            {
                                BusinessClasses.FileLink fileLink = new BusinessClasses.FileLink();
                                fileLink.RootId = folderLink.RootId;
                                fileLink.File = file;
                                childNode = treeListAllFiles.AppendNode(new object[] { file.Name + " (" + file.LastWriteTime.ToString("MM/dd/yy hh:mm tt") + ")" }, node, fileLink);
                                childNode.StateImageIndex = GetImageindex(file);
                            }
                            Application.DoEvents();
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
                _dragStartHitInfo = tl.CalcHitInfo(e.Location);
            }
        }

        private void treeList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList != null)
            {
                if (e.Button == MouseButtons.Left && _dragStartHitInfo != null && _dragStartHitInfo.Node != null)
                {
                    Size dragSize = SystemInformation.DragSize;
                    Rectangle dragRect = new Rectangle(new Point(_dragStartHitInfo.MousePoint.X - dragSize.Width / 2,
                        _dragStartHitInfo.MousePoint.Y - dragSize.Height / 2), dragSize);
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
                    if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
                    {
                        BusinessClasses.FolderLink subFolderLink = new BusinessClasses.FolderLink();
                        subFolderLink.RootId = folderLink.RootId;
                        subFolderLink.Folder = subFolder;
                        SearchFileInFolder(subFolderLink, keyWord, files);
                    }
                foreach (FileInfo file in folderLink.Folder.GetFiles("*" + keyWord + "*.*"))
                {
                    if (((file.LastWriteTime >= dateEditStartDate.DateTime && file.LastWriteTime <= dateEditEndDate.DateTime) || !checkEditDateRange.Checked) && ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => file.FullName.ToLower().Contains(x.ToLower())).Count() == 0 && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
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
            circularProgress.IsRunning = true;
            xtraTabControlFiles.Enabled = false;

            List<BusinessClasses.FileLink> files = new List<BusinessClasses.FileLink>();
            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                foreach (BusinessClasses.FolderLink folder in _rootFolders)
                    SearchFileInFolder(folder, textEditKeyWord.EditValue != null ? textEditKeyWord.EditValue.ToString() : string.Empty, files);
                if (files.Count > 0)
                {
                    files.Sort((x, y) => x.File.Name.CompareTo(y.File.Name));
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        foreach (BusinessClasses.FileLink file in files)
                        {
                            DevExpress.XtraTreeList.Nodes.TreeListNode childNode = treeListSearchFiles.AppendNode(new object[] { file.File.Name + " (" + file.File.LastWriteTime.ToShortDateString() + " " + file.File.LastWriteTime.ToShortTimeString() + ")" }, null, file);
                            childNode.StateImageIndex = GetImageindex(file.File);
                            Application.DoEvents();
                        }
                    });
                }
            }));
            thread.Start();

            while (thread.IsAlive)
                Application.DoEvents();

            pnTreeViewProgress.Visible = false;
            circularProgress.IsRunning = false;
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
                pnKeyWord.Height = gbDateRange.Bottom + 4;
            else
                pnKeyWord.Height = gbDateRange.Top + 4;
        }
        #endregion

        #region Other GUI Events
        private void xtraTabControlFiles_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtraTabControlFiles.SelectedTabPageIndex)
            {
                case 0:
                    treeListSearchFiles.Selection.Clear();
                    break;
                case 1:
                    treeListAllFiles.Selection.Clear();
                    simpleButtonSearch.Refresh();
                    break;
            }
        }
        #endregion

        #region Common Methods
        public void Init(BusinessClasses.Library library)
        {
            _parentLibrary = library;

            splitContainerControl.PanelVisibility = _parentLibrary.UseDirectAccess ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

            _rootFolders.Clear();
            _rootFolders.AddRange(_parentLibrary.ExtraFolders);
            _rootFolders.Sort((x, y) => (x as BusinessClasses.RootFolder).Order.CompareTo((y as BusinessClasses.RootFolder).Order));
            _rootFolders.Insert(0, _parentLibrary.RootFolder);

            UpdateStatistic();

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

        private void UpdateStatistic()
        {
            List<DirectoryInfo> folders = new List<DirectoryInfo>();
            List<FileInfo> files = new List<FileInfo>();
            foreach (DirectoryInfo folder in _rootFolders.Select(x => x.Folder))
            {
                DirectoryInfo[] childFolder = GetFolders(folder);
                if (childFolder.Length > 0 || folder.GetFiles().Where(x => x.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate).Count() > 0)
                {
                    folders.AddRange(childFolder);
                    folders.Add(folder);
                }
                files.AddRange(GetFiles(folder));
            }
            labelControlTotalFolders.Text = string.Format("Total folders: {0}", folders.Count.ToString("# ##0"));

            files.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Extension, y.Extension));
            labelControlFiles.Text = string.Empty;
            StringBuilder text = new StringBuilder();
            foreach (string extension in files.Select(x => x.Extension.ToLower()).Distinct())
            {
                text.AppendLine(string.Format("{0}: {1}", new string[] { extension.Replace(".", string.Empty), files.Where(x => x.Extension.ToLower().Equals(extension)).Count().ToString("# ##0") }));
                text.AppendLine(string.Empty);
            }
            labelControlFiles.Text = text.ToString();
        }

        private FileInfo[] GetFiles(DirectoryInfo folder)
        {
            List<FileInfo> files = new List<FileInfo>();
            foreach (DirectoryInfo subFolder in folder.GetDirectories())
                if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
                    files.AddRange(GetFiles(subFolder));
            files.AddRange(folder.GetFiles().Where(x => x.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate && ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(y => x.FullName.ToLower().Contains(y.ToLower())).Count() == 0));
            return files.ToArray();
        }

        private DirectoryInfo[] GetFolders(DirectoryInfo folder)
        {
            List<DirectoryInfo> folders = new List<DirectoryInfo>();
            foreach (DirectoryInfo subFolder in folder.GetDirectories())
            {
                if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
                {
                    DirectoryInfo[] childFolder = GetFolders(subFolder);
                    if (childFolder.Length > 0 || subFolder.GetFiles().Where(x => x.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate && ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(y => x.FullName.ToLower().Contains(y.ToLower())).Count() == 0).Count() > 0)
                    {
                        folders.AddRange(childFolder);
                        folders.Add(subFolder);
                    }
                }
            }
            return folders.ToArray();
        }
        #endregion
    }
}
