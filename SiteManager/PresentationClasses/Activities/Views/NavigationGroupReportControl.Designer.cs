namespace SalesDepot.SiteManager.PresentationClasses.Activities.Views
{
	public partial class NavigationGroupReportControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationGroupReportControl));
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandLibraries = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupLibrariesNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnGroupLibrariesPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridBandPages = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupPagesNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupPagesPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandTotal = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupTotalPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemDateEditDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.printingSystem = new DevExpress.XtraPrinting.PrintingSystem(this.components);
			this.printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
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
            this.gridBandLibraries,
            this.gridBandPages,
            this.gridBandTotal});
			this.advBandedGridViewData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnName,
            this.gridColumnGroupLibrariesNumber,
            this.gridColumnGroupLibrariesPercent,
            this.gridColumnGroupPagesNumber,
            this.gridColumnGroupPagesPercent,
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
			this.advBandedGridViewData.CustomDrawRowFooterCell += new DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventHandler(this.advBandedGridViewData_CustomDrawRowFooterCell);
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnName);
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.Width = 1305;
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
			this.gridColumnName.SummaryItem.DisplayFormat = "All Groups:";
			this.gridColumnName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
			this.gridColumnName.Visible = true;
			this.gridColumnName.Width = 1305;
			// 
			// gridBandLibraries
			// 
			this.gridBandLibraries.Caption = "Libraries";
			this.gridBandLibraries.Columns.Add(this.gridColumnGroupLibrariesNumber);
			this.gridBandLibraries.Columns.Add(this.gridColumnGroupLibrariesPercent);
			this.gridBandLibraries.Name = "gridBandLibraries";
			this.gridBandLibraries.OptionsBand.FixedWidth = true;
			this.gridBandLibraries.Width = 151;
			// 
			// gridColumnGroupLibrariesNumber
			// 
			this.gridColumnGroupLibrariesNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupLibrariesNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupLibrariesNumber.Caption = "#";
			this.gridColumnGroupLibrariesNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupLibrariesNumber.FieldName = "libs";
			this.gridColumnGroupLibrariesNumber.Name = "gridColumnGroupLibrariesNumber";
			this.gridColumnGroupLibrariesNumber.SummaryItem.DisplayFormat = "{0:#,##0}";
			this.gridColumnGroupLibrariesNumber.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.gridColumnGroupLibrariesNumber.Visible = true;
			this.gridColumnGroupLibrariesNumber.Width = 79;
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
			// gridColumnGroupLibrariesPercent
			// 
			this.gridColumnGroupLibrariesPercent.Caption = "%";
			this.gridColumnGroupLibrariesPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupLibrariesPercent.FieldName = "LibrariesPercent";
			this.gridColumnGroupLibrariesPercent.Name = "gridColumnGroupLibrariesPercent";
			this.gridColumnGroupLibrariesPercent.Visible = true;
			this.gridColumnGroupLibrariesPercent.Width = 72;
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
			// gridBandPages
			// 
			this.gridBandPages.Caption = "Pages";
			this.gridBandPages.Columns.Add(this.gridColumnGroupPagesNumber);
			this.gridBandPages.Columns.Add(this.gridColumnGroupPagesPercent);
			this.gridBandPages.Name = "gridBandPages";
			this.gridBandPages.OptionsBand.FixedWidth = true;
			this.gridBandPages.Width = 145;
			// 
			// gridColumnGroupPagesNumber
			// 
			this.gridColumnGroupPagesNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupPagesNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupPagesNumber.Caption = "#";
			this.gridColumnGroupPagesNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupPagesNumber.FieldName = "pages";
			this.gridColumnGroupPagesNumber.Name = "gridColumnGroupPagesNumber";
			this.gridColumnGroupPagesNumber.SummaryItem.DisplayFormat = "{0:#,##0}";
			this.gridColumnGroupPagesNumber.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.gridColumnGroupPagesNumber.Visible = true;
			this.gridColumnGroupPagesNumber.Width = 72;
			// 
			// gridColumnGroupPagesPercent
			// 
			this.gridColumnGroupPagesPercent.Caption = "%";
			this.gridColumnGroupPagesPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupPagesPercent.FieldName = "PagesPercent";
			this.gridColumnGroupPagesPercent.Name = "gridColumnGroupPagesPercent";
			this.gridColumnGroupPagesPercent.Visible = true;
			this.gridColumnGroupPagesPercent.Width = 73;
			// 
			// gridBandTotal
			// 
			this.gridBandTotal.Caption = "Total";
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalNumber);
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalPercent);
			this.gridBandTotal.Name = "gridBandTotal";
			this.gridBandTotal.OptionsBand.FixedWidth = true;
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
			this.gridColumnGroupTotalNumber.SummaryItem.DisplayFormat = "{0:#,##0}";
			this.gridColumnGroupTotalNumber.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
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
			this.repositoryItemDateEditDate.DisplayFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.EditFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.Name = "repositoryItemDateEditDate";
			this.repositoryItemDateEditDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Lilian";
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
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
			// printingSystem
			// 
			this.printingSystem.Links.AddRange(new object[] {
            this.printableComponentLink});
			// 
			// printableComponentLink
			// 
			this.printableComponentLink.Component = this.gridControlData;
			// 
			// 
			// 
			this.printableComponentLink.ImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("printableComponentLink.ImageCollection.ImageStream")));
			this.printableComponentLink.Landscape = true;
			this.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
			this.printableComponentLink.PrintingSystem = this.printingSystem;
			this.printableComponentLink.PrintingSystemBase = this.printingSystem;
			this.printableComponentLink.CreateReportHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.printableComponentLink_CreateReportHeaderArea);
			// 
			// NavigationGroupReportControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "NavigationGroupReportControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDate;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewData;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLibrariesNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLibrariesPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupPagesNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupPagesPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalPercent;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraPrinting.PrintingSystem printingSystem;
		private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLibraries;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandPages;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandTotal;
    }
}
