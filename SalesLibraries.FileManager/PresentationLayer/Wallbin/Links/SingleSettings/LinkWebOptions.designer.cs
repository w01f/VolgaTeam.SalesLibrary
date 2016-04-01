﻿namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
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
			this.labelControlName = new DevExpress.XtraEditors.LabelControl();
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			this.textEditPath = new DevExpress.XtraEditors.TextEdit();
			this.labelControlPath = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).BeginInit();
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
			this.ckIsUrl365.Location = new System.Drawing.Point(8, 196);
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
			this.ckForcePreview.Location = new System.Drawing.Point(8, 240);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(301, 20);
			this.ckForcePreview.TabIndex = 26;
			this.ckForcePreview.Text = "Immediately Launch this URL when clicked";
			this.ckForcePreview.UseVisualStyleBackColor = true;
			// 
			// labelControlName
			// 
			this.labelControlName.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlName.Location = new System.Drawing.Point(8, 55);
			this.labelControlName.Name = "labelControlName";
			this.labelControlName.Size = new System.Drawing.Size(62, 16);
			this.labelControlName.StyleController = this.styleController;
			this.labelControlName.TabIndex = 27;
			this.labelControlName.Text = "Link Name";
			// 
			// textEditName
			// 
			this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditName.Location = new System.Drawing.Point(8, 77);
			this.textEditName.Name = "textEditName";
			this.textEditName.Size = new System.Drawing.Size(513, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 28;
			// 
			// textEditPath
			// 
			this.textEditPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditPath.Location = new System.Drawing.Point(8, 143);
			this.textEditPath.Name = "textEditPath";
			this.textEditPath.Size = new System.Drawing.Size(513, 22);
			this.textEditPath.StyleController = this.styleController;
			this.textEditPath.TabIndex = 30;
			// 
			// labelControlPath
			// 
			this.labelControlPath.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlPath.Location = new System.Drawing.Point(8, 121);
			this.labelControlPath.Name = "labelControlPath";
			this.labelControlPath.Size = new System.Drawing.Size(55, 16);
			this.labelControlPath.StyleController = this.styleController;
			this.labelControlPath.TabIndex = 29;
			this.labelControlPath.Text = "Link Path";
			// 
			// LinkWebOptions
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.textEditPath);
			this.Controls.Add(this.labelControlPath);
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.labelControlName);
			this.Controls.Add(this.ckForcePreview);
			this.Controls.Add(this.ckIsUrl365);
			this.Controls.Add(this.labelControlTitle);
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.CheckBox ckIsUrl365;
		public System.Windows.Forms.CheckBox ckForcePreview;
		private DevExpress.XtraEditors.LabelControl labelControlName;
		private DevExpress.XtraEditors.TextEdit textEditName;
		private DevExpress.XtraEditors.TextEdit textEditPath;
		private DevExpress.XtraEditors.LabelControl labelControlPath;
	}
}
