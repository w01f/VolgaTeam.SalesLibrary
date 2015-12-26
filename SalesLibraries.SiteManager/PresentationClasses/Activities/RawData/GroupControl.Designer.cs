namespace SalesLibraries.SiteManager.PresentationClasses.Activities.RawData
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
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemDateEditDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.gridColumnLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnSubType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnFile = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.CalendarTimeProperties)).BeginInit();
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
			this.gridViewData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewData.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.gridViewData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.gridViewData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDate,
            this.gridColumnLogin,
            this.gridColumnType,
            this.gridColumnSubType,
            this.gridColumnFile});
			this.gridViewData.GridControl = this.gridControlData;
			this.gridViewData.Name = "gridViewData";
			this.gridViewData.OptionsBehavior.Editable = false;
			this.gridViewData.OptionsBehavior.ReadOnly = true;
			this.gridViewData.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewData.OptionsCustomization.AllowFilter = false;
			this.gridViewData.OptionsCustomization.AllowGroup = false;
			this.gridViewData.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewData.OptionsMenu.EnableColumnMenu = false;
			this.gridViewData.OptionsMenu.EnableFooterMenu = false;
			this.gridViewData.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewData.OptionsMenu.ShowAutoFilterRowItem = false;
			this.gridViewData.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewData.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewData.OptionsPrint.PrintPreview = true;
			this.gridViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewData.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewData.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewData.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewData.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewData.OptionsView.ShowDetailButtons = false;
			this.gridViewData.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewData.OptionsView.ShowGroupPanel = false;
			this.gridViewData.OptionsView.ShowIndicator = false;
			this.gridViewData.OptionsView.ShowPreview = true;
			this.gridViewData.PreviewFieldName = "Details";
			this.gridViewData.PreviewIndent = 5;
			this.gridViewData.RowHeight = 35;
			this.gridViewData.RowSeparatorHeight = 5;
			// 
			// gridColumnDate
			// 
			this.gridColumnDate.Caption = "Date\\Time";
			this.gridColumnDate.ColumnEdit = this.repositoryItemDateEditDate;
			this.gridColumnDate.FieldName = "ActivityDate";
			this.gridColumnDate.Name = "gridColumnDate";
			this.gridColumnDate.Visible = true;
			this.gridColumnDate.VisibleIndex = 0;
			this.gridColumnDate.Width = 243;
			// 
			// repositoryItemDateEditDate
			// 
			this.repositoryItemDateEditDate.AutoHeight = false;
			this.repositoryItemDateEditDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemDateEditDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemDateEditDate.DisplayFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.EditFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.Name = "repositoryItemDateEditDate";
			// 
			// gridColumnLogin
			// 
			this.gridColumnLogin.Caption = "Login";
			this.gridColumnLogin.FieldName = "login";
			this.gridColumnLogin.Name = "gridColumnLogin";
			this.gridColumnLogin.Visible = true;
			this.gridColumnLogin.VisibleIndex = 1;
			this.gridColumnLogin.Width = 231;
			// 
			// gridColumnType
			// 
			this.gridColumnType.Caption = "Action Group";
			this.gridColumnType.FieldName = "type";
			this.gridColumnType.Name = "gridColumnType";
			this.gridColumnType.Visible = true;
			this.gridColumnType.VisibleIndex = 2;
			this.gridColumnType.Width = 258;
			// 
			// gridColumnSubType
			// 
			this.gridColumnSubType.Caption = "Action";
			this.gridColumnSubType.FieldName = "subType";
			this.gridColumnSubType.Name = "gridColumnSubType";
			this.gridColumnSubType.Visible = true;
			this.gridColumnSubType.VisibleIndex = 3;
			this.gridColumnSubType.Width = 258;
			// 
			// gridColumnFile
			// 
			this.gridColumnFile.Caption = "File";
			this.gridColumnFile.FieldName = "File";
			this.gridColumnFile.Name = "gridColumnFile";
			this.gridColumnFile.Visible = true;
			this.gridColumnFile.VisibleIndex = 4;
			this.gridColumnFile.Width = 758;
			// 
			// GroupControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDate;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLogin;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDate;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnType;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSubType;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnFile;
    }
}
