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
			this.gridBandLogin.Columns.Add(this.gridColumnUserLoginNumber);
			this.gridBandLogin.Columns.Add(this.gridColumnUserLoginPercent);
			this.gridBandLogin.Columns.Add(this.gridColumnGroupLoginNumber);
			this.gridBandLogin.Name = "gridBandLogin";
			this.gridBandLogin.OptionsBand.FixedWidth = true;
			this.gridBandLogin.Width = 143;
			// 
			// gridColumnUserLoginNumber
			// 
			this.gridColumnUserLoginNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserLoginNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserLoginNumber.Caption = "#";
			this.gridColumnUserLoginNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserLoginNumber.FieldName = "logins";
			this.gridColumnUserLoginNumber.Name = "gridColumnUserLoginNumber";
			this.gridColumnUserLoginNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
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
			this.gridColumnUserLoginPercent.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnUserLoginPercent.Visible = true;
			this.gridColumnUserLoginPercent.Width = 64;
			// 
			// repositoryItemSpinEditPercent
			// 
			this.repositoryItemSpinEditPercent.AutoHeight = false;
			this.repositoryItemSpinEditPercent.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemSpinEditPercent.DisplayFormat.FormatString = "#0%";
			this.repositoryItemSpinEditPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditPercent.EditFormat.FormatString = "#0%";
			this.repositoryItemSpinEditPercent.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditPercent.Name = "repositoryItemSpinEditPercent";
			this.repositoryItemSpinEditPercent.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// gridColumnGroupLoginNumber
			// 
			this.gridColumnGroupLoginNumber.Caption = "All#";
			this.gridColumnGroupLoginNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupLoginNumber.FieldName = "AllLogins";
			this.gridColumnGroupLoginNumber.Name = "gridColumnGroupLoginNumber";
			this.gridColumnGroupLoginNumber.RowIndex = 1;
			this.gridColumnGroupLoginNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
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
			this.gridBandDocs.Width = 136;
			// 
			// gridColumnUserDocsNumber
			// 
			this.gridColumnUserDocsNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserDocsNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserDocsNumber.Caption = "#";
			this.gridColumnUserDocsNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserDocsNumber.FieldName = "docs";
			this.gridColumnUserDocsNumber.Name = "gridColumnUserDocsNumber";
			this.gridColumnUserDocsNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
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
			this.gridColumnGroupDocsNumber.Caption = "All#";
			this.gridColumnGroupDocsNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupDocsNumber.FieldName = "AllDocs";
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
			this.gridBandVideos.Width = 144;
			// 
			// gridColumnUserVideosNumber
			// 
			this.gridColumnUserVideosNumber.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUserVideosNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnUserVideosNumber.Caption = "#";
			this.gridColumnUserVideosNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnUserVideosNumber.FieldName = "videos";
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
			this.gridColumnGroupVideosNumber.Caption = "All#";
			this.gridColumnGroupVideosNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupVideosNumber.FieldName = "AllVideos";
			this.gridColumnGroupVideosNumber.Name = "gridColumnGroupVideosNumber";
			this.gridColumnGroupVideosNumber.RowIndex = 1;
			this.gridColumnGroupVideosNumber.Visible = true;
			this.gridColumnGroupVideosNumber.Width = 73;
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
			this.gridColumnUserTotalNumber.FieldName = "totals";
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
			this.gridColumnGroupTotalNumber.Caption = "All#";
			this.gridColumnGroupTotalNumber.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnGroupTotalNumber.FieldName = "AllTotals";
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