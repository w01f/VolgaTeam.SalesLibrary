namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	partial class EmailSettingsControl
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
			this.buttonXOpen = new DevComponents.DotNetBar.ButtonX();
			this.pnBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// pnBody
			// 
			this.pnBody.Controls.Add(this.buttonXOpen);
			this.pnBody.Controls.SetChildIndex(this.pbLogo, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXOpen, 0);
			// 
			// laTitle
			// 
			this.laTitle.Text = "Change your Email settings";
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::SalesLibraries.SalesDepot.Properties.Resources.SettingsEmail;
			// 
			// buttonXOpen
			// 
			this.buttonXOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpen.Location = new System.Drawing.Point(127, 27);
			this.buttonXOpen.Name = "buttonXOpen";
			this.buttonXOpen.Size = new System.Drawing.Size(134, 34);
			this.buttonXOpen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpen.TabIndex = 1;
			this.buttonXOpen.Text = "Edit Settings";
			this.buttonXOpen.Click += new System.EventHandler(this.buttonXOpen_Click);
			// 
			// EmailSettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "EmailSettingsControl";
			this.pnBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXOpen;
	}
}
