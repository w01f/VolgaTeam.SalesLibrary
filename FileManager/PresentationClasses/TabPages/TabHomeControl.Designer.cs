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
			DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem7 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem8 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip9 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem9 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip10 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem10 = new DevExpress.Utils.ToolTipItem();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barMinibar = new DevExpress.XtraBars.Bar();
			this.barButtonItemSeparator1 = new DevExpress.XtraBars.BarButtonItem();
			this.barCheckItemTabs = new DevExpress.XtraBars.BarCheckItem();
			this.barButtonItemFontUp = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemFontDown = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkUp = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkDown = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemSeparator2 = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.btSetupWallBin = new System.Windows.Forms.Button();
			this.pnHeader = new FileManager.PresentationClasses.WallBin.WallbinHeaderPanel();
			this.labelControlSelectedLink = new DevExpress.XtraEditors.LabelControl();
			this.SuperFilterControl = new FileManager.PresentationClasses.WallBin.SuperFilterControl();
			this.labelControlTagCountInfo = new DevExpress.XtraEditors.LabelControl();
			this.retractableBar = new SalesDepot.CommonGUI.RetractableBar.RetractableBarLeft();
			this.pnContainer = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			this.pnHeader.SuspendLayout();
			this.pnContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// barManager
			// 
			this.barManager.AllowCustomization = false;
			this.barManager.AllowQuickCustomization = false;
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMinibar});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this.pnContainer;
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
			toolTipItem6.Text = "Tabs View";
			superToolTip6.Items.Add(toolTipItem6);
			this.barCheckItemTabs.SuperTip = superToolTip6;
			// 
			// barButtonItemFontUp
			// 
			this.barButtonItemFontUp.Caption = "Font Up";
			this.barButtonItemFontUp.Glyph = global::FileManager.Properties.Resources.MinibarFontLarger;
			this.barButtonItemFontUp.Id = 2;
			this.barButtonItemFontUp.Name = "barButtonItemFontUp";
			toolTipItem7.Text = "Increase Text Size";
			superToolTip7.Items.Add(toolTipItem7);
			this.barButtonItemFontUp.SuperTip = superToolTip7;
			// 
			// barButtonItemFontDown
			// 
			this.barButtonItemFontDown.Caption = "Font Down";
			this.barButtonItemFontDown.Glyph = global::FileManager.Properties.Resources.MinibarFontSmaller;
			this.barButtonItemFontDown.Id = 3;
			this.barButtonItemFontDown.Name = "barButtonItemFontDown";
			toolTipItem8.Text = "Decrease Text Size";
			superToolTip8.Items.Add(toolTipItem8);
			this.barButtonItemFontDown.SuperTip = superToolTip8;
			// 
			// barButtonItemLinkUp
			// 
			this.barButtonItemLinkUp.Caption = "Link Up";
			this.barButtonItemLinkUp.Enabled = false;
			this.barButtonItemLinkUp.Glyph = global::FileManager.Properties.Resources.MinibarLinkUp;
			this.barButtonItemLinkUp.Id = 5;
			this.barButtonItemLinkUp.Name = "barButtonItemLinkUp";
			toolTipItem9.Text = "Move this Link Up";
			superToolTip9.Items.Add(toolTipItem9);
			this.barButtonItemLinkUp.SuperTip = superToolTip9;
			// 
			// barButtonItemLinkDown
			// 
			this.barButtonItemLinkDown.Caption = "Link Down";
			this.barButtonItemLinkDown.Enabled = false;
			this.barButtonItemLinkDown.Glyph = global::FileManager.Properties.Resources.MinibarLinkDown;
			this.barButtonItemLinkDown.Id = 6;
			this.barButtonItemLinkDown.Name = "barButtonItemLinkDown";
			toolTipItem10.Text = "Move this Link Down";
			superToolTip10.Items.Add(toolTipItem10);
			this.barButtonItemLinkDown.SuperTip = superToolTip10;
			// 
			// barButtonItemSeparator2
			// 
			this.barButtonItemSeparator2.Enabled = false;
			this.barButtonItemSeparator2.Id = 4;
			this.barButtonItemSeparator2.Name = "barButtonItemSeparator2";
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(624, 0);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 497);
			this.barDockControlBottom.Size = new System.Drawing.Size(624, 31);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 497);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(624, 0);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 497);
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipLocation = DevExpress.Utils.ToolTipLocation.TopRight;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// pnEmpty
			// 
			this.pnEmpty.BackColor = System.Drawing.Color.Transparent;
			this.pnEmpty.Location = new System.Drawing.Point(313, 212);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(63, 56);
			this.pnEmpty.TabIndex = 29;
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Location = new System.Drawing.Point(401, 212);
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
			// pnHeader
			// 
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
			this.labelControlSelectedLink.Size = new System.Drawing.Size(343, 47);
			this.labelControlSelectedLink.TabIndex = 0;
			this.labelControlSelectedLink.Text = "labelControl1";
			// 
			// SuperFilterControl
			// 
			this.SuperFilterControl.BackColor = System.Drawing.Color.White;
			this.SuperFilterControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.SuperFilterControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SuperFilterControl.Location = new System.Drawing.Point(0, 0);
			this.SuperFilterControl.Name = "SuperFilterControl";
			this.SuperFilterControl.SelectedLink = null;
			this.SuperFilterControl.Size = new System.Drawing.Size(161, 47);
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
			this.labelControlTagCountInfo.Location = new System.Drawing.Point(504, 0);
			this.labelControlTagCountInfo.Name = "labelControlTagCountInfo";
			this.labelControlTagCountInfo.Size = new System.Drawing.Size(396, 47);
			this.labelControlTagCountInfo.TabIndex = 1;
			this.labelControlTagCountInfo.Text = "You need to start TAGGING your Links!";
			// 
			// retractableBar
			// 
			this.retractableBar.AnimationDelay = 0;
			this.retractableBar.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBar.Content
			// 
			this.retractableBar.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBar.Content.Name = "Content";
			this.retractableBar.Content.Size = new System.Drawing.Size(272, 484);
			this.retractableBar.Content.TabIndex = 1;
			this.retractableBar.ContentSize = 350;
			this.retractableBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.retractableBar.Location = new System.Drawing.Point(0, 47);
			this.retractableBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBar.Name = "retractableBar";
			this.retractableBar.Size = new System.Drawing.Size(276, 528);
			this.retractableBar.TabIndex = 42;
			// 
			// pnContainer
			// 
			this.pnContainer.Controls.Add(this.pnEmpty);
			this.pnContainer.Controls.Add(this.pnMain);
			this.pnContainer.Controls.Add(this.barDockControlLeft);
			this.pnContainer.Controls.Add(this.barDockControlRight);
			this.pnContainer.Controls.Add(this.barDockControlBottom);
			this.pnContainer.Controls.Add(this.barDockControlTop);
			this.pnContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContainer.Location = new System.Drawing.Point(276, 47);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(624, 528);
			this.pnContainer.TabIndex = 43;
			this.pnContainer.Resize += new System.EventHandler(this.Container_Resize);
			// 
			// TabHomeControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnContainer);
			this.Controls.Add(this.retractableBar);
			this.Controls.Add(this.pnHeader);
			this.Controls.Add(this.btSetupWallBin);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "TabHomeControl";
			this.Size = new System.Drawing.Size(900, 575);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			this.pnHeader.ResumeLayout(false);
			this.pnContainer.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
        public System.Windows.Forms.Panel pnEmpty;
        public System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Button btSetupWallBin;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.Bar barMinibar;
		private DevExpress.XtraBars.BarButtonItem barButtonItemSeparator1;
		private DevExpress.XtraBars.BarButtonItem barButtonItemSeparator2;
		private DevExpress.Utils.ToolTipController toolTipController;
		public DevExpress.XtraBars.BarCheckItem barCheckItemTabs;
		public DevExpress.XtraBars.BarButtonItem barButtonItemFontUp;
		public DevExpress.XtraBars.BarButtonItem barButtonItemFontDown;
		private FileManager.PresentationClasses.WallBin.WallbinHeaderPanel pnHeader;
		private DevExpress.XtraEditors.LabelControl labelControlSelectedLink;
		private DevExpress.XtraEditors.LabelControl labelControlTagCountInfo;
		public WallBin.SuperFilterControl SuperFilterControl;
		public DevExpress.XtraBars.BarButtonItem barButtonItemLinkUp;
		public DevExpress.XtraBars.BarButtonItem barButtonItemLinkDown;
		private SalesDepot.CommonGUI.RetractableBar.RetractableBarLeft retractableBar;
		private System.Windows.Forms.Panel pnContainer;
    }
}
