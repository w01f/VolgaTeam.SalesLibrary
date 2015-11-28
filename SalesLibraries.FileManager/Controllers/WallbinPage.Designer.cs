namespace SalesLibraries.FileManager.Controllers
{
	sealed partial class WallbinPage
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
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barMinibar = new DevExpress.XtraBars.Bar();
			this.barButtonItemSeparator1 = new DevExpress.XtraBars.BarButtonItem();
			this.barCheckItemTabs = new DevExpress.XtraBars.BarCheckItem();
			this.barButtonItemFontUp = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemFontDown = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkUp = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkDown = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemSeparator2 = new DevExpress.XtraBars.BarButtonItem();
			this.retractableBar = new SalesLibraries.CommonGUI.RetractableBar.RetractableBarLeft();
			this.laEditorTitle = new System.Windows.Forms.Label();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.linkInfoControl = new SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl();
			this.pnTagInfoContainer = new System.Windows.Forms.Panel();
			this.superFilterControl = new SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings.SuperFilterControl();
			this.pnMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			this.retractableBar.Header.SuspendLayout();
			this.pnHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnContainer
			// 
			this.pnContainer.BackColor = System.Drawing.Color.White;
			this.pnContainer.Location = new System.Drawing.Point(33, 49);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(199, 158);
			this.pnContainer.TabIndex = 0;
			// 
			// pnEmpty
			// 
			this.pnEmpty.BackColor = System.Drawing.Color.White;
			this.pnEmpty.Location = new System.Drawing.Point(33, 259);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(199, 141);
			this.pnEmpty.TabIndex = 1;
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pnContainer);
			this.pnMain.Controls.Add(this.pnEmpty);
			this.pnMain.Controls.Add(this.barDockControlLeft);
			this.pnMain.Controls.Add(this.barDockControlRight);
			this.pnMain.Controls.Add(this.barDockControlBottom);
			this.pnMain.Controls.Add(this.barDockControlTop);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(312, 47);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(676, 647);
			this.pnMain.TabIndex = 2;
			this.pnMain.Resize += new System.EventHandler(this.OnMinibarResize);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 613);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(676, 0);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 613);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 613);
			this.barDockControlBottom.Size = new System.Drawing.Size(676, 34);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(676, 0);
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
			this.barManager.Form = this.pnMain;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCheckItemTabs,
            this.barButtonItemSeparator1,
            this.barButtonItemFontUp,
            this.barButtonItemFontDown,
            this.barButtonItemSeparator2,
            this.barButtonItemLinkUp,
            this.barButtonItemLinkDown});
			this.barManager.MaxItemId = 7;
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
			this.barCheckItemTabs.Glyph = global::SalesLibraries.FileManager.Properties.Resources.MinibarTabsView;
			this.barCheckItemTabs.Id = 0;
			this.barCheckItemTabs.Name = "barCheckItemTabs";
			toolTipItem1.Text = "Tabs View";
			superToolTip1.Items.Add(toolTipItem1);
			this.barCheckItemTabs.SuperTip = superToolTip1;
			// 
			// barButtonItemFontUp
			// 
			this.barButtonItemFontUp.Caption = "Font Up";
			this.barButtonItemFontUp.Glyph = global::SalesLibraries.FileManager.Properties.Resources.MinibarFontLarger;
			this.barButtonItemFontUp.Id = 2;
			this.barButtonItemFontUp.Name = "barButtonItemFontUp";
			toolTipItem2.Text = "Increase Text Size";
			superToolTip2.Items.Add(toolTipItem2);
			this.barButtonItemFontUp.SuperTip = superToolTip2;
			// 
			// barButtonItemFontDown
			// 
			this.barButtonItemFontDown.Caption = "Font Down";
			this.barButtonItemFontDown.Glyph = global::SalesLibraries.FileManager.Properties.Resources.MinibarFontSmaller;
			this.barButtonItemFontDown.Id = 3;
			this.barButtonItemFontDown.Name = "barButtonItemFontDown";
			toolTipItem3.Text = "Decrease Text Size";
			superToolTip3.Items.Add(toolTipItem3);
			this.barButtonItemFontDown.SuperTip = superToolTip3;
			// 
			// barButtonItemLinkUp
			// 
			this.barButtonItemLinkUp.Caption = "Link Up";
			this.barButtonItemLinkUp.Enabled = false;
			this.barButtonItemLinkUp.Glyph = global::SalesLibraries.FileManager.Properties.Resources.MinibarLinkUp;
			this.barButtonItemLinkUp.Id = 5;
			this.barButtonItemLinkUp.Name = "barButtonItemLinkUp";
			toolTipItem4.Text = "Move this Link Up";
			superToolTip4.Items.Add(toolTipItem4);
			this.barButtonItemLinkUp.SuperTip = superToolTip4;
			// 
			// barButtonItemLinkDown
			// 
			this.barButtonItemLinkDown.Caption = "Link Down";
			this.barButtonItemLinkDown.Enabled = false;
			this.barButtonItemLinkDown.Glyph = global::SalesLibraries.FileManager.Properties.Resources.MinibarLinkDown;
			this.barButtonItemLinkDown.Id = 6;
			this.barButtonItemLinkDown.Name = "barButtonItemLinkDown";
			toolTipItem5.Text = "Move this Link Down";
			superToolTip5.Items.Add(toolTipItem5);
			this.barButtonItemLinkDown.SuperTip = superToolTip5;
			// 
			// barButtonItemSeparator2
			// 
			this.barButtonItemSeparator2.Enabled = false;
			this.barButtonItemSeparator2.Id = 4;
			this.barButtonItemSeparator2.Name = "barButtonItemSeparator2";
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
			this.retractableBar.Content.Size = new System.Drawing.Size(308, 603);
			this.retractableBar.Content.TabIndex = 1;
			this.retractableBar.ContentSize = 350;
			this.retractableBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBar.Header
			// 
			this.retractableBar.Header.Controls.Add(this.laEditorTitle);
			this.retractableBar.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBar.Header.Name = "Header";
			this.retractableBar.Header.Size = new System.Drawing.Size(257, 36);
			this.retractableBar.Header.TabIndex = 2;
			this.retractableBar.Location = new System.Drawing.Point(0, 47);
			this.retractableBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBar.Name = "retractableBar";
			this.retractableBar.Size = new System.Drawing.Size(312, 647);
			this.retractableBar.TabIndex = 7;
			// 
			// laEditorTitle
			// 
			this.laEditorTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laEditorTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laEditorTitle.Location = new System.Drawing.Point(0, 0);
			this.laEditorTitle.Name = "laEditorTitle";
			this.laEditorTitle.Size = new System.Drawing.Size(257, 36);
			this.laEditorTitle.TabIndex = 1;
			this.laEditorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnHeader
			// 
			this.pnHeader.BackColor = System.Drawing.Color.White;
			this.pnHeader.Controls.Add(this.linkInfoControl);
			this.pnHeader.Controls.Add(this.pnTagInfoContainer);
			this.pnHeader.Controls.Add(this.superFilterControl);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(988, 47);
			this.pnHeader.TabIndex = 8;
			// 
			// linkInfoControl
			// 
			this.linkInfoControl.AllowHtmlString = true;
			this.linkInfoControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkInfoControl.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
			this.linkInfoControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.linkInfoControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.linkInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkInfoControl.Location = new System.Drawing.Point(196, 0);
			this.linkInfoControl.Name = "linkInfoControl";
			this.linkInfoControl.Size = new System.Drawing.Size(422, 47);
			this.linkInfoControl.TabIndex = 2;
			// 
			// pnTagInfoContainer
			// 
			this.pnTagInfoContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnTagInfoContainer.Location = new System.Drawing.Point(618, 0);
			this.pnTagInfoContainer.Name = "pnTagInfoContainer";
			this.pnTagInfoContainer.Size = new System.Drawing.Size(370, 47);
			this.pnTagInfoContainer.TabIndex = 1;
			// 
			// superFilterControl
			// 
			this.superFilterControl.BackColor = System.Drawing.Color.White;
			this.superFilterControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.superFilterControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superFilterControl.Location = new System.Drawing.Point(0, 0);
			this.superFilterControl.Name = "superFilterControl";
			this.superFilterControl.Size = new System.Drawing.Size(196, 47);
			this.superFilterControl.TabIndex = 0;
			// 
			// WallbinPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.retractableBar);
			this.Controls.Add(this.pnHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WallbinPage";
			this.Size = new System.Drawing.Size(988, 694);
			this.pnMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			this.retractableBar.Header.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnContainer;
		private System.Windows.Forms.Panel pnEmpty;
		private System.Windows.Forms.Panel pnMain;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.Bar barMinibar;
		private DevExpress.XtraBars.BarButtonItem barButtonItemSeparator1;
		public DevExpress.XtraBars.BarCheckItem barCheckItemTabs;
		public DevExpress.XtraBars.BarButtonItem barButtonItemFontUp;
		public DevExpress.XtraBars.BarButtonItem barButtonItemFontDown;
		public DevExpress.XtraBars.BarButtonItem barButtonItemLinkUp;
		public DevExpress.XtraBars.BarButtonItem barButtonItemLinkDown;
		private DevExpress.XtraBars.BarButtonItem barButtonItemSeparator2;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBar;
		private System.Windows.Forms.Label laEditorTitle;
		private System.Windows.Forms.Panel pnHeader;
		private PresentationLayer.Wallbin.Links.SingleSettings.SuperFilterControl superFilterControl;
		private System.Windows.Forms.Panel pnTagInfoContainer;
		private PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl linkInfoControl;
	}
}
