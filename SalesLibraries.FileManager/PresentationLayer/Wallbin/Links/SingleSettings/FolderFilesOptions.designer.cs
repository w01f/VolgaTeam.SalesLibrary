namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class FolderFilesOptions
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnFileName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnOperations = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemButtonEditOperations = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barToolbar = new DevExpress.XtraBars.Bar();
			this.barLargeButtonItemSyncSettings = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemTags = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barSubItemSelectByType = new DevExpress.XtraBars.BarSubItem();
			this.barLargeButtonItemSelectPowerPoint = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemSelectPdf = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemSelectWord = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemSelectExcel = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemSelectVideo = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemSelectOther = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditOperations)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 44);
			this.gridControl.MainView = this.advBandedGridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditOperations});
			this.gridControl.Size = new System.Drawing.Size(531, 497);
			this.gridControl.TabIndex = 0;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView});
			// 
			// advBandedGridView
			// 
			this.advBandedGridView.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView.Appearance.BandPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.OddRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.Row.Options.UseFont = true;
			this.advBandedGridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandMain});
			this.advBandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnFileName,
            this.bandedGridColumnOperations});
			this.advBandedGridView.GridControl = this.gridControl;
			this.advBandedGridView.Name = "advBandedGridView";
			this.advBandedGridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridView.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridView.OptionsBehavior.AutoPopulateColumns = false;
			this.advBandedGridView.OptionsCustomization.AllowBandMoving = false;
			this.advBandedGridView.OptionsCustomization.AllowBandResizing = false;
			this.advBandedGridView.OptionsCustomization.AllowChangeBandParent = true;
			this.advBandedGridView.OptionsCustomization.AllowChangeColumnParent = true;
			this.advBandedGridView.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridView.OptionsCustomization.AllowColumnResizing = false;
			this.advBandedGridView.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.advBandedGridView.OptionsCustomization.AllowSort = false;
			this.advBandedGridView.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView.OptionsSelection.MultiSelect = true;
			this.advBandedGridView.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridView.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView.OptionsView.ShowBands = false;
			this.advBandedGridView.OptionsView.ShowColumnHeaders = false;
			this.advBandedGridView.OptionsView.ShowDetailButtons = false;
			this.advBandedGridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridView.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView.OptionsView.ShowIndicator = false;
			this.advBandedGridView.RowHeight = 30;
			this.advBandedGridView.RowSeparatorHeight = 5;
			// 
			// gridBandMain
			// 
			this.gridBandMain.Caption = "Main";
			this.gridBandMain.Columns.Add(this.bandedGridColumnFileName);
			this.gridBandMain.Columns.Add(this.bandedGridColumnOperations);
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 736;
			// 
			// bandedGridColumnFileName
			// 
			this.bandedGridColumnFileName.Caption = "File";
			this.bandedGridColumnFileName.FieldName = "NameWithExtension";
			this.bandedGridColumnFileName.Name = "bandedGridColumnFileName";
			this.bandedGridColumnFileName.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnFileName.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnFileName.Visible = true;
			this.bandedGridColumnFileName.Width = 656;
			// 
			// bandedGridColumnOperations
			// 
			this.bandedGridColumnOperations.ColumnEdit = this.repositoryItemButtonEditOperations;
			this.bandedGridColumnOperations.Name = "bandedGridColumnOperations";
			this.bandedGridColumnOperations.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnOperations.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.bandedGridColumnOperations.Visible = true;
			this.bandedGridColumnOperations.Width = 80;
			// 
			// repositoryItemButtonEditOperations
			// 
			this.repositoryItemButtonEditOperations.AutoHeight = false;
			this.repositoryItemButtonEditOperations.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderSync, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Edit Sync Settings", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderTags, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Edit Tags", null, null, true)});
			this.repositoryItemButtonEditOperations.Name = "repositoryItemButtonEditOperations";
			this.repositoryItemButtonEditOperations.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditOperations.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditOperations_ButtonClick);
			// 
			// barManager
			// 
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barToolbar});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemSyncSettings,
            this.barLargeButtonItemTags,
            this.barSubItemSelectByType,
            this.barLargeButtonItemSelectPowerPoint,
            this.barLargeButtonItemSelectPdf,
            this.barLargeButtonItemSelectWord,
            this.barLargeButtonItemSelectExcel,
            this.barLargeButtonItemSelectVideo,
            this.barLargeButtonItemSelectOther});
			this.barManager.MaxItemId = 14;
			// 
			// barToolbar
			// 
			this.barToolbar.BarName = "Tools";
			this.barToolbar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barToolbar.DockCol = 0;
			this.barToolbar.DockRow = 0;
			this.barToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemSyncSettings),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemTags),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItemSelectByType, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.barToolbar.OptionsBar.AllowQuickCustomization = false;
			this.barToolbar.OptionsBar.DisableClose = true;
			this.barToolbar.OptionsBar.DisableCustomization = true;
			this.barToolbar.OptionsBar.DrawBorder = false;
			this.barToolbar.OptionsBar.DrawDragBorder = false;
			this.barToolbar.OptionsBar.UseWholeRow = true;
			this.barToolbar.Text = "Tools";
			// 
			// barLargeButtonItemSyncSettings
			// 
			this.barLargeButtonItemSyncSettings.Caption = "Edit Sync Settings";
			this.barLargeButtonItemSyncSettings.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderSync;
			this.barLargeButtonItemSyncSettings.Hint = "Edit Sync Settings for selected files";
			this.barLargeButtonItemSyncSettings.Id = 0;
			this.barLargeButtonItemSyncSettings.Name = "barLargeButtonItemSyncSettings";
			this.barLargeButtonItemSyncSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemSyncSettings_ItemClick);
			// 
			// barLargeButtonItemTags
			// 
			this.barLargeButtonItemTags.Caption = "Edit Tags";
			this.barLargeButtonItemTags.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderTags;
			this.barLargeButtonItemTags.Hint = "Edit Tags for selected files";
			this.barLargeButtonItemTags.Id = 1;
			this.barLargeButtonItemTags.Name = "barLargeButtonItemTags";
			this.barLargeButtonItemTags.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemTags_ItemClick);
			// 
			// barSubItemSelectByType
			// 
			this.barSubItemSelectByType.Caption = "Select Links by Type";
			this.barSubItemSelectByType.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderSelectByType;
			this.barSubItemSelectByType.Id = 3;
			this.barSubItemSelectByType.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Immediate;
			this.barSubItemSelectByType.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemSelectPowerPoint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemSelectPdf),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemSelectWord),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemSelectExcel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemSelectVideo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemSelectOther)});
			this.barSubItemSelectByType.Name = "barSubItemSelectByType";
			// 
			// barLargeButtonItemSelectPowerPoint
			// 
			this.barLargeButtonItemSelectPowerPoint.Caption = "All PowerPoint";
			this.barLargeButtonItemSelectPowerPoint.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderPptx;
			this.barLargeButtonItemSelectPowerPoint.Id = 5;
			this.barLargeButtonItemSelectPowerPoint.Name = "barLargeButtonItemSelectPowerPoint";
			this.barLargeButtonItemSelectPowerPoint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemSelectPowerPoint_ItemClick);
			// 
			// barLargeButtonItemSelectPdf
			// 
			this.barLargeButtonItemSelectPdf.Caption = "All PDF";
			this.barLargeButtonItemSelectPdf.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderPdf;
			this.barLargeButtonItemSelectPdf.Id = 6;
			this.barLargeButtonItemSelectPdf.Name = "barLargeButtonItemSelectPdf";
			this.barLargeButtonItemSelectPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemSelectPdf_ItemClick);
			// 
			// barLargeButtonItemSelectWord
			// 
			this.barLargeButtonItemSelectWord.Caption = "All Word";
			this.barLargeButtonItemSelectWord.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderDocx;
			this.barLargeButtonItemSelectWord.Id = 7;
			this.barLargeButtonItemSelectWord.Name = "barLargeButtonItemSelectWord";
			this.barLargeButtonItemSelectWord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemSelectWord_ItemClick);
			// 
			// barLargeButtonItemSelectExcel
			// 
			this.barLargeButtonItemSelectExcel.Caption = "All Excel";
			this.barLargeButtonItemSelectExcel.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderXlsx;
			this.barLargeButtonItemSelectExcel.Id = 8;
			this.barLargeButtonItemSelectExcel.Name = "barLargeButtonItemSelectExcel";
			this.barLargeButtonItemSelectExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemSelectExcel_ItemClick);
			// 
			// barLargeButtonItemSelectVideo
			// 
			this.barLargeButtonItemSelectVideo.Caption = "All Video";
			this.barLargeButtonItemSelectVideo.Glyph = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsFolderWmv;
			this.barLargeButtonItemSelectVideo.Id = 9;
			this.barLargeButtonItemSelectVideo.Name = "barLargeButtonItemSelectVideo";
			this.barLargeButtonItemSelectVideo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemSelectVideo_ItemClick);
			// 
			// barLargeButtonItemSelectOther
			// 
			this.barLargeButtonItemSelectOther.Caption = "All Other Files";
			this.barLargeButtonItemSelectOther.Id = 11;
			this.barLargeButtonItemSelectOther.Name = "barLargeButtonItemSelectOther";
			this.barLargeButtonItemSelectOther.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemSelectOther_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(531, 44);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 541);
			this.barDockControlBottom.Size = new System.Drawing.Size(531, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 44);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 497);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(531, 44);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 497);
			// 
			// FolderFilesOptions
			// 
			this.Controls.Add(this.gridControl);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FolderFilesOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditOperations)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnFileName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnOperations;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditOperations;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.Bar barToolbar;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemSyncSettings;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemTags;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarSubItem barSubItemSelectByType;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemSelectPowerPoint;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemSelectPdf;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemSelectWord;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemSelectExcel;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemSelectVideo;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemSelectOther;
	}
}
