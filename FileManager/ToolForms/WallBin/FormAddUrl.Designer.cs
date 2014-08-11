namespace FileManager.ToolForms.WallBin
{
    partial class FormAddUrl
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
			this.textEditWebAddress = new DevExpress.XtraEditors.TextEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.laTitle = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.laLinkPath = new System.Windows.Forms.Label();
			this.laLinkName = new System.Windows.Forms.Label();
			this.edLinkName = new System.Windows.Forms.TextBox();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.textEditWebAddress.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// textEditWebAddress
			// 
			this.textEditWebAddress.Location = new System.Drawing.Point(7, 137);
			this.textEditWebAddress.Name = "textEditWebAddress";
			this.textEditWebAddress.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditWebAddress.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditWebAddress.Properties.Appearance.Options.UseBackColor = true;
			this.textEditWebAddress.Properties.Appearance.Options.UseForeColor = true;
			this.textEditWebAddress.Size = new System.Drawing.Size(380, 22);
			this.textEditWebAddress.StyleController = this.styleController;
			this.textEditWebAddress.TabIndex = 11;
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
			this.laTitle.TabIndex = 9;
			this.laTitle.Text = "Add a Website Link to your Sales Library…";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.White;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::FileManager.Properties.Resources.AddUrl;
			this.pbLogo.Location = new System.Drawing.Point(7, 3);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(55, 50);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbLogo.TabIndex = 8;
			this.pbLogo.TabStop = false;
			// 
			// laLinkPath
			// 
			this.laLinkPath.AutoSize = true;
			this.laLinkPath.BackColor = System.Drawing.Color.White;
			this.laLinkPath.ForeColor = System.Drawing.Color.Black;
			this.laLinkPath.Location = new System.Drawing.Point(4, 118);
			this.laLinkPath.Name = "laLinkPath";
			this.laLinkPath.Size = new System.Drawing.Size(63, 16);
			this.laLinkPath.TabIndex = 14;
			this.laLinkPath.Text = "Link Path";
			// 
			// laLinkName
			// 
			this.laLinkName.AutoSize = true;
			this.laLinkName.BackColor = System.Drawing.Color.White;
			this.laLinkName.ForeColor = System.Drawing.Color.Black;
			this.laLinkName.Location = new System.Drawing.Point(4, 65);
			this.laLinkName.Name = "laLinkName";
			this.laLinkName.Size = new System.Drawing.Size(70, 16);
			this.laLinkName.TabIndex = 13;
			this.laLinkName.Text = "Link Name";
			// 
			// edLinkName
			// 
			this.edLinkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.edLinkName.BackColor = System.Drawing.Color.White;
			this.edLinkName.ForeColor = System.Drawing.Color.Black;
			this.edLinkName.Location = new System.Drawing.Point(7, 84);
			this.edLinkName.Name = "edLinkName";
			this.edLinkName.Size = new System.Drawing.Size(380, 22);
			this.edLinkName.TabIndex = 12;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(294, 171);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 16;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(195, 171);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(93, 32);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 15;
			this.buttonXSave.Text = "Save";
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			// 
			// FormAddUrl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(394, 214);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.Controls.Add(this.laLinkPath);
			this.Controls.Add(this.laLinkName);
			this.Controls.Add(this.edLinkName);
			this.Controls.Add(this.textEditWebAddress);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.pbLogo);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddUrl";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Website Link";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddLinkForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.textEditWebAddress.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbLogo;
		private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.TextEdit textEditWebAddress;
		private System.Windows.Forms.Label laLinkPath;
		private System.Windows.Forms.Label laLinkName;
		private System.Windows.Forms.TextBox edLinkName;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXSave;
    }
}