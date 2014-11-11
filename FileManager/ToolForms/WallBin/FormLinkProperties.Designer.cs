namespace FileManager.ToolForms.WallBin
{
    partial class FormLinkProperties
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLinkProperties));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
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
			this.edCustomNote = new System.Windows.Forms.TextBox();
			this.rbCustomNote = new System.Windows.Forms.RadioButton();
			this.rbNone = new System.Windows.Forms.RadioButton();
			this.rbAttention = new System.Windows.Forms.RadioButton();
			this.rbSell = new System.Windows.Forms.RadioButton();
			this.rbUpdated = new System.Windows.Forms.RadioButton();
			this.rbNew = new System.Windows.Forms.RadioButton();
			this.rbLinkBoldFormat = new System.Windows.Forms.RadioButton();
			this.rbLinkRegularFormat = new System.Windows.Forms.RadioButton();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageNotes = new DevExpress.XtraTab.XtraTabPage();
			this.ckDoNotGenerateText = new System.Windows.Forms.CheckBox();
			this.ckIsUrl365 = new System.Windows.Forms.CheckBox();
			this.groupControlTextFormat = new DevExpress.XtraEditors.GroupControl();
			this.colorEditLinkSpecialColor = new DevExpress.XtraEditors.ColorEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonEditLinkSpecialFont = new DevExpress.XtraEditors.ButtonEdit();
			this.rbLinkSpecialFormat = new System.Windows.Forms.RadioButton();
			this.groupControlNotes = new DevExpress.XtraEditors.GroupControl();
			this.ckForcePreview = new System.Windows.Forms.CheckBox();
			this.pnAdminTools = new System.Windows.Forms.Panel();
			this.laAdminTools = new System.Windows.Forms.Label();
			this.buttonXRefreshPreview = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenQV = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenWV = new DevComponents.DotNetBar.ButtonX();
			this.ckDoNotGeneratePreview = new System.Windows.Forms.CheckBox();
			this.xtraTabPageSearchTags = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabControlSearchTags = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageSearchTagsCategories = new DevExpress.XtraTab.XtraTabPage();
			this.splitContainerSearchTagsCategories = new DevExpress.XtraEditors.SplitContainerControl();
			this.xtraScrollableControlSearchTagsCategories = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pnSearchTagsCategoriesHeader = new System.Windows.Forms.Panel();
			this.labelControlSearchTagsCategoriesHeader = new DevExpress.XtraEditors.LabelControl();
			this.buttonXWipeTags = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageSearchTagsKeywords = new DevExpress.XtraTab.XtraTabPage();
			this.buttonXAddKeyWord = new DevComponents.DotNetBar.ButtonX();
			this.gridControlSearchTagsKeywords = new DevExpress.XtraGrid.GridControl();
			this.gridViewSearchTagsKeywords = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnSearchTagsKeywordsValue = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditKeyword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.xtraTabPageExpiredLinks = new DevExpress.XtraTab.XtraTabPage();
			this.gbExpiredLinks = new DevExpress.XtraEditors.GroupControl();
			this.checkBoxLabelLink = new System.Windows.Forms.CheckBox();
			this.timeEditExpirationTime = new DevExpress.XtraEditors.TimeEdit();
			this.checkBoxSendEmailWhenDelete = new System.Windows.Forms.CheckBox();
			this.laExpireddateActions = new System.Windows.Forms.Label();
			this.dateEditExpirationDate = new DevExpress.XtraEditors.DateEdit();
			this.laExpirationDateTitle = new System.Windows.Forms.Label();
			this.laAddDateValue = new System.Windows.Forms.Label();
			this.laAddDateTitle = new System.Windows.Forms.Label();
			this.checkBoxEnableExpiredLinks = new System.Windows.Forms.CheckBox();
			this.xtraTabPageLineBrealProperties = new DevExpress.XtraTab.XtraTabPage();
			this.memoEditNote = new DevExpress.XtraEditors.MemoEdit();
			this.laNote = new System.Windows.Forms.Label();
			this.laFont = new System.Windows.Forms.Label();
			this.buttonEditLineBreakFont = new DevExpress.XtraEditors.ButtonEdit();
			this.colorEditLineBreakFontColor = new DevExpress.XtraEditors.ColorEdit();
			this.laFontColor = new System.Windows.Forms.Label();
			this.xtraTabPageSecurity = new DevExpress.XtraTab.XtraTabPage();
			this.groupBoxSecurity = new DevExpress.XtraEditors.GroupControl();
			this.rbSecurityBlackList = new System.Windows.Forms.RadioButton();
			this.rbSecurityForbidden = new System.Windows.Forms.RadioButton();
			this.ckSecurityShareLink = new System.Windows.Forms.CheckBox();
			this.rbSecurityAllowed = new System.Windows.Forms.RadioButton();
			this.rbSecurityWhiteList = new System.Windows.Forms.RadioButton();
			this.rbSecurityDenied = new System.Windows.Forms.RadioButton();
			this.pnSecurityUserList = new System.Windows.Forms.Panel();
			this.pnSecurityUserListGrid = new System.Windows.Forms.Panel();
			this.buttonXSecurityUserListClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSecurityUserListSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.circularSecurityUserListProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.laSecurityUserListInfo = new DevExpress.XtraEditors.LabelControl();
			this.xtraTabPageWidgets = new DevExpress.XtraTab.XtraTabPage();
			this.groupBoxWidgets = new DevExpress.XtraEditors.GroupControl();
			this.xtraTabControlWidgets = new DevExpress.XtraTab.XtraTabControl();
			this.laWidgetFileName = new System.Windows.Forms.Label();
			this.pbSelectedWidget = new System.Windows.Forms.PictureBox();
			this.laAvailableWidgets = new System.Windows.Forms.Label();
			this.laSelectedWidget = new System.Windows.Forms.Label();
			this.checkBoxEnableWidget = new System.Windows.Forms.CheckBox();
			this.xtraTabPageBanner = new DevExpress.XtraTab.XtraTabPage();
			this.groupBoxBanners = new DevExpress.XtraEditors.GroupControl();
			this.xtraTabControlBanners = new DevExpress.XtraTab.XtraTabControl();
			this.pbSelectedBanner = new DevExpress.XtraEditors.PictureEdit();
			this.laBannerFileName = new System.Windows.Forms.Label();
			this.colorEditBannerTextColor = new DevExpress.XtraEditors.ColorEdit();
			this.buttonEditBannerTextFont = new DevExpress.XtraEditors.ButtonEdit();
			this.memoEditBannerText = new DevExpress.XtraEditors.MemoEdit();
			this.checkBoxBannerShowText = new System.Windows.Forms.CheckBox();
			this.rbBannerAligmentRight = new System.Windows.Forms.RadioButton();
			this.rbBannerAligmentCenter = new System.Windows.Forms.RadioButton();
			this.rbBannerAligmentLeft = new System.Windows.Forms.RadioButton();
			this.laBannerAligment = new System.Windows.Forms.Label();
			this.laAvailableBanners = new System.Windows.Forms.Label();
			this.laSelectedBanner = new System.Windows.Forms.Label();
			this.checkBoxEnableBanner = new System.Windows.Forms.CheckBox();
			this.persistentRepository = new DevExpress.XtraEditors.Repository.PersistentRepository(this.components);
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.dlgFont = new System.Windows.Forms.FontDialog();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.hyperLinkEditRequestNewCategories = new DevExpress.XtraEditors.HyperLinkEdit();
			this.laLinkHoverNote = new System.Windows.Forms.Label();
			this.textEditLinkHoverNote = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditSecurityUserList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSecurityUserList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageNotes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControlTextFormat)).BeginInit();
			this.groupControlTextFormat.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLinkSpecialColor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLinkSpecialFont.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlNotes)).BeginInit();
			this.groupControlNotes.SuspendLayout();
			this.pnAdminTools.SuspendLayout();
			this.xtraTabPageSearchTags.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSearchTags)).BeginInit();
			this.xtraTabControlSearchTags.SuspendLayout();
			this.xtraTabPageSearchTagsCategories.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSearchTagsCategories)).BeginInit();
			this.splitContainerSearchTagsCategories.SuspendLayout();
			this.pnSearchTagsCategoriesHeader.SuspendLayout();
			this.xtraTabPageSearchTagsKeywords.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSearchTagsKeywords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSearchTagsKeywords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditKeyword)).BeginInit();
			this.xtraTabPageExpiredLinks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gbExpiredLinks)).BeginInit();
			this.gbExpiredLinks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.timeEditExpirationTime.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties)).BeginInit();
			this.xtraTabPageLineBrealProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditNote.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLineBreakFont.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLineBreakFontColor.Properties)).BeginInit();
			this.xtraTabPageSecurity.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxSecurity)).BeginInit();
			this.groupBoxSecurity.SuspendLayout();
			this.pnSecurityUserList.SuspendLayout();
			this.pnSecurityUserListGrid.SuspendLayout();
			this.xtraTabPageWidgets.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxWidgets)).BeginInit();
			this.groupBoxWidgets.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedWidget)).BeginInit();
			this.xtraTabPageBanner.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxBanners)).BeginInit();
			this.groupBoxBanners.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlBanners)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedBanner.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditBannerTextColor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditBannerTextFont.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditBannerText.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditRequestNewCategories.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkHoverNote.Properties)).BeginInit();
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
			this.gridControlSecurityUserList.Location = new System.Drawing.Point(0, 41);
			this.gridControlSecurityUserList.MainView = this.gridViewSecurityGroups;
			this.gridControlSecurityUserList.Name = "gridControlSecurityUserList";
			this.gridControlSecurityUserList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditSecurityUserList});
			this.gridControlSecurityUserList.Size = new System.Drawing.Size(505, 77);
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
			// edCustomNote
			// 
			this.edCustomNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.edCustomNote.BackColor = System.Drawing.Color.White;
			this.edCustomNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.edCustomNote.ForeColor = System.Drawing.Color.Black;
			this.edCustomNote.Location = new System.Drawing.Point(22, 221);
			this.edCustomNote.Name = "edCustomNote";
			this.edCustomNote.Size = new System.Drawing.Size(553, 26);
			this.edCustomNote.TabIndex = 6;
			// 
			// rbCustomNote
			// 
			this.rbCustomNote.AutoSize = true;
			this.rbCustomNote.BackColor = System.Drawing.Color.White;
			this.rbCustomNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbCustomNote.ForeColor = System.Drawing.Color.Black;
			this.rbCustomNote.Location = new System.Drawing.Point(5, 192);
			this.rbCustomNote.Name = "rbCustomNote";
			this.rbCustomNote.Size = new System.Drawing.Size(127, 23);
			this.rbCustomNote.TabIndex = 5;
			this.rbCustomNote.TabStop = true;
			this.rbCustomNote.Text = "Custom Note";
			this.rbCustomNote.UseVisualStyleBackColor = false;
			this.rbCustomNote.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
			// 
			// rbNone
			// 
			this.rbNone.AutoSize = true;
			this.rbNone.BackColor = System.Drawing.Color.White;
			this.rbNone.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbNone.ForeColor = System.Drawing.Color.Black;
			this.rbNone.Location = new System.Drawing.Point(5, 10);
			this.rbNone.Name = "rbNone";
			this.rbNone.Size = new System.Drawing.Size(68, 23);
			this.rbNone.TabIndex = 4;
			this.rbNone.TabStop = true;
			this.rbNone.Text = "None";
			this.rbNone.UseVisualStyleBackColor = false;
			this.rbNone.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
			// 
			// rbAttention
			// 
			this.rbAttention.AutoSize = true;
			this.rbAttention.BackColor = System.Drawing.Color.White;
			this.rbAttention.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbAttention.ForeColor = System.Drawing.Color.Black;
			this.rbAttention.Location = new System.Drawing.Point(5, 155);
			this.rbAttention.Name = "rbAttention";
			this.rbAttention.Size = new System.Drawing.Size(127, 23);
			this.rbAttention.TabIndex = 3;
			this.rbAttention.TabStop = true;
			this.rbAttention.Text = "-ATTENTION!";
			this.rbAttention.UseVisualStyleBackColor = false;
			this.rbAttention.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
			// 
			// rbSell
			// 
			this.rbSell.AutoSize = true;
			this.rbSell.BackColor = System.Drawing.Color.White;
			this.rbSell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSell.ForeColor = System.Drawing.Color.Black;
			this.rbSell.Location = new System.Drawing.Point(5, 118);
			this.rbSell.Name = "rbSell";
			this.rbSell.Size = new System.Drawing.Size(119, 23);
			this.rbSell.TabIndex = 2;
			this.rbSell.TabStop = true;
			this.rbSell.Text = "-SELL THIS!";
			this.rbSell.UseVisualStyleBackColor = false;
			this.rbSell.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
			// 
			// rbUpdated
			// 
			this.rbUpdated.AutoSize = true;
			this.rbUpdated.BackColor = System.Drawing.Color.White;
			this.rbUpdated.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbUpdated.ForeColor = System.Drawing.Color.Black;
			this.rbUpdated.Location = new System.Drawing.Point(5, 81);
			this.rbUpdated.Name = "rbUpdated";
			this.rbUpdated.Size = new System.Drawing.Size(114, 23);
			this.rbUpdated.TabIndex = 1;
			this.rbUpdated.TabStop = true;
			this.rbUpdated.Text = "-UPDATED!";
			this.rbUpdated.UseVisualStyleBackColor = false;
			this.rbUpdated.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
			// 
			// rbNew
			// 
			this.rbNew.AutoSize = true;
			this.rbNew.BackColor = System.Drawing.Color.White;
			this.rbNew.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbNew.ForeColor = System.Drawing.Color.Black;
			this.rbNew.Location = new System.Drawing.Point(5, 44);
			this.rbNew.Name = "rbNew";
			this.rbNew.Size = new System.Drawing.Size(74, 23);
			this.rbNew.TabIndex = 0;
			this.rbNew.TabStop = true;
			this.rbNew.Text = "-NEW!";
			this.rbNew.UseVisualStyleBackColor = false;
			this.rbNew.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
			// 
			// rbLinkBoldFormat
			// 
			this.rbLinkBoldFormat.AutoSize = true;
			this.rbLinkBoldFormat.BackColor = System.Drawing.Color.White;
			this.rbLinkBoldFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbLinkBoldFormat.ForeColor = System.Drawing.Color.Black;
			this.rbLinkBoldFormat.Location = new System.Drawing.Point(5, 63);
			this.rbLinkBoldFormat.Name = "rbLinkBoldFormat";
			this.rbLinkBoldFormat.Size = new System.Drawing.Size(62, 20);
			this.rbLinkBoldFormat.TabIndex = 1;
			this.rbLinkBoldFormat.Text = "BOLD";
			this.rbLinkBoldFormat.UseVisualStyleBackColor = false;
			// 
			// rbLinkRegularFormat
			// 
			this.rbLinkRegularFormat.AutoSize = true;
			this.rbLinkRegularFormat.BackColor = System.Drawing.Color.White;
			this.rbLinkRegularFormat.Checked = true;
			this.rbLinkRegularFormat.ForeColor = System.Drawing.Color.Black;
			this.rbLinkRegularFormat.Location = new System.Drawing.Point(5, 27);
			this.rbLinkRegularFormat.Name = "rbLinkRegularFormat";
			this.rbLinkRegularFormat.Size = new System.Drawing.Size(67, 20);
			this.rbLinkRegularFormat.TabIndex = 0;
			this.rbLinkRegularFormat.TabStop = true;
			this.rbLinkRegularFormat.Text = "Normal";
			this.rbLinkRegularFormat.UseVisualStyleBackColor = false;
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControl.Appearance.Options.UseBackColor = true;
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.Appearance.Options.UseForeColor = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageNotes;
			this.xtraTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
			this.xtraTabControl.Size = new System.Drawing.Size(607, 551);
			this.xtraTabControl.TabIndex = 4;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageNotes,
            this.xtraTabPageSearchTags,
            this.xtraTabPageExpiredLinks,
            this.xtraTabPageLineBrealProperties,
            this.xtraTabPageSecurity,
            this.xtraTabPageWidgets,
            this.xtraTabPageBanner});
			this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl_SelectedPageChanged);
			// 
			// xtraTabPageNotes
			// 
			this.xtraTabPageNotes.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageNotes.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageNotes.Controls.Add(this.textEditLinkHoverNote);
			this.xtraTabPageNotes.Controls.Add(this.laLinkHoverNote);
			this.xtraTabPageNotes.Controls.Add(this.ckDoNotGenerateText);
			this.xtraTabPageNotes.Controls.Add(this.ckIsUrl365);
			this.xtraTabPageNotes.Controls.Add(this.groupControlTextFormat);
			this.xtraTabPageNotes.Controls.Add(this.groupControlNotes);
			this.xtraTabPageNotes.Controls.Add(this.ckForcePreview);
			this.xtraTabPageNotes.Controls.Add(this.pnAdminTools);
			this.xtraTabPageNotes.Controls.Add(this.ckDoNotGeneratePreview);
			this.xtraTabPageNotes.Name = "xtraTabPageNotes";
			this.xtraTabPageNotes.Size = new System.Drawing.Size(601, 545);
			this.xtraTabPageNotes.Text = "Notes";
			// 
			// ckDoNotGenerateText
			// 
			this.ckDoNotGenerateText.AutoSize = true;
			this.ckDoNotGenerateText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGenerateText.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGenerateText.Location = new System.Drawing.Point(10, 441);
			this.ckDoNotGenerateText.Name = "ckDoNotGenerateText";
			this.ckDoNotGenerateText.Size = new System.Drawing.Size(452, 20);
			this.ckDoNotGenerateText.TabIndex = 18;
			this.ckDoNotGenerateText.Text = "Do NOT Create Full Data File (Always select this for Nielsen Books) ";
			this.ckDoNotGenerateText.UseVisualStyleBackColor = true;
			// 
			// ckIsUrl365
			// 
			this.ckIsUrl365.AutoSize = true;
			this.ckIsUrl365.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckIsUrl365.ForeColor = System.Drawing.Color.Black;
			this.ckIsUrl365.Location = new System.Drawing.Point(10, 415);
			this.ckIsUrl365.Name = "ckIsUrl365";
			this.ckIsUrl365.Size = new System.Drawing.Size(244, 20);
			this.ckIsUrl365.TabIndex = 17;
			this.ckIsUrl365.Text = "This URL is an Office 365 URL Link";
			this.ckIsUrl365.UseVisualStyleBackColor = true;
			// 
			// groupControlTextFormat
			// 
			this.groupControlTextFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlTextFormat.Appearance.BackColor = System.Drawing.Color.White;
			this.groupControlTextFormat.Appearance.ForeColor = System.Drawing.Color.Black;
			this.groupControlTextFormat.Appearance.Options.UseBackColor = true;
			this.groupControlTextFormat.Appearance.Options.UseForeColor = true;
			this.groupControlTextFormat.AppearanceCaption.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupControlTextFormat.AppearanceCaption.Options.UseFont = true;
			this.groupControlTextFormat.Controls.Add(this.colorEditLinkSpecialColor);
			this.groupControlTextFormat.Controls.Add(this.buttonEditLinkSpecialFont);
			this.groupControlTextFormat.Controls.Add(this.rbLinkSpecialFormat);
			this.groupControlTextFormat.Controls.Add(this.rbLinkBoldFormat);
			this.groupControlTextFormat.Controls.Add(this.rbLinkRegularFormat);
			this.groupControlTextFormat.Location = new System.Drawing.Point(10, 273);
			this.groupControlTextFormat.Name = "groupControlTextFormat";
			this.groupControlTextFormat.Size = new System.Drawing.Size(580, 93);
			this.groupControlTextFormat.TabIndex = 16;
			this.groupControlTextFormat.Text = "Line Text Format";
			// 
			// colorEditLinkSpecialColor
			// 
			this.colorEditLinkSpecialColor.EditValue = System.Drawing.Color.Empty;
			this.colorEditLinkSpecialColor.Enabled = false;
			this.colorEditLinkSpecialColor.Location = new System.Drawing.Point(278, 61);
			this.colorEditLinkSpecialColor.Name = "colorEditLinkSpecialColor";
			this.colorEditLinkSpecialColor.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.colorEditLinkSpecialColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditLinkSpecialColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditLinkSpecialColor.Properties.Appearance.Options.UseForeColor = true;
			this.colorEditLinkSpecialColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.colorEditLinkSpecialColor.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colorEditLinkSpecialColor.Size = new System.Drawing.Size(105, 22);
			this.colorEditLinkSpecialColor.StyleController = this.styleController;
			this.colorEditLinkSpecialColor.TabIndex = 31;
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
			// buttonEditLinkSpecialFont
			// 
			this.buttonEditLinkSpecialFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditLinkSpecialFont.Enabled = false;
			this.buttonEditLinkSpecialFont.Location = new System.Drawing.Point(278, 26);
			this.buttonEditLinkSpecialFont.Name = "buttonEditLinkSpecialFont";
			this.buttonEditLinkSpecialFont.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditLinkSpecialFont.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditLinkSpecialFont.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditLinkSpecialFont.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditLinkSpecialFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditLinkSpecialFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.buttonEditLinkSpecialFont.Size = new System.Drawing.Size(297, 22);
			this.buttonEditLinkSpecialFont.StyleController = this.styleController;
			this.buttonEditLinkSpecialFont.TabIndex = 30;
			this.buttonEditLinkSpecialFont.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FontEdit_ButtonClick);
			this.buttonEditLinkSpecialFont.Click += new System.EventHandler(this.FontEdit_Click);
			// 
			// rbLinkSpecialFormat
			// 
			this.rbLinkSpecialFormat.AutoSize = true;
			this.rbLinkSpecialFormat.BackColor = System.Drawing.Color.White;
			this.rbLinkSpecialFormat.ForeColor = System.Drawing.Color.Black;
			this.rbLinkSpecialFormat.Location = new System.Drawing.Point(158, 27);
			this.rbLinkSpecialFormat.Name = "rbLinkSpecialFormat";
			this.rbLinkSpecialFormat.Size = new System.Drawing.Size(114, 20);
			this.rbLinkSpecialFormat.TabIndex = 2;
			this.rbLinkSpecialFormat.Text = "Special Format";
			this.rbLinkSpecialFormat.UseVisualStyleBackColor = false;
			this.rbLinkSpecialFormat.CheckedChanged += new System.EventHandler(this.rbLinkSpecialFormat_CheckedChanged);
			// 
			// groupControlNotes
			// 
			this.groupControlNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlNotes.Appearance.BackColor = System.Drawing.Color.White;
			this.groupControlNotes.Appearance.ForeColor = System.Drawing.Color.Black;
			this.groupControlNotes.Appearance.Options.UseBackColor = true;
			this.groupControlNotes.Appearance.Options.UseForeColor = true;
			this.groupControlNotes.Controls.Add(this.edCustomNote);
			this.groupControlNotes.Controls.Add(this.rbNone);
			this.groupControlNotes.Controls.Add(this.rbCustomNote);
			this.groupControlNotes.Controls.Add(this.rbNew);
			this.groupControlNotes.Controls.Add(this.rbUpdated);
			this.groupControlNotes.Controls.Add(this.rbAttention);
			this.groupControlNotes.Controls.Add(this.rbSell);
			this.groupControlNotes.Location = new System.Drawing.Point(10, 11);
			this.groupControlNotes.Name = "groupControlNotes";
			this.groupControlNotes.ShowCaption = false;
			this.groupControlNotes.Size = new System.Drawing.Size(580, 256);
			this.groupControlNotes.TabIndex = 15;
			this.groupControlNotes.Text = "groupControl1";
			// 
			// ckForcePreview
			// 
			this.ckForcePreview.AutoSize = true;
			this.ckForcePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForcePreview.ForeColor = System.Drawing.Color.Black;
			this.ckForcePreview.Location = new System.Drawing.Point(10, 415);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(311, 20);
			this.ckForcePreview.TabIndex = 8;
			this.ckForcePreview.Text = "Immediately Play this Video in Cloud Library";
			this.ckForcePreview.UseVisualStyleBackColor = true;
			// 
			// pnAdminTools
			// 
			this.pnAdminTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnAdminTools.Controls.Add(this.laAdminTools);
			this.pnAdminTools.Controls.Add(this.buttonXRefreshPreview);
			this.pnAdminTools.Controls.Add(this.buttonXOpenQV);
			this.pnAdminTools.Controls.Add(this.buttonXOpenWV);
			this.pnAdminTools.ForeColor = System.Drawing.Color.Black;
			this.pnAdminTools.Location = new System.Drawing.Point(10, 492);
			this.pnAdminTools.Name = "pnAdminTools";
			this.pnAdminTools.Size = new System.Drawing.Size(580, 52);
			this.pnAdminTools.TabIndex = 14;
			// 
			// laAdminTools
			// 
			this.laAdminTools.AutoSize = true;
			this.laAdminTools.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdminTools.ForeColor = System.Drawing.Color.Black;
			this.laAdminTools.Location = new System.Drawing.Point(-1, 0);
			this.laAdminTools.Name = "laAdminTools";
			this.laAdminTools.Size = new System.Drawing.Size(83, 16);
			this.laAdminTools.TabIndex = 10;
			this.laAdminTools.Text = "Admin Tools:";
			// 
			// buttonXRefreshPreview
			// 
			this.buttonXRefreshPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshPreview.Location = new System.Drawing.Point(286, 19);
			this.buttonXRefreshPreview.Name = "buttonXRefreshPreview";
			this.buttonXRefreshPreview.Size = new System.Drawing.Size(118, 26);
			this.buttonXRefreshPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshPreview.TabIndex = 13;
			this.buttonXRefreshPreview.Text = "Refresh QV && WV";
			this.buttonXRefreshPreview.TextColor = System.Drawing.Color.Black;
			this.buttonXRefreshPreview.Click += new System.EventHandler(this.buttonXRefreshPreview_Click);
			// 
			// buttonXOpenQV
			// 
			this.buttonXOpenQV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenQV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenQV.Location = new System.Drawing.Point(0, 19);
			this.buttonXOpenQV.Name = "buttonXOpenQV";
			this.buttonXOpenQV.Size = new System.Drawing.Size(118, 26);
			this.buttonXOpenQV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenQV.TabIndex = 11;
			this.buttonXOpenQV.Text = "!QV Folder";
			this.buttonXOpenQV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenQV.Click += new System.EventHandler(this.buttonXOpenQV_Click);
			// 
			// buttonXOpenWV
			// 
			this.buttonXOpenWV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenWV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenWV.Location = new System.Drawing.Point(143, 19);
			this.buttonXOpenWV.Name = "buttonXOpenWV";
			this.buttonXOpenWV.Size = new System.Drawing.Size(118, 26);
			this.buttonXOpenWV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenWV.TabIndex = 12;
			this.buttonXOpenWV.Text = "!WV Folder";
			this.buttonXOpenWV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenWV.Click += new System.EventHandler(this.buttonXOpenWV_Click);
			// 
			// ckDoNotGeneratePreview
			// 
			this.ckDoNotGeneratePreview.AutoSize = true;
			this.ckDoNotGeneratePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGeneratePreview.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGeneratePreview.Location = new System.Drawing.Point(10, 415);
			this.ckDoNotGeneratePreview.Name = "ckDoNotGeneratePreview";
			this.ckDoNotGeneratePreview.Size = new System.Drawing.Size(579, 20);
			this.ckDoNotGeneratePreview.TabIndex = 5;
			this.ckDoNotGeneratePreview.Text = "Do NOT Generate PNG and JPEG preview images (Always select this for Nielsen Books" +
    ")";
			this.ckDoNotGeneratePreview.UseVisualStyleBackColor = true;
			// 
			// xtraTabPageSearchTags
			// 
			this.xtraTabPageSearchTags.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageSearchTags.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageSearchTags.Controls.Add(this.xtraTabControlSearchTags);
			this.xtraTabPageSearchTags.Name = "xtraTabPageSearchTags";
			this.xtraTabPageSearchTags.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.xtraTabPageSearchTags.Size = new System.Drawing.Size(601, 545);
			this.xtraTabPageSearchTags.Text = "Search Tags";
			// 
			// xtraTabControlSearchTags
			// 
			this.xtraTabControlSearchTags.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControlSearchTags.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSearchTags.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlSearchTags.Appearance.Options.UseBackColor = true;
			this.xtraTabControlSearchTags.Appearance.Options.UseFont = true;
			this.xtraTabControlSearchTags.Appearance.Options.UseForeColor = true;
			this.xtraTabControlSearchTags.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSearchTags.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlSearchTags.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSearchTags.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlSearchTags.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSearchTags.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlSearchTags.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSearchTags.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlSearchTags.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSearchTags.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlSearchTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlSearchTags.Location = new System.Drawing.Point(0, 10);
			this.xtraTabControlSearchTags.Name = "xtraTabControlSearchTags";
			this.xtraTabControlSearchTags.SelectedTabPage = this.xtraTabPageSearchTagsCategories;
			this.xtraTabControlSearchTags.Size = new System.Drawing.Size(601, 535);
			this.xtraTabControlSearchTags.TabIndex = 1;
			this.xtraTabControlSearchTags.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageSearchTagsCategories,
            this.xtraTabPageSearchTagsKeywords});
			// 
			// xtraTabPageSearchTagsCategories
			// 
			this.xtraTabPageSearchTagsCategories.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageSearchTagsCategories.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageSearchTagsCategories.Controls.Add(this.splitContainerSearchTagsCategories);
			this.xtraTabPageSearchTagsCategories.Controls.Add(this.pnSearchTagsCategoriesHeader);
			this.xtraTabPageSearchTagsCategories.Name = "xtraTabPageSearchTagsCategories";
			this.xtraTabPageSearchTagsCategories.Size = new System.Drawing.Size(595, 504);
			this.xtraTabPageSearchTagsCategories.Text = "Assign Categories";
			// 
			// splitContainerSearchTagsCategories
			// 
			this.splitContainerSearchTagsCategories.Appearance.ForeColor = System.Drawing.Color.Black;
			this.splitContainerSearchTagsCategories.Appearance.Options.UseForeColor = true;
			this.splitContainerSearchTagsCategories.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerSearchTagsCategories.Location = new System.Drawing.Point(0, 49);
			this.splitContainerSearchTagsCategories.Name = "splitContainerSearchTagsCategories";
			this.splitContainerSearchTagsCategories.Panel1.Appearance.BackColor = System.Drawing.Color.White;
			this.splitContainerSearchTagsCategories.Panel1.Appearance.ForeColor = System.Drawing.Color.Black;
			this.splitContainerSearchTagsCategories.Panel1.Appearance.Options.UseBackColor = true;
			this.splitContainerSearchTagsCategories.Panel1.Appearance.Options.UseForeColor = true;
			this.splitContainerSearchTagsCategories.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerSearchTagsCategories.Panel1.Controls.Add(this.xtraScrollableControlSearchTagsCategories);
			this.splitContainerSearchTagsCategories.Panel1.MinSize = 250;
			this.splitContainerSearchTagsCategories.Panel1.Text = "Panel1";
			this.splitContainerSearchTagsCategories.Panel2.Appearance.BackColor = System.Drawing.Color.White;
			this.splitContainerSearchTagsCategories.Panel2.Appearance.ForeColor = System.Drawing.Color.Black;
			this.splitContainerSearchTagsCategories.Panel2.Appearance.Options.UseBackColor = true;
			this.splitContainerSearchTagsCategories.Panel2.Appearance.Options.UseForeColor = true;
			this.splitContainerSearchTagsCategories.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerSearchTagsCategories.Panel2.Text = "Panel2";
			this.splitContainerSearchTagsCategories.Size = new System.Drawing.Size(595, 455);
			this.splitContainerSearchTagsCategories.SplitterPosition = 250;
			this.splitContainerSearchTagsCategories.TabIndex = 1;
			this.splitContainerSearchTagsCategories.Text = "splitContainerControl1";
			// 
			// xtraScrollableControlSearchTagsCategories
			// 
			this.xtraScrollableControlSearchTagsCategories.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraScrollableControlSearchTagsCategories.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraScrollableControlSearchTagsCategories.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlSearchTagsCategories.Appearance.Options.UseForeColor = true;
			this.xtraScrollableControlSearchTagsCategories.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlSearchTagsCategories.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControlSearchTagsCategories.Name = "xtraScrollableControlSearchTagsCategories";
			this.xtraScrollableControlSearchTagsCategories.Size = new System.Drawing.Size(246, 451);
			this.xtraScrollableControlSearchTagsCategories.TabIndex = 0;
			// 
			// pnSearchTagsCategoriesHeader
			// 
			this.pnSearchTagsCategoriesHeader.Controls.Add(this.labelControlSearchTagsCategoriesHeader);
			this.pnSearchTagsCategoriesHeader.Controls.Add(this.buttonXWipeTags);
			this.pnSearchTagsCategoriesHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnSearchTagsCategoriesHeader.ForeColor = System.Drawing.Color.Black;
			this.pnSearchTagsCategoriesHeader.Location = new System.Drawing.Point(0, 0);
			this.pnSearchTagsCategoriesHeader.Name = "pnSearchTagsCategoriesHeader";
			this.pnSearchTagsCategoriesHeader.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.pnSearchTagsCategoriesHeader.Size = new System.Drawing.Size(595, 49);
			this.pnSearchTagsCategoriesHeader.TabIndex = 2;
			// 
			// labelControlSearchTagsCategoriesHeader
			// 
			this.labelControlSearchTagsCategoriesHeader.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSearchTagsCategoriesHeader.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlSearchTagsCategoriesHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlSearchTagsCategoriesHeader.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControlSearchTagsCategoriesHeader.Location = new System.Drawing.Point(5, 0);
			this.labelControlSearchTagsCategoriesHeader.Name = "labelControlSearchTagsCategoriesHeader";
			this.labelControlSearchTagsCategoriesHeader.Size = new System.Drawing.Size(480, 49);
			this.labelControlSearchTagsCategoriesHeader.StyleController = this.styleController;
			this.labelControlSearchTagsCategoriesHeader.TabIndex = 0;
			this.labelControlSearchTagsCategoriesHeader.Text = "Only 0 Tags are allowed\r\nNo Tags";
			// 
			// buttonXWipeTags
			// 
			this.buttonXWipeTags.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXWipeTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXWipeTags.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXWipeTags.Location = new System.Drawing.Point(615, 7);
			this.buttonXWipeTags.Name = "buttonXWipeTags";
			this.buttonXWipeTags.Size = new System.Drawing.Size(120, 34);
			this.buttonXWipeTags.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXWipeTags.TabIndex = 7;
			this.buttonXWipeTags.Text = "Clear Tags";
			this.buttonXWipeTags.TextColor = System.Drawing.Color.Black;
			this.buttonXWipeTags.Click += new System.EventHandler(this.buttonXWipeTags_Click);
			// 
			// xtraTabPageSearchTagsKeywords
			// 
			this.xtraTabPageSearchTagsKeywords.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageSearchTagsKeywords.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageSearchTagsKeywords.Controls.Add(this.buttonXAddKeyWord);
			this.xtraTabPageSearchTagsKeywords.Controls.Add(this.gridControlSearchTagsKeywords);
			this.xtraTabPageSearchTagsKeywords.Name = "xtraTabPageSearchTagsKeywords";
			this.xtraTabPageSearchTagsKeywords.Size = new System.Drawing.Size(595, 504);
			this.xtraTabPageSearchTagsKeywords.Text = "Assign Keywords";
			// 
			// buttonXAddKeyWord
			// 
			this.buttonXAddKeyWord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAddKeyWord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAddKeyWord.Image = global::FileManager.Properties.Resources.PlusButton;
			this.buttonXAddKeyWord.ImageFixedSize = new System.Drawing.Size(24, 24);
			this.buttonXAddKeyWord.Location = new System.Drawing.Point(10, 9);
			this.buttonXAddKeyWord.Name = "buttonXAddKeyWord";
			this.buttonXAddKeyWord.Size = new System.Drawing.Size(184, 34);
			this.buttonXAddKeyWord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAddKeyWord.TabIndex = 6;
			this.buttonXAddKeyWord.Text = "Add New Keyword";
			this.buttonXAddKeyWord.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXAddKeyWord.TextColor = System.Drawing.Color.Black;
			this.buttonXAddKeyWord.Click += new System.EventHandler(this.buttonXAddKeyWord_Click);
			// 
			// gridControlSearchTagsKeywords
			// 
			this.gridControlSearchTagsKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlSearchTagsKeywords.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlSearchTagsKeywords.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlSearchTagsKeywords.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlSearchTagsKeywords.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlSearchTagsKeywords.Location = new System.Drawing.Point(10, 49);
			this.gridControlSearchTagsKeywords.MainView = this.gridViewSearchTagsKeywords;
			this.gridControlSearchTagsKeywords.Name = "gridControlSearchTagsKeywords";
			this.gridControlSearchTagsKeywords.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditKeyword});
			this.gridControlSearchTagsKeywords.Size = new System.Drawing.Size(575, 450);
			this.gridControlSearchTagsKeywords.TabIndex = 0;
			this.gridControlSearchTagsKeywords.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSearchTagsKeywords});
			// 
			// gridViewSearchTagsKeywords
			// 
			this.gridViewSearchTagsKeywords.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSearchTagsKeywords.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewSearchTagsKeywords.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewSearchTagsKeywords.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewSearchTagsKeywords.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewSearchTagsKeywords.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewSearchTagsKeywords.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSearchTagsKeywords.Appearance.Row.Options.UseFont = true;
			this.gridViewSearchTagsKeywords.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSearchTagsKeywords.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewSearchTagsKeywords.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSearchTagsKeywordsValue});
			this.gridViewSearchTagsKeywords.GridControl = this.gridControlSearchTagsKeywords;
			this.gridViewSearchTagsKeywords.Name = "gridViewSearchTagsKeywords";
			this.gridViewSearchTagsKeywords.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSearchTagsKeywords.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewSearchTagsKeywords.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewSearchTagsKeywords.OptionsCustomization.AllowFilter = false;
			this.gridViewSearchTagsKeywords.OptionsCustomization.AllowGroup = false;
			this.gridViewSearchTagsKeywords.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewSearchTagsKeywords.OptionsCustomization.AllowSort = false;
			this.gridViewSearchTagsKeywords.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewSearchTagsKeywords.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewSearchTagsKeywords.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewSearchTagsKeywords.OptionsView.ShowColumnHeaders = false;
			this.gridViewSearchTagsKeywords.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewSearchTagsKeywords.OptionsView.ShowGroupPanel = false;
			this.gridViewSearchTagsKeywords.OptionsView.ShowIndicator = false;
			this.gridViewSearchTagsKeywords.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSearchTagsKeywords.RowHeight = 35;
			// 
			// gridColumnSearchTagsKeywordsValue
			// 
			this.gridColumnSearchTagsKeywordsValue.Caption = "Value";
			this.gridColumnSearchTagsKeywordsValue.ColumnEdit = this.repositoryItemButtonEditKeyword;
			this.gridColumnSearchTagsKeywordsValue.FieldName = "Value";
			this.gridColumnSearchTagsKeywordsValue.Name = "gridColumnSearchTagsKeywordsValue";
			this.gridColumnSearchTagsKeywordsValue.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnSearchTagsKeywordsValue.Visible = true;
			this.gridColumnSearchTagsKeywordsValue.VisibleIndex = 0;
			// 
			// repositoryItemButtonEditKeyword
			// 
			this.repositoryItemButtonEditKeyword.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditKeyword.Appearance.Options.UseFont = true;
			this.repositoryItemButtonEditKeyword.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditKeyword.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemButtonEditKeyword.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditKeyword.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemButtonEditKeyword.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditKeyword.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemButtonEditKeyword.AutoHeight = false;
			this.repositoryItemButtonEditKeyword.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::FileManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.repositoryItemButtonEditKeyword.Name = "repositoryItemButtonEditKeyword";
			this.repositoryItemButtonEditKeyword.NullText = "Type Keyword...";
			this.repositoryItemButtonEditKeyword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditKeyword_ButtonClick);
			// 
			// xtraTabPageExpiredLinks
			// 
			this.xtraTabPageExpiredLinks.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageExpiredLinks.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageExpiredLinks.Controls.Add(this.gbExpiredLinks);
			this.xtraTabPageExpiredLinks.Controls.Add(this.checkBoxEnableExpiredLinks);
			this.xtraTabPageExpiredLinks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabPageExpiredLinks.Name = "xtraTabPageExpiredLinks";
			this.xtraTabPageExpiredLinks.Size = new System.Drawing.Size(601, 545);
			this.xtraTabPageExpiredLinks.Text = "Expiration Date";
			// 
			// gbExpiredLinks
			// 
			this.gbExpiredLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbExpiredLinks.Appearance.BackColor = System.Drawing.Color.White;
			this.gbExpiredLinks.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gbExpiredLinks.Appearance.Options.UseBackColor = true;
			this.gbExpiredLinks.Appearance.Options.UseForeColor = true;
			this.gbExpiredLinks.Controls.Add(this.checkBoxLabelLink);
			this.gbExpiredLinks.Controls.Add(this.timeEditExpirationTime);
			this.gbExpiredLinks.Controls.Add(this.checkBoxSendEmailWhenDelete);
			this.gbExpiredLinks.Controls.Add(this.laExpireddateActions);
			this.gbExpiredLinks.Controls.Add(this.dateEditExpirationDate);
			this.gbExpiredLinks.Controls.Add(this.laExpirationDateTitle);
			this.gbExpiredLinks.Controls.Add(this.laAddDateValue);
			this.gbExpiredLinks.Controls.Add(this.laAddDateTitle);
			this.gbExpiredLinks.Enabled = false;
			this.gbExpiredLinks.Location = new System.Drawing.Point(11, 37);
			this.gbExpiredLinks.Name = "gbExpiredLinks";
			this.gbExpiredLinks.ShowCaption = false;
			this.gbExpiredLinks.Size = new System.Drawing.Size(579, 496);
			this.gbExpiredLinks.TabIndex = 1;
			// 
			// checkBoxLabelLink
			// 
			this.checkBoxLabelLink.AutoSize = true;
			this.checkBoxLabelLink.BackColor = System.Drawing.Color.Transparent;
			this.checkBoxLabelLink.ForeColor = System.Drawing.Color.Black;
			this.checkBoxLabelLink.Location = new System.Drawing.Point(9, 150);
			this.checkBoxLabelLink.Name = "checkBoxLabelLink";
			this.checkBoxLabelLink.Size = new System.Drawing.Size(164, 20);
			this.checkBoxLabelLink.TabIndex = 11;
			this.checkBoxLabelLink.Text = "Display EXPIRED Label";
			this.checkBoxLabelLink.UseVisualStyleBackColor = false;
			// 
			// timeEditExpirationTime
			// 
			this.timeEditExpirationTime.EditValue = new System.DateTime(2011, 8, 15, 0, 0, 0, 0);
			this.timeEditExpirationTime.Location = new System.Drawing.Point(135, 103);
			this.timeEditExpirationTime.Name = "timeEditExpirationTime";
			this.timeEditExpirationTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.timeEditExpirationTime.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.timeEditExpirationTime.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.timeEditExpirationTime.Properties.Appearance.Options.UseBackColor = true;
			this.timeEditExpirationTime.Properties.Appearance.Options.UseFont = true;
			this.timeEditExpirationTime.Properties.Appearance.Options.UseForeColor = true;
			this.timeEditExpirationTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.timeEditExpirationTime.Properties.EditValueChangedDelay = 10000;
			this.timeEditExpirationTime.Properties.HideSelection = false;
			this.timeEditExpirationTime.Size = new System.Drawing.Size(100, 22);
			this.timeEditExpirationTime.TabIndex = 10;
			// 
			// checkBoxSendEmailWhenDelete
			// 
			this.checkBoxSendEmailWhenDelete.AutoSize = true;
			this.checkBoxSendEmailWhenDelete.BackColor = System.Drawing.Color.Transparent;
			this.checkBoxSendEmailWhenDelete.ForeColor = System.Drawing.Color.Black;
			this.checkBoxSendEmailWhenDelete.Location = new System.Drawing.Point(9, 176);
			this.checkBoxSendEmailWhenDelete.Name = "checkBoxSendEmailWhenDelete";
			this.checkBoxSendEmailWhenDelete.Size = new System.Drawing.Size(282, 20);
			this.checkBoxSendEmailWhenDelete.TabIndex = 9;
			this.checkBoxSendEmailWhenDelete.Text = "Send Reminder Email to Admin List at Sync";
			this.checkBoxSendEmailWhenDelete.UseVisualStyleBackColor = false;
			// 
			// laExpireddateActions
			// 
			this.laExpireddateActions.AutoSize = true;
			this.laExpireddateActions.BackColor = System.Drawing.Color.Transparent;
			this.laExpireddateActions.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laExpireddateActions.ForeColor = System.Drawing.Color.Black;
			this.laExpireddateActions.Location = new System.Drawing.Point(6, 131);
			this.laExpireddateActions.Name = "laExpireddateActions";
			this.laExpireddateActions.Size = new System.Drawing.Size(153, 16);
			this.laExpireddateActions.TabIndex = 6;
			this.laExpireddateActions.Text = "When the Link Expires,";
			// 
			// dateEditExpirationDate
			// 
			this.dateEditExpirationDate.EditValue = null;
			this.dateEditExpirationDate.Location = new System.Drawing.Point(9, 103);
			this.dateEditExpirationDate.Name = "dateEditExpirationDate";
			this.dateEditExpirationDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditExpirationDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.dateEditExpirationDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditExpirationDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.dateEditExpirationDate.Properties.Appearance.Options.UseBackColor = true;
			this.dateEditExpirationDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditExpirationDate.Properties.Appearance.Options.UseForeColor = true;
			this.dateEditExpirationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditExpirationDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.dateEditExpirationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditExpirationDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditExpirationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditExpirationDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditExpirationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditExpirationDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditExpirationDate.Properties.NullText = "Select";
			this.dateEditExpirationDate.Properties.ShowPopupShadow = false;
			this.dateEditExpirationDate.Properties.ShowToday = false;
			this.dateEditExpirationDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditExpirationDate.Size = new System.Drawing.Size(120, 22);
			this.dateEditExpirationDate.TabIndex = 5;
			// 
			// laExpirationDateTitle
			// 
			this.laExpirationDateTitle.AutoSize = true;
			this.laExpirationDateTitle.BackColor = System.Drawing.Color.Transparent;
			this.laExpirationDateTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laExpirationDateTitle.ForeColor = System.Drawing.Color.Black;
			this.laExpirationDateTitle.Location = new System.Drawing.Point(6, 65);
			this.laExpirationDateTitle.Name = "laExpirationDateTitle";
			this.laExpirationDateTitle.Size = new System.Drawing.Size(140, 16);
			this.laExpirationDateTitle.TabIndex = 2;
			this.laExpirationDateTitle.Text = "This Link Expires on:";
			// 
			// laAddDateValue
			// 
			this.laAddDateValue.AutoSize = true;
			this.laAddDateValue.BackColor = System.Drawing.Color.Transparent;
			this.laAddDateValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAddDateValue.ForeColor = System.Drawing.Color.Black;
			this.laAddDateValue.Location = new System.Drawing.Point(6, 30);
			this.laAddDateValue.Name = "laAddDateValue";
			this.laAddDateValue.Size = new System.Drawing.Size(42, 16);
			this.laAddDateValue.TabIndex = 1;
			this.laAddDateValue.Text = "label1";
			// 
			// laAddDateTitle
			// 
			this.laAddDateTitle.AutoSize = true;
			this.laAddDateTitle.BackColor = System.Drawing.Color.Transparent;
			this.laAddDateTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAddDateTitle.ForeColor = System.Drawing.Color.Black;
			this.laAddDateTitle.Location = new System.Drawing.Point(6, 9);
			this.laAddDateTitle.Name = "laAddDateTitle";
			this.laAddDateTitle.Size = new System.Drawing.Size(104, 16);
			this.laAddDateTitle.TabIndex = 0;
			this.laAddDateTitle.Text = "Link Added on:";
			// 
			// checkBoxEnableExpiredLinks
			// 
			this.checkBoxEnableExpiredLinks.AutoSize = true;
			this.checkBoxEnableExpiredLinks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxEnableExpiredLinks.ForeColor = System.Drawing.Color.Black;
			this.checkBoxEnableExpiredLinks.Location = new System.Drawing.Point(11, 11);
			this.checkBoxEnableExpiredLinks.Name = "checkBoxEnableExpiredLinks";
			this.checkBoxEnableExpiredLinks.Size = new System.Drawing.Size(173, 20);
			this.checkBoxEnableExpiredLinks.TabIndex = 0;
			this.checkBoxEnableExpiredLinks.Text = "Enable Expiration Date";
			this.checkBoxEnableExpiredLinks.UseVisualStyleBackColor = true;
			this.checkBoxEnableExpiredLinks.CheckedChanged += new System.EventHandler(this.checkBoxEnableExpiredLinks_CheckedChanged);
			// 
			// xtraTabPageLineBrealProperties
			// 
			this.xtraTabPageLineBrealProperties.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageLineBrealProperties.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageLineBrealProperties.Controls.Add(this.memoEditNote);
			this.xtraTabPageLineBrealProperties.Controls.Add(this.laNote);
			this.xtraTabPageLineBrealProperties.Controls.Add(this.laFont);
			this.xtraTabPageLineBrealProperties.Controls.Add(this.buttonEditLineBreakFont);
			this.xtraTabPageLineBrealProperties.Controls.Add(this.colorEditLineBreakFontColor);
			this.xtraTabPageLineBrealProperties.Controls.Add(this.laFontColor);
			this.xtraTabPageLineBrealProperties.Name = "xtraTabPageLineBrealProperties";
			this.xtraTabPageLineBrealProperties.Size = new System.Drawing.Size(601, 545);
			this.xtraTabPageLineBrealProperties.Text = "Info";
			// 
			// memoEditNote
			// 
			this.memoEditNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditNote.Location = new System.Drawing.Point(98, 94);
			this.memoEditNote.Name = "memoEditNote";
			this.memoEditNote.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditNote.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.memoEditNote.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditNote.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditNote.Size = new System.Drawing.Size(480, 423);
			this.memoEditNote.StyleController = this.styleController;
			this.memoEditNote.TabIndex = 33;
			this.memoEditNote.UseOptimizedRendering = true;
			// 
			// laNote
			// 
			this.laNote.AutoSize = true;
			this.laNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNote.ForeColor = System.Drawing.Color.Black;
			this.laNote.Location = new System.Drawing.Point(7, 95);
			this.laNote.Name = "laNote";
			this.laNote.Size = new System.Drawing.Size(37, 16);
			this.laNote.TabIndex = 32;
			this.laNote.Text = "Note";
			// 
			// laFont
			// 
			this.laFont.AutoSize = true;
			this.laFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laFont.ForeColor = System.Drawing.Color.Black;
			this.laFont.Location = new System.Drawing.Point(7, 16);
			this.laFont.Name = "laFont";
			this.laFont.Size = new System.Drawing.Size(34, 16);
			this.laFont.TabIndex = 30;
			this.laFont.Text = "Font";
			// 
			// buttonEditLineBreakFont
			// 
			this.buttonEditLineBreakFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditLineBreakFont.Location = new System.Drawing.Point(98, 15);
			this.buttonEditLineBreakFont.Name = "buttonEditLineBreakFont";
			this.buttonEditLineBreakFont.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditLineBreakFont.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditLineBreakFont.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditLineBreakFont.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditLineBreakFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditLineBreakFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.buttonEditLineBreakFont.Size = new System.Drawing.Size(480, 22);
			this.buttonEditLineBreakFont.StyleController = this.styleController;
			this.buttonEditLineBreakFont.TabIndex = 29;
			this.buttonEditLineBreakFont.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FontEdit_ButtonClick);
			this.buttonEditLineBreakFont.Click += new System.EventHandler(this.FontEdit_Click);
			// 
			// colorEditLineBreakFontColor
			// 
			this.colorEditLineBreakFontColor.EditValue = System.Drawing.Color.Empty;
			this.colorEditLineBreakFontColor.Location = new System.Drawing.Point(98, 54);
			this.colorEditLineBreakFontColor.Name = "colorEditLineBreakFontColor";
			this.colorEditLineBreakFontColor.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.colorEditLineBreakFontColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditLineBreakFontColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditLineBreakFontColor.Properties.Appearance.Options.UseForeColor = true;
			this.colorEditLineBreakFontColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.colorEditLineBreakFontColor.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colorEditLineBreakFontColor.Size = new System.Drawing.Size(105, 22);
			this.colorEditLineBreakFontColor.StyleController = this.styleController;
			this.colorEditLineBreakFontColor.TabIndex = 26;
			// 
			// laFontColor
			// 
			this.laFontColor.AutoSize = true;
			this.laFontColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laFontColor.ForeColor = System.Drawing.Color.Black;
			this.laFontColor.Location = new System.Drawing.Point(7, 57);
			this.laFontColor.Name = "laFontColor";
			this.laFontColor.Size = new System.Drawing.Size(69, 16);
			this.laFontColor.TabIndex = 25;
			this.laFontColor.Text = "Font Color";
			// 
			// xtraTabPageSecurity
			// 
			this.xtraTabPageSecurity.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageSecurity.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageSecurity.Controls.Add(this.groupBoxSecurity);
			this.xtraTabPageSecurity.Name = "xtraTabPageSecurity";
			this.xtraTabPageSecurity.Size = new System.Drawing.Size(601, 545);
			this.xtraTabPageSecurity.Text = "Security";
			// 
			// groupBoxSecurity
			// 
			this.groupBoxSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxSecurity.Appearance.BackColor = System.Drawing.Color.White;
			this.groupBoxSecurity.Appearance.ForeColor = System.Drawing.Color.Black;
			this.groupBoxSecurity.Appearance.Options.UseBackColor = true;
			this.groupBoxSecurity.Appearance.Options.UseForeColor = true;
			this.groupBoxSecurity.Controls.Add(this.rbSecurityBlackList);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityForbidden);
			this.groupBoxSecurity.Controls.Add(this.ckSecurityShareLink);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityAllowed);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityWhiteList);
			this.groupBoxSecurity.Controls.Add(this.rbSecurityDenied);
			this.groupBoxSecurity.Controls.Add(this.pnSecurityUserList);
			this.groupBoxSecurity.Location = new System.Drawing.Point(10, 3);
			this.groupBoxSecurity.Name = "groupBoxSecurity";
			this.groupBoxSecurity.ShowCaption = false;
			this.groupBoxSecurity.Size = new System.Drawing.Size(541, 513);
			this.groupBoxSecurity.TabIndex = 1;
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
			this.rbSecurityBlackList.Size = new System.Drawing.Size(521, 45);
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
			this.rbSecurityForbidden.Size = new System.Drawing.Size(521, 45);
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
			this.ckSecurityShareLink.Location = new System.Drawing.Point(8, 478);
			this.ckSecurityShareLink.Name = "ckSecurityShareLink";
			this.ckSecurityShareLink.Size = new System.Drawing.Size(521, 29);
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
			this.rbSecurityAllowed.Size = new System.Drawing.Size(521, 39);
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
			this.rbSecurityWhiteList.Size = new System.Drawing.Size(521, 45);
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
			this.rbSecurityDenied.Size = new System.Drawing.Size(521, 45);
			this.rbSecurityDenied.TabIndex = 0;
			this.rbSecurityDenied.TabStop = true;
			this.rbSecurityDenied.Text = "This link is ONLY in the Local Sales Library. It is NOT visible in the Web Sales " +
    "Library)";
			this.rbSecurityDenied.UseVisualStyleBackColor = false;
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
			this.pnSecurityUserList.Location = new System.Drawing.Point(24, 259);
			this.pnSecurityUserList.Name = "pnSecurityUserList";
			this.pnSecurityUserList.Size = new System.Drawing.Size(505, 213);
			this.pnSecurityUserList.TabIndex = 37;
			// 
			// pnSecurityUserListGrid
			// 
			this.pnSecurityUserListGrid.Controls.Add(this.buttonXSecurityUserListClearAll);
			this.pnSecurityUserListGrid.Controls.Add(this.buttonXSecurityUserListSelectAll);
			this.pnSecurityUserListGrid.Controls.Add(this.gridControlSecurityUserList);
			this.pnSecurityUserListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnSecurityUserListGrid.Enabled = false;
			this.pnSecurityUserListGrid.ForeColor = System.Drawing.Color.Black;
			this.pnSecurityUserListGrid.Location = new System.Drawing.Point(0, 95);
			this.pnSecurityUserListGrid.Name = "pnSecurityUserListGrid";
			this.pnSecurityUserListGrid.Size = new System.Drawing.Size(505, 118);
			this.pnSecurityUserListGrid.TabIndex = 7;
			// 
			// buttonXSecurityUserListClearAll
			// 
			this.buttonXSecurityUserListClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSecurityUserListClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSecurityUserListClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSecurityUserListClearAll.Location = new System.Drawing.Point(274, 6);
			this.buttonXSecurityUserListClearAll.Name = "buttonXSecurityUserListClearAll";
			this.buttonXSecurityUserListClearAll.Size = new System.Drawing.Size(231, 32);
			this.buttonXSecurityUserListClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSecurityUserListClearAll.TabIndex = 8;
			this.buttonXSecurityUserListClearAll.Text = "REMOVE ALL Groups and Users";
			this.buttonXSecurityUserListClearAll.Click += new System.EventHandler(this.buttonXSecurityUserListClearAll_Click);
			// 
			// buttonXSecurityUserListSelectAll
			// 
			this.buttonXSecurityUserListSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSecurityUserListSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSecurityUserListSelectAll.Location = new System.Drawing.Point(0, 6);
			this.buttonXSecurityUserListSelectAll.Name = "buttonXSecurityUserListSelectAll";
			this.buttonXSecurityUserListSelectAll.Size = new System.Drawing.Size(231, 32);
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
			this.circularSecurityUserListProgress.Size = new System.Drawing.Size(505, 39);
			this.circularSecurityUserListProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularSecurityUserListProgress.TabIndex = 4;
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
			this.laSecurityUserListInfo.Size = new System.Drawing.Size(505, 56);
			this.laSecurityUserListInfo.TabIndex = 5;
			this.laSecurityUserListInfo.Text = "labelControl";
			// 
			// xtraTabPageWidgets
			// 
			this.xtraTabPageWidgets.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageWidgets.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageWidgets.Controls.Add(this.groupBoxWidgets);
			this.xtraTabPageWidgets.Controls.Add(this.checkBoxEnableWidget);
			this.xtraTabPageWidgets.Name = "xtraTabPageWidgets";
			this.xtraTabPageWidgets.Size = new System.Drawing.Size(601, 545);
			this.xtraTabPageWidgets.Text = "Widget";
			// 
			// groupBoxWidgets
			// 
			this.groupBoxWidgets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxWidgets.Appearance.BackColor = System.Drawing.Color.White;
			this.groupBoxWidgets.Appearance.ForeColor = System.Drawing.Color.Black;
			this.groupBoxWidgets.Appearance.Options.UseBackColor = true;
			this.groupBoxWidgets.Appearance.Options.UseForeColor = true;
			this.groupBoxWidgets.Controls.Add(this.xtraTabControlWidgets);
			this.groupBoxWidgets.Controls.Add(this.laWidgetFileName);
			this.groupBoxWidgets.Controls.Add(this.pbSelectedWidget);
			this.groupBoxWidgets.Controls.Add(this.laAvailableWidgets);
			this.groupBoxWidgets.Controls.Add(this.laSelectedWidget);
			this.groupBoxWidgets.Enabled = false;
			this.groupBoxWidgets.Location = new System.Drawing.Point(11, 37);
			this.groupBoxWidgets.Name = "groupBoxWidgets";
			this.groupBoxWidgets.ShowCaption = false;
			this.groupBoxWidgets.Size = new System.Drawing.Size(540, 486);
			this.groupBoxWidgets.TabIndex = 3;
			// 
			// xtraTabControlWidgets
			// 
			this.xtraTabControlWidgets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControlWidgets.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraTabControlWidgets.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlWidgets.Appearance.Options.UseBackColor = true;
			this.xtraTabControlWidgets.Appearance.Options.UseForeColor = true;
			this.xtraTabControlWidgets.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlWidgets.Location = new System.Drawing.Point(6, 79);
			this.xtraTabControlWidgets.Name = "xtraTabControlWidgets";
			this.xtraTabControlWidgets.Size = new System.Drawing.Size(528, 401);
			this.xtraTabControlWidgets.TabIndex = 6;
			this.xtraTabControlWidgets.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlWidgets_SelectedPageChanged);
			// 
			// laWidgetFileName
			// 
			this.laWidgetFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.laWidgetFileName.BackColor = System.Drawing.Color.Transparent;
			this.laWidgetFileName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laWidgetFileName.ForeColor = System.Drawing.Color.Black;
			this.laWidgetFileName.Location = new System.Drawing.Point(315, 17);
			this.laWidgetFileName.Name = "laWidgetFileName";
			this.laWidgetFileName.Size = new System.Drawing.Size(208, 18);
			this.laWidgetFileName.TabIndex = 5;
			this.laWidgetFileName.Text = "Selected Widget";
			this.laWidgetFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pbSelectedWidget
			// 
			this.pbSelectedWidget.BackColor = System.Drawing.Color.Transparent;
			this.pbSelectedWidget.ForeColor = System.Drawing.Color.Black;
			this.pbSelectedWidget.Location = new System.Drawing.Point(129, 9);
			this.pbSelectedWidget.Name = "pbSelectedWidget";
			this.pbSelectedWidget.Size = new System.Drawing.Size(36, 36);
			this.pbSelectedWidget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbSelectedWidget.TabIndex = 3;
			this.pbSelectedWidget.TabStop = false;
			// 
			// laAvailableWidgets
			// 
			this.laAvailableWidgets.AutoSize = true;
			this.laAvailableWidgets.BackColor = System.Drawing.Color.Transparent;
			this.laAvailableWidgets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvailableWidgets.ForeColor = System.Drawing.Color.Black;
			this.laAvailableWidgets.Location = new System.Drawing.Point(6, 60);
			this.laAvailableWidgets.Name = "laAvailableWidgets";
			this.laAvailableWidgets.Size = new System.Drawing.Size(254, 16);
			this.laAvailableWidgets.TabIndex = 2;
			this.laAvailableWidgets.Text = "Click on image below to select widget:";
			// 
			// laSelectedWidget
			// 
			this.laSelectedWidget.AutoSize = true;
			this.laSelectedWidget.BackColor = System.Drawing.Color.Transparent;
			this.laSelectedWidget.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSelectedWidget.ForeColor = System.Drawing.Color.Black;
			this.laSelectedWidget.Location = new System.Drawing.Point(6, 19);
			this.laSelectedWidget.Name = "laSelectedWidget";
			this.laSelectedWidget.Size = new System.Drawing.Size(117, 16);
			this.laSelectedWidget.TabIndex = 0;
			this.laSelectedWidget.Text = "Selected Widget:";
			// 
			// checkBoxEnableWidget
			// 
			this.checkBoxEnableWidget.AutoSize = true;
			this.checkBoxEnableWidget.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxEnableWidget.ForeColor = System.Drawing.Color.Black;
			this.checkBoxEnableWidget.Location = new System.Drawing.Point(11, 11);
			this.checkBoxEnableWidget.Name = "checkBoxEnableWidget";
			this.checkBoxEnableWidget.Size = new System.Drawing.Size(120, 20);
			this.checkBoxEnableWidget.TabIndex = 2;
			this.checkBoxEnableWidget.Text = "Enable Widget";
			this.checkBoxEnableWidget.UseVisualStyleBackColor = true;
			this.checkBoxEnableWidget.CheckedChanged += new System.EventHandler(this.checkBoxEnableWidget_CheckedChanged);
			// 
			// xtraTabPageBanner
			// 
			this.xtraTabPageBanner.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageBanner.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageBanner.Controls.Add(this.groupBoxBanners);
			this.xtraTabPageBanner.Controls.Add(this.checkBoxEnableBanner);
			this.xtraTabPageBanner.Name = "xtraTabPageBanner";
			this.xtraTabPageBanner.Size = new System.Drawing.Size(601, 545);
			this.xtraTabPageBanner.Text = "Banner";
			// 
			// groupBoxBanners
			// 
			this.groupBoxBanners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxBanners.Appearance.BackColor = System.Drawing.Color.White;
			this.groupBoxBanners.Appearance.ForeColor = System.Drawing.Color.Black;
			this.groupBoxBanners.Appearance.Options.UseBackColor = true;
			this.groupBoxBanners.Appearance.Options.UseForeColor = true;
			this.groupBoxBanners.Controls.Add(this.xtraTabControlBanners);
			this.groupBoxBanners.Controls.Add(this.pbSelectedBanner);
			this.groupBoxBanners.Controls.Add(this.laBannerFileName);
			this.groupBoxBanners.Controls.Add(this.colorEditBannerTextColor);
			this.groupBoxBanners.Controls.Add(this.buttonEditBannerTextFont);
			this.groupBoxBanners.Controls.Add(this.memoEditBannerText);
			this.groupBoxBanners.Controls.Add(this.checkBoxBannerShowText);
			this.groupBoxBanners.Controls.Add(this.rbBannerAligmentRight);
			this.groupBoxBanners.Controls.Add(this.rbBannerAligmentCenter);
			this.groupBoxBanners.Controls.Add(this.rbBannerAligmentLeft);
			this.groupBoxBanners.Controls.Add(this.laBannerAligment);
			this.groupBoxBanners.Controls.Add(this.laAvailableBanners);
			this.groupBoxBanners.Controls.Add(this.laSelectedBanner);
			this.groupBoxBanners.Enabled = false;
			this.groupBoxBanners.Location = new System.Drawing.Point(11, 37);
			this.groupBoxBanners.Name = "groupBoxBanners";
			this.groupBoxBanners.ShowCaption = false;
			this.groupBoxBanners.Size = new System.Drawing.Size(540, 487);
			this.groupBoxBanners.TabIndex = 5;
			// 
			// xtraTabControlBanners
			// 
			this.xtraTabControlBanners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControlBanners.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraTabControlBanners.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlBanners.Appearance.Options.UseBackColor = true;
			this.xtraTabControlBanners.Appearance.Options.UseForeColor = true;
			this.xtraTabControlBanners.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlBanners.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlBanners.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlBanners.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlBanners.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlBanners.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlBanners.Location = new System.Drawing.Point(9, 90);
			this.xtraTabControlBanners.Name = "xtraTabControlBanners";
			this.xtraTabControlBanners.Size = new System.Drawing.Size(525, 223);
			this.xtraTabControlBanners.TabIndex = 36;
			this.xtraTabControlBanners.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlBanners_SelectedPageChanged);
			// 
			// pbSelectedBanner
			// 
			this.pbSelectedBanner.Location = new System.Drawing.Point(129, 9);
			this.pbSelectedBanner.Name = "pbSelectedBanner";
			this.pbSelectedBanner.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pbSelectedBanner.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.pbSelectedBanner.Properties.Appearance.Options.UseBackColor = true;
			this.pbSelectedBanner.Properties.Appearance.Options.UseForeColor = true;
			this.pbSelectedBanner.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pbSelectedBanner.Properties.NullText = " ";
			this.pbSelectedBanner.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			this.pbSelectedBanner.Size = new System.Drawing.Size(266, 59);
			this.pbSelectedBanner.TabIndex = 35;
			// 
			// laBannerFileName
			// 
			this.laBannerFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.laBannerFileName.BackColor = System.Drawing.Color.Transparent;
			this.laBannerFileName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laBannerFileName.ForeColor = System.Drawing.Color.Black;
			this.laBannerFileName.Location = new System.Drawing.Point(326, 17);
			this.laBannerFileName.Name = "laBannerFileName";
			this.laBannerFileName.Size = new System.Drawing.Size(208, 18);
			this.laBannerFileName.TabIndex = 33;
			this.laBannerFileName.Text = "Selected Banner";
			this.laBannerFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// colorEditBannerTextColor
			// 
			this.colorEditBannerTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.colorEditBannerTextColor.EditValue = System.Drawing.Color.Empty;
			this.colorEditBannerTextColor.Enabled = false;
			this.colorEditBannerTextColor.Location = new System.Drawing.Point(429, 451);
			this.colorEditBannerTextColor.Name = "colorEditBannerTextColor";
			this.colorEditBannerTextColor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.colorEditBannerTextColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditBannerTextColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditBannerTextColor.Properties.Appearance.Options.UseForeColor = true;
			this.colorEditBannerTextColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.colorEditBannerTextColor.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colorEditBannerTextColor.Size = new System.Drawing.Size(105, 22);
			this.colorEditBannerTextColor.StyleController = this.styleController;
			this.colorEditBannerTextColor.TabIndex = 32;
			this.colorEditBannerTextColor.EditValueChanged += new System.EventHandler(this.colorEditBannerTextColor_EditValueChanged);
			// 
			// buttonEditBannerTextFont
			// 
			this.buttonEditBannerTextFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditBannerTextFont.Enabled = false;
			this.buttonEditBannerTextFont.Location = new System.Drawing.Point(10, 451);
			this.buttonEditBannerTextFont.Name = "buttonEditBannerTextFont";
			this.buttonEditBannerTextFont.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.buttonEditBannerTextFont.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditBannerTextFont.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditBannerTextFont.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditBannerTextFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditBannerTextFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.buttonEditBannerTextFont.Size = new System.Drawing.Size(403, 22);
			this.buttonEditBannerTextFont.StyleController = this.styleController;
			this.buttonEditBannerTextFont.TabIndex = 31;
			this.buttonEditBannerTextFont.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FontEdit_ButtonClick);
			this.buttonEditBannerTextFont.EditValueChanged += new System.EventHandler(this.buttonEditBannerTextFont_EditValueChanged);
			this.buttonEditBannerTextFont.Click += new System.EventHandler(this.FontEdit_Click);
			// 
			// memoEditBannerText
			// 
			this.memoEditBannerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditBannerText.Enabled = false;
			this.memoEditBannerText.Location = new System.Drawing.Point(9, 396);
			this.memoEditBannerText.Name = "memoEditBannerText";
			this.memoEditBannerText.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.memoEditBannerText.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.memoEditBannerText.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditBannerText.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditBannerText.Size = new System.Drawing.Size(525, 49);
			this.memoEditBannerText.StyleController = this.styleController;
			this.memoEditBannerText.TabIndex = 10;
			this.memoEditBannerText.UseOptimizedRendering = true;
			// 
			// checkBoxBannerShowText
			// 
			this.checkBoxBannerShowText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxBannerShowText.AutoSize = true;
			this.checkBoxBannerShowText.BackColor = System.Drawing.Color.Transparent;
			this.checkBoxBannerShowText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.checkBoxBannerShowText.ForeColor = System.Drawing.Color.Black;
			this.checkBoxBannerShowText.Location = new System.Drawing.Point(9, 370);
			this.checkBoxBannerShowText.Name = "checkBoxBannerShowText";
			this.checkBoxBannerShowText.Size = new System.Drawing.Size(134, 20);
			this.checkBoxBannerShowText.TabIndex = 9;
			this.checkBoxBannerShowText.Text = "Show Link Label";
			this.checkBoxBannerShowText.UseVisualStyleBackColor = false;
			this.checkBoxBannerShowText.CheckedChanged += new System.EventHandler(this.checkBoxBannerShowText_CheckedChanged);
			// 
			// rbBannerAligmentRight
			// 
			this.rbBannerAligmentRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbBannerAligmentRight.BackColor = System.Drawing.Color.Transparent;
			this.rbBannerAligmentRight.ForeColor = System.Drawing.Color.Black;
			this.rbBannerAligmentRight.Location = new System.Drawing.Point(206, 335);
			this.rbBannerAligmentRight.Name = "rbBannerAligmentRight";
			this.rbBannerAligmentRight.Size = new System.Drawing.Size(86, 20);
			this.rbBannerAligmentRight.TabIndex = 8;
			this.rbBannerAligmentRight.TabStop = true;
			this.rbBannerAligmentRight.Text = "Right";
			this.rbBannerAligmentRight.UseVisualStyleBackColor = false;
			// 
			// rbBannerAligmentCenter
			// 
			this.rbBannerAligmentCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbBannerAligmentCenter.BackColor = System.Drawing.Color.Transparent;
			this.rbBannerAligmentCenter.ForeColor = System.Drawing.Color.Black;
			this.rbBannerAligmentCenter.Location = new System.Drawing.Point(107, 335);
			this.rbBannerAligmentCenter.Name = "rbBannerAligmentCenter";
			this.rbBannerAligmentCenter.Size = new System.Drawing.Size(94, 20);
			this.rbBannerAligmentCenter.TabIndex = 7;
			this.rbBannerAligmentCenter.TabStop = true;
			this.rbBannerAligmentCenter.Text = "Center";
			this.rbBannerAligmentCenter.UseVisualStyleBackColor = false;
			// 
			// rbBannerAligmentLeft
			// 
			this.rbBannerAligmentLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbBannerAligmentLeft.BackColor = System.Drawing.Color.Transparent;
			this.rbBannerAligmentLeft.ForeColor = System.Drawing.Color.Black;
			this.rbBannerAligmentLeft.Location = new System.Drawing.Point(9, 335);
			this.rbBannerAligmentLeft.Name = "rbBannerAligmentLeft";
			this.rbBannerAligmentLeft.Size = new System.Drawing.Size(77, 20);
			this.rbBannerAligmentLeft.TabIndex = 6;
			this.rbBannerAligmentLeft.TabStop = true;
			this.rbBannerAligmentLeft.Text = "Left";
			this.rbBannerAligmentLeft.UseVisualStyleBackColor = false;
			// 
			// laBannerAligment
			// 
			this.laBannerAligment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.laBannerAligment.AutoSize = true;
			this.laBannerAligment.BackColor = System.Drawing.Color.Transparent;
			this.laBannerAligment.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laBannerAligment.ForeColor = System.Drawing.Color.Black;
			this.laBannerAligment.Location = new System.Drawing.Point(6, 316);
			this.laBannerAligment.Name = "laBannerAligment";
			this.laBannerAligment.Size = new System.Drawing.Size(127, 16);
			this.laBannerAligment.TabIndex = 5;
			this.laBannerAligment.Text = "Banner Alignment:";
			// 
			// laAvailableBanners
			// 
			this.laAvailableBanners.AutoSize = true;
			this.laAvailableBanners.BackColor = System.Drawing.Color.Transparent;
			this.laAvailableBanners.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvailableBanners.ForeColor = System.Drawing.Color.Black;
			this.laAvailableBanners.Location = new System.Drawing.Point(6, 71);
			this.laAvailableBanners.Name = "laAvailableBanners";
			this.laAvailableBanners.Size = new System.Drawing.Size(256, 16);
			this.laAvailableBanners.TabIndex = 2;
			this.laAvailableBanners.Text = "Click on image below to select banner:";
			// 
			// laSelectedBanner
			// 
			this.laSelectedBanner.AutoSize = true;
			this.laSelectedBanner.BackColor = System.Drawing.Color.Transparent;
			this.laSelectedBanner.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSelectedBanner.ForeColor = System.Drawing.Color.Black;
			this.laSelectedBanner.Location = new System.Drawing.Point(6, 19);
			this.laSelectedBanner.Name = "laSelectedBanner";
			this.laSelectedBanner.Size = new System.Drawing.Size(118, 16);
			this.laSelectedBanner.TabIndex = 0;
			this.laSelectedBanner.Text = "Selected Banner:";
			// 
			// checkBoxEnableBanner
			// 
			this.checkBoxEnableBanner.AutoSize = true;
			this.checkBoxEnableBanner.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxEnableBanner.ForeColor = System.Drawing.Color.Black;
			this.checkBoxEnableBanner.Location = new System.Drawing.Point(11, 11);
			this.checkBoxEnableBanner.Name = "checkBoxEnableBanner";
			this.checkBoxEnableBanner.Size = new System.Drawing.Size(121, 20);
			this.checkBoxEnableBanner.TabIndex = 4;
			this.checkBoxEnableBanner.Text = "Enable Banner";
			this.checkBoxEnableBanner.UseVisualStyleBackColor = true;
			this.checkBoxEnableBanner.CheckedChanged += new System.EventHandler(this.checkBoxEnableBanner_CheckedChanged);
			// 
			// persistentRepository
			// 
			this.persistentRepository.Items.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(364, 555);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(111, 34);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 5;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			this.buttonXOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(495, 555);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(111, 34);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 6;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// hyperLinkEditRequestNewCategories
			// 
			this.hyperLinkEditRequestNewCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.hyperLinkEditRequestNewCategories.EditValue = "Request New Search Tags? Click Here";
			this.hyperLinkEditRequestNewCategories.Location = new System.Drawing.Point(13, 561);
			this.hyperLinkEditRequestNewCategories.Name = "hyperLinkEditRequestNewCategories";
			this.hyperLinkEditRequestNewCategories.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.hyperLinkEditRequestNewCategories.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditRequestNewCategories.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.hyperLinkEditRequestNewCategories.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditRequestNewCategories.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditRequestNewCategories.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditRequestNewCategories.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditRequestNewCategories.Size = new System.Drawing.Size(251, 20);
			this.hyperLinkEditRequestNewCategories.TabIndex = 7;
			this.hyperLinkEditRequestNewCategories.TabStop = false;
			this.hyperLinkEditRequestNewCategories.Visible = false;
			this.hyperLinkEditRequestNewCategories.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEditRequestNewCategories_OpenLink);
			// 
			// laLinkHoverNote
			// 
			this.laLinkHoverNote.AutoSize = true;
			this.laLinkHoverNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laLinkHoverNote.ForeColor = System.Drawing.Color.Black;
			this.laLinkHoverNote.Location = new System.Drawing.Point(9, 378);
			this.laLinkHoverNote.Name = "laLinkHoverNote";
			this.laLinkHoverNote.Size = new System.Drawing.Size(101, 19);
			this.laLinkHoverNote.TabIndex = 19;
			this.laLinkHoverNote.Text = "Hover Note:";
			// 
			// textEditLinkHoverNote
			// 
			this.textEditLinkHoverNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditLinkHoverNote.Location = new System.Drawing.Point(116, 377);
			this.textEditLinkHoverNote.Name = "textEditLinkHoverNote";
			this.textEditLinkHoverNote.Size = new System.Drawing.Size(469, 22);
			this.textEditLinkHoverNote.StyleController = this.styleController;
			this.textEditLinkHoverNote.TabIndex = 20;
			// 
			// FormLinkProperties
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(607, 592);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.hyperLinkEditRequestNewCategories);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.buttonXCancel);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MinimizeBox = false;
			this.Name = "FormLinkProperties";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Link Properties";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLinkProperties_FormClosed);
			this.Load += new System.EventHandler(this.FormProperties_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditSecurityUserList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSecurityUserList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSecurityGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageNotes.ResumeLayout(false);
			this.xtraTabPageNotes.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControlTextFormat)).EndInit();
			this.groupControlTextFormat.ResumeLayout(false);
			this.groupControlTextFormat.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLinkSpecialColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLinkSpecialFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlNotes)).EndInit();
			this.groupControlNotes.ResumeLayout(false);
			this.groupControlNotes.PerformLayout();
			this.pnAdminTools.ResumeLayout(false);
			this.pnAdminTools.PerformLayout();
			this.xtraTabPageSearchTags.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSearchTags)).EndInit();
			this.xtraTabControlSearchTags.ResumeLayout(false);
			this.xtraTabPageSearchTagsCategories.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSearchTagsCategories)).EndInit();
			this.splitContainerSearchTagsCategories.ResumeLayout(false);
			this.pnSearchTagsCategoriesHeader.ResumeLayout(false);
			this.xtraTabPageSearchTagsKeywords.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlSearchTagsKeywords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSearchTagsKeywords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditKeyword)).EndInit();
			this.xtraTabPageExpiredLinks.ResumeLayout(false);
			this.xtraTabPageExpiredLinks.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gbExpiredLinks)).EndInit();
			this.gbExpiredLinks.ResumeLayout(false);
			this.gbExpiredLinks.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.timeEditExpirationTime.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties)).EndInit();
			this.xtraTabPageLineBrealProperties.ResumeLayout(false);
			this.xtraTabPageLineBrealProperties.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditNote.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLineBreakFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLineBreakFontColor.Properties)).EndInit();
			this.xtraTabPageSecurity.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupBoxSecurity)).EndInit();
			this.groupBoxSecurity.ResumeLayout(false);
			this.pnSecurityUserList.ResumeLayout(false);
			this.pnSecurityUserListGrid.ResumeLayout(false);
			this.xtraTabPageWidgets.ResumeLayout(false);
			this.xtraTabPageWidgets.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxWidgets)).EndInit();
			this.groupBoxWidgets.ResumeLayout(false);
			this.groupBoxWidgets.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedWidget)).EndInit();
			this.xtraTabPageBanner.ResumeLayout(false);
			this.xtraTabPageBanner.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxBanners)).EndInit();
			this.groupBoxBanners.ResumeLayout(false);
			this.groupBoxBanners.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlBanners)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedBanner.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditBannerTextColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditBannerTextFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditBannerText.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditRequestNewCategories.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkHoverNote.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.RadioButton rbUpdated;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.RadioButton rbAttention;
        private System.Windows.Forms.RadioButton rbSell;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbCustomNote;
		private System.Windows.Forms.TextBox edCustomNote;
		private DevExpress.XtraEditors.GroupControl gbExpiredLinks;
        private System.Windows.Forms.Label laAddDateTitle;
        private System.Windows.Forms.CheckBox checkBoxEnableExpiredLinks;
        private System.Windows.Forms.CheckBox checkBoxSendEmailWhenDelete;
        private System.Windows.Forms.Label laExpireddateActions;
        private DevExpress.XtraEditors.DateEdit dateEditExpirationDate;
        private System.Windows.Forms.Label laExpirationDateTitle;
        private System.Windows.Forms.Label laAddDateValue;
        private DevExpress.XtraEditors.TimeEdit timeEditExpirationTime;
        private System.Windows.Forms.CheckBox checkBoxLabelLink;
        public DevExpress.XtraTab.XtraTabPage xtraTabPageNotes;
		public DevExpress.XtraTab.XtraTabPage xtraTabPageSearchTags;
		private DevExpress.XtraEditors.GroupControl groupBoxWidgets;
        private System.Windows.Forms.Label laAvailableWidgets;
        private System.Windows.Forms.Label laSelectedWidget;
        private System.Windows.Forms.CheckBox checkBoxEnableWidget;
		private System.Windows.Forms.PictureBox pbSelectedWidget;
        private DevExpress.XtraEditors.ColorEdit colorEditLineBreakFontColor;
		private System.Windows.Forms.Label laFontColor;
		private DevExpress.XtraEditors.GroupControl groupBoxBanners;
        private System.Windows.Forms.Label laAvailableBanners;
        private System.Windows.Forms.Label laSelectedBanner;
        private System.Windows.Forms.CheckBox checkBoxEnableBanner;
        private System.Windows.Forms.FontDialog dlgFont;
        private System.Windows.Forms.Label laFont;
        private DevExpress.XtraEditors.ButtonEdit buttonEditLineBreakFont;
        private System.Windows.Forms.Label laNote;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.MemoEdit memoEditNote;
        private System.Windows.Forms.RadioButton rbBannerAligmentRight;
        private System.Windows.Forms.RadioButton rbBannerAligmentCenter;
        private System.Windows.Forms.RadioButton rbBannerAligmentLeft;
        private System.Windows.Forms.Label laBannerAligment;
        private DevExpress.XtraEditors.MemoEdit memoEditBannerText;
        private System.Windows.Forms.CheckBox checkBoxBannerShowText;
        private DevExpress.XtraEditors.ColorEdit colorEditBannerTextColor;
		private DevExpress.XtraEditors.ButtonEdit buttonEditBannerTextFont;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSearchTagsCategories;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSearchTagsKeywords;
        private DevExpress.XtraGrid.GridControl gridControlSearchTagsKeywords;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSearchTagsKeywords;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXAddKeyWord;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSearchTagsKeywordsValue;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditKeyword;
		private DevExpress.XtraEditors.GroupControl groupBoxSecurity;
		public System.Windows.Forms.RadioButton rbSecurityAllowed;
		public System.Windows.Forms.RadioButton rbSecurityWhiteList;
		public System.Windows.Forms.RadioButton rbSecurityDenied;
		private System.Windows.Forms.Label laWidgetFileName;
		private System.Windows.Forms.Label laBannerFileName;
		private DevExpress.XtraEditors.PictureEdit pbSelectedBanner;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlBanners;
		private DevExpress.XtraEditors.Repository.PersistentRepository persistentRepository;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerSearchTagsCategories;
		public System.Windows.Forms.CheckBox ckForcePreview;
		public System.Windows.Forms.CheckBox ckSecurityShareLink;
		public DevComponents.DotNetBar.ButtonX buttonXRefreshPreview;
		public DevComponents.DotNetBar.ButtonX buttonXOpenWV;
		public DevComponents.DotNetBar.ButtonX buttonXOpenQV;
		public System.Windows.Forms.Label laAdminTools;
		public System.Windows.Forms.Panel pnAdminTools;
		private DevComponents.DotNetBar.ButtonX buttonXWipeTags;
		private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlSearchTagsCategories;
		private System.Windows.Forms.Panel pnSearchTagsCategoriesHeader;
		private DevExpress.XtraEditors.LabelControl labelControlSearchTagsCategoriesHeader;
		private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditRequestNewCategories;
		public System.Windows.Forms.CheckBox ckDoNotGeneratePreview;
		public System.Windows.Forms.RadioButton rbSecurityForbidden;
		private DevExpress.XtraEditors.GroupControl groupControlNotes;
		private DevExpress.XtraEditors.GroupControl groupControlTextFormat;
		private System.Windows.Forms.Panel pnSecurityUserList;
		private DevComponents.DotNetBar.Controls.CircularProgress circularSecurityUserListProgress;
		private DevExpress.XtraEditors.LabelControl laSecurityUserListInfo;
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
		private System.Windows.Forms.Panel pnSecurityUserListGrid;
		private DevComponents.DotNetBar.ButtonX buttonXSecurityUserListClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXSecurityUserListSelectAll;
		public System.Windows.Forms.RadioButton rbSecurityBlackList;
		public System.Windows.Forms.CheckBox ckIsUrl365;
		public DevExpress.XtraTab.XtraTabPage xtraTabPageExpiredLinks;
		public DevExpress.XtraTab.XtraTabPage xtraTabPageWidgets;
		public DevExpress.XtraTab.XtraTabPage xtraTabPageLineBrealProperties;
		public DevExpress.XtraTab.XtraTabPage xtraTabPageBanner;
		public DevExpress.XtraTab.XtraTabControl xtraTabControlSearchTags;
		public DevExpress.XtraTab.XtraTabPage xtraTabPageSecurity;
		public DevExpress.XtraTab.XtraTabControl xtraTabControl;
		public System.Windows.Forms.CheckBox ckDoNotGenerateText;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlWidgets;
		public DevExpress.XtraEditors.ButtonEdit buttonEditLinkSpecialFont;
		public System.Windows.Forms.RadioButton rbLinkBoldFormat;
		public System.Windows.Forms.RadioButton rbLinkRegularFormat;
		public System.Windows.Forms.RadioButton rbLinkSpecialFormat;
		public DevExpress.XtraEditors.ColorEdit colorEditLinkSpecialColor;
		public DevExpress.XtraEditors.TextEdit textEditLinkHoverNote;
		public System.Windows.Forms.Label laLinkHoverNote;
    }
}