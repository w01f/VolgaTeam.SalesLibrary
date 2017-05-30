namespace SalesLibraries.BatchTagger.PresentationLayer
{
	public partial class LibraryControl
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
			this.gridColumnLinkName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnFileName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnPageName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnExtensionGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnFileExtension = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnLinkAddDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnLinkModifyDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridColumnFileDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
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
			this.advBandedGridViewData.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridViewData.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.advBandedGridViewData.Appearance.Preview.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.Preview.Options.UseForeColor = true;
			this.advBandedGridViewData.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.Row.Options.UseFont = true;
			this.advBandedGridViewData.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewData.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridViewData.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.advBandedGridViewData.AppearancePrint.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.advBandedGridViewData.AppearancePrint.Preview.Options.UseForeColor = true;
			this.advBandedGridViewData.AppearancePrint.Preview.Options.UseTextOptions = true;
			this.advBandedGridViewData.AppearancePrint.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.advBandedGridViewData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandMain});
			this.advBandedGridViewData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnLinkName,
            this.gridColumnFileName,
            this.gridColumnPageName,
            this.gridColumnExtensionGroup,
            this.gridColumnFileExtension,
            this.gridColumnLinkAddDate,
            this.gridColumnLinkModifyDate,
            this.gridColumnFileDate});
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
			this.advBandedGridViewData.OptionsSelection.MultiSelect = true;
			this.advBandedGridViewData.OptionsView.AutoCalcPreviewLineCount = true;
			this.advBandedGridViewData.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridViewData.OptionsView.ShowBands = false;
			this.advBandedGridViewData.OptionsView.ShowDetailButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridViewData.OptionsView.ShowGroupPanel = false;
			this.advBandedGridViewData.OptionsView.ShowIndicator = false;
			this.advBandedGridViewData.OptionsView.ShowPreview = true;
			this.advBandedGridViewData.PreviewFieldName = "Details";
			this.advBandedGridViewData.RowHeight = 30;
			this.advBandedGridViewData.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.OnCustomDrawCell);
			this.advBandedGridViewData.CustomDrawRowPreview += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.OnCustomDrawRowPreview);
			this.advBandedGridViewData.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.OnPopupMenuShowing);
			// 
			// gridBandMain
			// 
			this.gridBandMain.Caption = "User:";
			this.gridBandMain.Columns.Add(this.gridColumnLinkName);
			this.gridBandMain.Columns.Add(this.gridColumnFileName);
			this.gridBandMain.Columns.Add(this.gridColumnPageName);
			this.gridBandMain.Columns.Add(this.gridColumnExtensionGroup);
			this.gridBandMain.Columns.Add(this.gridColumnFileExtension);
			this.gridBandMain.Columns.Add(this.gridColumnLinkAddDate);
			this.gridBandMain.Columns.Add(this.gridColumnLinkModifyDate);
			this.gridBandMain.Columns.Add(this.gridColumnFileDate);
			this.gridBandMain.MinWidth = 20;
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 1077;
			// 
			// gridColumnLinkName
			// 
			this.gridColumnLinkName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnLinkName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnLinkName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnLinkName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnLinkName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnLinkName.Caption = "Link Name:";
			this.gridColumnLinkName.FieldName = "linkName";
			this.gridColumnLinkName.Name = "gridColumnLinkName";
			this.gridColumnLinkName.OptionsColumn.AllowEdit = false;
			this.gridColumnLinkName.OptionsColumn.ReadOnly = true;
			this.gridColumnLinkName.Visible = true;
			this.gridColumnLinkName.Width = 255;
			// 
			// gridColumnFileName
			// 
			this.gridColumnFileName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFileName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnFileName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumnFileName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnFileName.Caption = "File Name:";
			this.gridColumnFileName.FieldName = "fileName";
			this.gridColumnFileName.Name = "gridColumnFileName";
			this.gridColumnFileName.OptionsColumn.AllowEdit = false;
			this.gridColumnFileName.OptionsColumn.ReadOnly = true;
			this.gridColumnFileName.Visible = true;
			this.gridColumnFileName.Width = 173;
			// 
			// gridColumnPageName
			// 
			this.gridColumnPageName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnPageName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnPageName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnPageName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnPageName.Caption = "Page:";
			this.gridColumnPageName.FieldName = "pageName";
			this.gridColumnPageName.Name = "gridColumnPageName";
			this.gridColumnPageName.OptionsColumn.AllowEdit = false;
			this.gridColumnPageName.OptionsColumn.ReadOnly = true;
			this.gridColumnPageName.Visible = true;
			this.gridColumnPageName.Width = 173;
			// 
			// gridColumnExtensionGroup
			// 
			this.gridColumnExtensionGroup.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnExtensionGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnExtensionGroup.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnExtensionGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnExtensionGroup.Caption = "Type:";
			this.gridColumnExtensionGroup.FieldName = "ExtensionGroup";
			this.gridColumnExtensionGroup.Name = "gridColumnExtensionGroup";
			this.gridColumnExtensionGroup.OptionsColumn.AllowEdit = false;
			this.gridColumnExtensionGroup.OptionsColumn.ReadOnly = true;
			this.gridColumnExtensionGroup.Visible = true;
			this.gridColumnExtensionGroup.Width = 91;
			// 
			// gridColumnFileExtension
			// 
			this.gridColumnFileExtension.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFileExtension.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnFileExtension.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnFileExtension.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnFileExtension.Caption = "Ext:";
			this.gridColumnFileExtension.FieldName = "Extension";
			this.gridColumnFileExtension.Name = "gridColumnFileExtension";
			this.gridColumnFileExtension.OptionsColumn.AllowEdit = false;
			this.gridColumnFileExtension.OptionsColumn.ReadOnly = true;
			this.gridColumnFileExtension.Visible = true;
			this.gridColumnFileExtension.Width = 91;
			// 
			// gridColumnLinkAddDate
			// 
			this.gridColumnLinkAddDate.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnLinkAddDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnLinkAddDate.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnLinkAddDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnLinkAddDate.Caption = "Link Added:";
			this.gridColumnLinkAddDate.DisplayFormat.FormatString = "d";
			this.gridColumnLinkAddDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumnLinkAddDate.FieldName = "LinkAddDate";
			this.gridColumnLinkAddDate.Name = "gridColumnLinkAddDate";
			this.gridColumnLinkAddDate.OptionsColumn.AllowEdit = false;
			this.gridColumnLinkAddDate.OptionsColumn.ReadOnly = true;
			this.gridColumnLinkAddDate.Visible = true;
			this.gridColumnLinkAddDate.Width = 91;
			// 
			// gridColumnLinkModifyDate
			// 
			this.gridColumnLinkModifyDate.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnLinkModifyDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnLinkModifyDate.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnLinkModifyDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnLinkModifyDate.Caption = "Link Changed:";
			this.gridColumnLinkModifyDate.DisplayFormat.FormatString = "d";
			this.gridColumnLinkModifyDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumnLinkModifyDate.FieldName = "LinkModifyDate";
			this.gridColumnLinkModifyDate.Name = "gridColumnLinkModifyDate";
			this.gridColumnLinkModifyDate.OptionsColumn.AllowEdit = false;
			this.gridColumnLinkModifyDate.OptionsColumn.ReadOnly = true;
			this.gridColumnLinkModifyDate.Visible = true;
			this.gridColumnLinkModifyDate.Width = 91;
			// 
			// gridColumnFileDate
			// 
			this.gridColumnFileDate.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFileDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnFileDate.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnFileDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnFileDate.Caption = "File Date:";
			this.gridColumnFileDate.DisplayFormat.FormatString = "d";
			this.gridColumnFileDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumnFileDate.FieldName = "FileDate";
			this.gridColumnFileDate.Name = "gridColumnFileDate";
			this.gridColumnFileDate.OptionsColumn.AllowEdit = false;
			this.gridColumnFileDate.OptionsColumn.ReadOnly = true;
			this.gridColumnFileDate.Visible = true;
			this.gridColumnFileDate.Width = 112;
			// 
			// repositoryItemHyperLinkEdit
			// 
			this.repositoryItemHyperLinkEdit.AutoHeight = false;
			this.repositoryItemHyperLinkEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.BatchTagger.Properties.Resources.Url, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Copy URL to Clipboard", null, null, true)});
			this.repositoryItemHyperLinkEdit.Name = "repositoryItemHyperLinkEdit";
			this.repositoryItemHyperLinkEdit.SingleClick = true;
			// 
			// LibraryControl
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
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnLinkName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnFileName;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnFileExtension;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnFileDate;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnExtensionGroup;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnLinkAddDate;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnLinkModifyDate;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnPageName;
	}
}
