namespace OutlookSalesDepotAddIn.Controls.Viewers
{
    partial class FolderViewer
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
			this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnWidget = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.gridColumnDisplayName = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			this.SuspendLayout();
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
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ShowShadow = false;
			this.toolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController_GetActiveObjectInfo);
			// 
			// gridControlFiles
			// 
			this.gridControlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlFiles.Location = new System.Drawing.Point(0, 0);
			this.gridControlFiles.MainView = this.gridViewFiles;
			this.gridControlFiles.Name = "gridControlFiles";
			this.gridControlFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			this.gridControlFiles.Size = new System.Drawing.Size(407, 332);
			this.gridControlFiles.TabIndex = 1;
			this.gridControlFiles.ToolTipController = this.toolTipController;
			this.gridControlFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFiles});
			// 
			// gridViewFiles
			// 
			this.gridViewFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewFiles.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.GroupRow.Options.UseFont = true;
			this.gridViewFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewFiles.Appearance.Row.Options.UseFont = true;
			this.gridViewFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnWidget,
            this.gridColumnDisplayName});
			this.gridViewFiles.GridControl = this.gridControlFiles;
			this.gridViewFiles.GroupRowHeight = 25;
			this.gridViewFiles.Name = "gridViewFiles";
			this.gridViewFiles.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFiles.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFiles.OptionsBehavior.Editable = false;
			this.gridViewFiles.OptionsBehavior.ReadOnly = true;
			this.gridViewFiles.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewFiles.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewFiles.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewFiles.OptionsFilter.AllowFilterEditor = false;
			this.gridViewFiles.OptionsFilter.AllowMRUFilterList = false;
			this.gridViewFiles.OptionsFind.AllowFindPanel = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewFiles.OptionsView.RowAutoHeight = true;
			this.gridViewFiles.OptionsView.ShowColumnHeaders = false;
			this.gridViewFiles.OptionsView.ShowDetailButtons = false;
			this.gridViewFiles.OptionsView.ShowGroupPanel = false;
			this.gridViewFiles.OptionsView.ShowIndicator = false;
			this.gridViewFiles.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFiles.RowHeight = 25;
			this.gridViewFiles.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewFiles_RowCellClick);
			this.gridViewFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridViewFiles_MouseMove);
			this.gridViewFiles.MouseLeave += new System.EventHandler(this.gridViewFiles_MouseLeave);
			// 
			// gridColumnWidget
			// 
			this.gridColumnWidget.ColumnEdit = this.repositoryItemPictureEdit;
			this.gridColumnWidget.FieldName = "Widget";
			this.gridColumnWidget.Name = "gridColumnWidget";
			this.gridColumnWidget.OptionsColumn.FixedWidth = true;
			this.gridColumnWidget.Visible = true;
			this.gridColumnWidget.VisibleIndex = 0;
			this.gridColumnWidget.Width = 48;
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.NullText = " ";
			this.repositoryItemPictureEdit.ShowMenu = false;
			// 
			// gridColumnDisplayName
			// 
			this.gridColumnDisplayName.Caption = "Link";
			this.gridColumnDisplayName.FieldName = "DisplayName";
			this.gridColumnDisplayName.Name = "gridColumnDisplayName";
			this.gridColumnDisplayName.Visible = true;
			this.gridColumnDisplayName.VisibleIndex = 1;
			this.gridColumnDisplayName.Width = 633;
			// 
			// FolderViewer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.gridControlFiles);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FolderViewer";
			this.Size = new System.Drawing.Size(407, 332);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.Utils.ToolTipController toolTipController;
		public DevExpress.XtraGrid.GridControl gridControlFiles;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnWidget;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnDisplayName;
    }
}
