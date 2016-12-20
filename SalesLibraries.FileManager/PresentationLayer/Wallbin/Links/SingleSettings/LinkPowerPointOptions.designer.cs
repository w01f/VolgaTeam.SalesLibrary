namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkPowerPointOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkPowerPointOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.ckDoNotGeneratePreview = new System.Windows.Forms.CheckBox();
			this.ckDoNotGenerateText = new System.Windows.Forms.CheckBox();
			this.labelControlTitleButtons = new DevExpress.XtraEditors.LabelControl();
			this.labelControlTitleSettings = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonXRefreshPreview = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenWV = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenQV = new DevComponents.DotNetBar.ButtonX();
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
			// ckDoNotGeneratePreview
			// 
			this.ckDoNotGeneratePreview.AutoSize = true;
			this.ckDoNotGeneratePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGeneratePreview.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGeneratePreview.Location = new System.Drawing.Point(30, 170);
			this.ckDoNotGeneratePreview.Name = "ckDoNotGeneratePreview";
			this.ckDoNotGeneratePreview.Size = new System.Drawing.Size(513, 20);
			this.ckDoNotGeneratePreview.TabIndex = 27;
			this.ckDoNotGeneratePreview.Text = "Do NOT Generate PNG preview images (Always select this for Nielsen Books)";
			this.ckDoNotGeneratePreview.UseVisualStyleBackColor = true;
			// 
			// ckDoNotGenerateText
			// 
			this.ckDoNotGenerateText.AutoSize = true;
			this.ckDoNotGenerateText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGenerateText.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGenerateText.Location = new System.Drawing.Point(30, 200);
			this.ckDoNotGenerateText.Name = "ckDoNotGenerateText";
			this.ckDoNotGenerateText.Size = new System.Drawing.Size(452, 20);
			this.ckDoNotGenerateText.TabIndex = 26;
			this.ckDoNotGenerateText.Text = "Do NOT Create Full Data File (Always select this for Nielsen Books) ";
			this.ckDoNotGenerateText.UseVisualStyleBackColor = true;
			// 
			// labelControlTitleButtons
			// 
			this.labelControlTitleButtons.Location = new System.Drawing.Point(17, 263);
			this.labelControlTitleButtons.Name = "labelControlTitleButtons";
			this.labelControlTitleButtons.Size = new System.Drawing.Size(121, 16);
			this.labelControlTitleButtons.StyleController = this.styleController;
			this.labelControlTitleButtons.TabIndex = 62;
			this.labelControlTitleButtons.Text = "Link Archive Folders:";
			// 
			// labelControlTitleSettings
			// 
			this.labelControlTitleSettings.Location = new System.Drawing.Point(17, 139);
			this.labelControlTitleSettings.Name = "labelControlTitleSettings";
			this.labelControlTitleSettings.Size = new System.Drawing.Size(140, 16);
			this.labelControlTitleSettings.StyleController = this.styleController;
			this.labelControlTitleSettings.TabIndex = 61;
			this.labelControlTitleSettings.Text = "Link Archive Resources:";
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsPreviewOptions;
			this.pictureBoxLogo.Location = new System.Drawing.Point(17, 18);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
			this.pictureBoxLogo.TabIndex = 58;
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
			this.labelControlTitle.TabIndex = 57;
			this.labelControlTitle.Text = resources.GetString("labelControlTitle.Text");
			// 
			// buttonXRefreshPreview
			// 
			this.buttonXRefreshPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefreshPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshPreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXRefreshPreview.Location = new System.Drawing.Point(77, 402);
			this.buttonXRefreshPreview.Name = "buttonXRefreshPreview";
			this.buttonXRefreshPreview.Size = new System.Drawing.Size(375, 30);
			this.buttonXRefreshPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshPreview.TabIndex = 65;
			this.buttonXRefreshPreview.Text = "Refresh QV & WV";
			this.buttonXRefreshPreview.TextColor = System.Drawing.Color.Black;
			this.buttonXRefreshPreview.UseMnemonic = false;
			this.buttonXRefreshPreview.Click += new System.EventHandler(this.buttonXRefreshPreview_Click);
			// 
			// buttonXOpenWV
			// 
			this.buttonXOpenWV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenWV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOpenWV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenWV.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpenWV.Location = new System.Drawing.Point(77, 351);
			this.buttonXOpenWV.Name = "buttonXOpenWV";
			this.buttonXOpenWV.Size = new System.Drawing.Size(375, 30);
			this.buttonXOpenWV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenWV.TabIndex = 64;
			this.buttonXOpenWV.Text = "!WV Folder";
			this.buttonXOpenWV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenWV.Click += new System.EventHandler(this.buttonXOpenWV_Click);
			// 
			// buttonXOpenQV
			// 
			this.buttonXOpenQV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenQV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOpenQV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenQV.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpenQV.Location = new System.Drawing.Point(77, 300);
			this.buttonXOpenQV.Name = "buttonXOpenQV";
			this.buttonXOpenQV.Size = new System.Drawing.Size(375, 30);
			this.buttonXOpenQV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenQV.TabIndex = 63;
			this.buttonXOpenQV.Text = "!QV Folder";
			this.buttonXOpenQV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenQV.Click += new System.EventHandler(this.buttonXOpenQV_Click);
			// 
			// LinkPowerPointOptions
			// 
			this.Controls.Add(this.buttonXRefreshPreview);
			this.Controls.Add(this.buttonXOpenWV);
			this.Controls.Add(this.buttonXOpenQV);
			this.Controls.Add(this.labelControlTitleButtons);
			this.Controls.Add(this.labelControlTitleSettings);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.ckDoNotGenerateText);
			this.Controls.Add(this.ckDoNotGeneratePreview);
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		public System.Windows.Forms.CheckBox ckDoNotGeneratePreview;
		public System.Windows.Forms.CheckBox ckDoNotGenerateText;
		private DevExpress.XtraEditors.LabelControl labelControlTitleButtons;
		private DevExpress.XtraEditors.LabelControl labelControlTitleSettings;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public DevComponents.DotNetBar.ButtonX buttonXRefreshPreview;
		public DevComponents.DotNetBar.ButtonX buttonXOpenWV;
		public DevComponents.DotNetBar.ButtonX buttonXOpenQV;
	}
}
