namespace SalesDepot.ToolForms.WallBin
{
    partial class FormSlideOutput
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
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.laTitle = new System.Windows.Forms.Label();
            this.buttonXBackToForm = new DevComponents.DotNetBar.ButtonX();
            this.buttonXStayHere = new DevComponents.DotNetBar.ButtonX();
            this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
            this.laDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::SalesDepot.Properties.Resources.Output;
            this.pbLogo.Location = new System.Drawing.Point(3, 2);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(64, 67);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // laTitle
            // 
            this.laTitle.AutoSize = true;
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(73, 24);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(104, 29);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "Success!";
            // 
            // buttonXBackToForm
            // 
            this.buttonXBackToForm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXBackToForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXBackToForm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXBackToForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXBackToForm.Location = new System.Drawing.Point(184, 52);
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
            this.buttonXStayHere.Location = new System.Drawing.Point(184, 12);
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
            this.buttonXClose.Location = new System.Drawing.Point(184, 92);
            this.buttonXClose.Name = "buttonXClose";
            this.buttonXClose.Size = new System.Drawing.Size(155, 32);
            this.buttonXClose.TabIndex = 11;
            this.buttonXClose.Text = "Close Library";
            this.buttonXClose.TextColor = System.Drawing.Color.Black;
            // 
            // laDescription
            // 
            this.laDescription.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDescription.Location = new System.Drawing.Point(3, 73);
            this.laDescription.Name = "laDescription";
            this.laDescription.Size = new System.Drawing.Size(174, 51);
            this.laDescription.TabIndex = 12;
            this.laDescription.Text = "The slides have been added…";
            this.laDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormSlideOutput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(347, 136);
            this.Controls.Add(this.laDescription);
            this.Controls.Add(this.buttonXClose);
            this.Controls.Add(this.buttonXStayHere);
            this.Controls.Add(this.buttonXBackToForm);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSlideOutput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slide Output Success!";
            this.Load += new System.EventHandler(this.FormSlideOutput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXBackToForm;
        private DevComponents.DotNetBar.ButtonX buttonXStayHere;
        private DevComponents.DotNetBar.ButtonX buttonXClose;
        private System.Windows.Forms.Label laDescription;
    }
}