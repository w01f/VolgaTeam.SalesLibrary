namespace SalesLibraries.SiteManager.PresentationClasses.QBuilder
{
	sealed partial class QPagesManagerControl
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.gridControlRecords = new DevExpress.XtraGrid.GridControl();
			this.gridViewRecords = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnPagesName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesGroup = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesUrl = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemHyperLinkEditPages = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
			this.gridColumnPagesType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesSecurityType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesDateCreate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemDateEditPages = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.gridColumnPagesDateExpiration = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditPagesActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnTotalViews = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPinCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.splitContainerControlData = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnCustomFilter = new System.Windows.Forms.Panel();
			this.pnFilterButtons = new System.Windows.Forms.Panel();
			this.buttonXLoadData = new DevComponents.DotNetBar.ButtonX();
			this.gbDate = new System.Windows.Forms.GroupBox();
			this.labelControlDateEnd = new DevExpress.XtraEditors.LabelControl();
			this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
			this.labelControlDateStart = new DevExpress.XtraEditors.LabelControl();
			this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEditPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditPages.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPagesActions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlData)).BeginInit();
			this.splitContainerControlData.SuspendLayout();
			this.pnFilterButtons.SuspendLayout();
			this.gbDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
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
			// gridControlRecords
			// 
			this.gridControlRecords.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlRecords.Location = new System.Drawing.Point(0, 0);
			this.gridControlRecords.MainView = this.gridViewRecords;
			this.gridControlRecords.Name = "gridControlRecords";
			this.gridControlRecords.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditPages,
            this.repositoryItemButtonEditPagesActions,
            this.repositoryItemHyperLinkEditPages});
			this.gridControlRecords.Size = new System.Drawing.Size(669, 483);
			this.gridControlRecords.TabIndex = 3;
			this.gridControlRecords.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRecords});
			// 
			// gridViewRecords
			// 
			this.gridViewRecords.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewRecords.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewRecords.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewRecords.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.OddRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.Preview.Options.UseFont = true;
			this.gridViewRecords.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.Row.Options.UseFont = true;
			this.gridViewRecords.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewRecords.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPagesName,
            this.gridColumnPagesGroup,
            this.gridColumnPagesUrl,
            this.gridColumnPagesType,
            this.gridColumnPagesSecurityType,
            this.gridColumnPagesDateCreate,
            this.gridColumnPagesDateExpiration,
            this.gridColumnPagesActions,
            this.gridColumnTotalViews,
            this.gridColumnPinCode});
			this.gridViewRecords.GridControl = this.gridControlRecords;
			this.gridViewRecords.Name = "gridViewRecords";
			this.gridViewRecords.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewRecords.OptionsCustomization.AllowFilter = false;
			this.gridViewRecords.OptionsCustomization.AllowGroup = false;
			this.gridViewRecords.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewRecords.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewRecords.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewRecords.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewRecords.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewRecords.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewRecords.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewRecords.OptionsView.ShowDetailButtons = false;
			this.gridViewRecords.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewRecords.OptionsView.ShowGroupPanel = false;
			this.gridViewRecords.OptionsView.ShowIndicator = false;
			this.gridViewRecords.PreviewFieldName = "DetailString";
			this.gridViewRecords.PreviewIndent = 5;
			this.gridViewRecords.RowHeight = 35;
			this.gridViewRecords.RowSeparatorHeight = 5;
			this.gridViewRecords.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnPagesDateCreate, DevExpress.Data.ColumnSortOrder.Descending)});
			// 
			// gridColumnPagesName
			// 
			this.gridColumnPagesName.Caption = "User";
			this.gridColumnPagesName.FieldName = "FullName";
			this.gridColumnPagesName.Name = "gridColumnPagesName";
			this.gridColumnPagesName.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesName.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesName.Visible = true;
			this.gridColumnPagesName.VisibleIndex = 3;
			this.gridColumnPagesName.Width = 54;
			// 
			// gridColumnPagesGroup
			// 
			this.gridColumnPagesGroup.Caption = "Group";
			this.gridColumnPagesGroup.FieldName = "groups";
			this.gridColumnPagesGroup.Name = "gridColumnPagesGroup";
			this.gridColumnPagesGroup.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesGroup.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesGroup.Visible = true;
			this.gridColumnPagesGroup.VisibleIndex = 4;
			this.gridColumnPagesGroup.Width = 38;
			// 
			// gridColumnPagesUrl
			// 
			this.gridColumnPagesUrl.Caption = "URL";
			this.gridColumnPagesUrl.ColumnEdit = this.repositoryItemHyperLinkEditPages;
			this.gridColumnPagesUrl.FieldName = "url";
			this.gridColumnPagesUrl.Name = "gridColumnPagesUrl";
			this.gridColumnPagesUrl.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnPagesUrl.Visible = true;
			this.gridColumnPagesUrl.VisibleIndex = 0;
			this.gridColumnPagesUrl.Width = 63;
			// 
			// repositoryItemHyperLinkEditPages
			// 
			this.repositoryItemHyperLinkEditPages.AutoHeight = false;
			this.repositoryItemHyperLinkEditPages.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.Url, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Copy URL to Clipboard", null, null, true)});
			this.repositoryItemHyperLinkEditPages.Name = "repositoryItemHyperLinkEditPages";
			this.repositoryItemHyperLinkEditPages.ReadOnly = true;
			this.repositoryItemHyperLinkEditPages.SingleClick = true;
			this.repositoryItemHyperLinkEditPages.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemHyperLinkEditPages_ButtonClick);
			// 
			// gridColumnPagesType
			// 
			this.gridColumnPagesType.Caption = "Type";
			this.gridColumnPagesType.FieldName = "Type";
			this.gridColumnPagesType.Name = "gridColumnPagesType";
			this.gridColumnPagesType.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesType.OptionsColumn.FixedWidth = true;
			this.gridColumnPagesType.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesType.Visible = true;
			this.gridColumnPagesType.VisibleIndex = 1;
			this.gridColumnPagesType.Width = 91;
			// 
			// gridColumnPagesSecurityType
			// 
			this.gridColumnPagesSecurityType.Caption = "Security";
			this.gridColumnPagesSecurityType.FieldName = "SecurityType";
			this.gridColumnPagesSecurityType.Name = "gridColumnPagesSecurityType";
			this.gridColumnPagesSecurityType.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesSecurityType.OptionsColumn.FixedWidth = true;
			this.gridColumnPagesSecurityType.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesSecurityType.Visible = true;
			this.gridColumnPagesSecurityType.VisibleIndex = 2;
			this.gridColumnPagesSecurityType.Width = 100;
			// 
			// gridColumnPagesDateCreate
			// 
			this.gridColumnPagesDateCreate.Caption = "Created";
			this.gridColumnPagesDateCreate.ColumnEdit = this.repositoryItemDateEditPages;
			this.gridColumnPagesDateCreate.FieldName = "CreateDate";
			this.gridColumnPagesDateCreate.Name = "gridColumnPagesDateCreate";
			this.gridColumnPagesDateCreate.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesDateCreate.OptionsColumn.FixedWidth = true;
			this.gridColumnPagesDateCreate.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesDateCreate.Visible = true;
			this.gridColumnPagesDateCreate.VisibleIndex = 5;
			this.gridColumnPagesDateCreate.Width = 93;
			// 
			// repositoryItemDateEditPages
			// 
			this.repositoryItemDateEditPages.AutoHeight = false;
			this.repositoryItemDateEditPages.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemDateEditPages.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemDateEditPages.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.repositoryItemDateEditPages.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditPages.EditFormat.FormatString = "MM/dd/yyyy";
			this.repositoryItemDateEditPages.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditPages.Name = "repositoryItemDateEditPages";
			// 
			// gridColumnPagesDateExpiration
			// 
			this.gridColumnPagesDateExpiration.Caption = "Expires";
			this.gridColumnPagesDateExpiration.ColumnEdit = this.repositoryItemDateEditPages;
			this.gridColumnPagesDateExpiration.FieldName = "ExpirationDate";
			this.gridColumnPagesDateExpiration.Name = "gridColumnPagesDateExpiration";
			this.gridColumnPagesDateExpiration.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesDateExpiration.OptionsColumn.FixedWidth = true;
			this.gridColumnPagesDateExpiration.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesDateExpiration.Visible = true;
			this.gridColumnPagesDateExpiration.VisibleIndex = 6;
			this.gridColumnPagesDateExpiration.Width = 88;
			// 
			// gridColumnPagesActions
			// 
			this.gridColumnPagesActions.Caption = "Actions";
			this.gridColumnPagesActions.ColumnEdit = this.repositoryItemButtonEditPagesActions;
			this.gridColumnPagesActions.Name = "gridColumnPagesActions";
			this.gridColumnPagesActions.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumnPagesActions.OptionsColumn.FixedWidth = true;
			this.gridColumnPagesActions.OptionsColumn.ShowCaption = false;
			this.gridColumnPagesActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnPagesActions.Visible = true;
			this.gridColumnPagesActions.VisibleIndex = 9;
			this.gridColumnPagesActions.Width = 45;
			// 
			// repositoryItemButtonEditPagesActions
			// 
			this.repositoryItemButtonEditPagesActions.AutoHeight = false;
			this.repositoryItemButtonEditPagesActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Delete record", null, null, true)});
			this.repositoryItemButtonEditPagesActions.Name = "repositoryItemButtonEditPagesActions";
			this.repositoryItemButtonEditPagesActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditPagesActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditPagesActions_ButtonClick);
			// 
			// gridColumnTotalViews
			// 
			this.gridColumnTotalViews.Caption = "Views";
			this.gridColumnTotalViews.FieldName = "TotalViews";
			this.gridColumnTotalViews.Name = "gridColumnTotalViews";
			this.gridColumnTotalViews.OptionsColumn.AllowEdit = false;
			this.gridColumnTotalViews.OptionsColumn.FixedWidth = true;
			this.gridColumnTotalViews.OptionsColumn.ReadOnly = true;
			this.gridColumnTotalViews.Visible = true;
			this.gridColumnTotalViews.VisibleIndex = 8;
			this.gridColumnTotalViews.Width = 60;
			// 
			// gridColumnPinCode
			// 
			this.gridColumnPinCode.Caption = "Pin";
			this.gridColumnPinCode.FieldName = "pinCode";
			this.gridColumnPinCode.Name = "gridColumnPinCode";
			this.gridColumnPinCode.OptionsColumn.AllowEdit = false;
			this.gridColumnPinCode.OptionsColumn.FixedWidth = true;
			this.gridColumnPinCode.OptionsColumn.ReadOnly = true;
			this.gridColumnPinCode.Visible = true;
			this.gridColumnPinCode.VisibleIndex = 7;
			this.gridColumnPinCode.Width = 60;
			// 
			// splitContainerControlData
			// 
			this.splitContainerControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlData.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlData.Name = "splitContainerControlData";
			this.splitContainerControlData.Panel1.Controls.Add(this.pnCustomFilter);
			this.splitContainerControlData.Panel1.Controls.Add(this.pnFilterButtons);
			this.splitContainerControlData.Panel1.Controls.Add(this.gbDate);
			this.splitContainerControlData.Panel1.MinSize = 230;
			this.splitContainerControlData.Panel1.Text = "Panel1";
			this.splitContainerControlData.Panel2.Controls.Add(this.gridControlRecords);
			this.splitContainerControlData.Panel2.Text = "Panel2";
			this.splitContainerControlData.Size = new System.Drawing.Size(911, 483);
			this.splitContainerControlData.TabIndex = 0;
			this.splitContainerControlData.Text = "splitContainerControl1";
			// 
			// pnCustomFilter
			// 
			this.pnCustomFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnCustomFilter.Location = new System.Drawing.Point(0, 137);
			this.pnCustomFilter.Name = "pnCustomFilter";
			this.pnCustomFilter.Size = new System.Drawing.Size(230, 346);
			this.pnCustomFilter.TabIndex = 21;
			// 
			// pnFilterButtons
			// 
			this.pnFilterButtons.Controls.Add(this.buttonXLoadData);
			this.pnFilterButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnFilterButtons.Location = new System.Drawing.Point(0, 98);
			this.pnFilterButtons.Name = "pnFilterButtons";
			this.pnFilterButtons.Size = new System.Drawing.Size(230, 39);
			this.pnFilterButtons.TabIndex = 23;
			// 
			// buttonXLoadData
			// 
			this.buttonXLoadData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLoadData.CausesValidation = false;
			this.buttonXLoadData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLoadData.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXLoadData.Location = new System.Drawing.Point(7, 5);
			this.buttonXLoadData.Name = "buttonXLoadData";
			this.buttonXLoadData.Size = new System.Drawing.Size(216, 27);
			this.buttonXLoadData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLoadData.TabIndex = 16;
			this.buttonXLoadData.Text = "Load Data";
			this.buttonXLoadData.TextColor = System.Drawing.Color.Black;
			this.buttonXLoadData.Click += new System.EventHandler(this.buttonXLoadData_Click);
			// 
			// gbDate
			// 
			this.gbDate.Controls.Add(this.labelControlDateEnd);
			this.gbDate.Controls.Add(this.dateEditEnd);
			this.gbDate.Controls.Add(this.labelControlDateStart);
			this.gbDate.Controls.Add(this.dateEditStart);
			this.gbDate.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbDate.Location = new System.Drawing.Point(0, 0);
			this.gbDate.Name = "gbDate";
			this.gbDate.Size = new System.Drawing.Size(230, 98);
			this.gbDate.TabIndex = 22;
			this.gbDate.TabStop = false;
			this.gbDate.Text = "Date range";
			// 
			// labelControlDateEnd
			// 
			this.labelControlDateEnd.Location = new System.Drawing.Point(10, 63);
			this.labelControlDateEnd.Name = "labelControlDateEnd";
			this.labelControlDateEnd.Size = new System.Drawing.Size(58, 16);
			this.labelControlDateEnd.StyleController = this.styleController;
			this.labelControlDateEnd.TabIndex = 3;
			this.labelControlDateEnd.Text = "End Date:";
			// 
			// dateEditEnd
			// 
			this.dateEditEnd.EditValue = null;
			this.dateEditEnd.Location = new System.Drawing.Point(100, 60);
			this.dateEditEnd.Name = "dateEditEnd";
			this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditEnd.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEnd.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEnd.Size = new System.Drawing.Size(123, 22);
			this.dateEditEnd.StyleController = this.styleController;
			this.dateEditEnd.TabIndex = 2;
			// 
			// labelControlDateStart
			// 
			this.labelControlDateStart.Location = new System.Drawing.Point(10, 24);
			this.labelControlDateStart.Name = "labelControlDateStart";
			this.labelControlDateStart.Size = new System.Drawing.Size(63, 16);
			this.labelControlDateStart.StyleController = this.styleController;
			this.labelControlDateStart.TabIndex = 1;
			this.labelControlDateStart.Text = "Start Date:";
			// 
			// dateEditStart
			// 
			this.dateEditStart.EditValue = null;
			this.dateEditStart.Location = new System.Drawing.Point(100, 21);
			this.dateEditStart.Name = "dateEditStart";
			this.dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditStart.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Size = new System.Drawing.Size(123, 22);
			this.dateEditStart.StyleController = this.styleController;
			this.dateEditStart.TabIndex = 0;
			// 
			// QPagesManagerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.splitContainerControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "QPagesManagerControl";
			this.Size = new System.Drawing.Size(911, 483);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlRecords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewRecords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEditPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditPages.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPagesActions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlData)).EndInit();
			this.splitContainerControlData.ResumeLayout(false);
			this.pnFilterButtons.ResumeLayout(false);
			this.gbDate.ResumeLayout(false);
			this.gbDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraGrid.GridControl gridControlRecords;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewRecords;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesName;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControlData;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesGroup;
		private System.Windows.Forms.Panel pnCustomFilter;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesUrl;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesType;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesDateCreate;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesDateExpiration;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditPages;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditPagesActions;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEditPages;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPinCode;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTotalViews;
		private System.Windows.Forms.Panel pnFilterButtons;
		private DevComponents.DotNetBar.ButtonX buttonXLoadData;
		private System.Windows.Forms.GroupBox gbDate;
		private DevExpress.XtraEditors.LabelControl labelControlDateEnd;
		private DevExpress.XtraEditors.DateEdit dateEditEnd;
		private DevExpress.XtraEditors.LabelControl labelControlDateStart;
		private DevExpress.XtraEditors.DateEdit dateEditStart;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPagesSecurityType;
	}
}
