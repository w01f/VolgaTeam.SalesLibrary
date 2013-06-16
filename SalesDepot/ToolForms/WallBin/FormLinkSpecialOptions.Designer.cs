namespace SalesDepot.ToolForms.WallBin
{
    partial class FormLinkSpecialOptions
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
			this.buttonXQuickSiteAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXQuickSiteEmail = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXQuickSiteAdd
			// 
			this.buttonXQuickSiteAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXQuickSiteAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXQuickSiteAdd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXQuickSiteAdd.Image = global::SalesDepot.Properties.Resources.QuickSiteAddLink;
			this.buttonXQuickSiteAdd.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXQuickSiteAdd.Location = new System.Drawing.Point(12, 77);
			this.buttonXQuickSiteAdd.Name = "buttonXQuickSiteAdd";
			this.buttonXQuickSiteAdd.Size = new System.Drawing.Size(290, 51);
			this.buttonXQuickSiteAdd.TabIndex = 6;
			this.buttonXQuickSiteAdd.Text = "   Add to quickSITE";
			this.buttonXQuickSiteAdd.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXQuickSiteAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXQuickSiteAdd.Click += new System.EventHandler(this.buttonXQuickSiteAdd_Click);
			// 
			// buttonXQuickSiteEmail
			// 
			this.buttonXQuickSiteEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXQuickSiteEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXQuickSiteEmail.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXQuickSiteEmail.Image = global::SalesDepot.Properties.Resources.QuickSiteEmailLink;
			this.buttonXQuickSiteEmail.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXQuickSiteEmail.Location = new System.Drawing.Point(12, 12);
			this.buttonXQuickSiteEmail.Name = "buttonXQuickSiteEmail";
			this.buttonXQuickSiteEmail.Size = new System.Drawing.Size(290, 51);
			this.buttonXQuickSiteEmail.TabIndex = 5;
			this.buttonXQuickSiteEmail.Text = "   Email as Web Link";
			this.buttonXQuickSiteEmail.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXQuickSiteEmail.TextColor = System.Drawing.Color.Black;
			this.buttonXQuickSiteEmail.Click += new System.EventHandler(this.buttonXQuickSiteEmail_Click);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(12, 142);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(290, 51);
			this.buttonXClose.TabIndex = 4;
			this.buttonXClose.Text = "   Close this Window";
			this.buttonXClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
			// 
			// FormLinkSpecialOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(315, 209);
			this.Controls.Add(this.buttonXQuickSiteAdd);
			this.Controls.Add(this.buttonXQuickSiteEmail);
			this.Controls.Add(this.buttonXClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLinkSpecialOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File Options - {0}";
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevComponents.DotNetBar.ButtonX buttonXQuickSiteEmail;
		private DevComponents.DotNetBar.ButtonX buttonXQuickSiteAdd;

    }
}