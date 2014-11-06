namespace FileManager.PresentationClasses.WallBin
{
    partial class MultitabLibraryControl
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
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.contextMenuStripPageProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemDeleteLinks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteSecurity = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteTags = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteWidgets = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteBanners = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.contextMenuStripPageProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Hide;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Hide;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Hide;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Hide;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Hide;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.Size = new System.Drawing.Size(506, 386);
			this.xtraTabControl.TabIndex = 0;
			this.xtraTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabControl_MouseDown);
			// 
			// pnEmpty
			// 
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(0, 0);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(506, 386);
			this.pnEmpty.TabIndex = 1;
			// 
			// contextMenuStripPageProperties
			// 
			this.contextMenuStripPageProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteLinks,
            this.toolStripMenuItemDeleteSecurity,
            this.toolStripMenuItemDeleteTags,
            this.toolStripMenuItemDeleteWidgets,
            this.toolStripMenuItemDeleteBanners});
			this.contextMenuStripPageProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripPageProperties.Size = new System.Drawing.Size(337, 136);
			// 
			// toolStripMenuItemDeleteLinks
			// 
			this.toolStripMenuItemDeleteLinks.Name = "toolStripMenuItemDeleteLinks";
			this.toolStripMenuItemDeleteLinks.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteLinks.Text = "Delete ALL links on this page";
			this.toolStripMenuItemDeleteLinks.Click += new System.EventHandler(this.toolStripMenuItemDeleteLinks_Click);
			// 
			// toolStripMenuItemDeleteSecurity
			// 
			this.toolStripMenuItemDeleteSecurity.Name = "toolStripMenuItemDeleteSecurity";
			this.toolStripMenuItemDeleteSecurity.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteSecurity.Text = "Delete Security Settings for ALL Links on this page";
			this.toolStripMenuItemDeleteSecurity.Click += new System.EventHandler(this.toolStripMenuItemDeleteSecurity_Click);
			// 
			// toolStripMenuItemDeleteTags
			// 
			this.toolStripMenuItemDeleteTags.Name = "toolStripMenuItemDeleteTags";
			this.toolStripMenuItemDeleteTags.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteTags.Text = "Wipe ALL Tags for ALL Links on this page";
			this.toolStripMenuItemDeleteTags.Click += new System.EventHandler(this.toolStripMenuItemDeleteTags_Click);
			// 
			// toolStripMenuItemDeleteWidgets
			// 
			this.toolStripMenuItemDeleteWidgets.Name = "toolStripMenuItemDeleteWidgets";
			this.toolStripMenuItemDeleteWidgets.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteWidgets.Text = "Remove all Widgets for ALL Links on this page";
			this.toolStripMenuItemDeleteWidgets.Click += new System.EventHandler(this.toolStripMenuItemDeleteWidgets_Click);
			// 
			// toolStripMenuItemDeleteBanners
			// 
			this.toolStripMenuItemDeleteBanners.Name = "toolStripMenuItemDeleteBanners";
			this.toolStripMenuItemDeleteBanners.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteBanners.Text = "Remove all Banners for ALL Links on this page";
			this.toolStripMenuItemDeleteBanners.Click += new System.EventHandler(this.toolStripMenuItemDeleteBanners_Click);
			// 
			// MultitabLibraryControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.pnEmpty);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MultitabLibraryControl";
			this.Size = new System.Drawing.Size(506, 386);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.contextMenuStripPageProperties.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnEmpty;
		public DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripPageProperties;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteLinks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteTags;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteWidgets;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteBanners;
    }
}
