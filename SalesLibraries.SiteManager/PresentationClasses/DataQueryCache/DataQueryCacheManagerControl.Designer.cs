namespace SalesLibraries.SiteManager.PresentationClasses.DataQueryCache
{
	sealed partial class DataQueryCacheManagerControl
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
			this.gridControlRecords = new DevExpress.XtraGrid.GridControl();
			this.gridViewRecords = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnTitle = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridControlRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewRecords)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlRecords
			// 
			this.gridControlRecords.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlRecords.Location = new System.Drawing.Point(0, 0);
			this.gridControlRecords.MainView = this.gridViewRecords;
			this.gridControlRecords.Name = "gridControlRecords";
			this.gridControlRecords.Size = new System.Drawing.Size(911, 483);
			this.gridControlRecords.TabIndex = 3;
			this.gridControlRecords.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRecords});
			// 
			// gridViewRecords
			// 
			this.gridViewRecords.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewRecords.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewRecords.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewRecords.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.OddRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.Preview.Options.UseFont = true;
			this.gridViewRecords.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.Row.Options.UseFont = true;
			this.gridViewRecords.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewRecords.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnTitle,
            this.gridColumnId,
            this.gridColumnDescription});
			this.gridViewRecords.GridControl = this.gridControlRecords;
			this.gridViewRecords.Name = "gridViewRecords";
			this.gridViewRecords.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewRecords.OptionsCustomization.AllowFilter = false;
			this.gridViewRecords.OptionsCustomization.AllowGroup = false;
			this.gridViewRecords.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewRecords.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewRecords.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewRecords.OptionsSelection.MultiSelect = true;
			this.gridViewRecords.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gridViewRecords.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
			this.gridViewRecords.OptionsSelection.UseIndicatorForSelection = false;
			this.gridViewRecords.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewRecords.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewRecords.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewRecords.OptionsView.ShowDetailButtons = false;
			this.gridViewRecords.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewRecords.OptionsView.ShowGroupPanel = false;
			this.gridViewRecords.OptionsView.ShowIndicator = false;
			this.gridViewRecords.PreviewFieldName = "DetailString";
			this.gridViewRecords.PreviewIndent = 5;
			this.gridViewRecords.RowHeight = 35;
			this.gridViewRecords.RowSeparatorHeight = 5;
			// 
			// gridColumnTitle
			// 
			this.gridColumnTitle.Caption = "Title";
			this.gridColumnTitle.FieldName = "title";
			this.gridColumnTitle.Name = "gridColumnTitle";
			this.gridColumnTitle.OptionsColumn.AllowEdit = false;
			this.gridColumnTitle.OptionsColumn.ReadOnly = true;
			this.gridColumnTitle.Visible = true;
			this.gridColumnTitle.VisibleIndex = 2;
			this.gridColumnTitle.Width = 166;
			// 
			// gridColumnId
			// 
			this.gridColumnId.Caption = "ID";
			this.gridColumnId.FieldName = "id";
			this.gridColumnId.Name = "gridColumnId";
			this.gridColumnId.OptionsColumn.AllowEdit = false;
			this.gridColumnId.OptionsColumn.ReadOnly = true;
			this.gridColumnId.Visible = true;
			this.gridColumnId.VisibleIndex = 1;
			this.gridColumnId.Width = 100;
			// 
			// gridColumnDescription
			// 
			this.gridColumnDescription.Caption = "Description";
			this.gridColumnDescription.FieldName = "description";
			this.gridColumnDescription.Name = "gridColumnDescription";
			this.gridColumnDescription.OptionsColumn.AllowEdit = false;
			this.gridColumnDescription.OptionsColumn.ReadOnly = true;
			this.gridColumnDescription.Visible = true;
			this.gridColumnDescription.VisibleIndex = 3;
			this.gridColumnDescription.Width = 413;
			// 
			// DataQueryCacheManagerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.gridControlRecords);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DataQueryCacheManagerControl";
			this.Size = new System.Drawing.Size(911, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlRecords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewRecords)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
		private DevExpress.XtraGrid.GridControl gridControlRecords;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewRecords;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTitle;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
	}
}
