namespace FileManager.PresentationClasses.Tags
{
	partial class SecurityEditor
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
			this.laHeader = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.rbSecurityBlackList = new System.Windows.Forms.RadioButton();
			this.pnSecurityUserList = new System.Windows.Forms.Panel();
			this.pnSecurityUserListGrid = new System.Windows.Forms.Panel();
			this.buttonXSecurityUserListClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSecurityUserListSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.circularSecurityUserListProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.laSecurityUserListInfo = new DevExpress.XtraEditors.LabelControl();
			this.rbSecurityForbidden = new System.Windows.Forms.RadioButton();
			this.ckSecurityShareLink = new System.Windows.Forms.CheckBox();
			this.rbSecurityAllowed = new System.Windows.Forms.RadioButton();
			this.rbSecurityWhiteList = new System.Windows.Forms.RadioButton();
			this.rbSecurityDenied = new System.Windows.Forms.RadioButton();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditSecurityUserList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSecurityUserList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityGroups)).BeginInit();
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			this.pnSecurityUserList.SuspendLayout();
			this.pnSecurityUserListGrid.SuspendLayout();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
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
			this.gridColumnSecurityUserId.FieldName = "id";
			this.gridColumnSecurityUserId.Name = "gridColumnSecurityUserId";
			// 
			// gridColumnSecurityUserSelected
			// 
			this.gridColumnSecurityUserSelected.Caption = "Selected";
			this.gridColumnSecurityUserSelected.ColumnEdit = this.repositoryItemCheckEditSecurityUserList;
			this.gridColumnSecurityUserSelected.FieldName = "selected";
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
			this.gridControlSecurityUserList.Location = new System.Drawing.Point(0, 82);
			this.gridControlSecurityUserList.MainView = this.gridViewSecurityGroups;
			this.gridControlSecurityUserList.Name = "gridControlSecurityUserList";
			this.gridControlSecurityUserList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditSecurityUserList});
			this.gridControlSecurityUserList.Size = new System.Drawing.Size(296, 70);
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
			this.gridColumnSecurityGroupId.FieldName = "id";
			this.gridColumnSecurityGroupId.Name = "gridColumnSecurityGroupId";
			// 
			// gridColumnSecurityGroupSelected
			// 
			this.gridColumnSecurityGroupSelected.Caption = "Selected";
			this.gridColumnSecurityGroupSelected.ColumnEdit = this.repositoryItemCheckEditSecurityUserList;
			this.gridColumnSecurityGroupSelected.FieldName = "selected";
			this.gridColumnSecurityGroupSelected.Name = "gridColumnSecurityGroupSelected";
			this.gridColumnSecurityGroupSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnSecurityGroupSelected.Visible = true;
			this.gridColumnSecurityGroupSelected.VisibleIndex = 0;
			this.gridColumnSecurityGroupSelected.Width = 30;
			// 
			// gridColumnSecurityGroupName
			// 
			this.gridColumnSecurityGroupName.Caption = "Name";
			this.gridColumnSecurityGroupName.FieldName = "name";
			this.gridColumnSecurityGroupName.Name = "gridColumnSecurityGroupName";
			this.gridColumnSecurityGroupName.OptionsColumn.AllowEdit = false;
			this.gridColumnSecurityGroupName.OptionsColumn.ReadOnly = true;
			this.gridColumnSecurityGroupName.Visible = true;
			this.gridColumnSecurityGroupName.VisibleIndex = 1;
			this.gridColumnSecurityGroupName.Width = 355;
			// 
			// laHeader
			// 
			this.laHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.laHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laHeader.Location = new System.Drawing.Point(0, 0);
			this.laHeader.Name = "laHeader";
			this.laHeader.Size = new System.Drawing.Size(306, 24);
			this.laHeader.TabIndex = 0;
			this.laHeader.Text = "Manage Security";
			this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Controls.Add(this.pnButtons);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 24);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(306, 606);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.rbSecurityBlackList);
			this.pnData.Controls.Add(this.pnSecurityUserList);
			this.pnData.Controls.Add(this.rbSecurityForbidden);
			this.pnData.Controls.Add(this.ckSecurityShareLink);
			this.pnData.Controls.Add(this.rbSecurityAllowed);
			this.pnData.Controls.Add(this.rbSecurityWhiteList);
			this.pnData.Controls.Add(this.rbSecurityDenied);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 47);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(306, 559);
			this.pnData.TabIndex = 1;
			// 
			// rbSecurityBlackList
			// 
			this.rbSecurityBlackList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityBlackList.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityBlackList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityBlackList.Image = global::FileManager.Properties.Resources.TagsSecurityBlackListWidget;
			this.rbSecurityBlackList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.rbSecurityBlackList.Location = new System.Drawing.Point(5, 203);
			this.rbSecurityBlackList.Name = "rbSecurityBlackList";
			this.rbSecurityBlackList.Size = new System.Drawing.Size(296, 53);
			this.rbSecurityBlackList.TabIndex = 42;
			this.rbSecurityBlackList.TabStop = true;
			this.rbSecurityBlackList.Text = "Enable Black list for Groups or Users for this link in the Web Sales Library";
			this.rbSecurityBlackList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rbSecurityBlackList.UseVisualStyleBackColor = false;
			this.rbSecurityBlackList.CheckedChanged += new System.EventHandler(this.rbSecurityRestricted_CheckedChanged);
			// 
			// pnSecurityUserList
			// 
			this.pnSecurityUserList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnSecurityUserList.BackColor = System.Drawing.Color.Transparent;
			this.pnSecurityUserList.Controls.Add(this.pnSecurityUserListGrid);
			this.pnSecurityUserList.Controls.Add(this.circularSecurityUserListProgress);
			this.pnSecurityUserList.Controls.Add(this.laSecurityUserListInfo);
			this.pnSecurityUserList.ForeColor = System.Drawing.Color.Black;
			this.pnSecurityUserList.Location = new System.Drawing.Point(5, 262);
			this.pnSecurityUserList.Name = "pnSecurityUserList";
			this.pnSecurityUserList.Size = new System.Drawing.Size(296, 247);
			this.pnSecurityUserList.TabIndex = 41;
			// 
			// pnSecurityUserListGrid
			// 
			this.pnSecurityUserListGrid.Controls.Add(this.buttonXSecurityUserListClearAll);
			this.pnSecurityUserListGrid.Controls.Add(this.buttonXSecurityUserListSelectAll);
			this.pnSecurityUserListGrid.Controls.Add(this.gridControlSecurityUserList);
			this.pnSecurityUserListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnSecurityUserListGrid.Enabled = false;
			this.pnSecurityUserListGrid.Location = new System.Drawing.Point(0, 95);
			this.pnSecurityUserListGrid.Name = "pnSecurityUserListGrid";
			this.pnSecurityUserListGrid.Size = new System.Drawing.Size(296, 152);
			this.pnSecurityUserListGrid.TabIndex = 8;
			// 
			// buttonXSecurityUserListClearAll
			// 
			this.buttonXSecurityUserListClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSecurityUserListClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSecurityUserListClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSecurityUserListClearAll.Location = new System.Drawing.Point(0, 44);
			this.buttonXSecurityUserListClearAll.Name = "buttonXSecurityUserListClearAll";
			this.buttonXSecurityUserListClearAll.Size = new System.Drawing.Size(296, 32);
			this.buttonXSecurityUserListClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSecurityUserListClearAll.TabIndex = 8;
			this.buttonXSecurityUserListClearAll.Text = "REMOVE ALL Groups and Users";
			this.buttonXSecurityUserListClearAll.Click += new System.EventHandler(this.buttonXSecurityUserListClearAll_Click);
			// 
			// buttonXSecurityUserListSelectAll
			// 
			this.buttonXSecurityUserListSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSecurityUserListSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSecurityUserListSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSecurityUserListSelectAll.Location = new System.Drawing.Point(0, 6);
			this.buttonXSecurityUserListSelectAll.Name = "buttonXSecurityUserListSelectAll";
			this.buttonXSecurityUserListSelectAll.Size = new System.Drawing.Size(296, 32);
			this.buttonXSecurityUserListSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSecurityUserListSelectAll.TabIndex = 7;
			this.buttonXSecurityUserListSelectAll.Text = "SELECT ALL Groups and Users";
			this.buttonXSecurityUserListSelectAll.Click += new System.EventHandler(this.buttonXSecurityUserListSelectAll_Click);
			// 
			// circularSecurityUserListProgress
			// 
			this.circularSecurityUserListProgress.AnimationSpeed = 50;
			this.circularSecurityUserListProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularSecurityUserListProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularSecurityUserListProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.circularSecurityUserListProgress.Location = new System.Drawing.Point(0, 56);
			this.circularSecurityUserListProgress.Name = "circularSecurityUserListProgress";
			this.circularSecurityUserListProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularSecurityUserListProgress.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.circularSecurityUserListProgress.ProgressTextFormat = "";
			this.circularSecurityUserListProgress.Size = new System.Drawing.Size(296, 39);
			this.circularSecurityUserListProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularSecurityUserListProgress.TabIndex = 4;
			this.circularSecurityUserListProgress.Visible = false;
			// 
			// laSecurityUserListInfo
			// 
			this.laSecurityUserListInfo.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.laSecurityUserListInfo.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSecurityUserListInfo.Appearance.ForeColor = System.Drawing.Color.Black;
			this.laSecurityUserListInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.laSecurityUserListInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.laSecurityUserListInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.laSecurityUserListInfo.Location = new System.Drawing.Point(0, 0);
			this.laSecurityUserListInfo.Name = "laSecurityUserListInfo";
			this.laSecurityUserListInfo.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
			this.laSecurityUserListInfo.Size = new System.Drawing.Size(296, 56);
			this.laSecurityUserListInfo.TabIndex = 5;
			this.laSecurityUserListInfo.Text = "labelControl";
			this.laSecurityUserListInfo.Visible = false;
			// 
			// rbSecurityForbidden
			// 
			this.rbSecurityForbidden.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityForbidden.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityForbidden.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityForbidden.Image = global::FileManager.Properties.Resources.TagsSecurityHiddenWidget;
			this.rbSecurityForbidden.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.rbSecurityForbidden.Location = new System.Drawing.Point(5, 47);
			this.rbSecurityForbidden.Name = "rbSecurityForbidden";
			this.rbSecurityForbidden.Size = new System.Drawing.Size(296, 44);
			this.rbSecurityForbidden.TabIndex = 40;
			this.rbSecurityForbidden.TabStop = true;
			this.rbSecurityForbidden.Text = "This link is HIDDEN in the Local Library and the Web Library";
			this.rbSecurityForbidden.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rbSecurityForbidden.UseVisualStyleBackColor = false;
			// 
			// ckSecurityShareLink
			// 
			this.ckSecurityShareLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ckSecurityShareLink.BackColor = System.Drawing.Color.Transparent;
			this.ckSecurityShareLink.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckSecurityShareLink.Location = new System.Drawing.Point(5, 515);
			this.ckSecurityShareLink.Name = "ckSecurityShareLink";
			this.ckSecurityShareLink.Size = new System.Drawing.Size(296, 41);
			this.ckSecurityShareLink.TabIndex = 39;
			this.ckSecurityShareLink.Text = "Allow Users to Email this Link and post to quickSITES";
			this.ckSecurityShareLink.UseVisualStyleBackColor = false;
			this.ckSecurityShareLink.CheckedChanged += new System.EventHandler(this.ValueCheckedChanged);
			// 
			// rbSecurityAllowed
			// 
			this.rbSecurityAllowed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityAllowed.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityAllowed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityAllowed.Image = global::FileManager.Properties.Resources.TagsSecurityVisibleWidget;
			this.rbSecurityAllowed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.rbSecurityAllowed.Location = new System.Drawing.Point(5, 6);
			this.rbSecurityAllowed.Name = "rbSecurityAllowed";
			this.rbSecurityAllowed.Size = new System.Drawing.Size(296, 38);
			this.rbSecurityAllowed.TabIndex = 37;
			this.rbSecurityAllowed.TabStop = true;
			this.rbSecurityAllowed.Text = "Everyone sees this Link in the Local Sales Library and Web Sales Library";
			this.rbSecurityAllowed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rbSecurityAllowed.UseVisualStyleBackColor = false;
			this.rbSecurityAllowed.CheckedChanged += new System.EventHandler(this.ValueCheckedChanged);
			// 
			// rbSecurityWhiteList
			// 
			this.rbSecurityWhiteList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityWhiteList.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityWhiteList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityWhiteList.Image = global::FileManager.Properties.Resources.TagsSecurityWhiteListWidget;
			this.rbSecurityWhiteList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.rbSecurityWhiteList.Location = new System.Drawing.Point(5, 144);
			this.rbSecurityWhiteList.Name = "rbSecurityWhiteList";
			this.rbSecurityWhiteList.Size = new System.Drawing.Size(296, 53);
			this.rbSecurityWhiteList.TabIndex = 36;
			this.rbSecurityWhiteList.TabStop = true;
			this.rbSecurityWhiteList.Text = "Enable White list for Groups or Users for this link in the Web Sales Library";
			this.rbSecurityWhiteList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rbSecurityWhiteList.UseVisualStyleBackColor = false;
			this.rbSecurityWhiteList.CheckedChanged += new System.EventHandler(this.rbSecurityRestricted_CheckedChanged);
			// 
			// rbSecurityDenied
			// 
			this.rbSecurityDenied.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityDenied.BackColor = System.Drawing.Color.Transparent;
			this.rbSecurityDenied.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityDenied.Image = global::FileManager.Properties.Resources.TagsSecurityLocalWidget;
			this.rbSecurityDenied.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.rbSecurityDenied.Location = new System.Drawing.Point(5, 94);
			this.rbSecurityDenied.Name = "rbSecurityDenied";
			this.rbSecurityDenied.Size = new System.Drawing.Size(296, 44);
			this.rbSecurityDenied.TabIndex = 35;
			this.rbSecurityDenied.TabStop = true;
			this.rbSecurityDenied.Text = "This link is ONLY in the Local Sales Library. It is NOT visible in the Web Sales " +
    "Library)";
			this.rbSecurityDenied.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rbSecurityDenied.UseVisualStyleBackColor = false;
			this.rbSecurityDenied.CheckedChanged += new System.EventHandler(this.ValueCheckedChanged);
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.buttonXReset);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(306, 47);
			this.pnButtons.TabIndex = 0;
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Location = new System.Drawing.Point(5, 8);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(296, 30);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 0;
			this.buttonXReset.Text = "RESET ALL SECURITY for the Selected Links";
			this.buttonXReset.TextColor = System.Drawing.Color.Black;
			this.buttonXReset.Click += new System.EventHandler(this.buttonXReset_Click);
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// SecurityEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.laHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SecurityEditor";
			this.Size = new System.Drawing.Size(306, 630);
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditSecurityUserList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSecurityUserList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityGroups)).EndInit();
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			this.pnSecurityUserList.ResumeLayout(false);
			this.pnSecurityUserListGrid.ResumeLayout(false);
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label laHeader;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private DevExpress.XtraEditors.StyleController styleController;
		public System.Windows.Forms.RadioButton rbSecurityAllowed;
		public System.Windows.Forms.RadioButton rbSecurityWhiteList;
		public System.Windows.Forms.RadioButton rbSecurityDenied;
		public System.Windows.Forms.CheckBox ckSecurityShareLink;
		public System.Windows.Forms.RadioButton rbSecurityForbidden;
		private System.Windows.Forms.Panel pnSecurityUserList;
		private DevComponents.DotNetBar.Controls.CircularProgress circularSecurityUserListProgress;
		private DevExpress.XtraEditors.LabelControl laSecurityUserListInfo;
		private System.Windows.Forms.Panel pnSecurityUserListGrid;
		private DevComponents.DotNetBar.ButtonX buttonXSecurityUserListClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXSecurityUserListSelectAll;
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
		public System.Windows.Forms.RadioButton rbSecurityBlackList;
	}
}
