namespace SalesLibraries.SiteManager.PresentationClasses.LibraryFiles
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnFileName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnTaggedFilesCount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnVideoCount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnTaggedVideoCount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnFileDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.griodColumnDaysFormLastUpdate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemHyperLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlData
			// 
			this.gridControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlData.Location = new System.Drawing.Point(0, 0);
			this.gridControlData.MainView = this.advBandedGridViewData;
			this.gridControlData.Name = "gridControlData";
			this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit});
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
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.advBandedGridViewData.AppearancePrint.Preview.Options.UseTextOptions = true;
			this.advBandedGridViewData.AppearancePrint.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.advBandedGridViewData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandMain});
			this.advBandedGridViewData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnName,
            this.gridColumnFileName,
            this.gridColumnTaggedFilesCount,
            this.gridColumnVideoCount,
            this.gridColumnTaggedVideoCount,
            this.gridColumnFileDate,
            this.griodColumnDaysFormLastUpdate});
			this.advBandedGridViewData.GridControl = this.gridControlData;
			this.advBandedGridViewData.Name = "advBandedGridViewData";
			this.advBandedGridViewData.OptionsCustomization.AllowBandMoving = false;
			this.advBandedGridViewData.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridViewData.OptionsCustomization.AllowColumnResizing = false;
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
			this.advBandedGridViewData.OptionsPrint.PrintPreview = true;
			this.advBandedGridViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridViewData.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridViewData.OptionsView.AutoCalcPreviewLineCount = true;
			this.advBandedGridViewData.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridViewData.OptionsView.ShowBands = false;
			this.advBandedGridViewData.OptionsView.ShowDetailButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupPanel = false;
			this.advBandedGridViewData.OptionsView.ShowIndicator = false;
			this.advBandedGridViewData.OptionsView.ShowPreview = true;
			this.advBandedGridViewData.RowHeight = 30;
			this.advBandedGridViewData.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.OnCustomDrawCell);
			// 
			// gridBandMain
			// 
			this.gridBandMain.Caption = "User:";
			this.gridBandMain.Columns.Add(this.gridColumnName);
			this.gridBandMain.Columns.Add(this.gridColumnFileName);
			this.gridBandMain.Columns.Add(this.gridColumnTaggedFilesCount);
			this.gridBandMain.Columns.Add(this.gridColumnVideoCount);
			this.gridBandMain.Columns.Add(this.gridColumnTaggedVideoCount);
			this.gridBandMain.Columns.Add(this.gridColumnFileDate);
			this.gridBandMain.Columns.Add(this.griodColumnDaysFormLastUpdate);
			this.gridBandMain.MinWidth = 20;
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 896;
			// 
			// gridColumnName
			// 
			this.gridColumnName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnName.Caption = "Active Sales Libraries:";
			this.gridColumnName.FieldName = "Name";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.OptionsColumn.AllowEdit = false;
			this.gridColumnName.OptionsColumn.ReadOnly = true;
			this.gridColumnName.Visible = true;
			this.gridColumnName.Width = 136;
			// 
			// gridColumnFileName
			// 
			this.gridColumnFileName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFileName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnFileName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnFileName.Caption = "Total Files";
			this.gridColumnFileName.FieldName = "FilesTotalCount";
			this.gridColumnFileName.Name = "gridColumnFileName";
			this.gridColumnFileName.OptionsColumn.AllowEdit = false;
			this.gridColumnFileName.OptionsColumn.FixedWidth = true;
			this.gridColumnFileName.OptionsColumn.ReadOnly = true;
			this.gridColumnFileName.Visible = true;
			this.gridColumnFileName.Width = 120;
			// 
			// gridColumnTaggedFilesCount
			// 
			this.gridColumnTaggedFilesCount.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnTaggedFilesCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnTaggedFilesCount.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnTaggedFilesCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnTaggedFilesCount.AutoFillDown = true;
			this.gridColumnTaggedFilesCount.Caption = "Tagged";
			this.gridColumnTaggedFilesCount.FieldName = "FilesTaggedCount";
			this.gridColumnTaggedFilesCount.Name = "gridColumnTaggedFilesCount";
			this.gridColumnTaggedFilesCount.OptionsColumn.AllowEdit = false;
			this.gridColumnTaggedFilesCount.OptionsColumn.FixedWidth = true;
			this.gridColumnTaggedFilesCount.OptionsColumn.ReadOnly = true;
			this.gridColumnTaggedFilesCount.Visible = true;
			this.gridColumnTaggedFilesCount.Width = 120;
			// 
			// gridColumnVideoCount
			// 
			this.gridColumnVideoCount.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnVideoCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnVideoCount.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnVideoCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnVideoCount.Caption = "Total Videos";
			this.gridColumnVideoCount.FieldName = "VideoTotalCount";
			this.gridColumnVideoCount.Name = "gridColumnVideoCount";
			this.gridColumnVideoCount.OptionsColumn.AllowEdit = false;
			this.gridColumnVideoCount.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoCount.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoCount.Visible = true;
			this.gridColumnVideoCount.Width = 120;
			// 
			// gridColumnTaggedVideoCount
			// 
			this.gridColumnTaggedVideoCount.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnTaggedVideoCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnTaggedVideoCount.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnTaggedVideoCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnTaggedVideoCount.Caption = "Tagged";
			this.gridColumnTaggedVideoCount.FieldName = "VideoTaggedCount";
			this.gridColumnTaggedVideoCount.Name = "gridColumnTaggedVideoCount";
			this.gridColumnTaggedVideoCount.OptionsColumn.AllowEdit = false;
			this.gridColumnTaggedVideoCount.OptionsColumn.FixedWidth = true;
			this.gridColumnTaggedVideoCount.OptionsColumn.ReadOnly = true;
			this.gridColumnTaggedVideoCount.Visible = true;
			this.gridColumnTaggedVideoCount.Width = 120;
			// 
			// gridColumnFileDate
			// 
			this.gridColumnFileDate.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFileDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnFileDate.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnFileDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnFileDate.Caption = "Library Date";
			this.gridColumnFileDate.DisplayFormat.FormatString = "d";
			this.gridColumnFileDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumnFileDate.FieldName = "LibraryDate";
			this.gridColumnFileDate.Name = "gridColumnFileDate";
			this.gridColumnFileDate.OptionsColumn.AllowEdit = false;
			this.gridColumnFileDate.OptionsColumn.FixedWidth = true;
			this.gridColumnFileDate.OptionsColumn.ReadOnly = true;
			this.gridColumnFileDate.Visible = true;
			this.gridColumnFileDate.Width = 120;
			// 
			// griodColumnDaysFormLastUpdate
			// 
			this.griodColumnDaysFormLastUpdate.AppearanceCell.Options.UseTextOptions = true;
			this.griodColumnDaysFormLastUpdate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.griodColumnDaysFormLastUpdate.AppearanceHeader.Options.UseTextOptions = true;
			this.griodColumnDaysFormLastUpdate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.griodColumnDaysFormLastUpdate.Caption = "Days since Update";
			this.griodColumnDaysFormLastUpdate.FieldName = "DaysFormLastUpdate";
			this.griodColumnDaysFormLastUpdate.Name = "griodColumnDaysFormLastUpdate";
			this.griodColumnDaysFormLastUpdate.OptionsColumn.AllowEdit = false;
			this.griodColumnDaysFormLastUpdate.OptionsColumn.FixedWidth = true;
			this.griodColumnDaysFormLastUpdate.OptionsColumn.ReadOnly = true;
			this.griodColumnDaysFormLastUpdate.Visible = true;
			this.griodColumnDaysFormLastUpdate.Width = 160;
			// 
			// repositoryItemHyperLinkEdit
			// 
			this.repositoryItemHyperLinkEdit.AutoHeight = false;
			this.repositoryItemHyperLinkEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.Url, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Copy URL to Clipboard", null, null, true)});
			this.repositoryItemHyperLinkEdit.Name = "repositoryItemHyperLinkEdit";
			this.repositoryItemHyperLinkEdit.SingleClick = true;
			// 
			// TotalControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewData;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnFileName;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnVideoCount;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnFileDate;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn griodColumnDaysFormLastUpdate;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnTaggedFilesCount;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnTaggedVideoCount;
	}
}
