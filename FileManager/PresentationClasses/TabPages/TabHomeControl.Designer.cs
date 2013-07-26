namespace FileManager.PresentationClasses.TabPages
{
    partial class TabHomeControl
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
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem7 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem8 = new DevExpress.Utils.ToolTipItem();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
			this.dockPanelTreeView = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanelTreeView_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.btSetupWallBin = new System.Windows.Forms.Button();
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barMinibar = new DevExpress.XtraBars.Bar();
			this.barButtonItemSeparator1 = new DevExpress.XtraBars.BarButtonItem();
			this.barCheckItemTabs = new DevExpress.XtraBars.BarCheckItem();
			this.barButtonItemFontUp = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemFontDown = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkUp = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkDown = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemSeparator2 = new DevExpress.XtraBars.BarButtonItem();
			this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.pnHeader = new FileManager.PresentationClasses.WallBin.WallbinHeaderPanel();
			this.labelControlSelectedLink = new DevExpress.XtraEditors.LabelControl();
			this.SuperFilterControl = new FileManager.PresentationClasses.WallBin.SuperFilterControl();
			this.labelControlTagCountInfo = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
			this.dockPanelTreeView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
			this.pnHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockManager
			// 
			this.dockManager.DockingOptions.FloatOnDblClick = false;
			this.dockManager.DockingOptions.ShowMaximizeButton = false;
			this.dockManager.Form = this;
			this.dockManager.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelTreeView});
			this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevComponents.DotNetBar.RibbonControl",
            "FileManager.PresentationClasses.WallBin.WallbinHeaderPanel"});
			this.dockManager.ClosedPanel += new DevExpress.XtraBars.Docking.DockPanelEventHandler(this.dockPanelTreeView_ClosedPanel);
			this.dockManager.Sizing += new DevExpress.XtraBars.Docking.SizingEventHandler(this.dockManager_Sizing);
			// 
			// dockPanelTreeView
			// 
			this.dockPanelTreeView.Controls.Add(this.dockPanelTreeView_Container);
			this.dockPanelTreeView.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
			this.dockPanelTreeView.FloatSize = new System.Drawing.Size(350, 450);
			this.dockPanelTreeView.ID = new System.Guid("0ddf2383-2c01-4ebf-a2d6-02369d6da8ce");
			this.dockPanelTreeView.Location = new System.Drawing.Point(0, 0);
			this.dockPanelTreeView.Name = "dockPanelTreeView";
			this.dockPanelTreeView.Options.AllowDockBottom = false;
			this.dockPanelTreeView.Options.AllowDockFill = false;
			this.dockPanelTreeView.Options.AllowDockRight = false;
			this.dockPanelTreeView.Options.AllowDockTop = false;
			this.dockPanelTreeView.Options.ShowAutoHideButton = false;
			this.dockPanelTreeView.Options.ShowMaximizeButton = false;
			this.dockPanelTreeView.OriginalSize = new System.Drawing.Size(309, 200);
			this.dockPanelTreeView.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
			this.dockPanelTreeView.SavedIndex = 0;
			this.dockPanelTreeView.Size = new System.Drawing.Size(309, 317);
			this.dockPanelTreeView.Text = "Drag & Drop!";
			this.dockPanelTreeView.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
			this.dockPanelTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dockPanelTreeView_MouseDoubleClick);
			// 
			// dockPanelTreeView_Container
			// 
			this.dockPanelTreeView_Container.Location = new System.Drawing.Point(2, 24);
			this.dockPanelTreeView_Container.Name = "dockPanelTreeView_Container";
			this.dockPanelTreeView_Container.Size = new System.Drawing.Size(305, 291);
			this.dockPanelTreeView_Container.TabIndex = 0;
			// 
			// pnEmpty
			// 
			this.pnEmpty.Location = new System.Drawing.Point(16, 18);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(63, 56);
			this.pnEmpty.TabIndex = 29;
			// 
			// pnMain
			// 
			this.pnMain.Location = new System.Drawing.Point(104, 18);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(65, 56);
			this.pnMain.TabIndex = 28;
			// 
			// btSetupWallBin
			// 
			this.btSetupWallBin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btSetupWallBin.BackColor = System.Drawing.Color.White;
			this.btSetupWallBin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btSetupWallBin.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btSetupWallBin.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btSetupWallBin.Image = global::FileManager.Properties.Resources.settings;
			this.btSetupWallBin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btSetupWallBin.Location = new System.Drawing.Point(318, 129);
			this.btSetupWallBin.Name = "btSetupWallBin";
			this.btSetupWallBin.Size = new System.Drawing.Size(264, 58);
			this.btSetupWallBin.TabIndex = 27;
			this.btSetupWallBin.Text = "Set Up Wall Bin";
			this.btSetupWallBin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btSetupWallBin.UseVisualStyleBackColor = false;
			this.btSetupWallBin.Visible = false;
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 47);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.MinSize = 350;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.pnMain);
			this.splitContainerControl.Panel2.Controls.Add(this.pnEmpty);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(900, 243);
			this.splitContainerControl.SplitterPosition = 350;
			this.splitContainerControl.TabIndex = 32;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// barManager
			// 
			this.barManager.AllowCustomization = false;
			this.barManager.AllowQuickCustomization = false;
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMinibar});
			this.barManager.Controller = this.barAndDockingController;
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.DockManager = this.dockManager;
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCheckItemTabs,
            this.barButtonItemSeparator1,
            this.barButtonItemFontUp,
            this.barButtonItemFontDown,
            this.barButtonItemSeparator2,
            this.barButtonItemLinkUp,
            this.barButtonItemLinkDown});
			this.barManager.MaxItemId = 7;
			this.barManager.ToolTipController = this.toolTipController;
			// 
			// barMinibar
			// 
			this.barMinibar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.barMinibar.Appearance.Options.UseBackColor = true;
			this.barMinibar.BarName = "Tools";
			this.barMinibar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.barMinibar.DockCol = 0;
			this.barMinibar.DockRow = 0;
			this.barMinibar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.barMinibar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSeparator1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemTabs, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemFontUp, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemFontDown),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkUp, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkDown),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSeparator2, true)});
			this.barMinibar.OptionsBar.AllowQuickCustomization = false;
			this.barMinibar.OptionsBar.DisableClose = true;
			this.barMinibar.OptionsBar.DisableCustomization = true;
			this.barMinibar.OptionsBar.DrawDragBorder = false;
			this.barMinibar.Text = "Minibar";
			// 
			// barButtonItemSeparator1
			// 
			this.barButtonItemSeparator1.Enabled = false;
			this.barButtonItemSeparator1.Id = 1;
			this.barButtonItemSeparator1.Name = "barButtonItemSeparator1";
			// 
			// barCheckItemTabs
			// 
			this.barCheckItemTabs.Caption = "Tabs";
			this.barCheckItemTabs.Glyph = global::FileManager.Properties.Resources.TabsView;
			this.barCheckItemTabs.Id = 0;
			this.barCheckItemTabs.Name = "barCheckItemTabs";
			toolTipItem4.Text = "Tabs View";
			superToolTip4.Items.Add(toolTipItem4);
			this.barCheckItemTabs.SuperTip = superToolTip4;
			// 
			// barButtonItemFontUp
			// 
			this.barButtonItemFontUp.Caption = "Font Up";
			this.barButtonItemFontUp.Glyph = global::FileManager.Properties.Resources.MinibarFontLarger;
			this.barButtonItemFontUp.Id = 2;
			this.barButtonItemFontUp.Name = "barButtonItemFontUp";
			toolTipItem5.Text = "Increase Text Size";
			superToolTip5.Items.Add(toolTipItem5);
			this.barButtonItemFontUp.SuperTip = superToolTip5;
			// 
			// barButtonItemFontDown
			// 
			this.barButtonItemFontDown.Caption = "Font Down";
			this.barButtonItemFontDown.Glyph = global::FileManager.Properties.Resources.MinibarFontSmaller;
			this.barButtonItemFontDown.Id = 3;
			this.barButtonItemFontDown.Name = "barButtonItemFontDown";
			toolTipItem6.Text = "Decrease Text Size";
			superToolTip6.Items.Add(toolTipItem6);
			this.barButtonItemFontDown.SuperTip = superToolTip6;
			// 
			// barButtonItemLinkUp
			// 
			this.barButtonItemLinkUp.Caption = "Link Up";
			this.barButtonItemLinkUp.Enabled = false;
			this.barButtonItemLinkUp.Glyph = global::FileManager.Properties.Resources.MinibarLinkUp;
			this.barButtonItemLinkUp.Id = 5;
			this.barButtonItemLinkUp.Name = "barButtonItemLinkUp";
			toolTipItem7.Text = "Move this Link Up";
			superToolTip7.Items.Add(toolTipItem7);
			this.barButtonItemLinkUp.SuperTip = superToolTip7;
			// 
			// barButtonItemLinkDown
			// 
			this.barButtonItemLinkDown.Caption = "Link Down";
			this.barButtonItemLinkDown.Enabled = false;
			this.barButtonItemLinkDown.Glyph = global::FileManager.Properties.Resources.MinibarLinkDown;
			this.barButtonItemLinkDown.Id = 6;
			this.barButtonItemLinkDown.Name = "barButtonItemLinkDown";
			toolTipItem8.Text = "Move this Link Down";
			superToolTip8.Items.Add(toolTipItem8);
			this.barButtonItemLinkDown.SuperTip = superToolTip8;
			// 
			// barButtonItemSeparator2
			// 
			this.barButtonItemSeparator2.Enabled = false;
			this.barButtonItemSeparator2.Id = 4;
			this.barButtonItemSeparator2.Name = "barButtonItemSeparator2";
			// 
			// barAndDockingController
			// 
			this.barAndDockingController.PaintStyleName = "Office2003";
			this.barAndDockingController.PropertiesBar.AllowLinkLighting = false;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(900, 0);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.barDockControlBottom.Appearance.Options.UseBackColor = true;
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 290);
			this.barDockControlBottom.Size = new System.Drawing.Size(900, 27);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 290);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(900, 0);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 290);
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipLocation = DevExpress.Utils.ToolTipLocation.TopRight;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// pnHeader
			// 
			this.pnHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnHeader.Controls.Add(this.labelControlSelectedLink);
			this.pnHeader.Controls.Add(this.SuperFilterControl);
			this.pnHeader.Controls.Add(this.labelControlTagCountInfo);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(900, 47);
			this.pnHeader.TabIndex = 37;
			// 
			// labelControlSelectedLink
			// 
			this.labelControlSelectedLink.AllowHtmlString = true;
			this.labelControlSelectedLink.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSelectedLink.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlSelectedLink.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlSelectedLink.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlSelectedLink.Location = new System.Drawing.Point(161, 0);
			this.labelControlSelectedLink.Name = "labelControlSelectedLink";
			this.labelControlSelectedLink.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.labelControlSelectedLink.Size = new System.Drawing.Size(339, 43);
			this.labelControlSelectedLink.TabIndex = 0;
			this.labelControlSelectedLink.Text = "labelControl1";
			// 
			// SuperFilterControl
			// 
			this.SuperFilterControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.SuperFilterControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.SuperFilterControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SuperFilterControl.Location = new System.Drawing.Point(0, 0);
			this.SuperFilterControl.Name = "SuperFilterControl";
			this.SuperFilterControl.SelectedLink = null;
			this.SuperFilterControl.Size = new System.Drawing.Size(161, 43);
			this.SuperFilterControl.TabIndex = 2;
			// 
			// labelControlTagCountInfo
			// 
			this.labelControlTagCountInfo.AllowHtmlString = true;
			this.labelControlTagCountInfo.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTagCountInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.labelControlTagCountInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTagCountInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTagCountInfo.Dock = System.Windows.Forms.DockStyle.Right;
			this.labelControlTagCountInfo.Location = new System.Drawing.Point(500, 0);
			this.labelControlTagCountInfo.Name = "labelControlTagCountInfo";
			this.labelControlTagCountInfo.Size = new System.Drawing.Size(396, 43);
			this.labelControlTagCountInfo.TabIndex = 1;
			this.labelControlTagCountInfo.Text = "You need to start TAGGING your Links!";
			// 
			// TabHomeControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.splitContainerControl);
			this.Controls.Add(this.pnHeader);
			this.Controls.Add(this.btSetupWallBin);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "TabHomeControl";
			this.Size = new System.Drawing.Size(900, 317);
			this.Resize += new System.EventHandler(this.TabHomeControl_Resize);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
			this.dockPanelTreeView.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
			this.pnHeader.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        public System.Windows.Forms.Panel pnEmpty;
        public System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Button btSetupWallBin;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelTreeView;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanelTreeView_Container;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.Bar barMinibar;
		private DevExpress.XtraBars.BarButtonItem barButtonItemSeparator1;
		private DevExpress.XtraBars.BarButtonItem barButtonItemSeparator2;
		private DevExpress.Utils.ToolTipController toolTipController;
		private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
		public DevExpress.XtraBars.BarCheckItem barCheckItemTabs;
		public DevExpress.XtraBars.BarButtonItem barButtonItemFontUp;
		public DevExpress.XtraBars.BarButtonItem barButtonItemFontDown;
		private FileManager.PresentationClasses.WallBin.WallbinHeaderPanel pnHeader;
		private DevExpress.XtraEditors.LabelControl labelControlSelectedLink;
		private DevExpress.XtraEditors.LabelControl labelControlTagCountInfo;
		public WallBin.SuperFilterControl SuperFilterControl;
		public DevExpress.XtraBars.BarButtonItem barButtonItemLinkUp;
		public DevExpress.XtraBars.BarButtonItem barButtonItemLinkDown;
    }
}
