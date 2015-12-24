namespace SalesDepot.SiteManager.ToolForms
{
    partial class FormEditGroup
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
			this.gridViewPages = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnPageId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPageSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditLibrary = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnPageName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridControlLibraries = new DevExpress.XtraGrid.GridControl();
			this.gridViewLibraries = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnLibraryId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLibrarySelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLibraryName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laLogin = new System.Windows.Forms.Label();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
			this.xtraTabControl = new SalesDepot.SiteManager.ToolForms.ValidatableTabControl();
			this.xtraTabPageUsers = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlUsers = new DevExpress.XtraGrid.GridControl();
			this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnUserId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUserSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditUsers = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnUserLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnAssignedUsers2 = new System.Windows.Forms.Panel();
			this.buttonXExportUsers = new DevComponents.DotNetBar.ButtonX();
			this.pnAssignedUsers1 = new System.Windows.Forms.Panel();
			this.buttonXUsersClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXUsersSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageLibraries = new DevExpress.XtraTab.XtraTabPage();
			this.pnAssignedLibraries = new System.Windows.Forms.Panel();
			this.buttonXLibrariesClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLibrariesSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.comboBoxEditName = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditLibrary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageUsers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUsers)).BeginInit();
			this.pnAssignedUsers2.SuspendLayout();
			this.pnAssignedUsers1.SuspendLayout();
			this.xtraTabPageLibraries.SuspendLayout();
			this.pnAssignedLibraries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// gridViewPages
			// 
			this.gridViewPages.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewPages.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewPages.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewPages.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewPages.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.Row.Options.UseFont = true;
			this.gridViewPages.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewPages.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPageId,
            this.gridColumnPageSelected,
            this.gridColumnPageName});
			this.gridViewPages.GridControl = this.gridControlLibraries;
			this.gridViewPages.Name = "gridViewPages";
			this.gridViewPages.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewPages.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewPages.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewPages.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewPages.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewPages.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewPages.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewPages.OptionsCustomization.AllowFilter = false;
			this.gridViewPages.OptionsCustomization.AllowGroup = false;
			this.gridViewPages.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewPages.OptionsCustomization.AllowSort = false;
			this.gridViewPages.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewPages.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewPages.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewPages.OptionsView.ShowColumnHeaders = false;
			this.gridViewPages.OptionsView.ShowGroupPanel = false;
			this.gridViewPages.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewPages.OptionsView.ShowIndicator = false;
			this.gridViewPages.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewPages.RowHeight = 35;
			// 
			// gridColumnPageId
			// 
			this.gridColumnPageId.Caption = "Id";
			this.gridColumnPageId.FieldName = "id";
			this.gridColumnPageId.Name = "gridColumnPageId";
			// 
			// gridColumnPageSelected
			// 
			this.gridColumnPageSelected.Caption = "Selected";
			this.gridColumnPageSelected.ColumnEdit = this.repositoryItemCheckEditLibrary;
			this.gridColumnPageSelected.FieldName = "selected";
			this.gridColumnPageSelected.Name = "gridColumnPageSelected";
			this.gridColumnPageSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnPageSelected.Visible = true;
			this.gridColumnPageSelected.VisibleIndex = 0;
			this.gridColumnPageSelected.Width = 30;
			// 
			// repositoryItemCheckEditLibrary
			// 
			this.repositoryItemCheckEditLibrary.AutoHeight = false;
			this.repositoryItemCheckEditLibrary.Caption = "Check";
			this.repositoryItemCheckEditLibrary.Name = "repositoryItemCheckEditLibrary";
			this.repositoryItemCheckEditLibrary.CheckedChanged += new System.EventHandler(this.RepositoryItemCheckEditCheckedChanged);
			// 
			// gridColumnPageName
			// 
			this.gridColumnPageName.Caption = "Name";
			this.gridColumnPageName.FieldName = "name";
			this.gridColumnPageName.Name = "gridColumnPageName";
			this.gridColumnPageName.OptionsColumn.AllowEdit = false;
			this.gridColumnPageName.OptionsColumn.ReadOnly = true;
			this.gridColumnPageName.Visible = true;
			this.gridColumnPageName.VisibleIndex = 1;
			// 
			// gridControlLibraries
			// 
			this.gridControlLibraries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlLibraries.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlLibraries.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlLibraries.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlLibraries.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			gridLevelNode1.LevelTemplate = this.gridViewPages;
			gridLevelNode1.RelationName = "Pages";
			this.gridControlLibraries.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
			this.gridControlLibraries.Location = new System.Drawing.Point(0, 46);
			this.gridControlLibraries.MainView = this.gridViewLibraries;
			this.gridControlLibraries.Name = "gridControlLibraries";
			this.gridControlLibraries.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditLibrary});
			this.gridControlLibraries.Size = new System.Drawing.Size(375, 448);
			this.gridControlLibraries.TabIndex = 0;
			this.gridControlLibraries.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLibraries,
            this.gridViewPages});
			// 
			// gridViewLibraries
			// 
			this.gridViewLibraries.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewLibraries.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewLibraries.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewLibraries.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewLibraries.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewLibraries.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewLibraries.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewLibraries.Appearance.Row.Options.UseFont = true;
			this.gridViewLibraries.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewLibraries.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewLibraries.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnLibraryId,
            this.gridColumnLibrarySelected,
            this.gridColumnLibraryName});
			this.gridViewLibraries.GridControl = this.gridControlLibraries;
			this.gridViewLibraries.Name = "gridViewLibraries";
			this.gridViewLibraries.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewLibraries.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewLibraries.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewLibraries.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewLibraries.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewLibraries.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewLibraries.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewLibraries.OptionsCustomization.AllowFilter = false;
			this.gridViewLibraries.OptionsCustomization.AllowGroup = false;
			this.gridViewLibraries.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewLibraries.OptionsCustomization.AllowSort = false;
			this.gridViewLibraries.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
			this.gridViewLibraries.OptionsDetail.ShowDetailTabs = false;
			this.gridViewLibraries.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewLibraries.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewLibraries.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewLibraries.OptionsView.ShowColumnHeaders = false;
			this.gridViewLibraries.OptionsView.ShowGroupPanel = false;
			this.gridViewLibraries.OptionsView.ShowIndicator = false;
			this.gridViewLibraries.RowHeight = 35;
			// 
			// gridColumnLibraryId
			// 
			this.gridColumnLibraryId.Caption = "Id";
			this.gridColumnLibraryId.FieldName = "id";
			this.gridColumnLibraryId.Name = "gridColumnLibraryId";
			// 
			// gridColumnLibrarySelected
			// 
			this.gridColumnLibrarySelected.Caption = "Selected";
			this.gridColumnLibrarySelected.ColumnEdit = this.repositoryItemCheckEditLibrary;
			this.gridColumnLibrarySelected.FieldName = "selected";
			this.gridColumnLibrarySelected.Name = "gridColumnLibrarySelected";
			this.gridColumnLibrarySelected.OptionsColumn.FixedWidth = true;
			this.gridColumnLibrarySelected.Visible = true;
			this.gridColumnLibrarySelected.VisibleIndex = 0;
			this.gridColumnLibrarySelected.Width = 30;
			// 
			// gridColumnLibraryName
			// 
			this.gridColumnLibraryName.Caption = "Name";
			this.gridColumnLibraryName.FieldName = "name";
			this.gridColumnLibraryName.Name = "gridColumnLibraryName";
			this.gridColumnLibraryName.OptionsColumn.AllowEdit = false;
			this.gridColumnLibraryName.OptionsColumn.ReadOnly = true;
			this.gridColumnLibraryName.Visible = true;
			this.gridColumnLibraryName.VisibleIndex = 1;
			this.gridColumnLibraryName.Width = 355;
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
			// laLogin
			// 
			this.laLogin.AutoSize = true;
			this.laLogin.BackColor = System.Drawing.Color.White;
			this.laLogin.ForeColor = System.Drawing.Color.Black;
			this.laLogin.Location = new System.Drawing.Point(8, 9);
			this.laLogin.Name = "laLogin";
			this.laLogin.Size = new System.Drawing.Size(85, 16);
			this.laLogin.TabIndex = 0;
			this.laLogin.Text = "Group Name:";
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
			this.xtraTabControl.Location = new System.Drawing.Point(2, 49);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageUsers;
			this.xtraTabControl.Size = new System.Drawing.Size(377, 522);
			this.xtraTabControl.TabIndex = 14;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageUsers,
            this.xtraTabPageLibraries});
			// 
			// xtraTabPageUsers
			// 
			this.xtraTabPageUsers.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageUsers.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageUsers.Controls.Add(this.gridControlUsers);
			this.xtraTabPageUsers.Controls.Add(this.pnAssignedUsers2);
			this.xtraTabPageUsers.Controls.Add(this.pnAssignedUsers1);
			this.xtraTabPageUsers.Name = "xtraTabPageUsers";
			this.xtraTabPageUsers.Size = new System.Drawing.Size(375, 494);
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
            this.repositoryItemCheckEditUsers});
			this.gridControlUsers.Size = new System.Drawing.Size(375, 391);
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
			this.gridColumnUserSelected.ColumnEdit = this.repositoryItemCheckEditUsers;
			this.gridColumnUserSelected.FieldName = "selected";
			this.gridColumnUserSelected.Name = "gridColumnUserSelected";
			this.gridColumnUserSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnUserSelected.Visible = true;
			this.gridColumnUserSelected.VisibleIndex = 0;
			this.gridColumnUserSelected.Width = 30;
			// 
			// repositoryItemCheckEditUsers
			// 
			this.repositoryItemCheckEditUsers.AutoHeight = false;
			this.repositoryItemCheckEditUsers.Caption = "Check";
			this.repositoryItemCheckEditUsers.Name = "repositoryItemCheckEditUsers";
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
			// pnAssignedUsers2
			// 
			this.pnAssignedUsers2.Controls.Add(this.buttonXExportUsers);
			this.pnAssignedUsers2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnAssignedUsers2.ForeColor = System.Drawing.Color.Black;
			this.pnAssignedUsers2.Location = new System.Drawing.Point(0, 437);
			this.pnAssignedUsers2.Name = "pnAssignedUsers2";
			this.pnAssignedUsers2.Size = new System.Drawing.Size(375, 57);
			this.pnAssignedUsers2.TabIndex = 3;
			// 
			// buttonXExportUsers
			// 
			this.buttonXExportUsers.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExportUsers.CausesValidation = false;
			this.buttonXExportUsers.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExportUsers.Image = global::SalesDepot.SiteManager.Properties.Resources.ExportGroup;
			this.buttonXExportUsers.Location = new System.Drawing.Point(71, 5);
			this.buttonXExportUsers.Name = "buttonXExportUsers";
			this.buttonXExportUsers.Size = new System.Drawing.Size(227, 47);
			this.buttonXExportUsers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExportUsers.TabIndex = 13;
			this.buttonXExportUsers.Text = "Export to Excel";
			this.buttonXExportUsers.TextColor = System.Drawing.Color.Black;
			this.buttonXExportUsers.Click += new System.EventHandler(this.buttonXExportUsers_Click);
			// 
			// pnAssignedUsers1
			// 
			this.pnAssignedUsers1.BackColor = System.Drawing.Color.Transparent;
			this.pnAssignedUsers1.Controls.Add(this.buttonXUsersClearAll);
			this.pnAssignedUsers1.Controls.Add(this.buttonXUsersSelectAll);
			this.pnAssignedUsers1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnAssignedUsers1.ForeColor = System.Drawing.Color.Black;
			this.pnAssignedUsers1.Location = new System.Drawing.Point(0, 0);
			this.pnAssignedUsers1.Name = "pnAssignedUsers1";
			this.pnAssignedUsers1.Size = new System.Drawing.Size(375, 46);
			this.pnAssignedUsers1.TabIndex = 2;
			// 
			// buttonXUsersClearAll
			// 
			this.buttonXUsersClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUsersClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXUsersClearAll.CausesValidation = false;
			this.buttonXUsersClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUsersClearAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUsersClearAll.Location = new System.Drawing.Point(256, 7);
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
			// xtraTabPageLibraries
			// 
			this.xtraTabPageLibraries.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageLibraries.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageLibraries.Controls.Add(this.gridControlLibraries);
			this.xtraTabPageLibraries.Controls.Add(this.pnAssignedLibraries);
			this.xtraTabPageLibraries.Name = "xtraTabPageLibraries";
			this.xtraTabPageLibraries.Size = new System.Drawing.Size(375, 494);
			this.xtraTabPageLibraries.Text = "Assigned Libraries";
			// 
			// pnAssignedLibraries
			// 
			this.pnAssignedLibraries.Controls.Add(this.buttonXLibrariesClearAll);
			this.pnAssignedLibraries.Controls.Add(this.buttonXLibrariesSelectAll);
			this.pnAssignedLibraries.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnAssignedLibraries.ForeColor = System.Drawing.Color.Black;
			this.pnAssignedLibraries.Location = new System.Drawing.Point(0, 0);
			this.pnAssignedLibraries.Name = "pnAssignedLibraries";
			this.pnAssignedLibraries.Size = new System.Drawing.Size(375, 46);
			this.pnAssignedLibraries.TabIndex = 1;
			// 
			// buttonXLibrariesClearAll
			// 
			this.buttonXLibrariesClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLibrariesClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLibrariesClearAll.CausesValidation = false;
			this.buttonXLibrariesClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLibrariesClearAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXLibrariesClearAll.Location = new System.Drawing.Point(256, 7);
			this.buttonXLibrariesClearAll.Name = "buttonXLibrariesClearAll";
			this.buttonXLibrariesClearAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXLibrariesClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLibrariesClearAll.TabIndex = 14;
			this.buttonXLibrariesClearAll.Text = "Clear All";
			this.buttonXLibrariesClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXLibrariesClearAll.Click += new System.EventHandler(this.buttonXLibrariesClearAll_Click);
			// 
			// buttonXLibrariesSelectAll
			// 
			this.buttonXLibrariesSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLibrariesSelectAll.CausesValidation = false;
			this.buttonXLibrariesSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLibrariesSelectAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXLibrariesSelectAll.Location = new System.Drawing.Point(29, 7);
			this.buttonXLibrariesSelectAll.Name = "buttonXLibrariesSelectAll";
			this.buttonXLibrariesSelectAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXLibrariesSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLibrariesSelectAll.TabIndex = 13;
			this.buttonXLibrariesSelectAll.Text = "Select All";
			this.buttonXLibrariesSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXLibrariesSelectAll.Click += new System.EventHandler(this.buttonXLibrariesSelectAll_Click);
			// 
			// comboBoxEditName
			// 
			this.comboBoxEditName.Location = new System.Drawing.Point(118, 6);
			this.comboBoxEditName.Name = "comboBoxEditName";
			this.comboBoxEditName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.comboBoxEditName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.comboBoxEditName.Properties.Appearance.Options.UseBackColor = true;
			this.comboBoxEditName.Properties.Appearance.Options.UseForeColor = true;
			this.comboBoxEditName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditName.Properties.NullText = "Select or Type Name...";
			this.comboBoxEditName.Size = new System.Drawing.Size(260, 22);
			this.comboBoxEditName.StyleController = this.styleController;
			this.comboBoxEditName.TabIndex = 15;
			this.comboBoxEditName.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxEditName_Validating);
			// 
			// FormEditGroup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.ClientSize = new System.Drawing.Size(381, 617);
			this.Controls.Add(this.comboBoxEditName);
			this.Controls.Add(this.laLogin);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditGroup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit User";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditGroup_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditLibrary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageUsers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUsers)).EndInit();
			this.pnAssignedUsers2.ResumeLayout(false);
			this.pnAssignedUsers1.ResumeLayout(false);
			this.xtraTabPageLibraries.ResumeLayout(false);
			this.pnAssignedLibraries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Label laLogin;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
		private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
		private ValidatableTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageLibraries;
		private DevExpress.XtraGrid.GridControl gridControlLibraries;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewLibraries;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLibraryId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLibrarySelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditLibrary;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLibraryName;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewPages;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPageId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPageSelected;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPageName;
		private System.Windows.Forms.Panel pnAssignedLibraries;
		private DevComponents.DotNetBar.ButtonX buttonXLibrariesClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXLibrariesSelectAll;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageUsers;
		private DevExpress.XtraGrid.GridControl gridControlUsers;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUserId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUserSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUsers;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUserLogin;
		private System.Windows.Forms.Panel pnAssignedUsers1;
		private DevComponents.DotNetBar.ButtonX buttonXUsersClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXUsersSelectAll;
		public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditName;
		private System.Windows.Forms.Panel pnAssignedUsers2;
		private DevComponents.DotNetBar.ButtonX buttonXExportUsers;
    }
}