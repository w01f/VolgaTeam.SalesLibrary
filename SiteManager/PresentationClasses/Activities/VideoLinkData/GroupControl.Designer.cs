namespace SalesDepot.SiteManager.PresentationClasses.Activities.VideoLinkData
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridViewData = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridColumnFileName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnLinkName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnCategoryGroups = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnCategoryTags = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnKeywords = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnHoverNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnMp4Url = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemHyperLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
			this.gridColumnStation = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnThumbUrl = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
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
            this.gridColumnFileName,
            this.gridColumnLinkName,
            this.gridColumnCategoryGroups,
            this.gridColumnCategoryTags,
            this.gridColumnKeywords,
            this.gridColumnNote,
            this.gridColumnHoverNote,
            this.gridColumnMp4Url,
            this.gridColumnThumbUrl,
            this.gridColumnStation});
			this.advBandedGridViewData.GridControl = this.gridControlData;
			this.advBandedGridViewData.Name = "advBandedGridViewData";
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
			// 
			// gridColumnFileName
			// 
			this.gridColumnFileName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFileName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnFileName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnFileName.Caption = "File Name:";
			this.gridColumnFileName.FieldName = "fileName";
			this.gridColumnFileName.Name = "gridColumnFileName";
			this.gridColumnFileName.OptionsColumn.AllowEdit = false;
			this.gridColumnFileName.OptionsColumn.ReadOnly = true;
			this.gridColumnFileName.Visible = true;
			this.gridColumnFileName.Width = 109;
			// 
			// gridColumnLinkName
			// 
			this.gridColumnLinkName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnLinkName.AppearanceCell.Options.UseFont = true;
			this.gridColumnLinkName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnLinkName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnLinkName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnLinkName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnLinkName.Caption = "Link Name:";
			this.gridColumnLinkName.FieldName = "linkName";
			this.gridColumnLinkName.Name = "gridColumnLinkName";
			this.gridColumnLinkName.OptionsColumn.AllowEdit = false;
			this.gridColumnLinkName.OptionsColumn.ReadOnly = true;
			this.gridColumnLinkName.Visible = true;
			this.gridColumnLinkName.Width = 118;
			// 
			// gridColumnCategoryGroups
			// 
			this.gridColumnCategoryGroups.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnCategoryGroups.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnCategoryGroups.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnCategoryGroups.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnCategoryGroups.Caption = "Category Groups:";
			this.gridColumnCategoryGroups.FieldName = "categoryGroups";
			this.gridColumnCategoryGroups.Name = "gridColumnCategoryGroups";
			this.gridColumnCategoryGroups.OptionsColumn.AllowEdit = false;
			this.gridColumnCategoryGroups.OptionsColumn.ReadOnly = true;
			this.gridColumnCategoryGroups.Visible = true;
			this.gridColumnCategoryGroups.Width = 134;
			// 
			// gridColumnCategoryTags
			// 
			this.gridColumnCategoryTags.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnCategoryTags.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnCategoryTags.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnCategoryTags.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnCategoryTags.Caption = "Category Tags:";
			this.gridColumnCategoryTags.FieldName = "categoryTags";
			this.gridColumnCategoryTags.Name = "gridColumnCategoryTags";
			this.gridColumnCategoryTags.OptionsColumn.AllowEdit = false;
			this.gridColumnCategoryTags.OptionsColumn.ReadOnly = true;
			this.gridColumnCategoryTags.Visible = true;
			this.gridColumnCategoryTags.Width = 127;
			// 
			// gridColumnKeywords
			// 
			this.gridColumnKeywords.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnKeywords.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnKeywords.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnKeywords.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnKeywords.Caption = "Keywords:";
			this.gridColumnKeywords.FieldName = "keywords";
			this.gridColumnKeywords.Name = "gridColumnKeywords";
			this.gridColumnKeywords.OptionsColumn.AllowEdit = false;
			this.gridColumnKeywords.OptionsColumn.ReadOnly = true;
			this.gridColumnKeywords.Visible = true;
			this.gridColumnKeywords.Width = 100;
			// 
			// gridColumnNote
			// 
			this.gridColumnNote.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnNote.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnNote.Caption = "Link Notes:";
			this.gridColumnNote.FieldName = "linkNote";
			this.gridColumnNote.Name = "gridColumnNote";
			this.gridColumnNote.OptionsColumn.AllowEdit = false;
			this.gridColumnNote.OptionsColumn.ReadOnly = true;
			this.gridColumnNote.Visible = true;
			this.gridColumnNote.Width = 100;
			// 
			// gridColumnHoverNote
			// 
			this.gridColumnHoverNote.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnHoverNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnHoverNote.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnHoverNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnHoverNote.Caption = "Hover Notes:";
			this.gridColumnHoverNote.FieldName = "hoverNote";
			this.gridColumnHoverNote.Name = "gridColumnHoverNote";
			this.gridColumnHoverNote.OptionsColumn.AllowEdit = false;
			this.gridColumnHoverNote.OptionsColumn.ReadOnly = true;
			this.gridColumnHoverNote.Visible = true;
			this.gridColumnHoverNote.Width = 110;
			// 
			// gridColumnMp4Url
			// 
			this.gridColumnMp4Url.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnMp4Url.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnMp4Url.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnMp4Url.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnMp4Url.Caption = "MP4 Url:";
			this.gridColumnMp4Url.ColumnEdit = this.repositoryItemHyperLinkEdit;
			this.gridColumnMp4Url.FieldName = "mp4Url";
			this.gridColumnMp4Url.Name = "gridColumnMp4Url";
			this.gridColumnMp4Url.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnMp4Url.Visible = true;
			this.gridColumnMp4Url.Width = 312;
			// 
			// repositoryItemHyperLinkEdit
			// 
			this.repositoryItemHyperLinkEdit.AutoHeight = false;
			this.repositoryItemHyperLinkEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesDepot.SiteManager.Properties.Resources.Url, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Copy URL to Clipboard", null, null, true)});
			this.repositoryItemHyperLinkEdit.Name = "repositoryItemHyperLinkEdit";
			this.repositoryItemHyperLinkEdit.SingleClick = true;
			this.repositoryItemHyperLinkEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemHyperLinkEdit_ButtonClick);
			// 
			// gridColumnStation
			// 
			this.gridColumnStation.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnStation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnStation.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnStation.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnStation.Caption = "Station:";
			this.gridColumnStation.FieldName = "station";
			this.gridColumnStation.Name = "gridColumnStation";
			this.gridColumnStation.OptionsColumn.AllowEdit = false;
			this.gridColumnStation.OptionsColumn.ReadOnly = true;
			this.gridColumnStation.Visible = true;
			this.gridColumnStation.Width = 76;
			// 
			// gridColumnThumbUrl
			// 
			this.gridColumnThumbUrl.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnThumbUrl.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnThumbUrl.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnThumbUrl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnThumbUrl.Caption = "PNG Url:";
			this.gridColumnThumbUrl.ColumnEdit = this.repositoryItemHyperLinkEdit;
			this.gridColumnThumbUrl.FieldName = "thumbUrl";
			this.gridColumnThumbUrl.Name = "gridColumnThumbUrl";
			this.gridColumnThumbUrl.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnThumbUrl.Visible = true;
			this.gridColumnThumbUrl.Width = 321;
			// 
			// gridBandMain
			// 
			this.gridBandMain.Caption = "User:";
			this.gridBandMain.Columns.Add(this.gridColumnFileName);
			this.gridBandMain.Columns.Add(this.gridColumnLinkName);
			this.gridBandMain.Columns.Add(this.gridColumnCategoryGroups);
			this.gridBandMain.Columns.Add(this.gridColumnCategoryTags);
			this.gridBandMain.Columns.Add(this.gridColumnKeywords);
			this.gridBandMain.Columns.Add(this.gridColumnNote);
			this.gridBandMain.Columns.Add(this.gridColumnHoverNote);
			this.gridBandMain.Columns.Add(this.gridColumnMp4Url);
			this.gridBandMain.Columns.Add(this.gridColumnThumbUrl);
			this.gridBandMain.Columns.Add(this.gridColumnStation);
			this.gridBandMain.MinWidth = 20;
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 1507;
			// 
			// GroupControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnLinkName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnFileName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnCategoryTags;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnKeywords;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnMp4Url;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnStation;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnCategoryGroups;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnNote;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnHoverNote;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnThumbUrl;
    }
}
