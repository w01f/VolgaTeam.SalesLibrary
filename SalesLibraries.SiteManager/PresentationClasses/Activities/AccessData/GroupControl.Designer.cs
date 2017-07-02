namespace SalesLibraries.SiteManager.PresentationClasses.Activities.AccessData
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
			this.bandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
			this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnTotalUsersNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnActiveUsersNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.bandedGridColumnActiveUsersPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnInactiveUsersNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnInactiveUsersPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUsersNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandActive = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnActiveNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnActivePercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandInactive = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnInactiveNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnInactivePercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlData
			// 
			this.gridControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlData.Location = new System.Drawing.Point(0, 0);
			this.gridControlData.MainView = this.bandedGridView;
			this.gridControlData.Name = "gridControlData";
			this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditNumeric,
            this.repositoryItemSpinEditPercent,
            this.repositoryItemMemoEdit});
			this.gridControlData.Size = new System.Drawing.Size(898, 483);
			this.gridControlData.TabIndex = 2;
			this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView,
            this.advBandedGridViewData});
			// 
			// bandedGridView
			// 
			this.bandedGridView.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.bandedGridView.Appearance.BandPanel.Options.UseFont = true;
			this.bandedGridView.Appearance.BandPanel.Options.UseTextOptions = true;
			this.bandedGridView.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridView.Appearance.EvenRow.Options.UseFont = true;
			this.bandedGridView.Appearance.EvenRow.Options.UseTextOptions = true;
			this.bandedGridView.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridView.Appearance.FocusedCell.Options.UseFont = true;
			this.bandedGridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridView.Appearance.FocusedRow.Options.UseFont = true;
			this.bandedGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.bandedGridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.bandedGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.bandedGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridView.Appearance.OddRow.Options.UseFont = true;
			this.bandedGridView.Appearance.OddRow.Options.UseTextOptions = true;
			this.bandedGridView.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridView.Appearance.Preview.Options.UseFont = true;
			this.bandedGridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridView.Appearance.Row.Options.UseFont = true;
			this.bandedGridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridView.Appearance.SelectedRow.Options.UseFont = true;
			this.bandedGridView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bandedGridView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.bandedGridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.bandedGridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3});
			this.bandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnTotalUsersNumber,
            this.bandedGridColumnActiveUsersNumber,
            this.bandedGridColumnActiveUsersPercent,
            this.bandedGridColumnInactiveUsersNumber,
            this.bandedGridColumnInactiveUsersPercent});
			this.bandedGridView.GridControl = this.gridControlData;
			this.bandedGridView.Name = "bandedGridView";
			this.bandedGridView.OptionsBehavior.Editable = false;
			this.bandedGridView.OptionsBehavior.ReadOnly = true;
			this.bandedGridView.OptionsCustomization.AllowBandMoving = false;
			this.bandedGridView.OptionsCustomization.AllowBandResizing = false;
			this.bandedGridView.OptionsCustomization.AllowColumnMoving = false;
			this.bandedGridView.OptionsCustomization.AllowColumnResizing = false;
			this.bandedGridView.OptionsCustomization.AllowFilter = false;
			this.bandedGridView.OptionsCustomization.AllowGroup = false;
			this.bandedGridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.bandedGridView.OptionsCustomization.AllowSort = false;
			this.bandedGridView.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.bandedGridView.OptionsMenu.EnableColumnMenu = false;
			this.bandedGridView.OptionsMenu.EnableFooterMenu = false;
			this.bandedGridView.OptionsMenu.EnableGroupPanelMenu = false;
			this.bandedGridView.OptionsMenu.ShowAutoFilterRowItem = false;
			this.bandedGridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.bandedGridView.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.bandedGridView.OptionsPrint.PrintBandHeader = false;
			this.bandedGridView.OptionsPrint.PrintPreview = true;
			this.bandedGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.bandedGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.bandedGridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.bandedGridView.OptionsView.RowAutoHeight = true;
			this.bandedGridView.OptionsView.ShowBands = false;
			this.bandedGridView.OptionsView.ShowDetailButtons = false;
			this.bandedGridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.bandedGridView.OptionsView.ShowGroupPanel = false;
			this.bandedGridView.OptionsView.ShowIndicator = false;
			// 
			// gridBand1
			// 
			this.gridBand1.Columns.Add(this.bandedGridColumnTotalUsersNumber);
			this.gridBand1.MinWidth = 20;
			this.gridBand1.Name = "gridBand1";
			this.gridBand1.VisibleIndex = 0;
			this.gridBand1.Width = 120;
			// 
			// bandedGridColumnTotalUsersNumber
			// 
			this.bandedGridColumnTotalUsersNumber.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnTotalUsersNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnTotalUsersNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnTotalUsersNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnTotalUsersNumber.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnTotalUsersNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnTotalUsersNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnTotalUsersNumber.Caption = "Total Users#";
			this.bandedGridColumnTotalUsersNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.bandedGridColumnTotalUsersNumber.FieldName = "userCount";
			this.bandedGridColumnTotalUsersNumber.Name = "bandedGridColumnTotalUsersNumber";
			this.bandedGridColumnTotalUsersNumber.Visible = true;
			this.bandedGridColumnTotalUsersNumber.Width = 120;
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
			// gridBand2
			// 
			this.gridBand2.Caption = "Active";
			this.gridBand2.Columns.Add(this.bandedGridColumnActiveUsersNumber);
			this.gridBand2.Columns.Add(this.bandedGridColumnActiveUsersPercent);
			this.gridBand2.MinWidth = 20;
			this.gridBand2.Name = "gridBand2";
			this.gridBand2.VisibleIndex = 1;
			this.gridBand2.Width = 372;
			// 
			// bandedGridColumnActiveUsersNumber
			// 
			this.bandedGridColumnActiveUsersNumber.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnActiveUsersNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnActiveUsersNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnActiveUsersNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnActiveUsersNumber.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnActiveUsersNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnActiveUsersNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnActiveUsersNumber.Caption = "Active#";
			this.bandedGridColumnActiveUsersNumber.ColumnEdit = this.repositoryItemMemoEdit;
			this.bandedGridColumnActiveUsersNumber.FieldName = "ActiveNames";
			this.bandedGridColumnActiveUsersNumber.Name = "bandedGridColumnActiveUsersNumber";
			this.bandedGridColumnActiveUsersNumber.Visible = true;
			this.bandedGridColumnActiveUsersNumber.Width = 252;
			// 
			// repositoryItemMemoEdit
			// 
			this.repositoryItemMemoEdit.Name = "repositoryItemMemoEdit";
			// 
			// bandedGridColumnActiveUsersPercent
			// 
			this.bandedGridColumnActiveUsersPercent.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnActiveUsersPercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnActiveUsersPercent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnActiveUsersPercent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnActiveUsersPercent.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnActiveUsersPercent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnActiveUsersPercent.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnActiveUsersPercent.Caption = "Active%";
			this.bandedGridColumnActiveUsersPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.bandedGridColumnActiveUsersPercent.FieldName = "ActivePercent";
			this.bandedGridColumnActiveUsersPercent.Name = "bandedGridColumnActiveUsersPercent";
			this.bandedGridColumnActiveUsersPercent.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnActiveUsersPercent.Visible = true;
			this.bandedGridColumnActiveUsersPercent.Width = 120;
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
			// gridBand3
			// 
			this.gridBand3.Caption = "Inactive";
			this.gridBand3.Columns.Add(this.bandedGridColumnInactiveUsersNumber);
			this.gridBand3.Columns.Add(this.bandedGridColumnInactiveUsersPercent);
			this.gridBand3.MinWidth = 20;
			this.gridBand3.Name = "gridBand3";
			this.gridBand3.VisibleIndex = 2;
			this.gridBand3.Width = 403;
			// 
			// bandedGridColumnInactiveUsersNumber
			// 
			this.bandedGridColumnInactiveUsersNumber.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnInactiveUsersNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnInactiveUsersNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnInactiveUsersNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnInactiveUsersNumber.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnInactiveUsersNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnInactiveUsersNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnInactiveUsersNumber.Caption = "Inactive#";
			this.bandedGridColumnInactiveUsersNumber.ColumnEdit = this.repositoryItemMemoEdit;
			this.bandedGridColumnInactiveUsersNumber.FieldName = "InactiveNames";
			this.bandedGridColumnInactiveUsersNumber.Name = "bandedGridColumnInactiveUsersNumber";
			this.bandedGridColumnInactiveUsersNumber.Visible = true;
			this.bandedGridColumnInactiveUsersNumber.Width = 283;
			// 
			// bandedGridColumnInactiveUsersPercent
			// 
			this.bandedGridColumnInactiveUsersPercent.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnInactiveUsersPercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnInactiveUsersPercent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnInactiveUsersPercent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnInactiveUsersPercent.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnInactiveUsersPercent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnInactiveUsersPercent.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.bandedGridColumnInactiveUsersPercent.Caption = "Inactive%";
			this.bandedGridColumnInactiveUsersPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.bandedGridColumnInactiveUsersPercent.FieldName = "InactivePercent";
			this.bandedGridColumnInactiveUsersPercent.Name = "bandedGridColumnInactiveUsersPercent";
			this.bandedGridColumnInactiveUsersPercent.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnInactiveUsersPercent.Visible = true;
			this.bandedGridColumnInactiveUsersPercent.Width = 120;
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
            this.gridBandActive,
            this.gridBandInactive});
			this.advBandedGridViewData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnUsersNumber,
            this.gridColumnActiveNumber,
            this.gridColumnActivePercent,
            this.gridColumnInactiveNumber,
            this.gridColumnInactivePercent});
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
			this.advBandedGridViewData.OptionsPrint.PrintBandHeader = false;
			this.advBandedGridViewData.OptionsPrint.PrintDetails = true;
			this.advBandedGridViewData.OptionsPrint.PrintPreview = true;
			this.advBandedGridViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridViewData.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridViewData.OptionsView.AutoCalcPreviewLineCount = true;
			this.advBandedGridViewData.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridViewData.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridViewData.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridViewData.OptionsView.ShowBands = false;
			this.advBandedGridViewData.OptionsView.ShowDetailButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupPanel = false;
			this.advBandedGridViewData.OptionsView.ShowIndicator = false;
			this.advBandedGridViewData.PreviewFieldName = "Details";
			this.advBandedGridViewData.PreviewIndent = 5;
			this.advBandedGridViewData.RowSeparatorHeight = 10;
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnUsersNumber);
			this.gridBandMain.MinWidth = 20;
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 120;
			// 
			// gridColumnUsersNumber
			// 
			this.gridColumnUsersNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUsersNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnUsersNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnUsersNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnUsersNumber.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnUsersNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnUsersNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnUsersNumber.Caption = "Total Users#";
			this.gridColumnUsersNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUsersNumber.FieldName = "userCount";
			this.gridColumnUsersNumber.Name = "gridColumnUsersNumber";
			this.gridColumnUsersNumber.OptionsColumn.FixedWidth = true;
			this.gridColumnUsersNumber.Visible = true;
			this.gridColumnUsersNumber.Width = 120;
			// 
			// gridBandActive
			// 
			this.gridBandActive.Caption = "Active";
			this.gridBandActive.Columns.Add(this.gridColumnActiveNumber);
			this.gridBandActive.Columns.Add(this.gridColumnActivePercent);
			this.gridBandActive.MinWidth = 20;
			this.gridBandActive.Name = "gridBandActive";
			this.gridBandActive.VisibleIndex = 1;
			this.gridBandActive.Width = 372;
			// 
			// gridColumnActiveNumber
			// 
			this.gridColumnActiveNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnActiveNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnActiveNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnActiveNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnActiveNumber.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnActiveNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnActiveNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnActiveNumber.Caption = "Active#";
			this.gridColumnActiveNumber.ColumnEdit = this.repositoryItemMemoEdit;
			this.gridColumnActiveNumber.FieldName = "ActiveNames";
			this.gridColumnActiveNumber.Name = "gridColumnActiveNumber";
			this.gridColumnActiveNumber.Visible = true;
			this.gridColumnActiveNumber.Width = 252;
			// 
			// gridColumnActivePercent
			// 
			this.gridColumnActivePercent.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnActivePercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnActivePercent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnActivePercent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnActivePercent.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnActivePercent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnActivePercent.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnActivePercent.Caption = "Active%";
			this.gridColumnActivePercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnActivePercent.FieldName = "ActivePercent";
			this.gridColumnActivePercent.Name = "gridColumnActivePercent";
			this.gridColumnActivePercent.OptionsColumn.FixedWidth = true;
			this.gridColumnActivePercent.Visible = true;
			this.gridColumnActivePercent.Width = 120;
			// 
			// gridBandInactive
			// 
			this.gridBandInactive.Caption = "Inactive";
			this.gridBandInactive.Columns.Add(this.gridColumnInactiveNumber);
			this.gridBandInactive.Columns.Add(this.gridColumnInactivePercent);
			this.gridBandInactive.MinWidth = 20;
			this.gridBandInactive.Name = "gridBandInactive";
			this.gridBandInactive.VisibleIndex = 2;
			this.gridBandInactive.Width = 403;
			// 
			// gridColumnInactiveNumber
			// 
			this.gridColumnInactiveNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnInactiveNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnInactiveNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnInactiveNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnInactiveNumber.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnInactiveNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnInactiveNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnInactiveNumber.Caption = "Inactive#";
			this.gridColumnInactiveNumber.ColumnEdit = this.repositoryItemMemoEdit;
			this.gridColumnInactiveNumber.FieldName = "InactiveNames";
			this.gridColumnInactiveNumber.Name = "gridColumnInactiveNumber";
			this.gridColumnInactiveNumber.Visible = true;
			this.gridColumnInactiveNumber.Width = 283;
			// 
			// gridColumnInactivePercent
			// 
			this.gridColumnInactivePercent.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnInactivePercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnInactivePercent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnInactivePercent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnInactivePercent.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnInactivePercent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnInactivePercent.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnInactivePercent.Caption = "Inactive%";
			this.gridColumnInactivePercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnInactivePercent.FieldName = "InactivePercent";
			this.gridColumnInactivePercent.Name = "gridColumnInactivePercent";
			this.gridColumnInactivePercent.OptionsColumn.FixedWidth = true;
			this.gridColumnInactivePercent.Visible = true;
			this.gridColumnInactivePercent.Width = 120;
			// 
			// GroupControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "AccessGroupReportControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewData;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUsersNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnActiveNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnActivePercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnInactiveNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnInactivePercent;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandInactive;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandActive;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTotalUsersNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnActiveUsersNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnActiveUsersPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnInactiveUsersNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnInactiveUsersPercent;
	}
}
