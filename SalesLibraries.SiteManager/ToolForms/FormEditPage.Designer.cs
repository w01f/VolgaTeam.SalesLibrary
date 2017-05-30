namespace SalesLibraries.SiteManager.ToolForms
{
    partial class FormEditPage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laLibrary = new System.Windows.Forms.Label();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
			this.xtraTabControl = new SalesLibraries.SiteManager.ToolForms.ValidatableTabControl();
			this.xtraTabPageUsers = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlUsers = new DevExpress.XtraGrid.GridControl();
			this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnUserId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUserSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditUser = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnUserLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnAssignedUsers = new System.Windows.Forms.Panel();
			this.buttonXUsersClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXUsersSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageGroups = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlGroups = new DevExpress.XtraGrid.GridControl();
			this.gridViewGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnGroupSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditGroup = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnAssignedGroups = new System.Windows.Forms.Panel();
			this.buttonXGroupsClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXGroupsSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.laPage = new System.Windows.Forms.Label();
			this.checkEditapplyForLibrary = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageUsers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUser)).BeginInit();
			this.pnAssignedUsers.SuspendLayout();
			this.xtraTabPageGroups.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditGroup)).BeginInit();
			this.pnAssignedGroups.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditapplyForLibrary.Properties)).BeginInit();
			this.SuspendLayout();
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
			// laLibrary
			// 
			this.laLibrary.AutoSize = true;
			this.laLibrary.BackColor = System.Drawing.Color.White;
			this.laLibrary.ForeColor = System.Drawing.Color.Black;
			this.laLibrary.Location = new System.Drawing.Point(8, 9);
			this.laLibrary.Name = "laLibrary";
			this.laLibrary.Size = new System.Drawing.Size(70, 16);
			this.laLibrary.TabIndex = 0;
			this.laLibrary.Text = "Library: {0}";
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(102, 577);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(75, 33);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 11;
			this.buttonXSave.Text = "Save";
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.CausesValidation = false;
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(204, 577);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(75, 33);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 12;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// dxErrorProvider
			// 
			this.dxErrorProvider.ContainerControl = this;
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControl.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControl.Appearance.Options.UseBackColor = true;
			this.xtraTabControl.Appearance.Options.UseForeColor = true;
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
			this.xtraTabControl.Location = new System.Drawing.Point(2, 99);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageUsers;
			this.xtraTabControl.Size = new System.Drawing.Size(377, 472);
			this.xtraTabControl.TabIndex = 14;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageUsers,
            this.xtraTabPageGroups});
			// 
			// xtraTabPageUsers
			// 
			this.xtraTabPageUsers.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageUsers.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageUsers.Controls.Add(this.gridControlUsers);
			this.xtraTabPageUsers.Controls.Add(this.pnAssignedUsers);
			this.xtraTabPageUsers.Name = "xtraTabPageUsers";
			this.xtraTabPageUsers.Size = new System.Drawing.Size(371, 441);
			this.xtraTabPageUsers.Text = "Users";
			// 
			// gridControlUsers
			// 
			this.gridControlUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlUsers.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlUsers.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlUsers.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlUsers.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlUsers.Location = new System.Drawing.Point(0, 46);
			this.gridControlUsers.MainView = this.gridViewUsers;
			this.gridControlUsers.Name = "gridControlUsers";
			this.gridControlUsers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditUser});
			this.gridControlUsers.Size = new System.Drawing.Size(371, 395);
			this.gridControlUsers.TabIndex = 1;
			this.gridControlUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers});
			// 
			// gridViewUsers
			// 
			this.gridViewUsers.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewUsers.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewUsers.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewUsers.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewUsers.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewUsers.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.Row.Options.UseFont = true;
			this.gridViewUsers.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnUserId,
            this.gridColumnUserSelected,
            this.gridColumnUserLogin});
			this.gridViewUsers.GridControl = this.gridControlUsers;
			this.gridViewUsers.Name = "gridViewUsers";
			this.gridViewUsers.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewUsers.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewUsers.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewUsers.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewUsers.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewUsers.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewUsers.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewUsers.OptionsCustomization.AllowFilter = false;
			this.gridViewUsers.OptionsCustomization.AllowGroup = false;
			this.gridViewUsers.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewUsers.OptionsCustomization.AllowSort = false;
			this.gridViewUsers.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
			this.gridViewUsers.OptionsDetail.ShowDetailTabs = false;
			this.gridViewUsers.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewUsers.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewUsers.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewUsers.OptionsView.ShowColumnHeaders = false;
			this.gridViewUsers.OptionsView.ShowGroupPanel = false;
			this.gridViewUsers.OptionsView.ShowIndicator = false;
			this.gridViewUsers.RowHeight = 35;
			// 
			// gridColumnUserId
			// 
			this.gridColumnUserId.Caption = "Id";
			this.gridColumnUserId.FieldName = "id";
			this.gridColumnUserId.Name = "gridColumnUserId";
			// 
			// gridColumnUserSelected
			// 
			this.gridColumnUserSelected.Caption = "Selected";
			this.gridColumnUserSelected.ColumnEdit = this.repositoryItemCheckEditUser;
			this.gridColumnUserSelected.FieldName = "selected";
			this.gridColumnUserSelected.Name = "gridColumnUserSelected";
			this.gridColumnUserSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnUserSelected.Visible = true;
			this.gridColumnUserSelected.VisibleIndex = 0;
			this.gridColumnUserSelected.Width = 30;
			// 
			// repositoryItemCheckEditUser
			// 
			this.repositoryItemCheckEditUser.AutoHeight = false;
			this.repositoryItemCheckEditUser.Caption = "Check";
			this.repositoryItemCheckEditUser.Name = "repositoryItemCheckEditUser";
			// 
			// gridColumnUserLogin
			// 
			this.gridColumnUserLogin.Caption = "Login";
			this.gridColumnUserLogin.FieldName = "LoginWithName";
			this.gridColumnUserLogin.Name = "gridColumnUserLogin";
			this.gridColumnUserLogin.OptionsColumn.AllowEdit = false;
			this.gridColumnUserLogin.OptionsColumn.ReadOnly = true;
			this.gridColumnUserLogin.Visible = true;
			this.gridColumnUserLogin.VisibleIndex = 1;
			this.gridColumnUserLogin.Width = 80;
			// 
			// pnAssignedUsers
			// 
			this.pnAssignedUsers.Controls.Add(this.buttonXUsersClearAll);
			this.pnAssignedUsers.Controls.Add(this.buttonXUsersSelectAll);
			this.pnAssignedUsers.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnAssignedUsers.ForeColor = System.Drawing.Color.Black;
			this.pnAssignedUsers.Location = new System.Drawing.Point(0, 0);
			this.pnAssignedUsers.Name = "pnAssignedUsers";
			this.pnAssignedUsers.Size = new System.Drawing.Size(371, 46);
			this.pnAssignedUsers.TabIndex = 2;
			// 
			// buttonXUsersClearAll
			// 
			this.buttonXUsersClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUsersClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXUsersClearAll.CausesValidation = false;
			this.buttonXUsersClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUsersClearAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUsersClearAll.Location = new System.Drawing.Point(252, 7);
			this.buttonXUsersClearAll.Name = "buttonXUsersClearAll";
			this.buttonXUsersClearAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXUsersClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUsersClearAll.TabIndex = 14;
			this.buttonXUsersClearAll.Text = "Clear All";
			this.buttonXUsersClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXUsersClearAll.Click += new System.EventHandler(this.buttonXUsersClearAll_Click);
			// 
			// buttonXUsersSelectAll
			// 
			this.buttonXUsersSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUsersSelectAll.CausesValidation = false;
			this.buttonXUsersSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUsersSelectAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUsersSelectAll.Location = new System.Drawing.Point(29, 7);
			this.buttonXUsersSelectAll.Name = "buttonXUsersSelectAll";
			this.buttonXUsersSelectAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXUsersSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUsersSelectAll.TabIndex = 13;
			this.buttonXUsersSelectAll.Text = "Select All";
			this.buttonXUsersSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXUsersSelectAll.Click += new System.EventHandler(this.buttonXUsersSelectAll_Click);
			// 
			// xtraTabPageGroups
			// 
			this.xtraTabPageGroups.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageGroups.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageGroups.Controls.Add(this.gridControlGroups);
			this.xtraTabPageGroups.Controls.Add(this.pnAssignedGroups);
			this.xtraTabPageGroups.Name = "xtraTabPageGroups";
			this.xtraTabPageGroups.Size = new System.Drawing.Size(371, 441);
			this.xtraTabPageGroups.Text = "Groups";
			// 
			// gridControlGroups
			// 
			this.gridControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlGroups.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlGroups.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlGroups.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlGroups.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlGroups.Location = new System.Drawing.Point(0, 46);
			this.gridControlGroups.MainView = this.gridViewGroups;
			this.gridControlGroups.Name = "gridControlGroups";
			this.gridControlGroups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditGroup});
			this.gridControlGroups.Size = new System.Drawing.Size(371, 395);
			this.gridControlGroups.TabIndex = 2;
			this.gridControlGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGroups});
			// 
			// gridViewGroups
			// 
			this.gridViewGroups.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewGroups.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewGroups.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewGroups.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewGroups.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewGroups.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.Row.Options.UseFont = true;
			this.gridViewGroups.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnGroupId,
            this.gridColumnGroupSelected,
            this.gridColumnGroupName});
			this.gridViewGroups.GridControl = this.gridControlGroups;
			this.gridViewGroups.Name = "gridViewGroups";
			this.gridViewGroups.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewGroups.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewGroups.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewGroups.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewGroups.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewGroups.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewGroups.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewGroups.OptionsCustomization.AllowFilter = false;
			this.gridViewGroups.OptionsCustomization.AllowGroup = false;
			this.gridViewGroups.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewGroups.OptionsCustomization.AllowSort = false;
			this.gridViewGroups.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
			this.gridViewGroups.OptionsDetail.ShowDetailTabs = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewGroups.OptionsView.ShowColumnHeaders = false;
			this.gridViewGroups.OptionsView.ShowGroupPanel = false;
			this.gridViewGroups.OptionsView.ShowIndicator = false;
			this.gridViewGroups.RowHeight = 35;
			// 
			// gridColumnGroupId
			// 
			this.gridColumnGroupId.Caption = "Id";
			this.gridColumnGroupId.FieldName = "id";
			this.gridColumnGroupId.Name = "gridColumnGroupId";
			// 
			// gridColumnGroupSelected
			// 
			this.gridColumnGroupSelected.Caption = "Selected";
			this.gridColumnGroupSelected.ColumnEdit = this.repositoryItemCheckEditGroup;
			this.gridColumnGroupSelected.FieldName = "selected";
			this.gridColumnGroupSelected.Name = "gridColumnGroupSelected";
			this.gridColumnGroupSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnGroupSelected.Visible = true;
			this.gridColumnGroupSelected.VisibleIndex = 0;
			this.gridColumnGroupSelected.Width = 30;
			// 
			// repositoryItemCheckEditGroup
			// 
			this.repositoryItemCheckEditGroup.AutoHeight = false;
			this.repositoryItemCheckEditGroup.Caption = "Check";
			this.repositoryItemCheckEditGroup.Name = "repositoryItemCheckEditGroup";
			// 
			// gridColumnGroupName
			// 
			this.gridColumnGroupName.Caption = "Group";
			this.gridColumnGroupName.FieldName = "name";
			this.gridColumnGroupName.Name = "gridColumnGroupName";
			this.gridColumnGroupName.OptionsColumn.AllowEdit = false;
			this.gridColumnGroupName.OptionsColumn.ReadOnly = true;
			this.gridColumnGroupName.Visible = true;
			this.gridColumnGroupName.VisibleIndex = 1;
			this.gridColumnGroupName.Width = 80;
			// 
			// pnAssignedGroups
			// 
			this.pnAssignedGroups.Controls.Add(this.buttonXGroupsClearAll);
			this.pnAssignedGroups.Controls.Add(this.buttonXGroupsSelectAll);
			this.pnAssignedGroups.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnAssignedGroups.ForeColor = System.Drawing.Color.Black;
			this.pnAssignedGroups.Location = new System.Drawing.Point(0, 0);
			this.pnAssignedGroups.Name = "pnAssignedGroups";
			this.pnAssignedGroups.Size = new System.Drawing.Size(371, 46);
			this.pnAssignedGroups.TabIndex = 1;
			// 
			// buttonXGroupsClearAll
			// 
			this.buttonXGroupsClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroupsClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXGroupsClearAll.CausesValidation = false;
			this.buttonXGroupsClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroupsClearAll.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXGroupsClearAll.Location = new System.Drawing.Point(252, 7);
			this.buttonXGroupsClearAll.Name = "buttonXGroupsClearAll";
			this.buttonXGroupsClearAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXGroupsClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroupsClearAll.TabIndex = 14;
			this.buttonXGroupsClearAll.Text = "Clear All";
			this.buttonXGroupsClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXGroupsClearAll.Click += new System.EventHandler(this.buttonXGroupsClearAll_Click);
			// 
			// buttonXGroupsSelectAll
			// 
			this.buttonXGroupsSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroupsSelectAll.CausesValidation = false;
			this.buttonXGroupsSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroupsSelectAll.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXGroupsSelectAll.Location = new System.Drawing.Point(29, 7);
			this.buttonXGroupsSelectAll.Name = "buttonXGroupsSelectAll";
			this.buttonXGroupsSelectAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXGroupsSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroupsSelectAll.TabIndex = 13;
			this.buttonXGroupsSelectAll.Text = "Select All";
			this.buttonXGroupsSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXGroupsSelectAll.Click += new System.EventHandler(this.buttonXGroupsSelectAll_Click);
			// 
			// laPage
			// 
			this.laPage.AutoSize = true;
			this.laPage.BackColor = System.Drawing.Color.White;
			this.laPage.ForeColor = System.Drawing.Color.Black;
			this.laPage.Location = new System.Drawing.Point(8, 37);
			this.laPage.Name = "laPage";
			this.laPage.Size = new System.Drawing.Size(61, 16);
			this.laPage.TabIndex = 15;
			this.laPage.Text = "Page: {0}";
			// 
			// checkEditapplyForLibrary
			// 
			this.checkEditapplyForLibrary.Location = new System.Drawing.Point(9, 65);
			this.checkEditapplyForLibrary.Name = "checkEditapplyForLibrary";
			this.checkEditapplyForLibrary.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.checkEditapplyForLibrary.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditapplyForLibrary.Properties.Appearance.Options.UseFont = true;
			this.checkEditapplyForLibrary.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditapplyForLibrary.Properties.AutoWidth = true;
			this.checkEditapplyForLibrary.Properties.Caption = "Apply Permissions for WHOLE {0} Library";
			this.checkEditapplyForLibrary.Size = new System.Drawing.Size(281, 20);
			this.checkEditapplyForLibrary.StyleController = this.styleController;
			this.checkEditapplyForLibrary.TabIndex = 16;
			// 
			// FormEditPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.ClientSize = new System.Drawing.Size(381, 617);
			this.Controls.Add(this.checkEditapplyForLibrary);
			this.Controls.Add(this.laPage);
			this.Controls.Add(this.laLibrary);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditPage";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit User";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditPage_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageUsers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUser)).EndInit();
			this.pnAssignedUsers.ResumeLayout(false);
			this.xtraTabPageGroups.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditGroup)).EndInit();
			this.pnAssignedGroups.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditapplyForLibrary.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
		private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
		private ValidatableTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageGroups;
		private System.Windows.Forms.Panel pnAssignedGroups;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsSelectAll;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageUsers;
		private DevExpress.XtraGrid.GridControl gridControlUsers;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUserId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUserSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUser;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUserLogin;
		private System.Windows.Forms.Panel pnAssignedUsers;
		private DevComponents.DotNetBar.ButtonX buttonXUsersClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXUsersSelectAll;
		public DevExpress.XtraEditors.CheckEdit checkEditapplyForLibrary;
		public System.Windows.Forms.Label laPage;
		public System.Windows.Forms.Label laLibrary;
		private DevExpress.XtraGrid.GridControl gridControlGroups;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewGroups;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditGroup;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupName;
    }
}