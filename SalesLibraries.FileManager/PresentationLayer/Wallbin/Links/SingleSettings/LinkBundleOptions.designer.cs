namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkBundleOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkBundleOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlDescription = new DevExpress.XtraEditors.LabelControl();
			this.checkEditPowerPoint = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditVideo = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditPdf = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditWord = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditExcel = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditUrl = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditPng = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditJpeg = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPowerPoint.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditVideo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPdf.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditWord.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditExcel.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditUrl.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPng.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditJpeg.Properties)).BeginInit();
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
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsPreviewOptions;
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
			// labelControlDescription
			// 
			this.labelControlDescription.AllowHtmlString = true;
			this.labelControlDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDescription.Location = new System.Drawing.Point(17, 137);
			this.labelControlDescription.Name = "labelControlDescription";
			this.labelControlDescription.Size = new System.Drawing.Size(492, 124);
			this.labelControlDescription.StyleController = this.styleController;
			this.labelControlDescription.TabIndex = 53;
			this.labelControlDescription.Text = resources.GetString("labelControlDescription.Text");
			// 
			// checkEditPowerPoint
			// 
			this.checkEditPowerPoint.Location = new System.Drawing.Point(17, 276);
			this.checkEditPowerPoint.Name = "checkEditPowerPoint";
			this.checkEditPowerPoint.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditPowerPoint.Properties.AutoWidth = true;
			this.checkEditPowerPoint.Properties.Caption = "PowerPoint";
			this.checkEditPowerPoint.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditPowerPoint.Properties.RadioGroupIndex = 1;
			this.checkEditPowerPoint.Size = new System.Drawing.Size(88, 20);
			this.checkEditPowerPoint.StyleController = this.styleController;
			this.checkEditPowerPoint.TabIndex = 54;
			// 
			// checkEditVideo
			// 
			this.checkEditVideo.Location = new System.Drawing.Point(154, 276);
			this.checkEditVideo.Name = "checkEditVideo";
			this.checkEditVideo.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditVideo.Properties.AutoWidth = true;
			this.checkEditVideo.Properties.Caption = "Video";
			this.checkEditVideo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditVideo.Properties.RadioGroupIndex = 1;
			this.checkEditVideo.Size = new System.Drawing.Size(55, 20);
			this.checkEditVideo.StyleController = this.styleController;
			this.checkEditVideo.TabIndex = 55;
			this.checkEditVideo.TabStop = false;
			// 
			// checkEditPdf
			// 
			this.checkEditPdf.Location = new System.Drawing.Point(265, 276);
			this.checkEditPdf.Name = "checkEditPdf";
			this.checkEditPdf.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditPdf.Properties.AutoWidth = true;
			this.checkEditPdf.Properties.Caption = "PDF";
			this.checkEditPdf.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditPdf.Properties.RadioGroupIndex = 1;
			this.checkEditPdf.Size = new System.Drawing.Size(48, 20);
			this.checkEditPdf.StyleController = this.styleController;
			this.checkEditPdf.TabIndex = 56;
			this.checkEditPdf.TabStop = false;
			// 
			// checkEditWord
			// 
			this.checkEditWord.Location = new System.Drawing.Point(383, 276);
			this.checkEditWord.Name = "checkEditWord";
			this.checkEditWord.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditWord.Properties.AutoWidth = true;
			this.checkEditWord.Properties.Caption = "Word";
			this.checkEditWord.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditWord.Properties.RadioGroupIndex = 1;
			this.checkEditWord.Size = new System.Drawing.Size(53, 20);
			this.checkEditWord.StyleController = this.styleController;
			this.checkEditWord.TabIndex = 57;
			this.checkEditWord.TabStop = false;
			// 
			// checkEditExcel
			// 
			this.checkEditExcel.Location = new System.Drawing.Point(17, 336);
			this.checkEditExcel.Name = "checkEditExcel";
			this.checkEditExcel.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditExcel.Properties.AutoWidth = true;
			this.checkEditExcel.Properties.Caption = "Excel";
			this.checkEditExcel.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditExcel.Properties.RadioGroupIndex = 1;
			this.checkEditExcel.Size = new System.Drawing.Size(55, 20);
			this.checkEditExcel.StyleController = this.styleController;
			this.checkEditExcel.TabIndex = 58;
			this.checkEditExcel.TabStop = false;
			// 
			// checkEditUrl
			// 
			this.checkEditUrl.Location = new System.Drawing.Point(154, 336);
			this.checkEditUrl.Name = "checkEditUrl";
			this.checkEditUrl.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditUrl.Properties.AutoWidth = true;
			this.checkEditUrl.Properties.Caption = "Url";
			this.checkEditUrl.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditUrl.Properties.RadioGroupIndex = 1;
			this.checkEditUrl.Size = new System.Drawing.Size(38, 20);
			this.checkEditUrl.StyleController = this.styleController;
			this.checkEditUrl.TabIndex = 59;
			this.checkEditUrl.TabStop = false;
			// 
			// checkEditPng
			// 
			this.checkEditPng.Location = new System.Drawing.Point(265, 336);
			this.checkEditPng.Name = "checkEditPng";
			this.checkEditPng.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditPng.Properties.AutoWidth = true;
			this.checkEditPng.Properties.Caption = "Image (png)";
			this.checkEditPng.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditPng.Properties.RadioGroupIndex = 1;
			this.checkEditPng.Size = new System.Drawing.Size(90, 20);
			this.checkEditPng.StyleController = this.styleController;
			this.checkEditPng.TabIndex = 60;
			this.checkEditPng.TabStop = false;
			// 
			// checkEditJpeg
			// 
			this.checkEditJpeg.Location = new System.Drawing.Point(383, 336);
			this.checkEditJpeg.Name = "checkEditJpeg";
			this.checkEditJpeg.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditJpeg.Properties.AutoWidth = true;
			this.checkEditJpeg.Properties.Caption = "Image (jpeg)";
			this.checkEditJpeg.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditJpeg.Properties.RadioGroupIndex = 1;
			this.checkEditJpeg.Size = new System.Drawing.Size(93, 20);
			this.checkEditJpeg.StyleController = this.styleController;
			this.checkEditJpeg.TabIndex = 61;
			this.checkEditJpeg.TabStop = false;
			// 
			// LinkBundleOptions
			// 
			this.Controls.Add(this.checkEditJpeg);
			this.Controls.Add(this.checkEditPng);
			this.Controls.Add(this.checkEditUrl);
			this.Controls.Add(this.checkEditExcel);
			this.Controls.Add(this.checkEditWord);
			this.Controls.Add(this.checkEditPdf);
			this.Controls.Add(this.checkEditVideo);
			this.Controls.Add(this.checkEditPowerPoint);
			this.Controls.Add(this.labelControlDescription);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Name = "LinkBundleOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPowerPoint.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditVideo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPdf.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditWord.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditExcel.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditUrl.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPng.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditJpeg.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.LabelControl labelControlDescription;
		private DevExpress.XtraEditors.CheckEdit checkEditPowerPoint;
		private DevExpress.XtraEditors.CheckEdit checkEditVideo;
		private DevExpress.XtraEditors.CheckEdit checkEditPdf;
		private DevExpress.XtraEditors.CheckEdit checkEditWord;
		private DevExpress.XtraEditors.CheckEdit checkEditExcel;
		private DevExpress.XtraEditors.CheckEdit checkEditUrl;
		private DevExpress.XtraEditors.CheckEdit checkEditPng;
		private DevExpress.XtraEditors.CheckEdit checkEditJpeg;
	}
}
