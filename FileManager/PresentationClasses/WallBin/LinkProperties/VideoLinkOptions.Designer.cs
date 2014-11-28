namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	partial class VideoLinkOptions
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
			((System.ComponentModel.ISupportInitialize)(this.colorEditLinkSpecialColor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLinkSpecialFont.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkHoverNote.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// colorEditLinkSpecialColor
			// 
			this.colorEditLinkSpecialColor.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.colorEditLinkSpecialColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditLinkSpecialColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditLinkSpecialColor.Properties.Appearance.Options.UseForeColor = true;
			// 
			// buttonEditLinkSpecialFont
			// 
			this.buttonEditLinkSpecialFont.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditLinkSpecialFont.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditLinkSpecialFont.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditLinkSpecialFont.Properties.Appearance.Options.UseForeColor = true;
			// 
			// textEditLinkHoverNote
			// 
			this.textEditLinkHoverNote.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditLinkHoverNote.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditLinkHoverNote.Properties.Appearance.Options.UseBackColor = true;
			this.textEditLinkHoverNote.Properties.Appearance.Options.UseForeColor = true;
			// 
			// ckForcePreview
			// 
			this.ckForcePreview.AutoSize = true;
			this.ckForcePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForcePreview.ForeColor = System.Drawing.Color.Black;
			this.ckForcePreview.Location = new System.Drawing.Point(7, 398);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(311, 20);
			this.ckForcePreview.TabIndex = 24;
			this.ckForcePreview.Text = "Immediately Play this Video in Cloud Library";
			this.ckForcePreview.UseVisualStyleBackColor = true;
			// 
			// VideoLinkOptions
			// 
			this.Controls.Add(this.ckForcePreview);
			this.Name = "VideoLinkOptions";
			this.Controls.SetChildIndex(this.laLinkHoverNote, 0);
			this.Controls.SetChildIndex(this.textEditLinkHoverNote, 0);
			this.Controls.SetChildIndex(this.pnAdminTools, 0);
			this.Controls.SetChildIndex(this.ckForcePreview, 0);
			this.pnAdminTools.ResumeLayout(false);
			this.pnAdminTools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLinkSpecialColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLinkSpecialFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkHoverNote.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.CheckBox ckForcePreview;
	}
}
