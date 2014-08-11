namespace SalesDepot.SiteManager.PresentationClasses.Ticker
{
	sealed partial class TickerManagerControl
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.gridControlTicker = new DevExpress.XtraGrid.GridControl();
			this.gridViewTicker = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnTickerType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTickerText = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTickerActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditTickerActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnTickerOrder = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditTickerOrder = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlTicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditTickerActions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditTickerOrder)).BeginInit();
			this.SuspendLayout();
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
			// gridControlTicker
			// 
			this.gridControlTicker.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlTicker.Location = new System.Drawing.Point(0, 0);
			this.gridControlTicker.MainView = this.gridViewTicker;
			this.gridControlTicker.Name = "gridControlTicker";
			this.gridControlTicker.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditTickerActions,
            this.repositoryItemButtonEditTickerOrder});
			this.gridControlTicker.Size = new System.Drawing.Size(898, 483);
			this.gridControlTicker.TabIndex = 3;
			this.gridControlTicker.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTicker});
			// 
			// gridViewTicker
			// 
			this.gridViewTicker.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTicker.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewTicker.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTicker.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewTicker.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTicker.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewTicker.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewTicker.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewTicker.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTicker.Appearance.OddRow.Options.UseFont = true;
			this.gridViewTicker.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTicker.Appearance.Preview.Options.UseFont = true;
			this.gridViewTicker.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTicker.Appearance.Row.Options.UseFont = true;
			this.gridViewTicker.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTicker.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewTicker.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnTickerType,
            this.gridColumnTickerText,
            this.gridColumnTickerActions,
            this.gridColumnTickerOrder});
			this.gridViewTicker.GridControl = this.gridControlTicker;
			this.gridViewTicker.Name = "gridViewTicker";
			this.gridViewTicker.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewTicker.OptionsCustomization.AllowFilter = false;
			this.gridViewTicker.OptionsCustomization.AllowGroup = false;
			this.gridViewTicker.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewTicker.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewTicker.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewTicker.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewTicker.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewTicker.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewTicker.OptionsView.ShowDetailButtons = false;
			this.gridViewTicker.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewTicker.OptionsView.ShowGroupPanel = false;
			this.gridViewTicker.OptionsView.ShowIndicator = false;
			this.gridViewTicker.OptionsView.ShowPreview = true;
			this.gridViewTicker.PreviewFieldName = "DetailString";
			this.gridViewTicker.PreviewIndent = 5;
			this.gridViewTicker.RowHeight = 35;
			this.gridViewTicker.RowSeparatorHeight = 5;
			// 
			// gridColumnTickerType
			// 
			this.gridColumnTickerType.Caption = "Type";
			this.gridColumnTickerType.FieldName = "TypeString";
			this.gridColumnTickerType.Name = "gridColumnTickerType";
			this.gridColumnTickerType.OptionsColumn.AllowEdit = false;
			this.gridColumnTickerType.OptionsColumn.ReadOnly = true;
			this.gridColumnTickerType.Visible = true;
			this.gridColumnTickerType.VisibleIndex = 1;
			this.gridColumnTickerType.Width = 137;
			// 
			// gridColumnTickerText
			// 
			this.gridColumnTickerText.Caption = "Text";
			this.gridColumnTickerText.FieldName = "text";
			this.gridColumnTickerText.Name = "gridColumnTickerText";
			this.gridColumnTickerText.OptionsColumn.AllowEdit = false;
			this.gridColumnTickerText.OptionsColumn.ReadOnly = true;
			this.gridColumnTickerText.Visible = true;
			this.gridColumnTickerText.VisibleIndex = 2;
			this.gridColumnTickerText.Width = 537;
			// 
			// gridColumnTickerActions
			// 
			this.gridColumnTickerActions.ColumnEdit = this.repositoryItemButtonEditTickerActions;
			this.gridColumnTickerActions.Name = "gridColumnTickerActions";
			this.gridColumnTickerActions.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumnTickerActions.OptionsColumn.FixedWidth = true;
			this.gridColumnTickerActions.OptionsColumn.ShowCaption = false;
			this.gridColumnTickerActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnTickerActions.Visible = true;
			this.gridColumnTickerActions.VisibleIndex = 3;
			this.gridColumnTickerActions.Width = 80;
			// 
			// repositoryItemButtonEditTickerActions
			// 
			this.repositoryItemButtonEditTickerActions.AutoHeight = false;
			this.repositoryItemButtonEditTickerActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesDepot.SiteManager.Properties.Resources.EditTicker, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesDepot.SiteManager.Properties.Resources.DeleteButton, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemButtonEditTickerActions.Name = "repositoryItemButtonEditTickerActions";
			this.repositoryItemButtonEditTickerActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditTickerActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditUsersActions_ButtonClick);
			// 
			// gridColumnTickerOrder
			// 
			this.gridColumnTickerOrder.Caption = "Order";
			this.gridColumnTickerOrder.ColumnEdit = this.repositoryItemButtonEditTickerOrder;
			this.gridColumnTickerOrder.FieldName = "UserOrder";
			this.gridColumnTickerOrder.Name = "gridColumnTickerOrder";
			this.gridColumnTickerOrder.OptionsColumn.FixedWidth = true;
			this.gridColumnTickerOrder.OptionsColumn.ReadOnly = true;
			this.gridColumnTickerOrder.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnTickerOrder.Visible = true;
			this.gridColumnTickerOrder.VisibleIndex = 0;
			this.gridColumnTickerOrder.Width = 140;
			// 
			// repositoryItemButtonEditTickerOrder
			// 
			this.repositoryItemButtonEditTickerOrder.AutoHeight = false;
			this.repositoryItemButtonEditTickerOrder.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesDepot.SiteManager.Properties.Resources.NudgeUp, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesDepot.SiteManager.Properties.Resources.NudgeDown, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
			this.repositoryItemButtonEditTickerOrder.Name = "repositoryItemButtonEditTickerOrder";
			this.repositoryItemButtonEditTickerOrder.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEditTickerOrder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditTickerOrder_ButtonClick);
			// 
			// TickerManagerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.gridControlTicker);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "TickerManagerControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlTicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditTickerActions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditTickerOrder)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraGrid.GridControl gridControlTicker;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTicker;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTickerType;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTickerText;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTickerActions;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditTickerActions;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTickerOrder;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditTickerOrder;
    }
}
