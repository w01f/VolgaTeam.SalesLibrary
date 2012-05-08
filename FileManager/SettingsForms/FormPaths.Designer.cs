namespace FileManager.SettingsForms
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
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditNetworkSyncFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditBackupFolder.Properties)).BeginInit();
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
            this.buttonXOK.Location = new System.Drawing.Point(67, 113);
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
            this.buttonXCancel.Location = new System.Drawing.Point(204, 113);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
            this.buttonXCancel.TabIndex = 9;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // buttonEditNetworkSyncFolder
            // 
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
            this.buttonEditBackupFolder.Location = new System.Drawing.Point(12, 28);
            this.buttonEditBackupFolder.Name = "buttonEditBackupFolder";
            this.buttonEditBackupFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditBackupFolder.Size = new System.Drawing.Size(349, 22);
            this.buttonEditBackupFolder.StyleController = this.styleController;
            this.buttonEditBackupFolder.TabIndex = 11;
            this.buttonEditBackupFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditBackupFolder_ButtonClick);
            // 
            // FormPaths
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(365, 154);
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
    }
}