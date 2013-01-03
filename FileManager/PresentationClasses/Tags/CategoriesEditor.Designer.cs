namespace FileManager.PresentationClasses.Tags
{
	partial class CategoriesEditor
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
			this.gridViewTags = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnPageSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditLibrary = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnPageName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.gridViewGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnLibrarySelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLibraryName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.laHeader = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTags)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditLibrary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).BeginInit();
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			this.pnButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridViewTags
			// 
			this.gridViewTags.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTags.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewTags.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTags.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewTags.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewTags.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewTags.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTags.Appearance.Row.Options.UseFont = true;
			this.gridViewTags.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTags.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewTags.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPageSelected,
            this.gridColumnPageName});
			this.gridViewTags.GridControl = this.gridControl;
			this.gridViewTags.Name = "gridViewTags";
			this.gridViewTags.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewTags.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewTags.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewTags.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewTags.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewTags.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewTags.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewTags.OptionsCustomization.AllowFilter = false;
			this.gridViewTags.OptionsCustomization.AllowGroup = false;
			this.gridViewTags.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewTags.OptionsCustomization.AllowSort = false;
			this.gridViewTags.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewTags.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewTags.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewTags.OptionsView.ShowColumnHeaders = false;
			this.gridViewTags.OptionsView.ShowGroupPanel = false;
			this.gridViewTags.OptionsView.ShowHorzLines = false;
			this.gridViewTags.OptionsView.ShowIndicator = false;
			this.gridViewTags.OptionsView.ShowVertLines = false;
			this.gridViewTags.RowHeight = 35;
			// 
			// gridColumnPageSelected
			// 
			this.gridColumnPageSelected.Caption = "Selected";
			this.gridColumnPageSelected.ColumnEdit = this.repositoryItemCheckEditLibrary;
			this.gridColumnPageSelected.FieldName = "Selected";
			this.gridColumnPageSelected.Name = "gridColumnPageSelected";
			this.gridColumnPageSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnPageSelected.Visible = true;
			this.gridColumnPageSelected.VisibleIndex = 0;
			this.gridColumnPageSelected.Width = 30;
			// 
			// repositoryItemCheckEditLibrary
			// 
			this.repositoryItemCheckEditLibrary.AutoHeight = false;
			this.repositoryItemCheckEditLibrary.Name = "repositoryItemCheckEditLibrary";
			this.repositoryItemCheckEditLibrary.CheckedChanged += new System.EventHandler(this.RepositoryItemCheckEditCheckedChanged);
			// 
			// gridColumnPageName
			// 
			this.gridColumnPageName.Caption = "Name";
			this.gridColumnPageName.FieldName = "Name";
			this.gridColumnPageName.Name = "gridColumnPageName";
			this.gridColumnPageName.OptionsColumn.AllowEdit = false;
			this.gridColumnPageName.OptionsColumn.ReadOnly = true;
			this.gridColumnPageName.Visible = true;
			this.gridColumnPageName.VisibleIndex = 1;
			// 
			// gridControl
			// 
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			gridLevelNode1.LevelTemplate = this.gridViewTags;
			gridLevelNode1.RelationName = "Tags";
			this.gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
			this.gridControl.Location = new System.Drawing.Point(0, 0);
			this.gridControl.MainView = this.gridViewGroups;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditLibrary});
			this.gridControl.Size = new System.Drawing.Size(346, 581);
			this.gridControl.TabIndex = 1;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGroups,
            this.gridViewTags});
			// 
			// gridViewGroups
			// 
			this.gridViewGroups.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewGroups.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewGroups.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewGroups.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewGroups.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewGroups.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.Row.Options.UseFont = true;
			this.gridViewGroups.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewGroups.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnLibrarySelected,
            this.gridColumnLibraryName});
			this.gridViewGroups.GridControl = this.gridControl;
			this.gridViewGroups.Name = "gridViewGroups";
			this.gridViewGroups.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewGroups.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewGroups.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewGroups.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewGroups.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewGroups.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewGroups.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewGroups.OptionsCustomization.AllowFilter = false;
			this.gridViewGroups.OptionsCustomization.AllowGroup = false;
			this.gridViewGroups.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewGroups.OptionsCustomization.AllowSort = false;
			this.gridViewGroups.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
			this.gridViewGroups.OptionsDetail.ShowDetailTabs = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewGroups.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewGroups.OptionsView.ShowColumnHeaders = false;
			this.gridViewGroups.OptionsView.ShowGroupPanel = false;
			this.gridViewGroups.OptionsView.ShowIndicator = false;
			this.gridViewGroups.RowHeight = 35;
			// 
			// gridColumnLibrarySelected
			// 
			this.gridColumnLibrarySelected.Caption = "Selected";
			this.gridColumnLibrarySelected.ColumnEdit = this.repositoryItemCheckEditLibrary;
			this.gridColumnLibrarySelected.FieldName = "Selected";
			this.gridColumnLibrarySelected.Name = "gridColumnLibrarySelected";
			this.gridColumnLibrarySelected.OptionsColumn.FixedWidth = true;
			this.gridColumnLibrarySelected.Visible = true;
			this.gridColumnLibrarySelected.VisibleIndex = 0;
			this.gridColumnLibrarySelected.Width = 30;
			// 
			// gridColumnLibraryName
			// 
			this.gridColumnLibraryName.Caption = "Name";
			this.gridColumnLibraryName.FieldName = "Name";
			this.gridColumnLibraryName.Name = "gridColumnLibraryName";
			this.gridColumnLibraryName.OptionsColumn.AllowEdit = false;
			this.gridColumnLibraryName.OptionsColumn.ReadOnly = true;
			this.gridColumnLibraryName.Visible = true;
			this.gridColumnLibraryName.VisibleIndex = 1;
			this.gridColumnLibraryName.Width = 355;
			// 
			// laHeader
			// 
			this.laHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.laHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.laHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laHeader.Location = new System.Drawing.Point(0, 0);
			this.laHeader.Name = "laHeader";
			this.laHeader.Size = new System.Drawing.Size(350, 24);
			this.laHeader.TabIndex = 0;
			this.laHeader.Text = "Manage Search Tags";
			this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Controls.Add(this.pnButtons);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 24);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(350, 632);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.gridControl);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 47);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(346, 581);
			this.pnData.TabIndex = 1;
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.buttonXReset);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(346, 47);
			this.pnButtons.TabIndex = 0;
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Location = new System.Drawing.Point(5, 8);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(336, 30);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 0;
			this.buttonXReset.Text = "RESET ALL TAGS for the Selected Links";
			this.buttonXReset.TextColor = System.Drawing.Color.Black;
			this.buttonXReset.Click += new System.EventHandler(this.buttonXReset_Click);
			// 
			// CategoriesEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.laHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "CategoriesEditor";
			this.Size = new System.Drawing.Size(350, 656);
			((System.ComponentModel.ISupportInitialize)(this.gridViewTags)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditLibrary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).EndInit();
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			this.pnButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label laHeader;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTags;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPageSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditLibrary;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPageName;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewGroups;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLibrarySelected;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLibraryName;
	}
}
