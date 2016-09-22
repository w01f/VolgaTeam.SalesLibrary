namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	partial class TabbedWallbin
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
			this.contextMenuStripPageProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemClone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCloneWindowsAndLinks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCloneWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemManageImages = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteWidgets = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteBanners = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItemCleanupTools = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteTags = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteLinks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteExpirationDates = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteSecurity = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemEditTags = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemResetLinkSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemMakeLinkTextWordWrap = new System.Windows.Forms.ToolStripMenuItem();
			this.pnContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.contextMenuStripPageProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnEmpty
			// 
			this.pnEmpty.BackColor = System.Drawing.Color.White;
			this.pnEmpty.Size = new System.Drawing.Size(697, 45);
			// 
			// pnContainer
			// 
			this.pnContainer.BackColor = System.Drawing.Color.White;
			this.pnContainer.Controls.Add(this.xtraTabControl);
			this.pnContainer.Size = new System.Drawing.Size(697, 432);
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.Size = new System.Drawing.Size(697, 432);
			this.xtraTabControl.TabIndex = 0;
			this.xtraTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabControl_MouseDown);
			// 
			// contextMenuStripPageProperties
			// 
			this.contextMenuStripPageProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRename,
            this.toolStripMenuItemDelete,
            this.toolStripSeparator2,
            this.toolStripMenuItemClone,
            this.toolStripSeparator3,
            this.toolStripMenuItemResetLinkSettings,
            this.toolStripSeparator1,
            this.toolStripMenuItemManageImages,
            this.toolsToolStripMenuItemCleanupTools,
            this.toolStripMenuItemEditTags});
			this.contextMenuStripPageProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripPageProperties.Size = new System.Drawing.Size(281, 198);
			// 
			// toolStripMenuItemRename
			// 
			this.toolStripMenuItemRename.Name = "toolStripMenuItemRename";
			this.toolStripMenuItemRename.Size = new System.Drawing.Size(280, 22);
			this.toolStripMenuItemRename.Text = "Rename this page";
			this.toolStripMenuItemRename.Click += new System.EventHandler(this.toolStripMenuItemRename_Click);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(280, 22);
			this.toolStripMenuItemDelete.Text = "Delete this page";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(277, 6);
			// 
			// toolStripMenuItemClone
			// 
			this.toolStripMenuItemClone.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCloneWindowsAndLinks,
            this.toolStripMenuItemCloneWindows});
			this.toolStripMenuItemClone.Name = "toolStripMenuItemClone";
			this.toolStripMenuItemClone.Size = new System.Drawing.Size(280, 22);
			this.toolStripMenuItemClone.Text = "Clone this Page";
			// 
			// toolStripMenuItemCloneWindowsAndLinks
			// 
			this.toolStripMenuItemCloneWindowsAndLinks.Name = "toolStripMenuItemCloneWindowsAndLinks";
			this.toolStripMenuItemCloneWindowsAndLinks.Size = new System.Drawing.Size(166, 22);
			this.toolStripMenuItemCloneWindowsAndLinks.Text = "Windows && Links";
			this.toolStripMenuItemCloneWindowsAndLinks.Click += new System.EventHandler(this.toolStripMenuItemCloneWindowsAndLinks_Click);
			// 
			// toolStripMenuItemCloneWindows
			// 
			this.toolStripMenuItemCloneWindows.Name = "toolStripMenuItemCloneWindows";
			this.toolStripMenuItemCloneWindows.Size = new System.Drawing.Size(166, 22);
			this.toolStripMenuItemCloneWindows.Text = "Windows Only";
			this.toolStripMenuItemCloneWindows.Click += new System.EventHandler(this.toolStripMenuItemCloneWindows_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(277, 6);
			// 
			// toolStripMenuItemManageImages
			// 
			this.toolStripMenuItemManageImages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteWidgets,
            this.toolStripMenuItemDeleteBanners});
			this.toolStripMenuItemManageImages.Name = "toolStripMenuItemManageImages";
			this.toolStripMenuItemManageImages.Size = new System.Drawing.Size(280, 22);
			this.toolStripMenuItemManageImages.Text = "Manage Link Artwork for this page";
			// 
			// toolStripMenuItemDeleteWidgets
			// 
			this.toolStripMenuItemDeleteWidgets.Name = "toolStripMenuItemDeleteWidgets";
			this.toolStripMenuItemDeleteWidgets.Size = new System.Drawing.Size(283, 22);
			this.toolStripMenuItemDeleteWidgets.Text = "DELETE ALL Widget Icons on this page";
			this.toolStripMenuItemDeleteWidgets.Click += new System.EventHandler(this.toolStripMenuItemDeleteWidgets_Click);
			// 
			// toolStripMenuItemDeleteBanners
			// 
			this.toolStripMenuItemDeleteBanners.Name = "toolStripMenuItemDeleteBanners";
			this.toolStripMenuItemDeleteBanners.Size = new System.Drawing.Size(283, 22);
			this.toolStripMenuItemDeleteBanners.Text = "DELETE ALL Clipart Images on this page";
			this.toolStripMenuItemDeleteBanners.Click += new System.EventHandler(this.toolStripMenuItemDeleteBanners_Click);
			// 
			// toolsToolStripMenuItemCleanupTools
			// 
			this.toolsToolStripMenuItemCleanupTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteTags,
            this.toolStripMenuItemDeleteLinks,
            this.toolStripMenuItemDeleteExpirationDates,
            this.toolStripMenuItemDeleteSecurity,
            this.toolStripMenuItemMakeLinkTextWordWrap});
			this.toolsToolStripMenuItemCleanupTools.Name = "toolsToolStripMenuItemCleanupTools";
			this.toolsToolStripMenuItemCleanupTools.Size = new System.Drawing.Size(280, 22);
			this.toolsToolStripMenuItemCleanupTools.Text = "Advanced Page Cleanup Tools";
			// 
			// toolStripMenuItemDeleteTags
			// 
			this.toolStripMenuItemDeleteTags.Name = "toolStripMenuItemDeleteTags";
			this.toolStripMenuItemDeleteTags.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteTags.Text = "Wipe ALL Tags for ALL Links on this page";
			this.toolStripMenuItemDeleteTags.Click += new System.EventHandler(this.toolStripMenuItemDeleteTags_Click);
			// 
			// toolStripMenuItemDeleteLinks
			// 
			this.toolStripMenuItemDeleteLinks.Name = "toolStripMenuItemDeleteLinks";
			this.toolStripMenuItemDeleteLinks.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteLinks.Text = "Delete ALL links on this page";
			this.toolStripMenuItemDeleteLinks.Click += new System.EventHandler(this.toolStripMenuItemDeleteLinks_Click);
			// 
			// toolStripMenuItemDeleteExpirationDates
			// 
			this.toolStripMenuItemDeleteExpirationDates.Name = "toolStripMenuItemDeleteExpirationDates";
			this.toolStripMenuItemDeleteExpirationDates.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteExpirationDates.Text = "Remove ALL Expiration Dates on this page";
			this.toolStripMenuItemDeleteExpirationDates.Click += new System.EventHandler(this.toolStripMenuItemDeleteExpirationDates_Click);
			// 
			// toolStripMenuItemDeleteSecurity
			// 
			this.toolStripMenuItemDeleteSecurity.Name = "toolStripMenuItemDeleteSecurity";
			this.toolStripMenuItemDeleteSecurity.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteSecurity.Text = "Delete Security Settings for ALL Links on this page";
			this.toolStripMenuItemDeleteSecurity.Click += new System.EventHandler(this.toolStripMenuItemDeleteSecurity_Click);
			// 
			// toolStripMenuItemEditTags
			// 
			this.toolStripMenuItemEditTags.Name = "toolStripMenuItemEditTags";
			this.toolStripMenuItemEditTags.Size = new System.Drawing.Size(280, 22);
			this.toolStripMenuItemEditTags.Text = "Add Search Tag to all links on this page";
			this.toolStripMenuItemEditTags.Click += new System.EventHandler(this.toolStripMenuItemEditTags_Click);
			// 
			// toolStripMenuItemResetLinkSettings
			// 
			this.toolStripMenuItemResetLinkSettings.Name = "toolStripMenuItemResetLinkSettings";
			this.toolStripMenuItemResetLinkSettings.Size = new System.Drawing.Size(280, 22);
			this.toolStripMenuItemResetLinkSettings.Text = "Reset all Links on this page";
			this.toolStripMenuItemResetLinkSettings.Click += new System.EventHandler(this.toolStripMenuItemResetLinkSettings_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(277, 6);
			// 
			// toolStripMenuItemMakeLinkTextWordWrap
			// 
			this.toolStripMenuItemMakeLinkTextWordWrap.Name = "toolStripMenuItemMakeLinkTextWordWrap";
			this.toolStripMenuItemMakeLinkTextWordWrap.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemMakeLinkTextWordWrap.Text = "Make all Links on this Page RESPONSIVE";
			this.toolStripMenuItemMakeLinkTextWordWrap.Click += new System.EventHandler(this.toolStripMenuItemMakeLinkTextWordWrap_Click);
			// 
			// TabbedWallbin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Name = "TabbedWallbin";
			this.Size = new System.Drawing.Size(714, 502);
			this.Controls.SetChildIndex(this.pnEmpty, 0);
			this.Controls.SetChildIndex(this.pnContainer, 0);
			this.pnContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.contextMenuStripPageProperties.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripPageProperties;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRename;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemManageImages;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteWidgets;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteBanners;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItemCleanupTools;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteTags;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteLinks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteExpirationDates;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditTags;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClone;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloneWindowsAndLinks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloneWindows;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemResetLinkSettings;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMakeLinkTextWordWrap;
	}
}
