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
			this.simpleButtonSearch = new DevExpress.XtraEditors.SimpleButton();
			this.checkEditDateRange = new DevExpress.XtraEditors.CheckEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.textEditKeyWord = new DevExpress.XtraEditors.TextEdit();
			this.gbDateRange = new System.Windows.Forms.GroupBox();
			this.dateEditEndDate = new DevExpress.XtraEditors.DateEdit();
			this.dateEditStartDate = new DevExpress.XtraEditors.DateEdit();
			this.laEndDate = new System.Windows.Forms.Label();
			this.laStartDate = new System.Windows.Forms.Label();
			this.pnTreeViewProgress = new System.Windows.Forms.Panel();
			this.panelExProgressTreeView = new DevComponents.DotNetBar.PanelEx();
			this.laTreeViewTreeViewProgressLable = new System.Windows.Forms.Label();
			this.circularProgressTreeView = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.pnRefresh = new System.Windows.Forms.Panel();
			this.panelExRefresh = new DevComponents.DotNetBar.PanelEx();
			this.simpleButtonRefresh = new DevExpress.XtraEditors.SimpleButton();
			this.laDoubleClick = new System.Windows.Forms.Label();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tmiOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.xtraTabControlFiles = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageRegular = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageSearch = new DevExpress.XtraTab.XtraTabPage();
			this.pnLeft = new System.Windows.Forms.Panel();
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnRight = new System.Windows.Forms.Panel();
			this.pnPreview = new System.Windows.Forms.Panel();
			this.xtraScrollableControlStatistic = new DevExpress.XtraEditors.XtraScrollableControl();
			this.labelControlFiles = new DevExpress.XtraEditors.LabelControl();
			this.labelControlTotalFolders = new DevExpress.XtraEditors.LabelControl();
			this.pnStatisticProgress = new System.Windows.Forms.Panel();
			this.panelExProgressStatistic = new DevComponents.DotNetBar.PanelEx();
			this.laStatisticProgressLable = new System.Windows.Forms.Label();
			this.circularProgressStatistic = new DevComponents.DotNetBar.Controls.CircularProgress();
			((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListSearchFiles)).BeginInit();
			this.pnKeyWord.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDateRange.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditKeyWord.Properties)).BeginInit();
			this.gbDateRange.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).BeginInit();
			this.pnTreeViewProgress.SuspendLayout();
			this.panelExProgressTreeView.SuspendLayout();
			this.pnRefresh.SuspendLayout();
			this.panelExRefresh.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlFiles)).BeginInit();
			this.xtraTabControlFiles.SuspendLayout();
			this.xtraTabPageRegular.SuspendLayout();
			this.xtraTabPageSearch.SuspendLayout();
			this.pnLeft.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.pnRight.SuspendLayout();
			this.pnPreview.SuspendLayout();
			this.xtraScrollableControlStatistic.SuspendLayout();
			this.pnStatisticProgress.SuspendLayout();
			this.panelExProgressStatistic.SuspendLayout();
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
			this.treeListAllFiles.Size = new System.Drawing.Size(589, 371);
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
			this.treeListSearchFiles.Location = new System.Drawing.Point(0, 146);
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
			this.treeListSearchFiles.Size = new System.Drawing.Size(589, 225);
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
			this.pnKeyWord.Controls.Add(this.simpleButtonSearch);
			this.pnKeyWord.Controls.Add(this.checkEditDateRange);
			this.pnKeyWord.Controls.Add(this.textEditKeyWord);
			this.pnKeyWord.Controls.Add(this.gbDateRange);
			this.pnKeyWord.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnKeyWord.Location = new System.Drawing.Point(0, 0);
			this.pnKeyWord.Name = "pnKeyWord";
			this.pnKeyWord.Size = new System.Drawing.Size(589, 146);
			this.pnKeyWord.TabIndex = 0;
			// 
			// simpleButtonSearch
			// 
			this.simpleButtonSearch.AllowFocus = false;
			this.simpleButtonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonSearch.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonSearch.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonSearch.Appearance.Options.UseFont = true;
			this.simpleButtonSearch.Appearance.Options.UseForeColor = true;
			this.simpleButtonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.simpleButtonSearch.Location = new System.Drawing.Point(480, 35);
			this.simpleButtonSearch.Name = "simpleButtonSearch";
			this.simpleButtonSearch.Size = new System.Drawing.Size(100, 27);
			this.simpleButtonSearch.TabIndex = 5;
			this.simpleButtonSearch.Text = "Search";
			this.simpleButtonSearch.Click += new System.EventHandler(this.btSearch_Click);
			// 
			// checkEditDateRange
			// 
			this.checkEditDateRange.Location = new System.Drawing.Point(6, 41);
			this.checkEditDateRange.Name = "checkEditDateRange";
			this.checkEditDateRange.Properties.AutoWidth = true;
			this.checkEditDateRange.Properties.Caption = "Set Date Range";
			this.checkEditDateRange.Size = new System.Drawing.Size(115, 21);
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
			this.textEditKeyWord.Size = new System.Drawing.Size(572, 22);
			this.textEditKeyWord.StyleController = this.styleController;
			this.textEditKeyWord.TabIndex = 3;
			this.textEditKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edKeyWord_KeyDown);
			// 
			// gbDateRange
			// 
			this.gbDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbDateRange.Controls.Add(this.dateEditEndDate);
			this.gbDateRange.Controls.Add(this.dateEditStartDate);
			this.gbDateRange.Controls.Add(this.laEndDate);
			this.gbDateRange.Controls.Add(this.laStartDate);
			this.gbDateRange.Location = new System.Drawing.Point(8, 64);
			this.gbDateRange.Name = "gbDateRange";
			this.gbDateRange.Size = new System.Drawing.Size(572, 71);
			this.gbDateRange.TabIndex = 2;
			this.gbDateRange.TabStop = false;
			// 
			// dateEditEndDate
			// 
			this.dateEditEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dateEditEndDate.EditValue = null;
			this.dateEditEndDate.Location = new System.Drawing.Point(446, 42);
			this.dateEditEndDate.Name = "dateEditEndDate";
			this.dateEditEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditEndDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditEndDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditEndDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.dateEditEndDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEndDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEndDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditEndDate.Properties.NullText = "Select";
			this.dateEditEndDate.Properties.ShowPopupShadow = false;
			this.dateEditEndDate.Properties.ShowToday = false;
			this.dateEditEndDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditEndDate.Properties.UseParentBackground = true;
			this.dateEditEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditEndDate.Size = new System.Drawing.Size(120, 22);
			this.dateEditEndDate.TabIndex = 7;
			// 
			// dateEditStartDate
			// 
			this.dateEditStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dateEditStartDate.EditValue = null;
			this.dateEditStartDate.Location = new System.Drawing.Point(446, 14);
			this.dateEditStartDate.Name = "dateEditStartDate";
			this.dateEditStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditStartDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditStartDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditStartDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.dateEditStartDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStartDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStartDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditStartDate.Properties.NullText = "Select";
			this.dateEditStartDate.Properties.ShowPopupShadow = false;
			this.dateEditStartDate.Properties.ShowToday = false;
			this.dateEditStartDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditStartDate.Properties.UseParentBackground = true;
			this.dateEditStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditStartDate.Size = new System.Drawing.Size(120, 22);
			this.dateEditStartDate.TabIndex = 6;
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
			// pnTreeViewProgress
			// 
			this.pnTreeViewProgress.Controls.Add(this.panelExProgressTreeView);
			this.pnTreeViewProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnTreeViewProgress.Location = new System.Drawing.Point(0, 447);
			this.pnTreeViewProgress.Name = "pnTreeViewProgress";
			this.pnTreeViewProgress.Padding = new System.Windows.Forms.Padding(5);
			this.pnTreeViewProgress.Size = new System.Drawing.Size(591, 53);
			this.pnTreeViewProgress.TabIndex = 5;
			// 
			// panelExProgressTreeView
			// 
			this.panelExProgressTreeView.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExProgressTreeView.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExProgressTreeView.Controls.Add(this.laTreeViewTreeViewProgressLable);
			this.panelExProgressTreeView.Controls.Add(this.circularProgressTreeView);
			this.panelExProgressTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExProgressTreeView.Location = new System.Drawing.Point(5, 5);
			this.panelExProgressTreeView.Name = "panelExProgressTreeView";
			this.panelExProgressTreeView.Padding = new System.Windows.Forms.Padding(5);
			this.panelExProgressTreeView.Size = new System.Drawing.Size(581, 43);
			this.panelExProgressTreeView.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExProgressTreeView.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.panelExProgressTreeView.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelExProgressTreeView.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelExProgressTreeView.Style.BorderColor.Color = System.Drawing.Color.White;
			this.panelExProgressTreeView.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelExProgressTreeView.Style.GradientAngle = 90;
			this.panelExProgressTreeView.TabIndex = 0;
			// 
			// laTreeViewTreeViewProgressLable
			// 
			this.laTreeViewTreeViewProgressLable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTreeViewTreeViewProgressLable.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTreeViewTreeViewProgressLable.Location = new System.Drawing.Point(68, 5);
			this.laTreeViewTreeViewProgressLable.Name = "laTreeViewTreeViewProgressLable";
			this.laTreeViewTreeViewProgressLable.Size = new System.Drawing.Size(508, 33);
			this.laTreeViewTreeViewProgressLable.TabIndex = 0;
			this.laTreeViewTreeViewProgressLable.Text = "label1";
			this.laTreeViewTreeViewProgressLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.circularProgressTreeView.Size = new System.Drawing.Size(63, 33);
			this.circularProgressTreeView.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressTreeView.TabIndex = 1;
			// 
			// pnRefresh
			// 
			this.pnRefresh.Controls.Add(this.panelExRefresh);
			this.pnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnRefresh.Location = new System.Drawing.Point(0, 0);
			this.pnRefresh.Name = "pnRefresh";
			this.pnRefresh.Padding = new System.Windows.Forms.Padding(5);
			this.pnRefresh.Size = new System.Drawing.Size(591, 50);
			this.pnRefresh.TabIndex = 6;
			// 
			// panelExRefresh
			// 
			this.panelExRefresh.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExRefresh.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExRefresh.Controls.Add(this.simpleButtonRefresh);
			this.panelExRefresh.Controls.Add(this.laDoubleClick);
			this.panelExRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExRefresh.Location = new System.Drawing.Point(5, 5);
			this.panelExRefresh.Name = "panelExRefresh";
			this.panelExRefresh.Padding = new System.Windows.Forms.Padding(5);
			this.panelExRefresh.Size = new System.Drawing.Size(581, 40);
			this.panelExRefresh.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExRefresh.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.panelExRefresh.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelExRefresh.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelExRefresh.Style.BorderColor.Color = System.Drawing.Color.White;
			this.panelExRefresh.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelExRefresh.Style.GradientAngle = 90;
			this.panelExRefresh.TabIndex = 1;
			// 
			// simpleButtonRefresh
			// 
			this.simpleButtonRefresh.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonRefresh.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonRefresh.Appearance.Options.UseFont = true;
			this.simpleButtonRefresh.Appearance.Options.UseForeColor = true;
			this.simpleButtonRefresh.Location = new System.Drawing.Point(4, 6);
			this.simpleButtonRefresh.Name = "simpleButtonRefresh";
			this.simpleButtonRefresh.Size = new System.Drawing.Size(75, 28);
			this.simpleButtonRefresh.StyleController = this.styleController;
			this.simpleButtonRefresh.TabIndex = 6;
			this.simpleButtonRefresh.Text = "Refresh";
			this.simpleButtonRefresh.Click += new System.EventHandler(this.Refresh_Click);
			// 
			// laDoubleClick
			// 
			this.laDoubleClick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laDoubleClick.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDoubleClick.Location = new System.Drawing.Point(85, 6);
			this.laDoubleClick.Name = "laDoubleClick";
			this.laDoubleClick.Size = new System.Drawing.Size(488, 28);
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
			this.xtraTabControlFiles.Location = new System.Drawing.Point(0, 50);
			this.xtraTabControlFiles.Name = "xtraTabControlFiles";
			this.xtraTabControlFiles.SelectedTabPage = this.xtraTabPageRegular;
			this.xtraTabControlFiles.Size = new System.Drawing.Size(591, 397);
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
			this.xtraTabPageRegular.Size = new System.Drawing.Size(589, 371);
			this.xtraTabPageRegular.Text = "Tree View";
			// 
			// xtraTabPageSearch
			// 
			this.xtraTabPageSearch.Controls.Add(this.treeListSearchFiles);
			this.xtraTabPageSearch.Controls.Add(this.pnKeyWord);
			this.xtraTabPageSearch.Name = "xtraTabPageSearch";
			this.xtraTabPageSearch.Size = new System.Drawing.Size(589, 371);
			this.xtraTabPageSearch.Text = "Search";
			// 
			// pnLeft
			// 
			this.pnLeft.Controls.Add(this.xtraTabControlFiles);
			this.pnLeft.Controls.Add(this.pnRefresh);
			this.pnLeft.Controls.Add(this.pnTreeViewProgress);
			this.pnLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnLeft.Location = new System.Drawing.Point(0, 0);
			this.pnLeft.Name = "pnLeft";
			this.pnLeft.Size = new System.Drawing.Size(591, 500);
			this.pnLeft.TabIndex = 8;
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.pnLeft);
			this.splitContainerControl.Panel1.MinSize = 300;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.pnRight);
			this.splitContainerControl.Panel2.MinSize = 200;
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(797, 500);
			this.splitContainerControl.SplitterPosition = 172;
			this.splitContainerControl.TabIndex = 9;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// pnRight
			// 
			this.pnRight.Controls.Add(this.pnPreview);
			this.pnRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnRight.Location = new System.Drawing.Point(0, 0);
			this.pnRight.Name = "pnRight";
			this.pnRight.Size = new System.Drawing.Size(200, 500);
			this.pnRight.TabIndex = 8;
			// 
			// pnPreview
			// 
			this.pnPreview.Controls.Add(this.xtraScrollableControlStatistic);
			this.pnPreview.Controls.Add(this.pnStatisticProgress);
			this.pnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnPreview.Location = new System.Drawing.Point(0, 0);
			this.pnPreview.Name = "pnPreview";
			this.pnPreview.Size = new System.Drawing.Size(200, 500);
			this.pnPreview.TabIndex = 4;
			// 
			// xtraScrollableControlStatistic
			// 
			this.xtraScrollableControlStatistic.Controls.Add(this.labelControlFiles);
			this.xtraScrollableControlStatistic.Controls.Add(this.labelControlTotalFolders);
			this.xtraScrollableControlStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlStatistic.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControlStatistic.Name = "xtraScrollableControlStatistic";
			this.xtraScrollableControlStatistic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.xtraScrollableControlStatistic.Size = new System.Drawing.Size(200, 447);
			this.xtraScrollableControlStatistic.TabIndex = 2;
			// 
			// labelControlFiles
			// 
			this.labelControlFiles.Appearance.Font = new System.Drawing.Font("Arial", 12F);
			this.labelControlFiles.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControlFiles.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlFiles.Location = new System.Drawing.Point(5, 50);
			this.labelControlFiles.Name = "labelControlFiles";
			this.labelControlFiles.Size = new System.Drawing.Size(195, 18);
			this.labelControlFiles.TabIndex = 2;
			this.labelControlFiles.Text = "Total Files:";
			// 
			// labelControlTotalFolders
			// 
			this.labelControlTotalFolders.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTotalFolders.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTotalFolders.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTotalFolders.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlTotalFolders.Location = new System.Drawing.Point(5, 0);
			this.labelControlTotalFolders.Name = "labelControlTotalFolders";
			this.labelControlTotalFolders.Size = new System.Drawing.Size(195, 50);
			this.labelControlTotalFolders.TabIndex = 3;
			this.labelControlTotalFolders.Text = "Total Folders:";
			// 
			// pnStatisticProgress
			// 
			this.pnStatisticProgress.Controls.Add(this.panelExProgressStatistic);
			this.pnStatisticProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnStatisticProgress.Location = new System.Drawing.Point(0, 447);
			this.pnStatisticProgress.Name = "pnStatisticProgress";
			this.pnStatisticProgress.Padding = new System.Windows.Forms.Padding(5);
			this.pnStatisticProgress.Size = new System.Drawing.Size(200, 53);
			this.pnStatisticProgress.TabIndex = 6;
			// 
			// panelExProgressStatistic
			// 
			this.panelExProgressStatistic.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExProgressStatistic.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExProgressStatistic.Controls.Add(this.laStatisticProgressLable);
			this.panelExProgressStatistic.Controls.Add(this.circularProgressStatistic);
			this.panelExProgressStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExProgressStatistic.Location = new System.Drawing.Point(5, 5);
			this.panelExProgressStatistic.Name = "panelExProgressStatistic";
			this.panelExProgressStatistic.Padding = new System.Windows.Forms.Padding(5);
			this.panelExProgressStatistic.Size = new System.Drawing.Size(190, 43);
			this.panelExProgressStatistic.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExProgressStatistic.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.panelExProgressStatistic.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelExProgressStatistic.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelExProgressStatistic.Style.BorderColor.Color = System.Drawing.Color.White;
			this.panelExProgressStatistic.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelExProgressStatistic.Style.GradientAngle = 90;
			this.panelExProgressStatistic.TabIndex = 0;
			// 
			// laStatisticProgressLable
			// 
			this.laStatisticProgressLable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laStatisticProgressLable.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laStatisticProgressLable.Location = new System.Drawing.Point(68, 5);
			this.laStatisticProgressLable.Name = "laStatisticProgressLable";
			this.laStatisticProgressLable.Size = new System.Drawing.Size(117, 33);
			this.laStatisticProgressLable.TabIndex = 0;
			this.laStatisticProgressLable.Text = "label1";
			this.laStatisticProgressLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// circularProgressStatistic
			// 
			this.circularProgressStatistic.AnimationSpeed = 50;
			// 
			// 
			// 
			this.circularProgressStatistic.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressStatistic.Dock = System.Windows.Forms.DockStyle.Left;
			this.circularProgressStatistic.FocusCuesEnabled = false;
			this.circularProgressStatistic.Location = new System.Drawing.Point(5, 5);
			this.circularProgressStatistic.Name = "circularProgressStatistic";
			this.circularProgressStatistic.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressStatistic.ProgressTextFormat = "";
			this.circularProgressStatistic.Size = new System.Drawing.Size(63, 33);
			this.circularProgressStatistic.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressStatistic.TabIndex = 1;
			// 
			// WallBinTreeListControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.splitContainerControl);
			this.Name = "WallBinTreeListControl";
			this.Size = new System.Drawing.Size(797, 500);
			((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListSearchFiles)).EndInit();
			this.pnKeyWord.ResumeLayout(false);
			this.pnKeyWord.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDateRange.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditKeyWord.Properties)).EndInit();
			this.gbDateRange.ResumeLayout(false);
			this.gbDateRange.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).EndInit();
			this.pnTreeViewProgress.ResumeLayout(false);
			this.panelExProgressTreeView.ResumeLayout(false);
			this.pnRefresh.ResumeLayout(false);
			this.panelExRefresh.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlFiles)).EndInit();
			this.xtraTabControlFiles.ResumeLayout(false);
			this.xtraTabPageRegular.ResumeLayout(false);
			this.xtraTabPageSearch.ResumeLayout(false);
			this.pnLeft.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.pnRight.ResumeLayout(false);
			this.pnPreview.ResumeLayout(false);
			this.xtraScrollableControlStatistic.ResumeLayout(false);
			this.pnStatisticProgress.ResumeLayout(false);
			this.panelExProgressStatistic.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnKeyWord;
        private System.Windows.Forms.Panel pnTreeViewProgress;
        private System.Windows.Forms.Label laTreeViewTreeViewProgressLable;
        private System.Windows.Forms.Panel pnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tmiOpen;
        private System.Windows.Forms.Label laDoubleClick;
        private System.Windows.Forms.GroupBox gbDateRange;
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
        private System.Windows.Forms.Panel pnLeft;
        private DevComponents.DotNetBar.PanelEx panelExProgressTreeView;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgressTreeView;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSearch;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.CheckEdit checkEditDateRange;
        private DevExpress.XtraEditors.TextEdit textEditKeyWord;
        private DevExpress.XtraEditors.DateEdit dateEditEndDate;
        private DevExpress.XtraEditors.DateEdit dateEditStartDate;
        private DevComponents.DotNetBar.PanelEx panelExRefresh;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRefresh;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnPreview;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlStatistic;
        private DevExpress.XtraEditors.LabelControl labelControlFiles;
        private DevExpress.XtraEditors.LabelControl labelControlTotalFolders;
        private System.Windows.Forms.Panel pnStatisticProgress;
        private DevComponents.DotNetBar.PanelEx panelExProgressStatistic;
        private System.Windows.Forms.Label laStatisticProgressLable;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgressStatistic;
    }
}
