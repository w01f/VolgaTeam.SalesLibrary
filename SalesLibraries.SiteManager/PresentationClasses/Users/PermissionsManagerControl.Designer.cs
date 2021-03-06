﻿namespace SalesLibraries.SiteManager.PresentationClasses.Users
{
	sealed partial class PermissionsManagerControl
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControlUsers = new DevExpress.XtraGrid.GridControl();
			this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnUsersFullName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersEmail = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditUsersActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnUsersPhone = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageUsers = new DevExpress.XtraTab.XtraTabPage();
			this.splitContainerControlUsers = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnUsersFilterContent = new System.Windows.Forms.Panel();
			this.checkedListBoxControlUserFilterGroups = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.pnUsersFilterButtons = new System.Windows.Forms.Panel();
			this.buttonXUserFilterGroupsNone = new DevComponents.DotNetBar.ButtonX();
			this.checkEditEnableUserFilter = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXUserFilterGroupsAll = new DevComponents.DotNetBar.ButtonX();
			this.labelControlUserFilterGroupsTitle = new DevExpress.XtraEditors.LabelControl();
			this.xtraTabPageGroups = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlGroups = new DevExpress.XtraGrid.GridControl();
			this.gridViewGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnGroupActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditGroupActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.xtraTabPageLibraries = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlPages = new DevExpress.XtraGrid.GridControl();
			this.gridViewPages = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnPageName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPageActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditPageActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnLibraryName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnLibraraies = new System.Windows.Forms.Panel();
			this.buttonXCollapseLibraries = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExpandLibraries = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditUsersActions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageUsers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlUsers)).BeginInit();
			this.splitContainerControlUsers.SuspendLayout();
			this.pnUsersFilterContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlUserFilterGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnUsersFilterButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableUserFilter.Properties)).BeginInit();
			this.xtraTabPageGroups.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditGroupActions)).BeginInit();
			this.xtraTabPageLibraries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPageActions)).BeginInit();
			this.pnLibraraies.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridControlUsers
			// 
			this.gridControlUsers.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlUsers.Location = new System.Drawing.Point(0, 0);
			this.gridControlUsers.MainView = this.gridViewUsers;
			this.gridControlUsers.Name = "gridControlUsers";
			this.gridControlUsers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditUsersActions});
			this.gridControlUsers.Size = new System.Drawing.Size(637, 452);
			this.gridControlUsers.TabIndex = 2;
			this.gridControlUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers});
			// 
			// gridViewUsers
			// 
			this.gridViewUsers.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewUsers.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewUsers.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewUsers.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewUsers.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewUsers.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.OddRow.Options.UseFont = true;
			this.gridViewUsers.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.Preview.Options.UseFont = true;
			this.gridViewUsers.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.Row.Options.UseFont = true;
			this.gridViewUsers.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnUsersFullName,
            this.gridColumnUsersEmail,
            this.gridColumnUsersLogin,
            this.gridColumnUsersActions,
            this.gridColumnUsersPhone,
            this.gridColumnUsersCreateDate});
			this.gridViewUsers.GridControl = this.gridControlUsers;
			this.gridViewUsers.Name = "gridViewUsers";
			this.gridViewUsers.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewUsers.OptionsCustomization.AllowFilter = false;
			this.gridViewUsers.OptionsCustomization.AllowGroup = false;
			this.gridViewUsers.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewUsers.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewUsers.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewUsers.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewUsers.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewUsers.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewUsers.OptionsView.ShowDetailButtons = false;
			this.gridViewUsers.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewUsers.OptionsView.ShowGroupPanel = false;
			this.gridViewUsers.OptionsView.ShowIndicator = false;
			this.gridViewUsers.OptionsView.ShowPreview = true;
			this.gridViewUsers.PreviewFieldName = "AssignedObjects";
			this.gridViewUsers.PreviewIndent = 5;
			this.gridViewUsers.RowHeight = 35;
			this.gridViewUsers.RowSeparatorHeight = 5;
			this.gridViewUsers.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.OnGridUsersRowCellStyle);
			this.gridViewUsers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
			// 
			// gridColumnUsersFullName
			// 
			this.gridColumnUsersFullName.Caption = "User";
			this.gridColumnUsersFullName.FieldName = "FullName";
			this.gridColumnUsersFullName.Name = "gridColumnUsersFullName";
			this.gridColumnUsersFullName.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersFullName.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersFullName.Visible = true;
			this.gridColumnUsersFullName.VisibleIndex = 0;
			this.gridColumnUsersFullName.Width = 138;
			// 
			// gridColumnUsersEmail
			// 
			this.gridColumnUsersEmail.Caption = "Email";
			this.gridColumnUsersEmail.FieldName = "email";
			this.gridColumnUsersEmail.Name = "gridColumnUsersEmail";
			this.gridColumnUsersEmail.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersEmail.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersEmail.Visible = true;
			this.gridColumnUsersEmail.VisibleIndex = 1;
			this.gridColumnUsersEmail.Width = 105;
			// 
			// gridColumnUsersLogin
			// 
			this.gridColumnUsersLogin.Caption = "Login";
			this.gridColumnUsersLogin.FieldName = "login";
			this.gridColumnUsersLogin.Name = "gridColumnUsersLogin";
			this.gridColumnUsersLogin.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersLogin.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersLogin.Visible = true;
			this.gridColumnUsersLogin.VisibleIndex = 3;
			this.gridColumnUsersLogin.Width = 106;
			// 
			// gridColumnUsersActions
			// 
			this.gridColumnUsersActions.ColumnEdit = this.repositoryItemButtonEditUsersActions;
			this.gridColumnUsersActions.Name = "gridColumnUsersActions";
			this.gridColumnUsersActions.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumnUsersActions.OptionsColumn.FixedWidth = true;
			this.gridColumnUsersActions.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumnUsersActions.OptionsColumn.ShowCaption = false;
			this.gridColumnUsersActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnUsersActions.Visible = true;
			this.gridColumnUsersActions.VisibleIndex = 5;
			this.gridColumnUsersActions.Width = 80;
			// 
			// repositoryItemButtonEditUsersActions
			// 
			this.repositoryItemButtonEditUsersActions.AutoHeight = false;
			this.repositoryItemButtonEditUsersActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.EditButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject7, "", null, null, true)});
			this.repositoryItemButtonEditUsersActions.Name = "repositoryItemButtonEditUsersActions";
			this.repositoryItemButtonEditUsersActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditUsersActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditUsersActions_ButtonClick);
			// 
			// gridColumnUsersPhone
			// 
			this.gridColumnUsersPhone.Caption = "Phone";
			this.gridColumnUsersPhone.FieldName = "phone";
			this.gridColumnUsersPhone.Name = "gridColumnUsersPhone";
			this.gridColumnUsersPhone.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersPhone.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersPhone.Visible = true;
			this.gridColumnUsersPhone.VisibleIndex = 2;
			this.gridColumnUsersPhone.Width = 131;
			// 
			// gridColumnUsersCreateDate
			// 
			this.gridColumnUsersCreateDate.Caption = "Created";
			this.gridColumnUsersCreateDate.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.gridColumnUsersCreateDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumnUsersCreateDate.FieldName = "CreateDate";
			this.gridColumnUsersCreateDate.Name = "gridColumnUsersCreateDate";
			this.gridColumnUsersCreateDate.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersCreateDate.OptionsColumn.FixedWidth = true;
			this.gridColumnUsersCreateDate.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersCreateDate.Visible = true;
			this.gridColumnUsersCreateDate.VisibleIndex = 4;
			this.gridColumnUsersCreateDate.Width = 90;
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
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageUsers;
			this.xtraTabControl.Size = new System.Drawing.Size(898, 483);
			this.xtraTabControl.TabIndex = 3;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageUsers,
            this.xtraTabPageGroups,
            this.xtraTabPageLibraries});
			this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl_SelectedPageChanged);
			// 
			// xtraTabPageUsers
			// 
			this.xtraTabPageUsers.Controls.Add(this.splitContainerControlUsers);
			this.xtraTabPageUsers.Name = "xtraTabPageUsers";
			this.xtraTabPageUsers.Size = new System.Drawing.Size(892, 452);
			this.xtraTabPageUsers.Text = "Users";
			// 
			// splitContainerControlUsers
			// 
			this.splitContainerControlUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlUsers.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlUsers.Name = "splitContainerControlUsers";
			this.splitContainerControlUsers.Panel1.Controls.Add(this.pnUsersFilterContent);
			this.splitContainerControlUsers.Panel1.Controls.Add(this.pnUsersFilterButtons);
			this.splitContainerControlUsers.Panel1.MinSize = 250;
			this.splitContainerControlUsers.Panel1.Text = "Panel1";
			this.splitContainerControlUsers.Panel2.Controls.Add(this.gridControlUsers);
			this.splitContainerControlUsers.Panel2.Text = "Panel2";
			this.splitContainerControlUsers.Size = new System.Drawing.Size(892, 452);
			this.splitContainerControlUsers.SplitterPosition = 250;
			this.splitContainerControlUsers.TabIndex = 3;
			this.splitContainerControlUsers.Text = "splitContainerControl1";
			// 
			// pnUsersFilterContent
			// 
			this.pnUsersFilterContent.Controls.Add(this.checkedListBoxControlUserFilterGroups);
			this.pnUsersFilterContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnUsersFilterContent.Location = new System.Drawing.Point(0, 101);
			this.pnUsersFilterContent.Name = "pnUsersFilterContent";
			this.pnUsersFilterContent.Padding = new System.Windows.Forms.Padding(5);
			this.pnUsersFilterContent.Size = new System.Drawing.Size(250, 351);
			this.pnUsersFilterContent.TabIndex = 26;
			// 
			// checkedListBoxControlUserFilterGroups
			// 
			this.checkedListBoxControlUserFilterGroups.CheckOnClick = true;
			this.checkedListBoxControlUserFilterGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxControlUserFilterGroups.Enabled = false;
			this.checkedListBoxControlUserFilterGroups.ItemHeight = 35;
			this.checkedListBoxControlUserFilterGroups.Location = new System.Drawing.Point(5, 5);
			this.checkedListBoxControlUserFilterGroups.Name = "checkedListBoxControlUserFilterGroups";
			this.checkedListBoxControlUserFilterGroups.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControlUserFilterGroups.Size = new System.Drawing.Size(240, 341);
			this.checkedListBoxControlUserFilterGroups.StyleController = this.styleController;
			this.checkedListBoxControlUserFilterGroups.TabIndex = 21;
			this.checkedListBoxControlUserFilterGroups.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlUserFilterGroups_ItemCheck);
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
			// pnUsersFilterButtons
			// 
			this.pnUsersFilterButtons.Controls.Add(this.buttonXUserFilterGroupsNone);
			this.pnUsersFilterButtons.Controls.Add(this.checkEditEnableUserFilter);
			this.pnUsersFilterButtons.Controls.Add(this.buttonXUserFilterGroupsAll);
			this.pnUsersFilterButtons.Controls.Add(this.labelControlUserFilterGroupsTitle);
			this.pnUsersFilterButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnUsersFilterButtons.Location = new System.Drawing.Point(0, 0);
			this.pnUsersFilterButtons.Name = "pnUsersFilterButtons";
			this.pnUsersFilterButtons.Size = new System.Drawing.Size(250, 101);
			this.pnUsersFilterButtons.TabIndex = 25;
			// 
			// buttonXUserFilterGroupsNone
			// 
			this.buttonXUserFilterGroupsNone.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUserFilterGroupsNone.CausesValidation = false;
			this.buttonXUserFilterGroupsNone.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUserFilterGroupsNone.Enabled = false;
			this.buttonXUserFilterGroupsNone.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUserFilterGroupsNone.Location = new System.Drawing.Point(137, 65);
			this.buttonXUserFilterGroupsNone.Name = "buttonXUserFilterGroupsNone";
			this.buttonXUserFilterGroupsNone.Size = new System.Drawing.Size(103, 23);
			this.buttonXUserFilterGroupsNone.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
			this.buttonXUserFilterGroupsNone.TabIndex = 24;
			this.buttonXUserFilterGroupsNone.Text = "Clear All";
			this.buttonXUserFilterGroupsNone.TextColor = System.Drawing.Color.Black;
			this.buttonXUserFilterGroupsNone.Click += new System.EventHandler(this.buttonXUserFilterGroupsNone_Click);
			// 
			// checkEditEnableUserFilter
			// 
			this.checkEditEnableUserFilter.Location = new System.Drawing.Point(6, 15);
			this.checkEditEnableUserFilter.Name = "checkEditEnableUserFilter";
			this.checkEditEnableUserFilter.Properties.Caption = "Enable Filter";
			this.checkEditEnableUserFilter.Size = new System.Drawing.Size(236, 20);
			this.checkEditEnableUserFilter.StyleController = this.styleController;
			this.checkEditEnableUserFilter.TabIndex = 20;
			this.checkEditEnableUserFilter.CheckedChanged += new System.EventHandler(this.checkEditEnableUserFilter_CheckedChanged);
			// 
			// buttonXUserFilterGroupsAll
			// 
			this.buttonXUserFilterGroupsAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUserFilterGroupsAll.CausesValidation = false;
			this.buttonXUserFilterGroupsAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUserFilterGroupsAll.Enabled = false;
			this.buttonXUserFilterGroupsAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUserFilterGroupsAll.Location = new System.Drawing.Point(6, 65);
			this.buttonXUserFilterGroupsAll.Name = "buttonXUserFilterGroupsAll";
			this.buttonXUserFilterGroupsAll.Size = new System.Drawing.Size(103, 23);
			this.buttonXUserFilterGroupsAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
			this.buttonXUserFilterGroupsAll.TabIndex = 23;
			this.buttonXUserFilterGroupsAll.Text = "Select All";
			this.buttonXUserFilterGroupsAll.TextColor = System.Drawing.Color.Black;
			this.buttonXUserFilterGroupsAll.Click += new System.EventHandler(this.buttonXUserFilterGroupsAll_Click);
			// 
			// labelControlUserFilterGroupsTitle
			// 
			this.labelControlUserFilterGroupsTitle.Location = new System.Drawing.Point(6, 41);
			this.labelControlUserFilterGroupsTitle.Name = "labelControlUserFilterGroupsTitle";
			this.labelControlUserFilterGroupsTitle.Size = new System.Drawing.Size(46, 16);
			this.labelControlUserFilterGroupsTitle.StyleController = this.styleController;
			this.labelControlUserFilterGroupsTitle.TabIndex = 22;
			this.labelControlUserFilterGroupsTitle.Text = "Groups:";
			// 
			// xtraTabPageGroups
			// 
			this.xtraTabPageGroups.Controls.Add(this.gridControlGroups);
			this.xtraTabPageGroups.Name = "xtraTabPageGroups";
			this.xtraTabPageGroups.Size = new System.Drawing.Size(892, 452);
			this.xtraTabPageGroups.Text = "Groups";
			// 
			// gridControlGroups
			// 
			this.gridControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlGroups.Location = new System.Drawing.Point(0, 0);
			this.gridControlGroups.MainView = this.gridViewGroups;
			this.gridControlGroups.Name = "gridControlGroups";
			this.gridControlGroups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditGroupActions});
			this.gridControlGroups.Size = new System.Drawing.Size(892, 452);
			this.gridControlGroups.TabIndex = 3;
			this.gridControlGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGroups});
			// 
			// gridViewGroups
			// 
			this.gridViewGroups.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewGroups.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewGroups.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewGroups.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewGroups.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewGroups.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewGroups.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.OddRow.Options.UseFont = true;
			this.gridViewGroups.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.Preview.Options.UseFont = true;
			this.gridViewGroups.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.Row.Options.UseFont = true;
			this.gridViewGroups.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnGroupName,
            this.gridColumnGroupActions});
			this.gridViewGroups.GridControl = this.gridControlGroups;
			this.gridViewGroups.Name = "gridViewGroups";
			this.gridViewGroups.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewGroups.OptionsCustomization.AllowFilter = false;
			this.gridViewGroups.OptionsCustomization.AllowGroup = false;
			this.gridViewGroups.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewGroups.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewGroups.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewGroups.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewGroups.OptionsView.ShowDetailButtons = false;
			this.gridViewGroups.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewGroups.OptionsView.ShowGroupPanel = false;
			this.gridViewGroups.OptionsView.ShowIndicator = false;
			this.gridViewGroups.OptionsView.ShowPreview = true;
			this.gridViewGroups.PreviewFieldName = "AssignedObjects";
			this.gridViewGroups.PreviewIndent = 5;
			this.gridViewGroups.RowHeight = 35;
			this.gridViewGroups.RowSeparatorHeight = 5;
			this.gridViewGroups.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
			// 
			// gridColumnGroupName
			// 
			this.gridColumnGroupName.Caption = "Group Name";
			this.gridColumnGroupName.FieldName = "name";
			this.gridColumnGroupName.Name = "gridColumnGroupName";
			this.gridColumnGroupName.OptionsColumn.AllowEdit = false;
			this.gridColumnGroupName.OptionsColumn.ReadOnly = true;
			this.gridColumnGroupName.Visible = true;
			this.gridColumnGroupName.VisibleIndex = 0;
			this.gridColumnGroupName.Width = 271;
			// 
			// gridColumnGroupActions
			// 
			this.gridColumnGroupActions.ColumnEdit = this.repositoryItemButtonEditGroupActions;
			this.gridColumnGroupActions.Name = "gridColumnGroupActions";
			this.gridColumnGroupActions.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumnGroupActions.OptionsColumn.FixedWidth = true;
			this.gridColumnGroupActions.OptionsColumn.ShowCaption = false;
			this.gridColumnGroupActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnGroupActions.Visible = true;
			this.gridColumnGroupActions.VisibleIndex = 1;
			this.gridColumnGroupActions.Width = 80;
			// 
			// repositoryItemButtonEditGroupActions
			// 
			this.repositoryItemButtonEditGroupActions.AutoHeight = false;
			this.repositoryItemButtonEditGroupActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.EditButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject8, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, "", null, null, true)});
			this.repositoryItemButtonEditGroupActions.Name = "repositoryItemButtonEditGroupActions";
			this.repositoryItemButtonEditGroupActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditGroupActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditGroupActions_ButtonClick);
			// 
			// xtraTabPageLibraries
			// 
			this.xtraTabPageLibraries.Controls.Add(this.gridControlPages);
			this.xtraTabPageLibraries.Controls.Add(this.pnLibraraies);
			this.xtraTabPageLibraries.Name = "xtraTabPageLibraries";
			this.xtraTabPageLibraries.Size = new System.Drawing.Size(892, 452);
			this.xtraTabPageLibraries.Text = "Libraries";
			// 
			// gridControlPages
			// 
			this.gridControlPages.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlPages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlPages.Location = new System.Drawing.Point(0, 47);
			this.gridControlPages.MainView = this.gridViewPages;
			this.gridControlPages.Name = "gridControlPages";
			this.gridControlPages.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditPageActions});
			this.gridControlPages.Size = new System.Drawing.Size(892, 405);
			this.gridControlPages.TabIndex = 4;
			this.gridControlPages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPages});
			// 
			// gridViewPages
			// 
			this.gridViewPages.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewPages.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewPages.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewPages.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewPages.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewPages.Appearance.GroupRow.Options.UseFont = true;
			this.gridViewPages.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewPages.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewPages.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.OddRow.Options.UseFont = true;
			this.gridViewPages.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.Preview.Options.UseFont = true;
			this.gridViewPages.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.Row.Options.UseFont = true;
			this.gridViewPages.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewPages.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPageName,
            this.gridColumnPageActions,
            this.gridColumnLibraryName});
			this.gridViewPages.GridControl = this.gridControlPages;
			this.gridViewPages.GroupCount = 1;
			this.gridViewPages.Name = "gridViewPages";
			this.gridViewPages.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewPages.OptionsCustomization.AllowFilter = false;
			this.gridViewPages.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewPages.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewPages.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewPages.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewPages.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewPages.OptionsView.ShowDetailButtons = false;
			this.gridViewPages.OptionsView.ShowGroupPanel = false;
			this.gridViewPages.OptionsView.ShowIndicator = false;
			this.gridViewPages.RowHeight = 35;
			this.gridViewPages.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnLibraryName, DevExpress.Data.ColumnSortOrder.Ascending)});
			this.gridViewPages.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
			// 
			// gridColumnPageName
			// 
			this.gridColumnPageName.Caption = "Page";
			this.gridColumnPageName.FieldName = "name";
			this.gridColumnPageName.Name = "gridColumnPageName";
			this.gridColumnPageName.OptionsColumn.AllowEdit = false;
			this.gridColumnPageName.OptionsColumn.ReadOnly = true;
			this.gridColumnPageName.Visible = true;
			this.gridColumnPageName.VisibleIndex = 0;
			this.gridColumnPageName.Width = 692;
			// 
			// gridColumnPageActions
			// 
			this.gridColumnPageActions.ColumnEdit = this.repositoryItemButtonEditPageActions;
			this.gridColumnPageActions.Name = "gridColumnPageActions";
			this.gridColumnPageActions.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumnPageActions.OptionsColumn.FixedWidth = true;
			this.gridColumnPageActions.OptionsColumn.ShowCaption = false;
			this.gridColumnPageActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnPageActions.Visible = true;
			this.gridColumnPageActions.VisibleIndex = 1;
			this.gridColumnPageActions.Width = 50;
			// 
			// repositoryItemButtonEditPageActions
			// 
			this.repositoryItemButtonEditPageActions.AutoHeight = false;
			this.repositoryItemButtonEditPageActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.EditButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject10, "", null, null, true)});
			this.repositoryItemButtonEditPageActions.Name = "repositoryItemButtonEditPageActions";
			this.repositoryItemButtonEditPageActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditPageActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditPageActions_ButtonClick);
			// 
			// gridColumnLibraryName
			// 
			this.gridColumnLibraryName.Caption = "Library";
			this.gridColumnLibraryName.FieldName = "libraryName";
			this.gridColumnLibraryName.Name = "gridColumnLibraryName";
			this.gridColumnLibraryName.OptionsColumn.AllowEdit = false;
			this.gridColumnLibraryName.OptionsColumn.ReadOnly = true;
			this.gridColumnLibraryName.Visible = true;
			this.gridColumnLibraryName.VisibleIndex = 0;
			this.gridColumnLibraryName.Width = 120;
			// 
			// pnLibraraies
			// 
			this.pnLibraraies.Controls.Add(this.buttonXCollapseLibraries);
			this.pnLibraraies.Controls.Add(this.buttonXExpandLibraries);
			this.pnLibraraies.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnLibraraies.Location = new System.Drawing.Point(0, 0);
			this.pnLibraraies.Name = "pnLibraraies";
			this.pnLibraraies.Size = new System.Drawing.Size(892, 47);
			this.pnLibraraies.TabIndex = 5;
			// 
			// buttonXCollapseLibraries
			// 
			this.buttonXCollapseLibraries.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCollapseLibraries.CausesValidation = false;
			this.buttonXCollapseLibraries.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCollapseLibraries.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXCollapseLibraries.Location = new System.Drawing.Point(191, 7);
			this.buttonXCollapseLibraries.Name = "buttonXCollapseLibraries";
			this.buttonXCollapseLibraries.Size = new System.Drawing.Size(159, 33);
			this.buttonXCollapseLibraries.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCollapseLibraries.TabIndex = 16;
			this.buttonXCollapseLibraries.Text = "Collapse Libraries";
			this.buttonXCollapseLibraries.TextColor = System.Drawing.Color.Black;
			this.buttonXCollapseLibraries.Click += new System.EventHandler(this.buttonXCollapseLibraries_Click);
			// 
			// buttonXExpandLibraries
			// 
			this.buttonXExpandLibraries.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExpandLibraries.CausesValidation = false;
			this.buttonXExpandLibraries.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExpandLibraries.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXExpandLibraries.Location = new System.Drawing.Point(3, 7);
			this.buttonXExpandLibraries.Name = "buttonXExpandLibraries";
			this.buttonXExpandLibraries.Size = new System.Drawing.Size(159, 33);
			this.buttonXExpandLibraries.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExpandLibraries.TabIndex = 15;
			this.buttonXExpandLibraries.Text = "Expand Libraries";
			this.buttonXExpandLibraries.TextColor = System.Drawing.Color.Black;
			this.buttonXExpandLibraries.Click += new System.EventHandler(this.buttonXExpandLibraries_Click);
			// 
			// PermissionsManagerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PermissionsManagerControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditUsersActions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageUsers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlUsers)).EndInit();
			this.splitContainerControlUsers.ResumeLayout(false);
			this.pnUsersFilterContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlUserFilterGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnUsersFilterButtons.ResumeLayout(false);
			this.pnUsersFilterButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableUserFilter.Properties)).EndInit();
			this.xtraTabPageGroups.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditGroupActions)).EndInit();
			this.xtraTabPageLibraries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPageActions)).EndInit();
			this.pnLibraraies.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlUsers;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersFullName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersEmail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersLogin;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersActions;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditUsersActions;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageUsers;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageGroups;
		private DevExpress.XtraGrid.GridControl gridControlGroups;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewGroups;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroupActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditGroupActions;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageLibraries;
		private DevExpress.XtraGrid.GridControl gridControlPages;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewPages;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPageName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPageActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditPageActions;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLibraryName;
		private System.Windows.Forms.Panel pnLibraraies;
		private DevComponents.DotNetBar.ButtonX buttonXCollapseLibraries;
		private DevComponents.DotNetBar.ButtonX buttonXExpandLibraries;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersPhone;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControlUsers;
		private DevComponents.DotNetBar.ButtonX buttonXUserFilterGroupsNone;
		private DevComponents.DotNetBar.ButtonX buttonXUserFilterGroupsAll;
		private DevExpress.XtraEditors.LabelControl labelControlUserFilterGroupsTitle;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlUserFilterGroups;
		private DevExpress.XtraEditors.CheckEdit checkEditEnableUserFilter;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersCreateDate;
		private System.Windows.Forms.Panel pnUsersFilterContent;
		private System.Windows.Forms.Panel pnUsersFilterButtons;
	}
}
