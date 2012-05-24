namespace FileManager.ToolForms.WallBin
{
    partial class FormIncorrectLinksNotification
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
            this.laTitle = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btFix = new System.Windows.Forms.Button();
            this.btIgnore = new System.Windows.Forms.Button();
            this.laQuestion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // laWarning
            // 
            this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.ForeColor = System.Drawing.Color.White;
            this.laTitle.Location = new System.Drawing.Point(109, 9);
            this.laTitle.Name = "laWarning";
            this.laTitle.Size = new System.Drawing.Size(191, 95);
            this.laTitle.TabIndex = 0;
            this.laTitle.Text = "You have {0} Links in your Sales Library!";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::FileManager.Properties.Resources.DeadLinks;
            this.pbLogo.Location = new System.Drawing.Point(12, 9);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(91, 95);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 1;
            this.pbLogo.TabStop = false;
            // 
            // btFix
            // 
            this.btFix.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btFix.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btFix.Location = new System.Drawing.Point(12, 193);
            this.btFix.Name = "btFix";
            this.btFix.Size = new System.Drawing.Size(285, 31);
            this.btFix.TabIndex = 2;
            this.btFix.Text = "Fix these NOW!";
            this.btFix.UseVisualStyleBackColor = true;
            // 
            // btIgnore
            // 
            this.btIgnore.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btIgnore.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btIgnore.Location = new System.Drawing.Point(12, 147);
            this.btIgnore.Name = "btIgnore";
            this.btIgnore.Size = new System.Drawing.Size(285, 31);
            this.btIgnore.TabIndex = 3;
            this.btIgnore.Text = "Ignore these and deal with them later...";
            this.btIgnore.UseVisualStyleBackColor = true;
            // 
            // laQuestion
            // 
            this.laQuestion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laQuestion.ForeColor = System.Drawing.Color.White;
            this.laQuestion.Location = new System.Drawing.Point(12, 118);
            this.laQuestion.Name = "laQuestion";
            this.laQuestion.Size = new System.Drawing.Size(288, 26);
            this.laQuestion.TabIndex = 4;
            this.laQuestion.Text = "What do you want to do?";
            // 
            // FormIncorrectLinksNotification
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(309, 239);
            this.Controls.Add(this.laQuestion);
            this.Controls.Add(this.btIgnore);
            this.Controls.Add(this.btFix);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.laTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIncorrectLinksNotification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{0} LINKS NOTIFICATION";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btFix;
        private System.Windows.Forms.Button btIgnore;
        private System.Windows.Forms.Label laQuestion;
        public System.Windows.Forms.Label laTitle;
        public System.Windows.Forms.PictureBox pbLogo;
    }
}