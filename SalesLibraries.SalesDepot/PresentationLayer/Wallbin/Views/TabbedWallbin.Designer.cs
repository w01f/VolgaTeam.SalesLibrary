﻿namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
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
			this.toolStripMenuItemDeleteLinks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteSecurity = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteTags = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteWidgets = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteBanners = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
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
			// 
			// contextMenuStripPageProperties
			// 
			this.contextMenuStripPageProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteLinks,
            this.toolStripMenuItemDeleteSecurity,
            this.toolStripMenuItemDeleteTags,
            this.toolStripMenuItemDeleteWidgets,
            this.toolStripMenuItemDeleteBanners,
            this.toolStripMenuItemRename,
            this.toolStripMenuItemDelete});
			this.contextMenuStripPageProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripPageProperties.Size = new System.Drawing.Size(337, 180);
			// 
			// toolStripMenuItemDeleteLinks
			// 
			this.toolStripMenuItemDeleteLinks.Name = "toolStripMenuItemDeleteLinks";
			this.toolStripMenuItemDeleteLinks.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteLinks.Text = "Delete ALL links on this page";
			// 
			// toolStripMenuItemDeleteSecurity
			// 
			this.toolStripMenuItemDeleteSecurity.Name = "toolStripMenuItemDeleteSecurity";
			this.toolStripMenuItemDeleteSecurity.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteSecurity.Text = "Delete Security Settings for ALL Links on this page";
			// 
			// toolStripMenuItemDeleteTags
			// 
			this.toolStripMenuItemDeleteTags.Name = "toolStripMenuItemDeleteTags";
			this.toolStripMenuItemDeleteTags.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteTags.Text = "Wipe ALL Tags for ALL Links on this page";
			// 
			// toolStripMenuItemDeleteWidgets
			// 
			this.toolStripMenuItemDeleteWidgets.Name = "toolStripMenuItemDeleteWidgets";
			this.toolStripMenuItemDeleteWidgets.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteWidgets.Text = "Remove all Widgets for ALL Links on this page";
			// 
			// toolStripMenuItemDeleteBanners
			// 
			this.toolStripMenuItemDeleteBanners.Name = "toolStripMenuItemDeleteBanners";
			this.toolStripMenuItemDeleteBanners.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDeleteBanners.Text = "Remove all Banners for ALL Links on this page";
			// 
			// toolStripMenuItemRename
			// 
			this.toolStripMenuItemRename.Name = "toolStripMenuItemRename";
			this.toolStripMenuItemRename.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemRename.Text = "Rename this page";
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(336, 22);
			this.toolStripMenuItemDelete.Text = "Delete this page";
			// 
			// TabbedWallbin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteWidgets;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteBanners;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRename;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
	}
}
