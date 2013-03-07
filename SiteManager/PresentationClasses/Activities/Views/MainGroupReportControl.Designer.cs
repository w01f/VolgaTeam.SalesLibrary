﻿namespace SalesDepot.SiteManager.PresentationClasses.Activities.Views
{
	public partial class MainGroupReportControl
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
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandLogin = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupLoginNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnGroupLoginPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnAllLoginNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandDocs = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupDocsNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupDocsPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnAllDocsNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandVideos = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupVideosNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupVideosPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnAllVideosNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandTotal = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnGroupTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnGroupTotalPercent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnAllTotalNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemDateEditDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
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
			this.advBandedGridViewData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandMain,
            this.gridBandLogin,
            this.gridBandDocs,
            this.gridBandVideos,
            this.gridBandTotal});
			this.advBandedGridViewData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnName,
            this.gridColumnGroupLoginNumber,
            this.gridColumnGroupLoginPercent,
            this.gridColumnAllLoginNumber,
            this.gridColumnGroupDocsNumber,
            this.gridColumnGroupDocsPercent,
            this.gridColumnAllDocsNumber,
            this.gridColumnGroupVideosNumber,
            this.gridColumnGroupVideosPercent,
            this.gridColumnAllVideosNumber,
            this.gridColumnGroupTotalNumber,
            this.gridColumnGroupTotalPercent,
            this.gridColumnAllTotalNumber});
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
			this.advBandedGridViewData.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gridViewData_CustomColumnSort);
			// 
			// gridBandMain
			// 
			this.gridBandMain.Columns.Add(this.gridColumnName);
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.Width = 1188;
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
			this.gridColumnName.RowCount = 2;
			this.gridColumnName.Visible = true;
			this.gridColumnName.Width = 1188;
			// 
			// gridBandLogin
			// 
			this.gridBandLogin.Caption = "Login";
			this.gridBandLogin.Columns.Add(this.gridColumnGroupLoginNumber);
			this.gridBandLogin.Columns.Add(this.gridColumnGroupLoginPercent);
			this.gridBandLogin.Columns.Add(this.gridColumnAllLoginNumber);
			this.gridBandLogin.Name = "gridBandLogin";
			this.gridBandLogin.OptionsBand.FixedWidth = true;
			this.gridBandLogin.Width = 143;
			// 
			// gridColumnGroupLoginNumber
			// 
			this.gridColumnGroupLoginNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupLoginNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupLoginNumber.Caption = "#";
			this.gridColumnGroupLoginNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupLoginNumber.FieldName = "logins";
			this.gridColumnGroupLoginNumber.Name = "gridColumnGroupLoginNumber";
			this.gridColumnGroupLoginNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnGroupLoginNumber.Visible = true;
			this.gridColumnGroupLoginNumber.Width = 143;
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
			this.gridColumnGroupLoginPercent.RowIndex = 1;
			this.gridColumnGroupLoginPercent.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnGroupLoginPercent.Visible = true;
			this.gridColumnGroupLoginPercent.Width = 64;
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
			// gridColumnAllLoginNumber
			// 
			this.gridColumnAllLoginNumber.Caption = "All#";
			this.gridColumnAllLoginNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnAllLoginNumber.FieldName = "AllLogins";
			this.gridColumnAllLoginNumber.Name = "gridColumnAllLoginNumber";
			this.gridColumnAllLoginNumber.RowIndex = 1;
			this.gridColumnAllLoginNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnAllLoginNumber.Visible = true;
			this.gridColumnAllLoginNumber.Width = 79;
			// 
			// gridBandDocs
			// 
			this.gridBandDocs.Caption = "Doc";
			this.gridBandDocs.Columns.Add(this.gridColumnGroupDocsNumber);
			this.gridBandDocs.Columns.Add(this.gridColumnGroupDocsPercent);
			this.gridBandDocs.Columns.Add(this.gridColumnAllDocsNumber);
			this.gridBandDocs.Name = "gridBandDocs";
			this.gridBandDocs.OptionsBand.FixedWidth = true;
			this.gridBandDocs.Width = 136;
			// 
			// gridColumnGroupDocsNumber
			// 
			this.gridColumnGroupDocsNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupDocsNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupDocsNumber.Caption = "#";
			this.gridColumnGroupDocsNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupDocsNumber.FieldName = "docs";
			this.gridColumnGroupDocsNumber.Name = "gridColumnGroupDocsNumber";
			this.gridColumnGroupDocsNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnGroupDocsNumber.Visible = true;
			this.gridColumnGroupDocsNumber.Width = 136;
			// 
			// gridColumnGroupDocsPercent
			// 
			this.gridColumnGroupDocsPercent.Caption = "%";
			this.gridColumnGroupDocsPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupDocsPercent.FieldName = "DocsPercent";
			this.gridColumnGroupDocsPercent.Name = "gridColumnGroupDocsPercent";
			this.gridColumnGroupDocsPercent.RowIndex = 1;
			this.gridColumnGroupDocsPercent.Visible = true;
			this.gridColumnGroupDocsPercent.Width = 62;
			// 
			// gridColumnAllDocsNumber
			// 
			this.gridColumnAllDocsNumber.Caption = "All#";
			this.gridColumnAllDocsNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnAllDocsNumber.FieldName = "AllDocs";
			this.gridColumnAllDocsNumber.Name = "gridColumnAllDocsNumber";
			this.gridColumnAllDocsNumber.RowIndex = 1;
			this.gridColumnAllDocsNumber.Visible = true;
			this.gridColumnAllDocsNumber.Width = 74;
			// 
			// gridBandVideos
			// 
			this.gridBandVideos.Caption = "Video";
			this.gridBandVideos.Columns.Add(this.gridColumnGroupVideosNumber);
			this.gridBandVideos.Columns.Add(this.gridColumnGroupVideosPercent);
			this.gridBandVideos.Columns.Add(this.gridColumnAllVideosNumber);
			this.gridBandVideos.Name = "gridBandVideos";
			this.gridBandVideos.OptionsBand.FixedWidth = true;
			this.gridBandVideos.Width = 144;
			// 
			// gridColumnGroupVideosNumber
			// 
			this.gridColumnGroupVideosNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupVideosNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupVideosNumber.Caption = "#";
			this.gridColumnGroupVideosNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupVideosNumber.FieldName = "videos";
			this.gridColumnGroupVideosNumber.Name = "gridColumnGroupVideosNumber";
			this.gridColumnGroupVideosNumber.Visible = true;
			this.gridColumnGroupVideosNumber.Width = 144;
			// 
			// gridColumnGroupVideosPercent
			// 
			this.gridColumnGroupVideosPercent.Caption = "%";
			this.gridColumnGroupVideosPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupVideosPercent.FieldName = "VideosPercent";
			this.gridColumnGroupVideosPercent.Name = "gridColumnGroupVideosPercent";
			this.gridColumnGroupVideosPercent.RowIndex = 1;
			this.gridColumnGroupVideosPercent.Visible = true;
			this.gridColumnGroupVideosPercent.Width = 71;
			// 
			// gridColumnAllVideosNumber
			// 
			this.gridColumnAllVideosNumber.Caption = "All#";
			this.gridColumnAllVideosNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnAllVideosNumber.FieldName = "AllVideos";
			this.gridColumnAllVideosNumber.Name = "gridColumnAllVideosNumber";
			this.gridColumnAllVideosNumber.RowIndex = 1;
			this.gridColumnAllVideosNumber.Visible = true;
			this.gridColumnAllVideosNumber.Width = 73;
			// 
			// gridBandTotal
			// 
			this.gridBandTotal.Caption = "Total";
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalNumber);
			this.gridBandTotal.Columns.Add(this.gridColumnGroupTotalPercent);
			this.gridBandTotal.Columns.Add(this.gridColumnAllTotalNumber);
			this.gridBandTotal.Name = "gridBandTotal";
			this.gridBandTotal.OptionsBand.FixedWidth = true;
			this.gridBandTotal.Width = 135;
			// 
			// gridColumnGroupTotalNumber
			// 
			this.gridColumnGroupTotalNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnGroupTotalNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnGroupTotalNumber.Caption = "#";
			this.gridColumnGroupTotalNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupTotalNumber.FieldName = "totals";
			this.gridColumnGroupTotalNumber.Name = "gridColumnGroupTotalNumber";
			this.gridColumnGroupTotalNumber.Visible = true;
			this.gridColumnGroupTotalNumber.Width = 135;
			// 
			// gridColumnGroupTotalPercent
			// 
			this.gridColumnGroupTotalPercent.Caption = "%";
			this.gridColumnGroupTotalPercent.ColumnEdit = this.repositoryItemSpinEditPercent;
			this.gridColumnGroupTotalPercent.FieldName = "TotalPercent";
			this.gridColumnGroupTotalPercent.Name = "gridColumnGroupTotalPercent";
			this.gridColumnGroupTotalPercent.RowIndex = 1;
			this.gridColumnGroupTotalPercent.Visible = true;
			this.gridColumnGroupTotalPercent.Width = 69;
			// 
			// gridColumnAllTotalNumber
			// 
			this.gridColumnAllTotalNumber.Caption = "All#";
			this.gridColumnAllTotalNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnAllTotalNumber.FieldName = "AllTotals";
			this.gridColumnAllTotalNumber.Name = "gridColumnAllTotalNumber";
			this.gridColumnAllTotalNumber.RowIndex = 1;
			this.gridColumnAllTotalNumber.Visible = true;
			this.gridColumnAllTotalNumber.Width = 66;
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
			// MainGroupReportControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainGroupReportControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
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
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLoginNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupLoginPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnAllLoginNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupDocsNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupDocsPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnAllDocsNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupVideosNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupVideosPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnAllVideosNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalNumber;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnGroupTotalPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnAllTotalNumber;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLogin;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandDocs;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandVideos;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandTotal;
    }
}
