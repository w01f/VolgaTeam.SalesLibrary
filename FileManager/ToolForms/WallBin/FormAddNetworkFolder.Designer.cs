namespace FileManager.ToolForm.WallBin
{
    partial class FormAddNetworkFolder
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
            this.gbLinkName = new System.Windows.Forms.GroupBox();
            this.edLinkName = new System.Windows.Forms.TextBox();
            this.gbLinkPath = new System.Windows.Forms.GroupBox();
            this.btSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btCancel = new System.Windows.Forms.Button();
            this.laTitle = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.buttonEditFolderPath = new DevExpress.XtraEditors.ButtonEdit();
            this.gbLinkName.SuspendLayout();
            this.gbLinkPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLinkName
            // 
            this.gbLinkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLinkName.Controls.Add(this.edLinkName);
            this.gbLinkName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbLinkName.ForeColor = System.Drawing.Color.Black;
            this.gbLinkName.Location = new System.Drawing.Point(7, 59);
            this.gbLinkName.Name = "gbLinkName";
            this.gbLinkName.Size = new System.Drawing.Size(380, 52);
            this.gbLinkName.TabIndex = 2;
            this.gbLinkName.TabStop = false;
            this.gbLinkName.Text = "Link Name";
            // 
            // edLinkName
            // 
            this.edLinkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edLinkName.Location = new System.Drawing.Point(6, 21);
            this.edLinkName.Name = "edLinkName";
            this.edLinkName.Size = new System.Drawing.Size(368, 22);
            this.edLinkName.TabIndex = 0;
            // 
            // gbLinkPath
            // 
            this.gbLinkPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLinkPath.Controls.Add(this.buttonEditFolderPath);
            this.gbLinkPath.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbLinkPath.ForeColor = System.Drawing.Color.Black;
            this.gbLinkPath.Location = new System.Drawing.Point(7, 117);
            this.gbLinkPath.Name = "gbLinkPath";
            this.gbLinkPath.Size = new System.Drawing.Size(380, 52);
            this.gbLinkPath.TabIndex = 3;
            this.gbLinkPath.TabStop = false;
            this.gbLinkPath.Text = "Link Path";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSave.Location = new System.Drawing.Point(231, 175);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 28);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select the path";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btCancel.Location = new System.Drawing.Point(312, 175);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 28);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(68, 3);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(319, 50);
            this.laTitle.TabIndex = 7;
            this.laTitle.Text = "Add a Network Folder Link to your Sales Library…";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::FileManager.Properties.Resources.AddNetworkShare;
            this.pbLogo.Location = new System.Drawing.Point(7, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(55, 50);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 6;
            this.pbLogo.TabStop = false;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
            // buttonEditFolderPath
            // 
            this.buttonEditFolderPath.Location = new System.Drawing.Point(6, 21);
            this.buttonEditFolderPath.Name = "buttonEditFolderPath";
            this.buttonEditFolderPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditFolderPath.Size = new System.Drawing.Size(368, 22);
            this.buttonEditFolderPath.StyleController = this.styleController;
            this.buttonEditFolderPath.TabIndex = 0;
            this.buttonEditFolderPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditFolderPath_ButtonClick);
            // 
            // FormAddNetworkFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(394, 208);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.gbLinkPath);
            this.Controls.Add(this.gbLinkName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddNetworkFolder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Folder Link";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddLinkForm_FormClosing);
            this.gbLinkName.ResumeLayout(false);
            this.gbLinkName.PerformLayout();
            this.gbLinkPath.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderPath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLinkName;
        private System.Windows.Forms.TextBox edLinkName;
        private System.Windows.Forms.GroupBox gbLinkPath;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;
        private DevExpress.XtraEditors.ButtonEdit buttonEditFolderPath;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
    }
}