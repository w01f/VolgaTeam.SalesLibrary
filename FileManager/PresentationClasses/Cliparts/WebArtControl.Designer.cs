namespace FileManager.PresentationClasses.Cliparts
{
    partial class WebArtControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebArtControl));
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnTreeList = new System.Windows.Forms.Panel();
			this.treeListFiles = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumnPath = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.imageListFiles = new System.Windows.Forms.ImageList(this.components);
			this.pnTreeViewProgress = new System.Windows.Forms.Panel();
			this.laTreeViewProgressLabel = new System.Windows.Forms.Label();
			this.circularProgressTreeView = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.pnPictures = new System.Windows.Forms.Panel();
			this.xtraScrollableControlPicture = new DevExpress.XtraEditors.XtraScrollableControl();
			this.imageListView = new Manina.Windows.Forms.ImageListView();
			this.pbPicture = new System.Windows.Forms.PictureBox();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barToolButtons = new DevExpress.XtraBars.Bar();
			this.barButtonItemAddNode = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemChangeNodeName = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemDeleteNode = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.pnTreeList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListFiles)).BeginInit();
			this.pnTreeViewProgress.SuspendLayout();
			this.pnPictures.SuspendLayout();
			this.xtraScrollableControlPicture.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControl.Panel1.Controls.Add(this.pnTreeList);
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControl.Panel2.Controls.Add(this.pnPictures);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(640, 475);
			this.splitContainerControl.SplitterPosition = 264;
			this.splitContainerControl.TabIndex = 0;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// pnTreeList
			// 
			this.pnTreeList.BackColor = System.Drawing.Color.Transparent;
			this.pnTreeList.Controls.Add(this.treeListFiles);
			this.pnTreeList.Controls.Add(this.pnTreeViewProgress);
			this.pnTreeList.Controls.Add(this.barDockControlLeft);
			this.pnTreeList.Controls.Add(this.barDockControlRight);
			this.pnTreeList.Controls.Add(this.barDockControlBottom);
			this.pnTreeList.Controls.Add(this.barDockControlTop);
			this.pnTreeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnTreeList.Location = new System.Drawing.Point(0, 0);
			this.pnTreeList.Name = "pnTreeList";
			this.pnTreeList.Size = new System.Drawing.Size(260, 471);
			this.pnTreeList.TabIndex = 0;
			// 
			// treeListFiles
			// 
			this.treeListFiles.AllowDrop = true;
			this.treeListFiles.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListFiles.Appearance.FocusedCell.Options.UseFont = true;
			this.treeListFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListFiles.Appearance.FocusedRow.Options.UseFont = true;
			this.treeListFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeListFiles.Appearance.Row.Options.UseFont = true;
			this.treeListFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListFiles.Appearance.SelectedRow.Options.UseFont = true;
			this.treeListFiles.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName,
            this.treeListColumnPath});
			this.treeListFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListFiles.Location = new System.Drawing.Point(0, 29);
			this.treeListFiles.Name = "treeListFiles";
			this.treeListFiles.OptionsBehavior.AllowExpandOnDblClick = false;
			this.treeListFiles.OptionsBehavior.AutoChangeParent = false;
			this.treeListFiles.OptionsBehavior.Editable = false;
			this.treeListFiles.OptionsBehavior.ResizeNodes = false;
			this.treeListFiles.OptionsLayout.AddNewColumns = false;
			this.treeListFiles.OptionsMenu.EnableColumnMenu = false;
			this.treeListFiles.OptionsMenu.EnableFooterMenu = false;
			this.treeListFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeListFiles.OptionsView.ShowColumns = false;
			this.treeListFiles.OptionsView.ShowFocusedFrame = false;
			this.treeListFiles.OptionsView.ShowHorzLines = false;
			this.treeListFiles.OptionsView.ShowIndicator = false;
			this.treeListFiles.OptionsView.ShowVertLines = false;
			this.treeListFiles.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
			this.treeListFiles.Size = new System.Drawing.Size(260, 402);
			this.treeListFiles.StateImageList = this.imageListFiles;
			this.treeListFiles.TabIndex = 6;
			this.treeListFiles.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler(this.treeListFiles_AfterExpand);
			this.treeListFiles.AfterCollapse += new DevExpress.XtraTreeList.NodeEventHandler(this.treeListFiles_AfterCollapse);
			this.treeListFiles.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeListFiles_AfterFocusNode);
			this.treeListFiles.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListFiles_FocusedNodeChanged);
			this.treeListFiles.HiddenEditor += new System.EventHandler(this.treeListFiles_HiddenEditor);
			this.treeListFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeListFiles_DragDrop);
			this.treeListFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeListFiles_DragEnter);
			this.treeListFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.treeListFiles_DragOver);
			this.treeListFiles.DragLeave += new System.EventHandler(this.treeListFiles_DragLeave);
			this.treeListFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListFiles_MouseDoubleClick);
			// 
			// treeListColumnName
			// 
			this.treeListColumnName.Caption = "treeListColumn1";
			this.treeListColumnName.FieldName = "treeListColumn1";
			this.treeListColumnName.MinWidth = 33;
			this.treeListColumnName.Name = "treeListColumnName";
			this.treeListColumnName.Visible = true;
			this.treeListColumnName.VisibleIndex = 0;
			// 
			// treeListColumnPath
			// 
			this.treeListColumnPath.Caption = "treeListColumn1";
			this.treeListColumnPath.FieldName = "treeListColumn1";
			this.treeListColumnPath.Name = "treeListColumnPath";
			// 
			// imageListFiles
			// 
			this.imageListFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFiles.ImageStream")));
			this.imageListFiles.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListFiles.Images.SetKeyName(0, "");
			this.imageListFiles.Images.SetKeyName(1, "");
			this.imageListFiles.Images.SetKeyName(2, "Image Files.png");
			// 
			// pnTreeViewProgress
			// 
			this.pnTreeViewProgress.Controls.Add(this.laTreeViewProgressLabel);
			this.pnTreeViewProgress.Controls.Add(this.circularProgressTreeView);
			this.pnTreeViewProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnTreeViewProgress.Location = new System.Drawing.Point(0, 431);
			this.pnTreeViewProgress.Name = "pnTreeViewProgress";
			this.pnTreeViewProgress.Padding = new System.Windows.Forms.Padding(5);
			this.pnTreeViewProgress.Size = new System.Drawing.Size(260, 40);
			this.pnTreeViewProgress.TabIndex = 13;
			// 
			// laTreeViewProgressLabel
			// 
			this.laTreeViewProgressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTreeViewProgressLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTreeViewProgressLabel.Location = new System.Drawing.Point(68, 5);
			this.laTreeViewProgressLabel.Name = "laTreeViewProgressLabel";
			this.laTreeViewProgressLabel.Size = new System.Drawing.Size(187, 30);
			this.laTreeViewProgressLabel.TabIndex = 0;
			this.laTreeViewProgressLabel.Text = "label1";
			this.laTreeViewProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// circularProgressTreeView
			// 
			this.circularProgressTreeView.AnimationSpeed = 50;
			// 
			// 
			// 
			this.circularProgressTreeView.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.circularProgressTreeView.FocusCuesEnabled = false;
			this.circularProgressTreeView.Location = new System.Drawing.Point(5, 5);
			this.circularProgressTreeView.Name = "circularProgressTreeView";
			this.circularProgressTreeView.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressTreeView.ProgressTextFormat = "";
			this.circularProgressTreeView.Size = new System.Drawing.Size(63, 30);
			this.circularProgressTreeView.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressTreeView.TabIndex = 1;
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 442);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(260, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 442);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 471);
			this.barDockControlBottom.Size = new System.Drawing.Size(260, 0);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(260, 29);
			// 
			// pnPictures
			// 
			this.pnPictures.Controls.Add(this.xtraScrollableControlPicture);
			this.pnPictures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnPictures.Location = new System.Drawing.Point(0, 0);
			this.pnPictures.Name = "pnPictures";
			this.pnPictures.Size = new System.Drawing.Size(367, 471);
			this.pnPictures.TabIndex = 12;
			this.pnPictures.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnPictures_MouseMove);
			this.pnPictures.Resize += new System.EventHandler(this.pnPictures_Resize);
			// 
			// xtraScrollableControlPicture
			// 
			this.xtraScrollableControlPicture.AlwaysScrollActiveControlIntoView = false;
			this.xtraScrollableControlPicture.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraScrollableControlPicture.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlPicture.Controls.Add(this.imageListView);
			this.xtraScrollableControlPicture.Controls.Add(this.pbPicture);
			this.xtraScrollableControlPicture.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlPicture.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControlPicture.Name = "xtraScrollableControlPicture";
			this.xtraScrollableControlPicture.Size = new System.Drawing.Size(367, 471);
			this.xtraScrollableControlPicture.TabIndex = 13;
			// 
			// imageListView
			// 
			this.imageListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.imageListView.CacheLimit = "0";
			this.imageListView.DefaultImage = ((System.Drawing.Image)(resources.GetObject("imageListView.DefaultImage")));
			this.imageListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageListView.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageListView.ErrorImage")));
			this.imageListView.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.imageListView.Location = new System.Drawing.Point(0, 120);
			this.imageListView.Name = "imageListView";
			this.imageListView.Size = new System.Drawing.Size(367, 351);
			this.imageListView.TabIndex = 12;
			this.imageListView.Text = "";
			this.imageListView.ThumbnailSize = new System.Drawing.Size(200, 200);
			this.imageListView.Visible = false;
			this.imageListView.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView_ItemDoubleClick);
			// 
			// pbPicture
			// 
			this.pbPicture.BackColor = System.Drawing.Color.White;
			this.pbPicture.Cursor = System.Windows.Forms.Cursors.Default;
			this.pbPicture.Dock = System.Windows.Forms.DockStyle.Top;
			this.pbPicture.Location = new System.Drawing.Point(0, 0);
			this.pbPicture.Name = "pbPicture";
			this.pbPicture.Size = new System.Drawing.Size(367, 120);
			this.pbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbPicture.TabIndex = 11;
			this.pbPicture.TabStop = false;
			this.pbPicture.Visible = false;
			this.pbPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnPictures_MouseMove);
			// 
			// barManager
			// 
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barToolButtons});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this.pnTreeList;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemAddNode,
            this.barButtonItemChangeNodeName,
            this.barButtonItemDeleteNode,
            this.barButtonItem1});
			this.barManager.MaxItemId = 4;
			// 
			// barToolButtons
			// 
			this.barToolButtons.BarName = "Tools";
			this.barToolButtons.DockCol = 0;
			this.barToolButtons.DockRow = 0;
			this.barToolButtons.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barToolButtons.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAddNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemChangeNodeName, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDeleteNode, true)});
			this.barToolButtons.OptionsBar.AllowQuickCustomization = false;
			this.barToolButtons.OptionsBar.DrawDragBorder = false;
			this.barToolButtons.OptionsBar.UseWholeRow = true;
			this.barToolButtons.Text = "Tools";
			// 
			// barButtonItemAddNode
			// 
			this.barButtonItemAddNode.Caption = "New Folder";
			this.barButtonItemAddNode.Id = 0;
			this.barButtonItemAddNode.Name = "barButtonItemAddNode";
			this.barButtonItemAddNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAddNode_ItemClick);
			// 
			// barButtonItemChangeNodeName
			// 
			this.barButtonItemChangeNodeName.Caption = "Edit Name";
			this.barButtonItemChangeNodeName.Id = 1;
			this.barButtonItemChangeNodeName.Name = "barButtonItemChangeNodeName";
			this.barButtonItemChangeNodeName.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemChangeNodeName_ItemClick);
			// 
			// barButtonItemDeleteNode
			// 
			this.barButtonItemDeleteNode.Caption = "Delete";
			this.barButtonItemDeleteNode.Id = 2;
			this.barButtonItemDeleteNode.Name = "barButtonItemDeleteNode";
			this.barButtonItemDeleteNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDeleteNode_ItemClick);
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Caption = "Add Root";
			this.barButtonItem1.Id = 3;
			this.barButtonItem1.Name = "barButtonItem1";
			// 
			// WebArtControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "WebArtControl";
			this.Size = new System.Drawing.Size(640, 475);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.pnTreeList.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListFiles)).EndInit();
			this.pnTreeViewProgress.ResumeLayout(false);
			this.pnPictures.ResumeLayout(false);
			this.xtraScrollableControlPicture.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private System.Windows.Forms.Panel pnTreeList;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barToolButtons;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAddNode;
        private DevExpress.XtraBars.BarButtonItem barButtonItemChangeNodeName;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDeleteNode;
        private System.Windows.Forms.ImageList imageListFiles;
        private DevExpress.XtraTreeList.TreeList treeListFiles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnPath;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.Panel pnPictures;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlPicture;
        internal System.Windows.Forms.PictureBox pbPicture;
        private Manina.Windows.Forms.ImageListView imageListView;
		private System.Windows.Forms.Panel pnTreeViewProgress;
		private System.Windows.Forms.Label laTreeViewProgressLabel;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressTreeView;
    }
}
