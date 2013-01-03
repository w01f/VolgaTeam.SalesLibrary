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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
			this.dockPanelTreeView = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanelTreeView_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.btSetupWallBin = new System.Windows.Forms.Button();
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
			this.dockPanelTreeView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
            "DevComponents.DotNetBar.RibbonControl"});
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
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.MinSize = 350;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.pnMain);
			this.splitContainerControl.Panel2.Controls.Add(this.pnEmpty);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(900, 317);
			this.splitContainerControl.TabIndex = 32;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// TabHomeControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.splitContainerControl);
			this.Controls.Add(this.btSetupWallBin);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "TabHomeControl";
			this.Size = new System.Drawing.Size(900, 317);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
			this.dockPanelTreeView.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        public System.Windows.Forms.Panel pnEmpty;
        public System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Button btSetupWallBin;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelTreeView;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanelTreeView_Container;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
    }
}
