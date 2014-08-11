namespace SalesDepot.SiteManager.PresentationClasses.Activities.MainData
{
	public partial class GroupControl
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
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandLogin = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUserLoginNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnUserLoginPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnGroupLoginNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandDocs = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUserDocsNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnUserDocsPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupDocsNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandVideos = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUserVideosNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnUserVideosPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupVideosNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandTotal = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUserTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnUserTotalPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemDateEditDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.CalendarTimeProperties)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlData
			// 
			this.gridControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlData.Location = new System.Drawing.Point(0, 0);
			this.gridControlData.MainView = this.advBandedGridViewData;
			this.gridControlData.Name = "gridControlData";
			this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditDate,
            this.repositoryItemSpinEditNumeric,
            this.repositoryItemSpinEditPercent});
			this.gridControlData.Size = new System.Drawing.Size(898, 483);
			this.gridControlData.TabIndex = 2;
			this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridViewData});
			// 
			// advBandedGridViewData
			// 
			this.advBandedGridViewData.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridViewData.Appearance.BandPanel.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridViewData.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewData.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.EvenRow.Options.UseTextOptions = true;
			this.advBandedGridViewData.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewData.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridViewData.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridViewData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewData.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.OddRow.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.OddRow.Options.UseTextOptions = true;
			this.advBandedGridViewData.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewData.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.Preview.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.Row.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.advBandedGridViewData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandMain,
            this.gridBandLogin,
            this.gridBandDocs,
            this.gridBandVideos,
            this.gridBandTotal});
			this.advBandedGridViewData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.advBandedGridViewData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnName,
            this.gridColumnGroup,
            this.gridColumnUserLoginNumber,
            this.gridColumnUserLoginPercent,
            this.gridColumnGroupLoginNumber,
            this.gridColumnUserDocsNumber,
            this.gridColumnUserDocsPercent,
            this.gridColumnGroupDocsNumber,
            this.gridColumnUserVideosNumber,
            this.gridColumnUserVideosPercent,
            this.gridColumnGroupVideosNumber,
            this.gridColumnUserTotalNumber,
            this.gridColumnUserTotalPercent,
            this.gridColumnGroupTotalNumber});
			this.advBandedGridViewData.GridControl = this.gridControlData;
			this.advBandedGridViewData.Name = "advBandedGridViewData";
			this.advBandedGridViewData.OptionsBehavior.Editable = false;
			this.advBandedGridViewData.OptionsBehavior.ReadOnly = true;
			this.advBandedGridViewData.OptionsCustomization.AllowBandMoving = false;
			this.advBandedGridViewData.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridViewData.OptionsCustomization.AllowFilter = false;
			this.advBandedGridViewData.OptionsCustomization.AllowGroup = false;
			this.advBandedGridViewData.OptionsCustomization.AllowQuickHideColumns = false;
			this.advBandedGridViewData.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridViewData.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridViewData.OptionsMenu.EnableFooterMenu = false;
			this.advBandedGridViewData.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridViewData.OptionsMenu.ShowAutoFilterRowItem = false;
			this.advBandedGridViewData.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.advBandedGridViewData.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.advBandedGridViewData.OptionsPrint.PrintPreview = true;
			this.advBandedGridViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridViewData.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridViewData.OptionsView.AutoCalcPreviewLineCount = true;
			this.advBandedGridViewData.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridViewData.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridViewData.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridViewData.OptionsView.ShowDetailButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupPanel = false;
			this.advBandedGridViewData.OptionsView.ShowIndicator = false;
			this.advBandedGridViewData.PreviewIndent = 5;
			this.advBandedGridViewData.RowHeight = 35;
			this.advBandedGridViewData.RowSeparatorHeight = 10;
			this.advBandedGridViewData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnUserTotalNumber, DevExpress.Data.ColumnSortOrder.Descending)});
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnName);
			this.gridBandMain.Columns.Add(this.gridColumnGroup);
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 336;
			// 
			// gridColumnName
			// 
			this.gridColumnName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnName.Caption = "Name";
			this.gridColumnName.FieldName = "FullName";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.Visible = true;
			this.gridColumnName.Width = 336;
			// 
			// gridColumnGroup
			// 
			this.gridColumnGroup.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnGroup.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnGroup.Caption = "Group";
			this.gridColumnGroup.FieldName = "GroupName";
			this.gridColumnGroup.Name = "gridColumnGroup";
			this.gridColumnGroup.RowIndex = 1;
			this.gridColumnGroup.Visible = true;
			this.gridColumnGroup.Width = 336;
			// 
			// gridBandLogin
			// 
			this.gridBandLogin.Caption = "Login";
			this.gridBandLogin.Columns.Add(this.gridColumnUserLoginNumber);
			this.gridBandLogin.Columns.Add(this.gridColumnUserLoginPercent);
			this.gridBandLogin.Columns.Add(this.gridColumnGroupLoginNumber);
			this.gridBandLogin.Name = "gridBandLogin";
			this.gridBandLogin.OptionsBand.FixedWidth = true;
			this.gridBandLogin.VisibleIndex = 1;
			this.gridBandLogin.Width = 143;
			// 
			// gridColumnUserLoginNumber
			// 
			this.gridColumnUserLoginNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserLoginNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserLoginNumber.Caption = "#";
			this.gridColumnUserLoginNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserLoginNumber.FieldName = "userLogins";
			this.gridColumnUserLoginNumber.Name = "gridColumnUserLoginNumber";
			this.gridColumnUserLoginNumber.Visible = true;
			this.gridColumnUserLoginNumber.Width = 143;
			// 
			// repositoryItemSpinEditNumeric
			// 
			this.repositoryItemSpinEditNumeric.AutoHeight = false;
			this.repositoryItemSpinEditNumeric.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemSpinEditNumeric.DisplayFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditNumeric.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditNumeric.EditFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditNumeric.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditNumeric.Name = "repositoryItemSpinEditNumeric";
			this.repositoryItemSpinEditNumeric.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// gridColumnUserLoginPercent
			// 
			this.gridColumnUserLoginPercent.Caption = "%";
			this.gridColumnUserLoginPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnUserLoginPercent.FieldName = "LoginsPercent";
			this.gridColumnUserLoginPercent.Name = "gridColumnUserLoginPercent";
			this.gridColumnUserLoginPercent.RowIndex = 1;
			this.gridColumnUserLoginPercent.Visible = true;
			this.gridColumnUserLoginPercent.Width = 64;
			// 
			// repositoryItemSpinEditPercent
			// 
			this.repositoryItemSpinEditPercent.AutoHeight = false;
			this.repositoryItemSpinEditPercent.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemSpinEditPercent.DisplayFormat.FormatString = "#0.##%";
			this.repositoryItemSpinEditPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditPercent.EditFormat.FormatString = "#0.##%";
			this.repositoryItemSpinEditPercent.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditPercent.Name = "repositoryItemSpinEditPercent";
			this.repositoryItemSpinEditPercent.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// gridColumnGroupLoginNumber
			// 
			this.gridColumnGroupLoginNumber.Caption = "Group#";
			this.gridColumnGroupLoginNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupLoginNumber.FieldName = "groupLogins";
			this.gridColumnGroupLoginNumber.Name = "gridColumnGroupLoginNumber";
			this.gridColumnGroupLoginNumber.RowIndex = 1;
			this.gridColumnGroupLoginNumber.Visible = true;
			this.gridColumnGroupLoginNumber.Width = 79;
			// 
			// gridBandDocs
			// 
			this.gridBandDocs.Caption = "Doc";
			this.gridBandDocs.Columns.Add(this.gridColumnUserDocsNumber);
			this.gridBandDocs.Columns.Add(this.gridColumnUserDocsPercent);
			this.gridBandDocs.Columns.Add(this.gridColumnGroupDocsNumber);
			this.gridBandDocs.Name = "gridBandDocs";
			this.gridBandDocs.OptionsBand.FixedWidth = true;
			this.gridBandDocs.VisibleIndex = 2;
			this.gridBandDocs.Width = 136;
			// 
			// gridColumnUserDocsNumber
			// 
			this.gridColumnUserDocsNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserDocsNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserDocsNumber.Caption = "#";
			this.gridColumnUserDocsNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserDocsNumber.FieldName = "userDocs";
			this.gridColumnUserDocsNumber.Name = "gridColumnUserDocsNumber";
			this.gridColumnUserDocsNumber.Visible = true;
			this.gridColumnUserDocsNumber.Width = 136;
			// 
			// gridColumnUserDocsPercent
			// 
			this.gridColumnUserDocsPercent.Caption = "%";
			this.gridColumnUserDocsPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnUserDocsPercent.FieldName = "DocsPercent";
			this.gridColumnUserDocsPercent.Name = "gridColumnUserDocsPercent";
			this.gridColumnUserDocsPercent.RowIndex = 1;
			this.gridColumnUserDocsPercent.Visible = true;
			this.gridColumnUserDocsPercent.Width = 62;
			// 
			// gridColumnGroupDocsNumber
			// 
			this.gridColumnGroupDocsNumber.Caption = "Group#";
			this.gridColumnGroupDocsNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupDocsNumber.FieldName = "groupDocs";
			this.gridColumnGroupDocsNumber.Name = "gridColumnGroupDocsNumber";
			this.gridColumnGroupDocsNumber.RowIndex = 1;
			this.gridColumnGroupDocsNumber.Visible = true;
			this.gridColumnGroupDocsNumber.Width = 74;
			// 
			// gridBandVideos
			// 
			this.gridBandVideos.Caption = "Video";
			this.gridBandVideos.Columns.Add(this.gridColumnUserVideosNumber);
			this.gridBandVideos.Columns.Add(this.gridColumnUserVideosPercent);
			this.gridBandVideos.Columns.Add(this.gridColumnGroupVideosNumber);
			this.gridBandVideos.Name = "gridBandVideos";
			this.gridBandVideos.OptionsBand.FixedWidth = true;
			this.gridBandVideos.VisibleIndex = 3;
			this.gridBandVideos.Width = 144;
			// 
			// gridColumnUserVideosNumber
			// 
			this.gridColumnUserVideosNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserVideosNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserVideosNumber.Caption = "#";
			this.gridColumnUserVideosNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserVideosNumber.FieldName = "userVideos";
			this.gridColumnUserVideosNumber.Name = "gridColumnUserVideosNumber";
			this.gridColumnUserVideosNumber.Visible = true;
			this.gridColumnUserVideosNumber.Width = 144;
			// 
			// gridColumnUserVideosPercent
			// 
			this.gridColumnUserVideosPercent.Caption = "%";
			this.gridColumnUserVideosPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnUserVideosPercent.FieldName = "VideosPercent";
			this.gridColumnUserVideosPercent.Name = "gridColumnUserVideosPercent";
			this.gridColumnUserVideosPercent.RowIndex = 1;
			this.gridColumnUserVideosPercent.Visible = true;
			this.gridColumnUserVideosPercent.Width = 71;
			// 
			// gridColumnGroupVideosNumber
			// 
			this.gridColumnGroupVideosNumber.Caption = "Group#";
			this.gridColumnGroupVideosNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupVideosNumber.FieldName = "groupVideos";
			this.gridColumnGroupVideosNumber.Name = "gridColumnGroupVideosNumber";
			this.gridColumnGroupVideosNumber.RowIndex = 1;
			this.gridColumnGroupVideosNumber.Visible = true;
			this.gridColumnGroupVideosNumber.Width = 73;
			// 
			// gridBandTotal
			// 
			this.gridBandTotal.Caption = "Site Activity";
			this.gridBandTotal.Columns.Add(this.gridColumnUserTotalNumber);
			this.gridBandTotal.Columns.Add(this.gridColumnUserTotalPercent);
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalNumber);
			this.gridBandTotal.Name = "gridBandTotal";
			this.gridBandTotal.OptionsBand.FixedWidth = true;
			this.gridBandTotal.VisibleIndex = 4;
			this.gridBandTotal.Width = 135;
			// 
			// gridColumnUserTotalNumber
			// 
			this.gridColumnUserTotalNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserTotalNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserTotalNumber.Caption = "#";
			this.gridColumnUserTotalNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserTotalNumber.FieldName = "userTotal";
			this.gridColumnUserTotalNumber.Name = "gridColumnUserTotalNumber";
			this.gridColumnUserTotalNumber.Visible = true;
			this.gridColumnUserTotalNumber.Width = 135;
			// 
			// gridColumnUserTotalPercent
			// 
			this.gridColumnUserTotalPercent.Caption = "%";
			this.gridColumnUserTotalPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnUserTotalPercent.FieldName = "TotalPercent";
			this.gridColumnUserTotalPercent.Name = "gridColumnUserTotalPercent";
			this.gridColumnUserTotalPercent.RowIndex = 1;
			this.gridColumnUserTotalPercent.Visible = true;
			this.gridColumnUserTotalPercent.Width = 69;
			// 
			// gridColumnGroupTotalNumber
			// 
			this.gridColumnGroupTotalNumber.Caption = "Group#";
			this.gridColumnGroupTotalNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupTotalNumber.FieldName = "groupTotal";
			this.gridColumnGroupTotalNumber.Name = "gridColumnGroupTotalNumber";
			this.gridColumnGroupTotalNumber.RowIndex = 1;
			this.gridColumnGroupTotalNumber.Visible = true;
			this.gridColumnGroupTotalNumber.Width = 66;
			// 
			// repositoryItemDateEditDate
			// 
			this.repositoryItemDateEditDate.AutoHeight = false;
			this.repositoryItemDateEditDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemDateEditDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemDateEditDate.DisplayFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.EditFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.Name = "repositoryItemDateEditDate";
			// 
			// GroupControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDate;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewData;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroup;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserLoginNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserLoginPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLoginNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserDocsNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserDocsPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupDocsNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserVideosNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserVideosPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupVideosNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserTotalNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserTotalPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalNumber;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLogin;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandDocs;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandVideos;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandTotal;
    }
}
