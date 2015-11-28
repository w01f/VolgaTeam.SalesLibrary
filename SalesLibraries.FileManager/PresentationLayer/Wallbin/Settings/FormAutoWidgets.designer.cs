namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
    partial class FormAutoWidgets
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
			this.components = new System.ComponentModel.Container();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.gridControlAutoWidgets = new DevExpress.XtraGrid.GridControl();
			this.gridViewAutoWidgets = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnExtension = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnWidget = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.buttonEditNewExtension = new DevExpress.XtraEditors.ButtonEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlAutoWidgets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewAutoWidgets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditNewExtension.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(73, 321);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 8;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(210, 321);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 9;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
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
			// gridControlAutoWidgets
			// 
			this.gridControlAutoWidgets.AllowDrop = true;
			this.gridControlAutoWidgets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlAutoWidgets.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlAutoWidgets.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlAutoWidgets.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlAutoWidgets.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlAutoWidgets.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlAutoWidgets.Location = new System.Drawing.Point(12, 56);
			this.gridControlAutoWidgets.MainView = this.gridViewAutoWidgets;
			this.gridControlAutoWidgets.Name = "gridControlAutoWidgets";
			this.gridControlAutoWidgets.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemPictureEdit});
			this.gridControlAutoWidgets.Size = new System.Drawing.Size(353, 259);
			this.gridControlAutoWidgets.TabIndex = 10;
			this.gridControlAutoWidgets.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAutoWidgets});
			// 
			// gridViewAutoWidgets
			// 
			this.gridViewAutoWidgets.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAutoWidgets.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewAutoWidgets.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewAutoWidgets.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewAutoWidgets.Appearance.Row.Options.UseFont = true;
			this.gridViewAutoWidgets.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewAutoWidgets.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewAutoWidgets.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnExtension,
            this.gridColumnWidget});
			this.gridViewAutoWidgets.GridControl = this.gridControlAutoWidgets;
			this.gridViewAutoWidgets.Name = "gridViewAutoWidgets";
			this.gridViewAutoWidgets.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewAutoWidgets.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewAutoWidgets.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewAutoWidgets.OptionsCustomization.AllowFilter = false;
			this.gridViewAutoWidgets.OptionsCustomization.AllowGroup = false;
			this.gridViewAutoWidgets.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewAutoWidgets.OptionsCustomization.AllowSort = false;
			this.gridViewAutoWidgets.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewAutoWidgets.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewAutoWidgets.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewAutoWidgets.OptionsView.RowAutoHeight = true;
			this.gridViewAutoWidgets.OptionsView.ShowColumnHeaders = false;
			this.gridViewAutoWidgets.OptionsView.ShowDetailButtons = false;
			this.gridViewAutoWidgets.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewAutoWidgets.OptionsView.ShowGroupPanel = false;
			this.gridViewAutoWidgets.OptionsView.ShowIndicator = false;
			// 
			// gridColumnExtension
			// 
			this.gridColumnExtension.Caption = "Extension";
			this.gridColumnExtension.ColumnEdit = this.repositoryItemButtonEdit;
			this.gridColumnExtension.FieldName = "Extension";
			this.gridColumnExtension.Name = "gridColumnExtension";
			this.gridColumnExtension.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnExtension.Visible = true;
			this.gridColumnExtension.VisibleIndex = 0;
			this.gridColumnExtension.Width = 289;
			// 
			// repositoryItemButtonEdit
			// 
			this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonAutoWidgets, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
			this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
			// 
			// gridColumnWidget
			// 
			this.gridColumnWidget.Caption = "Widget";
			this.gridColumnWidget.ColumnEdit = this.repositoryItemPictureEdit;
			this.gridColumnWidget.FieldName = "Widget";
			this.gridColumnWidget.Name = "gridColumnWidget";
			this.gridColumnWidget.OptionsColumn.FixedWidth = true;
			this.gridColumnWidget.Visible = true;
			this.gridColumnWidget.VisibleIndex = 1;
			this.gridColumnWidget.Width = 64;
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.AllowFocused = false;
			this.repositoryItemPictureEdit.Appearance.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.NullText = "Empty";
			this.repositoryItemPictureEdit.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.ShowMenu = false;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			// 
			// buttonEditNewExtension
			// 
			this.buttonEditNewExtension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditNewExtension.Location = new System.Drawing.Point(12, 9);
			this.buttonEditNewExtension.Name = "buttonEditNewExtension";
			this.buttonEditNewExtension.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditNewExtension.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditNewExtension.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditNewExtension.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditNewExtension.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.FileManager.Properties.Resources.ButtonPlus, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
			this.buttonEditNewExtension.Properties.NullText = "Add new extension here...";
			this.buttonEditNewExtension.Size = new System.Drawing.Size(353, 38);
			this.buttonEditNewExtension.StyleController = this.styleController;
			this.buttonEditNewExtension.TabIndex = 11;
			this.buttonEditNewExtension.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditNewExtension_ButtonClick);
			this.buttonEditNewExtension.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonEditNewExtension_KeyDown);
			// 
			// FormAutoWidgets
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(377, 362);
			this.Controls.Add(this.buttonEditNewExtension);
			this.Controls.Add(this.gridControlAutoWidgets);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoWidgets";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auto Widgets";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAutoWidgets_FormClosed);
			this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlAutoWidgets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewAutoWidgets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditNewExtension.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraGrid.GridControl gridControlAutoWidgets;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAutoWidgets;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnExtension;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnWidget;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
        private DevExpress.XtraEditors.ButtonEdit buttonEditNewExtension;
    }
}