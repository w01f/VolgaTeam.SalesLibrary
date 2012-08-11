namespace FileManager.ToolForms.Settings
{
    partial class FormPaths
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
            this.laBackup = new System.Windows.Forms.Label();
            this.laNetwork = new System.Windows.Forms.Label();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonEditNetworkSyncFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.buttonEditBackupFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.checkEditDirectAccess = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditFileAgeLimit = new DevExpress.XtraEditors.CheckEdit();
            this.spinEditFileAgeLimil = new DevExpress.XtraEditors.SpinEdit();
            this.laFileAgeLimit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditNetworkSyncFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditBackupFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDirectAccess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFileAgeLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFileAgeLimil.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // laBackup
            // 
            this.laBackup.AutoSize = true;
            this.laBackup.Location = new System.Drawing.Point(12, 9);
            this.laBackup.Name = "laBackup";
            this.laBackup.Size = new System.Drawing.Size(136, 16);
            this.laBackup.TabIndex = 0;
            this.laBackup.Text = "Primary Backup Root:";
            // 
            // laNetwork
            // 
            this.laNetwork.AutoSize = true;
            this.laNetwork.Location = new System.Drawing.Point(12, 63);
            this.laNetwork.Name = "laNetwork";
            this.laNetwork.Size = new System.Drawing.Size(133, 16);
            this.laNetwork.TabIndex = 3;
            this.laNetwork.Text = "Network Sync Folder:";
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(67, 190);
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
            this.buttonXCancel.Location = new System.Drawing.Point(204, 190);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
            this.buttonXCancel.TabIndex = 9;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // buttonEditNetworkSyncFolder
            // 
            this.buttonEditNetworkSyncFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditNetworkSyncFolder.Location = new System.Drawing.Point(12, 82);
            this.buttonEditNetworkSyncFolder.Name = "buttonEditNetworkSyncFolder";
            this.buttonEditNetworkSyncFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditNetworkSyncFolder.Size = new System.Drawing.Size(349, 22);
            this.buttonEditNetworkSyncFolder.StyleController = this.styleController;
            this.buttonEditNetworkSyncFolder.TabIndex = 10;
            this.buttonEditNetworkSyncFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditNetworkSyncFolder_ButtonClick);
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
            // buttonEditBackupFolder
            // 
            this.buttonEditBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditBackupFolder.Location = new System.Drawing.Point(12, 28);
            this.buttonEditBackupFolder.Name = "buttonEditBackupFolder";
            this.buttonEditBackupFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditBackupFolder.Size = new System.Drawing.Size(349, 22);
            this.buttonEditBackupFolder.StyleController = this.styleController;
            this.buttonEditBackupFolder.TabIndex = 11;
            this.buttonEditBackupFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditBackupFolder_ButtonClick);
            // 
            // checkEditDirectAccess
            // 
            this.checkEditDirectAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkEditDirectAccess.Location = new System.Drawing.Point(13, 121);
            this.checkEditDirectAccess.Name = "checkEditDirectAccess";
            this.checkEditDirectAccess.Properties.AutoWidth = true;
            this.checkEditDirectAccess.Properties.Caption = "Use Direct Access to Files";
            this.checkEditDirectAccess.Size = new System.Drawing.Size(179, 21);
            this.checkEditDirectAccess.StyleController = this.styleController;
            this.checkEditDirectAccess.TabIndex = 12;
            this.checkEditDirectAccess.CheckedChanged += new System.EventHandler(this.checkEditDirectAccess_CheckedChanged);
            // 
            // checkEditFileAgeLimit
            // 
            this.checkEditFileAgeLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkEditFileAgeLimit.Enabled = false;
            this.checkEditFileAgeLimit.Location = new System.Drawing.Point(13, 153);
            this.checkEditFileAgeLimit.Name = "checkEditFileAgeLimit";
            this.checkEditFileAgeLimit.Properties.AutoWidth = true;
            this.checkEditFileAgeLimit.Properties.Caption = "Sync only last";
            this.checkEditFileAgeLimit.Size = new System.Drawing.Size(106, 21);
            this.checkEditFileAgeLimit.StyleController = this.styleController;
            this.checkEditFileAgeLimit.TabIndex = 13;
            this.checkEditFileAgeLimit.CheckedChanged += new System.EventHandler(this.checkEditFileAgeLimit_CheckedChanged);
            // 
            // spinEditFileAgeLimil
            // 
            this.spinEditFileAgeLimil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spinEditFileAgeLimil.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditFileAgeLimil.Enabled = false;
            this.spinEditFileAgeLimil.Location = new System.Drawing.Point(144, 152);
            this.spinEditFileAgeLimil.Name = "spinEditFileAgeLimil";
            this.spinEditFileAgeLimil.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditFileAgeLimil.Properties.IsFloatValue = false;
            this.spinEditFileAgeLimil.Properties.Mask.EditMask = "N00";
            this.spinEditFileAgeLimil.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spinEditFileAgeLimil.Size = new System.Drawing.Size(70, 22);
            this.spinEditFileAgeLimil.StyleController = this.styleController;
            this.spinEditFileAgeLimil.TabIndex = 14;
            // 
            // laFileAgeLimit
            // 
            this.laFileAgeLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.laFileAgeLimit.AutoSize = true;
            this.laFileAgeLimit.Enabled = false;
            this.laFileAgeLimit.Location = new System.Drawing.Point(220, 156);
            this.laFileAgeLimit.Name = "laFileAgeLimit";
            this.laFileAgeLimit.Size = new System.Drawing.Size(36, 16);
            this.laFileAgeLimit.TabIndex = 15;
            this.laFileAgeLimit.Text = "days";
            // 
            // FormPaths
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(365, 231);
            this.Controls.Add(this.laFileAgeLimit);
            this.Controls.Add(this.spinEditFileAgeLimil);
            this.Controls.Add(this.checkEditFileAgeLimit);
            this.Controls.Add(this.checkEditDirectAccess);
            this.Controls.Add(this.buttonEditBackupFolder);
            this.Controls.Add(this.buttonEditNetworkSyncFolder);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.laNetwork);
            this.Controls.Add(this.laBackup);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPaths";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paths";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApplicationSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditNetworkSyncFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditBackupFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDirectAccess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFileAgeLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFileAgeLimil.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laBackup;
        private System.Windows.Forms.Label laNetwork;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.ButtonEdit buttonEditNetworkSyncFolder;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.ButtonEdit buttonEditBackupFolder;
        private DevExpress.XtraEditors.CheckEdit checkEditDirectAccess;
        private DevExpress.XtraEditors.CheckEdit checkEditFileAgeLimit;
        private DevExpress.XtraEditors.SpinEdit spinEditFileAgeLimil;
        private System.Windows.Forms.Label laFileAgeLimit;
    }
}