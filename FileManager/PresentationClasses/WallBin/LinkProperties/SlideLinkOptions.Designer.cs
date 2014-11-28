namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	partial class SlideLinkOptions
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
			// ckDoNotGenerateText
			// 
			this.ckDoNotGenerateText.AutoSize = true;
			this.ckDoNotGenerateText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGenerateText.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGenerateText.Location = new System.Drawing.Point(7, 429);
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
			this.ckDoNotGeneratePreview.Location = new System.Drawing.Point(7, 398);
			this.ckDoNotGeneratePreview.Name = "ckDoNotGeneratePreview";
			this.ckDoNotGeneratePreview.Size = new System.Drawing.Size(579, 20);
			this.ckDoNotGeneratePreview.TabIndex = 25;
			this.ckDoNotGeneratePreview.Text = "Do NOT Generate PNG and JPEG preview images (Always select this for Nielsen Books" +
    ")";
			this.ckDoNotGeneratePreview.UseVisualStyleBackColor = true;
			// 
			// SlideLinkOptions
			// 
			this.Controls.Add(this.ckDoNotGeneratePreview);
			this.Controls.Add(this.ckDoNotGenerateText);
			this.Name = "SlideLinkOptions";
			this.Controls.SetChildIndex(this.laLinkHoverNote, 0);
			this.Controls.SetChildIndex(this.textEditLinkHoverNote, 0);
			this.Controls.SetChildIndex(this.pnAdminTools, 0);
			this.Controls.SetChildIndex(this.ckDoNotGenerateText, 0);
			this.Controls.SetChildIndex(this.ckDoNotGeneratePreview, 0);
			this.pnAdminTools.ResumeLayout(false);
			this.pnAdminTools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLinkSpecialColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLinkSpecialFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkHoverNote.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.CheckBox ckDoNotGenerateText;
		public System.Windows.Forms.CheckBox ckDoNotGeneratePreview;
	}
}
