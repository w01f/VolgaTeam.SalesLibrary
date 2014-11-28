namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	partial class VideoEmbeddedLinkOptions
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
			this.ckForcePreview = new System.Windows.Forms.CheckBox();
			this.pnAdminTools.SuspendLayout();
			this.SuspendLayout();
			// 
			// ckForcePreview
			// 
			this.ckForcePreview.AutoSize = true;
			this.ckForcePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForcePreview.ForeColor = System.Drawing.Color.Black;
			this.ckForcePreview.Location = new System.Drawing.Point(5, 5);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(311, 20);
			this.ckForcePreview.TabIndex = 24;
			this.ckForcePreview.Text = "Immediately Play this Video in Cloud Library";
			this.ckForcePreview.UseVisualStyleBackColor = true;
			// 
			// VideoEmbeddedLinkOptions
			// 
			this.Controls.Add(this.ckForcePreview);
			this.Name = "VideoEmbeddedLinkOptions";
			this.Controls.SetChildIndex(this.pnAdminTools, 0);
			this.Controls.SetChildIndex(this.ckForcePreview, 0);
			this.pnAdminTools.ResumeLayout(false);
			this.pnAdminTools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.CheckBox ckForcePreview;
	}
}
