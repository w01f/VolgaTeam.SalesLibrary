using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class WallBinTreeListControl : UserControl
    {
        private BusinessClasses.Library _parentLibrary = null;
        private List<CoreObjects.BusinessClasses.FolderLink> _rootFolders = new List<CoreObjects.BusinessClasses.FolderLink>();

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
                    foreach (CoreObjects.BusinessClasses.FolderLink rootFolder in _rootFolders)
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
                            if (hitInfo.Node.Tag.GetType() == typeof(CoreObjects.BusinessClasses.FileLink))
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
                                    foreach (CoreObjects.BusinessClasses.FolderLink rootFolder in _rootFolders)
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
                            if (hitInfo.Node.Tag.GetType() == typeof(CoreObjects.BusinessClasses.FolderLink))
                                FillNode(hitInfo.Node, false);
                            else if (hitInfo.Node.Tag.GetType() == typeof(CoreObjects.BusinessClasses.FileLink))
                                ViewItem(hitInfo.Node.Tag as CoreObjects.BusinessClasses.FileLink);
                        }
                        treeList.ResumeLayout();
                    }
                }
            }
        }

        private void tmiOpen_Click(object sender, EventArgs e)
        {
            CoreObjects.BusinessClasses.FileLink fileLink = null;
            switch (xtraTabControlFiles.SelectedTabPageIndex)
            {
                case 0:
                    if (treeListAllFiles.Selection.Count > 0)
                        fileLink = treeListAllFiles.Selection[0].Tag as CoreObjects.BusinessClasses.FileLink;
                    break;
                case 1:
                    if (treeListSearchFiles.Selection.Count > 0)
                        fileLink = treeListSearchFiles.Selection[0].Tag as CoreObjects.BusinessClasses.FileLink;
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
                CoreObjects.BusinessClasses.FolderLink folderLink = node.Tag as CoreObjects.BusinessClasses.FolderLink;
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
                                CoreObjects.BusinessClasses.FolderLink subFolderLink = new CoreObjects.BusinessClasses.FolderLink();
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
                        List<FileInfo> files = new List<FileInfo>();
                        files.AddRange(folderLink.Folder.GetFiles());
                        files.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                        foreach (FileInfo file in files)
                        {
                            if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => file.Name.ToLower().Contains(x.ToLower())).Count() == 0 && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
                            {
                                CoreObjects.BusinessClasses.FileLink fileLink = new CoreObjects.BusinessClasses.FileLink();
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
        #endregion

        #region Kew Word Files Tree View
        private void SearchFileInFolder(CoreObjects.BusinessClasses.FolderLink folderLink, string keyWord, List<CoreObjects.BusinessClasses.FileLink> files)
        {
            try
            {
                foreach (DirectoryInfo subFolder in folderLink.Folder.GetDirectories())
                    if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
                    {
                        CoreObjects.BusinessClasses.FolderLink subFolderLink = new CoreObjects.BusinessClasses.FolderLink();
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
                    if (((file.LastWriteTime >= dateEditStartDate.DateTime && file.LastWriteTime <= dateEditEndDate.DateTime) || !checkEditDateRange.Checked) && ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => file.FullName.ToLower().Contains(x.ToLower())).Count() == 0 && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
                    {
                        CoreObjects.BusinessClasses.FileLink fileLink = new CoreObjects.BusinessClasses.FileLink();
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

            List<CoreObjects.BusinessClasses.FileLink> files = new List<CoreObjects.BusinessClasses.FileLink>();
            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                foreach (CoreObjects.BusinessClasses.FolderLink folder in _rootFolders)
                    SearchFileInFolder(folder, textEditKeyWord.EditValue != null ? textEditKeyWord.EditValue.ToString() : string.Empty, files);
                if (files.Count > 0)
                {
                    files.Sort((x, y) => x.File.Name.CompareTo(y.File.Name));
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        foreach (CoreObjects.BusinessClasses.FileLink file in files)
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

            xtraTabPageRegular.Text = _parentLibrary.Parent.Name;

            _rootFolders.Clear();
            _rootFolders.AddRange(_parentLibrary.ExtraFolders);
            _rootFolders.Sort((x, y) => (x as CoreObjects.BusinessClasses.RootFolder).Order.CompareTo((y as CoreObjects.BusinessClasses.RootFolder).Order));
            _rootFolders.Insert(0, _parentLibrary.RootFolder);

            Refresh_Click(null, null);
            ckDateRange_CheckedChanged(null, null);
        }

        private void ViewItem(CoreObjects.BusinessClasses.FileLink file)
        {
            try
            {
                BusinessClasses.LibraryFile link = new BusinessClasses.LibraryFile(new CoreObjects.BusinessClasses.LibraryFolder(new CoreObjects.BusinessClasses.LibraryPage(_parentLibrary)));
                link.OriginalPath = file.File.FullName;
                link.SetProperties();
                BusinessClasses.LinkManager.Instance.OpenLink(link);
            }
            catch { }
        }
        #endregion

        #region Previewer
        private Viewers.IFileViewer _selectedFileViewer = null;

        private void UpdateViewAccordingFileType(BusinessClasses.LibraryFile file)
        {
            barButtonItemOpenLink.Enabled = false;
            barButtonItemSave.Enabled = false;
            barButtonItemEmailLink.Enabled = false;
            barButtonItemPrintLink.Enabled = false;
            if (file != null)
            {
                barButtonItemOpenLink.Enabled = true;
                switch (file.Type)
                {
                    case CoreObjects.BusinessClasses.FileTypes.BuggyPresentation:
                    case CoreObjects.BusinessClasses.FileTypes.FriendlyPresentation:
                    case CoreObjects.BusinessClasses.FileTypes.Presentation:
                        barButtonItemSave.Enabled = true;
                        barButtonItemEmailLink.Enabled = true;
                        barButtonItemPrintLink.Enabled = true;
                        break;
                    case CoreObjects.BusinessClasses.FileTypes.Excel:
                    case CoreObjects.BusinessClasses.FileTypes.PDF:
                    case CoreObjects.BusinessClasses.FileTypes.Word:
                        barButtonItemSave.Enabled = true;
                        barButtonItemEmailLink.Enabled = true;
                        barButtonItemPrintLink.Enabled = true;
                        break;
                    case CoreObjects.BusinessClasses.FileTypes.Other:
                        barButtonItemSave.Enabled = true;
                        barButtonItemEmailLink.Enabled = true;
                        barButtonItemPrintLink.Enabled = true;
                        break;
                    case CoreObjects.BusinessClasses.FileTypes.MediaPlayerVideo:
                    case CoreObjects.BusinessClasses.FileTypes.QuickTimeVideo:
                        barButtonItemEmailLink.Enabled = true;
                        break;
                    case CoreObjects.BusinessClasses.FileTypes.Url:
                        break;
                }
            }
        }

        private void treeList_SelectionChanged(object sender, EventArgs e)
        {
            if (_selectedFileViewer != null)
            {
                pnPreview.Controls.Clear();
                _selectedFileViewer.ReleaseResources();
                _selectedFileViewer = null;
            }
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList != null)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode node = treeList.Selection.Count > 0 ? treeList.Selection[0] : null;
                if (node != null && node.Tag != null)
                {
                    CoreObjects.BusinessClasses.FileLink file = node.Tag as CoreObjects.BusinessClasses.FileLink;
                    if (file != null)
                    {
                        BusinessClasses.LibraryFile libraryFile = new BusinessClasses.LibraryFile(new CoreObjects.BusinessClasses.LibraryFolder(new CoreObjects.BusinessClasses.LibraryPage(_parentLibrary)));
                        libraryFile.OriginalPath = file.File.FullName;
                        libraryFile.SetProperties();

                        using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                        {
                            form.laProgress.Text = "Loading preview...";
                            form.TopMost = true;
                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {

                                switch (libraryFile.Type)
                                {
                                    case CoreObjects.BusinessClasses.FileTypes.Excel:
                                        FormMain.Instance.Invoke((MethodInvoker)delegate()
                                        {
                                            try
                                            { _selectedFileViewer = new Viewers.ExcelViewer(libraryFile); }
                                            catch { _selectedFileViewer = new Viewers.DefaultViewer(libraryFile); }
                                        });
                                        break;
                                    case CoreObjects.BusinessClasses.FileTypes.Word:
                                        FormMain.Instance.Invoke((MethodInvoker)delegate()
                                        {
                                            try
                                            { _selectedFileViewer = new Viewers.WordViewer(libraryFile); }
                                            catch { _selectedFileViewer = new Viewers.DefaultViewer(libraryFile); }
                                        });
                                        break;
                                    case CoreObjects.BusinessClasses.FileTypes.PDF:
                                        FormMain.Instance.Invoke((MethodInvoker)delegate()
                                        {
                                            try
                                            { _selectedFileViewer = new Viewers.PDFViewer(libraryFile); }
                                            catch { _selectedFileViewer = new Viewers.DefaultViewer(libraryFile); }
                                        });
                                        break;
                                    case CoreObjects.BusinessClasses.FileTypes.MediaPlayerVideo:
                                        FormMain.Instance.Invoke((MethodInvoker)delegate()
                                        {
                                            try
                                            { _selectedFileViewer = new Viewers.VideoViewer(libraryFile); }
                                            catch { _selectedFileViewer = new Viewers.DefaultViewer(libraryFile); }
                                        });
                                        break;
                                    case CoreObjects.BusinessClasses.FileTypes.Url:
                                        FormMain.Instance.Invoke((MethodInvoker)delegate()
                                        {
                                            try
                                            { _selectedFileViewer = new Viewers.WebViewer(libraryFile); }
                                            catch { _selectedFileViewer = new Viewers.DefaultViewer(libraryFile); }
                                        });
                                        break;
                                    default:
                                        FormMain.Instance.Invoke((MethodInvoker)delegate() { _selectedFileViewer = new Viewers.DefaultViewer(libraryFile); });
                                        break;
                                }
                            }));

                            form.Show();
                            Application.DoEvents();

                            thread.Start();

                            while (thread.IsAlive)
                                Application.DoEvents();

                            FormMain.Instance.ribbonControl.Enabled = true;
                            form.Close();
                        }
                    }
                }
            }
            if (_selectedFileViewer == null)
                _selectedFileViewer = new Viewers.EmptyViewer(null);
            (_selectedFileViewer as Control).Visible = true;
            UpdateViewAccordingFileType(_selectedFileViewer.File);
            pnPreview.Controls.Add(_selectedFileViewer as Control);
        }

        #region Toolbar Buttons Clicks
        private void barButtonItemOpenLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Open();
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Save();
        }

        private void barButtonItemSaveAsPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Viewers.PowerPointViewer viewer = _selectedFileViewer as Viewers.PowerPointViewer;
            if (viewer != null)
                viewer.SaveAsPDF();
        }

        private void barButtonItemEmailLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Email();
        }

        private void barButtonItemPrintLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Print();
        }

        private void barButtonItemAddSlide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Viewers.PowerPointViewer viewer = _selectedFileViewer as Viewers.PowerPointViewer;
            if (viewer != null)
                viewer.InsertSlide();
        }

        private void barButtonItemOpenQuickView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Viewers.PowerPointViewer viewer = _selectedFileViewer as Viewers.PowerPointViewer;
            if (viewer != null)
                viewer.OpenInQuickView();
        }
        #endregion
        #endregion
    }
}
