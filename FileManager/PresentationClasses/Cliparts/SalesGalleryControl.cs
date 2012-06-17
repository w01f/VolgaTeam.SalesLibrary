using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.Cliparts
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SalesGalleryControl : UserControl
    {
        private static SalesGalleryControl _instance = null;
        private bool _allowToPreviewImages = true;

        public static SalesGalleryControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SalesGalleryControl();
                return _instance;
            }
        }

        private SalesGalleryControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        #region Tree List methods
        public void InitTreeList()
        {
            treeListFiles.SuspendLayout();
            treeListFiles.Nodes.Clear();
            laTreeViewProgressLable.Text = "Loading Tree View...";
            pnTreeViewProgress.Visible = true;
            DevExpress.XtraTreeList.Nodes.TreeListNode rootNode = null;
            Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
            {
                FormMain.Instance.Invoke((MethodInvoker)delegate()
                {
                    if (Directory.Exists(ConfigurationClasses.SettingsManager.Instance.SalesGalleryRootPath))
                    {
                        DirectoryInfo rootFolder = new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.SalesGalleryRootPath);
                        rootNode = treeListFiles.AppendNode(new object[] { rootFolder.Name }, null, rootFolder);
                        rootNode.StateImageIndex = 0;
                        Application.DoEvents();
                        FillNode(rootNode, true);
                        rootNode.Expanded = true;
                        Application.DoEvents();
                    }
                });
            }));
            thread.Start();

            while (thread.IsAlive)
                Application.DoEvents();

            pnTreeViewProgress.Visible = false;
            treeListFiles.ResumeLayout();
            treeListFiles.Enabled = true;

            if (rootNode != null && rootNode.Nodes.Count > 0)
            {
                rootNode.Nodes[0].Selected = true;
            }
            imageListView.View = Manina.Windows.Forms.View.Pane;
            imageListView.View = Manina.Windows.Forms.View.Thumbnails;
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
                                if (ConfigurationClasses.SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
                                {
                                    childNode = treeListFiles.AppendNode(new object[] { subFolder.Name }, node, subFolder);
                                    childNode.StateImageIndex = 0;
                                    FillNode(childNode, true);
                                }
                            }
                            if (showFiles)
                                foreach (FileInfo file in folder.GetFiles())
                                {
                                    switch (file.Extension.ToLower())
                                    {
                                        case ".png":
                                        case ".jpg":
                                        case ".bmp":
                                        case ".gif":
                                        case ".tif":
                                        case ".wmf":
                                            childNode = treeListFiles.AppendNode(new object[] { file.Name }, node, file);
                                            childNode.StateImageIndex = 2;
                                            break;
                                    }
                                }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        #endregion

        #region Common Methods
        private void UpdateButtonStatus()
        {
            barButtonItemAddNode.Enabled = false;
            barButtonItemChangeNodeName.Enabled = false;
            barButtonItemDeleteNode.Enabled = false;
            if (treeListFiles.Selection.Count > 0)
            {
                DirectoryInfo selectedFolder = treeListFiles.Selection[0].Tag as DirectoryInfo;
                if (selectedFolder != null)
                {
                    barButtonItemAddNode.Enabled = true;
                    if (selectedFolder.FullName.ToLower().Equals(ConfigurationClasses.SettingsManager.Instance.SalesGalleryRootPath.ToLower()))
                    {
                        barButtonItemChangeNodeName.Enabled = false;
                        barButtonItemDeleteNode.Enabled = false;
                    }
                    else
                    {
                        barButtonItemChangeNodeName.Enabled = true;
                        barButtonItemDeleteNode.Enabled = true;
                    }
                }
                else
                {
                    barButtonItemChangeNodeName.Enabled = true;
                    barButtonItemDeleteNode.Enabled = true;
                }
            }
        }

        private void ClearViewArea()
        {
            Image img = pbPicture.Image;
            if (img != null)
                img.Dispose();
            pbPicture.Image = null;
            imageListView.Items.Clear();
            imageListView.ClearThumbnailCache();
        }
        #endregion

        #region Pictures Methods
        private void ShowThumbnails(DevExpress.XtraTreeList.Nodes.TreeListNode folderNode)
        {
            imageListView.SuspendLayout();
            List<string> imageFiles = new List<string>();
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in folderNode.Nodes)
            {
                FileInfo imageFile = childNode.Tag as FileInfo;
                if (imageFile != null)
                    imageFiles.Add(imageFile.FullName);
            }
            imageListView.Items.AddRange(imageFiles.ToArray());
            imageListView.ResumeLayout();
        }

        private void ShowPicture(DevExpress.XtraTreeList.Nodes.TreeListNode fileNode)
        {
            Image img = new Bitmap((fileNode.Tag as FileInfo).FullName);
            pbPicture.Tag = fileNode.Tag;
            double imageHeight = img.Height;
            double imageWidth = img.Width > pbPicture.Width ? pbPicture.Width : img.Width;
            if (img.Width > pbPicture.Width)
                img = ToolClasses.ImageHelper.GetThumbnail(img, (int)(imageHeight * (imageWidth / (double)img.Width)), (int)imageWidth);

            if (img.Height > xtraScrollableControlPicture.Height)
                pbPicture.Height = img.Height;
            else
                pbPicture.Height = xtraScrollableControlPicture.Height - 10;

            pbPicture.Image = img;
        }

        public bool ThumbnailCallback()
        {
            return false;
        }
        #endregion

        #region Tree List Event Handlers
        private void treeListFiles_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            UpdateButtonStatus();
        }

        private void treeListFiles_HiddenEditor(object sender, System.EventArgs e)
        {
            treeListFiles.OptionsBehavior.Editable = false;
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.FocusedNode;
            node.Selected = false;
            treeListFiles.FocusedNode = null;
            ClearViewArea();
            if (node.Tag.GetType() == typeof(DirectoryInfo))
            {
                DirectoryInfo oldFolder = node.Tag as DirectoryInfo;
                if (oldFolder != null)
                {
                    try
                    {
                        ToolClasses.SyncManager.Instance.MakeFolderAvailable(oldFolder);
                        string parentFolderPath = oldFolder.Parent.FullName;
                        string oldFoldeName = oldFolder.Name;
                        string newFoldeName = node.GetValue(treeListColumnName).ToString();
                        if (!oldFoldeName.Equals(newFoldeName))
                            Directory.Move(Path.Combine(parentFolderPath, oldFoldeName), Path.Combine(parentFolderPath, newFoldeName));
                        DirectoryInfo newFolder = new DirectoryInfo(Path.Combine(parentFolderPath, newFoldeName));
                        node.Tag = newFolder;
                        node.Nodes.Clear();
                        FillNode(node, true);
                    }
                    catch (IOException)
                    {
                        AppManager.Instance.ShowWarning("Couldn't rename folder.\nIt is busy");
                        node.SetValue(treeListColumnName, oldFolder.Name);
                    }
                    catch
                    {
                        AppManager.Instance.ShowWarning("Couldn't rename folder.");
                        node.SetValue(treeListColumnName, oldFolder.Name);
                    }
                }
            }
            else if (node.Tag.GetType() == typeof(FileInfo))
            {
                FileInfo currentFile = node.Tag as FileInfo;
                if (currentFile != null)
                {
                    try
                    {
                        string fileExtension = currentFile.Extension;
                        string newFilePath = Path.Combine(currentFile.DirectoryName, node.GetValue(treeListColumnName).ToString() + fileExtension);
                        if (!currentFile.FullName.Equals(newFilePath))
                            File.Move(currentFile.FullName, newFilePath);
                        currentFile = new FileInfo(newFilePath);
                        node.Tag = currentFile;
                    }
                    catch (IOException)
                    {
                        AppManager.Instance.ShowWarning("Couldn't rename file.\nIt is busy");
                    }
                    catch
                    {
                        AppManager.Instance.ShowWarning("Couldn't rename file.");
                    }
                    finally
                    {
                        node.SetValue(treeListColumnName, currentFile.Name);
                    }
                }
            }
            node.Selected = true;
            treeListFiles.FocusedNode = node;
        }

        private void treeListFiles_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            e.Node.StateImageIndex = 0;
        }

        private void treeListFiles_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            e.Node.StateImageIndex = 1;
        }

        private void treeListFiles_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.FocusedNode;
            if (_allowToPreviewImages && node != null)
            {
                ClearViewArea();
                imageListView.Visible = false;
                pbPicture.Visible = false;
                if (node.Tag != null)
                {
                    if (node.Tag.GetType() == typeof(DirectoryInfo))
                    {
                        imageListView.Visible = true;
                        if (!(node.Nodes.Count > 0))
                            FillNode(node, true);
                        ShowThumbnails(node);
                    }
                    else if (node.Tag.GetType() == typeof(FileInfo))
                    {
                        pbPicture.Visible = true;
                        ShowPicture(node);
                    }
                }
            }
        }

        private void treeListFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.CalcHitInfo(e.Location).Node;
            if (node != null)
                barButtonItemChangeNodeName_ItemClick(null, null);
        }
        #endregion

        #region Drag&Drop Event Handlers
        private void treeListFiles_DragEnter(object sender, DragEventArgs e)
        {
            _allowToPreviewImages = false;
        }

        private void treeListFiles_DragLeave(object sender, System.EventArgs e)
        {
            _allowToPreviewImages = true;
        }
        private void treeListFiles_DragOver(object sender, DragEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.CalcHitInfo(treeListFiles.PointToClient(new Point(e.X, e.Y))).Node;
            if (node != null && e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                e.Effect = DragDropEffects.Copy;
                _allowToPreviewImages = false;
                if (node.Tag.GetType() == typeof(DirectoryInfo))
                {
                    node.Expanded = true;
                    treeListFiles.FocusedNode = node;
                    node.Selected = true;
                }
                else
                {
                    node.ParentNode.Selected = true;
                    treeListFiles.FocusedNode = node.ParentNode;
                }
                return;
            }
            e.Effect = DragDropEffects.None;
            _allowToPreviewImages = true;
        }

        private void treeListFiles_DragDrop(object sender, DragEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.CalcHitInfo(treeListFiles.PointToClient(new Point(e.X, e.Y))).Node;
            if (node != null && e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                if (node.Tag.GetType() != typeof(DirectoryInfo))
                    node = node.ParentNode;
                DirectoryInfo parentFolder = node.Tag as DirectoryInfo;
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data != null && parentFolder != null)
                {
                    foreach (object dragItem in (object[])data)
                    {
                        FileInfo sourceFile = dragItem as FileInfo;
                        if (sourceFile != null)
                        {
                            sourceFile = sourceFile.CopyTo(Path.Combine(parentFolder.FullName, sourceFile.Name), true);
                            DevExpress.XtraTreeList.Nodes.TreeListNode childNode = treeListFiles.AppendNode(new object[] { sourceFile.Name }, node, sourceFile);
                            childNode.StateImageIndex = 2;
                            node.Expanded = true;
                            _allowToPreviewImages = true;
                            childNode.Selected = true;
                            treeListFiles.FocusedNode = childNode;
                        }
                    }
                }
            }
        }
        #endregion

        #region Tool Buttons Clicks
        private void barButtonItemAddNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeListFiles.Selection.Count > 0)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.Selection[0];
                if (treeListFiles.Selection[0].Tag.GetType() == typeof(DirectoryInfo))
                {
                    DirectoryInfo parentFolder = treeListFiles.Selection[0].Tag as DirectoryInfo;
                    int newFolderIndex = parentFolder.GetDirectories("New Folder*").Length + 1;
                    string newFoldeName = "New Folder" + (newFolderIndex > 1 ? newFolderIndex.ToString() : string.Empty);
                    if (!Directory.Exists(Path.Combine(parentFolder.FullName, newFoldeName)))
                        Directory.CreateDirectory(Path.Combine(parentFolder.FullName, newFoldeName));
                    DirectoryInfo childFolder = new DirectoryInfo(Path.Combine(parentFolder.FullName, newFoldeName));
                    DevExpress.XtraTreeList.Nodes.TreeListNode childNode = treeListFiles.AppendNode(new object[] { childFolder.Name }, node, childFolder);
                    childNode.StateImageIndex = 0;
                    node.Expanded = true;
                    childNode.Selected = true;
                    treeListFiles.FocusedNode = childNode;
                }
            }
        }

        private void barButtonItemChangeNodeName_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.FocusedNode;
            if (node != null)
            {
                FileInfo file = node.Tag as FileInfo;
                if (file != null)
                    node.SetValue(treeListColumnName, Path.GetFileNameWithoutExtension(file.FullName));
                treeListFiles.OptionsBehavior.Editable = true;
                treeListFiles.ShowEditor();
            }
        }

        private void barButtonItemDeleteNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeListFiles.Selection.Count > 0)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.Selection[0];
                if (node.Tag.GetType() == typeof(DirectoryInfo))
                {
                    if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete selected folder?") == DialogResult.Yes)
                    {
                        ClearViewArea();
                        ToolClasses.SyncManager.Instance.DeleteFolder(node.Tag as DirectoryInfo);
                        node.ParentNode.Nodes.Remove(node);
                    }
                }
                else if (node.Tag.GetType() == typeof(FileInfo))
                {
                    if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete selected file?") == DialogResult.Yes)
                    {
                        ClearViewArea();
                        (node.Tag as FileInfo).Attributes = FileAttributes.Normal;
                        (node.Tag as FileInfo).Delete();
                        node.ParentNode.Nodes.Remove(node);
                    }
                }
            }
        }
        #endregion

        #region Common Event Handlers
        private void imageListView_ItemDoubleClick(object sender, Manina.Windows.Forms.ItemClickEventArgs e)
        {
            string filePath = Path.Combine(e.Item.FilePath, e.Item.FileName);
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeListFiles.FocusedNode;
            if (node != null && node.Tag.GetType() == typeof(DirectoryInfo) && File.Exists(filePath))
            {
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in node.Nodes)
                {
                    FileInfo file = childNode.Tag as FileInfo;
                    if (file != null && file.FullName.Equals(filePath))
                    {
                        childNode.Selected = true;
                        treeListFiles.FocusedNode = childNode;
                        break;
                    }
                }
            }
        }

        private void pnPictures_MouseMove(object sender, MouseEventArgs e)
        {
            xtraScrollableControlPicture.Focus();
        }

        private void pnPictures_Resize(object sender, EventArgs e)
        {
            treeListFiles_AfterFocusNode(null, null);
        }
        #endregion
    }
}
