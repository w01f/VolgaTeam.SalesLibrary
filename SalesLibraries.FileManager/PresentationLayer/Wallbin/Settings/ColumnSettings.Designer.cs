using System.Drawing;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	sealed partial class ColumnSettings
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnWindow = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnOperations = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditWindowOperations = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.repositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditWindowOperations)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).BeginInit();
			// 
			// gridControl
			// 
			this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 0);
			this.gridControl.MainView = this.gridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditWindowOperations,
            this.repositoryItemTextEdit});
			this.gridControl.Size = new System.Drawing.Size(437, 404);
			this.gridControl.TabIndex = 0;
			this.gridControl.ToolTipController = this.toolTipController;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
			// 
			// gridView
			// 
			this.gridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridView.Appearance.EvenRow.Options.UseFont = true;
			this.gridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedCell.Options.UseFont = true;
			this.gridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.OddRow.Options.UseFont = true;
			this.gridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.Row.Options.UseFont = true;
			this.gridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.SelectedRow.Options.UseFont = true;
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnWindow,
            this.gridColumnOperations});
			this.gridView.GridControl = this.gridControl;
			this.gridView.Name = "gridView";
			this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView.OptionsBehavior.AutoPopulateColumns = false;
			this.gridView.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
			this.gridView.OptionsCustomization.AllowColumnMoving = false;
			this.gridView.OptionsCustomization.AllowColumnResizing = false;
			this.gridView.OptionsCustomization.AllowFilter = false;
			this.gridView.OptionsCustomization.AllowGroup = false;
			this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridView.OptionsCustomization.AllowSort = false;
			this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridView.OptionsView.ShowColumnHeaders = false;
			this.gridView.OptionsView.ShowDetailButtons = false;
			this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridView.OptionsView.ShowGroupPanel = false;
			this.gridView.OptionsView.ShowIndicator = false;
			this.gridView.RowHeight = 30;
			this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
			this.gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
			// 
			// gridColumnWindow
			// 
			this.gridColumnWindow.Caption = "Name";
			this.gridColumnWindow.ColumnEdit = this.repositoryItemTextEdit;
			this.gridColumnWindow.FieldName = "Name";
			this.gridColumnWindow.Name = "gridColumnWindow";
			this.gridColumnWindow.Visible = true;
			this.gridColumnWindow.VisibleIndex = 0;
			this.gridColumnWindow.Width = 260;
			// 
			// gridColumnOperations
			// 
			this.gridColumnOperations.Caption = "Buttons";
			this.gridColumnOperations.ColumnEdit = this.repositoryItemButtonEditWindowOperations;
			this.gridColumnOperations.Name = "gridColumnOperations";
			this.gridColumnOperations.OptionsColumn.FixedWidth = true;
			this.gridColumnOperations.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnOperations.Visible = true;
			this.gridColumnOperations.VisibleIndex = 1;
			this.gridColumnOperations.Width = 200;
			// 
			// repositoryItemButtonEditWindowOperations
			// 
			this.repositoryItemButtonEditWindowOperations.AutoHeight = false;
			this.repositoryItemButtonEditWindowOperations.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowUp, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Move window up", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowDown, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Move window down", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowLeft, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "Move window left", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowRight, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "Move window right", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonSettings, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "Edit window settings", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "Delete window", null, null, true)});
			this.repositoryItemButtonEditWindowOperations.Name = "repositoryItemButtonEditWindowOperations";
			this.repositoryItemButtonEditWindowOperations.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditWindowOperations.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditWindowOperations_ButtonClick);
			// 
			// repositoryItemTextEdit
			// 
			this.repositoryItemTextEdit.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemTextEdit.Appearance.Options.UseFont = true;
			this.repositoryItemTextEdit.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemTextEdit.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemTextEdit.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemTextEdit.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemTextEdit.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemTextEdit.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemTextEdit.AutoHeight = false;
			this.repositoryItemTextEdit.Name = "repositoryItemTextEdit";
			// 
			// ColumnSettings
			// 
			this.Size = new System.Drawing.Size(437, 404);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditWindowOperations)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).EndInit();
		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnWindow;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnOperations;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditWindowOperations;
		private DevExpress.Utils.ToolTipController toolTipController;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit;
	}
}
