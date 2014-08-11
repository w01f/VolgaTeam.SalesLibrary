namespace FileManager.PresentationClasses.Tags
{
	partial class AttachmentsEditor
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.laHeader = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageFiles = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlAttachmentsFiles = new DevExpress.XtraGrid.GridControl();
			this.gridViewAttachmentsFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnAttachmentsFilesName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditAttachmentsFiles = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.buttonXAttachmentsFilesAdd = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlAttachmentsWeb = new DevExpress.XtraGrid.GridControl();
			this.gridViewAttachmentsWeb = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnAttachmentsWebSourcePath = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditAttachmentsWeb = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.buttonXAttachmentsWebAdd = new DevComponents.DotNetBar.ButtonX();
			this.checkBoxEnableAttachmnets = new System.Windows.Forms.CheckBox();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageFiles.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlAttachmentsFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewAttachmentsFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditAttachmentsFiles)).BeginInit();
			this.xtraTabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlAttachmentsWeb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewAttachmentsWeb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditAttachmentsWeb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// laHeader
			// 
			this.laHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.laHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laHeader.Location = new System.Drawing.Point(0, 0);
			this.laHeader.Name = "laHeader";
			this.laHeader.Size = new System.Drawing.Size(350, 24);
			this.laHeader.TabIndex = 0;
			this.laHeader.Text = "Manage Attachments";
			this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 24);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(350, 549);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.xtraTabControl);
			this.pnData.Controls.Add(this.checkBoxEnableAttachmnets);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 0);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(350, 549);
			this.pnData.TabIndex = 1;
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Enabled = false;
			this.xtraTabControl.Location = new System.Drawing.Point(3, 51);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageFiles;
			this.xtraTabControl.Size = new System.Drawing.Size(344, 495);
			this.xtraTabControl.TabIndex = 6;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageFiles,
            this.xtraTabPage2});
			// 
			// xtraTabPageFiles
			// 
			this.xtraTabPageFiles.Controls.Add(this.gridControlAttachmentsFiles);
			this.xtraTabPageFiles.Controls.Add(this.buttonXAttachmentsFilesAdd);
			this.xtraTabPageFiles.Name = "xtraTabPageFiles";
			this.xtraTabPageFiles.Size = new System.Drawing.Size(342, 467);
			this.xtraTabPageFiles.Text = "File Attachments";
			// 
			// gridControlAttachmentsFiles
			// 
			this.gridControlAttachmentsFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlAttachmentsFiles.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlAttachmentsFiles.Location = new System.Drawing.Point(3, 49);
			this.gridControlAttachmentsFiles.MainView = this.gridViewAttachmentsFiles;
			this.gridControlAttachmentsFiles.Name = "gridControlAttachmentsFiles";
			this.gridControlAttachmentsFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditAttachmentsFiles});
			this.gridControlAttachmentsFiles.Size = new System.Drawing.Size(336, 417);
			this.gridControlAttachmentsFiles.TabIndex = 10;
			this.gridControlAttachmentsFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAttachmentsFiles});
			// 
			// gridViewAttachmentsFiles
			// 
			this.gridViewAttachmentsFiles.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAttachmentsFiles.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewAttachmentsFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewAttachmentsFiles.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewAttachmentsFiles.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewAttachmentsFiles.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewAttachmentsFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAttachmentsFiles.Appearance.Row.Options.UseFont = true;
			this.gridViewAttachmentsFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAttachmentsFiles.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewAttachmentsFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnAttachmentsFilesName});
			this.gridViewAttachmentsFiles.GridControl = this.gridControlAttachmentsFiles;
			this.gridViewAttachmentsFiles.Name = "gridViewAttachmentsFiles";
			this.gridViewAttachmentsFiles.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewAttachmentsFiles.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewAttachmentsFiles.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewAttachmentsFiles.OptionsCustomization.AllowFilter = false;
			this.gridViewAttachmentsFiles.OptionsCustomization.AllowGroup = false;
			this.gridViewAttachmentsFiles.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewAttachmentsFiles.OptionsCustomization.AllowSort = false;
			this.gridViewAttachmentsFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewAttachmentsFiles.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewAttachmentsFiles.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewAttachmentsFiles.OptionsView.ShowColumnHeaders = false;
			this.gridViewAttachmentsFiles.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewAttachmentsFiles.OptionsView.ShowGroupPanel = false;
			this.gridViewAttachmentsFiles.OptionsView.ShowIndicator = false;
			this.gridViewAttachmentsFiles.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewAttachmentsFiles.RowHeight = 35;
			this.gridViewAttachmentsFiles.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewAttachmentsFiles_RowCellStyle);
			// 
			// gridColumnAttachmentsFilesName
			// 
			this.gridColumnAttachmentsFilesName.Caption = "Name";
			this.gridColumnAttachmentsFilesName.ColumnEdit = this.repositoryItemButtonEditAttachmentsFiles;
			this.gridColumnAttachmentsFilesName.FieldName = "Name";
			this.gridColumnAttachmentsFilesName.Name = "gridColumnAttachmentsFilesName";
			this.gridColumnAttachmentsFilesName.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnAttachmentsFilesName.Visible = true;
			this.gridColumnAttachmentsFilesName.VisibleIndex = 0;
			// 
			// repositoryItemButtonEditAttachmentsFiles
			// 
			this.repositoryItemButtonEditAttachmentsFiles.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditAttachmentsFiles.Appearance.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsFiles.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditAttachmentsFiles.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsFiles.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditAttachmentsFiles.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsFiles.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditAttachmentsFiles.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsFiles.AutoHeight = false;
			this.repositoryItemButtonEditAttachmentsFiles.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::FileManager.Properties.Resources.OpenVideoSmall, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::FileManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemButtonEditAttachmentsFiles.Name = "repositoryItemButtonEditAttachmentsFiles";
			this.repositoryItemButtonEditAttachmentsFiles.NullText = "Select File...";
			this.repositoryItemButtonEditAttachmentsFiles.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEditAttachmentsFiles.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditAttachmentsFiles_ButtonClick);
			// 
			// buttonXAttachmentsFilesAdd
			// 
			this.buttonXAttachmentsFilesAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAttachmentsFilesAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAttachmentsFilesAdd.Image = global::FileManager.Properties.Resources.PlusButton;
			this.buttonXAttachmentsFilesAdd.ImageFixedSize = new System.Drawing.Size(24, 24);
			this.buttonXAttachmentsFilesAdd.Location = new System.Drawing.Point(3, 9);
			this.buttonXAttachmentsFilesAdd.Name = "buttonXAttachmentsFilesAdd";
			this.buttonXAttachmentsFilesAdd.Size = new System.Drawing.Size(151, 34);
			this.buttonXAttachmentsFilesAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAttachmentsFilesAdd.TabIndex = 9;
			this.buttonXAttachmentsFilesAdd.Text = "Attach File";
			this.buttonXAttachmentsFilesAdd.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXAttachmentsFilesAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXAttachmentsFilesAdd.Click += new System.EventHandler(this.buttonXAttachmentsFilesAdd_Click);
			// 
			// xtraTabPage2
			// 
			this.xtraTabPage2.Controls.Add(this.gridControlAttachmentsWeb);
			this.xtraTabPage2.Controls.Add(this.buttonXAttachmentsWebAdd);
			this.xtraTabPage2.Name = "xtraTabPage2";
			this.xtraTabPage2.Size = new System.Drawing.Size(342, 467);
			this.xtraTabPage2.Text = "URL Attachments";
			// 
			// gridControlAttachmentsWeb
			// 
			this.gridControlAttachmentsWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlAttachmentsWeb.Location = new System.Drawing.Point(3, 49);
			this.gridControlAttachmentsWeb.MainView = this.gridViewAttachmentsWeb;
			this.gridControlAttachmentsWeb.Name = "gridControlAttachmentsWeb";
			this.gridControlAttachmentsWeb.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditAttachmentsWeb});
			this.gridControlAttachmentsWeb.Size = new System.Drawing.Size(336, 417);
			this.gridControlAttachmentsWeb.TabIndex = 12;
			this.gridControlAttachmentsWeb.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAttachmentsWeb});
			// 
			// gridViewAttachmentsWeb
			// 
			this.gridViewAttachmentsWeb.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAttachmentsWeb.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewAttachmentsWeb.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewAttachmentsWeb.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewAttachmentsWeb.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewAttachmentsWeb.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewAttachmentsWeb.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAttachmentsWeb.Appearance.Row.Options.UseFont = true;
			this.gridViewAttachmentsWeb.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAttachmentsWeb.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewAttachmentsWeb.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnAttachmentsWebSourcePath});
			this.gridViewAttachmentsWeb.GridControl = this.gridControlAttachmentsWeb;
			this.gridViewAttachmentsWeb.Name = "gridViewAttachmentsWeb";
			this.gridViewAttachmentsWeb.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewAttachmentsWeb.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewAttachmentsWeb.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewAttachmentsWeb.OptionsCustomization.AllowFilter = false;
			this.gridViewAttachmentsWeb.OptionsCustomization.AllowGroup = false;
			this.gridViewAttachmentsWeb.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewAttachmentsWeb.OptionsCustomization.AllowSort = false;
			this.gridViewAttachmentsWeb.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewAttachmentsWeb.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewAttachmentsWeb.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewAttachmentsWeb.OptionsView.ShowColumnHeaders = false;
			this.gridViewAttachmentsWeb.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewAttachmentsWeb.OptionsView.ShowGroupPanel = false;
			this.gridViewAttachmentsWeb.OptionsView.ShowIndicator = false;
			this.gridViewAttachmentsWeb.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewAttachmentsWeb.RowHeight = 35;
			// 
			// gridColumnAttachmentsWebSourcePath
			// 
			this.gridColumnAttachmentsWebSourcePath.Caption = "Name";
			this.gridColumnAttachmentsWebSourcePath.ColumnEdit = this.repositoryItemButtonEditAttachmentsWeb;
			this.gridColumnAttachmentsWebSourcePath.FieldName = "OriginalPath";
			this.gridColumnAttachmentsWebSourcePath.Name = "gridColumnAttachmentsWebSourcePath";
			this.gridColumnAttachmentsWebSourcePath.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnAttachmentsWebSourcePath.Visible = true;
			this.gridColumnAttachmentsWebSourcePath.VisibleIndex = 0;
			// 
			// repositoryItemButtonEditAttachmentsWeb
			// 
			this.repositoryItemButtonEditAttachmentsWeb.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditAttachmentsWeb.Appearance.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsWeb.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditAttachmentsWeb.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsWeb.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditAttachmentsWeb.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsWeb.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditAttachmentsWeb.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemButtonEditAttachmentsWeb.AutoHeight = false;
			this.repositoryItemButtonEditAttachmentsWeb.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::FileManager.Properties.Resources.UrlButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::FileManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
			this.repositoryItemButtonEditAttachmentsWeb.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
			this.repositoryItemButtonEditAttachmentsWeb.Mask.EditMask = "http://([a-zA-Z0-9.]|%[0-9A-Za-z]|/|:[0-9]?)*";
			this.repositoryItemButtonEditAttachmentsWeb.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
			this.repositoryItemButtonEditAttachmentsWeb.Name = "repositoryItemButtonEditAttachmentsWeb";
			this.repositoryItemButtonEditAttachmentsWeb.NullText = "Type website link...";
			this.repositoryItemButtonEditAttachmentsWeb.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditAttachmentsWeb_ButtonClick);
			// 
			// buttonXAttachmentsWebAdd
			// 
			this.buttonXAttachmentsWebAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAttachmentsWebAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAttachmentsWebAdd.Image = global::FileManager.Properties.Resources.PlusButton;
			this.buttonXAttachmentsWebAdd.ImageFixedSize = new System.Drawing.Size(24, 24);
			this.buttonXAttachmentsWebAdd.Location = new System.Drawing.Point(3, 9);
			this.buttonXAttachmentsWebAdd.Name = "buttonXAttachmentsWebAdd";
			this.buttonXAttachmentsWebAdd.Size = new System.Drawing.Size(151, 34);
			this.buttonXAttachmentsWebAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAttachmentsWebAdd.TabIndex = 11;
			this.buttonXAttachmentsWebAdd.Text = "Attach Website";
			this.buttonXAttachmentsWebAdd.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXAttachmentsWebAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXAttachmentsWebAdd.Click += new System.EventHandler(this.buttonXAttachmentsWebAdd_Click);
			// 
			// checkBoxEnableAttachmnets
			// 
			this.checkBoxEnableAttachmnets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxEnableAttachmnets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxEnableAttachmnets.Location = new System.Drawing.Point(3, 3);
			this.checkBoxEnableAttachmnets.Name = "checkBoxEnableAttachmnets";
			this.checkBoxEnableAttachmnets.Size = new System.Drawing.Size(344, 40);
			this.checkBoxEnableAttachmnets.TabIndex = 5;
			this.checkBoxEnableAttachmnets.Text = "Link other important files or Websites to this specific file...";
			this.checkBoxEnableAttachmnets.UseVisualStyleBackColor = true;
			this.checkBoxEnableAttachmnets.CheckedChanged += new System.EventHandler(this.checkBoxEnableAttachments_CheckedChanged);
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
			// AttachmentsEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.laHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "AttachmentsEditor";
			this.Size = new System.Drawing.Size(350, 573);
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageFiles.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlAttachmentsFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewAttachmentsFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditAttachmentsFiles)).EndInit();
			this.xtraTabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlAttachmentsWeb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewAttachmentsWeb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditAttachmentsWeb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label laHeader;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageFiles;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private System.Windows.Forms.CheckBox checkBoxEnableAttachmnets;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraGrid.GridControl gridControlAttachmentsFiles;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewAttachmentsFiles;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnAttachmentsFilesName;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditAttachmentsFiles;
		private DevComponents.DotNetBar.ButtonX buttonXAttachmentsFilesAdd;
		private DevExpress.XtraGrid.GridControl gridControlAttachmentsWeb;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewAttachmentsWeb;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnAttachmentsWebSourcePath;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditAttachmentsWeb;
		private DevComponents.DotNetBar.ButtonX buttonXAttachmentsWebAdd;
	}
}
