namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
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
			this.laLocalSyncPath = new System.Windows.Forms.Label();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonEditLocalSyncPath = new DevExpress.XtraEditors.ButtonEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonEditBackupFolder = new DevExpress.XtraEditors.ButtonEdit();
			this.buttonEditWebSyncPath = new DevExpress.XtraEditors.ButtonEdit();
			this.laWebSyncPath = new System.Windows.Forms.Label();
			this.laBackupDescription = new System.Windows.Forms.Label();
			this.laLocalSyncDesription = new System.Windows.Forms.Label();
			this.laWebSyncDescription = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLocalSyncPath.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditBackupFolder.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditWebSyncPath.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// laBackup
			// 
			this.laBackup.AutoSize = true;
			this.laBackup.BackColor = System.Drawing.Color.White;
			this.laBackup.ForeColor = System.Drawing.Color.Black;
			this.laBackup.Location = new System.Drawing.Point(12, 9);
			this.laBackup.Name = "laBackup";
			this.laBackup.Size = new System.Drawing.Size(152, 16);
			this.laBackup.TabIndex = 0;
			this.laBackup.Text = "1. Your SOURCE Folder:";
			// 
			// laLocalSyncPath
			// 
			this.laLocalSyncPath.AutoSize = true;
			this.laLocalSyncPath.BackColor = System.Drawing.Color.White;
			this.laLocalSyncPath.ForeColor = System.Drawing.Color.Black;
			this.laLocalSyncPath.Location = new System.Drawing.Point(12, 101);
			this.laLocalSyncPath.Name = "laLocalSyncPath";
			this.laLocalSyncPath.Size = new System.Drawing.Size(240, 16);
			this.laLocalSyncPath.TabIndex = 3;
			this.laLocalSyncPath.Text = "2. Your Local Library Distribution Folder:";
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(161, 293);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.TabIndex = 3;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(298, 293);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.TabIndex = 4;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonEditLocalSyncPath
			// 
			this.buttonEditLocalSyncPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditLocalSyncPath.Location = new System.Drawing.Point(12, 120);
			this.buttonEditLocalSyncPath.Name = "buttonEditLocalSyncPath";
			this.buttonEditLocalSyncPath.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditLocalSyncPath.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditLocalSyncPath.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditLocalSyncPath.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditLocalSyncPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditLocalSyncPath.Size = new System.Drawing.Size(536, 22);
			this.buttonEditLocalSyncPath.StyleController = this.styleController;
			this.buttonEditLocalSyncPath.TabIndex = 1;
			this.buttonEditLocalSyncPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditFolderSelector_ButtonClick);
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
			// buttonEditBackupFolder
			// 
			this.buttonEditBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditBackupFolder.Location = new System.Drawing.Point(12, 28);
			this.buttonEditBackupFolder.Name = "buttonEditBackupFolder";
			this.buttonEditBackupFolder.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditBackupFolder.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditBackupFolder.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditBackupFolder.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditBackupFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditBackupFolder.Size = new System.Drawing.Size(536, 22);
			this.buttonEditBackupFolder.StyleController = this.styleController;
			this.buttonEditBackupFolder.TabIndex = 0;
			this.buttonEditBackupFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditFolderSelector_ButtonClick);
			// 
			// buttonEditWebSyncPath
			// 
			this.buttonEditWebSyncPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditWebSyncPath.Location = new System.Drawing.Point(12, 216);
			this.buttonEditWebSyncPath.Name = "buttonEditWebSyncPath";
			this.buttonEditWebSyncPath.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditWebSyncPath.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditWebSyncPath.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditWebSyncPath.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditWebSyncPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditWebSyncPath.Size = new System.Drawing.Size(536, 22);
			this.buttonEditWebSyncPath.StyleController = this.styleController;
			this.buttonEditWebSyncPath.TabIndex = 2;
			this.buttonEditWebSyncPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditFolderSelector_ButtonClick);
			// 
			// laWebSyncPath
			// 
			this.laWebSyncPath.AutoSize = true;
			this.laWebSyncPath.BackColor = System.Drawing.Color.White;
			this.laWebSyncPath.ForeColor = System.Drawing.Color.Black;
			this.laWebSyncPath.Location = new System.Drawing.Point(12, 197);
			this.laWebSyncPath.Name = "laWebSyncPath";
			this.laWebSyncPath.Size = new System.Drawing.Size(217, 16);
			this.laWebSyncPath.TabIndex = 12;
			this.laWebSyncPath.Text = "3. Your Cloud Library Upload Folder:";
			// 
			// laBackupDescription
			// 
			this.laBackupDescription.AutoSize = true;
			this.laBackupDescription.BackColor = System.Drawing.Color.White;
			this.laBackupDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laBackupDescription.ForeColor = System.Drawing.Color.Black;
			this.laBackupDescription.Location = new System.Drawing.Point(12, 53);
			this.laBackupDescription.Name = "laBackupDescription";
			this.laBackupDescription.Size = new System.Drawing.Size(387, 15);
			this.laBackupDescription.TabIndex = 14;
			this.laBackupDescription.Text = "(This is the top level folder where you keep all of your sales materials)";
			// 
			// laLocalSyncDesription
			// 
			this.laLocalSyncDesription.AutoSize = true;
			this.laLocalSyncDesription.BackColor = System.Drawing.Color.White;
			this.laLocalSyncDesription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laLocalSyncDesription.ForeColor = System.Drawing.Color.Black;
			this.laLocalSyncDesription.Location = new System.Drawing.Point(12, 145);
			this.laLocalSyncDesription.Name = "laLocalSyncDesription";
			this.laLocalSyncDesription.Size = new System.Drawing.Size(341, 15);
			this.laLocalSyncDesription.TabIndex = 15;
			this.laLocalSyncDesription.Text = "(This is where you “share” your sales library with other users)";
			// 
			// laWebSyncDescription
			// 
			this.laWebSyncDescription.AutoSize = true;
			this.laWebSyncDescription.BackColor = System.Drawing.Color.White;
			this.laWebSyncDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laWebSyncDescription.ForeColor = System.Drawing.Color.Black;
			this.laWebSyncDescription.Location = new System.Drawing.Point(12, 241);
			this.laWebSyncDescription.Name = "laWebSyncDescription";
			this.laWebSyncDescription.Size = new System.Drawing.Size(378, 15);
			this.laWebSyncDescription.TabIndex = 16;
			this.laWebSyncDescription.Text = "(This is a “cloud-copy” of your library, configured for mobile and web)";
			// 
			// FormPaths
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(552, 334);
			this.Controls.Add(this.laWebSyncDescription);
			this.Controls.Add(this.laLocalSyncDesription);
			this.Controls.Add(this.laBackupDescription);
			this.Controls.Add(this.buttonEditWebSyncPath);
			this.Controls.Add(this.laWebSyncPath);
			this.Controls.Add(this.buttonEditBackupFolder);
			this.Controls.Add(this.buttonEditLocalSyncPath);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.laLocalSyncPath);
			this.Controls.Add(this.laBackup);
			this.DoubleBuffered = true;
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
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLocalSyncPath.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditBackupFolder.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditWebSyncPath.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laBackup;
        private System.Windows.Forms.Label laLocalSyncPath;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.ButtonEdit buttonEditLocalSyncPath;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.ButtonEdit buttonEditBackupFolder;
		private DevExpress.XtraEditors.ButtonEdit buttonEditWebSyncPath;
		private System.Windows.Forms.Label laWebSyncPath;
		private System.Windows.Forms.Label laBackupDescription;
		private System.Windows.Forms.Label laLocalSyncDesription;
		private System.Windows.Forms.Label laWebSyncDescription;
    }
}