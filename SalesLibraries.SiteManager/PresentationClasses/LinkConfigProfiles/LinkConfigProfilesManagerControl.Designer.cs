namespace SalesLibraries.SiteManager.PresentationClasses.LinkConfigProfiles
{
	partial class LinkConfigProfilesManagerControl
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
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.gridControlProfiles = new DevExpress.XtraGrid.GridControl();
			this.gridViewProfiles = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnProfileName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnProfileActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.labelControlProfilesTitle = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnProfileContentContainer = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlProfiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewProfiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditActions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControl.Panel1.Controls.Add(this.gridControlProfiles);
			this.splitContainerControl.Panel1.Controls.Add(this.labelControlProfilesTitle);
			this.splitContainerControl.Panel1.MinSize = 250;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.pnProfileContentContainer);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(911, 658);
			this.splitContainerControl.SplitterPosition = 193;
			this.splitContainerControl.TabIndex = 0;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// gridControlProfiles
			// 
			this.gridControlProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlProfiles.Location = new System.Drawing.Point(0, 38);
			this.gridControlProfiles.MainView = this.gridViewProfiles;
			this.gridControlProfiles.Name = "gridControlProfiles";
			this.gridControlProfiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditActions});
			this.gridControlProfiles.Size = new System.Drawing.Size(246, 616);
			this.gridControlProfiles.TabIndex = 4;
			this.gridControlProfiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProfiles});
			// 
			// gridViewProfiles
			// 
			this.gridViewProfiles.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProfiles.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewProfiles.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProfiles.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewProfiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProfiles.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewProfiles.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewProfiles.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewProfiles.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProfiles.Appearance.OddRow.Options.UseFont = true;
			this.gridViewProfiles.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProfiles.Appearance.Preview.Options.UseFont = true;
			this.gridViewProfiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProfiles.Appearance.Row.Options.UseFont = true;
			this.gridViewProfiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProfiles.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewProfiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnProfileName,
            this.gridColumnProfileActions});
			this.gridViewProfiles.GridControl = this.gridControlProfiles;
			this.gridViewProfiles.Name = "gridViewProfiles";
			this.gridViewProfiles.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewProfiles.OptionsCustomization.AllowFilter = false;
			this.gridViewProfiles.OptionsCustomization.AllowGroup = false;
			this.gridViewProfiles.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewProfiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewProfiles.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewProfiles.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewProfiles.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewProfiles.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewProfiles.OptionsView.ShowColumnHeaders = false;
			this.gridViewProfiles.OptionsView.ShowDetailButtons = false;
			this.gridViewProfiles.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewProfiles.OptionsView.ShowGroupPanel = false;
			this.gridViewProfiles.OptionsView.ShowIndicator = false;
			this.gridViewProfiles.PreviewFieldName = "DetailString";
			this.gridViewProfiles.PreviewIndent = 5;
			this.gridViewProfiles.RowHeight = 35;
			this.gridViewProfiles.RowSeparatorHeight = 5;
			this.gridViewProfiles.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnSelectedProfileChanged);
			// 
			// gridColumnProfileName
			// 
			this.gridColumnProfileName.Caption = "Name";
			this.gridColumnProfileName.FieldName = "ProfileName";
			this.gridColumnProfileName.Name = "gridColumnProfileName";
			this.gridColumnProfileName.OptionsColumn.AllowEdit = false;
			this.gridColumnProfileName.OptionsColumn.ReadOnly = true;
			this.gridColumnProfileName.Visible = true;
			this.gridColumnProfileName.VisibleIndex = 0;
			this.gridColumnProfileName.Width = 176;
			// 
			// gridColumnProfileActions
			// 
			this.gridColumnProfileActions.Caption = "Actions";
			this.gridColumnProfileActions.ColumnEdit = this.repositoryItemButtonEditActions;
			this.gridColumnProfileActions.Name = "gridColumnProfileActions";
			this.gridColumnProfileActions.OptionsColumn.FixedWidth = true;
			this.gridColumnProfileActions.Visible = true;
			this.gridColumnProfileActions.VisibleIndex = 1;
			// 
			// repositoryItemButtonEditActions
			// 
			this.repositoryItemButtonEditActions.AutoHeight = false;
			this.repositoryItemButtonEditActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.EditButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SiteManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemButtonEditActions.Name = "repositoryItemButtonEditActions";
			this.repositoryItemButtonEditActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnProfilesActionsButtonClick);
			// 
			// labelControlProfilesTitle
			// 
			this.labelControlProfilesTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlProfilesTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlProfilesTitle.Location = new System.Drawing.Point(0, 0);
			this.labelControlProfilesTitle.Name = "labelControlProfilesTitle";
			this.labelControlProfilesTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.labelControlProfilesTitle.Size = new System.Drawing.Size(246, 38);
			this.labelControlProfilesTitle.StyleController = this.styleController;
			this.labelControlProfilesTitle.TabIndex = 5;
			this.labelControlProfilesTitle.Text = "Profiles List:";
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
			// pnProfileContentContainer
			// 
			this.pnProfileContentContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnProfileContentContainer.Location = new System.Drawing.Point(0, 0);
			this.pnProfileContentContainer.Name = "pnProfileContentContainer";
			this.pnProfileContentContainer.Size = new System.Drawing.Size(656, 658);
			this.pnProfileContentContainer.TabIndex = 0;
			// 
			// LinkConfigProfilesManagerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "LinkConfigProfilesManagerControl";
			this.Size = new System.Drawing.Size(911, 658);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlProfiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewProfiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditActions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private DevExpress.XtraGrid.GridControl gridControlProfiles;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewProfiles;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnProfileName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnProfileActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditActions;
		private DevExpress.XtraEditors.LabelControl labelControlProfilesTitle;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnProfileContentContainer;
	}
}
