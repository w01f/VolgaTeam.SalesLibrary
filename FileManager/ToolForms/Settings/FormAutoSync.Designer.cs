namespace FileManager.ToolForms.Settings
{
    partial class FormAutoSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoSync));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.styleController = new DevExpress.XtraEditors.StyleController();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.gridControlSyncTimes = new DevExpress.XtraGrid.GridControl();
            this.gridViewSyncTimes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBoxWeekDays = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.buttonXAddSyncTime = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEnable = new DevComponents.DotNetBar.ButtonX();
            this.buttonXDisable = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSyncTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSyncTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxWeekDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(67, 319);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(93, 32);
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
            this.buttonXCancel.Location = new System.Drawing.Point(204, 319);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
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
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // gridControlSyncTimes
            // 
            this.gridControlSyncTimes.AllowDrop = true;
            this.gridControlSyncTimes.Location = new System.Drawing.Point(12, 107);
            this.gridControlSyncTimes.MainView = this.gridViewSyncTimes;
            this.gridControlSyncTimes.Name = "gridControlSyncTimes";
            this.gridControlSyncTimes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxWeekDays,
            this.repositoryItemTimeEdit});
            this.gridControlSyncTimes.Size = new System.Drawing.Size(341, 200);
            this.gridControlSyncTimes.TabIndex = 10;
            this.gridControlSyncTimes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSyncTimes});
            // 
            // gridViewSyncTimes
            // 
            this.gridViewSyncTimes.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewSyncTimes.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewSyncTimes.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewSyncTimes.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewSyncTimes.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewSyncTimes.Appearance.Row.Options.UseFont = true;
            this.gridViewSyncTimes.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewSyncTimes.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewSyncTimes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDay,
            this.gridColumnTime});
            this.gridViewSyncTimes.GridControl = this.gridControlSyncTimes;
            this.gridViewSyncTimes.Name = "gridViewSyncTimes";
            this.gridViewSyncTimes.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridViewSyncTimes.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewSyncTimes.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewSyncTimes.OptionsCustomization.AllowFilter = false;
            this.gridViewSyncTimes.OptionsCustomization.AllowGroup = false;
            this.gridViewSyncTimes.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewSyncTimes.OptionsCustomization.AllowSort = false;
            this.gridViewSyncTimes.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewSyncTimes.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewSyncTimes.OptionsView.RowAutoHeight = true;
            this.gridViewSyncTimes.OptionsView.ShowDetailButtons = false;
            this.gridViewSyncTimes.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewSyncTimes.OptionsView.ShowGroupPanel = false;
            this.gridViewSyncTimes.OptionsView.ShowIndicator = false;
            this.gridViewSyncTimes.RowHeight = 40;
            // 
            // gridColumnDay
            // 
            this.gridColumnDay.Caption = "Day";
            this.gridColumnDay.ColumnEdit = this.repositoryItemComboBoxWeekDays;
            this.gridColumnDay.FieldName = "Day";
            this.gridColumnDay.Name = "gridColumnDay";
            this.gridColumnDay.Visible = true;
            this.gridColumnDay.VisibleIndex = 0;
            this.gridColumnDay.Width = 127;
            // 
            // repositoryItemComboBoxWeekDays
            // 
            this.repositoryItemComboBoxWeekDays.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxWeekDays.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxWeekDays.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxWeekDays.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxWeekDays.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxWeekDays.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxWeekDays.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxWeekDays.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxWeekDays.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxWeekDays.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxWeekDays.AutoHeight = false;
            this.repositoryItemComboBoxWeekDays.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxWeekDays.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.repositoryItemComboBoxWeekDays.Name = "repositoryItemComboBoxWeekDays";
            this.repositoryItemComboBoxWeekDays.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridColumnTime
            // 
            this.gridColumnTime.Caption = "Time";
            this.gridColumnTime.ColumnEdit = this.repositoryItemTimeEdit;
            this.gridColumnTime.FieldName = "Time";
            this.gridColumnTime.Name = "gridColumnTime";
            this.gridColumnTime.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnTime.Visible = true;
            this.gridColumnTime.VisibleIndex = 1;
            this.gridColumnTime.Width = 210;
            // 
            // repositoryItemTimeEdit
            // 
            this.repositoryItemTimeEdit.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemTimeEdit.Appearance.Options.UseFont = true;
            this.repositoryItemTimeEdit.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemTimeEdit.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemTimeEdit.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemTimeEdit.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemTimeEdit.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemTimeEdit.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemTimeEdit.AutoHeight = false;
            this.repositoryItemTimeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemTimeEdit.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemTimeEdit.EditValueChangedDelay = 10000;
            this.repositoryItemTimeEdit.Name = "repositoryItemTimeEdit";
            this.repositoryItemTimeEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemTimeEdit_ButtonClick);
            // 
            // buttonXAddSyncTime
            // 
            this.buttonXAddSyncTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXAddSyncTime.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXAddSyncTime.Location = new System.Drawing.Point(12, 69);
            this.buttonXAddSyncTime.Name = "buttonXAddSyncTime";
            this.buttonXAddSyncTime.Size = new System.Drawing.Size(341, 32);
            this.buttonXAddSyncTime.TabIndex = 11;
            this.buttonXAddSyncTime.Text = "Add Sync Time";
            this.buttonXAddSyncTime.TextColor = System.Drawing.Color.Black;
            this.buttonXAddSyncTime.Click += new System.EventHandler(this.buttonXAddSyncTime_Click);
            // 
            // buttonXEnable
            // 
            this.buttonXEnable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXEnable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEnable.Location = new System.Drawing.Point(12, 12);
            this.buttonXEnable.Name = "buttonXEnable";
            this.buttonXEnable.Size = new System.Drawing.Size(148, 32);
            this.buttonXEnable.TabIndex = 12;
            this.buttonXEnable.Text = "Enable";
            this.buttonXEnable.TextColor = System.Drawing.Color.Black;
            this.buttonXEnable.CheckedChanged += new System.EventHandler(this.buttonXEnable_CheckedChanged);
            this.buttonXEnable.Click += new System.EventHandler(this.buttonXEnable_Click);
            // 
            // buttonXDisable
            // 
            this.buttonXDisable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDisable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDisable.Location = new System.Drawing.Point(205, 12);
            this.buttonXDisable.Name = "buttonXDisable";
            this.buttonXDisable.Size = new System.Drawing.Size(148, 32);
            this.buttonXDisable.TabIndex = 13;
            this.buttonXDisable.Text = "Disable";
            this.buttonXDisable.TextColor = System.Drawing.Color.Black;
            this.buttonXDisable.CheckedChanged += new System.EventHandler(this.buttonXEnable_CheckedChanged);
            this.buttonXDisable.Click += new System.EventHandler(this.buttonXEnable_Click);
            // 
            // FormAutoSync
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(365, 360);
            this.Controls.Add(this.buttonXDisable);
            this.Controls.Add(this.buttonXEnable);
            this.Controls.Add(this.buttonXAddSyncTime);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.gridControlSyncTimes);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAutoSync";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Sync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApplicationSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSyncTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSyncTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxWeekDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraGrid.GridControl gridControlSyncTimes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSyncTimes;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDay;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxWeekDays;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit;
        private DevComponents.DotNetBar.ButtonX buttonXAddSyncTime;
        private DevComponents.DotNetBar.ButtonX buttonXDisable;
        private DevComponents.DotNetBar.ButtonX buttonXEnable;
    }
}