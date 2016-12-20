namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkExcelOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkExcelOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.ckDoNotGenerateText = new System.Windows.Forms.CheckBox();
			this.ckForceOpen = new System.Windows.Forms.CheckBox();
			this.ckForceDownload = new System.Windows.Forms.CheckBox();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.ckIsArchiveResource = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
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
			// ckDoNotGenerateText
			// 
			this.ckDoNotGenerateText.AutoSize = true;
			this.ckDoNotGenerateText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGenerateText.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGenerateText.Location = new System.Drawing.Point(17, 182);
			this.ckDoNotGenerateText.Name = "ckDoNotGenerateText";
			this.ckDoNotGenerateText.Size = new System.Drawing.Size(452, 20);
			this.ckDoNotGenerateText.TabIndex = 25;
			this.ckDoNotGenerateText.Text = "Do NOT Create Full Data File (Always select this for Nielsen Books) ";
			this.ckDoNotGenerateText.UseVisualStyleBackColor = true;
			// 
			// ckForceOpen
			// 
			this.ckForceOpen.AutoSize = true;
			this.ckForceOpen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForceOpen.ForeColor = System.Drawing.Color.Black;
			this.ckForceOpen.Location = new System.Drawing.Point(17, 268);
			this.ckForceOpen.Name = "ckForceOpen";
			this.ckForceOpen.Size = new System.Drawing.Size(334, 20);
			this.ckForceOpen.TabIndex = 29;
			this.ckForceOpen.Text = "Auto-Open this file in custom sales app browsers";
			this.ckForceOpen.UseVisualStyleBackColor = true;
			// 
			// ckForceDownload
			// 
			this.ckForceDownload.AutoSize = true;
			this.ckForceDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForceDownload.ForeColor = System.Drawing.Color.Black;
			this.ckForceDownload.Location = new System.Drawing.Point(17, 225);
			this.ckForceDownload.Name = "ckForceDownload";
			this.ckForceDownload.Size = new System.Drawing.Size(351, 20);
			this.ckForceDownload.TabIndex = 28;
			this.ckForceDownload.Text = "Immediately Download this Excel file when clicked";
			this.ckForceDownload.UseVisualStyleBackColor = true;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.LinkSettingsPreviewOptions;
			this.pictureBoxLogo.Location = new System.Drawing.Point(17, 18);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
			this.pictureBoxLogo.TabIndex = 52;
			this.pictureBoxLogo.TabStop = false;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.AllowHtmlString = true;
			this.labelControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTitle.Location = new System.Drawing.Point(103, 18);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(406, 83);
			this.labelControlTitle.StyleController = this.styleController;
			this.labelControlTitle.TabIndex = 51;
			this.labelControlTitle.Text = resources.GetString("labelControlTitle.Text");
			// 
			// ckIsArchiveResource
			// 
			this.ckIsArchiveResource.AutoSize = true;
			this.ckIsArchiveResource.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckIsArchiveResource.ForeColor = System.Drawing.Color.Black;
			this.ckIsArchiveResource.Location = new System.Drawing.Point(17, 139);
			this.ckIsArchiveResource.Name = "ckIsArchiveResource";
			this.ckIsArchiveResource.Size = new System.Drawing.Size(175, 20);
			this.ckIsArchiveResource.TabIndex = 65;
			this.ckIsArchiveResource.Text = "Link Archive Resources";
			this.ckIsArchiveResource.UseVisualStyleBackColor = true;
			this.ckIsArchiveResource.CheckedChanged += new System.EventHandler(this.ckIsArchiveResource_CheckedChanged);
			// 
			// LinkExcelOptions
			// 
			this.Controls.Add(this.ckIsArchiveResource);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.ckForceOpen);
			this.Controls.Add(this.ckForceDownload);
			this.Controls.Add(this.ckDoNotGenerateText);
			this.Name = "LinkExcelOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		public System.Windows.Forms.CheckBox ckDoNotGenerateText;
		public System.Windows.Forms.CheckBox ckForceOpen;
		public System.Windows.Forms.CheckBox ckForceDownload;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.CheckBox ckIsArchiveResource;
	}
}
