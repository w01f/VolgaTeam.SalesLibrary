namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	partial class BaseSettingsControl
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
			this.pnBorders = new System.Windows.Forms.Panel();
			this.pnBody = new System.Windows.Forms.Panel();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.pnSeparator = new System.Windows.Forms.Panel();
			this.laTitle = new System.Windows.Forms.Label();
			this.pnBorders.SuspendLayout();
			this.pnBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// pnBorders
			// 
			this.pnBorders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
			this.pnBorders.Controls.Add(this.pnBody);
			this.pnBorders.Controls.Add(this.pnSeparator);
			this.pnBorders.Controls.Add(this.laTitle);
			this.pnBorders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBorders.Location = new System.Drawing.Point(10, 25);
			this.pnBorders.Name = "pnBorders";
			this.pnBorders.Padding = new System.Windows.Forms.Padding(1);
			this.pnBorders.Size = new System.Drawing.Size(314, 123);
			this.pnBorders.TabIndex = 0;
			// 
			// pnBody
			// 
			this.pnBody.BackColor = System.Drawing.Color.White;
			this.pnBody.Controls.Add(this.pbLogo);
			this.pnBody.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBody.Location = new System.Drawing.Point(1, 33);
			this.pnBody.Name = "pnBody";
			this.pnBody.Size = new System.Drawing.Size(312, 89);
			this.pnBody.TabIndex = 0;
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::SalesLibraries.SalesDepot.Properties.Resources.SettingsPDF;
			this.pbLogo.Location = new System.Drawing.Point(11, 13);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(64, 64);
			this.pbLogo.TabIndex = 0;
			this.pbLogo.TabStop = false;
			// 
			// pnSeparator
			// 
			this.pnSeparator.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnSeparator.Location = new System.Drawing.Point(1, 32);
			this.pnSeparator.Name = "pnSeparator";
			this.pnSeparator.Size = new System.Drawing.Size(312, 1);
			this.pnSeparator.TabIndex = 2;
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(1, 1);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(312, 31);
			this.laTitle.TabIndex = 1;
			this.laTitle.Text = "Title";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BaseSettingsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pnBorders);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "BaseSettingsControl";
			this.Padding = new System.Windows.Forms.Padding(10, 25, 10, 25);
			this.Size = new System.Drawing.Size(334, 173);
			this.pnBorders.ResumeLayout(false);
			this.pnBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnBorders;
		private System.Windows.Forms.Panel pnSeparator;
		protected System.Windows.Forms.Panel pnBody;
		protected System.Windows.Forms.Label laTitle;
		protected System.Windows.Forms.PictureBox pbLogo;
	}
}
