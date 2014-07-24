namespace SalesDepot.PresentationClasses.Gallery
{
	abstract partial class GalleryControl
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
			this.circularProgressWebpage = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.SuspendLayout();
			// 
			// circularProgressWebpage
			// 
			this.circularProgressWebpage.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgressWebpage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressWebpage.Location = new System.Drawing.Point(3, 0);
			this.circularProgressWebpage.Name = "circularProgressWebpage";
			this.circularProgressWebpage.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressWebpage.Size = new System.Drawing.Size(62, 59);
			this.circularProgressWebpage.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressWebpage.TabIndex = 1;
			this.circularProgressWebpage.TabStop = false;
			// 
			// GalleryControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.circularProgressWebpage);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "GalleryControl";
			this.Size = new System.Drawing.Size(672, 463);
			this.Resize += new System.EventHandler(this.GalleryControl_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressWebpage;

	}
}
