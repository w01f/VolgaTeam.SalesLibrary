namespace FileManager.PresentationClasses.WallBin
{
    partial class FolderBoxControl
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			this.ttCellInfo = new System.Windows.Forms.ToolTip(this.components);
			this.pnGrid = new System.Windows.Forms.Panel();
			this.grFiles = new System.Windows.Forms.DataGridView();
			this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pnHeaderBorder = new System.Windows.Forms.Panel();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.labelControlText = new DevExpress.XtraEditors.LabelControl();
			this.pbImage = new System.Windows.Forms.PictureBox();
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
			this.toolStripMenuItemFolderDeleteWidgets = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDeleteBanners = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripFolderProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemFolderSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderWidget = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFolderBanner = new System.Windows.Forms.ToolStripMenuItem();
			this.pnGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grFiles)).BeginInit();
			this.pnHeaderBorder.SuspendLayout();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.contextMenuStripSecurity.SuspendLayout();
			this.contextMenuStripLinkProperties.SuspendLayout();
			this.contextMenuStripFolderProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// ttCellInfo
			// 
			this.ttCellInfo.IsBalloon = true;
			this.ttCellInfo.ToolTipTitle = "Information About File";
			// 
			// pnGrid
			// 
			this.pnGrid.Controls.Add(this.grFiles);
			this.pnGrid.Controls.Add(this.pnHeaderBorder);
			this.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnGrid.Location = new System.Drawing.Point(0, 0);
			this.pnGrid.Name = "pnGrid";
			this.pnGrid.Padding = new System.Windows.Forms.Padding(1);
			this.pnGrid.Size = new System.Drawing.Size(311, 308);
			this.pnGrid.TabIndex = 3;
			this.pnGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBorders_Paint);
			// 
			// grFiles
			// 
			this.grFiles.AllowDrop = true;
			this.grFiles.AllowUserToAddRows = false;
			this.grFiles.AllowUserToDeleteRows = false;
			this.grFiles.AllowUserToResizeColumns = false;
			this.grFiles.AllowUserToResizeRows = false;
			this.grFiles.BackgroundColor = System.Drawing.Color.White;
			this.grFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.grFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.grFiles.ColumnHeadersVisible = false;
			this.grFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDisplayName});
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grFiles.DefaultCellStyle = dataGridViewCellStyle14;
			this.grFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grFiles.Location = new System.Drawing.Point(1, 40);
			this.grFiles.Name = "grFiles";
			this.grFiles.ReadOnly = true;
			this.grFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.grFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
			this.grFiles.RowHeadersVisible = false;
			this.grFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.grFiles.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.grFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grFiles.Size = new System.Drawing.Size(309, 267);
			this.grFiles.TabIndex = 3;
			this.grFiles.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grWindowFiles_CellBeginEdit);
			this.grFiles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grWindowFiles_CellEndEdit);
			this.grFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grFiles_CellFormatting);
			this.grFiles.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grFiles_CellMouseClick);
			this.grFiles.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grFiles_CellMouseDown);
			this.grFiles.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grFiles_CellMouseLeave);
			this.grFiles.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grFiles_CellMouseMove);
			this.grFiles.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grFiles_CellPainting);
			this.grFiles.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grFiles_RowsRemoved);
			this.grFiles.SelectionChanged += new System.EventHandler(this.grFiles_SelectionChanged);
			this.grFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.grFiles_DragDrop);
			this.grFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.grFiles_DragEnter);
			this.grFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.grFiles_DragOver);
			this.grFiles.DragLeave += new System.EventHandler(this.grFiles_DragLeave);
			this.grFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grFiles_MouseDown);
			// 
			// colDisplayName
			// 
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.colDisplayName.DefaultCellStyle = dataGridViewCellStyle13;
			this.colDisplayName.HeaderText = "Display Name and Note";
			this.colDisplayName.Name = "colDisplayName";
			this.colDisplayName.ReadOnly = true;
			this.colDisplayName.Width = 5;
			// 
			// pnHeaderBorder
			// 
			this.pnHeaderBorder.Controls.Add(this.pnHeader);
			this.pnHeaderBorder.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeaderBorder.Location = new System.Drawing.Point(1, 1);
			this.pnHeaderBorder.Name = "pnHeaderBorder";
			this.pnHeaderBorder.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.pnHeaderBorder.Size = new System.Drawing.Size(309, 39);
			this.pnHeaderBorder.TabIndex = 5;
			this.pnHeaderBorder.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBorders_Paint);
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.labelControlText);
			this.pnHeader.Controls.Add(this.pbImage);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.pnHeader.Size = new System.Drawing.Size(309, 38);
			this.pnHeader.TabIndex = 4;
			// 
			// labelControlText
			// 
			this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlText.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.labelControlText.Location = new System.Drawing.Point(43, 0);
			this.labelControlText.Name = "labelControlText";
			this.labelControlText.Size = new System.Drawing.Size(266, 37);
			this.labelControlText.TabIndex = 3;
			this.labelControlText.UseMnemonic = false;
			this.labelControlText.Click += new System.EventHandler(this.laFolderName_Click);
			this.labelControlText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseDown);
			this.labelControlText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseMove);
			// 
			// pbImage
			// 
			this.pbImage.BackColor = System.Drawing.SystemColors.Control;
			this.pbImage.Dock = System.Windows.Forms.DockStyle.Left;
			this.pbImage.Location = new System.Drawing.Point(0, 0);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(43, 37);
			this.pbImage.TabIndex = 0;
			this.pbImage.TabStop = false;
			this.pbImage.Click += new System.EventHandler(this.laFolderName_Click);
			this.pbImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseDown);
			this.pbImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseMove);
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
			this.toolStripMenuItemSecuritySelectAll.Image = global::FileManager.Properties.Resources.SecuritySelectMenu;
			this.toolStripMenuItemSecuritySelectAll.Name = "toolStripMenuItemSecuritySelectAll";
			this.toolStripMenuItemSecuritySelectAll.Size = new System.Drawing.Size(240, 30);
			this.toolStripMenuItemSecuritySelectAll.Text = "Select all Links in this Window";
			this.toolStripMenuItemSecuritySelectAll.Click += new System.EventHandler(this.toolStripMenuItemSelectAll_Click);
			// 
			// toolStripMenuItemSecurityResetAll
			// 
			this.toolStripMenuItemSecurityResetAll.Image = global::FileManager.Properties.Resources.SecurityResetMenu;
			this.toolStripMenuItemSecurityResetAll.Name = "toolStripMenuItemSecurityResetAll";
			this.toolStripMenuItemSecurityResetAll.Size = new System.Drawing.Size(240, 30);
			this.toolStripMenuItemSecurityResetAll.Text = "Reset all Links in this Window";
			this.toolStripMenuItemSecurityResetAll.Click += new System.EventHandler(this.toolStripMenuItemResetAll_Click);
			// 
			// contextMenuStripLinkProperties
			// 
			this.contextMenuStripLinkProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLinkPropertiesOpen,
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
			this.contextMenuStripLinkProperties.Size = new System.Drawing.Size(173, 208);
			// 
			// toolStripMenuItemLinkPropertiesOpen
			// 
			this.toolStripMenuItemLinkPropertiesOpen.Name = "toolStripMenuItemLinkPropertiesOpen";
			this.toolStripMenuItemLinkPropertiesOpen.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesOpen.Text = "Open this link";
			this.toolStripMenuItemLinkPropertiesOpen.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesOpen_Click);
			// 
			// toolStripMenuItemLinkPropertiesDelete
			// 
			this.toolStripMenuItemLinkPropertiesDelete.Name = "toolStripMenuItemLinkPropertiesDelete";
			this.toolStripMenuItemLinkPropertiesDelete.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesDelete.Text = "Delete this link";
			this.toolStripMenuItemLinkPropertiesDelete.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
			// 
			// toolStripMenuItemLinkPropertiesNotes
			// 
			this.toolStripMenuItemLinkPropertiesNotes.Name = "toolStripMenuItemLinkPropertiesNotes";
			this.toolStripMenuItemLinkPropertiesNotes.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesNotes.Text = "Link Settings";
			this.toolStripMenuItemLinkPropertiesNotes.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesNotes_Click);
			// 
			// toolStripMenuItemLinkPropertiesAdvanced
			// 
			this.toolStripMenuItemLinkPropertiesAdvanced.Name = "toolStripMenuItemLinkPropertiesAdvanced";
			this.toolStripMenuItemLinkPropertiesAdvanced.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesAdvanced.Text = "Advanced Settings";
			this.toolStripMenuItemLinkPropertiesAdvanced.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesAdvanced_Click);
			// 
			// toolStripMenuItemLinkPropertiesTags
			// 
			this.toolStripMenuItemLinkPropertiesTags.Name = "toolStripMenuItemLinkPropertiesTags";
			this.toolStripMenuItemLinkPropertiesTags.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesTags.Text = "Search Tag";
			this.toolStripMenuItemLinkPropertiesTags.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesTags_Click);
			// 
			// toolStripMenuItemLinkPropertiesExpirationDate
			// 
			this.toolStripMenuItemLinkPropertiesExpirationDate.Name = "toolStripMenuItemLinkPropertiesExpirationDate";
			this.toolStripMenuItemLinkPropertiesExpirationDate.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesExpirationDate.Text = "Expiration Date";
			this.toolStripMenuItemLinkPropertiesExpirationDate.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesExpirationDate_Click);
			// 
			// toolStripMenuItemLinkPropertiesSecurity
			// 
			this.toolStripMenuItemLinkPropertiesSecurity.Name = "toolStripMenuItemLinkPropertiesSecurity";
			this.toolStripMenuItemLinkPropertiesSecurity.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesSecurity.Text = "Link Security";
			this.toolStripMenuItemLinkPropertiesSecurity.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesSecurity_Click);
			// 
			// toolStripMenuItemLinkPropertiesWidget
			// 
			this.toolStripMenuItemLinkPropertiesWidget.Name = "toolStripMenuItemLinkPropertiesWidget";
			this.toolStripMenuItemLinkPropertiesWidget.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesWidget.Text = "Add Widget";
			this.toolStripMenuItemLinkPropertiesWidget.Click += new System.EventHandler(this.toolStripMenuItemLinkPropertiesWidget_Click);
			// 
			// toolStripMenuItemLinkPropertiesBanner
			// 
			this.toolStripMenuItemLinkPropertiesBanner.Name = "toolStripMenuItemLinkPropertiesBanner";
			this.toolStripMenuItemLinkPropertiesBanner.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItemLinkPropertiesBanner.Text = "Add Banner";
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
			// toolStripMenuItemFolderDeleteWidgets
			// 
			this.toolStripMenuItemFolderDeleteWidgets.Name = "toolStripMenuItemFolderDeleteWidgets";
			this.toolStripMenuItemFolderDeleteWidgets.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderDeleteWidgets.Text = "Remove all Widgets for all links in this window";
			this.toolStripMenuItemFolderDeleteWidgets.Click += new System.EventHandler(this.toolStripMenuItemFolderDeleteWidgets_Click);
			// 
			// toolStripMenuItemFolderDeleteBanners
			// 
			this.toolStripMenuItemFolderDeleteBanners.Name = "toolStripMenuItemFolderDeleteBanners";
			this.toolStripMenuItemFolderDeleteBanners.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderDeleteBanners.Text = "Remove all Banners for all links in this window";
			this.toolStripMenuItemFolderDeleteBanners.Click += new System.EventHandler(this.toolStripMenuItemFolderDeleteBanners_Click);
			// 
			// contextMenuStripFolderProperties
			// 
			this.contextMenuStripFolderProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFolderDeleteLinks,
            this.toolStripMenuItemFolderDeleteSecurity,
            this.toolStripMenuItemFolderDeleteTags,
            this.toolStripMenuItemFolderDeleteWidgets,
            this.toolStripMenuItemFolderDeleteBanners,
            this.toolStripMenuItemFolderSettings,
            this.toolStripMenuItemFolderDelete,
            this.toolStripMenuItemFolderWidget,
            this.toolStripMenuItemFolderBanner});
			this.contextMenuStripFolderProperties.Name = "contextMenuStripLinkProperties";
			this.contextMenuStripFolderProperties.Size = new System.Drawing.Size(349, 224);
			// 
			// toolStripMenuItemFolderSettings
			// 
			this.toolStripMenuItemFolderSettings.Name = "toolStripMenuItemFolderSettings";
			this.toolStripMenuItemFolderSettings.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderSettings.Text = "Edit window settings";
			this.toolStripMenuItemFolderSettings.Click += new System.EventHandler(this.toolStripMenuItemFolderSettings_Click);
			// 
			// toolStripMenuItemFolderDelete
			// 
			this.toolStripMenuItemFolderDelete.Name = "toolStripMenuItemFolderDelete";
			this.toolStripMenuItemFolderDelete.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderDelete.Text = "Delete this window";
			this.toolStripMenuItemFolderDelete.Click += new System.EventHandler(this.toolStripMenuItemFolderDelete_Click);
			// 
			// toolStripMenuItemFolderWidget
			// 
			this.toolStripMenuItemFolderWidget.Name = "toolStripMenuItemFolderWidget";
			this.toolStripMenuItemFolderWidget.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderWidget.Text = "Add a Widget to this window";
			this.toolStripMenuItemFolderWidget.Click += new System.EventHandler(this.toolStripMenuItemFolderWidget_Click);
			// 
			// toolStripMenuItemFolderBanner
			// 
			this.toolStripMenuItemFolderBanner.Name = "toolStripMenuItemFolderBanner";
			this.toolStripMenuItemFolderBanner.Size = new System.Drawing.Size(348, 22);
			this.toolStripMenuItemFolderBanner.Text = "Add a Banner to this Window";
			this.toolStripMenuItemFolderBanner.Click += new System.EventHandler(this.toolStripMenuItemFolderBanner_Click);
			// 
			// FolderBoxControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.pnGrid);
			this.Name = "FolderBoxControl";
			this.Size = new System.Drawing.Size(311, 308);
			this.pnGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grFiles)).EndInit();
			this.pnHeaderBorder.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.contextMenuStripSecurity.ResumeLayout(false);
			this.contextMenuStripLinkProperties.ResumeLayout(false);
			this.contextMenuStripFolderProperties.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolTip ttCellInfo;
        protected System.Windows.Forms.Panel pnGrid;
        private System.Windows.Forms.DataGridView grFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pnHeaderBorder;
        private DevExpress.XtraEditors.LabelControl labelControlText;
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
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteWidgets;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDeleteBanners;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderProperties;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinkPropertiesAdvanced;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderSettings;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderWidget;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFolderBanner;
    }
}
