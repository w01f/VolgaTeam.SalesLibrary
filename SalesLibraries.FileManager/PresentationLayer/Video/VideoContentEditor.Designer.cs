namespace SalesLibraries.FileManager.PresentationLayer.Video
{
	partial class VideoContentEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoContentEditor));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
			this.pnVideo = new System.Windows.Forms.Panel();
			this.pnVideoMain = new System.Windows.Forms.Panel();
			this.gridControlVideo = new DevExpress.XtraGrid.GridControl();
			this.gridViewVideo = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnVideoIndex = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnVideoMp4FileInfo = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditVideoMp4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnVideoIPadFolder = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditVideoFolderEnabled = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnVideoSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnVideoSourceFolder = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnVideoSourceFileInfo = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.gridColumnVideoConvert = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditVideoConvertDisabled = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnVideoRefresh = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditVideoRefersh = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repositoryItemButtonEditVideoConvertEnabled = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repositoryItemButtonEditVideoFolderDisabled = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.pnVideoTop = new System.Windows.Forms.Panel();
			this.labelControlMp4ConversionWarning = new DevExpress.XtraEditors.LabelControl();
			this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.laVideoTitle = new System.Windows.Forms.Label();
			this.gridColumnVideoLength = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnVideoWidth = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnVideoHeight = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnVideo.SuspendLayout();
			this.pnVideoMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlVideo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewVideo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoMp4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoFolderEnabled)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoConvertDisabled)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoRefersh)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoConvertEnabled)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoFolderDisabled)).BeginInit();
			this.pnVideoTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnVideo
			// 
			this.pnVideo.BackColor = System.Drawing.Color.White;
			this.pnVideo.Controls.Add(this.pnVideoMain);
			this.pnVideo.Controls.Add(this.pnVideoTop);
			this.pnVideo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnVideo.Location = new System.Drawing.Point(0, 0);
			this.pnVideo.Name = "pnVideo";
			this.pnVideo.Size = new System.Drawing.Size(1128, 637);
			this.pnVideo.TabIndex = 1;
			// 
			// pnVideoMain
			// 
			this.pnVideoMain.Controls.Add(this.gridControlVideo);
			this.pnVideoMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnVideoMain.Location = new System.Drawing.Point(0, 55);
			this.pnVideoMain.Name = "pnVideoMain";
			this.pnVideoMain.Size = new System.Drawing.Size(1128, 582);
			this.pnVideoMain.TabIndex = 1;
			// 
			// gridControlVideo
			// 
			this.gridControlVideo.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlVideo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlVideo.Location = new System.Drawing.Point(0, 0);
			this.gridControlVideo.MainView = this.gridViewVideo;
			this.gridControlVideo.Name = "gridControlVideo";
			this.gridControlVideo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditVideoMp4,
            this.repositoryItemButtonEditVideoFolderEnabled,
            this.repositoryItemCheckEdit,
            this.repositoryItemButtonEditVideoConvertDisabled,
            this.repositoryItemButtonEditVideoConvertEnabled,
            this.repositoryItemButtonEditVideoFolderDisabled,
            this.repositoryItemButtonEditVideoRefersh,
            this.repositoryItemTextEdit});
			this.gridControlVideo.Size = new System.Drawing.Size(1128, 582);
			this.gridControlVideo.TabIndex = 0;
			this.gridControlVideo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewVideo});
			// 
			// gridViewVideo
			// 
			this.gridViewVideo.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewVideo.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewVideo.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewVideo.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewVideo.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewVideo.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewVideo.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewVideo.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewVideo.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewVideo.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewVideo.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewVideo.Appearance.OddRow.Options.UseFont = true;
			this.gridViewVideo.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewVideo.Appearance.Row.Options.UseFont = true;
			this.gridViewVideo.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewVideo.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewVideo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnVideoIndex,
            this.gridColumnVideoMp4FileInfo,
            this.gridColumnVideoIPadFolder,
            this.gridColumnVideoSelected,
            this.gridColumnVideoSourceFolder,
            this.gridColumnVideoSourceFileInfo,
            this.gridColumnVideoConvert,
            this.gridColumnVideoRefresh,
            this.gridColumnVideoLength,
            this.gridColumnVideoWidth,
            this.gridColumnVideoHeight});
			this.gridViewVideo.GridControl = this.gridControlVideo;
			this.gridViewVideo.Name = "gridViewVideo";
			this.gridViewVideo.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewVideo.OptionsCustomization.AllowFilter = false;
			this.gridViewVideo.OptionsCustomization.AllowSort = false;
			this.gridViewVideo.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewVideo.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewVideo.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewVideo.OptionsView.ShowDetailButtons = false;
			this.gridViewVideo.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewVideo.OptionsView.ShowGroupPanel = false;
			this.gridViewVideo.OptionsView.ShowIndicator = false;
			this.gridViewVideo.RowHeight = 20;
			this.gridViewVideo.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewVideo_RowCellStyle);
			this.gridViewVideo.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewVideo_CustomRowCellEdit);
			this.gridViewVideo.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewVideo_CustomRowCellEditForEditing);
			// 
			// gridColumnVideoIndex
			// 
			this.gridColumnVideoIndex.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnVideoIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnVideoIndex.Caption = "#";
			this.gridColumnVideoIndex.FieldName = "Index";
			this.gridColumnVideoIndex.Name = "gridColumnVideoIndex";
			this.gridColumnVideoIndex.OptionsColumn.AllowEdit = false;
			this.gridColumnVideoIndex.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoIndex.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoIndex.Visible = true;
			this.gridColumnVideoIndex.VisibleIndex = 1;
			this.gridColumnVideoIndex.Width = 45;
			// 
			// gridColumnVideoMp4FileInfo
			// 
			this.gridColumnVideoMp4FileInfo.Caption = "MP4 for iPad";
			this.gridColumnVideoMp4FileInfo.ColumnEdit = this.repositoryItemButtonEditVideoMp4;
			this.gridColumnVideoMp4FileInfo.FieldName = "Mp4FileInfo";
			this.gridColumnVideoMp4FileInfo.Name = "gridColumnVideoMp4FileInfo";
			this.gridColumnVideoMp4FileInfo.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoMp4FileInfo.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnVideoMp4FileInfo.Visible = true;
			this.gridColumnVideoMp4FileInfo.VisibleIndex = 3;
			this.gridColumnVideoMp4FileInfo.Width = 197;
			// 
			// repositoryItemButtonEditVideoMp4
			// 
			this.repositoryItemButtonEditVideoMp4.AllowFocused = false;
			this.repositoryItemButtonEditVideoMp4.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.repositoryItemButtonEditVideoMp4.AutoHeight = false;
			this.repositoryItemButtonEditVideoMp4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditVideoMp4.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.repositoryItemButtonEditVideoMp4.Name = "repositoryItemButtonEditVideoMp4";
			this.repositoryItemButtonEditVideoMp4.NullText = "MISSING!";
			this.repositoryItemButtonEditVideoMp4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEditVideoMp4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditVideoMp4_ButtonClick);
			this.repositoryItemButtonEditVideoMp4.Click += new System.EventHandler(this.repositoryItemButtonEditVideoMp4_Click);
			// 
			// gridColumnVideoIPadFolder
			// 
			this.gridColumnVideoIPadFolder.Caption = "Output";
			this.gridColumnVideoIPadFolder.ColumnEdit = this.repositoryItemButtonEditVideoFolderEnabled;
			this.gridColumnVideoIPadFolder.Name = "gridColumnVideoIPadFolder";
			this.gridColumnVideoIPadFolder.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoIPadFolder.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoIPadFolder.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnVideoIPadFolder.Visible = true;
			this.gridColumnVideoIPadFolder.VisibleIndex = 7;
			this.gridColumnVideoIPadFolder.Width = 73;
			// 
			// repositoryItemButtonEditVideoFolderEnabled
			// 
			this.repositoryItemButtonEditVideoFolderEnabled.AllowFocused = false;
			this.repositoryItemButtonEditVideoFolderEnabled.AutoHeight = false;
			this.repositoryItemButtonEditVideoFolderEnabled.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonOpen, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemButtonEditVideoFolderEnabled.Name = "repositoryItemButtonEditVideoFolderEnabled";
			this.repositoryItemButtonEditVideoFolderEnabled.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditVideoFolderEnabled.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditVideoFolder_ButtonClick);
			// 
			// gridColumnVideoSelected
			// 
			this.gridColumnVideoSelected.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnVideoSelected.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnVideoSelected.ColumnEdit = this.repositoryItemCheckEdit;
			this.gridColumnVideoSelected.FieldName = "Selected";
			this.gridColumnVideoSelected.Name = "gridColumnVideoSelected";
			this.gridColumnVideoSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoSelected.OptionsColumn.ShowCaption = false;
			this.gridColumnVideoSelected.Visible = true;
			this.gridColumnVideoSelected.VisibleIndex = 0;
			this.gridColumnVideoSelected.Width = 30;
			// 
			// repositoryItemCheckEdit
			// 
			this.repositoryItemCheckEdit.AllowFocused = false;
			this.repositoryItemCheckEdit.AutoHeight = false;
			this.repositoryItemCheckEdit.Caption = "Check";
			this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
			this.repositoryItemCheckEdit.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit_EditValueChanged);
			// 
			// gridColumnVideoSourceFolder
			// 
			this.gridColumnVideoSourceFolder.Caption = "Source";
			this.gridColumnVideoSourceFolder.ColumnEdit = this.repositoryItemButtonEditVideoFolderEnabled;
			this.gridColumnVideoSourceFolder.Name = "gridColumnVideoSourceFolder";
			this.gridColumnVideoSourceFolder.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoSourceFolder.Visible = true;
			this.gridColumnVideoSourceFolder.VisibleIndex = 8;
			this.gridColumnVideoSourceFolder.Width = 64;
			// 
			// gridColumnVideoSourceFileInfo
			// 
			this.gridColumnVideoSourceFileInfo.Caption = "Source File";
			this.gridColumnVideoSourceFileInfo.ColumnEdit = this.repositoryItemTextEdit;
			this.gridColumnVideoSourceFileInfo.FieldName = "SourceFileInfo";
			this.gridColumnVideoSourceFileInfo.Name = "gridColumnVideoSourceFileInfo";
			this.gridColumnVideoSourceFileInfo.OptionsColumn.AllowEdit = false;
			this.gridColumnVideoSourceFileInfo.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoSourceFileInfo.Visible = true;
			this.gridColumnVideoSourceFileInfo.VisibleIndex = 2;
			this.gridColumnVideoSourceFileInfo.Width = 165;
			// 
			// repositoryItemTextEdit
			// 
			this.repositoryItemTextEdit.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.repositoryItemTextEdit.AutoHeight = false;
			this.repositoryItemTextEdit.Name = "repositoryItemTextEdit";
			// 
			// gridColumnVideoConvert
			// 
			this.gridColumnVideoConvert.Caption = "Convert";
			this.gridColumnVideoConvert.ColumnEdit = this.repositoryItemButtonEditVideoConvertDisabled;
			this.gridColumnVideoConvert.Name = "gridColumnVideoConvert";
			this.gridColumnVideoConvert.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoConvert.Visible = true;
			this.gridColumnVideoConvert.VisibleIndex = 9;
			this.gridColumnVideoConvert.Width = 63;
			// 
			// repositoryItemButtonEditVideoConvertDisabled
			// 
			this.repositoryItemButtonEditVideoConvertDisabled.AutoHeight = false;
			this.repositoryItemButtonEditVideoConvertDisabled.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonVideoConvert, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
			this.repositoryItemButtonEditVideoConvertDisabled.Name = "repositoryItemButtonEditVideoConvertDisabled";
			this.repositoryItemButtonEditVideoConvertDisabled.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// gridColumnVideoRefresh
			// 
			this.gridColumnVideoRefresh.Caption = "Refresh";
			this.gridColumnVideoRefresh.ColumnEdit = this.repositoryItemButtonEditVideoRefersh;
			this.gridColumnVideoRefresh.Name = "gridColumnVideoRefresh";
			this.gridColumnVideoRefresh.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoRefresh.Visible = true;
			this.gridColumnVideoRefresh.VisibleIndex = 10;
			this.gridColumnVideoRefresh.Width = 63;
			// 
			// repositoryItemButtonEditVideoRefersh
			// 
			this.repositoryItemButtonEditVideoRefersh.AutoHeight = false;
			this.repositoryItemButtonEditVideoRefersh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonVideoReset, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
			this.repositoryItemButtonEditVideoRefersh.Name = "repositoryItemButtonEditVideoRefersh";
			this.repositoryItemButtonEditVideoRefersh.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditVideoRefersh.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditVideoRefersh_ButtonClick);
			// 
			// repositoryItemButtonEditVideoConvertEnabled
			// 
			this.repositoryItemButtonEditVideoConvertEnabled.AutoHeight = false;
			this.repositoryItemButtonEditVideoConvertEnabled.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonVideoConvert, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", null, null, true)});
			this.repositoryItemButtonEditVideoConvertEnabled.Name = "repositoryItemButtonEditVideoConvertEnabled";
			this.repositoryItemButtonEditVideoConvertEnabled.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditVideoConvertEnabled.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditVideoConvert_ButtonClick);
			// 
			// repositoryItemButtonEditVideoFolderDisabled
			// 
			this.repositoryItemButtonEditVideoFolderDisabled.AutoHeight = false;
			this.repositoryItemButtonEditVideoFolderDisabled.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonOpen, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "", null, null, true)});
			this.repositoryItemButtonEditVideoFolderDisabled.Name = "repositoryItemButtonEditVideoFolderDisabled";
			this.repositoryItemButtonEditVideoFolderDisabled.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// pnVideoTop
			// 
			this.pnVideoTop.Controls.Add(this.labelControlMp4ConversionWarning);
			this.pnVideoTop.Controls.Add(this.buttonXClearAll);
			this.pnVideoTop.Controls.Add(this.buttonXSelectAll);
			this.pnVideoTop.Controls.Add(this.laVideoTitle);
			this.pnVideoTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnVideoTop.Location = new System.Drawing.Point(0, 0);
			this.pnVideoTop.Name = "pnVideoTop";
			this.pnVideoTop.Size = new System.Drawing.Size(1128, 55);
			this.pnVideoTop.TabIndex = 0;
			// 
			// labelControlMp4ConversionWarning
			// 
			this.labelControlMp4ConversionWarning.AllowHtmlString = true;
			this.labelControlMp4ConversionWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlMp4ConversionWarning.Appearance.Font = new System.Drawing.Font("Arial", 14.25F);
			this.labelControlMp4ConversionWarning.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.labelControlMp4ConversionWarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlMp4ConversionWarning.Location = new System.Drawing.Point(477, 7);
			this.labelControlMp4ConversionWarning.Name = "labelControlMp4ConversionWarning";
			this.labelControlMp4ConversionWarning.Size = new System.Drawing.Size(435, 40);
			this.labelControlMp4ConversionWarning.TabIndex = 3;
			this.labelControlMp4ConversionWarning.Text = "<i><color=red>MP4 Conversions Needed: {0}</color></i>";
			// 
			// buttonXClearAll
			// 
			this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClearAll.Location = new System.Drawing.Point(1034, 7);
			this.buttonXClearAll.Name = "buttonXClearAll";
			this.buttonXClearAll.Size = new System.Drawing.Size(87, 40);
			this.buttonXClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClearAll.TabIndex = 2;
			this.buttonXClearAll.Text = "Clear All";
			this.buttonXClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXClearAll.Click += new System.EventHandler(this.buttonXClearAll_Click);
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Location = new System.Drawing.Point(935, 7);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(87, 40);
			this.buttonXSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectAll.TabIndex = 1;
			this.buttonXSelectAll.Text = "Select All";
			this.buttonXSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectAll.Click += new System.EventHandler(this.buttonXSelectAll_Click);
			// 
			// laVideoTitle
			// 
			this.laVideoTitle.AutoSize = true;
			this.laVideoTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laVideoTitle.Location = new System.Drawing.Point(3, 16);
			this.laVideoTitle.Name = "laVideoTitle";
			this.laVideoTitle.Size = new System.Drawing.Size(286, 22);
			this.laVideoTitle.TabIndex = 0;
			this.laVideoTitle.Text = "Your Library has {0} Video File{1}";
			// 
			// gridColumnVideoLength
			// 
			this.gridColumnVideoLength.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnVideoLength.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnVideoLength.Caption = "Length";
			this.gridColumnVideoLength.FieldName = "Length";
			this.gridColumnVideoLength.Name = "gridColumnVideoLength";
			this.gridColumnVideoLength.OptionsColumn.AllowEdit = false;
			this.gridColumnVideoLength.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoLength.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoLength.Visible = true;
			this.gridColumnVideoLength.VisibleIndex = 6;
			// 
			// gridColumnVideoWidth
			// 
			this.gridColumnVideoWidth.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnVideoWidth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnVideoWidth.Caption = "Width";
			this.gridColumnVideoWidth.FieldName = "Width";
			this.gridColumnVideoWidth.Name = "gridColumnVideoWidth";
			this.gridColumnVideoWidth.OptionsColumn.AllowEdit = false;
			this.gridColumnVideoWidth.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoWidth.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoWidth.Visible = true;
			this.gridColumnVideoWidth.VisibleIndex = 4;
			// 
			// gridColumnVideoHeight
			// 
			this.gridColumnVideoHeight.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnVideoHeight.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnVideoHeight.Caption = "Height";
			this.gridColumnVideoHeight.FieldName = "Height";
			this.gridColumnVideoHeight.Name = "gridColumnVideoHeight";
			this.gridColumnVideoHeight.OptionsColumn.AllowEdit = false;
			this.gridColumnVideoHeight.OptionsColumn.FixedWidth = true;
			this.gridColumnVideoHeight.OptionsColumn.ReadOnly = true;
			this.gridColumnVideoHeight.Visible = true;
			this.gridColumnVideoHeight.VisibleIndex = 5;
			// 
			// VideoContentEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.pnVideo);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "VideoContentEditor";
			this.Size = new System.Drawing.Size(1128, 637);
			this.pnVideo.ResumeLayout(false);
			this.pnVideoMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlVideo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewVideo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoMp4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoFolderEnabled)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoConvertDisabled)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoRefersh)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoConvertEnabled)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditVideoFolderDisabled)).EndInit();
			this.pnVideoTop.ResumeLayout(false);
			this.pnVideoTop.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnVideo;
		private System.Windows.Forms.Panel pnVideoMain;
		private DevExpress.XtraGrid.GridControl gridControlVideo;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewVideo;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoIndex;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoMp4FileInfo;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditVideoMp4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoIPadFolder;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditVideoFolderEnabled;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoSourceFolder;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoSourceFileInfo;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoConvert;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditVideoConvertDisabled;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoRefresh;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditVideoRefersh;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditVideoConvertEnabled;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditVideoFolderDisabled;
		private System.Windows.Forms.Panel pnVideoTop;
		private DevComponents.DotNetBar.ButtonX buttonXClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXSelectAll;
		private System.Windows.Forms.Label laVideoTitle;
		private DevExpress.XtraEditors.LabelControl labelControlMp4ConversionWarning;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoLength;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoWidth;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideoHeight;
	}
}
