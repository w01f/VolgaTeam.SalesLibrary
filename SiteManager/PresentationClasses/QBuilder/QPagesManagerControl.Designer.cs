namespace SalesDepot.SiteManager.PresentationClasses.QBuilder
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.gridControlRecords = new DevExpress.XtraGrid.GridControl();
			this.gridViewRecords = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnPagesName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesGroup = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesUrl = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemHyperLinkEditPages = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
			this.gridColumnPagesType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesDateCreate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemDateEditPages = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.gridColumnPagesDateExpiration = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPagesActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditPagesActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnPinCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.splitContainerControlData = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnCustomFilter = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEditPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditPages.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPagesActions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlData)).BeginInit();
			this.splitContainerControlData.SuspendLayout();
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
            this.gridColumnPagesDateCreate,
            this.gridColumnPagesDateExpiration,
            this.gridColumnPagesActions,
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
			// 
			// gridColumnPagesName
			// 
			this.gridColumnPagesName.Caption = "User";
			this.gridColumnPagesName.FieldName = "FullName";
			this.gridColumnPagesName.Name = "gridColumnPagesName";
			this.gridColumnPagesName.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesName.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesName.Visible = true;
			this.gridColumnPagesName.VisibleIndex = 2;
			this.gridColumnPagesName.Width = 110;
			// 
			// gridColumnPagesGroup
			// 
			this.gridColumnPagesGroup.Caption = "Group";
			this.gridColumnPagesGroup.FieldName = "groups";
			this.gridColumnPagesGroup.Name = "gridColumnPagesGroup";
			this.gridColumnPagesGroup.OptionsColumn.AllowEdit = false;
			this.gridColumnPagesGroup.OptionsColumn.ReadOnly = true;
			this.gridColumnPagesGroup.Visible = true;
			this.gridColumnPagesGroup.VisibleIndex = 3;
			this.gridColumnPagesGroup.Width = 76;
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
			this.gridColumnPagesUrl.Width = 128;
			// 
			// repositoryItemHyperLinkEditPages
			// 
			this.repositoryItemHyperLinkEditPages.AutoHeight = false;
			this.repositoryItemHyperLinkEditPages.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesDepot.SiteManager.Properties.Resources.Url, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Copy URL to Clipboard", null, null, true)});
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
			this.gridColumnPagesDateCreate.VisibleIndex = 4;
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
			this.gridColumnPagesDateExpiration.VisibleIndex = 5;
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
			this.gridColumnPagesActions.VisibleIndex = 7;
			this.gridColumnPagesActions.Width = 45;
			// 
			// repositoryItemButtonEditPagesActions
			// 
			this.repositoryItemButtonEditPagesActions.AutoHeight = false;
			this.repositoryItemButtonEditPagesActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesDepot.SiteManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Delete record", null, null, true)});
			this.repositoryItemButtonEditPagesActions.Name = "repositoryItemButtonEditPagesActions";
			this.repositoryItemButtonEditPagesActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditPagesActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditPagesActions_ButtonClick);
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
			this.gridColumnPinCode.VisibleIndex = 6;
			this.gridColumnPinCode.Width = 60;
			// 
			// splitContainerControlData
			// 
			this.splitContainerControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlData.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlData.Name = "splitContainerControlData";
			this.splitContainerControlData.Panel1.Controls.Add(this.pnCustomFilter);
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
			this.pnCustomFilter.Location = new System.Drawing.Point(0, 0);
			this.pnCustomFilter.Name = "pnCustomFilter";
			this.pnCustomFilter.Size = new System.Drawing.Size(230, 483);
			this.pnCustomFilter.TabIndex = 21;
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
    }
}
