namespace SalesDepot.PresentationClasses.QBuilderControls
{
	partial class PageListControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageListControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.simpleButtonDelete = new DevExpress.XtraEditors.SimpleButton();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandName = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnPageName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandActions = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnActions = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemButtonEditActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditActions)).BeginInit();
			this.SuspendLayout();
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.simpleButtonDelete);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(304, 45);
			this.pnButtons.TabIndex = 0;
			// 
			// simpleButtonDelete
			// 
			this.simpleButtonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonDelete.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonDelete.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonDelete.Appearance.Options.UseFont = true;
			this.simpleButtonDelete.Appearance.Options.UseForeColor = true;
			this.simpleButtonDelete.Location = new System.Drawing.Point(13, 7);
			this.simpleButtonDelete.Name = "simpleButtonDelete";
			this.simpleButtonDelete.Size = new System.Drawing.Size(279, 31);
			this.simpleButtonDelete.TabIndex = 1;
			this.simpleButtonDelete.Text = "Clean up my quickSITES";
			this.simpleButtonDelete.Click += new System.EventHandler(this.simpleButtonDelete_Click);
			// 
			// gridControl
			// 
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 45);
			this.gridControl.MainView = this.advBandedGridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditActions});
			this.gridControl.Size = new System.Drawing.Size(304, 284);
			this.gridControl.TabIndex = 2;
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
            this.gridBandName,
            this.gridBandActions});
			this.advBandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnPageName,
            this.bandedGridColumnActions});
			this.advBandedGridView.GridControl = this.gridControl;
			this.advBandedGridView.Name = "advBandedGridView";
			this.advBandedGridView.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView.OptionsView.AutoCalcPreviewLineCount = true;
			this.advBandedGridView.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridView.OptionsView.ShowColumnHeaders = false;
			this.advBandedGridView.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView.OptionsView.ShowIndicator = false;
			this.advBandedGridView.OptionsView.ShowPreview = true;
			this.advBandedGridView.PreviewFieldName = "Details";
			this.advBandedGridView.RowHeight = 40;
			this.advBandedGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.advBandedGridView_FocusedRowChanged);
			// 
			// gridBandName
			// 
			this.gridBandName.Caption = "Name";
			this.gridBandName.Columns.Add(this.bandedGridColumnPageName);
			this.gridBandName.Name = "gridBandName";
			this.gridBandName.Width = 216;
			// 
			// bandedGridColumnPageName
			// 
			this.bandedGridColumnPageName.Caption = "Name";
			this.bandedGridColumnPageName.FieldName = "title";
			this.bandedGridColumnPageName.Name = "bandedGridColumnPageName";
			this.bandedGridColumnPageName.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnPageName.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnPageName.Visible = true;
			this.bandedGridColumnPageName.Width = 216;
			// 
			// gridBandActions
			// 
			this.gridBandActions.Caption = "gridBand2";
			this.gridBandActions.Columns.Add(this.bandedGridColumnActions);
			this.gridBandActions.Name = "gridBandActions";
			this.gridBandActions.OptionsBand.FixedWidth = true;
			this.gridBandActions.OptionsBand.ShowCaption = false;
			this.gridBandActions.Width = 84;
			// 
			// bandedGridColumnActions
			// 
			this.bandedGridColumnActions.Caption = "Actions";
			this.bandedGridColumnActions.ColumnEdit = this.repositoryItemButtonEditActions;
			this.bandedGridColumnActions.Name = "bandedGridColumnActions";
			this.bandedGridColumnActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.bandedGridColumnActions.Visible = true;
			this.bandedGridColumnActions.Width = 84;
			// 
			// repositoryItemButtonEditActions
			// 
			this.repositoryItemButtonEditActions.AutoHeight = false;
			this.repositoryItemButtonEditActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditActions.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Clone this quickSITE", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditActions.Buttons1"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Delete this quickSITE", null, null, true)});
			this.repositoryItemButtonEditActions.Name = "repositoryItemButtonEditActions";
			this.repositoryItemButtonEditActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditActions_ButtonClick);
			// 
			// PageListControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.gridControl);
			this.Controls.Add(this.pnButtons);
			this.Enabled = false;
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "PageListControl";
			this.Size = new System.Drawing.Size(304, 329);
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditActions)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnButtons;
		private DevExpress.XtraEditors.SimpleButton simpleButtonDelete;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnPageName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditActions;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandName;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandActions;
	}
}
