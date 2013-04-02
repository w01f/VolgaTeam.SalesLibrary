namespace SalesDepot.SiteManager.PresentationClasses.Activities.Views
{
	public partial class NavigationUserReportControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationUserReportControl));
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandLibraries = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUserLibrariesNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnUserLibrariesPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnGroupLibrariesNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandPages = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUserPagesNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnUserPagesPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupPagesNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandTotal = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnUserTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnUserTotalPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemDateEditDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
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
            this.gridColumnGroup,
            this.gridColumnUserLibrariesNumber,
            this.gridColumnUserLibrariesPercent,
            this.gridColumnGroupLibrariesNumber,
            this.gridColumnUserPagesNumber,
            this.gridColumnUserPagesPercent,
            this.gridColumnGroupPagesNumber,
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
			this.advBandedGridViewData.OptionsPrint.PrintDetails = true;
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
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnName);
			this.gridBandMain.Columns.Add(this.gridColumnGroup);
			this.gridBandMain.Name = "gridBandMain";
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
			// gridBandLibraries
			// 
			this.gridBandLibraries.Caption = "Libraries";
			this.gridBandLibraries.Columns.Add(this.gridColumnUserLibrariesNumber);
			this.gridBandLibraries.Columns.Add(this.gridColumnUserLibrariesPercent);
			this.gridBandLibraries.Columns.Add(this.gridColumnGroupLibrariesNumber);
			this.gridBandLibraries.Name = "gridBandLibraries";
			this.gridBandLibraries.OptionsBand.FixedWidth = true;
			this.gridBandLibraries.Width = 143;
			// 
			// gridColumnUserLibrariesNumber
			// 
			this.gridColumnUserLibrariesNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserLibrariesNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserLibrariesNumber.Caption = "#";
			this.gridColumnUserLibrariesNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserLibrariesNumber.FieldName = "userLibraries";
			this.gridColumnUserLibrariesNumber.Name = "gridColumnUserLibrariesNumber";
			this.gridColumnUserLibrariesNumber.Visible = true;
			this.gridColumnUserLibrariesNumber.Width = 143;
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
			// gridColumnUserLibrariesPercent
			// 
			this.gridColumnUserLibrariesPercent.Caption = "%";
			this.gridColumnUserLibrariesPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnUserLibrariesPercent.FieldName = "LibrariesPercent";
			this.gridColumnUserLibrariesPercent.Name = "gridColumnUserLibrariesPercent";
			this.gridColumnUserLibrariesPercent.RowIndex = 1;
			this.gridColumnUserLibrariesPercent.Visible = true;
			this.gridColumnUserLibrariesPercent.Width = 64;
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
			// gridColumnGroupLibrariesNumber
			// 
			this.gridColumnGroupLibrariesNumber.Caption = "Group#";
			this.gridColumnGroupLibrariesNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupLibrariesNumber.FieldName = "groupLibraries";
			this.gridColumnGroupLibrariesNumber.Name = "gridColumnGroupLibrariesNumber";
			this.gridColumnGroupLibrariesNumber.RowIndex = 1;
			this.gridColumnGroupLibrariesNumber.Visible = true;
			this.gridColumnGroupLibrariesNumber.Width = 79;
			// 
			// gridBandPages
			// 
			this.gridBandPages.Caption = "Pages";
			this.gridBandPages.Columns.Add(this.gridColumnUserPagesNumber);
			this.gridBandPages.Columns.Add(this.gridColumnUserPagesPercent);
			this.gridBandPages.Columns.Add(this.gridColumnGroupPagesNumber);
			this.gridBandPages.Name = "gridBandPages";
			this.gridBandPages.OptionsBand.FixedWidth = true;
			this.gridBandPages.Width = 136;
			// 
			// gridColumnUserPagesNumber
			// 
			this.gridColumnUserPagesNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserPagesNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserPagesNumber.Caption = "#";
			this.gridColumnUserPagesNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserPagesNumber.FieldName = "userPages";
			this.gridColumnUserPagesNumber.Name = "gridColumnUserPagesNumber";
			this.gridColumnUserPagesNumber.Visible = true;
			this.gridColumnUserPagesNumber.Width = 136;
			// 
			// gridColumnUserPagesPercent
			// 
			this.gridColumnUserPagesPercent.Caption = "%";
			this.gridColumnUserPagesPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnUserPagesPercent.FieldName = "PagesPercent";
			this.gridColumnUserPagesPercent.Name = "gridColumnUserPagesPercent";
			this.gridColumnUserPagesPercent.RowIndex = 1;
			this.gridColumnUserPagesPercent.Visible = true;
			this.gridColumnUserPagesPercent.Width = 62;
			// 
			// gridColumnGroupPagesNumber
			// 
			this.gridColumnGroupPagesNumber.Caption = "Group#";
			this.gridColumnGroupPagesNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupPagesNumber.FieldName = "groupPages";
			this.gridColumnGroupPagesNumber.Name = "gridColumnGroupPagesNumber";
			this.gridColumnGroupPagesNumber.RowIndex = 1;
			this.gridColumnGroupPagesNumber.Visible = true;
			this.gridColumnGroupPagesNumber.Width = 74;
			// 
			// gridBandTotal
			// 
			this.gridBandTotal.Caption = "Total";
			this.gridBandTotal.Columns.Add(this.gridColumnUserTotalNumber);
			this.gridBandTotal.Columns.Add(this.gridColumnUserTotalPercent);
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalNumber);
			this.gridBandTotal.Name = "gridBandTotal";
			this.gridBandTotal.OptionsBand.FixedWidth = true;
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
			this.repositoryItemDateEditDate.DisplayFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.EditFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.Name = "repositoryItemDateEditDate";
			this.repositoryItemDateEditDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
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
			// NavigationUserReportControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "NavigationUserReportControl";
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

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDate;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewData;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroup;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserLibrariesNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserLibrariesPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLibrariesNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserPagesNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserPagesPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupPagesNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserTotalNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnUserTotalPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalNumber;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLibraries;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandPages;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandTotal;
		private DevExpress.XtraPrinting.PrintingSystem printingSystem;
		private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink;
    }
}
