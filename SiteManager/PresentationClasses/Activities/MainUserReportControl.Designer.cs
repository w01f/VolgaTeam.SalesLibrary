namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	public partial class MainUserReportControl
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
			this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnGroups = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnFiles = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnVideos = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTotal = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemDateEditDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlData
			// 
			this.gridControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlData.Location = new System.Drawing.Point(0, 0);
			this.gridControlData.MainView = this.gridViewData;
			this.gridControlData.Name = "gridControlData";
			this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditDate});
			this.gridControlData.Size = new System.Drawing.Size(898, 483);
			this.gridControlData.TabIndex = 2;
			this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewData});
			// 
			// gridViewData
			// 
			this.gridViewData.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewData.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewData.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewData.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewData.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.OddRow.Options.UseFont = true;
			this.gridViewData.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.Preview.Options.UseFont = true;
			this.gridViewData.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.Row.Options.UseFont = true;
			this.gridViewData.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnGroups,
            this.gridColumnLogin,
            this.gridColumnFiles,
            this.gridColumnVideos,
            this.gridColumnTotal});
			this.gridViewData.GridControl = this.gridControlData;
			this.gridViewData.Name = "gridViewData";
			this.gridViewData.OptionsBehavior.Editable = false;
			this.gridViewData.OptionsBehavior.ReadOnly = true;
			this.gridViewData.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewData.OptionsCustomization.AllowFilter = false;
			this.gridViewData.OptionsCustomization.AllowGroup = false;
			this.gridViewData.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewData.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewData.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewData.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewData.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewData.OptionsView.ShowDetailButtons = false;
			this.gridViewData.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewData.OptionsView.ShowGroupPanel = false;
			this.gridViewData.OptionsView.ShowIndicator = false;
			this.gridViewData.PreviewIndent = 5;
			this.gridViewData.RowHeight = 35;
			this.gridViewData.RowSeparatorHeight = 5;
			this.gridViewData.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gridViewData_CustomColumnSort);
			// 
			// gridColumnName
			// 
			this.gridColumnName.Caption = "Name";
			this.gridColumnName.FieldName = "FullName";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.Visible = true;
			this.gridColumnName.VisibleIndex = 0;
			this.gridColumnName.Width = 230;
			// 
			// gridColumnGroups
			// 
			this.gridColumnGroups.Caption = "Groups";
			this.gridColumnGroups.FieldName = "groups";
			this.gridColumnGroups.Name = "gridColumnGroups";
			this.gridColumnGroups.Visible = true;
			this.gridColumnGroups.VisibleIndex = 1;
			this.gridColumnGroups.Width = 282;
			// 
			// gridColumnLogin
			// 
			this.gridColumnLogin.Caption = "Login Activity";
			this.gridColumnLogin.FieldName = "logins";
			this.gridColumnLogin.Name = "gridColumnLogin";
			this.gridColumnLogin.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnLogin.Visible = true;
			this.gridColumnLogin.VisibleIndex = 2;
			this.gridColumnLogin.Width = 302;
			// 
			// gridColumnFiles
			// 
			this.gridColumnFiles.Caption = "Documents Activity";
			this.gridColumnFiles.FieldName = "files";
			this.gridColumnFiles.Name = "gridColumnFiles";
			this.gridColumnFiles.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnFiles.Visible = true;
			this.gridColumnFiles.VisibleIndex = 3;
			this.gridColumnFiles.Width = 349;
			// 
			// gridColumnVideos
			// 
			this.gridColumnVideos.Caption = "Video Activity";
			this.gridColumnVideos.FieldName = "videos";
			this.gridColumnVideos.Name = "gridColumnVideos";
			this.gridColumnVideos.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnVideos.Visible = true;
			this.gridColumnVideos.VisibleIndex = 4;
			this.gridColumnVideos.Width = 283;
			// 
			// gridColumnTotal
			// 
			this.gridColumnTotal.Caption = "All Activity";
			this.gridColumnTotal.FieldName = "totals";
			this.gridColumnTotal.Name = "gridColumnTotal";
			this.gridColumnTotal.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnTotal.Visible = true;
			this.gridColumnTotal.VisibleIndex = 5;
			this.gridColumnTotal.Width = 302;
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
			// MainUserReportControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainUserReportControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroups;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDate;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLogin;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnFiles;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnVideos;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTotal;
    }
}
