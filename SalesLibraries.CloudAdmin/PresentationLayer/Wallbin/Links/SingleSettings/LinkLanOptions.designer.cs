namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkLanOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkLanOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlName = new DevExpress.XtraEditors.LabelControl();
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			this.textEditPath = new DevExpress.XtraEditors.TextEdit();
			this.labelControlPath = new DevExpress.XtraEditors.LabelControl();
			this.labelControlWarning = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).BeginInit();
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
			// labelControlName
			// 
			this.labelControlName.AllowHtmlString = true;
			this.labelControlName.Location = new System.Drawing.Point(17, 149);
			this.labelControlName.Name = "labelControlName";
			this.labelControlName.Size = new System.Drawing.Size(120, 16);
			this.labelControlName.StyleController = this.styleController;
			this.labelControlName.TabIndex = 27;
			this.labelControlName.Text = "<b>Link Name</b>";
			// 
			// textEditName
			// 
			this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditName.Location = new System.Drawing.Point(17, 171);
			this.textEditName.Name = "textEditName";
			this.textEditName.Size = new System.Drawing.Size(504, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 28;
			// 
			// textEditPath
			// 
			this.textEditPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditPath.Location = new System.Drawing.Point(17, 237);
			this.textEditPath.Name = "textEditPath";
			this.textEditPath.Size = new System.Drawing.Size(504, 22);
			this.textEditPath.StyleController = this.styleController;
			this.textEditPath.TabIndex = 30;
			// 
			// labelControlPath
			// 
			this.labelControlPath.AllowHtmlString = true;
			this.labelControlPath.Location = new System.Drawing.Point(17, 215);
			this.labelControlPath.Name = "labelControlPath";
			this.labelControlPath.Size = new System.Drawing.Size(163, 16);
			this.labelControlPath.StyleController = this.styleController;
			this.labelControlPath.TabIndex = 29;
			this.labelControlPath.Text = "<b>Network Location</b>";
			// 
			// labelControlWarning
			// 
			this.labelControlWarning.AllowHtmlString = true;
			this.labelControlWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlWarning.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
			this.labelControlWarning.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlWarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlWarning.Location = new System.Drawing.Point(17, 304);
			this.labelControlWarning.Name = "labelControlWarning";
			this.labelControlWarning.Size = new System.Drawing.Size(495, 234);
			this.labelControlWarning.StyleController = this.styleController;
			this.labelControlWarning.TabIndex = 34;
			this.labelControlWarning.Text = resources.GetString("labelControlWarning.Text");
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.LinkSettingsPreviewOptions;
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
			// LinkLanOptions
			// 
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.labelControlWarning);
			this.Controls.Add(this.textEditPath);
			this.Controls.Add(this.labelControlPath);
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.labelControlName);
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlName;
		private DevExpress.XtraEditors.TextEdit textEditName;
		private DevExpress.XtraEditors.TextEdit textEditPath;
		private DevExpress.XtraEditors.LabelControl labelControlPath;
		private DevExpress.XtraEditors.LabelControl labelControlWarning;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
	}
}
