namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries
{
    partial class FormInactiveLinksNotification
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
			this.laQuestion = new System.Windows.Forms.Label();
			this.buttonXIgnore = new DevComponents.DotNetBar.ButtonX();
			this.buttonXFix = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(109, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(191, 78);
			this.laTitle.TabIndex = 0;
			this.laTitle.Text = "You have INACTIVE Links in your Sales Library!";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.White;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.SettingsDeadLinks;
			this.pbLogo.Location = new System.Drawing.Point(12, 12);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(68, 75);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbLogo.TabIndex = 1;
			this.pbLogo.TabStop = false;
			// 
			// laQuestion
			// 
			this.laQuestion.BackColor = System.Drawing.Color.White;
			this.laQuestion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laQuestion.ForeColor = System.Drawing.Color.Black;
			this.laQuestion.Location = new System.Drawing.Point(9, 105);
			this.laQuestion.Name = "laQuestion";
			this.laQuestion.Size = new System.Drawing.Size(288, 26);
			this.laQuestion.TabIndex = 4;
			this.laQuestion.Text = "What do you want to do?";
			// 
			// buttonXIgnore
			// 
			this.buttonXIgnore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXIgnore.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXIgnore.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXIgnore.Location = new System.Drawing.Point(12, 147);
			this.buttonXIgnore.Name = "buttonXIgnore";
			this.buttonXIgnore.Size = new System.Drawing.Size(285, 31);
			this.buttonXIgnore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXIgnore.TabIndex = 5;
			this.buttonXIgnore.Text = "Ignore these and deal with them later...";
			// 
			// buttonXFix
			// 
			this.buttonXFix.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXFix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXFix.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXFix.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXFix.Location = new System.Drawing.Point(12, 193);
			this.buttonXFix.Name = "buttonXFix";
			this.buttonXFix.Size = new System.Drawing.Size(285, 31);
			this.buttonXFix.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXFix.TabIndex = 6;
			this.buttonXFix.Text = "Fix these NOW!";
			// 
			// FormInactiveLinksNotification
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(309, 239);
			this.Controls.Add(this.laQuestion);
			this.Controls.Add(this.pbLogo);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.buttonXIgnore);
			this.Controls.Add(this.buttonXFix);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInactiveLinksNotification";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "INACTIVE LINKS NOTIFICATION";
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Label laQuestion;
        public System.Windows.Forms.Label laTitle;
        public System.Windows.Forms.PictureBox pbLogo;
		private DevComponents.DotNetBar.ButtonX buttonXIgnore;
		private DevComponents.DotNetBar.ButtonX buttonXFix;
    }
}