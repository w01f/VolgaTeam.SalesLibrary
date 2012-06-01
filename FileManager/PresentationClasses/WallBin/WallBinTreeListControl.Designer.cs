namespace FileManager.PresentationClasses.WallBin
{
    partial class WallBinTreeListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WallBinTreeListControl));
            this.tcSalesDepotFiles = new System.Windows.Forms.TabControl();
            this.tpDefault = new System.Windows.Forms.TabPage();
            this.pnTreeView = new System.Windows.Forms.Panel();
            this.treeListAllFiles = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnPath = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageListFiles = new System.Windows.Forms.ImageList(this.components);
            this.tpSearch = new System.Windows.Forms.TabPage();
            this.pnFilesByKeyWord = new System.Windows.Forms.Panel();
            this.treeListSearchFiles = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnKeyWord = new System.Windows.Forms.Panel();
            this.gbDateRange = new System.Windows.Forms.GroupBox();
            this.laEndDate = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.laStartDate = new System.Windows.Forms.Label();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.btSearch = new System.Windows.Forms.Button();
            this.edKeyWord = new System.Windows.Forms.TextBox();
            this.ckDateRange = new System.Windows.Forms.CheckBox();
            this.pnTreeViewProgress = new System.Windows.Forms.Panel();
            this.gbTreeViewProgress = new System.Windows.Forms.GroupBox();
            this.pgTreeViewProgress = new System.Windows.Forms.ProgressBar();
            this.laTreeViewProgressLable = new System.Windows.Forms.Label();
            this.pnTreeViewOptions = new System.Windows.Forms.Panel();
            this.gbTreeViewOptions = new System.Windows.Forms.GroupBox();
            this.laDoubleClick = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tcSalesDepotFiles.SuspendLayout();
            this.tpDefault.SuspendLayout();
            this.pnTreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).BeginInit();
            this.tpSearch.SuspendLayout();
            this.pnFilesByKeyWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListSearchFiles)).BeginInit();
            this.pnKeyWord.SuspendLayout();
            this.gbDateRange.SuspendLayout();
            this.pnTreeViewProgress.SuspendLayout();
            this.gbTreeViewProgress.SuspendLayout();
            this.pnTreeViewOptions.SuspendLayout();
            this.gbTreeViewOptions.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSalesDepotFiles
            // 
            this.tcSalesDepotFiles.Controls.Add(this.tpDefault);
            this.tcSalesDepotFiles.Controls.Add(this.tpSearch);
            this.tcSalesDepotFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tcSalesDepotFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSalesDepotFiles.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tcSalesDepotFiles.Location = new System.Drawing.Point(0, 50);
            this.tcSalesDepotFiles.Name = "tcSalesDepotFiles";
            this.tcSalesDepotFiles.SelectedIndex = 0;
            this.tcSalesDepotFiles.Size = new System.Drawing.Size(270, 378);
            this.tcSalesDepotFiles.TabIndex = 4;
            // 
            // tpDefault
            // 
            this.tpDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tpDefault.Controls.Add(this.pnTreeView);
            this.tpDefault.Location = new System.Drawing.Point(4, 25);
            this.tpDefault.Name = "tpDefault";
            this.tpDefault.Padding = new System.Windows.Forms.Padding(3);
            this.tpDefault.Size = new System.Drawing.Size(262, 349);
            this.tpDefault.TabIndex = 0;
            this.tpDefault.Text = "Tree View";
            // 
            // pnTreeView
            // 
            this.pnTreeView.Controls.Add(this.treeListAllFiles);
            this.pnTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnTreeView.Location = new System.Drawing.Point(3, 3);
            this.pnTreeView.Name = "pnTreeView";
            this.pnTreeView.Size = new System.Drawing.Size(256, 343);
            this.pnTreeView.TabIndex = 1;
            // 
            // treeListAllFiles
            // 
            this.treeListAllFiles.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.treeListAllFiles.Appearance.FocusedCell.Options.UseFont = true;
            this.treeListAllFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.treeListAllFiles.Appearance.FocusedRow.Options.UseFont = true;
            this.treeListAllFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeListAllFiles.Appearance.Row.Options.UseFont = true;
            this.treeListAllFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.treeListAllFiles.Appearance.SelectedRow.Options.UseFont = true;
            this.treeListAllFiles.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName,
            this.treeListColumnPath});
            this.treeListAllFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListAllFiles.Location = new System.Drawing.Point(0, 0);
            this.treeListAllFiles.Name = "treeListAllFiles";
            this.treeListAllFiles.OptionsBehavior.AutoChangeParent = false;
            this.treeListAllFiles.OptionsBehavior.Editable = false;
            this.treeListAllFiles.OptionsBehavior.ResizeNodes = false;
            this.treeListAllFiles.OptionsLayout.AddNewColumns = false;
            this.treeListAllFiles.OptionsMenu.EnableColumnMenu = false;
            this.treeListAllFiles.OptionsMenu.EnableFooterMenu = false;
            this.treeListAllFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeListAllFiles.OptionsSelection.MultiSelect = true;
            this.treeListAllFiles.OptionsView.ShowColumns = false;
            this.treeListAllFiles.OptionsView.ShowFocusedFrame = false;
            this.treeListAllFiles.OptionsView.ShowHorzLines = false;
            this.treeListAllFiles.OptionsView.ShowIndicator = false;
            this.treeListAllFiles.OptionsView.ShowVertLines = false;
            this.treeListAllFiles.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.treeListAllFiles.Size = new System.Drawing.Size(256, 343);
            this.treeListAllFiles.StateImageList = this.imageListFiles;
            this.treeListAllFiles.TabIndex = 1;
            this.treeListAllFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeListAllFiles_MouseClick);
            this.treeListAllFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListAllFiles_MouseDoubleClick);
            this.treeListAllFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDown);
            this.treeListAllFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseMove);
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "treeListColumn1";
            this.treeListColumnName.FieldName = "treeListColumn1";
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
            this.imageListFiles.Images.SetKeyName(2, "All other Files.png");
            this.imageListFiles.Images.SetKeyName(3, "Excel Files.png");
            this.imageListFiles.Images.SetKeyName(4, "Image Files.png");
            this.imageListFiles.Images.SetKeyName(5, "PDF Files.png");
            this.imageListFiles.Images.SetKeyName(6, "PowerPoint Files.png");
            this.imageListFiles.Images.SetKeyName(7, "Video Clips.png");
            this.imageListFiles.Images.SetKeyName(8, "Web Links.png");
            this.imageListFiles.Images.SetKeyName(9, "Word Files.png");
            // 
            // tpSearch
            // 
            this.tpSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tpSearch.Controls.Add(this.pnFilesByKeyWord);
            this.tpSearch.Controls.Add(this.pnKeyWord);
            this.tpSearch.ImageIndex = 0;
            this.tpSearch.Location = new System.Drawing.Point(4, 25);
            this.tpSearch.Name = "tpSearch";
            this.tpSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpSearch.Size = new System.Drawing.Size(262, 349);
            this.tpSearch.TabIndex = 1;
            this.tpSearch.Text = "Key Word";
            // 
            // pnFilesByKeyWord
            // 
            this.pnFilesByKeyWord.Controls.Add(this.treeListSearchFiles);
            this.pnFilesByKeyWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnFilesByKeyWord.Location = new System.Drawing.Point(3, 131);
            this.pnFilesByKeyWord.Name = "pnFilesByKeyWord";
            this.pnFilesByKeyWord.Size = new System.Drawing.Size(256, 215);
            this.pnFilesByKeyWord.TabIndex = 1;
            // 
            // treeListSearchFiles
            // 
            this.treeListSearchFiles.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.treeListSearchFiles.Appearance.FocusedCell.Options.UseFont = true;
            this.treeListSearchFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.treeListSearchFiles.Appearance.FocusedRow.Options.UseFont = true;
            this.treeListSearchFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeListSearchFiles.Appearance.Row.Options.UseFont = true;
            this.treeListSearchFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.treeListSearchFiles.Appearance.SelectedRow.Options.UseFont = true;
            this.treeListSearchFiles.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeListSearchFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListSearchFiles.Location = new System.Drawing.Point(0, 0);
            this.treeListSearchFiles.Name = "treeListSearchFiles";
            this.treeListSearchFiles.OptionsBehavior.AutoChangeParent = false;
            this.treeListSearchFiles.OptionsBehavior.Editable = false;
            this.treeListSearchFiles.OptionsBehavior.ResizeNodes = false;
            this.treeListSearchFiles.OptionsLayout.AddNewColumns = false;
            this.treeListSearchFiles.OptionsMenu.EnableColumnMenu = false;
            this.treeListSearchFiles.OptionsMenu.EnableFooterMenu = false;
            this.treeListSearchFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeListSearchFiles.OptionsSelection.MultiSelect = true;
            this.treeListSearchFiles.OptionsView.ShowColumns = false;
            this.treeListSearchFiles.OptionsView.ShowFocusedFrame = false;
            this.treeListSearchFiles.OptionsView.ShowHorzLines = false;
            this.treeListSearchFiles.OptionsView.ShowIndicator = false;
            this.treeListSearchFiles.OptionsView.ShowVertLines = false;
            this.treeListSearchFiles.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.treeListSearchFiles.Size = new System.Drawing.Size(256, 215);
            this.treeListSearchFiles.StateImageList = this.imageListFiles;
            this.treeListSearchFiles.TabIndex = 2;
            this.treeListSearchFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeListAllFiles_MouseClick);
            this.treeListSearchFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListAllFiles_MouseDoubleClick);
            this.treeListSearchFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDown);
            this.treeListSearchFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseMove);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "treeListColumn1";
            this.treeListColumn1.FieldName = "treeListColumn1";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "treeListColumn1";
            this.treeListColumn2.FieldName = "treeListColumn1";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // pnKeyWord
            // 
            this.pnKeyWord.Controls.Add(this.gbDateRange);
            this.pnKeyWord.Controls.Add(this.btSearch);
            this.pnKeyWord.Controls.Add(this.edKeyWord);
            this.pnKeyWord.Controls.Add(this.ckDateRange);
            this.pnKeyWord.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnKeyWord.Location = new System.Drawing.Point(3, 3);
            this.pnKeyWord.Name = "pnKeyWord";
            this.pnKeyWord.Size = new System.Drawing.Size(256, 128);
            this.pnKeyWord.TabIndex = 0;
            // 
            // gbDateRange
            // 
            this.gbDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDateRange.Controls.Add(this.laEndDate);
            this.gbDateRange.Controls.Add(this.dtEndDate);
            this.gbDateRange.Controls.Add(this.laStartDate);
            this.gbDateRange.Controls.Add(this.dtStartDate);
            this.gbDateRange.Location = new System.Drawing.Point(0, 53);
            this.gbDateRange.Name = "gbDateRange";
            this.gbDateRange.Size = new System.Drawing.Size(255, 71);
            this.gbDateRange.TabIndex = 2;
            this.gbDateRange.TabStop = false;
            // 
            // laEndDate
            // 
            this.laEndDate.AutoSize = true;
            this.laEndDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laEndDate.Location = new System.Drawing.Point(2, 45);
            this.laEndDate.Name = "laEndDate";
            this.laEndDate.Size = new System.Drawing.Size(62, 16);
            this.laEndDate.TabIndex = 4;
            this.laEndDate.Text = "End Date";
            // 
            // dtEndDate
            // 
            this.dtEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtEndDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndDate.Location = new System.Drawing.Point(90, 42);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(158, 22);
            this.dtEndDate.TabIndex = 3;
            // 
            // laStartDate
            // 
            this.laStartDate.AutoSize = true;
            this.laStartDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laStartDate.Location = new System.Drawing.Point(2, 17);
            this.laStartDate.Name = "laStartDate";
            this.laStartDate.Size = new System.Drawing.Size(67, 16);
            this.laStartDate.TabIndex = 2;
            this.laStartDate.Text = "Start Date";
            // 
            // dtStartDate
            // 
            this.dtStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtStartDate.CustomFormat = "MM/dd/yyy";
            this.dtStartDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartDate.Location = new System.Drawing.Point(90, 14);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(158, 22);
            this.dtStartDate.TabIndex = 1;
            // 
            // btSearch
            // 
            this.btSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSearch.Location = new System.Drawing.Point(181, 31);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 1;
            this.btSearch.Text = "Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // edKeyWord
            // 
            this.edKeyWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edKeyWord.Location = new System.Drawing.Point(1, 4);
            this.edKeyWord.Name = "edKeyWord";
            this.edKeyWord.Size = new System.Drawing.Size(254, 22);
            this.edKeyWord.TabIndex = 0;
            this.edKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edKeyWord_KeyDown);
            // 
            // ckDateRange
            // 
            this.ckDateRange.AutoSize = true;
            this.ckDateRange.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckDateRange.Location = new System.Drawing.Point(1, 33);
            this.ckDateRange.Name = "ckDateRange";
            this.ckDateRange.Size = new System.Drawing.Size(119, 20);
            this.ckDateRange.TabIndex = 0;
            this.ckDateRange.Text = "Set Date Range";
            this.ckDateRange.UseVisualStyleBackColor = true;
            this.ckDateRange.CheckedChanged += new System.EventHandler(this.ckDateRange_CheckedChanged);
            // 
            // pnTreeViewProgress
            // 
            this.pnTreeViewProgress.Controls.Add(this.gbTreeViewProgress);
            this.pnTreeViewProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnTreeViewProgress.Location = new System.Drawing.Point(0, 428);
            this.pnTreeViewProgress.Name = "pnTreeViewProgress";
            this.pnTreeViewProgress.Size = new System.Drawing.Size(270, 72);
            this.pnTreeViewProgress.TabIndex = 5;
            // 
            // gbTreeViewProgress
            // 
            this.gbTreeViewProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTreeViewProgress.Controls.Add(this.pgTreeViewProgress);
            this.gbTreeViewProgress.Controls.Add(this.laTreeViewProgressLable);
            this.gbTreeViewProgress.Location = new System.Drawing.Point(6, 3);
            this.gbTreeViewProgress.Name = "gbTreeViewProgress";
            this.gbTreeViewProgress.Size = new System.Drawing.Size(258, 61);
            this.gbTreeViewProgress.TabIndex = 0;
            this.gbTreeViewProgress.TabStop = false;
            // 
            // pgTreeViewProgress
            // 
            this.pgTreeViewProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTreeViewProgress.Location = new System.Drawing.Point(8, 34);
            this.pgTreeViewProgress.Name = "pgTreeViewProgress";
            this.pgTreeViewProgress.Size = new System.Drawing.Size(242, 19);
            this.pgTreeViewProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgTreeViewProgress.TabIndex = 1;
            // 
            // laTreeViewProgressLable
            // 
            this.laTreeViewProgressLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laTreeViewProgressLable.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTreeViewProgressLable.Location = new System.Drawing.Point(8, 12);
            this.laTreeViewProgressLable.Name = "laTreeViewProgressLable";
            this.laTreeViewProgressLable.Size = new System.Drawing.Size(242, 19);
            this.laTreeViewProgressLable.TabIndex = 0;
            this.laTreeViewProgressLable.Text = "label1";
            // 
            // pnTreeViewOptions
            // 
            this.pnTreeViewOptions.Controls.Add(this.gbTreeViewOptions);
            this.pnTreeViewOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTreeViewOptions.Location = new System.Drawing.Point(0, 0);
            this.pnTreeViewOptions.Name = "pnTreeViewOptions";
            this.pnTreeViewOptions.Size = new System.Drawing.Size(270, 50);
            this.pnTreeViewOptions.TabIndex = 6;
            // 
            // gbTreeViewOptions
            // 
            this.gbTreeViewOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTreeViewOptions.Controls.Add(this.laDoubleClick);
            this.gbTreeViewOptions.Controls.Add(this.btRefresh);
            this.gbTreeViewOptions.Location = new System.Drawing.Point(5, 2);
            this.gbTreeViewOptions.Name = "gbTreeViewOptions";
            this.gbTreeViewOptions.Size = new System.Drawing.Size(260, 39);
            this.gbTreeViewOptions.TabIndex = 0;
            this.gbTreeViewOptions.TabStop = false;
            // 
            // laDoubleClick
            // 
            this.laDoubleClick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laDoubleClick.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDoubleClick.Location = new System.Drawing.Point(82, 11);
            this.laDoubleClick.Name = "laDoubleClick";
            this.laDoubleClick.Size = new System.Drawing.Size(167, 23);
            this.laDoubleClick.TabIndex = 3;
            this.laDoubleClick.Text = "Double Click to Open";
            this.laDoubleClick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btRefresh
            // 
            this.btRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btRefresh.Location = new System.Drawing.Point(6, 11);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(70, 23);
            this.btRefresh.TabIndex = 2;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiOpen});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(82, 26);
            // 
            // tmiOpen
            // 
            this.tmiOpen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tmiOpen.Name = "tmiOpen";
            this.tmiOpen.Size = new System.Drawing.Size(81, 22);
            this.tmiOpen.Text = "Open";
            this.tmiOpen.Click += new System.EventHandler(this.tmiOpen_Click);
            // 
            // WallBinTreeListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.tcSalesDepotFiles);
            this.Controls.Add(this.pnTreeViewOptions);
            this.Controls.Add(this.pnTreeViewProgress);
            this.Name = "WallBinTreeListControl";
            this.Size = new System.Drawing.Size(270, 500);
            this.tcSalesDepotFiles.ResumeLayout(false);
            this.tpDefault.ResumeLayout(false);
            this.pnTreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).EndInit();
            this.tpSearch.ResumeLayout(false);
            this.pnFilesByKeyWord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListSearchFiles)).EndInit();
            this.pnKeyWord.ResumeLayout(false);
            this.pnKeyWord.PerformLayout();
            this.gbDateRange.ResumeLayout(false);
            this.gbDateRange.PerformLayout();
            this.pnTreeViewProgress.ResumeLayout(false);
            this.gbTreeViewProgress.ResumeLayout(false);
            this.pnTreeViewOptions.ResumeLayout(false);
            this.gbTreeViewOptions.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcSalesDepotFiles;
        private System.Windows.Forms.TabPage tpDefault;
        private System.Windows.Forms.Panel pnTreeView;
        private System.Windows.Forms.TabPage tpSearch;
        private System.Windows.Forms.Panel pnFilesByKeyWord;
        private System.Windows.Forms.Panel pnKeyWord;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.TextBox edKeyWord;
        private System.Windows.Forms.Panel pnTreeViewProgress;
        private System.Windows.Forms.GroupBox gbTreeViewProgress;
        private System.Windows.Forms.ProgressBar pgTreeViewProgress;
        private System.Windows.Forms.Label laTreeViewProgressLable;
        private System.Windows.Forms.Panel pnTreeViewOptions;
        private System.Windows.Forms.GroupBox gbTreeViewOptions;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tmiOpen;
        private System.Windows.Forms.Label laDoubleClick;
        private System.Windows.Forms.GroupBox gbDateRange;
        private System.Windows.Forms.CheckBox ckDateRange;
        private System.Windows.Forms.Label laEndDate;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.Label laStartDate;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.ImageList imageListFiles;
        private DevExpress.XtraTreeList.TreeList treeListAllFiles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnPath;
        private DevExpress.XtraTreeList.TreeList treeListSearchFiles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
    }
}
