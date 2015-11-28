namespace SalesLibraries.CommonGUI.Floater
{
    partial class FormFloater
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
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonXHide = new DevComponents.DotNetBar.ButtonX();
			this.buttonXBack = new DevComponents.DotNetBar.ButtonX();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnBorder = new System.Windows.Forms.Panel();
			this.labelCaption = new System.Windows.Forms.Label();
			this.pnMain.SuspendLayout();
			this.pnBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonXHide
			// 
			this.buttonXHide.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXHide.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXHide.FocusCuesEnabled = false;
			this.buttonXHide.Image = global::SalesLibraries.CommonGUI.Properties.Resources.FloaterHide;
			this.buttonXHide.Location = new System.Drawing.Point(267, 5);
			this.buttonXHide.Name = "buttonXHide";
			this.buttonXHide.Size = new System.Drawing.Size(68, 106);
			this.buttonXHide.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXHide, new DevComponents.DotNetBar.SuperTooltipInfo("Hide", "", "Hide Application", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXHide.TabIndex = 1;
			this.buttonXHide.Click += new System.EventHandler(this.buttonItemHide_Click);
			// 
			// buttonXBack
			// 
			this.buttonXBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXBack.FocusCuesEnabled = false;
			this.buttonXBack.Location = new System.Drawing.Point(5, 5);
			this.buttonXBack.Name = "buttonXBack";
			this.buttonXBack.Size = new System.Drawing.Size(255, 106);
			this.buttonXBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXBack.TabIndex = 0;
			this.buttonXBack.Click += new System.EventHandler(this.buttonItemBack_Click);
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.White;
			this.pnMain.Controls.Add(this.buttonXBack);
			this.pnMain.Controls.Add(this.buttonXHide);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(1, 24);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(341, 117);
			this.pnMain.TabIndex = 2;
			// 
			// pnBorder
			// 
			this.pnBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
			this.pnBorder.Controls.Add(this.pnMain);
			this.pnBorder.Controls.Add(this.labelCaption);
			this.pnBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBorder.Location = new System.Drawing.Point(0, 0);
			this.pnBorder.Name = "pnBorder";
			this.pnBorder.Padding = new System.Windows.Forms.Padding(1);
			this.pnBorder.Size = new System.Drawing.Size(343, 142);
			this.pnBorder.TabIndex = 3;
			// 
			// labelCaption
			// 
			this.labelCaption.BackColor = System.Drawing.Color.White;
			this.labelCaption.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelCaption.ForeColor = System.Drawing.Color.DimGray;
			this.labelCaption.Location = new System.Drawing.Point(1, 1);
			this.labelCaption.Name = "labelCaption";
			this.labelCaption.Size = new System.Drawing.Size(341, 23);
			this.labelCaption.TabIndex = 3;
			this.labelCaption.Text = "labelCaption";
			this.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelCaption_MouseDown);
			// 
			// FormFloater
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(343, 142);
			this.ControlBox = false;
			this.Controls.Add(this.pnBorder);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFloater";
			this.Opacity = 0.85D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "adSALESapps.com";
			this.TopMost = true;
			this.pnMain.ResumeLayout(false);
			this.pnBorder.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		public DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevComponents.DotNetBar.ButtonX buttonXBack;
		private DevComponents.DotNetBar.ButtonX buttonXHide;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnBorder;
		private System.Windows.Forms.Label labelCaption;

    }
}