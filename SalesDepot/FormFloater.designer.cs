namespace SalesDepot
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
			this.ribbonBarDashboard = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemDashboard = new DevComponents.DotNetBar.ButtonItem();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// ribbonBarDashboard
			// 
			this.ribbonBarDashboard.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarDashboard.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarDashboard.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarDashboard.ContainerControlProcessDialogKey = true;
			this.ribbonBarDashboard.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarDashboard.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarDashboard.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemDashboard});
			this.ribbonBarDashboard.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarDashboard.Location = new System.Drawing.Point(0, 0);
			this.ribbonBarDashboard.Name = "ribbonBarDashboard";
			this.ribbonBarDashboard.Size = new System.Drawing.Size(223, 107);
			this.ribbonBarDashboard.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarDashboard.TabIndex = 1;
			this.ribbonBarDashboard.Text = "Dashboard";
			// 
			// 
			// 
			this.ribbonBarDashboard.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarDashboard.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarDashboard.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// buttonItemDashboard
			// 
			this.buttonItemDashboard.Name = "buttonItemDashboard";
			this.buttonItemDashboard.SubItemsExpandWidth = 14;
			this.buttonItemDashboard.Text = "buttonItem1";
			this.buttonItemDashboard.Click += new System.EventHandler(this.buttonItemDefaultStar_Click);
			// 
			// FormFloater
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(220, 107);
			this.Controls.Add(this.ribbonBarDashboard);
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
        private DevComponents.DotNetBar.RibbonBar ribbonBarDashboard;
		private DevComponents.DotNetBar.ButtonItem buttonItemDashboard;

    }
}