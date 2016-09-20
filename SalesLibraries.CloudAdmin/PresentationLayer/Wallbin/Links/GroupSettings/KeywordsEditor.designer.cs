namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.GroupSettings
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnValue = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditPartialKeyword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repositoryItemButtonEditSharedKeyword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.buttonXAdd = new DevComponents.DotNetBar.ButtonX();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPartialKeyword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditSharedKeyword)).BeginInit();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Controls.Add(this.pnButtons);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(350, 404);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.gridControl);
			this.pnData.Controls.Add(this.buttonXAdd);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 47);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(350, 357);
			this.pnData.TabIndex = 1;
			// 
			// gridControl
			// 
			this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl.Location = new System.Drawing.Point(5, 46);
			this.gridControl.MainView = this.gridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
			this.repositoryItemButtonEditSharedKeyword,
			this.repositoryItemButtonEditPartialKeyword});
			this.gridControl.Size = new System.Drawing.Size(340, 308);
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
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.CloudAdmin.Properties.Resources.ButtonApplyForAll, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Apply for ALL Links and Edit", "MakeShared", null, true),
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.CloudAdmin.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Delete", "Delete", null, true)});
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
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.CloudAdmin.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "Delete", "Delete", null, true)});
			this.repositoryItemButtonEditSharedKeyword.Name = "repositoryItemButtonEditSharedKeyword";
			this.repositoryItemButtonEditSharedKeyword.NullText = "Type Keyword...";
			this.repositoryItemButtonEditSharedKeyword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnKeywordEditorButtonClick);
			// 
			// buttonXAdd
			// 
			this.buttonXAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAdd.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ButtonPlus;
			this.buttonXAdd.ImageFixedSize = new System.Drawing.Size(24, 24);
			this.buttonXAdd.Location = new System.Drawing.Point(5, 6);
			this.buttonXAdd.Name = "buttonXAdd";
			this.buttonXAdd.Size = new System.Drawing.Size(153, 34);
			this.buttonXAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAdd.TabIndex = 7;
			this.buttonXAdd.Text = "   Add Keyword";
			this.buttonXAdd.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXAdd.Click += new System.EventHandler(this.OnAddClick);
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.buttonXReset);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(350, 47);
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
			this.buttonXReset.Size = new System.Drawing.Size(340, 30);
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
			// KeywordsEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "KeywordsEditor";
			this.Size = new System.Drawing.Size(350, 404);
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPartialKeyword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditSharedKeyword)).EndInit();
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private DevComponents.DotNetBar.ButtonX buttonXAdd;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditSharedKeyword;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditPartialKeyword;
	}
}
