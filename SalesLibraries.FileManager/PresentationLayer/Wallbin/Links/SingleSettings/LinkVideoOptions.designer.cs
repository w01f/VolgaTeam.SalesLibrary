namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkVideoOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkVideoOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.ckForcePreview = new System.Windows.Forms.CheckBox();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonXRefreshPreview = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenWV = new DevComponents.DotNetBar.ButtonX();
			this.labelControlTitleSettings = new DevExpress.XtraEditors.LabelControl();
			this.labelControlTitleButtons = new DevExpress.XtraEditors.LabelControl();
			this.ckDownloadSource = new System.Windows.Forms.CheckBox();
			this.labelControlDownloadSourceDescription = new DevExpress.XtraEditors.LabelControl();
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
			// ckForcePreview
			// 
			this.ckForcePreview.AutoSize = true;
			this.ckForcePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForcePreview.ForeColor = System.Drawing.Color.Black;
			this.ckForcePreview.Location = new System.Drawing.Point(30, 170);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(311, 20);
			this.ckForcePreview.TabIndex = 25;
			this.ckForcePreview.Text = "Immediately Play this Video in Cloud Library";
			this.ckForcePreview.UseVisualStyleBackColor = true;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsPreviewOptions;
			this.pictureBoxLogo.Location = new System.Drawing.Point(17, 18);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
			this.pictureBoxLogo.TabIndex = 50;
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
			this.labelControlTitle.TabIndex = 49;
			this.labelControlTitle.Text = resources.GetString("labelControlTitle.Text");
			// 
			// buttonXRefreshPreview
			// 
			this.buttonXRefreshPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefreshPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshPreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXRefreshPreview.Location = new System.Drawing.Point(78, 373);
			this.buttonXRefreshPreview.Name = "buttonXRefreshPreview";
			this.buttonXRefreshPreview.Size = new System.Drawing.Size(375, 30);
			this.buttonXRefreshPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshPreview.TabIndex = 53;
			this.buttonXRefreshPreview.Text = "Refresh WV";
			this.buttonXRefreshPreview.TextColor = System.Drawing.Color.Black;
			this.buttonXRefreshPreview.Click += new System.EventHandler(this.buttonXRefreshPreview_Click);
			// 
			// buttonXOpenWV
			// 
			this.buttonXOpenWV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenWV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOpenWV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenWV.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpenWV.Location = new System.Drawing.Point(78, 321);
			this.buttonXOpenWV.Name = "buttonXOpenWV";
			this.buttonXOpenWV.Size = new System.Drawing.Size(375, 30);
			this.buttonXOpenWV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenWV.TabIndex = 52;
			this.buttonXOpenWV.Text = "!WV Folder";
			this.buttonXOpenWV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenWV.Click += new System.EventHandler(this.buttonXOpenWV_Click);
			// 
			// labelControlTitleSettings
			// 
			this.labelControlTitleSettings.Location = new System.Drawing.Point(17, 139);
			this.labelControlTitleSettings.Name = "labelControlTitleSettings";
			this.labelControlTitleSettings.Size = new System.Drawing.Size(140, 16);
			this.labelControlTitleSettings.StyleController = this.styleController;
			this.labelControlTitleSettings.TabIndex = 54;
			this.labelControlTitleSettings.Text = "Link Archive Resources:";
			// 
			// labelControlTitleButtons
			// 
			this.labelControlTitleButtons.Location = new System.Drawing.Point(17, 285);
			this.labelControlTitleButtons.Name = "labelControlTitleButtons";
			this.labelControlTitleButtons.Size = new System.Drawing.Size(121, 16);
			this.labelControlTitleButtons.StyleController = this.styleController;
			this.labelControlTitleButtons.TabIndex = 55;
			this.labelControlTitleButtons.Text = "Link Archive Folders:";
			// 
			// checkBoxDownloadSource
			// 
			this.ckDownloadSource.AutoSize = true;
			this.ckDownloadSource.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDownloadSource.ForeColor = System.Drawing.Color.Black;
			this.ckDownloadSource.Location = new System.Drawing.Point(30, 206);
			this.ckDownloadSource.Name = "ckDownloadSource";
			this.ckDownloadSource.Size = new System.Drawing.Size(232, 20);
			this.ckDownloadSource.TabIndex = 56;
			this.ckDownloadSource.Text = "Enable Source Download button";
			this.ckDownloadSource.UseVisualStyleBackColor = true;
			// 
			// labelControlDownloadSourceDescription
			// 
			this.labelControlDownloadSourceDescription.AllowHtmlString = true;
			this.labelControlDownloadSourceDescription.Location = new System.Drawing.Point(47, 232);
			this.labelControlDownloadSourceDescription.Name = "labelControlDownloadSourceDescription";
			this.labelControlDownloadSourceDescription.Size = new System.Drawing.Size(345, 16);
			this.labelControlDownloadSourceDescription.StyleController = this.styleController;
			this.labelControlDownloadSourceDescription.TabIndex = 57;
			this.labelControlDownloadSourceDescription.Text = "(This allows users to download the original <b>High-Res</b> Video)";
			// 
			// LinkVideoOptions
			// 
			this.Controls.Add(this.labelControlDownloadSourceDescription);
			this.Controls.Add(this.ckDownloadSource);
			this.Controls.Add(this.labelControlTitleButtons);
			this.Controls.Add(this.labelControlTitleSettings);
			this.Controls.Add(this.buttonXRefreshPreview);
			this.Controls.Add(this.buttonXOpenWV);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.ckForcePreview);
			this.Name = "LinkVideoOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		public System.Windows.Forms.CheckBox ckForcePreview;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public DevComponents.DotNetBar.ButtonX buttonXRefreshPreview;
		public DevComponents.DotNetBar.ButtonX buttonXOpenWV;
		private DevExpress.XtraEditors.LabelControl labelControlTitleSettings;
		private DevExpress.XtraEditors.LabelControl labelControlTitleButtons;
		public System.Windows.Forms.CheckBox ckDownloadSource;
		private DevExpress.XtraEditors.LabelControl labelControlDownloadSourceDescription;
	}
}
