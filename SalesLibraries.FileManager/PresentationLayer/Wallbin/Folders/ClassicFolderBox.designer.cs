namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders
{
	partial class ClassicFolderBox
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
		protected override void InitializeComponent()
		{
			base.InitializeComponent();
			this.components = new System.ComponentModel.Container();
			this.contextMenuStripSecurity = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSecuritySelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSecurityResetAll = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripLinkProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemLinkPropertiesOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemLinkPropertiesNotes = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesAdvanced = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesTags = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesExpirationDate = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesSecurity = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesWidget = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesBanner = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteLinks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteSecurity = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteTags = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripFolderProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemFolderSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemFolderDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.manageWidgetsAndBannersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderWidget = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderBanner = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteWidgets = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteBanners = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderSort = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLinkPropertiesOpenLocation = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.pnHeader.SuspendLayout();
			this.pnHeaderBorder.SuspendLayout();
			this.pnBorders.SuspendLayout();
			this.contextMenuStripSecurity.SuspendLayout();
			this.contextMenuStripLinkProperties.SuspendLayout();
			this.contextMenuStripFolderProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelControlText
			// 
			this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlText.Size = new System.Drawing.Size(266, 50);
			// 
			// pbImage
			// 
			this.pbImage.Size = new System.Drawing.Size(43, 50);
			// 
			// contextMenuStripSecurity
			// 
			this.contextMenuStripSecurity.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextMenuStripSecurity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSecuritySelectAll,
            this.toolStripMenuItemSecurityResetAll});
			this.contextMenuStripSecurity.Name = "contextMenuStrip";
			this.contextMenuStripSecurity.Size = new System.Drawing.Size(241, 64);
			// 
			// toolStripMenuItemSecuritySelectAll
			// 
			this.toolStripMenuItemSecuritySelectAll.Image = global::SalesLibraries.FileManager.Properties.Resources.SecurityMenuSelect;
			this.toolStripMenuItemSecuritySelectAll.Name = "toolStripMenuItemSecuritySelectAll";
			this.toolStripMenuItemSecuritySelectAll.Size = new System.Drawing.Size(240, 30);
			this.toolStripMenuItemSecuritySelectAll.Text = "Select all Links in this Window";
			this.toolStripMenuItemSecuritySelectAll.Click += new System.EventHandler(this.toolStripMenuItemSelectAll_Click);
			// 
			// toolStripMenuItemSecurityResetAll
			// 
			this.toolStripMenuItemSecurityResetAll.Image = global::SalesLibraries.FileManager.Properties.Resources.SecurityMenuReset;
			this.toolStripMenuItemSecurityResetAll.Name = "toolStripMenuItemSecurityResetAll";
			this.toolStripMenuItemSecurityResetAll.Size = new System.Drawing.Size(240, 30);
			this.toolStripMenuItemSecurityResetAll.Text = "Reset all Links in this Window";
			this.toolStripMenuItemSecurityResetAll.Click += new System.EventHandler(this.toolStripMenuItemResetAll_Click);
			// 
			// contextMenuStripLinkProperties
			// 
			this.contextMenuStripLinkProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLinkPropertiesOpen,
            this.toolStripMenuItemLinkPropertiesOpenLocation,
            this.toolStripMenuItemLinkPropertiesDelete,
            this.toolStripSeparator1,
            this.toolStripMenuItemLinkPropertiesNotes,
            this.toolStripMenuItemLinkPropertiesAdvanced,
            this.toolStripMenuItemLinkPropertiesTags,
            this.toolStripMenuItemLinkPropertiesExpirationDate,
            this.toolStripMenuItemLinkPropertiesSecurity,
            this.toolStripMenuItemLinkPropertiesWidget,
            this.toolStripMenuItemLinkPropertiesBanner});
			this.contextMenuStripLinkProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripLinkProperties.Size = new System.Drawing.Size(174, 252);
			// 
			// toolStripMenuItemLinkPropertiesOpen
			// 
			this.toolStripMenuItemLinkPropertiesOpen.Name = "toolStripMenuItemLinkPropertiesOpen";
			this.toolStripMenuItemLinkPropertiesOpen.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesOpen.Text = "Open this link";
			this.toolStripMenuItemLinkPropertiesOpen.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesOpen_Click);
			// 
			// toolStripMenuItemLinkPropertiesDelete
			// 
			this.toolStripMenuItemLinkPropertiesDelete.Name = "toolStripMenuItemLinkPropertiesDelete";
			this.toolStripMenuItemLinkPropertiesDelete.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesDelete.Text = "Delete this link";
			this.toolStripMenuItemLinkPropertiesDelete.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
			// 
			// toolStripMenuItemLinkPropertiesNotes
			// 
			this.toolStripMenuItemLinkPropertiesNotes.Name = "toolStripMenuItemLinkPropertiesNotes";
			this.toolStripMenuItemLinkPropertiesNotes.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesNotes.Text = "Link Settings";
			this.toolStripMenuItemLinkPropertiesNotes.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesNotes_Click);
			// 
			// toolStripMenuItemLinkPropertiesAdvanced
			// 
			this.toolStripMenuItemLinkPropertiesAdvanced.Name = "toolStripMenuItemLinkPropertiesAdvanced";
			this.toolStripMenuItemLinkPropertiesAdvanced.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesAdvanced.Text = "Advanced Settings";
			this.toolStripMenuItemLinkPropertiesAdvanced.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesAdvanced_Click);
			// 
			// toolStripMenuItemLinkPropertiesTags
			// 
			this.toolStripMenuItemLinkPropertiesTags.Name = "toolStripMenuItemLinkPropertiesTags";
			this.toolStripMenuItemLinkPropertiesTags.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesTags.Text = "Search Tag";
			this.toolStripMenuItemLinkPropertiesTags.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesTags_Click);
			// 
			// toolStripMenuItemLinkPropertiesExpirationDate
			// 
			this.toolStripMenuItemLinkPropertiesExpirationDate.Name = "toolStripMenuItemLinkPropertiesExpirationDate";
			this.toolStripMenuItemLinkPropertiesExpirationDate.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesExpirationDate.Text = "Expiration Date";
			this.toolStripMenuItemLinkPropertiesExpirationDate.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesExpirationDate_Click);
			// 
			// toolStripMenuItemLinkPropertiesSecurity
			// 
			this.toolStripMenuItemLinkPropertiesSecurity.Name = "toolStripMenuItemLinkPropertiesSecurity";
			this.toolStripMenuItemLinkPropertiesSecurity.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesSecurity.Text = "Link Security";
			this.toolStripMenuItemLinkPropertiesSecurity.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesSecurity_Click);
			// 
			// toolStripMenuItemLinkPropertiesWidget
			// 
			this.toolStripMenuItemLinkPropertiesWidget.Name = "toolStripMenuItemLinkPropertiesWidget";
			this.toolStripMenuItemLinkPropertiesWidget.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesWidget.Text = "Widget";
			this.toolStripMenuItemLinkPropertiesWidget.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesWidget_Click);
			// 
			// toolStripMenuItemLinkPropertiesBanner
			// 
			this.toolStripMenuItemLinkPropertiesBanner.Name = "toolStripMenuItemLinkPropertiesBanner";
			this.toolStripMenuItemLinkPropertiesBanner.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesBanner.Text = "Banner";
			this.toolStripMenuItemLinkPropertiesBanner.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesBanner_Click);
			// 
			// toolStripMenuItemFolderDeleteLinks
			// 
			this.toolStripMenuItemFolderDeleteLinks.Name = "toolStripMenuItemFolderDeleteLinks";
			this.toolStripMenuItemFolderDeleteLinks.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderDeleteLinks.Text = "Delete ALL Links in this window";
			this.toolStripMenuItemFolderDeleteLinks.Click += new System.EventHandler(this.toolStripMenuItemFolderDeleteLinks_Click);
			// 
			// toolStripMenuItemFolderDeleteSecurity
			// 
			this.toolStripMenuItemFolderDeleteSecurity.Name = "toolStripMenuItemFolderDeleteSecurity";
			this.toolStripMenuItemFolderDeleteSecurity.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderDeleteSecurity.Text = "Delete Security Settings for ALL Links in this window";
			this.toolStripMenuItemFolderDeleteSecurity.Click += new System.EventHandler(this.toolStripMenuItemFolderDeleteSecurity_Click);
			// 
			// toolStripMenuItemFolderDeleteTags
			// 
			this.toolStripMenuItemFolderDeleteTags.Name = "toolStripMenuItemFolderDeleteTags";
			this.toolStripMenuItemFolderDeleteTags.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderDeleteTags.Text = "Wipe ALL Tags for ALL Links in this window";
			this.toolStripMenuItemFolderDeleteTags.Click += new System.EventHandler(this.toolStripMenuItemFolderDeleteTags_Click);
			// 
			// contextMenuStripFolderProperties
			// 
			this.contextMenuStripFolderProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFolderSettings,
            this.toolStripSeparator2,
            this.toolStripMenuItemFolderDeleteLinks,
            this.toolStripMenuItemFolderDelete,
            this.toolStripSeparator3,
            this.manageWidgetsAndBannersToolStripMenuItem,
            this.toolStripMenuItemFolderDeleteSecurity,
            this.toolStripMenuItemFolderDeleteTags,
            this.toolStripMenuItemFolderSort});
			this.contextMenuStripFolderProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripFolderProperties.Size = new System.Drawing.Size(349, 170);
			// 
			// toolStripMenuItemFolderSettings
			// 
			this.toolStripMenuItemFolderSettings.Name = "toolStripMenuItemFolderSettings";
			this.toolStripMenuItemFolderSettings.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderSettings.Text = "Edit window settings";
			this.toolStripMenuItemFolderSettings.Click += new System.EventHandler(this.toolStripMenuItemFolderSettings_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(345, 6);
			// 
			// toolStripMenuItemFolderDelete
			// 
			this.toolStripMenuItemFolderDelete.Name = "toolStripMenuItemFolderDelete";
			this.toolStripMenuItemFolderDelete.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderDelete.Text = "Delete this window";
			this.toolStripMenuItemFolderDelete.Click += new System.EventHandler(this.toolStripMenuItemFolderDelete_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(345, 6);
			// 
			// manageWidgetsAndBannersToolStripMenuItem
			// 
			this.manageWidgetsAndBannersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFolderWidget,
            this.toolStripMenuItemFolderBanner,
            this.toolStripMenuItemFolderDeleteWidgets,
            this.toolStripMenuItemFolderDeleteBanners});
			this.manageWidgetsAndBannersToolStripMenuItem.Name = "manageWidgetsAndBannersToolStripMenuItem";
			this.manageWidgetsAndBannersToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
			this.manageWidgetsAndBannersToolStripMenuItem.Text = "Manage Widgets and Banners...";
			// 
			// toolStripMenuItemFolderWidget
			// 
			this.toolStripMenuItemFolderWidget.Name = "toolStripMenuItemFolderWidget";
			this.toolStripMenuItemFolderWidget.Size = new System.Drawing.Size(318, 22);
			this.toolStripMenuItemFolderWidget.Text = "Add a Widget to this window";
			this.toolStripMenuItemFolderWidget.Click += new System.EventHandler(this.toolStripMenuItemFolderWidget_Click);
			// 
			// toolStripMenuItemFolderBanner
			// 
			this.toolStripMenuItemFolderBanner.Name = "toolStripMenuItemFolderBanner";
			this.toolStripMenuItemFolderBanner.Size = new System.Drawing.Size(318, 22);
			this.toolStripMenuItemFolderBanner.Text = "Add a Banner to this Window";
			this.toolStripMenuItemFolderBanner.Click += new System.EventHandler(this.toolStripMenuItemFolderBanner_Click);
			// 
			// toolStripMenuItemFolderDeleteWidgets
			// 
			this.toolStripMenuItemFolderDeleteWidgets.Name = "toolStripMenuItemFolderDeleteWidgets";
			this.toolStripMenuItemFolderDeleteWidgets.Size = new System.Drawing.Size(318, 22);
			this.toolStripMenuItemFolderDeleteWidgets.Text = "Remove all Widgets for all links in this window";
			this.toolStripMenuItemFolderDeleteWidgets.Click += new System.EventHandler(this.toolStripMenuItemFolderDeleteWidgets_Click);
			// 
			// toolStripMenuItemFolderDeleteBanners
			// 
			this.toolStripMenuItemFolderDeleteBanners.Name = "toolStripMenuItemFolderDeleteBanners";
			this.toolStripMenuItemFolderDeleteBanners.Size = new System.Drawing.Size(318, 22);
			this.toolStripMenuItemFolderDeleteBanners.Text = "Remove all Banners for all links in this window";
			this.toolStripMenuItemFolderDeleteBanners.Click += new System.EventHandler(this.toolStripMenuItemFolderDeleteBanners_Click);
			// 
			// toolStripMenuItemFolderSort
			// 
			this.toolStripMenuItemFolderSort.Name = "toolStripMenuItemFolderSort";
			this.toolStripMenuItemFolderSort.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderSort.Text = "Sort Links in this Window A-Z";
			this.toolStripMenuItemFolderSort.Click += new System.EventHandler(this.toolStripMenuItemFolderSort_Click);
			// 
			// toolStripMenuItemLinkPropertiesOpenLocation
			// 
			this.toolStripMenuItemLinkPropertiesOpenLocation.Name = "toolStripMenuItemLinkPropertiesOpenLocation";
			this.toolStripMenuItemLinkPropertiesOpenLocation.Size = new System.Drawing.Size(173, 22);
			this.toolStripMenuItemLinkPropertiesOpenLocation.Text = "Open File Location";
			this.toolStripMenuItemLinkPropertiesOpenLocation.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesOpenLocation_Click);
			// 
			// ClassicFolderBox
			// 
			this.Name = "ClassicFolderBox";
			this.DragLeave += new System.EventHandler(this.OnDragLeave);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnBordersPaint);
			this.Controls.SetChildIndex(this.pnBorders, 0);
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.pnHeader.ResumeLayout(false);
			this.pnHeaderBorder.ResumeLayout(false);
			this.pnBorders.ResumeLayout(false);
			this.contextMenuStripSecurity.ResumeLayout(false);
			this.contextMenuStripLinkProperties.ResumeLayout(false);
			this.contextMenuStripFolderProperties.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStripSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSecuritySelectAll;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSecurityResetAll;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripLinkProperties;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesNotes;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesTags;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesExpirationDate;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesWidget;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesBanner;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteLinks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteTags;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderProperties;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesAdvanced;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderSettings;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderSort;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem manageWidgetsAndBannersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderWidget;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderBanner;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteWidgets;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteBanners;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesOpenLocation;
	}
}
