namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders
{
    partial class FormAddNetworkLink
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
			this.edLinkName = new System.Windows.Forms.TextBox();
			this.buttonEditFolderPath = new DevExpress.XtraEditors.ButtonEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.laTitle = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.laLinkName = new System.Windows.Forms.Label();
			this.laLinkPath = new System.Windows.Forms.Label();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderPath.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// edLinkName
			// 
			this.edLinkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.edLinkName.BackColor = System.Drawing.Color.White;
			this.edLinkName.ForeColor = System.Drawing.Color.Black;
			this.edLinkName.Location = new System.Drawing.Point(7, 89);
			this.edLinkName.Name = "edLinkName";
			this.edLinkName.Size = new System.Drawing.Size(380, 22);
			this.edLinkName.TabIndex = 0;
			// 
			// buttonEditFolderPath
			// 
			this.buttonEditFolderPath.Location = new System.Drawing.Point(7, 147);
			this.buttonEditFolderPath.Name = "buttonEditFolderPath";
			this.buttonEditFolderPath.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditFolderPath.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditFolderPath.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditFolderPath.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditFolderPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditFolderPath.Size = new System.Drawing.Size(380, 22);
			this.buttonEditFolderPath.StyleController = this.styleController;
			this.buttonEditFolderPath.TabIndex = 0;
			this.buttonEditFolderPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditFolderPath_ButtonClick);
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
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.Description = "Select the path";
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(68, 3);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(319, 50);
			this.laTitle.TabIndex = 7;
			this.laTitle.Text = "Add a Network Folder Link to your Sales Library…";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.White;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkAddNetwork;
			this.pbLogo.Location = new System.Drawing.Point(7, 3);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(55, 50);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbLogo.TabIndex = 6;
			this.pbLogo.TabStop = false;
			// 
			// laLinkName
			// 
			this.laLinkName.AutoSize = true;
			this.laLinkName.BackColor = System.Drawing.Color.White;
			this.laLinkName.ForeColor = System.Drawing.Color.Black;
			this.laLinkName.Location = new System.Drawing.Point(4, 70);
			this.laLinkName.Name = "laLinkName";
			this.laLinkName.Size = new System.Drawing.Size(70, 16);
			this.laLinkName.TabIndex = 8;
			this.laLinkName.Text = "Link Name";
			// 
			// laLinkPath
			// 
			this.laLinkPath.AutoSize = true;
			this.laLinkPath.BackColor = System.Drawing.Color.White;
			this.laLinkPath.ForeColor = System.Drawing.Color.Black;
			this.laLinkPath.Location = new System.Drawing.Point(4, 123);
			this.laLinkPath.Name = "laLinkPath";
			this.laLinkPath.Size = new System.Drawing.Size(63, 16);
			this.laLinkPath.TabIndex = 9;
			this.laLinkPath.Text = "Link Path";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(294, 182);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 11;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(195, 182);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(93, 32);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 10;
			this.buttonXSave.Text = "Save";
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			// 
			// FormAddNetworkLink
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(394, 226);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.Controls.Add(this.laLinkPath);
			this.Controls.Add(this.laLinkName);
			this.Controls.Add(this.edLinkName);
			this.Controls.Add(this.buttonEditFolderPath);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.pbLogo);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddNetworkLink";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Network Folder Link";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddLinkForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderPath.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox edLinkName;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;
        private DevExpress.XtraEditors.ButtonEdit buttonEditFolderPath;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Label laLinkName;
		private System.Windows.Forms.Label laLinkPath;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXSave;
    }
}