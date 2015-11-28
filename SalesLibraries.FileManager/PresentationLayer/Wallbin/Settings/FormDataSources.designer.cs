namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
    partial class FormDataSources
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.gridControlFolders = new DevExpress.XtraGrid.GridControl();
			this.gridViewFolders = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnPosition = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditOrder = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnPath = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditPath = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.laDeletePageWarning = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFolders)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFolders)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditOrder)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPath)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(246, 391);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 35;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(109, 391);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 34;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// gridControlFolders
			// 
			this.gridControlFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlFolders.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlFolders.Location = new System.Drawing.Point(12, 101);
			this.gridControlFolders.MainView = this.gridViewFolders;
			this.gridControlFolders.Name = "gridControlFolders";
			this.gridControlFolders.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditOrder,
            this.repositoryItemButtonEditPath});
			this.gridControlFolders.Size = new System.Drawing.Size(424, 284);
			this.gridControlFolders.TabIndex = 36;
			this.gridControlFolders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFolders});
			// 
			// gridViewFolders
			// 
			this.gridViewFolders.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFolders.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewFolders.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewFolders.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewFolders.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewFolders.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewFolders.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFolders.Appearance.Row.Options.UseFont = true;
			this.gridViewFolders.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFolders.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewFolders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPosition,
            this.gridColumnPath});
			this.gridViewFolders.GridControl = this.gridControlFolders;
			this.gridViewFolders.Name = "gridViewFolders";
			this.gridViewFolders.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFolders.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFolders.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewFolders.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewFolders.OptionsCustomization.AllowFilter = false;
			this.gridViewFolders.OptionsCustomization.AllowGroup = false;
			this.gridViewFolders.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewFolders.OptionsCustomization.AllowSort = false;
			this.gridViewFolders.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewFolders.OptionsFilter.AllowFilterEditor = false;
			this.gridViewFolders.OptionsFilter.AllowMRUFilterList = false;
			this.gridViewFolders.OptionsMenu.EnableColumnMenu = false;
			this.gridViewFolders.OptionsMenu.EnableFooterMenu = false;
			this.gridViewFolders.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewFolders.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewFolders.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewFolders.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewFolders.OptionsSelection.UseIndicatorForSelection = false;
			this.gridViewFolders.OptionsView.ShowDetailButtons = false;
			this.gridViewFolders.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewFolders.OptionsView.ShowGroupPanel = false;
			this.gridViewFolders.OptionsView.ShowIndicator = false;
			// 
			// gridColumnPosition
			// 
			this.gridColumnPosition.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnPosition.AppearanceCell.Options.UseFont = true;
			this.gridColumnPosition.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnPosition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnPosition.Caption = "Order";
			this.gridColumnPosition.ColumnEdit = this.repositoryItemButtonEditOrder;
			this.gridColumnPosition.FieldName = "Index";
			this.gridColumnPosition.Name = "gridColumnPosition";
			this.gridColumnPosition.OptionsColumn.AllowMove = false;
			this.gridColumnPosition.OptionsColumn.AllowSize = false;
			this.gridColumnPosition.OptionsColumn.FixedWidth = true;
			this.gridColumnPosition.OptionsColumn.ReadOnly = true;
			this.gridColumnPosition.OptionsColumn.ShowCaption = false;
			this.gridColumnPosition.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnPosition.Visible = true;
			this.gridColumnPosition.VisibleIndex = 0;
			this.gridColumnPosition.Width = 120;
			// 
			// repositoryItemButtonEditOrder
			// 
			this.repositoryItemButtonEditOrder.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditOrder.Appearance.Options.UseFont = true;
			this.repositoryItemButtonEditOrder.Appearance.Options.UseTextOptions = true;
			this.repositoryItemButtonEditOrder.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemButtonEditOrder.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditOrder.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemButtonEditOrder.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemButtonEditOrder.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemButtonEditOrder.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditOrder.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemButtonEditOrder.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEditOrder.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemButtonEditOrder.AutoHeight = false;
			this.repositoryItemButtonEditOrder.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowUp, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Nudge Up", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowDown, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Nudge Down", null, null, true)});
			this.repositoryItemButtonEditOrder.Name = "repositoryItemButtonEditOrder";
			this.repositoryItemButtonEditOrder.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEditOrder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
			// 
			// gridColumnPath
			// 
			this.gridColumnPath.Caption = "Path";
			this.gridColumnPath.ColumnEdit = this.repositoryItemButtonEditPath;
			this.gridColumnPath.FieldName = "Path";
			this.gridColumnPath.Name = "gridColumnPath";
			this.gridColumnPath.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnPath.Visible = true;
			this.gridColumnPath.VisibleIndex = 1;
			this.gridColumnPath.Width = 679;
			// 
			// repositoryItemButtonEditPath
			// 
			this.repositoryItemButtonEditPath.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditPath.Appearance.Options.UseFont = true;
			this.repositoryItemButtonEditPath.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditPath.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemButtonEditPath.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditPath.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemButtonEditPath.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemButtonEditPath.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemButtonEditPath.AutoHeight = false;
			this.repositoryItemButtonEditPath.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
			this.repositoryItemButtonEditPath.Name = "repositoryItemButtonEditPath";
			this.repositoryItemButtonEditPath.NullText = "Select Folder...";
			this.repositoryItemButtonEditPath.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEditPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditPath_ButtonClick);
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.Location = new System.Drawing.Point(12, 63);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(424, 32);
			this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonX1.TabIndex = 37;
			this.buttonX1.Text = "Add Root Folder";
			this.buttonX1.TextColor = System.Drawing.Color.Black;
			this.buttonX1.Click += new System.EventHandler(this.buttonXAdd_Click);
			// 
			// laDeletePageWarning
			// 
			this.laDeletePageWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laDeletePageWarning.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDeletePageWarning.ForeColor = System.Drawing.Color.Red;
			this.laDeletePageWarning.Location = new System.Drawing.Point(12, 5);
			this.laDeletePageWarning.Name = "laDeletePageWarning";
			this.laDeletePageWarning.Size = new System.Drawing.Size(424, 55);
			this.laDeletePageWarning.TabIndex = 38;
			this.laDeletePageWarning.Text = "WARNING!\r\nTalk to Billy BEFORE Adding more Root Folders...";
			this.laDeletePageWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormExtraRoots
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(448, 433);
			this.Controls.Add(this.laDeletePageWarning);
			this.Controls.Add(this.buttonX1);
			this.Controls.Add(this.gridControlFolders);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDataSources";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Extra Roots";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPages_FormClosed);
			this.Load += new System.EventHandler(this.Form_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControlFolders)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFolders)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditOrder)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditPath)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevExpress.XtraGrid.GridControl gridControlFolders;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFolders;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPosition;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditOrder;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPath;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditPath;
        private System.Windows.Forms.Label laDeletePageWarning;
    }
}