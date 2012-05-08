namespace SalesDepot.ToolForms
{
    partial class FormEmailPresentation
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
            this.rbAllSlides = new System.Windows.Forms.RadioButton();
            this.rbActiveSlide = new System.Windows.Forms.RadioButton();
            this.ckConvertToPDF = new System.Windows.Forms.CheckBox();
            this.pbChangeName = new System.Windows.Forms.PictureBox();
            this.pbEmail = new System.Windows.Forms.PictureBox();
            this.ckChangeEmailName = new System.Windows.Forms.CheckBox();
            this.textEditEmailName = new DevExpress.XtraEditors.TextEdit();
            this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.pbChangeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditEmailName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rbAllSlides
            // 
            this.rbAllSlides.AutoSize = true;
            this.rbAllSlides.Checked = true;
            this.rbAllSlides.Location = new System.Drawing.Point(79, 22);
            this.rbAllSlides.Name = "rbAllSlides";
            this.rbAllSlides.Size = new System.Drawing.Size(176, 20);
            this.rbAllSlides.TabIndex = 1;
            this.rbAllSlides.TabStop = true;
            this.rbAllSlides.Text = "Email All Slides in this file";
            this.rbAllSlides.UseVisualStyleBackColor = true;
            // 
            // rbActiveSlide
            // 
            this.rbActiveSlide.AutoSize = true;
            this.rbActiveSlide.ForeColor = System.Drawing.Color.Black;
            this.rbActiveSlide.Location = new System.Drawing.Point(79, 48);
            this.rbActiveSlide.Name = "rbActiveSlide";
            this.rbActiveSlide.Size = new System.Drawing.Size(256, 20);
            this.rbActiveSlide.TabIndex = 2;
            this.rbActiveSlide.Text = "Email just the Active Slide in the Viewer";
            this.rbActiveSlide.UseVisualStyleBackColor = false;
            // 
            // ckConvertToPDF
            // 
            this.ckConvertToPDF.AutoSize = true;
            this.ckConvertToPDF.Location = new System.Drawing.Point(79, 92);
            this.ckConvertToPDF.Name = "ckConvertToPDF";
            this.ckConvertToPDF.Size = new System.Drawing.Size(192, 20);
            this.ckConvertToPDF.TabIndex = 3;
            this.ckConvertToPDF.Text = "Convert to PDF then Email it";
            this.ckConvertToPDF.UseVisualStyleBackColor = true;
            // 
            // pbChangeName
            // 
            this.pbChangeName.Image = global::SalesDepot.Properties.Resources.ChangeEmailName;
            this.pbChangeName.Location = new System.Drawing.Point(1, 125);
            this.pbChangeName.Name = "pbChangeName";
            this.pbChangeName.Size = new System.Drawing.Size(72, 67);
            this.pbChangeName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbChangeName.TabIndex = 4;
            this.pbChangeName.TabStop = false;
            // 
            // pbEmail
            // 
            this.pbEmail.Image = global::SalesDepot.Properties.Resources.MicrosoftOfficeOutlook;
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
            this.ckChangeEmailName.Location = new System.Drawing.Point(79, 134);
            this.ckChangeEmailName.Name = "ckChangeEmailName";
            this.ckChangeEmailName.Size = new System.Drawing.Size(315, 20);
            this.ckChangeEmailName.TabIndex = 5;
            this.ckChangeEmailName.Text = "Re-Name this File Before Attaching it to the Email";
            this.ckChangeEmailName.UseVisualStyleBackColor = true;
            this.ckChangeEmailName.CheckedChanged += new System.EventHandler(this.ckChangeEmailName_CheckedChanged);
            // 
            // textEditEmailName
            // 
            this.textEditEmailName.Enabled = false;
            this.textEditEmailName.Location = new System.Drawing.Point(79, 160);
            this.textEditEmailName.Name = "textEditEmailName";
            this.textEditEmailName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditEmailName.Properties.Appearance.Options.UseFont = true;
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
            this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEmail.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXEmail.Location = new System.Drawing.Point(60, 233);
            this.buttonXEmail.Name = "buttonXEmail";
            this.buttonXEmail.Size = new System.Drawing.Size(149, 43);
            this.buttonXEmail.TabIndex = 7;
            this.buttonXEmail.Text = "Create Email Now";
            this.buttonXEmail.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(232, 233);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(149, 43);
            this.buttonXCancel.TabIndex = 8;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // FormEmailPresentation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(441, 288);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXEmail);
            this.Controls.Add(this.textEditEmailName);
            this.Controls.Add(this.ckChangeEmailName);
            this.Controls.Add(this.pbChangeName);
            this.Controls.Add(this.ckConvertToPDF);
            this.Controls.Add(this.rbActiveSlide);
            this.Controls.Add(this.rbAllSlides);
            this.Controls.Add(this.pbEmail);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEmailPresentation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Presentation - {0}";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormEmailPresentation_FormClosed);
            this.Load += new System.EventHandler(this.FormEmailPresentation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbChangeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditEmailName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEmail;
        private System.Windows.Forms.RadioButton rbAllSlides;
        private System.Windows.Forms.CheckBox ckConvertToPDF;
        private System.Windows.Forms.PictureBox pbChangeName;
        private System.Windows.Forms.CheckBox ckChangeEmailName;
        private DevExpress.XtraEditors.TextEdit textEditEmailName;
        private DevComponents.DotNetBar.ButtonX buttonXEmail;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        public System.Windows.Forms.RadioButton rbActiveSlide;
    }
}