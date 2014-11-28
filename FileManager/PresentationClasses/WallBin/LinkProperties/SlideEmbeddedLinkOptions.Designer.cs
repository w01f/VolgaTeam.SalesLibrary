﻿namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	partial class SlideEmbeddedLinkOptions
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
			this.ckDoNotGenerateText = new System.Windows.Forms.CheckBox();
			this.ckDoNotGeneratePreview = new System.Windows.Forms.CheckBox();
			this.pnAdminTools.SuspendLayout();
			this.SuspendLayout();
			// 
			// ckDoNotGenerateText
			// 
			this.ckDoNotGenerateText.AutoSize = true;
			this.ckDoNotGenerateText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGenerateText.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGenerateText.Location = new System.Drawing.Point(5, 31);
			this.ckDoNotGenerateText.Name = "ckDoNotGenerateText";
			this.ckDoNotGenerateText.Size = new System.Drawing.Size(452, 20);
			this.ckDoNotGenerateText.TabIndex = 24;
			this.ckDoNotGenerateText.Text = "Do NOT Create Full Data File (Always select this for Nielsen Books) ";
			this.ckDoNotGenerateText.UseVisualStyleBackColor = true;
			// 
			// ckDoNotGeneratePreview
			// 
			this.ckDoNotGeneratePreview.AutoSize = true;
			this.ckDoNotGeneratePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGeneratePreview.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGeneratePreview.Location = new System.Drawing.Point(5, 5);
			this.ckDoNotGeneratePreview.Name = "ckDoNotGeneratePreview";
			this.ckDoNotGeneratePreview.Size = new System.Drawing.Size(579, 20);
			this.ckDoNotGeneratePreview.TabIndex = 25;
			this.ckDoNotGeneratePreview.Text = "Do NOT Generate PNG and JPEG preview images (Always select this for Nielsen Books" +
    ")";
			this.ckDoNotGeneratePreview.UseVisualStyleBackColor = true;
			// 
			// SlideEmbeddedLinkOptions
			// 
			this.Controls.Add(this.ckDoNotGeneratePreview);
			this.Controls.Add(this.ckDoNotGenerateText);
			this.Name = "SlideEmbeddedLinkOptions";
			this.Controls.SetChildIndex(this.pnAdminTools, 0);
			this.Controls.SetChildIndex(this.ckDoNotGenerateText, 0);
			this.Controls.SetChildIndex(this.ckDoNotGeneratePreview, 0);
			this.pnAdminTools.ResumeLayout(false);
			this.pnAdminTools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.CheckBox ckDoNotGenerateText;
		public System.Windows.Forms.CheckBox ckDoNotGeneratePreview;
	}
}
