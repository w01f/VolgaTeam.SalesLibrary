namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemManageImages = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteWidgets = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteBanners = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteTags = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteLinks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteSecurity = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteExpirationDates = new System.Windows.Forms.ToolStripMenuItem();
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
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
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
            this.toolStripSeparator1,
            this.toolStripMenuItemManageImages,
            this.toolStripMenuItemDeleteTags,
            this.toolStripMenuItemDeleteLinks,
            this.toolStripMenuItemDeleteExpirationDates,
            this.toolStripMenuItemDeleteSecurity});
			this.contextMenuStripPageProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripPageProperties.Size = new System.Drawing.Size(337, 186);
			// 
			// toolStripMenuItemRename
			// 
			this.toolStripMenuItemRename.Name = "toolStripMenuItemRename";
			this.toolStripMenuItemRename.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemRename.Text = "Rename this page";
			this.toolStripMenuItemRename.Click += new System.EventHandler(this.toolStripMenuItemRename_Click);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDelete.Text = "Delete this page";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(333, 6);
			// 
			// toolStripMenuItemManageImages
			// 
			this.toolStripMenuItemManageImages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteWidgets,
            this.toolStripMenuItemDeleteBanners});
			this.toolStripMenuItemManageImages.Name = "toolStripMenuItemManageImages";
			this.toolStripMenuItemManageImages.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemManageImages.Text = "Manage Widgets and Banners...";
			// 
			// toolStripMenuItemDeleteWidgets
			// 
			this.toolStripMenuItemDeleteWidgets.Name = "toolStripMenuItemDeleteWidgets";
			this.toolStripMenuItemDeleteWidgets.Size = new System.Drawing.Size(317, 22);
			this.toolStripMenuItemDeleteWidgets.Text = "Remove all Widgets for ALL Links on this page";
			this.toolStripMenuItemDeleteWidgets.Click += new System.EventHandler(this.toolStripMenuItemDeleteWidgets_Click);
			// 
			// toolStripMenuItemDeleteBanners
			// 
			this.toolStripMenuItemDeleteBanners.Name = "toolStripMenuItemDeleteBanners";
			this.toolStripMenuItemDeleteBanners.Size = new System.Drawing.Size(317, 22);
			this.toolStripMenuItemDeleteBanners.Text = "Remove all Banners for ALL Links on this page";
			this.toolStripMenuItemDeleteBanners.Click += new System.EventHandler(this.toolStripMenuItemDeleteBanners_Click);
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
			// toolStripMenuItemDeleteSecurity
			// 
			this.toolStripMenuItemDeleteSecurity.Name = "toolStripMenuItemDeleteSecurity";
			this.toolStripMenuItemDeleteSecurity.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteSecurity.Text = "Delete Security Settings for ALL Links on this page";
			this.toolStripMenuItemDeleteSecurity.Click += new System.EventHandler(this.toolStripMenuItemDeleteSecurity_Click);
			// 
			// toolStripMenuItemDeleteExpirationDates
			// 
			this.toolStripMenuItemDeleteExpirationDates.Name = "toolStripMenuItemDeleteExpirationDates";
			this.toolStripMenuItemDeleteExpirationDates.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteExpirationDates.Text = "Remove ALL Expiration Dates on this page";
			this.toolStripMenuItemDeleteExpirationDates.Click += new System.EventHandler(this.toolStripMenuItemDeleteExpirationDates_Click);
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
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteLinks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteTags;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRename;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemManageImages;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteWidgets;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteBanners;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteExpirationDates;
	}
}
