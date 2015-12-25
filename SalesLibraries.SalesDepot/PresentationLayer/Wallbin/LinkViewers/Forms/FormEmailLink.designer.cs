namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
    partial class FormEmailLink
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
			this.pbEmail = new System.Windows.Forms.PictureBox();
			this.ckChangeEmailName = new System.Windows.Forms.CheckBox();
			this.textEditEmailName = new DevExpress.XtraEditors.TextEdit();
			this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.pbEmail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pbEmail
			// 
			this.pbEmail.BackColor = System.Drawing.Color.White;
			this.pbEmail.ForeColor = System.Drawing.Color.Black;
			this.pbEmail.Image = global::SalesLibraries.SalesDepot.Properties.Resources.EmailBinLogo;
			this.pbEmail.Location = new System.Drawing.Point(1, 12);
			this.pbEmail.Name = "pbEmail";
			this.pbEmail.Size = new System.Drawing.Size(72, 69);
			this.pbEmail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbEmail.TabIndex = 0;
			this.pbEmail.TabStop = false;
			// 
			// ckChangeEmailName
			// 
			this.ckChangeEmailName.AutoSize = true;
			this.ckChangeEmailName.BackColor = System.Drawing.Color.White;
			this.ckChangeEmailName.ForeColor = System.Drawing.Color.Black;
			this.ckChangeEmailName.Location = new System.Drawing.Point(79, 22);
			this.ckChangeEmailName.Name = "ckChangeEmailName";
			this.ckChangeEmailName.Size = new System.Drawing.Size(315, 20);
			this.ckChangeEmailName.TabIndex = 5;
			this.ckChangeEmailName.Text = "Re-Name this File Before Attaching it to the Email";
			this.ckChangeEmailName.UseVisualStyleBackColor = false;
			this.ckChangeEmailName.CheckedChanged += new System.EventHandler(this.ckChangeEmailName_CheckedChanged);
			// 
			// textEditEmailName
			// 
			this.textEditEmailName.Enabled = false;
			this.textEditEmailName.Location = new System.Drawing.Point(79, 48);
			this.textEditEmailName.Name = "textEditEmailName";
			this.textEditEmailName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditEmailName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textEditEmailName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditEmailName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditEmailName.Properties.Appearance.Options.UseFont = true;
			this.textEditEmailName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditEmailName.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.textEditEmailName.Properties.AppearanceDisabled.Options.UseFont = true;
			this.textEditEmailName.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.textEditEmailName.Properties.AppearanceFocused.Options.UseFont = true;
			this.textEditEmailName.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.textEditEmailName.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.textEditEmailName.Properties.NullText = "Type new file name here...";
			this.textEditEmailName.Size = new System.Drawing.Size(350, 22);
			this.textEditEmailName.TabIndex = 6;
			// 
			// buttonXEmail
			// 
			this.buttonXEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmail.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXEmail.Location = new System.Drawing.Point(60, 96);
			this.buttonXEmail.Name = "buttonXEmail";
			this.buttonXEmail.Size = new System.Drawing.Size(149, 43);
			this.buttonXEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmail.TabIndex = 7;
			this.buttonXEmail.Text = "Create Email Now";
			this.buttonXEmail.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(232, 96);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(149, 43);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 8;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// FormEmailLink
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(441, 151);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXEmail);
			this.Controls.Add(this.textEditEmailName);
			this.Controls.Add(this.ckChangeEmailName);
			this.Controls.Add(this.pbEmail);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailLink";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Email File - {0}";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormEmailPresentation_FormClosed);
			this.Load += new System.EventHandler(this.FormEmailPresentation_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbEmail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEmail;
        private System.Windows.Forms.CheckBox ckChangeEmailName;
        private DevExpress.XtraEditors.TextEdit textEditEmailName;
        private DevComponents.DotNetBar.ButtonX buttonXEmail;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
    }
}