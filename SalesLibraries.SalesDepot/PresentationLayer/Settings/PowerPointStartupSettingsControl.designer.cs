namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	partial class PowerPointStartupSettingsControl
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
			this.buttonXDisabled = new DevComponents.DotNetBar.ButtonX();
			this.buttonXEnabled = new DevComponents.DotNetBar.ButtonX();
			this.pnBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// pnBody
			// 
			this.pnBody.Controls.Add(this.buttonXDisabled);
			this.pnBody.Controls.Add(this.buttonXEnabled);
			this.pnBody.Controls.SetChildIndex(this.pbLogo, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXEnabled, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXDisabled, 0);
			// 
			// laTitle
			// 
			this.laTitle.Text = "Startup PowerPoint Warning";
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::SalesLibraries.SalesDepot.Properties.Resources.SettingsPowerPoint;
			// 
			// buttonXDisabled
			// 
			this.buttonXDisabled.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDisabled.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDisabled.Location = new System.Drawing.Point(203, 27);
			this.buttonXDisabled.Name = "buttonXDisabled";
			this.buttonXDisabled.Size = new System.Drawing.Size(95, 34);
			this.buttonXDisabled.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDisabled.TabIndex = 5;
			this.buttonXDisabled.Text = "Disabled";
			this.buttonXDisabled.CheckedChanged += new System.EventHandler(this.OnToggleButtonCheckedChanged);
			this.buttonXDisabled.Click += new System.EventHandler(this.OnToggleButtonClick);
			// 
			// buttonXEnabled
			// 
			this.buttonXEnabled.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEnabled.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEnabled.Location = new System.Drawing.Point(91, 27);
			this.buttonXEnabled.Name = "buttonXEnabled";
			this.buttonXEnabled.Size = new System.Drawing.Size(95, 34);
			this.buttonXEnabled.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEnabled.TabIndex = 4;
			this.buttonXEnabled.Text = "Enabled";
			this.buttonXEnabled.CheckedChanged += new System.EventHandler(this.OnToggleButtonCheckedChanged);
			this.buttonXEnabled.Click += new System.EventHandler(this.OnToggleButtonClick);
			// 
			// PowerPointStartupSettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "PowerPointStartupSettingsControl";
			this.pnBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevComponents.DotNetBar.ButtonX buttonXDisabled;
		protected DevComponents.DotNetBar.ButtonX buttonXEnabled;

	}
}
