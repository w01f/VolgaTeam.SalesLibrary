namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	partial class QuickViewSettingsControl
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
			this.buttonXImages = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSlides = new DevComponents.DotNetBar.ButtonX();
			this.pnBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// pnBody
			// 
			this.pnBody.Controls.Add(this.buttonXSlides);
			this.pnBody.Controls.Add(this.buttonXImages);
			this.pnBody.Controls.SetChildIndex(this.pbLogo, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXImages, 0);
			this.pnBody.Controls.SetChildIndex(this.buttonXSlides, 0);
			// 
			// laTitle
			// 
			this.laTitle.Text = "How will QuickView Work?";
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::SalesLibraries.SalesDepot.Properties.Resources.SettingsQuickView;
			// 
			// buttonXImages
			// 
			this.buttonXImages.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXImages.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXImages.Location = new System.Drawing.Point(91, 27);
			this.buttonXImages.Name = "buttonXImages";
			this.buttonXImages.Size = new System.Drawing.Size(95, 34);
			this.buttonXImages.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXImages.TabIndex = 1;
			this.buttonXImages.Text = "Show Images";
			this.buttonXImages.CheckedChanged += new System.EventHandler(this.ButtonX_CheckedChanged);
			this.buttonXImages.Click += new System.EventHandler(this.Button_Click);
			// 
			// buttonXSlides
			// 
			this.buttonXSlides.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSlides.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSlides.Location = new System.Drawing.Point(203, 27);
			this.buttonXSlides.Name = "buttonXSlides";
			this.buttonXSlides.Size = new System.Drawing.Size(95, 34);
			this.buttonXSlides.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSlides.TabIndex = 3;
			this.buttonXSlides.Text = "Show Slides";
			this.buttonXSlides.CheckedChanged += new System.EventHandler(this.ButtonX_CheckedChanged);
			this.buttonXSlides.Click += new System.EventHandler(this.Button_Click);
			// 
			// QuickViewSettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Name = "QuickViewSettingsControl";
			this.pnBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevComponents.DotNetBar.ButtonX buttonXImages;
		protected DevComponents.DotNetBar.ButtonX buttonXSlides;
	}
}
