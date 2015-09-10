namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	sealed partial class LinkWebOptions
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
			this.components = new System.ComponentModel.Container();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.ckIsUrl365 = new System.Windows.Forms.CheckBox();
			this.ckForcePreview = new System.Windows.Forms.CheckBox();
			this.textEditLinkPath = new DevExpress.XtraEditors.TextEdit();
			this.laLinkPath = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkPath.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
			this.labelControlTitle.Location = new System.Drawing.Point(8, 12);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(427, 17);
			this.labelControlTitle.TabIndex = 24;
			this.labelControlTitle.Text = "You may want to apply these special, advanced settings to the link";
			// 
			// ckIsUrl365
			// 
			this.ckIsUrl365.AutoSize = true;
			this.ckIsUrl365.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckIsUrl365.ForeColor = System.Drawing.Color.Black;
			this.ckIsUrl365.Location = new System.Drawing.Point(8, 50);
			this.ckIsUrl365.Name = "ckIsUrl365";
			this.ckIsUrl365.Size = new System.Drawing.Size(244, 20);
			this.ckIsUrl365.TabIndex = 25;
			this.ckIsUrl365.Text = "This URL is an Office 365 URL Link";
			this.ckIsUrl365.UseVisualStyleBackColor = true;
			// 
			// ckForcePreview
			// 
			this.ckForcePreview.AutoSize = true;
			this.ckForcePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForcePreview.ForeColor = System.Drawing.Color.Black;
			this.ckForcePreview.Location = new System.Drawing.Point(8, 88);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(301, 20);
			this.ckForcePreview.TabIndex = 26;
			this.ckForcePreview.Text = "Immediately Launch this URL when clicked";
			this.ckForcePreview.UseVisualStyleBackColor = true;
			// 
			// textEditLinkPath
			// 
			this.textEditLinkPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditLinkPath.Location = new System.Drawing.Point(8, 183);
			this.textEditLinkPath.Name = "textEditLinkPath";
			this.textEditLinkPath.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditLinkPath.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditLinkPath.Properties.Appearance.Options.UseBackColor = true;
			this.textEditLinkPath.Properties.Appearance.Options.UseForeColor = true;
			this.textEditLinkPath.Size = new System.Drawing.Size(515, 22);
			this.textEditLinkPath.StyleController = this.styleController;
			this.textEditLinkPath.TabIndex = 27;
			// 
			// laLinkPath
			// 
			this.laLinkPath.AutoSize = true;
			this.laLinkPath.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laLinkPath.ForeColor = System.Drawing.Color.Black;
			this.laLinkPath.Location = new System.Drawing.Point(4, 161);
			this.laLinkPath.Name = "laLinkPath";
			this.laLinkPath.Size = new System.Drawing.Size(169, 19);
			this.laLinkPath.TabIndex = 28;
			this.laLinkPath.Text = "Edit URL path here…";
			// 
			// LinkWebOptions
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.laLinkPath);
			this.Controls.Add(this.textEditLinkPath);
			this.Controls.Add(this.ckForcePreview);
			this.Controls.Add(this.ckIsUrl365);
			this.Controls.Add(this.labelControlTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LinkWebOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkPath.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.CheckBox ckIsUrl365;
		public System.Windows.Forms.CheckBox ckForcePreview;
		public DevExpress.XtraEditors.TextEdit textEditLinkPath;
		public System.Windows.Forms.Label laLinkPath;
	}
}
