namespace SalesDepot.ToolForms.WallBin
{
    partial class FormVideoOutput
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
            this.buttonXBackToForm = new DevComponents.DotNetBar.ButtonX();
            this.buttonXStayHere = new DevComponents.DotNetBar.ButtonX();
            this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.labelControlDescription = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXBackToForm
            // 
            this.buttonXBackToForm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXBackToForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXBackToForm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXBackToForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXBackToForm.Location = new System.Drawing.Point(442, 47);
            this.buttonXBackToForm.Name = "buttonXBackToForm";
            this.buttonXBackToForm.Size = new System.Drawing.Size(155, 32);
            this.buttonXBackToForm.TabIndex = 9;
            this.buttonXBackToForm.Text = "Back To {0}";
            this.buttonXBackToForm.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXStayHere
            // 
            this.buttonXStayHere.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXStayHere.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXStayHere.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXStayHere.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXStayHere.Location = new System.Drawing.Point(442, 2);
            this.buttonXStayHere.Name = "buttonXStayHere";
            this.buttonXStayHere.Size = new System.Drawing.Size(155, 32);
            this.buttonXStayHere.TabIndex = 10;
            this.buttonXStayHere.Text = "Stay Here";
            this.buttonXStayHere.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXClose
            // 
            this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClose.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.buttonXClose.Location = new System.Drawing.Point(442, 95);
            this.buttonXClose.Name = "buttonXClose";
            this.buttonXClose.Size = new System.Drawing.Size(155, 32);
            this.buttonXClose.TabIndex = 11;
            this.buttonXClose.Text = "Close Library";
            this.buttonXClose.TextColor = System.Drawing.Color.Black;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::SalesDepot.Properties.Resources.Video;
            this.pbLogo.Location = new System.Drawing.Point(3, 2);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(103, 125);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // labelControlDescription
            // 
            this.labelControlDescription.AllowHtmlString = true;
            this.labelControlDescription.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlDescription.Location = new System.Drawing.Point(112, 2);
            this.labelControlDescription.Name = "labelControlDescription";
            this.labelControlDescription.Size = new System.Drawing.Size(325, 125);
            this.labelControlDescription.TabIndex = 13;
            this.labelControlDescription.Text = "The Video file is now on your PowerPoint slide.\r\n<b>A Copy of the Video file must" +
    " ALWAYS be in the SAME FOLDER with your PowerPoint File…</b>";
            // 
            // FormVideoOutput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(605, 136);
            this.Controls.Add(this.labelControlDescription);
            this.Controls.Add(this.buttonXClose);
            this.Controls.Add(this.buttonXStayHere);
            this.Controls.Add(this.buttonXBackToForm);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVideoOutput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Inserted Successfully!";
            this.Load += new System.EventHandler(this.FormSlideOutput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private DevComponents.DotNetBar.ButtonX buttonXBackToForm;
        private DevComponents.DotNetBar.ButtonX buttonXStayHere;
        private DevComponents.DotNetBar.ButtonX buttonXClose;
        private DevExpress.XtraEditors.LabelControl labelControlDescription;
    }
}