namespace SalesDepot.ToolForms.WallBin
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
			this.btSave = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.gbPDFConvert = new System.Windows.Forms.GroupBox();
			this.rbSlide = new System.Windows.Forms.RadioButton();
			this.rbFile = new System.Windows.Forms.RadioButton();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.gbPDFConvert.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// btSave
			// 
			this.btSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btSave.Location = new System.Drawing.Point(89, 96);
			this.btSave.Name = "btSave";
			this.btSave.Size = new System.Drawing.Size(83, 43);
			this.btSave.TabIndex = 0;
			this.btSave.Text = "Save";
			this.btSave.UseVisualStyleBackColor = true;
			// 
			// btCancel
			// 
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(190, 96);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(83, 43);
			this.btCancel.TabIndex = 1;
			this.btCancel.Text = "Cancel";
			this.btCancel.UseVisualStyleBackColor = true;
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
			this.rbSlide.Size = new System.Drawing.Size(191, 17);
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
			this.rbFile.Size = new System.Drawing.Size(200, 17);
			this.rbFile.TabIndex = 0;
			this.rbFile.TabStop = true;
			this.rbFile.Text = "Convert entire PowerPoint file to PDF";
			this.rbFile.UseVisualStyleBackColor = true;
			// 
			// picLogo
			// 
			this.picLogo.Image = global::SalesDepot.Properties.Resources.QuickViewPdf;
			this.picLogo.Location = new System.Drawing.Point(12, 12);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(76, 75);
			this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picLogo.TabIndex = 3;
			this.picLogo.TabStop = false;
			// 
			// FormSaveAsPDF
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(362, 151);
			this.Controls.Add(this.picLogo);
			this.Controls.Add(this.gbPDFConvert);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btSave);
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

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox gbPDFConvert;
        private System.Windows.Forms.RadioButton rbSlide;
        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.PictureBox picLogo;
    }
}