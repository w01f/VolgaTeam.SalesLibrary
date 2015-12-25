namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	partial class SecurityOptions
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
			this.gridViewSecurityUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnSecurityUserId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnSecurityUserSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditSecurityUserList = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnSecurityUserName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridControlSecurityUserList = new DevExpress.XtraGrid.GridControl();
			this.gridViewSecurityGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnSecurityGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnSecurityGroupSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnSecurityGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.groupBoxSecurity = new DevExpress.XtraEditors.GroupControl();
			this.pnSecurityUserList = new System.Windows.Forms.Panel();
			this.buttonXImport = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSecurityUserListClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSecurityUserListSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.rbSecurityBlackList = new System.Windows.Forms.RadioButton();
			this.rbSecurityForbidden = new System.Windows.Forms.RadioButton();
			this.ckSecurityShareLink = new System.Windows.Forms.CheckBox();
			this.rbSecurityAllowed = new System.Windows.Forms.RadioButton();
			this.rbSecurityWhiteList = new System.Windows.Forms.RadioButton();
			this.rbSecurityDenied = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditSecurityUserList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSecurityUserList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxSecurity)).BeginInit();
			this.groupBoxSecurity.SuspendLayout();
			this.pnSecurityUserList.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridViewSecurityUsers
			// 
			this.gridViewSecurityUsers.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSecurityUsers.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewSecurityUsers.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSecurityUsers.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewSecurityUsers.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewSecurityUsers.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewSecurityUsers.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSecurityUsers.Appearance.Row.Options.UseFont = true;
			this.gridViewSecurityUsers.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSecurityUsers.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewSecurityUsers.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.gridViewSecurityUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSecurityUserId,
            this.gridColumnSecurityUserSelected,
            this.gridColumnSecurityUserName});
			this.gridViewSecurityUsers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
			this.gridViewSecurityUsers.GridControl = this.gridControlSecurityUserList;
			this.gridViewSecurityUsers.Name = "gridViewSecurityUsers";
			this.gridViewSecurityUsers.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityUsers.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityUsers.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewSecurityUsers.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewSecurityUsers.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewSecurityUsers.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewSecurityUsers.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewSecurityUsers.OptionsCustomization.AllowFilter = false;
			this.gridViewSecurityUsers.OptionsCustomization.AllowGroup = false;
			this.gridViewSecurityUsers.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewSecurityUsers.OptionsCustomization.AllowSort = false;
			this.gridViewSecurityUsers.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewSecurityUsers.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewSecurityUsers.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewSecurityUsers.OptionsView.ShowColumnHeaders = false;
			this.gridViewSecurityUsers.OptionsView.ShowGroupPanel = false;
			this.gridViewSecurityUsers.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityUsers.OptionsView.ShowIndicator = false;
			this.gridViewSecurityUsers.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityUsers.RowHeight = 35;
			// 
			// gridColumnSecurityUserId
			// 
			this.gridColumnSecurityUserId.Caption = "Id";
			this.gridColumnSecurityUserId.FieldName = "Id";
			this.gridColumnSecurityUserId.Name = "gridColumnSecurityUserId";
			// 
			// gridColumnSecurityUserSelected
			// 
			this.gridColumnSecurityUserSelected.Caption = "Selected";
			this.gridColumnSecurityUserSelected.ColumnEdit = this.repositoryItemCheckEditSecurityUserList;
			this.gridColumnSecurityUserSelected.FieldName = "Selected";
			this.gridColumnSecurityUserSelected.Name = "gridColumnSecurityUserSelected";
			this.gridColumnSecurityUserSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnSecurityUserSelected.Visible = true;
			this.gridColumnSecurityUserSelected.VisibleIndex = 0;
			this.gridColumnSecurityUserSelected.Width = 30;
			// 
			// repositoryItemCheckEditSecurityUserList
			// 
			this.repositoryItemCheckEditSecurityUserList.AllowFocused = false;
			this.repositoryItemCheckEditSecurityUserList.AutoHeight = false;
			this.repositoryItemCheckEditSecurityUserList.Caption = "Check";
			this.repositoryItemCheckEditSecurityUserList.Name = "repositoryItemCheckEditSecurityUserList";
			this.repositoryItemCheckEditSecurityUserList.CheckedChanged += new System.EventHandler(this.RepositoryItemCheckEditCheckedChanged);
			// 
			// gridColumnSecurityUserName
			// 
			this.gridColumnSecurityUserName.Caption = "Name";
			this.gridColumnSecurityUserName.FieldName = "FullName";
			this.gridColumnSecurityUserName.Name = "gridColumnSecurityUserName";
			this.gridColumnSecurityUserName.OptionsColumn.AllowEdit = false;
			this.gridColumnSecurityUserName.OptionsColumn.ReadOnly = true;
			this.gridColumnSecurityUserName.Visible = true;
			this.gridColumnSecurityUserName.VisibleIndex = 1;
			// 
			// gridControlSecurityUserList
			// 
			this.gridControlSecurityUserList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlSecurityUserList.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlSecurityUserList.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlSecurityUserList.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlSecurityUserList.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlSecurityUserList.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			gridLevelNode1.LevelTemplate = this.gridViewSecurityUsers;
			gridLevelNode1.RelationName = "Users";
			this.gridControlSecurityUserList.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
			this.gridControlSecurityUserList.Location = new System.Drawing.Point(0, 41);
			this.gridControlSecurityUserList.MainView = this.gridViewSecurityGroups;
			this.gridControlSecurityUserList.Name = "gridControlSecurityUserList";
			this.gridControlSecurityUserList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditSecurityUserList});
			this.gridControlSecurityUserList.Size = new System.Drawing.Size(501, 190);
			this.gridControlSecurityUserList.TabIndex = 6;
			this.gridControlSecurityUserList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSecurityGroups,
            this.gridViewSecurityUsers});
			// 
			// gridViewSecurityGroups
			// 
			this.gridViewSecurityGroups.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewSecurityGroups.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewSecurityGroups.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSecurityGroups.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewSecurityGroups.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewSecurityGroups.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewSecurityGroups.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSecurityGroups.Appearance.Row.Options.UseFont = true;
			this.gridViewSecurityGroups.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSecurityGroups.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewSecurityGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSecurityGroupId,
            this.gridColumnSecurityGroupSelected,
            this.gridColumnSecurityGroupName});
			this.gridViewSecurityGroups.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
			this.gridViewSecurityGroups.GridControl = this.gridControlSecurityUserList;
			this.gridViewSecurityGroups.Name = "gridViewSecurityGroups";
			this.gridViewSecurityGroups.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityGroups.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityGroups.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewSecurityGroups.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewSecurityGroups.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewSecurityGroups.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewSecurityGroups.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewSecurityGroups.OptionsCustomization.AllowFilter = false;
			this.gridViewSecurityGroups.OptionsCustomization.AllowGroup = false;
			this.gridViewSecurityGroups.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewSecurityGroups.OptionsCustomization.AllowSort = false;
			this.gridViewSecurityGroups.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
			this.gridViewSecurityGroups.OptionsDetail.ShowDetailTabs = false;
			this.gridViewSecurityGroups.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewSecurityGroups.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewSecurityGroups.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewSecurityGroups.OptionsView.ShowColumnHeaders = false;
			this.gridViewSecurityGroups.OptionsView.ShowGroupPanel = false;
			this.gridViewSecurityGroups.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityGroups.OptionsView.ShowIndicator = false;
			this.gridViewSecurityGroups.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSecurityGroups.RowHeight = 35;
			// 
			// gridColumnSecurityGroupId
			// 
			this.gridColumnSecurityGroupId.Caption = "Id";
			this.gridColumnSecurityGroupId.FieldName = "Id";
			this.gridColumnSecurityGroupId.Name = "gridColumnSecurityGroupId";
			// 
			// gridColumnSecurityGroupSelected
			// 
			this.gridColumnSecurityGroupSelected.Caption = "Selected";
			this.gridColumnSecurityGroupSelected.ColumnEdit = this.repositoryItemCheckEditSecurityUserList;
			this.gridColumnSecurityGroupSelected.FieldName = "Selected";
			this.gridColumnSecurityGroupSelected.Name = "gridColumnSecurityGroupSelected";
			this.gridColumnSecurityGroupSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnSecurityGroupSelected.Visible = true;
			this.gridColumnSecurityGroupSelected.VisibleIndex = 0;
			this.gridColumnSecurityGroupSelected.Width = 30;
			// 
			// gridColumnSecurityGroupName
			// 
			this.gridColumnSecurityGroupName.Caption = "Name";
			this.gridColumnSecurityGroupName.FieldName = "Name";
			this.gridColumnSecurityGroupName.Name = "gridColumnSecurityGroupName";
			this.gridColumnSecurityGroupName.OptionsColumn.AllowEdit = false;
			this.gridColumnSecurityGroupName.OptionsColumn.ReadOnly = true;
			this.gridColumnSecurityGroupName.Visible = true;
			this.gridColumnSecurityGroupName.VisibleIndex = 1;
			this.gridColumnSecurityGroupName.Width = 355;
			// 
			// groupBoxSecurity
			// 
			this.groupBoxSecurity.Appearance.BackColor = System.Drawing.Color.White;
			this.groupBoxSecurity.Appearance.ForeColor = System.Drawing.Color.Black;
			this.groupBoxSecurity.Appearance.Options.UseBackColor = true;
			this.groupBoxSecurity.Appearance.Options.UseForeColor = true;
			this.groupBoxSecurity.Controls.Add(this.pnSecurityUserList);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityBlackList);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityForbidden);
			this.groupBoxSecurity.Controls.Add(this.ckSecurityShareLink);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityAllowed);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityWhiteList);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityDenied);
			this.groupBoxSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxSecurity.Location = new System.Drawing.Point(5, 5);
			this.groupBoxSecurity.Name = "groupBoxSecurity";
			this.groupBoxSecurity.ShowCaption = false;
			this.groupBoxSecurity.Size = new System.Drawing.Size(521, 531);
			this.groupBoxSecurity.TabIndex = 2;
			// 
			// pnSecurityUserList
			// 
			this.pnSecurityUserList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnSecurityUserList.Controls.Add(this.buttonXImport);
			this.pnSecurityUserList.Controls.Add(this.buttonXSecurityUserListClearAll);
			this.pnSecurityUserList.Controls.Add(this.buttonXSecurityUserListSelectAll);
			this.pnSecurityUserList.Controls.Add(this.gridControlSecurityUserList);
			this.pnSecurityUserList.Enabled = false;
			this.pnSecurityUserList.ForeColor = System.Drawing.Color.Black;
			this.pnSecurityUserList.Location = new System.Drawing.Point(8, 259);
			this.pnSecurityUserList.Name = "pnSecurityUserList";
			this.pnSecurityUserList.Size = new System.Drawing.Size(501, 231);
			this.pnSecurityUserList.TabIndex = 7;
			// 
			// buttonXImport
			// 
			this.buttonXImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXImport.Location = new System.Drawing.Point(309, 5);
			this.buttonXImport.Name = "buttonXImport";
			this.buttonXImport.Size = new System.Drawing.Size(132, 32);
			this.buttonXImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXImport.TabIndex = 12;
			this.buttonXImport.Text = "Import";
			this.buttonXImport.Click += new System.EventHandler(this.buttonXImport_Click);
			// 
			// buttonXSecurityUserListClearAll
			// 
			this.buttonXSecurityUserListClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSecurityUserListClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSecurityUserListClearAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXSecurityUserListClearAll.Location = new System.Drawing.Point(156, 5);
			this.buttonXSecurityUserListClearAll.Name = "buttonXSecurityUserListClearAll";
			this.buttonXSecurityUserListClearAll.Size = new System.Drawing.Size(132, 32);
			this.buttonXSecurityUserListClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSecurityUserListClearAll.TabIndex = 11;
			this.buttonXSecurityUserListClearAll.Text = "Remove All";
			this.buttonXSecurityUserListClearAll.Click += new System.EventHandler(this.buttonXSecurityUserListClearAll_Click);
			// 
			// buttonXSecurityUserListSelectAll
			// 
			this.buttonXSecurityUserListSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSecurityUserListSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSecurityUserListSelectAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXSecurityUserListSelectAll.Location = new System.Drawing.Point(3, 5);
			this.buttonXSecurityUserListSelectAll.Name = "buttonXSecurityUserListSelectAll";
			this.buttonXSecurityUserListSelectAll.Size = new System.Drawing.Size(132, 32);
			this.buttonXSecurityUserListSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSecurityUserListSelectAll.TabIndex = 10;
			this.buttonXSecurityUserListSelectAll.Text = "Select All";
			this.buttonXSecurityUserListSelectAll.Click += new System.EventHandler(this.buttonXSecurityUserListSelectAll_Click);
			// 
			// rbSecurityBlackList
			// 
			this.rbSecurityBlackList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityBlackList.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityBlackList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityBlackList.ForeColor = System.Drawing.Color.Black;
			this.rbSecurityBlackList.Location = new System.Drawing.Point(8, 208);
			this.rbSecurityBlackList.Name = "rbSecurityBlackList";
			this.rbSecurityBlackList.Size = new System.Drawing.Size(501, 45);
			this.rbSecurityBlackList.TabIndex = 38;
			this.rbSecurityBlackList.TabStop = true;
			this.rbSecurityBlackList.Text = "Enable Black list for Groups or Users for this link in the Web Sales Library";
			this.rbSecurityBlackList.UseVisualStyleBackColor = false;
			this.rbSecurityBlackList.CheckedChanged += new System.EventHandler(this.rbSecurityRestricted_CheckedChanged);
			// 
			// rbSecurityForbidden
			// 
			this.rbSecurityForbidden.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityForbidden.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityForbidden.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityForbidden.ForeColor = System.Drawing.Color.Black;
			this.rbSecurityForbidden.Location = new System.Drawing.Point(8, 55);
			this.rbSecurityForbidden.Name = "rbSecurityForbidden";
			this.rbSecurityForbidden.Size = new System.Drawing.Size(501, 45);
			this.rbSecurityForbidden.TabIndex = 36;
			this.rbSecurityForbidden.TabStop = true;
			this.rbSecurityForbidden.Text = "This link is HIDDEN in the Local Library and the Web Library";
			this.rbSecurityForbidden.UseVisualStyleBackColor = false;
			// 
			// ckSecurityShareLink
			// 
			this.ckSecurityShareLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ckSecurityShareLink.BackColor = System.Drawing.Color.Transparent;
			this.ckSecurityShareLink.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckSecurityShareLink.ForeColor = System.Drawing.Color.Black;
			this.ckSecurityShareLink.Location = new System.Drawing.Point(8, 496);
			this.ckSecurityShareLink.Name = "ckSecurityShareLink";
			this.ckSecurityShareLink.Size = new System.Drawing.Size(501, 29);
			this.ckSecurityShareLink.TabIndex = 35;
			this.ckSecurityShareLink.Text = "Allow Users to Email this Link and post to quickSITES";
			this.ckSecurityShareLink.UseVisualStyleBackColor = false;
			// 
			// rbSecurityAllowed
			// 
			this.rbSecurityAllowed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityAllowed.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityAllowed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityAllowed.ForeColor = System.Drawing.Color.Black;
			this.rbSecurityAllowed.Location = new System.Drawing.Point(8, 10);
			this.rbSecurityAllowed.Name = "rbSecurityAllowed";
			this.rbSecurityAllowed.Size = new System.Drawing.Size(501, 39);
			this.rbSecurityAllowed.TabIndex = 4;
			this.rbSecurityAllowed.TabStop = true;
			this.rbSecurityAllowed.Text = "Everyone sees this Link in the Local Sales Library and Web Sales Library";
			this.rbSecurityAllowed.UseVisualStyleBackColor = false;
			// 
			// rbSecurityWhiteList
			// 
			this.rbSecurityWhiteList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityWhiteList.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityWhiteList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityWhiteList.ForeColor = System.Drawing.Color.Black;
			this.rbSecurityWhiteList.Location = new System.Drawing.Point(8, 157);
			this.rbSecurityWhiteList.Name = "rbSecurityWhiteList";
			this.rbSecurityWhiteList.Size = new System.Drawing.Size(501, 45);
			this.rbSecurityWhiteList.TabIndex = 1;
			this.rbSecurityWhiteList.TabStop = true;
			this.rbSecurityWhiteList.Text = "Enable White list for Groups or Users for this link in the Web Sales Library";
			this.rbSecurityWhiteList.UseVisualStyleBackColor = false;
			this.rbSecurityWhiteList.CheckedChanged += new System.EventHandler(this.rbSecurityRestricted_CheckedChanged);
			// 
			// rbSecurityDenied
			// 
			this.rbSecurityDenied.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityDenied.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityDenied.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityDenied.ForeColor = System.Drawing.Color.Black;
			this.rbSecurityDenied.Location = new System.Drawing.Point(8, 106);
			this.rbSecurityDenied.Name = "rbSecurityDenied";
			this.rbSecurityDenied.Size = new System.Drawing.Size(501, 45);
			this.rbSecurityDenied.TabIndex = 0;
			this.rbSecurityDenied.TabStop = true;
			this.rbSecurityDenied.Text = "This link is ONLY in the Local Sales Library. It is NOT visible in the Web Sales " +
    "Library";
			this.rbSecurityDenied.UseVisualStyleBackColor = false;
			// 
			// SecurityOptions
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.groupBoxSecurity);
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditSecurityUserList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSecurityUserList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxSecurity)).EndInit();
			this.groupBoxSecurity.ResumeLayout(false);
			this.pnSecurityUserList.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.GroupControl groupBoxSecurity;
		public System.Windows.Forms.RadioButton rbSecurityBlackList;
		public System.Windows.Forms.RadioButton rbSecurityForbidden;
		public System.Windows.Forms.CheckBox ckSecurityShareLink;
		public System.Windows.Forms.RadioButton rbSecurityAllowed;
		public System.Windows.Forms.RadioButton rbSecurityWhiteList;
		public System.Windows.Forms.RadioButton rbSecurityDenied;
		private System.Windows.Forms.Panel pnSecurityUserList;
		private DevExpress.XtraGrid.GridControl gridControlSecurityUserList;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewSecurityUsers;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSecurityUserId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSecurityUserSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditSecurityUserList;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSecurityUserName;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewSecurityGroups;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSecurityGroupId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSecurityGroupSelected;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSecurityGroupName;
		private DevComponents.DotNetBar.ButtonX buttonXImport;
		private DevComponents.DotNetBar.ButtonX buttonXSecurityUserListClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXSecurityUserListSelectAll;
	}
}
