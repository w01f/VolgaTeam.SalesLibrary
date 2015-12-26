namespace SalesLibraries.SiteManager.PresentationClasses.Activities.MainData
{
	public partial class TotalControl
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
			this.gridBandLogin = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupLoginNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnGroupLoginPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridBandDocs = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupDocsNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupDocsPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandVideos = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupVideosNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupVideosPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandTotal = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupTotalPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
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
			this.advBandedGridViewData.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridViewData.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.FooterPanel.Options.UseTextOptions = true;
			this.advBandedGridViewData.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
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
            this.gridColumnGroupLoginNumber,
            this.gridColumnGroupLoginPercent,
            this.gridColumnGroupDocsNumber,
            this.gridColumnGroupDocsPercent,
            this.gridColumnGroupVideosNumber,
            this.gridColumnGroupVideosPercent,
            this.gridColumnGroupTotalNumber,
            this.gridColumnGroupTotalPercent});
			this.advBandedGridViewData.GridControl = this.gridControlData;
			this.advBandedGridViewData.Name = "advBandedGridViewData";
			this.advBandedGridViewData.OptionsBehavior.Editable = false;
			this.advBandedGridViewData.OptionsBehavior.ReadOnly = true;
			this.advBandedGridViewData.OptionsCustomization.AllowBandMoving = false;
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
			this.advBandedGridViewData.OptionsView.ShowFooter = true;
			this.advBandedGridViewData.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupPanel = false;
			this.advBandedGridViewData.OptionsView.ShowIndicator = false;
			this.advBandedGridViewData.PreviewIndent = 5;
			this.advBandedGridViewData.RowHeight = 35;
			this.advBandedGridViewData.RowSeparatorHeight = 10;
			this.advBandedGridViewData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnGroupTotalNumber, DevExpress.Data.ColumnSortOrder.Descending)});
			this.advBandedGridViewData.CustomDrawRowFooterCell += new DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventHandler(this.advBandedGridViewData_CustomDrawRowFooterCell);
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnName);
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 1166;
			// 
			// gridColumnName
			// 
			this.gridColumnName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnName.Caption = "Name";
			this.gridColumnName.FieldName = "name";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "name", "All Groups:")});
			this.gridColumnName.Visible = true;
			this.gridColumnName.Width = 1166;
			// 
			// gridBandLogin
			// 
			this.gridBandLogin.Caption = "Login";
			this.gridBandLogin.Columns.Add(this.gridColumnGroupLoginNumber);
			this.gridBandLogin.Columns.Add(this.gridColumnGroupLoginPercent);
			this.gridBandLogin.Name = "gridBandLogin";
			this.gridBandLogin.OptionsBand.FixedWidth = true;
			this.gridBandLogin.VisibleIndex = 1;
			this.gridBandLogin.Width = 145;
			// 
			// gridColumnGroupLoginNumber
			// 
			this.gridColumnGroupLoginNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupLoginNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupLoginNumber.Caption = "#";
			this.gridColumnGroupLoginNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupLoginNumber.FieldName = "logins";
			this.gridColumnGroupLoginNumber.Name = "gridColumnGroupLoginNumber";
			this.gridColumnGroupLoginNumber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "logins", "{0:#,##0}")});
			this.gridColumnGroupLoginNumber.Visible = true;
			this.gridColumnGroupLoginNumber.Width = 68;
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
			// gridColumnGroupLoginPercent
			// 
			this.gridColumnGroupLoginPercent.Caption = "%";
			this.gridColumnGroupLoginPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupLoginPercent.FieldName = "LoginsPercent";
			this.gridColumnGroupLoginPercent.Name = "gridColumnGroupLoginPercent";
			this.gridColumnGroupLoginPercent.Visible = true;
			this.gridColumnGroupLoginPercent.Width = 77;
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
			// gridBandDocs
			// 
			this.gridBandDocs.Caption = "Doc";
			this.gridBandDocs.Columns.Add(this.gridColumnGroupDocsNumber);
			this.gridBandDocs.Columns.Add(this.gridColumnGroupDocsPercent);
			this.gridBandDocs.Name = "gridBandDocs";
			this.gridBandDocs.OptionsBand.FixedWidth = true;
			this.gridBandDocs.VisibleIndex = 2;
			this.gridBandDocs.Width = 145;
			// 
			// gridColumnGroupDocsNumber
			// 
			this.gridColumnGroupDocsNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupDocsNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupDocsNumber.Caption = "#";
			this.gridColumnGroupDocsNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupDocsNumber.FieldName = "docs";
			this.gridColumnGroupDocsNumber.Name = "gridColumnGroupDocsNumber";
			this.gridColumnGroupDocsNumber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "docs", "{0:#,##0}")});
			this.gridColumnGroupDocsNumber.Visible = true;
			this.gridColumnGroupDocsNumber.Width = 72;
			// 
			// gridColumnGroupDocsPercent
			// 
			this.gridColumnGroupDocsPercent.Caption = "%";
			this.gridColumnGroupDocsPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupDocsPercent.FieldName = "DocsPercent";
			this.gridColumnGroupDocsPercent.Name = "gridColumnGroupDocsPercent";
			this.gridColumnGroupDocsPercent.Visible = true;
			this.gridColumnGroupDocsPercent.Width = 73;
			// 
			// gridBandVideos
			// 
			this.gridBandVideos.Caption = "Video";
			this.gridBandVideos.Columns.Add(this.gridColumnGroupVideosNumber);
			this.gridBandVideos.Columns.Add(this.gridColumnGroupVideosPercent);
			this.gridBandVideos.Name = "gridBandVideos";
			this.gridBandVideos.OptionsBand.FixedWidth = true;
			this.gridBandVideos.VisibleIndex = 3;
			this.gridBandVideos.Width = 145;
			// 
			// gridColumnGroupVideosNumber
			// 
			this.gridColumnGroupVideosNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupVideosNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupVideosNumber.Caption = "#";
			this.gridColumnGroupVideosNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupVideosNumber.FieldName = "videos";
			this.gridColumnGroupVideosNumber.Name = "gridColumnGroupVideosNumber";
			this.gridColumnGroupVideosNumber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "videos", "{0:#,##0}")});
			this.gridColumnGroupVideosNumber.Visible = true;
			this.gridColumnGroupVideosNumber.Width = 70;
			// 
			// gridColumnGroupVideosPercent
			// 
			this.gridColumnGroupVideosPercent.Caption = "%";
			this.gridColumnGroupVideosPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupVideosPercent.FieldName = "VideosPercent";
			this.gridColumnGroupVideosPercent.Name = "gridColumnGroupVideosPercent";
			this.gridColumnGroupVideosPercent.Visible = true;
			// 
			// gridBandTotal
			// 
			this.gridBandTotal.Caption = "Site Activity";
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalNumber);
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalPercent);
			this.gridBandTotal.Name = "gridBandTotal";
			this.gridBandTotal.OptionsBand.FixedWidth = true;
			this.gridBandTotal.VisibleIndex = 4;
			this.gridBandTotal.Width = 145;
			// 
			// gridColumnGroupTotalNumber
			// 
			this.gridColumnGroupTotalNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupTotalNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupTotalNumber.Caption = "#";
			this.gridColumnGroupTotalNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupTotalNumber.FieldName = "totals";
			this.gridColumnGroupTotalNumber.Name = "gridColumnGroupTotalNumber";
			this.gridColumnGroupTotalNumber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "totals", "{0:#,##0}")});
			this.gridColumnGroupTotalNumber.Visible = true;
			this.gridColumnGroupTotalNumber.Width = 71;
			// 
			// gridColumnGroupTotalPercent
			// 
			this.gridColumnGroupTotalPercent.Caption = "%";
			this.gridColumnGroupTotalPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupTotalPercent.FieldName = "TotalPercent";
			this.gridColumnGroupTotalPercent.Name = "gridColumnGroupTotalPercent";
			this.gridColumnGroupTotalPercent.Visible = true;
			this.gridColumnGroupTotalPercent.Width = 74;
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
			// TotalControl
			// 
			this.Appearance.PageClient.Options.UseBackColor = true;
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
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLoginNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLoginPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupDocsNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupDocsPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupVideosNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupVideosPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalPercent;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLogin;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandDocs;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandVideos;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandTotal;
    }
}
