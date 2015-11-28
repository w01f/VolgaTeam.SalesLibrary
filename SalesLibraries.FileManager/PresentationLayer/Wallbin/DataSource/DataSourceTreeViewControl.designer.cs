namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	sealed partial class DataSourceTreeViewControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceTreeViewControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.treeListAllFiles = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumnPath = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.imageListFiles = new System.Windows.Forms.ImageList(this.components);
			this.treeListSearchFiles = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.pnKeyWord = new System.Windows.Forms.Panel();
			this.groupControlDateRange = new DevExpress.XtraEditors.GroupControl();
			this.laStartDate = new System.Windows.Forms.Label();
			this.dateEditEndDate = new DevExpress.XtraEditors.DateEdit();
			this.laEndDate = new System.Windows.Forms.Label();
			this.dateEditStartDate = new DevExpress.XtraEditors.DateEdit();
			this.buttonXSearch = new DevComponents.DotNetBar.ButtonX();
			this.checkEditDateRange = new DevExpress.XtraEditors.CheckEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.textEditKeyWord = new DevExpress.XtraEditors.TextEdit();
			this.pnRefresh = new System.Windows.Forms.Panel();
			this.buttonXRefresh = new DevComponents.DotNetBar.ButtonX();
			this.laDoubleClick = new System.Windows.Forms.Label();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tmiOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.xtraTabControlFiles = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageRegular = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageSearch = new DevExpress.XtraTab.XtraTabPage();
			this.pnTreeViewProgress = new System.Windows.Forms.Panel();
			this.laTreeViewProgressLabel = new System.Windows.Forms.Label();
			this.circularProgressTreeView = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.pnMain = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListSearchFiles)).BeginInit();
			this.pnKeyWord.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControlDateRange)).BeginInit();
			this.groupControlDateRange.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDateRange.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditKeyWord.Properties)).BeginInit();
			this.pnRefresh.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlFiles)).BeginInit();
			this.xtraTabControlFiles.SuspendLayout();
			this.xtraTabPageRegular.SuspendLayout();
			this.xtraTabPageSearch.SuspendLayout();
			this.pnTreeViewProgress.SuspendLayout();
			this.pnMain.SuspendLayout();
			this.SuspendLayout();
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
			this.treeListAllFiles.Size = new System.Drawing.Size(297, 389);
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
			this.imageListFiles.Images.SetKeyName(2, "All other Files.png");
			this.imageListFiles.Images.SetKeyName(3, "Excel Files.png");
			this.imageListFiles.Images.SetKeyName(4, "Image Files.png");
			this.imageListFiles.Images.SetKeyName(5, "PDF Files.png");
			this.imageListFiles.Images.SetKeyName(6, "PowerPoint Files.png");
			this.imageListFiles.Images.SetKeyName(7, "Video Clips.png");
			this.imageListFiles.Images.SetKeyName(8, "Web Links.png");
			this.imageListFiles.Images.SetKeyName(9, "Word Files.png");
			this.imageListFiles.Images.SetKeyName(10, "keynote.png");
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
			this.treeListSearchFiles.Location = new System.Drawing.Point(0, 142);
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
			this.treeListSearchFiles.Size = new System.Drawing.Size(293, 244);
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
			this.treeListColumn1.MinWidth = 33;
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
			this.pnKeyWord.Controls.Add(this.groupControlDateRange);
			this.pnKeyWord.Controls.Add(this.buttonXSearch);
			this.pnKeyWord.Controls.Add(this.checkEditDateRange);
			this.pnKeyWord.Controls.Add(this.textEditKeyWord);
			this.pnKeyWord.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnKeyWord.Location = new System.Drawing.Point(0, 0);
			this.pnKeyWord.Name = "pnKeyWord";
			this.pnKeyWord.Size = new System.Drawing.Size(293, 142);
			this.pnKeyWord.TabIndex = 0;
			// 
			// groupControlDateRange
			// 
			this.groupControlDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlDateRange.Controls.Add(this.laStartDate);
			this.groupControlDateRange.Controls.Add(this.dateEditEndDate);
			this.groupControlDateRange.Controls.Add(this.laEndDate);
			this.groupControlDateRange.Controls.Add(this.dateEditStartDate);
			this.groupControlDateRange.Location = new System.Drawing.Point(6, 69);
			this.groupControlDateRange.Name = "groupControlDateRange";
			this.groupControlDateRange.ShowCaption = false;
			this.groupControlDateRange.Size = new System.Drawing.Size(278, 64);
			this.groupControlDateRange.TabIndex = 0;
			// 
			// laStartDate
			// 
			this.laStartDate.AutoSize = true;
			this.laStartDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laStartDate.Location = new System.Drawing.Point(5, 10);
			this.laStartDate.Name = "laStartDate";
			this.laStartDate.Size = new System.Drawing.Size(67, 16);
			this.laStartDate.TabIndex = 2;
			this.laStartDate.Text = "Start Date";
			// 
			// dateEditEndDate
			// 
			this.dateEditEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dateEditEndDate.EditValue = null;
			this.dateEditEndDate.Location = new System.Drawing.Point(153, 35);
			this.dateEditEndDate.Name = "dateEditEndDate";
			this.dateEditEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditEndDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditEndDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditEndDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.dateEditEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditEndDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEndDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEndDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditEndDate.Properties.NullText = "Select";
			this.dateEditEndDate.Properties.ShowPopupShadow = false;
			this.dateEditEndDate.Properties.ShowToday = false;
			this.dateEditEndDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditEndDate.Size = new System.Drawing.Size(120, 22);
			this.dateEditEndDate.TabIndex = 7;
			// 
			// laEndDate
			// 
			this.laEndDate.AutoSize = true;
			this.laEndDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laEndDate.Location = new System.Drawing.Point(5, 38);
			this.laEndDate.Name = "laEndDate";
			this.laEndDate.Size = new System.Drawing.Size(62, 16);
			this.laEndDate.TabIndex = 4;
			this.laEndDate.Text = "End Date";
			// 
			// dateEditStartDate
			// 
			this.dateEditStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dateEditStartDate.EditValue = null;
			this.dateEditStartDate.Location = new System.Drawing.Point(153, 7);
			this.dateEditStartDate.Name = "dateEditStartDate";
			this.dateEditStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditStartDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditStartDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditStartDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.dateEditStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditStartDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStartDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStartDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditStartDate.Properties.NullText = "Select";
			this.dateEditStartDate.Properties.ShowPopupShadow = false;
			this.dateEditStartDate.Properties.ShowToday = false;
			this.dateEditStartDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditStartDate.Size = new System.Drawing.Size(120, 22);
			this.dateEditStartDate.TabIndex = 6;
			// 
			// buttonXSearch
			// 
			this.buttonXSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSearch.Location = new System.Drawing.Point(209, 37);
			this.buttonXSearch.Name = "buttonXSearch";
			this.buttonXSearch.Size = new System.Drawing.Size(75, 28);
			this.buttonXSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSearch.TabIndex = 5;
			this.buttonXSearch.Text = "Search";
			this.buttonXSearch.Click += new System.EventHandler(this.btSearch_Click);
			// 
			// checkEditDateRange
			// 
			this.checkEditDateRange.Location = new System.Drawing.Point(6, 41);
			this.checkEditDateRange.Name = "checkEditDateRange";
			this.checkEditDateRange.Properties.AutoWidth = true;
			this.checkEditDateRange.Properties.Caption = "Set Date Range";
			this.checkEditDateRange.Size = new System.Drawing.Size(114, 20);
			this.checkEditDateRange.StyleController = this.styleController;
			this.checkEditDateRange.TabIndex = 4;
			this.checkEditDateRange.CheckedChanged += new System.EventHandler(this.ckDateRange_CheckedChanged);
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			// 
			// textEditKeyWord
			// 
			this.textEditKeyWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditKeyWord.Location = new System.Drawing.Point(8, 7);
			this.textEditKeyWord.Name = "textEditKeyWord";
			this.textEditKeyWord.Properties.NullText = "Type keyword here...";
			this.textEditKeyWord.Size = new System.Drawing.Size(276, 22);
			this.textEditKeyWord.StyleController = this.styleController;
			this.textEditKeyWord.TabIndex = 3;
			this.textEditKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edKeyWord_KeyDown);
			// 
			// pnRefresh
			// 
			this.pnRefresh.Controls.Add(this.buttonXRefresh);
			this.pnRefresh.Controls.Add(this.laDoubleClick);
			this.pnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnRefresh.Location = new System.Drawing.Point(0, 0);
			this.pnRefresh.Name = "pnRefresh";
			this.pnRefresh.Padding = new System.Windows.Forms.Padding(5);
			this.pnRefresh.Size = new System.Drawing.Size(299, 43);
			this.pnRefresh.TabIndex = 6;
			// 
			// buttonXRefresh
			// 
			this.buttonXRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefresh.Location = new System.Drawing.Point(10, 7);
			this.buttonXRefresh.Name = "buttonXRefresh";
			this.buttonXRefresh.Size = new System.Drawing.Size(75, 28);
			this.buttonXRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefresh.TabIndex = 4;
			this.buttonXRefresh.Text = "Refresh";
			this.buttonXRefresh.Click += new System.EventHandler(this.Refresh_Click);
			// 
			// laDoubleClick
			// 
			this.laDoubleClick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laDoubleClick.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDoubleClick.Location = new System.Drawing.Point(93, 7);
			this.laDoubleClick.Name = "laDoubleClick";
			this.laDoubleClick.Size = new System.Drawing.Size(196, 28);
			this.laDoubleClick.TabIndex = 3;
			this.laDoubleClick.Text = "Double Click to Open";
			this.laDoubleClick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// xtraTabControlFiles
			// 
			this.xtraTabControlFiles.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlFiles.Appearance.Options.UseFont = true;
			this.xtraTabControlFiles.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlFiles.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlFiles.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlFiles.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlFiles.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlFiles.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlFiles.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlFiles.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlFiles.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlFiles.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlFiles.Location = new System.Drawing.Point(0, 43);
			this.xtraTabControlFiles.Name = "xtraTabControlFiles";
			this.xtraTabControlFiles.SelectedTabPage = this.xtraTabPageRegular;
			this.xtraTabControlFiles.Size = new System.Drawing.Size(299, 417);
			this.xtraTabControlFiles.TabIndex = 7;
			this.xtraTabControlFiles.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageRegular,
            this.xtraTabPageSearch});
			this.xtraTabControlFiles.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlFiles_SelectedPageChanged);
			// 
			// xtraTabPageRegular
			// 
			this.xtraTabPageRegular.Controls.Add(this.treeListAllFiles);
			this.xtraTabPageRegular.Name = "xtraTabPageRegular";
			this.xtraTabPageRegular.Size = new System.Drawing.Size(297, 389);
			this.xtraTabPageRegular.Text = "Tree View";
			// 
			// xtraTabPageSearch
			// 
			this.xtraTabPageSearch.Controls.Add(this.treeListSearchFiles);
			this.xtraTabPageSearch.Controls.Add(this.pnKeyWord);
			this.xtraTabPageSearch.Name = "xtraTabPageSearch";
			this.xtraTabPageSearch.Size = new System.Drawing.Size(293, 386);
			this.xtraTabPageSearch.Text = "Search";
			// 
			// pnTreeViewProgress
			// 
			this.pnTreeViewProgress.Controls.Add(this.laTreeViewProgressLabel);
			this.pnTreeViewProgress.Controls.Add(this.circularProgressTreeView);
			this.pnTreeViewProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnTreeViewProgress.Location = new System.Drawing.Point(0, 460);
			this.pnTreeViewProgress.Name = "pnTreeViewProgress";
			this.pnTreeViewProgress.Padding = new System.Windows.Forms.Padding(5);
			this.pnTreeViewProgress.Size = new System.Drawing.Size(299, 40);
			this.pnTreeViewProgress.TabIndex = 13;
			// 
			// laTreeViewProgressLabel
			// 
			this.laTreeViewProgressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTreeViewProgressLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTreeViewProgressLabel.Location = new System.Drawing.Point(68, 5);
			this.laTreeViewProgressLabel.Name = "laTreeViewProgressLabel";
			this.laTreeViewProgressLabel.Size = new System.Drawing.Size(226, 30);
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
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Controls.Add(this.xtraTabControlFiles);
			this.pnMain.Controls.Add(this.pnTreeViewProgress);
			this.pnMain.Controls.Add(this.pnRefresh);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(299, 500);
			this.pnMain.TabIndex = 8;
			// 
			// DataSourceTreeViewControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DataSourceTreeViewControl";
			this.Size = new System.Drawing.Size(299, 500);
			((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListSearchFiles)).EndInit();
			this.pnKeyWord.ResumeLayout(false);
			this.pnKeyWord.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControlDateRange)).EndInit();
			this.groupControlDateRange.ResumeLayout(false);
			this.groupControlDateRange.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDateRange.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditKeyWord.Properties)).EndInit();
			this.pnRefresh.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlFiles)).EndInit();
			this.xtraTabControlFiles.ResumeLayout(false);
			this.xtraTabPageRegular.ResumeLayout(false);
			this.xtraTabPageSearch.ResumeLayout(false);
			this.pnTreeViewProgress.ResumeLayout(false);
			this.pnMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnKeyWord;
        private System.Windows.Forms.Panel pnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tmiOpen;
		private System.Windows.Forms.Label laDoubleClick;
        private System.Windows.Forms.Label laEndDate;
        private System.Windows.Forms.Label laStartDate;
        private System.Windows.Forms.ImageList imageListFiles;
        private DevExpress.XtraTreeList.TreeList treeListAllFiles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnPath;
        private DevExpress.XtraTreeList.TreeList treeListSearchFiles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlFiles;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageRegular;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageSearch;
		private System.Windows.Forms.Panel pnMain;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.CheckEdit checkEditDateRange;
        private DevExpress.XtraEditors.TextEdit textEditKeyWord;
        private DevExpress.XtraEditors.DateEdit dateEditEndDate;
		private DevExpress.XtraEditors.DateEdit dateEditStartDate;
		private DevComponents.DotNetBar.ButtonX buttonXSearch;
		private DevComponents.DotNetBar.ButtonX buttonXRefresh;
		private System.Windows.Forms.Panel pnTreeViewProgress;
		private System.Windows.Forms.Label laTreeViewProgressLabel;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressTreeView;
		private DevExpress.XtraEditors.GroupControl groupControlDateRange;
    }
}
