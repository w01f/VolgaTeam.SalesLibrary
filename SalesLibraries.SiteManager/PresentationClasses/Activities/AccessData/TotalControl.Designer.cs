namespace SalesLibraries.SiteManager.PresentationClasses.Activities.AccessData
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
			this.bandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUsersNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.gridColumnActiveNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnActivePercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnInactiveNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnInactivePercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).BeginInit();
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
            this.bandedGridView});
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
			this.bandedGridView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
			this.bandedGridView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.bandedGridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.bandedGridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandMain});
			this.bandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnUsersNumber,
            this.gridColumnActiveNumber,
            this.gridColumnActivePercent,
            this.gridColumnInactiveNumber,
            this.gridColumnInactivePercent});
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
			this.bandedGridView.OptionsPrint.PrintHorzLines = false;
			this.bandedGridView.OptionsPrint.PrintPreview = true;
			this.bandedGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.bandedGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.bandedGridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.bandedGridView.OptionsView.RowAutoHeight = true;
			this.bandedGridView.OptionsView.ShowBands = false;
			this.bandedGridView.OptionsView.ShowDetailButtons = false;
			this.bandedGridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.bandedGridView.OptionsView.ShowGroupPanel = false;
			this.bandedGridView.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
			this.bandedGridView.OptionsView.ShowIndicator = false;
			this.bandedGridView.RowHeight = 25;
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnUsersNumber);
			this.gridBandMain.Columns.Add(this.gridColumnActiveNumber);
			this.gridBandMain.Columns.Add(this.gridColumnActivePercent);
			this.gridBandMain.Columns.Add(this.gridColumnInactiveNumber);
			this.gridBandMain.Columns.Add(this.gridColumnInactivePercent);
			this.gridBandMain.MinWidth = 20;
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 644;
			// 
			// gridColumnUsersNumber
			// 
			this.gridColumnUsersNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUsersNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnUsersNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnUsersNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnUsersNumber.Caption = "Total Users#";
			this.gridColumnUsersNumber.ColumnEdit = this.repositoryItemMemoEdit;
			this.gridColumnUsersNumber.FieldName = "GroupHeader";
			this.gridColumnUsersNumber.Name = "gridColumnUsersNumber";
			this.gridColumnUsersNumber.Visible = true;
			this.gridColumnUsersNumber.Width = 151;
			// 
			// repositoryItemMemoEdit
			// 
			this.repositoryItemMemoEdit.Name = "repositoryItemMemoEdit";
			// 
			// gridColumnActiveNumber
			// 
			this.gridColumnActiveNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnActiveNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnActiveNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnActiveNumber.Caption = "Active#";
			this.gridColumnActiveNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnActiveNumber.FieldName = "AllActive";
			this.gridColumnActiveNumber.Name = "gridColumnActiveNumber";
			this.gridColumnActiveNumber.Visible = true;
			this.gridColumnActiveNumber.Width = 130;
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
			// gridColumnActivePercent
			// 
			this.gridColumnActivePercent.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnActivePercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnActivePercent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnActivePercent.Caption = "Active%";
			this.gridColumnActivePercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnActivePercent.FieldName = "AllActivePercent";
			this.gridColumnActivePercent.Name = "gridColumnActivePercent";
			this.gridColumnActivePercent.Visible = true;
			this.gridColumnActivePercent.Width = 126;
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
			// gridColumnInactiveNumber
			// 
			this.gridColumnInactiveNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnInactiveNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnInactiveNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnInactiveNumber.Caption = "Inactive#";
			this.gridColumnInactiveNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnInactiveNumber.FieldName = "AllInactive";
			this.gridColumnInactiveNumber.Name = "gridColumnInactiveNumber";
			this.gridColumnInactiveNumber.Visible = true;
			this.gridColumnInactiveNumber.Width = 118;
			// 
			// gridColumnInactivePercent
			// 
			this.gridColumnInactivePercent.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnInactivePercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnInactivePercent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridColumnInactivePercent.Caption = "Inactive%";
			this.gridColumnInactivePercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnInactivePercent.FieldName = "AllInactivePercent";
			this.gridColumnInactivePercent.Name = "gridColumnInactivePercent";
			this.gridColumnInactivePercent.Visible = true;
			this.gridColumnInactivePercent.Width = 119;
			// 
			// TotalControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "AccessAllReportControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnInactivePercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnInactiveNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnActivePercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnActiveNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUsersNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
	}
}
