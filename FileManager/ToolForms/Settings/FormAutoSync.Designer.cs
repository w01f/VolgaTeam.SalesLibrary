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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoSync));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.gridControlSyncSchedule = new DevExpress.XtraGrid.GridControl();
            this.gridViewSyncSchedule = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.buttonXAddSyncTime = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEnable = new DevComponents.DotNetBar.ButtonX();
            this.buttonXDisable = new DevComponents.DotNetBar.ButtonX();
            this.laHint = new System.Windows.Forms.Label();
            this.gridColumnButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSyncSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSyncSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(67, 354);
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
            this.buttonXCancel.Location = new System.Drawing.Point(204, 354);
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
            // gridControlSyncSchedule
            // 
            this.gridControlSyncSchedule.AllowDrop = true;
            this.gridControlSyncSchedule.Location = new System.Drawing.Point(12, 141);
            this.gridControlSyncSchedule.MainView = this.gridViewSyncSchedule;
            this.gridControlSyncSchedule.Name = "gridControlSyncSchedule";
            this.gridControlSyncSchedule.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTimeEdit,
            this.repositoryItemButtonEdit});
            this.gridControlSyncSchedule.Size = new System.Drawing.Size(341, 200);
            this.gridControlSyncSchedule.TabIndex = 10;
            this.gridControlSyncSchedule.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSyncSchedule});
            // 
            // gridViewSyncSchedule
            // 
            this.gridViewSyncSchedule.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewSyncSchedule.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewSyncSchedule.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewSyncSchedule.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewSyncSchedule.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewSyncSchedule.Appearance.Row.Options.UseFont = true;
            this.gridViewSyncSchedule.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewSyncSchedule.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewSyncSchedule.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDay,
            this.gridColumnTime,
            this.gridColumnButton});
            this.gridViewSyncSchedule.GridControl = this.gridControlSyncSchedule;
            this.gridViewSyncSchedule.Name = "gridViewSyncSchedule";
            this.gridViewSyncSchedule.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridViewSyncSchedule.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewSyncSchedule.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewSyncSchedule.OptionsCustomization.AllowFilter = false;
            this.gridViewSyncSchedule.OptionsCustomization.AllowGroup = false;
            this.gridViewSyncSchedule.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewSyncSchedule.OptionsCustomization.AllowSort = false;
            this.gridViewSyncSchedule.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewSyncSchedule.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewSyncSchedule.OptionsView.RowAutoHeight = true;
            this.gridViewSyncSchedule.OptionsView.ShowDetailButtons = false;
            this.gridViewSyncSchedule.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewSyncSchedule.OptionsView.ShowGroupPanel = false;
            this.gridViewSyncSchedule.OptionsView.ShowIndicator = false;
            this.gridViewSyncSchedule.RowHeight = 40;
            this.gridViewSyncSchedule.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewSyncSchedule_RowClick);
            // 
            // gridColumnDay
            // 
            this.gridColumnDay.Caption = "Week Days";
            this.gridColumnDay.FieldName = "DayString";
            this.gridColumnDay.Name = "gridColumnDay";
            this.gridColumnDay.OptionsColumn.AllowEdit = false;
            this.gridColumnDay.OptionsColumn.ReadOnly = true;
            this.gridColumnDay.Visible = true;
            this.gridColumnDay.VisibleIndex = 0;
            this.gridColumnDay.Width = 189;
            // 
            // gridColumnTime
            // 
            this.gridColumnTime.Caption = "Time";
            this.gridColumnTime.ColumnEdit = this.repositoryItemTimeEdit;
            this.gridColumnTime.FieldName = "Time";
            this.gridColumnTime.Name = "gridColumnTime";
            this.gridColumnTime.OptionsColumn.AllowEdit = false;
            this.gridColumnTime.OptionsColumn.ReadOnly = true;
            this.gridColumnTime.Visible = true;
            this.gridColumnTime.VisibleIndex = 1;
            this.gridColumnTime.Width = 104;
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
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemTimeEdit.DisplayFormat.FormatString = "hh:mm tt";
            this.repositoryItemTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemTimeEdit.EditFormat.FormatString = "hh:mm tt";
            this.repositoryItemTimeEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemTimeEdit.EditValueChangedDelay = 10000;
            this.repositoryItemTimeEdit.Name = "repositoryItemTimeEdit";
            // 
            // buttonXAddSyncTime
            // 
            this.buttonXAddSyncTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXAddSyncTime.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXAddSyncTime.Location = new System.Drawing.Point(12, 69);
            this.buttonXAddSyncTime.Name = "buttonXAddSyncTime";
            this.buttonXAddSyncTime.Size = new System.Drawing.Size(341, 32);
            this.buttonXAddSyncTime.TabIndex = 11;
            this.buttonXAddSyncTime.Text = "Schedule New Sync";
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
            // laHint
            // 
            this.laHint.AutoSize = true;
            this.laHint.Location = new System.Drawing.Point(12, 122);
            this.laHint.Name = "laHint";
            this.laHint.Size = new System.Drawing.Size(265, 16);
            this.laHint.TabIndex = 14;
            this.laHint.Text = "Double-click on a row to edit existed records";
            // 
            // gridColumnButton
            // 
            this.gridColumnButton.ColumnEdit = this.repositoryItemButtonEdit;
            this.gridColumnButton.Name = "gridColumnButton";
            this.gridColumnButton.OptionsColumn.FixedWidth = true;
            this.gridColumnButton.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnButton.Visible = true;
            this.gridColumnButton.VisibleIndex = 2;
            this.gridColumnButton.Width = 44;
            // 
            // repositoryItemButtonEdit
            // 
            this.repositoryItemButtonEdit.AutoHeight = false;
            this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
            this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
            // 
            // FormAutoSync
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(365, 395);
            this.Controls.Add(this.laHint);
            this.Controls.Add(this.buttonXDisable);
            this.Controls.Add(this.buttonXEnable);
            this.Controls.Add(this.buttonXAddSyncTime);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.gridControlSyncSchedule);
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
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSyncSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSyncSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraGrid.GridControl gridControlSyncSchedule;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSyncSchedule;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDay;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit;
        private DevComponents.DotNetBar.ButtonX buttonXAddSyncTime;
        private DevComponents.DotNetBar.ButtonX buttonXDisable;
        private DevComponents.DotNetBar.ButtonX buttonXEnable;
        private System.Windows.Forms.Label laHint;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
    }
}