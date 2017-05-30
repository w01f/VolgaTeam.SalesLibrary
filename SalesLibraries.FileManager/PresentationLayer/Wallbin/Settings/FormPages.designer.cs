namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
    partial class FormPages
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
			this.laDeletePageWarning = new System.Windows.Forms.Label();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.gridControlPages = new DevExpress.XtraGrid.GridControl();
			this.gridViewPages = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnPosition = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.buttonXRemove = new DevComponents.DotNetBar.ButtonX();
			this.buttonXAdd = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.gridControlPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			this.SuspendLayout();
			// 
			// laDeletePageWarning
			// 
			this.laDeletePageWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laDeletePageWarning.BackColor = System.Drawing.Color.White;
			this.laDeletePageWarning.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDeletePageWarning.ForeColor = System.Drawing.Color.Black;
			this.laDeletePageWarning.Location = new System.Drawing.Point(12, 352);
			this.laDeletePageWarning.Name = "laDeletePageWarning";
			this.laDeletePageWarning.Size = new System.Drawing.Size(377, 80);
			this.laDeletePageWarning.TabIndex = 33;
			this.laDeletePageWarning.Text = "WARNING:\r\n If you Delete the Page Name, \r\nthen the Page Links will be LOST FOREVE" +
    "R!";
			this.laDeletePageWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(246, 435);
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
			this.buttonXOK.Location = new System.Drawing.Point(109, 435);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 34;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// gridControlPages
			// 
			this.gridControlPages.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlPages.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlPages.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlPages.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlPages.Location = new System.Drawing.Point(12, 12);
			this.gridControlPages.MainView = this.gridViewPages;
			this.gridControlPages.Name = "gridControlPages";
			this.gridControlPages.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit});
			this.gridControlPages.Size = new System.Drawing.Size(377, 337);
			this.gridControlPages.TabIndex = 36;
			this.gridControlPages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPages});
			// 
			// gridViewPages
			// 
			this.gridViewPages.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewPages.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewPages.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewPages.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewPages.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewPages.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.Row.Options.UseFont = true;
			this.gridViewPages.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewPages.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewPages.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPosition,
            this.gridColumnName});
			this.gridViewPages.GridControl = this.gridControlPages;
			this.gridViewPages.Name = "gridViewPages";
			this.gridViewPages.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewPages.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewPages.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewPages.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewPages.OptionsCustomization.AllowFilter = false;
			this.gridViewPages.OptionsCustomization.AllowGroup = false;
			this.gridViewPages.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewPages.OptionsCustomization.AllowSort = false;
			this.gridViewPages.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewPages.OptionsFilter.AllowFilterEditor = false;
			this.gridViewPages.OptionsFilter.AllowMRUFilterList = false;
			this.gridViewPages.OptionsMenu.EnableColumnMenu = false;
			this.gridViewPages.OptionsMenu.EnableFooterMenu = false;
			this.gridViewPages.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewPages.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewPages.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewPages.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewPages.OptionsSelection.UseIndicatorForSelection = false;
			this.gridViewPages.OptionsView.ShowDetailButtons = false;
			this.gridViewPages.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewPages.OptionsView.ShowGroupPanel = false;
			this.gridViewPages.OptionsView.ShowIndicator = false;
			// 
			// gridColumnPosition
			// 
			this.gridColumnPosition.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnPosition.AppearanceCell.Options.UseFont = true;
			this.gridColumnPosition.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnPosition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnPosition.Caption = "Order";
			this.gridColumnPosition.ColumnEdit = this.repositoryItemButtonEdit;
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
			// repositoryItemButtonEdit
			// 
			this.repositoryItemButtonEdit.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEdit.Appearance.Options.UseFont = true;
			this.repositoryItemButtonEdit.Appearance.Options.UseTextOptions = true;
			this.repositoryItemButtonEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemButtonEdit.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEdit.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemButtonEdit.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemButtonEdit.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemButtonEdit.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEdit.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemButtonEdit.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemButtonEdit.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemButtonEdit.AutoHeight = false;
			this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowUp, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Nudge Up", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ArrowDown, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Nudge Down", null, null, true)});
			this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
			this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
			// 
			// gridColumnName
			// 
			this.gridColumnName.Caption = "Page";
			this.gridColumnName.FieldName = "Name";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.Visible = true;
			this.gridColumnName.VisibleIndex = 1;
			this.gridColumnName.Width = 679;
			// 
			// buttonXRemove
			// 
			this.buttonXRemove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRemove.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRemove.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonXRemove.Image = global::SalesLibraries.FileManager.Properties.Resources.ButtonDelete;
			this.buttonXRemove.ImageFixedSize = new System.Drawing.Size(24, 24);
			this.buttonXRemove.Location = new System.Drawing.Point(400, 57);
			this.buttonXRemove.Name = "buttonXRemove";
			this.buttonXRemove.Size = new System.Drawing.Size(37, 37);
			this.buttonXRemove.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRemove.TabIndex = 38;
			this.buttonXRemove.Click += new System.EventHandler(this.buttonXRemove_Click);
			// 
			// buttonXAdd
			// 
			this.buttonXAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAdd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonXAdd.Image = global::SalesLibraries.FileManager.Properties.Resources.ButtonPlus;
			this.buttonXAdd.Location = new System.Drawing.Point(400, 12);
			this.buttonXAdd.Name = "buttonXAdd";
			this.buttonXAdd.Size = new System.Drawing.Size(37, 37);
			this.buttonXAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAdd.TabIndex = 37;
			this.buttonXAdd.Click += new System.EventHandler(this.buttonXAdd_Click);
			// 
			// FormPages
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(448, 477);
			this.Controls.Add(this.buttonXRemove);
			this.Controls.Add(this.buttonXAdd);
			this.Controls.Add(this.gridControlPages);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.laDeletePageWarning);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPages";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Pages";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPages_FormClosing);
			this.Load += new System.EventHandler(this.Form_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControlPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laDeletePageWarning;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.XtraGrid.GridControl gridControlPages;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPages;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPosition;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevComponents.DotNetBar.ButtonX buttonXRemove;
        private DevComponents.DotNetBar.ButtonX buttonXAdd;
    }
}