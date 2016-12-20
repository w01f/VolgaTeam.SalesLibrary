namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkDocumentOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkDocumentOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.ckDoNotGeneratePreview = new System.Windows.Forms.CheckBox();
			this.ckDoNotGenerateText = new System.Windows.Forms.CheckBox();
			this.labelControlTitleButtons = new DevExpress.XtraEditors.LabelControl();
			this.buttonXRefreshPreview = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenWV = new DevComponents.DotNetBar.ButtonX();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.ckForcePreview = new System.Windows.Forms.CheckBox();
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
			// ckDoNotGeneratePreview
			// 
			this.ckDoNotGeneratePreview.AutoSize = true;
			this.ckDoNotGeneratePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGeneratePreview.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGeneratePreview.Location = new System.Drawing.Point(17, 173);
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
			this.ckDoNotGenerateText.Location = new System.Drawing.Point(17, 201);
			this.ckDoNotGenerateText.Name = "ckDoNotGenerateText";
			this.ckDoNotGenerateText.Size = new System.Drawing.Size(452, 20);
			this.ckDoNotGenerateText.TabIndex = 26;
			this.ckDoNotGenerateText.Text = "Do NOT Create Full Data File (Always select this for Nielsen Books) ";
			this.ckDoNotGenerateText.UseVisualStyleBackColor = true;
			// 
			// labelControlTitleButtons
			// 
			this.labelControlTitleButtons.Location = new System.Drawing.Point(17, 278);
			this.labelControlTitleButtons.Name = "labelControlTitleButtons";
			this.labelControlTitleButtons.Size = new System.Drawing.Size(121, 16);
			this.labelControlTitleButtons.StyleController = this.styleController;
			this.labelControlTitleButtons.TabIndex = 62;
			this.labelControlTitleButtons.Text = "Link Archive Folders:";
			// 
			// buttonXRefreshPreview
			// 
			this.buttonXRefreshPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefreshPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshPreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXRefreshPreview.Location = new System.Drawing.Point(78, 366);
			this.buttonXRefreshPreview.Name = "buttonXRefreshPreview";
			this.buttonXRefreshPreview.Size = new System.Drawing.Size(375, 30);
			this.buttonXRefreshPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshPreview.TabIndex = 60;
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
			this.buttonXOpenWV.Location = new System.Drawing.Point(78, 314);
			this.buttonXOpenWV.Name = "buttonXOpenWV";
			this.buttonXOpenWV.Size = new System.Drawing.Size(375, 30);
			this.buttonXOpenWV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenWV.TabIndex = 59;
			this.buttonXOpenWV.Text = "!WV Folder";
			this.buttonXOpenWV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenWV.Click += new System.EventHandler(this.buttonXOpenWV_Click);
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
			// ckForcePreview
			// 
			this.ckForcePreview.AutoSize = true;
			this.ckForcePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForcePreview.ForeColor = System.Drawing.Color.Black;
			this.ckForcePreview.Location = new System.Drawing.Point(17, 229);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(343, 20);
			this.ckForcePreview.TabIndex = 63;
			this.ckForcePreview.Text = "Immediately Launch this PDF in new Browser Tab";
			this.ckForcePreview.UseVisualStyleBackColor = true;
			// 
			// ckIsArchiveResource
			// 
			this.ckIsArchiveResource.AutoSize = true;
			this.ckIsArchiveResource.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckIsArchiveResource.ForeColor = System.Drawing.Color.Black;
			this.ckIsArchiveResource.Location = new System.Drawing.Point(17, 145);
			this.ckIsArchiveResource.Name = "ckIsArchiveResource";
			this.ckIsArchiveResource.Size = new System.Drawing.Size(175, 20);
			this.ckIsArchiveResource.TabIndex = 64;
			this.ckIsArchiveResource.Text = "Link Archive Resources";
			this.ckIsArchiveResource.UseVisualStyleBackColor = true;
			this.ckIsArchiveResource.CheckedChanged += new System.EventHandler(this.ckIsArchiveResource_CheckedChanged);
			// 
			// LinkDocumentOptions
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.ckIsArchiveResource);
			this.Controls.Add(this.ckForcePreview);
			this.Controls.Add(this.labelControlTitleButtons);
			this.Controls.Add(this.buttonXRefreshPreview);
			this.Controls.Add(this.buttonXOpenWV);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.ckDoNotGenerateText);
			this.Controls.Add(this.ckDoNotGeneratePreview);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LinkDocumentOptions";
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
		public DevComponents.DotNetBar.ButtonX buttonXRefreshPreview;
		public DevComponents.DotNetBar.ButtonX buttonXOpenWV;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.CheckBox ckForcePreview;
		public System.Windows.Forms.CheckBox ckIsArchiveResource;
	}
}
