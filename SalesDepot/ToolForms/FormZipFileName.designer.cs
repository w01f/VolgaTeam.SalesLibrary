namespace SalesDepot.ToolForms
{
    partial class FormZipFileName
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
            this.laLogo = new System.Windows.Forms.Label();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.textEditFileName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::SalesDepot.Properties.Resources.MicrosoftOfficeOutlook;
            this.pbLogo.Location = new System.Drawing.Point(12, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(72, 65);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // laLogo
            // 
            this.laLogo.Location = new System.Drawing.Point(90, 12);
            this.laLogo.Name = "laLogo";
            this.laLogo.Size = new System.Drawing.Size(282, 29);
            this.laLogo.TabIndex = 1;
            this.laLogo.Text = "File Name:";
            this.laLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(65, 94);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(117, 30);
            this.buttonXOK.TabIndex = 1;
            this.buttonXOK.Text = "Generate Email";
            this.buttonXOK.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(202, 94);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(117, 30);
            this.buttonXCancel.TabIndex = 2;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // textEditFileName
            // 
            this.textEditFileName.Location = new System.Drawing.Point(90, 55);
            this.textEditFileName.Name = "textEditFileName";
            this.textEditFileName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditFileName.Properties.Appearance.Options.UseFont = true;
            this.textEditFileName.Properties.NullText = "Type here";
            this.textEditFileName.Size = new System.Drawing.Size(282, 22);
            this.textEditFileName.TabIndex = 0;
            // 
            // FormZipFileName
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(384, 137);
            this.Controls.Add(this.textEditFileName);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.laLogo);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormZipFileName";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Name the Zip File Before you Send it:";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laLogo;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.TextEdit textEditFileName;
    }
}