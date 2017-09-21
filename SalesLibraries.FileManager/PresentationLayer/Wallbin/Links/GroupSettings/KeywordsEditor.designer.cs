namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	sealed partial class KeywordsEditor
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnValue = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditPartialKeyword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repositoryItemButtonEditSharedKeyword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.buttonXAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemReset = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemAdd = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPartialKeyword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditSharedKeyword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl.Location = new System.Drawing.Point(12, 102);
			this.gridControl.MainView = this.gridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditSharedKeyword,
            this.repositoryItemButtonEditPartialKeyword});
			this.gridControl.Size = new System.Drawing.Size(326, 290);
			this.gridControl.TabIndex = 8;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
			// 
			// gridView
			// 
			this.gridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedCell.Options.UseFont = true;
			this.gridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridView.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.Row.Options.UseFont = true;
			this.gridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.SelectedRow.Options.UseFont = true;
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnValue});
			this.gridView.GridControl = this.gridControl;
			this.gridView.Name = "gridView";
			this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView.OptionsCustomization.AllowColumnMoving = false;
			this.gridView.OptionsCustomization.AllowColumnResizing = false;
			this.gridView.OptionsCustomization.AllowFilter = false;
			this.gridView.OptionsCustomization.AllowGroup = false;
			this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridView.OptionsCustomization.AllowSort = false;
			this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridView.OptionsView.ShowColumnHeaders = false;
			this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridView.OptionsView.ShowGroupPanel = false;
			this.gridView.OptionsView.ShowIndicator = false;
			this.gridView.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridView.RowHeight = 35;
			this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.OnGridViewRowCellStyle);
			this.gridView.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.OnGridViewCustomRowCellEdit);
			this.gridView.HiddenEditor += new System.EventHandler(this.OnHideEditor);
			// 
			// gridColumnValue
			// 
			this.gridColumnValue.Caption = "Name";
			this.gridColumnValue.ColumnEdit = this.repositoryItemButtonEditPartialKeyword;
			this.gridColumnValue.FieldName = "Name";
			this.gridColumnValue.Name = "gridColumnValue";
			this.gridColumnValue.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnValue.Visible = true;
			this.gridColumnValue.VisibleIndex = 0;
			// 
			// repositoryItemButtonEditPartialKeyword
			// 
			this.repositoryItemButtonEditPartialKeyword.AutoHeight = false;
			this.repositoryItemButtonEditPartialKeyword.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonApplyForAll, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject7, "Apply for ALL Links and Edit", "MakeShared", null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject8, "Delete", "Delete", null, true)});
			this.repositoryItemButtonEditPartialKeyword.Name = "repositoryItemButtonEditPartialKeyword";
			this.repositoryItemButtonEditPartialKeyword.NullText = "Type Keyword...";
			this.repositoryItemButtonEditPartialKeyword.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEditPartialKeyword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnKeywordEditorButtonClick);
			// 
			// repositoryItemButtonEditSharedKeyword
			// 
			this.repositoryItemButtonEditSharedKeyword.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditSharedKeyword.Appearance.Options.UseFont = true;
			this.repositoryItemButtonEditSharedKeyword.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditSharedKeyword.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemButtonEditSharedKeyword.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditSharedKeyword.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemButtonEditSharedKeyword.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditSharedKeyword.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemButtonEditSharedKeyword.AutoHeight = false;
			this.repositoryItemButtonEditSharedKeyword.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, "Delete", "Delete", null, true)});
			this.repositoryItemButtonEditSharedKeyword.Name = "repositoryItemButtonEditSharedKeyword";
			this.repositoryItemButtonEditSharedKeyword.NullText = "Type Keyword...";
			this.repositoryItemButtonEditSharedKeyword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnKeywordEditorButtonClick);
			// 
			// buttonXAdd
			// 
			this.buttonXAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAdd.Image = global::SalesLibraries.FileManager.Properties.Resources.ButtonPlus;
			this.buttonXAdd.ImageFixedSize = new System.Drawing.Size(24, 24);
			this.buttonXAdd.Location = new System.Drawing.Point(12, 57);
			this.buttonXAdd.Name = "buttonXAdd";
			this.buttonXAdd.Size = new System.Drawing.Size(155, 31);
			this.buttonXAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAdd.TabIndex = 7;
			this.buttonXAdd.Text = "   Add Keyword";
			this.buttonXAdd.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXAdd.Click += new System.EventHandler(this.OnAddClick);
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Location = new System.Drawing.Point(12, 12);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(326, 31);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 0;
			this.buttonXReset.Text = "RESET ALL KEYWORDS for the Selected Links";
			this.buttonXReset.TextColor = System.Drawing.Color.Black;
			this.buttonXReset.Click += new System.EventHandler(this.OnResetClick);
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
			// layoutControl
			// 
			this.layoutControl.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControl.Appearance.Control.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDisabled.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDown.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDownHeader.Options.UseFont = true;
			this.layoutControl.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlFocused.Options.UseFont = true;
			this.layoutControl.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlReadOnly.Options.UseFont = true;
			this.layoutControl.Controls.Add(this.gridControl);
			this.layoutControl.Controls.Add(this.buttonXReset);
			this.layoutControl.Controls.Add(this.buttonXAdd);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(350, 404);
			this.layoutControl.StyleController = this.styleController;
			this.layoutControl.TabIndex = 63;
			this.layoutControl.Text = "layoutControl1";
			// 
			// layoutControlGroupRoot
			// 
			this.layoutControlGroupRoot.AllowHtmlStringInCaption = true;
			this.layoutControlGroupRoot.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRoot.GroupBordersVisible = false;
			this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItemReset,
            this.layoutControlItemAdd,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.layoutControlItemGrid});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(350, 404);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 35);
			this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(330, 10);
			this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemReset
			// 
			this.layoutControlItemReset.Control = this.buttonXReset;
			this.layoutControlItemReset.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemReset.FillControlToClientArea = false;
			this.layoutControlItemReset.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemReset.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItemReset.MinSize = new System.Drawing.Size(104, 35);
			this.layoutControlItemReset.Name = "layoutControlItemReset";
			this.layoutControlItemReset.Size = new System.Drawing.Size(330, 35);
			this.layoutControlItemReset.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemReset.Text = "Reset";
			this.layoutControlItemReset.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemReset.TextVisible = false;
			this.layoutControlItemReset.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(159, 45);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(171, 35);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemAdd
			// 
			this.layoutControlItemAdd.Control = this.buttonXAdd;
			this.layoutControlItemAdd.Location = new System.Drawing.Point(0, 45);
			this.layoutControlItemAdd.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItemAdd.MinSize = new System.Drawing.Size(104, 35);
			this.layoutControlItemAdd.Name = "layoutControlItemAdd";
			this.layoutControlItemAdd.Size = new System.Drawing.Size(159, 35);
			this.layoutControlItemAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemAdd.Text = "Add";
			this.layoutControlItemAdd.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemAdd.TextVisible = false;
			this.layoutControlItemAdd.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 80);
			this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(330, 10);
			this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemGrid
			// 
			this.layoutControlItemGrid.Control = this.gridControl;
			this.layoutControlItemGrid.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemGrid.FillControlToClientArea = false;
			this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 90);
			this.layoutControlItemGrid.Name = "layoutControlItemGrid";
			this.layoutControlItemGrid.Size = new System.Drawing.Size(330, 294);
			this.layoutControlItemGrid.Text = "Grid";
			this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemGrid.TextVisible = false;
			this.layoutControlItemGrid.TrimClientAreaToControl = false;
			// 
			// KeywordsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "KeywordsEditor";
			this.Size = new System.Drawing.Size(350, 404);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPartialKeyword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditSharedKeyword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private DevComponents.DotNetBar.ButtonX buttonXAdd;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditSharedKeyword;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditPartialKeyword;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemReset;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAdd;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
	}
}
