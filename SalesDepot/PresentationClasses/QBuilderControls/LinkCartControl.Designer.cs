namespace SalesDepot.PresentationClasses.QBuilderControls
{
	partial class LinkCartControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkCartControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.simpleButtonAddAllLinks = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonClear = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonRefresh = new DevExpress.XtraEditors.SimpleButton();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandType = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemPictureEditType = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.gridBandLink = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnLinkName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandActions = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnActions = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemButtonEditActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEditType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditActions)).BeginInit();
			this.SuspendLayout();
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.simpleButtonAddAllLinks);
			this.pnButtons.Controls.Add(this.simpleButtonClear);
			this.pnButtons.Controls.Add(this.simpleButtonRefresh);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(290, 121);
			this.pnButtons.TabIndex = 0;
			// 
			// simpleButtonAddAllLinks
			// 
			this.simpleButtonAddAllLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonAddAllLinks.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonAddAllLinks.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonAddAllLinks.Appearance.Options.UseFont = true;
			this.simpleButtonAddAllLinks.Appearance.Options.UseForeColor = true;
			this.simpleButtonAddAllLinks.Location = new System.Drawing.Point(14, 81);
			this.simpleButtonAddAllLinks.Name = "simpleButtonAddAllLinks";
			this.simpleButtonAddAllLinks.Size = new System.Drawing.Size(263, 31);
			this.simpleButtonAddAllLinks.TabIndex = 2;
			this.simpleButtonAddAllLinks.Text = "Add All Links to quickSITE";
			this.simpleButtonAddAllLinks.Click += new System.EventHandler(this.simpleButtonAddAllLinks_Click);
			// 
			// simpleButtonClear
			// 
			this.simpleButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonClear.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonClear.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonClear.Appearance.Options.UseFont = true;
			this.simpleButtonClear.Appearance.Options.UseForeColor = true;
			this.simpleButtonClear.Location = new System.Drawing.Point(14, 44);
			this.simpleButtonClear.Name = "simpleButtonClear";
			this.simpleButtonClear.Size = new System.Drawing.Size(263, 31);
			this.simpleButtonClear.TabIndex = 1;
			this.simpleButtonClear.Text = "Empty Link Cart";
			this.simpleButtonClear.Click += new System.EventHandler(this.simpleButtonClear_Click);
			// 
			// simpleButtonRefresh
			// 
			this.simpleButtonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonRefresh.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonRefresh.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonRefresh.Appearance.Options.UseFont = true;
			this.simpleButtonRefresh.Appearance.Options.UseForeColor = true;
			this.simpleButtonRefresh.Location = new System.Drawing.Point(14, 7);
			this.simpleButtonRefresh.Name = "simpleButtonRefresh";
			this.simpleButtonRefresh.Size = new System.Drawing.Size(263, 31);
			this.simpleButtonRefresh.TabIndex = 0;
			this.simpleButtonRefresh.Text = "Refresh Link Cart";
			this.simpleButtonRefresh.Click += new System.EventHandler(this.simpleButtonRefresh_Click);
			// 
			// gridControl
			// 
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 121);
			this.gridControl.MainView = this.advBandedGridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditActions,
            this.repositoryItemPictureEditType});
			this.gridControl.Size = new System.Drawing.Size(290, 312);
			this.gridControl.TabIndex = 1;
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
            this.gridBandType,
            this.gridBandLink,
            this.gridBandActions});
			this.advBandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnType,
            this.bandedGridColumnLinkName,
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
			this.advBandedGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseDown);
			this.advBandedGridView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseMove);
			// 
			// gridBandType
			// 
			this.gridBandType.Caption = "Type";
			this.gridBandType.Columns.Add(this.bandedGridColumnType);
			this.gridBandType.Name = "gridBandType";
			this.gridBandType.OptionsBand.FixedWidth = true;
			this.gridBandType.Width = 42;
			// 
			// bandedGridColumnType
			// 
			this.bandedGridColumnType.Caption = "Type";
			this.bandedGridColumnType.ColumnEdit = this.repositoryItemPictureEditType;
			this.bandedGridColumnType.FieldName = "Logo";
			this.bandedGridColumnType.Name = "bandedGridColumnType";
			this.bandedGridColumnType.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnType.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnType.Visible = true;
			this.bandedGridColumnType.Width = 42;
			// 
			// repositoryItemPictureEditType
			// 
			this.repositoryItemPictureEditType.Name = "repositoryItemPictureEditType";
			this.repositoryItemPictureEditType.ReadOnly = true;
			this.repositoryItemPictureEditType.ShowMenu = false;
			this.repositoryItemPictureEditType.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			// 
			// gridBandLink
			// 
			this.gridBandLink.Caption = "Link";
			this.gridBandLink.Columns.Add(this.bandedGridColumnLinkName);
			this.gridBandLink.Name = "gridBandLink";
			this.gridBandLink.Width = 197;
			// 
			// bandedGridColumnLinkName
			// 
			this.bandedGridColumnLinkName.AppearanceCell.BorderColor = System.Drawing.Color.White;
			this.bandedGridColumnLinkName.AppearanceCell.Options.UseBorderColor = true;
			this.bandedGridColumnLinkName.Caption = "Link Name";
			this.bandedGridColumnLinkName.FieldName = "name";
			this.bandedGridColumnLinkName.Name = "bandedGridColumnLinkName";
			this.bandedGridColumnLinkName.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnLinkName.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnLinkName.Visible = true;
			this.bandedGridColumnLinkName.Width = 197;
			// 
			// gridBandActions
			// 
			this.gridBandActions.Caption = "gridBand2";
			this.gridBandActions.Columns.Add(this.bandedGridColumnActions);
			this.gridBandActions.Name = "gridBandActions";
			this.gridBandActions.OptionsBand.FixedWidth = true;
			this.gridBandActions.OptionsBand.ShowCaption = false;
			this.gridBandActions.Width = 47;
			// 
			// bandedGridColumnActions
			// 
			this.bandedGridColumnActions.Caption = "Actions";
			this.bandedGridColumnActions.ColumnEdit = this.repositoryItemButtonEditActions;
			this.bandedGridColumnActions.Name = "bandedGridColumnActions";
			this.bandedGridColumnActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.bandedGridColumnActions.Visible = true;
			this.bandedGridColumnActions.Width = 47;
			// 
			// repositoryItemButtonEditActions
			// 
			this.repositoryItemButtonEditActions.AutoHeight = false;
			this.repositoryItemButtonEditActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditActions.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Delete this file", null, null, true)});
			this.repositoryItemButtonEditActions.Name = "repositoryItemButtonEditActions";
			this.repositoryItemButtonEditActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditActions_ButtonClick);
			// 
			// LinkCartControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.gridControl);
			this.Controls.Add(this.pnButtons);
			this.Enabled = false;
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LinkCartControl";
			this.Size = new System.Drawing.Size(290, 433);
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEditType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditActions)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnButtons;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnType;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnLinkName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEditType;
		private DevExpress.XtraEditors.SimpleButton simpleButtonRefresh;
		private DevExpress.XtraEditors.SimpleButton simpleButtonClear;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandType;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLink;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandActions;
		private DevExpress.XtraEditors.SimpleButton simpleButtonAddAllLinks;
	}
}
