namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
    partial class FormSaveAsPDF
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
			this.gbPDFConvert = new System.Windows.Forms.GroupBox();
			this.rbSlide = new System.Windows.Forms.RadioButton();
			this.rbFile = new System.Windows.Forms.RadioButton();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.gbPDFConvert.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// gbPDFConvert
			// 
			this.gbPDFConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbPDFConvert.Controls.Add(this.rbSlide);
			this.gbPDFConvert.Controls.Add(this.rbFile);
			this.gbPDFConvert.Location = new System.Drawing.Point(94, 5);
			this.gbPDFConvert.Name = "gbPDFConvert";
			this.gbPDFConvert.Size = new System.Drawing.Size(256, 82);
			this.gbPDFConvert.TabIndex = 2;
			this.gbPDFConvert.TabStop = false;
			// 
			// rbSlide
			// 
			this.rbSlide.AutoSize = true;
			this.rbSlide.Location = new System.Drawing.Point(11, 52);
			this.rbSlide.Name = "rbSlide";
			this.rbSlide.Size = new System.Drawing.Size(229, 20);
			this.rbSlide.TabIndex = 1;
			this.rbSlide.Text = "Convert just the active slide to PDF";
			this.rbSlide.UseVisualStyleBackColor = true;
			// 
			// rbFile
			// 
			this.rbFile.AutoSize = true;
			this.rbFile.Checked = true;
			this.rbFile.Location = new System.Drawing.Point(11, 16);
			this.rbFile.Name = "rbFile";
			this.rbFile.Size = new System.Drawing.Size(240, 20);
			this.rbFile.TabIndex = 0;
			this.rbFile.TabStop = true;
			this.rbFile.Text = "Convert entire PowerPoint file to PDF";
			this.rbFile.UseVisualStyleBackColor = true;
			// 
			// picLogo
			// 
			this.picLogo.Image = global::SalesLibraries.SalesDepot.Properties.Resources.QuickViewPdf;
			this.picLogo.Location = new System.Drawing.Point(12, 12);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(76, 75);
			this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picLogo.TabIndex = 3;
			this.picLogo.TabStop = false;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(195, 103);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(98, 40);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 10;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(70, 103);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(98, 40);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 9;
			this.buttonXSave.Text = "Save";
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			// 
			// FormSaveAsPDF
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(362, 151);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.Controls.Add(this.picLogo);
			this.Controls.Add(this.gbPDFConvert);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSaveAsPDF";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Save As PDF";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSaveAsPDF_FormClosed);
			this.gbPDFConvert.ResumeLayout(false);
			this.gbPDFConvert.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox gbPDFConvert;
        private System.Windows.Forms.RadioButton rbSlide;
        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.PictureBox picLogo;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXSave;
    }
}