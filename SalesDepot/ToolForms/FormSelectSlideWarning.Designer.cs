namespace SalesDepot.ToolForms
{
    partial class FormSelectSlideWarning
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
            this.btOK = new System.Windows.Forms.Button();
            this.laText = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pbArrow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbArrow)).BeginInit();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btOK.Location = new System.Drawing.Point(359, 202);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 27);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // laText
            // 
            this.laText.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laText.ForeColor = System.Drawing.Color.Brown;
            this.laText.Location = new System.Drawing.Point(171, 12);
            this.laText.Name = "laText";
            this.laText.Size = new System.Drawing.Size(263, 124);
            this.laText.TabIndex = 2;
            this.laText.Text = "You need to fully select\r\nAn active slide in your\r\nPowerPoint presentation\r\nIf yo" +
                "u want \r\nto use Sales Depot!";
            this.laText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbImage
            // 
            this.pbImage.Image = global::SalesDepot.Properties.Resources.Slides;
            this.pbImage.Location = new System.Drawing.Point(12, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(153, 217);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // pbArrow
            // 
            this.pbArrow.BackColor = System.Drawing.Color.Transparent;
            this.pbArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbArrow.Image = global::SalesDepot.Properties.Resources.Arrow;
            this.pbArrow.Location = new System.Drawing.Point(145, 114);
            this.pbArrow.Name = "pbArrow";
            this.pbArrow.Size = new System.Drawing.Size(125, 114);
            this.pbArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbArrow.TabIndex = 3;
            this.pbArrow.TabStop = false;
            // 
            // FormSelectSlideWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 241);
            this.Controls.Add(this.laText);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.pbArrow);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectSlideWarning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Slide is Not Selected Properly";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbArrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label laText;
        private System.Windows.Forms.PictureBox pbArrow;
    }
}