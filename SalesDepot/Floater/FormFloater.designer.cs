namespace SalesDepot.Floater
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
			this.components = new System.ComponentModel.Container();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.ribbonBarBack = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemBack = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHide = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHide = new DevComponents.DotNetBar.ButtonItem();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// ribbonBarBack
			// 
			this.ribbonBarBack.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarBack.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarBack.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBack.ContainerControlProcessDialogKey = true;
			this.ribbonBarBack.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarBack.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarBack.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemBack});
			this.ribbonBarBack.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarBack.Location = new System.Drawing.Point(0, 0);
			this.ribbonBarBack.Name = "ribbonBarBack";
			this.ribbonBarBack.Size = new System.Drawing.Size(223, 107);
			this.ribbonBarBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarBack.TabIndex = 1;
			this.ribbonBarBack.Text = "Back";
			// 
			// 
			// 
			this.ribbonBarBack.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarBack.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBack.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// buttonItemBack
			// 
			this.buttonItemBack.Name = "buttonItemBack";
			this.buttonItemBack.SubItemsExpandWidth = 14;
			this.buttonItemBack.Text = "buttonItem1";
			this.buttonItemBack.Click += new System.EventHandler(this.buttonItemBack_Click);
			// 
			// ribbonBarHide
			// 
			this.ribbonBarHide.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHide.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHide.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHide.ContainerControlProcessDialogKey = true;
			this.ribbonBarHide.Dock = System.Windows.Forms.DockStyle.Right;
			this.ribbonBarHide.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarHide.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHide});
			this.ribbonBarHide.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHide.Location = new System.Drawing.Point(223, 0);
			this.ribbonBarHide.Name = "ribbonBarHide";
			this.ribbonBarHide.Size = new System.Drawing.Size(91, 107);
			this.ribbonBarHide.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHide.TabIndex = 2;
			this.ribbonBarHide.Text = "Hide";
			// 
			// 
			// 
			this.ribbonBarHide.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHide.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHide.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// buttonItemHide
			// 
			this.buttonItemHide.Image = global::SalesDepot.Properties.Resources.FloaterHide;
			this.buttonItemHide.Name = "buttonItemHide";
			this.buttonItemHide.SubItemsExpandWidth = 14;
			this.buttonItemHide.Text = "buttonItem1";
			this.buttonItemHide.Click += new System.EventHandler(this.buttonItemHide_Click);
			// 
			// FormFloater
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(314, 107);
			this.Controls.Add(this.ribbonBarHide);
			this.Controls.Add(this.ribbonBarBack);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormFloater";
			this.Opacity = 0.85D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "FormFloater";
			this.TopMost = true;
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager;
        private DevComponents.DotNetBar.RibbonBar ribbonBarBack;
		private DevComponents.DotNetBar.ButtonItem buttonItemBack;
        private DevComponents.DotNetBar.RibbonBar ribbonBarHide;
        private DevComponents.DotNetBar.ButtonItem buttonItemHide;

    }
}