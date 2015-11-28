namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries
{
    partial class FormDeleteInactiveLinks
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExpiredDate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDelete = new DevComponents.DotNetBar.ButtonX();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandMain = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnFolderName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnIsDead = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnIsExpired = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnPath = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnFolderName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnPath = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnIsDead = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnIsExpired = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			this.SuspendLayout();
			// 
			// pnButtons
			// 
			this.pnButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnButtons.Controls.Add(this.buttonXCancel);
			this.pnButtons.Controls.Add(this.buttonXOK);
			this.pnButtons.Controls.Add(this.buttonXExpiredDate);
			this.pnButtons.Controls.Add(this.buttonXDelete);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnButtons.ForeColor = System.Drawing.Color.Black;
			this.pnButtons.Location = new System.Drawing.Point(606, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(158, 512);
			this.pnButtons.TabIndex = 0;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(10, 468);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(136, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 11;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(10, 430);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(136, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 10;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXExpiredDate
			// 
			this.buttonXExpiredDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExpiredDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXExpiredDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExpiredDate.Location = new System.Drawing.Point(10, 62);
			this.buttonXExpiredDate.Name = "buttonXExpiredDate";
			this.buttonXExpiredDate.Size = new System.Drawing.Size(136, 44);
			this.buttonXExpiredDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExpiredDate.TabIndex = 6;
			this.buttonXExpiredDate.Text = "Expired Date\r\nOptions";
			this.buttonXExpiredDate.Click += new System.EventHandler(this.btExpiredDate_Click);
			// 
			// buttonXDelete
			// 
			this.buttonXDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDelete.Location = new System.Drawing.Point(10, 12);
			this.buttonXDelete.Name = "buttonXDelete";
			this.buttonXDelete.Size = new System.Drawing.Size(136, 44);
			this.buttonXDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDelete.TabIndex = 5;
			this.buttonXDelete.Text = "Delete\r\nthis Link";
			this.buttonXDelete.Click += new System.EventHandler(this.btDelete_Click);
			// 
			// gridControl
			// 
			this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControl.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControl.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControl.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControl.Location = new System.Drawing.Point(0, 0);
			this.gridControl.MainView = this.advBandedGridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit});
			this.gridControl.Size = new System.Drawing.Size(606, 512);
			this.gridControl.TabIndex = 1;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView,
            this.gridView});
			// 
			// advBandedGridView
			// 
			this.advBandedGridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.Row.Options.UseFont = true;
			this.advBandedGridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandMain});
			this.advBandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnName,
            this.bandedGridColumnFolderName,
            this.bandedGridColumnPath,
            this.bandedGridColumnIsDead,
            this.bandedGridColumnIsExpired});
			this.advBandedGridView.GridControl = this.gridControl;
			this.advBandedGridView.Name = "advBandedGridView";
			this.advBandedGridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
			this.advBandedGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
			this.advBandedGridView.OptionsBehavior.Editable = false;
			this.advBandedGridView.OptionsBehavior.ReadOnly = true;
			this.advBandedGridView.OptionsCustomization.AllowColumnResizing = false;
			this.advBandedGridView.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridView.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView.OptionsView.ShowBands = false;
			this.advBandedGridView.OptionsView.ShowDetailButtons = false;
			this.advBandedGridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridView.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView.OptionsView.ShowIndicator = false;
			this.advBandedGridView.RowSeparatorHeight = 10;
			this.advBandedGridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.advBandedGridView_RowCellStyle);
			// 
			// gridBandMain
			// 
			this.gridBandMain.Caption = "Main";
			this.gridBandMain.Columns.Add(this.bandedGridColumnName);
			this.gridBandMain.Columns.Add(this.bandedGridColumnFolderName);
			this.gridBandMain.Columns.Add(this.bandedGridColumnIsDead);
			this.gridBandMain.Columns.Add(this.bandedGridColumnIsExpired);
			this.gridBandMain.Columns.Add(this.bandedGridColumnPath);
			this.gridBandMain.Name = "gridBandMain";
			this.gridBandMain.RowCount = 2;
			this.gridBandMain.VisibleIndex = 0;
			this.gridBandMain.Width = 300;
			// 
			// bandedGridColumnName
			// 
			this.bandedGridColumnName.Caption = "Name";
			this.bandedGridColumnName.FieldName = "Name";
			this.bandedGridColumnName.Name = "bandedGridColumnName";
			this.bandedGridColumnName.Visible = true;
			// 
			// bandedGridColumnFolderName
			// 
			this.bandedGridColumnFolderName.Caption = "Window";
			this.bandedGridColumnFolderName.FieldName = "FolderName";
			this.bandedGridColumnFolderName.Name = "bandedGridColumnFolderName";
			this.bandedGridColumnFolderName.Visible = true;
			// 
			// bandedGridColumnIsDead
			// 
			this.bandedGridColumnIsDead.Caption = "Is Dead";
			this.bandedGridColumnIsDead.FieldName = "IsDead";
			this.bandedGridColumnIsDead.Name = "bandedGridColumnIsDead";
			this.bandedGridColumnIsDead.Visible = true;
			// 
			// bandedGridColumnIsExpired
			// 
			this.bandedGridColumnIsExpired.Caption = "Is Expired";
			this.bandedGridColumnIsExpired.FieldName = "IsExpired";
			this.bandedGridColumnIsExpired.Name = "bandedGridColumnIsExpired";
			this.bandedGridColumnIsExpired.Visible = true;
			// 
			// bandedGridColumnPath
			// 
			this.bandedGridColumnPath.Caption = "Path";
			this.bandedGridColumnPath.FieldName = "Path";
			this.bandedGridColumnPath.Name = "bandedGridColumnPath";
			this.bandedGridColumnPath.RowIndex = 1;
			this.bandedGridColumnPath.Visible = true;
			// 
			// repositoryItemCheckEdit
			// 
			this.repositoryItemCheckEdit.AutoHeight = false;
			this.repositoryItemCheckEdit.Caption = "Check";
			this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
			// 
			// gridView
			// 
			this.gridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridView.Appearance.EvenRow.Options.UseFont = true;
			this.gridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedCell.Options.UseFont = true;
			this.gridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.Row.Options.UseFont = true;
			this.gridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.SelectedRow.Options.UseFont = true;
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnFolderName,
            this.gridColumnPath,
            this.gridColumnIsDead,
            this.gridColumnIsExpired});
			this.gridView.GridControl = this.gridControl;
			this.gridView.Name = "gridView";
			this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
			this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
			this.gridView.OptionsBehavior.Editable = false;
			this.gridView.OptionsBehavior.ReadOnly = true;
			this.gridView.OptionsCustomization.AllowColumnMoving = false;
			this.gridView.OptionsCustomization.AllowColumnResizing = false;
			this.gridView.OptionsCustomization.AllowFilter = false;
			this.gridView.OptionsCustomization.AllowGroup = false;
			this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridView.OptionsView.EnableAppearanceEvenRow = true;
			this.gridView.OptionsView.ShowDetailButtons = false;
			this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridView.OptionsView.ShowGroupPanel = false;
			this.gridView.OptionsView.ShowIndicator = false;
			// 
			// gridColumnName
			// 
			this.gridColumnName.Caption = "Name";
			this.gridColumnName.FieldName = "Name";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.Visible = true;
			this.gridColumnName.VisibleIndex = 0;
			// 
			// gridColumnFolderName
			// 
			this.gridColumnFolderName.Caption = "Window";
			this.gridColumnFolderName.FieldName = "FolderName";
			this.gridColumnFolderName.Name = "gridColumnFolderName";
			this.gridColumnFolderName.Visible = true;
			this.gridColumnFolderName.VisibleIndex = 1;
			// 
			// gridColumnPath
			// 
			this.gridColumnPath.Caption = "Path";
			this.gridColumnPath.FieldName = "Path";
			this.gridColumnPath.Name = "gridColumnPath";
			this.gridColumnPath.Visible = true;
			this.gridColumnPath.VisibleIndex = 2;
			// 
			// gridColumnIsDead
			// 
			this.gridColumnIsDead.Caption = "Is Dead";
			this.gridColumnIsDead.FieldName = "IsDead";
			this.gridColumnIsDead.Name = "gridColumnIsDead";
			this.gridColumnIsDead.Visible = true;
			this.gridColumnIsDead.VisibleIndex = 3;
			// 
			// gridColumnIsExpired
			// 
			this.gridColumnIsExpired.Caption = "Is Expired";
			this.gridColumnIsExpired.FieldName = "IsExpired";
			this.gridColumnIsExpired.Name = "gridColumnIsExpired";
			this.gridColumnIsExpired.Visible = true;
			this.gridColumnIsExpired.VisibleIndex = 4;
			// 
			// FormDeleteInactiveLinks
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(764, 512);
			this.Controls.Add(this.gridControl);
			this.Controls.Add(this.pnButtons);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FormDeleteInactiveLinks";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Inactive Links";
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXExpiredDate;
		private DevComponents.DotNetBar.ButtonX buttonXDelete;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnFolderName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPath;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnIsDead;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnIsExpired;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMain;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnFolderName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnIsDead;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnIsExpired;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnPath;
    }
}