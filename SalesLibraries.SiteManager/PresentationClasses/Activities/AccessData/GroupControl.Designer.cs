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
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUsersNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridBandActive = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnActiveNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnActivePercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridBandInactive = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnInactiveNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnInactivePercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
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
            this.gridBandActive,
            this.gridBandInactive});
			this.advBandedGridViewData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
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
			this.advBandedGridViewData.OptionsView.ShowPreview = true;
			this.advBandedGridViewData.PreviewFieldName = "Details";
			this.advBandedGridViewData.PreviewIndent = 5;
			this.advBandedGridViewData.RowHeight = 35;
			this.advBandedGridViewData.RowSeparatorHeight = 10;
			this.advBandedGridViewData.CalcPreviewText += new DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventHandler(this.advBandedGridViewData_CalcPreviewText);
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnUsersNumber);
			this.gridBandMain.MinWidth = 20;
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 129;
			// 
			// gridColumnUsersNumber
			// 
			this.gridColumnUsersNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUsersNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUsersNumber.Caption = "Total Users#";
			this.gridColumnUsersNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUsersNumber.FieldName = "userCount";
			this.gridColumnUsersNumber.Name = "gridColumnUsersNumber";
			this.gridColumnUsersNumber.Visible = true;
			this.gridColumnUsersNumber.Width = 129;
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
			// gridBandActive
			// 
			this.gridBandActive.Caption = "Active";
			this.gridBandActive.Columns.Add(this.gridColumnActiveNumber);
			this.gridBandActive.Columns.Add(this.gridColumnActivePercent);
			this.gridBandActive.MinWidth = 20;
			this.gridBandActive.Name = "gridBandActive";
			this.gridBandActive.VisibleIndex = 1;
			this.gridBandActive.Width = 221;
			// 
			// gridColumnActiveNumber
			// 
			this.gridColumnActiveNumber.Caption = "Active#";
			this.gridColumnActiveNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnActiveNumber.FieldName = "activeCount";
			this.gridColumnActiveNumber.Name = "gridColumnActiveNumber";
			this.gridColumnActiveNumber.Visible = true;
			this.gridColumnActiveNumber.Width = 129;
			// 
			// gridColumnActivePercent
			// 
			this.gridColumnActivePercent.Caption = "Active%";
			this.gridColumnActivePercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnActivePercent.FieldName = "ActivePercent";
			this.gridColumnActivePercent.Name = "gridColumnActivePercent";
			this.gridColumnActivePercent.Visible = true;
			this.gridColumnActivePercent.Width = 92;
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
			// gridBandInactive
			// 
			this.gridBandInactive.Caption = "Inactive";
			this.gridBandInactive.Columns.Add(this.gridColumnInactiveNumber);
			this.gridBandInactive.Columns.Add(this.gridColumnInactivePercent);
			this.gridBandInactive.MinWidth = 20;
			this.gridBandInactive.Name = "gridBandInactive";
			this.gridBandInactive.VisibleIndex = 2;
			this.gridBandInactive.Width = 237;
			// 
			// gridColumnInactiveNumber
			// 
			this.gridColumnInactiveNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnInactiveNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnInactiveNumber.Caption = "Inactive#";
			this.gridColumnInactiveNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnInactiveNumber.FieldName = "inactiveCount";
			this.gridColumnInactiveNumber.Name = "gridColumnInactiveNumber";
			this.gridColumnInactiveNumber.Visible = true;
			this.gridColumnInactiveNumber.Width = 114;
			// 
			// gridColumnInactivePercent
			// 
			this.gridColumnInactivePercent.Caption = "Inactive%";
			this.gridColumnInactivePercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnInactivePercent.FieldName = "InactivePercent";
			this.gridColumnInactivePercent.Name = "gridColumnInactivePercent";
			this.gridColumnInactivePercent.Visible = true;
			this.gridColumnInactivePercent.Width = 123;
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
			this.Name = "AccessGroupReportControl";
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
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUsersNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnActiveNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnActivePercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnInactiveNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnInactivePercent;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandActive;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandInactive;
    }
}
