namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	partial class LinkSettingsControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonXViewer = new DevComponents.DotNetBar.ButtonX();
			this.buttonXMenu = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLaunch = new DevComponents.DotNetBar.ButtonX();
			this.pnBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// pnBody
			// 
			this.pnBody.Controls.Add(this.buttonXLaunch);
			this.pnBody.Controls.Add(this.buttonXMenu);
			this.pnBody.Controls.Add(this.buttonXViewer);
			this.pnBody.Size = new System.Drawing.Size(312, 89);
			this.pnBody.Controls.SetChildIndex(this.pbLogo, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXViewer, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXMenu, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXLaunch, 0);
			// 
			// laTitle
			// 
			this.laTitle.Size = new System.Drawing.Size(312, 31);
			// 
			// buttonXViewer
			// 
			this.buttonXViewer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXViewer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXViewer.Location = new System.Drawing.Point(91, 27);
			this.buttonXViewer.Name = "buttonXViewer";
			this.buttonXViewer.Size = new System.Drawing.Size(59, 34);
			this.buttonXViewer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXViewer.TabIndex = 1;
			this.buttonXViewer.Text = "Viewer";
			this.buttonXViewer.CheckedChanged += new System.EventHandler(this.ButtonX_CheckedChanged);
			this.buttonXViewer.Click += new System.EventHandler(this.Button_Click);
			// 
			// buttonXMenu
			// 
			this.buttonXMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXMenu.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXMenu.Location = new System.Drawing.Point(165, 27);
			this.buttonXMenu.Name = "buttonXMenu";
			this.buttonXMenu.Size = new System.Drawing.Size(59, 34);
			this.buttonXMenu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXMenu.TabIndex = 2;
			this.buttonXMenu.Text = "Menu";
			this.buttonXMenu.CheckedChanged += new System.EventHandler(this.ButtonX_CheckedChanged);
			this.buttonXMenu.Click += new System.EventHandler(this.Button_Click);
			// 
			// buttonXLaunch
			// 
			this.buttonXLaunch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLaunch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLaunch.Location = new System.Drawing.Point(239, 27);
			this.buttonXLaunch.Name = "buttonXLaunch";
			this.buttonXLaunch.Size = new System.Drawing.Size(59, 34);
			this.buttonXLaunch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLaunch.TabIndex = 3;
			this.buttonXLaunch.Text = "Launch";
			this.buttonXLaunch.CheckedChanged += new System.EventHandler(this.ButtonX_CheckedChanged);
			this.buttonXLaunch.Click += new System.EventHandler(this.Button_Click);
			// 
			// LinkSettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "LinkSettingsControl";
			this.Size = new System.Drawing.Size(334, 173);
			this.pnBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevComponents.DotNetBar.ButtonX buttonXViewer;
		protected DevComponents.DotNetBar.ButtonX buttonXMenu;
		protected DevComponents.DotNetBar.ButtonX buttonXLaunch;
	}
}
