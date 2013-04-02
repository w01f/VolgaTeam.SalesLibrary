namespace SalesDepot.SiteManager.ToolForms
{
    partial class FormEditUser
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
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
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laLogin = new System.Windows.Forms.Label();
			this.textEditLogin = new DevExpress.XtraEditors.TextEdit();
			this.laFirstName = new System.Windows.Forms.Label();
			this.textEditFirstName = new DevExpress.XtraEditors.TextEdit();
			this.textEditLastName = new DevExpress.XtraEditors.TextEdit();
			this.laLastName = new System.Windows.Forms.Label();
			this.textEditEmail = new DevExpress.XtraEditors.TextEdit();
			this.laEmail = new System.Windows.Forms.Label();
			this.laPassword = new System.Windows.Forms.Label();
			this.checkEditPassword = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			this.buttonEditPassword = new DevExpress.XtraEditors.ButtonEdit();
			this.textEditEmailConfirm = new DevExpress.XtraEditors.TextEdit();
			this.textEditPhone = new DevExpress.XtraEditors.TextEdit();
			this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
			this.xtraTabControl = new SalesDepot.SiteManager.ToolForms.ValidatableTabControl();
			this.xtraTabPageUser = new DevExpress.XtraTab.XtraTabPage();
			this.laPhone = new System.Windows.Forms.Label();
			this.laEmailConfirm = new System.Windows.Forms.Label();
			this.xtraTabPageGroups = new DevExpress.XtraTab.XtraTabPage();
			this.pnAssignedGroups = new System.Windows.Forms.Panel();
			this.buttonXGroupsClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXGroupsSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.gridControlGroups = new DevExpress.XtraGrid.GridControl();
			this.gridViewGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnGroupSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditGroup = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.xtraTabPageLibraries = new DevExpress.XtraTab.XtraTabPage();
			this.pnAssignedLibraries = new System.Windows.Forms.Panel();
			this.buttonXLibrariesClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLibrariesSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditLibrary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLogin.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditFirstName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLastName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmail.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailConfirm.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPhone.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageUser.SuspendLayout();
			this.xtraTabPageGroups.SuspendLayout();
			this.pnAssignedGroups.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditGroup)).BeginInit();
			this.xtraTabPageLibraries.SuspendLayout();
			this.pnAssignedLibraries.SuspendLayout();
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
			this.gridViewPages.OptionsView.ShowHorzLines = false;
			this.gridViewPages.OptionsView.ShowIndicator = false;
			this.gridViewPages.OptionsView.ShowVertLines = false;
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
			gridLevelNode1.LevelTemplate = this.gridViewPages;
			gridLevelNode1.RelationName = "Pages";
			this.gridControlLibraries.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
			this.gridControlLibraries.Location = new System.Drawing.Point(0, 46);
			this.gridControlLibraries.MainView = this.gridViewLibraries;
			this.gridControlLibraries.Name = "gridControlLibraries";
			this.gridControlLibraries.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditLibrary});
			this.gridControlLibraries.Size = new System.Drawing.Size(369, 491);
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
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Lilian";
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
			this.laLogin.Location = new System.Drawing.Point(3, 11);
			this.laLogin.Name = "laLogin";
			this.laLogin.Size = new System.Drawing.Size(43, 16);
			this.laLogin.TabIndex = 0;
			this.laLogin.Text = "Login:";
			// 
			// textEditLogin
			// 
			this.textEditLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dxValidationProvider.SetIconAlignment(this.textEditLogin, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditLogin, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditLogin.Location = new System.Drawing.Point(102, 8);
			this.textEditLogin.Name = "textEditLogin";
			this.textEditLogin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.textEditLogin.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.textEditLogin.Properties.NullText = "Type...";
			this.textEditLogin.Size = new System.Drawing.Size(251, 22);
			this.textEditLogin.StyleController = this.styleController;
			this.textEditLogin.TabIndex = 1;
			this.textEditLogin.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
			// 
			// laFirstName
			// 
			this.laFirstName.AutoSize = true;
			this.laFirstName.Location = new System.Drawing.Point(3, 48);
			this.laFirstName.Name = "laFirstName";
			this.laFirstName.Size = new System.Drawing.Size(76, 16);
			this.laFirstName.TabIndex = 2;
			this.laFirstName.Text = "First Name:";
			// 
			// textEditFirstName
			// 
			this.textEditFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dxValidationProvider.SetIconAlignment(this.textEditFirstName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditFirstName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditFirstName.Location = new System.Drawing.Point(102, 45);
			this.textEditFirstName.Name = "textEditFirstName";
			this.textEditFirstName.Properties.NullText = "Type...";
			this.textEditFirstName.Size = new System.Drawing.Size(251, 22);
			this.textEditFirstName.StyleController = this.styleController;
			this.textEditFirstName.TabIndex = 3;
			this.textEditFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
			// 
			// textEditLastName
			// 
			this.textEditLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dxValidationProvider.SetIconAlignment(this.textEditLastName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditLastName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditLastName.Location = new System.Drawing.Point(102, 85);
			this.textEditLastName.Name = "textEditLastName";
			this.textEditLastName.Properties.NullText = "Type...";
			this.textEditLastName.Size = new System.Drawing.Size(251, 22);
			this.textEditLastName.StyleController = this.styleController;
			this.textEditLastName.TabIndex = 5;
			this.textEditLastName.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
			// 
			// laLastName
			// 
			this.laLastName.AutoSize = true;
			this.laLastName.Location = new System.Drawing.Point(3, 88);
			this.laLastName.Name = "laLastName";
			this.laLastName.Size = new System.Drawing.Size(75, 16);
			this.laLastName.TabIndex = 4;
			this.laLastName.Text = "Last Name:";
			// 
			// textEditEmail
			// 
			this.textEditEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dxValidationProvider.SetIconAlignment(this.textEditEmail, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditEmail, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditEmail.Location = new System.Drawing.Point(102, 166);
			this.textEditEmail.Name = "textEditEmail";
			this.textEditEmail.Properties.Mask.EditMask = "(\\w|[\\.\\-])+@(\\w|[\\-]+\\.)*(\\w|[\\-]){2,63}\\.[a-zA-Z]{2,4}";
			this.textEditEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
			this.textEditEmail.Properties.NullText = "Type...";
			this.textEditEmail.Size = new System.Drawing.Size(251, 22);
			this.textEditEmail.StyleController = this.styleController;
			this.textEditEmail.TabIndex = 7;
			this.textEditEmail.Validating += new System.ComponentModel.CancelEventHandler(this.textEditEmail_Validating);
			// 
			// laEmail
			// 
			this.laEmail.AutoSize = true;
			this.laEmail.Location = new System.Drawing.Point(3, 169);
			this.laEmail.Name = "laEmail";
			this.laEmail.Size = new System.Drawing.Size(45, 16);
			this.laEmail.TabIndex = 6;
			this.laEmail.Text = "Email:";
			// 
			// laPassword
			// 
			this.laPassword.AutoSize = true;
			this.laPassword.Location = new System.Drawing.Point(3, 250);
			this.laPassword.Name = "laPassword";
			this.laPassword.Size = new System.Drawing.Size(69, 16);
			this.laPassword.TabIndex = 8;
			this.laPassword.Text = "Password:";
			// 
			// checkEditPassword
			// 
			this.checkEditPassword.EditValue = true;
			this.checkEditPassword.Location = new System.Drawing.Point(3, 238);
			this.checkEditPassword.Name = "checkEditPassword";
			this.checkEditPassword.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditPassword.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditPassword.Properties.AutoHeight = false;
			this.checkEditPassword.Properties.Caption = "Reset password";
			this.checkEditPassword.Size = new System.Drawing.Size(85, 43);
			this.checkEditPassword.StyleController = this.styleController;
			this.checkEditPassword.TabIndex = 10;
			this.checkEditPassword.Visible = false;
			this.checkEditPassword.CheckedChanged += new System.EventHandler(this.checkEditPassword_CheckedChanged);
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
			// buttonEditPassword
			// 
			this.buttonEditPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dxValidationProvider.SetIconAlignment(this.buttonEditPassword, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.buttonEditPassword, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.buttonEditPassword.Location = new System.Drawing.Point(102, 247);
			this.buttonEditPassword.Name = "buttonEditPassword";
			this.buttonEditPassword.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.buttonEditPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Generate new password", null, null, true)});
			this.buttonEditPassword.Properties.NullText = "Type...";
			this.buttonEditPassword.Size = new System.Drawing.Size(251, 22);
			this.buttonEditPassword.StyleController = this.styleController;
			this.buttonEditPassword.TabIndex = 13;
			this.buttonEditPassword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditPassword_ButtonClick);
			// 
			// textEditEmailConfirm
			// 
			this.textEditEmailConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dxValidationProvider.SetIconAlignment(this.textEditEmailConfirm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditEmailConfirm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditEmailConfirm.Location = new System.Drawing.Point(102, 205);
			this.textEditEmailConfirm.Name = "textEditEmailConfirm";
			this.textEditEmailConfirm.Properties.Mask.EditMask = "(\\w|[\\.\\-])+@(\\w|[\\-]+\\.)*(\\w|[\\-]){2,63}\\.[a-zA-Z]{2,4}";
			this.textEditEmailConfirm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
			this.textEditEmailConfirm.Properties.NullText = "Type...";
			this.textEditEmailConfirm.Size = new System.Drawing.Size(251, 22);
			this.textEditEmailConfirm.StyleController = this.styleController;
			this.textEditEmailConfirm.TabIndex = 15;
			this.textEditEmailConfirm.Validating += new System.ComponentModel.CancelEventHandler(this.textEditEmail_Validating);
			// 
			// textEditPhone
			// 
			this.textEditPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dxValidationProvider.SetIconAlignment(this.textEditPhone, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditPhone, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditPhone.Location = new System.Drawing.Point(102, 126);
			this.textEditPhone.Name = "textEditPhone";
			this.textEditPhone.Properties.NullText = "Type...";
			this.textEditPhone.Size = new System.Drawing.Size(251, 22);
			this.textEditPhone.StyleController = this.styleController;
			this.textEditPhone.TabIndex = 17;
			this.textEditPhone.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
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
			this.xtraTabControl.Location = new System.Drawing.Point(2, 2);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageUser;
			this.xtraTabControl.Size = new System.Drawing.Size(377, 569);
			this.xtraTabControl.TabIndex = 14;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageUser,
            this.xtraTabPageGroups,
            this.xtraTabPageLibraries});
			// 
			// xtraTabPageUser
			// 
			this.xtraTabPageUser.Controls.Add(this.laPhone);
			this.xtraTabPageUser.Controls.Add(this.textEditPhone);
			this.xtraTabPageUser.Controls.Add(this.textEditEmailConfirm);
			this.xtraTabPageUser.Controls.Add(this.laEmailConfirm);
			this.xtraTabPageUser.Controls.Add(this.laLogin);
			this.xtraTabPageUser.Controls.Add(this.buttonEditPassword);
			this.xtraTabPageUser.Controls.Add(this.checkEditPassword);
			this.xtraTabPageUser.Controls.Add(this.textEditLogin);
			this.xtraTabPageUser.Controls.Add(this.laFirstName);
			this.xtraTabPageUser.Controls.Add(this.laPassword);
			this.xtraTabPageUser.Controls.Add(this.textEditFirstName);
			this.xtraTabPageUser.Controls.Add(this.textEditEmail);
			this.xtraTabPageUser.Controls.Add(this.laLastName);
			this.xtraTabPageUser.Controls.Add(this.laEmail);
			this.xtraTabPageUser.Controls.Add(this.textEditLastName);
			this.xtraTabPageUser.Name = "xtraTabPageUser";
			this.xtraTabPageUser.Size = new System.Drawing.Size(369, 537);
			this.xtraTabPageUser.Text = "User";
			// 
			// laPhone
			// 
			this.laPhone.AutoSize = true;
			this.laPhone.Location = new System.Drawing.Point(3, 129);
			this.laPhone.Name = "laPhone";
			this.laPhone.Size = new System.Drawing.Size(49, 16);
			this.laPhone.TabIndex = 16;
			this.laPhone.Text = "Phone:";
			// 
			// laEmailConfirm
			// 
			this.laEmailConfirm.AutoSize = true;
			this.laEmailConfirm.Location = new System.Drawing.Point(3, 208);
			this.laEmailConfirm.Name = "laEmailConfirm";
			this.laEmailConfirm.Size = new System.Drawing.Size(93, 16);
			this.laEmailConfirm.TabIndex = 14;
			this.laEmailConfirm.Text = "Confirm Email:";
			// 
			// xtraTabPageGroups
			// 
			this.xtraTabPageGroups.Controls.Add(this.pnAssignedGroups);
			this.xtraTabPageGroups.Controls.Add(this.gridControlGroups);
			this.xtraTabPageGroups.Name = "xtraTabPageGroups";
			this.xtraTabPageGroups.Size = new System.Drawing.Size(369, 537);
			this.xtraTabPageGroups.Text = "Groups";
			// 
			// pnAssignedGroups
			// 
			this.pnAssignedGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.pnAssignedGroups.Controls.Add(this.buttonXGroupsClearAll);
			this.pnAssignedGroups.Controls.Add(this.buttonXGroupsSelectAll);
			this.pnAssignedGroups.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnAssignedGroups.Location = new System.Drawing.Point(0, 0);
			this.pnAssignedGroups.Name = "pnAssignedGroups";
			this.pnAssignedGroups.Size = new System.Drawing.Size(369, 46);
			this.pnAssignedGroups.TabIndex = 2;
			// 
			// buttonXGroupsClearAll
			// 
			this.buttonXGroupsClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroupsClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXGroupsClearAll.CausesValidation = false;
			this.buttonXGroupsClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroupsClearAll.Location = new System.Drawing.Point(250, 7);
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
			this.buttonXGroupsSelectAll.Location = new System.Drawing.Point(29, 7);
			this.buttonXGroupsSelectAll.Name = "buttonXGroupsSelectAll";
			this.buttonXGroupsSelectAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXGroupsSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroupsSelectAll.TabIndex = 13;
			this.buttonXGroupsSelectAll.Text = "Select All";
			this.buttonXGroupsSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXGroupsSelectAll.Click += new System.EventHandler(this.buttonXGroupsSelectAll_Click);
			// 
			// gridControlGroups
			// 
			this.gridControlGroups.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.gridControlGroups.Location = new System.Drawing.Point(0, 40);
			this.gridControlGroups.MainView = this.gridViewGroups;
			this.gridControlGroups.Name = "gridControlGroups";
			this.gridControlGroups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditGroup});
			this.gridControlGroups.Size = new System.Drawing.Size(369, 497);
			this.gridControlGroups.TabIndex = 1;
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
			this.repositoryItemCheckEditGroup.Name = "repositoryItemCheckEditGroup";
			// 
			// gridColumnGroupName
			// 
			this.gridColumnGroupName.Caption = "Name";
			this.gridColumnGroupName.FieldName = "name";
			this.gridColumnGroupName.Name = "gridColumnGroupName";
			this.gridColumnGroupName.OptionsColumn.AllowEdit = false;
			this.gridColumnGroupName.OptionsColumn.ReadOnly = true;
			this.gridColumnGroupName.Visible = true;
			this.gridColumnGroupName.VisibleIndex = 1;
			this.gridColumnGroupName.Width = 355;
			// 
			// xtraTabPageLibraries
			// 
			this.xtraTabPageLibraries.Controls.Add(this.gridControlLibraries);
			this.xtraTabPageLibraries.Controls.Add(this.pnAssignedLibraries);
			this.xtraTabPageLibraries.Name = "xtraTabPageLibraries";
			this.xtraTabPageLibraries.Size = new System.Drawing.Size(369, 537);
			this.xtraTabPageLibraries.Text = "Assigned Libraries";
			// 
			// pnAssignedLibraries
			// 
			this.pnAssignedLibraries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.pnAssignedLibraries.Controls.Add(this.buttonXLibrariesClearAll);
			this.pnAssignedLibraries.Controls.Add(this.buttonXLibrariesSelectAll);
			this.pnAssignedLibraries.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnAssignedLibraries.Location = new System.Drawing.Point(0, 0);
			this.pnAssignedLibraries.Name = "pnAssignedLibraries";
			this.pnAssignedLibraries.Size = new System.Drawing.Size(369, 46);
			this.pnAssignedLibraries.TabIndex = 1;
			// 
			// buttonXLibrariesClearAll
			// 
			this.buttonXLibrariesClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLibrariesClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLibrariesClearAll.CausesValidation = false;
			this.buttonXLibrariesClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLibrariesClearAll.Location = new System.Drawing.Point(250, 7);
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
			this.buttonXLibrariesSelectAll.Location = new System.Drawing.Point(29, 7);
			this.buttonXLibrariesSelectAll.Name = "buttonXLibrariesSelectAll";
			this.buttonXLibrariesSelectAll.Size = new System.Drawing.Size(90, 33);
			this.buttonXLibrariesSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLibrariesSelectAll.TabIndex = 13;
			this.buttonXLibrariesSelectAll.Text = "Select All";
			this.buttonXLibrariesSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXLibrariesSelectAll.Click += new System.EventHandler(this.buttonXLibrariesSelectAll_Click);
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// FormEditUser
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.ClientSize = new System.Drawing.Size(381, 617);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditUser";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit User";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditUser_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditLibrary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLogin.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditFirstName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLastName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmail.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailConfirm.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPhone.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageUser.ResumeLayout(false);
			this.xtraTabPageUser.PerformLayout();
			this.xtraTabPageGroups.ResumeLayout(false);
			this.pnAssignedGroups.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditGroup)).EndInit();
			this.xtraTabPageLibraries.ResumeLayout(false);
			this.pnAssignedLibraries.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Label laLogin;
        private System.Windows.Forms.Label laFirstName;
        private System.Windows.Forms.Label laLastName;
        private System.Windows.Forms.Label laEmail;
        private System.Windows.Forms.Label laPassword;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        public DevExpress.XtraEditors.TextEdit textEditLogin;
        public DevExpress.XtraEditors.TextEdit textEditFirstName;
        public DevExpress.XtraEditors.TextEdit textEditLastName;
		public DevExpress.XtraEditors.TextEdit textEditEmail;
        public DevExpress.XtraEditors.CheckEdit checkEditPassword;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
		private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
		public DevExpress.XtraEditors.ButtonEdit buttonEditPassword;
		private ValidatableTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageUser;
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
		private DevExpress.XtraTab.XtraTabPage xtraTabPageGroups;
		private DevExpress.XtraGrid.GridControl gridControlGroups;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewGroups;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditGroup;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupName;
		private System.Windows.Forms.Panel pnAssignedGroups;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsSelectAll;
		public DevExpress.XtraEditors.TextEdit textEditEmailConfirm;
		private System.Windows.Forms.Label laEmailConfirm;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private System.Windows.Forms.Label laPhone;
		public DevExpress.XtraEditors.TextEdit textEditPhone;
    }
}