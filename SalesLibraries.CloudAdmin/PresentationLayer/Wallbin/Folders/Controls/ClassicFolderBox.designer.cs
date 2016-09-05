namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Controls
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
		/// 			//
		/// </summary>
		protected override void InitializeComponent()
		{
			base.InitializeComponent();
			this.components = new System.ComponentModel.Container();
			this.contextMenuStripSecurity = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSecuritySelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSecurityResetAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteLinks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteSecurity = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteTags = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripFolderProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemFolderSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemFolderDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderMove = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemFolderManageWidgetsAndBanners = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderWidget = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderBanner = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteWidgets = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteBanners = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderSort = new System.Windows.Forms.ToolStripMenuItem();
			this.popupMenuLinkProperties = new DevExpress.XtraBars.PopupMenu(this.components);
			this.barButtonItemLinkPropertiesOpenLink = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesDelete = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesLinkSettings = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesAdvancedSettings = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesTags = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesWidget = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesBanner = new DevExpress.XtraBars.BarButtonItem();
			this.barSubItemLinkPropertiesAdvanced = new DevExpress.XtraBars.BarSubItem();
			this.barButtonItemLinkPropertiesFileLocation = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesRefreshPreview = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesExpirationDate = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesSecurity = new DevExpress.XtraBars.BarButtonItem();
			this.barSubItemLinkPropertiesQuickTools = new DevExpress.XtraBars.BarSubItem();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.barButtonItemLinkPropertiesCopy = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesCut = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemLinkPropertiesPaste = new DevExpress.XtraBars.BarButtonItem();
			this.pnHeader.SuspendLayout();
			this.pnHeaderBorder.SuspendLayout();
			this.pnBorders.SuspendLayout();
			this.contextMenuStripSecurity.SuspendLayout();
			this.contextMenuStripFolderProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.popupMenuLinkProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControlText
			// 
			this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
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
			this.toolStripMenuItemFolderCopy,
			this.toolStripMenuItemFolderMove,
			this.toolStripSeparator3,
			this.toolStripMenuItemFolderManageWidgetsAndBanners,
			this.toolStripMenuItemFolderDeleteSecurity,
			this.toolStripMenuItemFolderDeleteTags,
			this.toolStripMenuItemFolderSort});
			this.contextMenuStripFolderProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripFolderProperties.Size = new System.Drawing.Size(349, 214);
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
			// toolStripMenuItemFolderCopy
			// 
			this.toolStripMenuItemFolderCopy.Name = "toolStripMenuItemFolderCopy";
			this.toolStripMenuItemFolderCopy.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderCopy.Text = "Copy this Window to a another page...";
			// 
			// toolStripMenuItemFolderMove
			// 
			this.toolStripMenuItemFolderMove.Name = "toolStripMenuItemFolderMove";
			this.toolStripMenuItemFolderMove.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderMove.Text = "Move this Window to a another page...";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(345, 6);
			// 
			// toolStripMenuItemFolderManageWidgetsAndBanners
			// 
			this.toolStripMenuItemFolderManageWidgetsAndBanners.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripMenuItemFolderWidget,
			this.toolStripMenuItemFolderBanner,
			this.toolStripMenuItemFolderDeleteWidgets,
			this.toolStripMenuItemFolderDeleteBanners});
			this.toolStripMenuItemFolderManageWidgetsAndBanners.Name = "toolStripMenuItemFolderManageWidgetsAndBanners";
			this.toolStripMenuItemFolderManageWidgetsAndBanners.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderManageWidgetsAndBanners.Text = "Manage Widgets and Banners...";
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
			// popupMenuLinkProperties
			// 
			this.popupMenuLinkProperties.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesCopy, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesCut),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesPaste),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesOpenLink, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesDelete),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesLinkSettings, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesAdvancedSettings),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesTags),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesWidget),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesBanner),
			new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemLinkPropertiesAdvanced, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemLinkPropertiesQuickTools, true)});
			this.popupMenuLinkProperties.Manager = this.barManager;
			this.popupMenuLinkProperties.Name = "popupMenuLinkProperties";
			// 
			// barButtonItemLinkPropertiesOpenLink
			// 
			this.barButtonItemLinkPropertiesOpenLink.Caption = "Open this Link";
			this.barButtonItemLinkPropertiesOpenLink.Id = 0;
			this.barButtonItemLinkPropertiesOpenLink.Name = "barButtonItemLinkPropertiesOpenLink";
			this.barButtonItemLinkPropertiesOpenLink.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesOpenLink_ItemClick);
			// 
			// barButtonItemLinkPropertiesDelete
			// 
			this.barButtonItemLinkPropertiesDelete.Caption = "Delete this link";
			this.barButtonItemLinkPropertiesDelete.Id = 2;
			this.barButtonItemLinkPropertiesDelete.Name = "barButtonItemLinkPropertiesDelete";
			this.barButtonItemLinkPropertiesDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesDelete_ItemClick);
			// 
			// barButtonItemLinkPropertiesLinkSettings
			// 
			this.barButtonItemLinkPropertiesLinkSettings.Caption = "Link Settings";
			this.barButtonItemLinkPropertiesLinkSettings.Id = 4;
			this.barButtonItemLinkPropertiesLinkSettings.Name = "barButtonItemLinkPropertiesLinkSettings";
			this.barButtonItemLinkPropertiesLinkSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesLinkSettings_ItemClick);
			// 
			// barButtonItemLinkPropertiesAdvancedSettings
			// 
			this.barButtonItemLinkPropertiesAdvancedSettings.Caption = "Advanced Settings";
			this.barButtonItemLinkPropertiesAdvancedSettings.Id = 5;
			this.barButtonItemLinkPropertiesAdvancedSettings.Name = "barButtonItemLinkPropertiesAdvancedSettings";
			this.barButtonItemLinkPropertiesAdvancedSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesAdvancedSettings_ItemClick);
			// 
			// barButtonItemLinkPropertiesTags
			// 
			this.barButtonItemLinkPropertiesTags.Caption = "Search Tag";
			this.barButtonItemLinkPropertiesTags.Id = 6;
			this.barButtonItemLinkPropertiesTags.Name = "barButtonItemLinkPropertiesTags";
			this.barButtonItemLinkPropertiesTags.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesTags_ItemClick);
			// 
			// barButtonItemLinkPropertiesWidget
			// 
			this.barButtonItemLinkPropertiesWidget.Caption = "Widget  (tiny)";
			this.barButtonItemLinkPropertiesWidget.Id = 9;
			this.barButtonItemLinkPropertiesWidget.Name = "barButtonItemLinkPropertiesWidget";
			this.barButtonItemLinkPropertiesWidget.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesWidget_ItemClick);
			// 
			// barButtonItemLinkPropertiesBanner
			// 
			this.barButtonItemLinkPropertiesBanner.Caption = "Banner  (BIG)";
			this.barButtonItemLinkPropertiesBanner.Id = 10;
			this.barButtonItemLinkPropertiesBanner.Name = "barButtonItemLinkPropertiesBanner";
			this.barButtonItemLinkPropertiesBanner.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesBanner_ItemClick);
			// 
			// barSubItemLinkPropertiesAdvanced
			// 
			this.barSubItemLinkPropertiesAdvanced.Caption = "Advanced Options";
			this.barSubItemLinkPropertiesAdvanced.Id = 29;
			this.barSubItemLinkPropertiesAdvanced.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesFileLocation),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesRefreshPreview),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesExpirationDate),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLinkPropertiesSecurity)});
			this.barSubItemLinkPropertiesAdvanced.Name = "barSubItemLinkPropertiesAdvanced";
			// 
			// barButtonItemLinkPropertiesFileLocation
			// 
			this.barButtonItemLinkPropertiesFileLocation.Caption = "Open File Location";
			this.barButtonItemLinkPropertiesFileLocation.Id = 1;
			this.barButtonItemLinkPropertiesFileLocation.Name = "barButtonItemLinkPropertiesFileLocation";
			this.barButtonItemLinkPropertiesFileLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesFileLocation_ItemClick);
			// 
			// barButtonItemLinkPropertiesRefreshPreview
			// 
			this.barButtonItemLinkPropertiesRefreshPreview.Caption = "Refresh this link";
			this.barButtonItemLinkPropertiesRefreshPreview.Id = 3;
			this.barButtonItemLinkPropertiesRefreshPreview.Name = "barButtonItemLinkPropertiesRefreshPreview";
			this.barButtonItemLinkPropertiesRefreshPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesRefreshPreview_ItemClick);
			// 
			// barButtonItemLinkPropertiesExpirationDate
			// 
			this.barButtonItemLinkPropertiesExpirationDate.Caption = "Expiration Date";
			this.barButtonItemLinkPropertiesExpirationDate.Id = 7;
			this.barButtonItemLinkPropertiesExpirationDate.Name = "barButtonItemLinkPropertiesExpirationDate";
			this.barButtonItemLinkPropertiesExpirationDate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesExpirationDate_ItemClick);
			// 
			// barButtonItemLinkPropertiesSecurity
			// 
			this.barButtonItemLinkPropertiesSecurity.Caption = "Link Security";
			this.barButtonItemLinkPropertiesSecurity.Id = 8;
			this.barButtonItemLinkPropertiesSecurity.Name = "barButtonItemLinkPropertiesSecurity";
			this.barButtonItemLinkPropertiesSecurity.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesSecurity_ItemClick);
			// 
			// barSubItemLinkPropertiesQuickTools
			// 
			this.barSubItemLinkPropertiesQuickTools.Caption = "Quick Tools";
			this.barSubItemLinkPropertiesQuickTools.Id = 11;
			this.barSubItemLinkPropertiesQuickTools.Name = "barSubItemLinkPropertiesQuickTools";
			// 
			// barManager
			// 
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
			this.barButtonItemLinkPropertiesOpenLink,
			this.barButtonItemLinkPropertiesFileLocation,
			this.barButtonItemLinkPropertiesDelete,
			this.barButtonItemLinkPropertiesRefreshPreview,
			this.barButtonItemLinkPropertiesLinkSettings,
			this.barButtonItemLinkPropertiesAdvancedSettings,
			this.barButtonItemLinkPropertiesTags,
			this.barButtonItemLinkPropertiesExpirationDate,
			this.barButtonItemLinkPropertiesSecurity,
			this.barButtonItemLinkPropertiesWidget,
			this.barButtonItemLinkPropertiesBanner,
			this.barSubItemLinkPropertiesQuickTools,
			this.barSubItemLinkPropertiesAdvanced,
			this.barButtonItemLinkPropertiesCopy,
			this.barButtonItemLinkPropertiesCut,
			this.barButtonItemLinkPropertiesPaste});
			this.barManager.MaxItemId = 33;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(311, 0);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 308);
			this.barDockControlBottom.Size = new System.Drawing.Size(311, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 308);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(311, 0);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 308);
			// 
			// barButtonItemLinkPropertiesCopy
			// 
			this.barButtonItemLinkPropertiesCopy.Caption = "Copy";
			this.barButtonItemLinkPropertiesCopy.Id = 30;
			this.barButtonItemLinkPropertiesCopy.Name = "barButtonItemLinkPropertiesCopy";
			this.barButtonItemLinkPropertiesCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesCopy_ItemClick);
			// 
			// barButtonItemLinkPropertiesCut
			// 
			this.barButtonItemLinkPropertiesCut.Caption = "Cut";
			this.barButtonItemLinkPropertiesCut.Id = 31;
			this.barButtonItemLinkPropertiesCut.Name = "barButtonItemLinkPropertiesCut";
			this.barButtonItemLinkPropertiesCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesCut_ItemClick);
			// 
			// barButtonItemLinkPropertiesPaste
			// 
			this.barButtonItemLinkPropertiesPaste.Caption = "Paste";
			this.barButtonItemLinkPropertiesPaste.Id = 32;
			this.barButtonItemLinkPropertiesPaste.Name = "barButtonItemLinkPropertiesPaste";
			this.barButtonItemLinkPropertiesPaste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLinkPropertiesPaste_ItemClick);
			// 
			// ClassicFolderBox
			// 
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "ClassicFolderBox";
			this.DragLeave += new System.EventHandler(this.OnDragLeave);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnBordersPaint);
			this.Controls.SetChildIndex(this.barDockControlTop, 0);
			this.Controls.SetChildIndex(this.barDockControlBottom, 0);
			this.Controls.SetChildIndex(this.barDockControlRight, 0);
			this.Controls.SetChildIndex(this.barDockControlLeft, 0);
			this.Controls.SetChildIndex(this.pnBorders, 0);
			this.pnHeader.ResumeLayout(false);
			this.pnHeaderBorder.ResumeLayout(false);
			this.pnBorders.ResumeLayout(false);
			this.contextMenuStripSecurity.ResumeLayout(false);
			this.contextMenuStripFolderProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.popupMenuLinkProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStripSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSecuritySelectAll;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSecurityResetAll;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteLinks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteSecurity;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteTags;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderProperties;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderSettings;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderSort;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderManageWidgetsAndBanners;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderWidget;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderBanner;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteWidgets;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteBanners;
		private DevExpress.XtraBars.PopupMenu popupMenuLinkProperties;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesOpenLink;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesFileLocation;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesDelete;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesRefreshPreview;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesLinkSettings;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesAdvancedSettings;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesTags;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesExpirationDate;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesSecurity;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesWidget;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesBanner;
		private DevExpress.XtraBars.BarSubItem barSubItemLinkPropertiesQuickTools;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderCopy;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderMove;
		private DevExpress.XtraBars.BarSubItem barSubItemLinkPropertiesAdvanced;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesCopy;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesCut;
		private DevExpress.XtraBars.BarButtonItem barButtonItemLinkPropertiesPaste;
	}
}
