namespace SalesLibraries.BatchTagger.PresentationLayer
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
			this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnFileName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTaggedFilesCount = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnVideoCount = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTaggedVideoCount = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnFileDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.griodColumnDaysFormLastUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemHyperLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlData
			// 
			this.gridControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlData.Location = new System.Drawing.Point(0, 0);
			this.gridControlData.MainView = this.gridView;
			this.gridControlData.Name = "gridControlData";
			this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit});
			this.gridControlData.Size = new System.Drawing.Size(898, 483);
			this.gridControlData.TabIndex = 2;
			this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
			// 
			// gridView
			// 
			this.gridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.EvenRow.Options.UseFont = true;
			this.gridView.Appearance.EvenRow.Options.UseTextOptions = true;
			this.gridView.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedCell.Options.UseFont = true;
			this.gridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.OddRow.Options.UseFont = true;
			this.gridView.Appearance.OddRow.Options.UseTextOptions = true;
			this.gridView.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.Preview.Options.UseFont = true;
			this.gridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.Row.Options.UseFont = true;
			this.gridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.SelectedRow.Options.UseFont = true;
			this.gridView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridView.AppearancePrint.Preview.Options.UseTextOptions = true;
			this.gridView.AppearancePrint.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnFileName,
            this.gridColumnTaggedFilesCount,
            this.gridColumnVideoCount,
            this.gridColumnTaggedVideoCount,
            this.gridColumnFileDate,
            this.griodColumnDaysFormLastUpdate});
			this.gridView.GridControl = this.gridControlData;
			this.gridView.Name = "gridView";
			this.gridView.OptionsCustomization.AllowFilter = false;
			this.gridView.OptionsCustomization.AllowGroup = false;
			this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridView.OptionsMenu.EnableColumnMenu = false;
			this.gridView.OptionsMenu.EnableFooterMenu = false;
			this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
			this.gridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridView.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridView.OptionsPrint.PrintPreview = true;
			this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridView.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridView.OptionsView.ColumnAutoWidth = false;
			this.gridView.OptionsView.ShowDetailButtons = false;
			this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridView.OptionsView.ShowGroupPanel = false;
			this.gridView.OptionsView.ShowIndicator = false;
			this.gridView.OptionsView.ShowPreview = true;
			this.gridView.RowHeight = 30;
			this.gridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.OnRowClick);
			this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.OnCustomDrawCell);
			this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.OnRowCellStyle);
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
			this.gridColumnName.VisibleIndex = 0;
			this.gridColumnName.Width = 169;
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
			this.gridColumnFileName.OptionsColumn.ReadOnly = true;
			this.gridColumnFileName.Visible = true;
			this.gridColumnFileName.VisibleIndex = 1;
			this.gridColumnFileName.Width = 133;
			// 
			// gridColumnTaggedFilesCount
			// 
			this.gridColumnTaggedFilesCount.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnTaggedFilesCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnTaggedFilesCount.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnTaggedFilesCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnTaggedFilesCount.Caption = "Tagged";
			this.gridColumnTaggedFilesCount.FieldName = "FilesTaggedCount";
			this.gridColumnTaggedFilesCount.Name = "gridColumnTaggedFilesCount";
			this.gridColumnTaggedFilesCount.OptionsColumn.AllowEdit = false;
			this.gridColumnTaggedFilesCount.OptionsColumn.ReadOnly = true;
			this.gridColumnTaggedFilesCount.Visible = true;
			this.gridColumnTaggedFilesCount.VisibleIndex = 2;
			this.gridColumnTaggedFilesCount.Width = 136;
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
			this.gridColumnVideoCount.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoCount.Visible = true;
			this.gridColumnVideoCount.VisibleIndex = 3;
			this.gridColumnVideoCount.Width = 141;
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
			this.gridColumnTaggedVideoCount.OptionsColumn.ReadOnly = true;
			this.gridColumnTaggedVideoCount.Visible = true;
			this.gridColumnTaggedVideoCount.VisibleIndex = 4;
			this.gridColumnTaggedVideoCount.Width = 130;
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
			this.gridColumnFileDate.OptionsColumn.ReadOnly = true;
			this.gridColumnFileDate.Visible = true;
			this.gridColumnFileDate.VisibleIndex = 5;
			this.gridColumnFileDate.Width = 144;
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
			this.griodColumnDaysFormLastUpdate.OptionsColumn.ReadOnly = true;
			this.griodColumnDaysFormLastUpdate.Visible = true;
			this.griodColumnDaysFormLastUpdate.VisibleIndex = 6;
			this.griodColumnDaysFormLastUpdate.Width = 237;
			// 
			// repositoryItemHyperLinkEdit
			// 
			this.repositoryItemHyperLinkEdit.AutoHeight = false;
			this.repositoryItemHyperLinkEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.BatchTagger.Properties.Resources.Url, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Copy URL to Clipboard", null, null, true)});
			this.repositoryItemHyperLinkEdit.Name = "repositoryItemHyperLinkEdit";
			this.repositoryItemHyperLinkEdit.SingleClick = true;
			// 
			// TotalControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnFileName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTaggedFilesCount;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoCount;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTaggedVideoCount;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnFileDate;
		private DevExpress.XtraGrid.Columns.GridColumn griodColumnDaysFormLastUpdate;
	}
}
