namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	partial class BannerSettingsControl
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.checkBoxEnableBanner = new System.Windows.Forms.CheckBox();
			this.pbSelectedBanner = new DevExpress.XtraEditors.PictureEdit();
			this.rbBannerAligmentRight = new System.Windows.Forms.RadioButton();
			this.rbBannerAligmentCenter = new System.Windows.Forms.RadioButton();
			this.rbBannerAligmentLeft = new System.Windows.Forms.RadioButton();
			this.laBannerAligment = new System.Windows.Forms.Label();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.colorEditBannerTextColor = new DevExpress.XtraEditors.ColorEdit();
			this.buttonEditBannerTextFont = new DevExpress.XtraEditors.ButtonEdit();
			this.memoEditBannerText = new DevExpress.XtraEditors.MemoEdit();
			this.checkBoxBannerShowText = new System.Windows.Forms.CheckBox();
			this.laTextFormat = new System.Windows.Forms.Label();
			this.xtraTabControlBanners = new DevExpress.XtraTab.XtraTabControl();
			this.pnControls = new System.Windows.Forms.Panel();
			this.pnSearch = new System.Windows.Forms.Panel();
			this.labelControlSearchTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonXSearch = new DevComponents.DotNetBar.ButtonX();
			this.textEditSearch = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedBanner.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditBannerTextColor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditBannerTextFont.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditBannerText.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlBanners)).BeginInit();
			this.pnControls.SuspendLayout();
			this.pnSearch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxEnableBanner
			// 
			this.checkBoxEnableBanner.AutoSize = true;
			this.checkBoxEnableBanner.BackColor = System.Drawing.Color.White;
			this.checkBoxEnableBanner.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxEnableBanner.ForeColor = System.Drawing.Color.Black;
			this.checkBoxEnableBanner.Location = new System.Drawing.Point(12, 12);
			this.checkBoxEnableBanner.Name = "checkBoxEnableBanner";
			this.checkBoxEnableBanner.Size = new System.Drawing.Size(121, 20);
			this.checkBoxEnableBanner.TabIndex = 7;
			this.checkBoxEnableBanner.Text = "Enable Banner";
			this.checkBoxEnableBanner.UseVisualStyleBackColor = false;
			this.checkBoxEnableBanner.CheckedChanged += new System.EventHandler(this.checkBoxEnableBanner_CheckedChanged);
			// 
			// pbSelectedBanner
			// 
			this.pbSelectedBanner.Location = new System.Drawing.Point(143, 10);
			this.pbSelectedBanner.Name = "pbSelectedBanner";
			this.pbSelectedBanner.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pbSelectedBanner.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.pbSelectedBanner.Properties.Appearance.Options.UseBackColor = true;
			this.pbSelectedBanner.Properties.Appearance.Options.UseForeColor = true;
			this.pbSelectedBanner.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pbSelectedBanner.Properties.NullText = " ";
			this.pbSelectedBanner.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			this.pbSelectedBanner.Size = new System.Drawing.Size(266, 59);
			this.pbSelectedBanner.TabIndex = 36;
			// 
			// rbBannerAligmentRight
			// 
			this.rbBannerAligmentRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbBannerAligmentRight.BackColor = System.Drawing.Color.Transparent;
			this.rbBannerAligmentRight.ForeColor = System.Drawing.Color.Black;
			this.rbBannerAligmentRight.Location = new System.Drawing.Point(836, 8);
			this.rbBannerAligmentRight.Name = "rbBannerAligmentRight";
			this.rbBannerAligmentRight.Size = new System.Drawing.Size(86, 20);
			this.rbBannerAligmentRight.TabIndex = 40;
			this.rbBannerAligmentRight.TabStop = true;
			this.rbBannerAligmentRight.Text = "Right";
			this.rbBannerAligmentRight.UseVisualStyleBackColor = false;
			this.rbBannerAligmentRight.CheckedChanged += new System.EventHandler(this.OnBannerAligmentChanged);
			// 
			// rbBannerAligmentCenter
			// 
			this.rbBannerAligmentCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbBannerAligmentCenter.BackColor = System.Drawing.Color.Transparent;
			this.rbBannerAligmentCenter.ForeColor = System.Drawing.Color.Black;
			this.rbBannerAligmentCenter.Location = new System.Drawing.Point(733, 8);
			this.rbBannerAligmentCenter.Name = "rbBannerAligmentCenter";
			this.rbBannerAligmentCenter.Size = new System.Drawing.Size(83, 20);
			this.rbBannerAligmentCenter.TabIndex = 39;
			this.rbBannerAligmentCenter.TabStop = true;
			this.rbBannerAligmentCenter.Text = "Center";
			this.rbBannerAligmentCenter.UseVisualStyleBackColor = false;
			this.rbBannerAligmentCenter.CheckedChanged += new System.EventHandler(this.OnBannerAligmentChanged);
			// 
			// rbBannerAligmentLeft
			// 
			this.rbBannerAligmentLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbBannerAligmentLeft.BackColor = System.Drawing.Color.Transparent;
			this.rbBannerAligmentLeft.ForeColor = System.Drawing.Color.Black;
			this.rbBannerAligmentLeft.Location = new System.Drawing.Point(639, 8);
			this.rbBannerAligmentLeft.Name = "rbBannerAligmentLeft";
			this.rbBannerAligmentLeft.Size = new System.Drawing.Size(77, 20);
			this.rbBannerAligmentLeft.TabIndex = 38;
			this.rbBannerAligmentLeft.TabStop = true;
			this.rbBannerAligmentLeft.Text = "Left";
			this.rbBannerAligmentLeft.UseVisualStyleBackColor = false;
			this.rbBannerAligmentLeft.CheckedChanged += new System.EventHandler(this.OnBannerAligmentChanged);
			// 
			// laBannerAligment
			// 
			this.laBannerAligment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.laBannerAligment.AutoSize = true;
			this.laBannerAligment.BackColor = System.Drawing.Color.Transparent;
			this.laBannerAligment.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laBannerAligment.ForeColor = System.Drawing.Color.Black;
			this.laBannerAligment.Location = new System.Drawing.Point(493, 10);
			this.laBannerAligment.Name = "laBannerAligment";
			this.laBannerAligment.Size = new System.Drawing.Size(127, 16);
			this.laBannerAligment.TabIndex = 37;
			this.laBannerAligment.Text = "Banner Alignment:";
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Options.UseForeColor = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// colorEditBannerTextColor
			// 
			this.colorEditBannerTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.colorEditBannerTextColor.EditValue = System.Drawing.Color.Empty;
			this.colorEditBannerTextColor.Enabled = false;
			this.colorEditBannerTextColor.Location = new System.Drawing.Point(677, 573);
			this.colorEditBannerTextColor.Name = "colorEditBannerTextColor";
			this.colorEditBannerTextColor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.colorEditBannerTextColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditBannerTextColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditBannerTextColor.Properties.Appearance.Options.UseForeColor = true;
			this.colorEditBannerTextColor.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.colorEditBannerTextColor.Properties.AppearanceDisabled.Options.UseBackColor = true;
			this.colorEditBannerTextColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.colorEditBannerTextColor.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colorEditBannerTextColor.Properties.ShowSystemColors = false;
			this.colorEditBannerTextColor.Size = new System.Drawing.Size(105, 22);
			this.colorEditBannerTextColor.StyleController = this.styleController;
			this.colorEditBannerTextColor.TabIndex = 44;
			this.colorEditBannerTextColor.EditValueChanged += new System.EventHandler(this.colorEditBannerTextColor_EditValueChanged);
			// 
			// buttonEditBannerTextFont
			// 
			this.buttonEditBannerTextFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditBannerTextFont.Enabled = false;
			this.buttonEditBannerTextFont.Location = new System.Drawing.Point(677, 545);
			this.buttonEditBannerTextFont.Name = "buttonEditBannerTextFont";
			this.buttonEditBannerTextFont.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.buttonEditBannerTextFont.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditBannerTextFont.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditBannerTextFont.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditBannerTextFont.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.buttonEditBannerTextFont.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.buttonEditBannerTextFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditBannerTextFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.buttonEditBannerTextFont.Size = new System.Drawing.Size(234, 22);
			this.buttonEditBannerTextFont.StyleController = this.styleController;
			this.buttonEditBannerTextFont.TabIndex = 43;
			this.buttonEditBannerTextFont.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FontEdit_ButtonClick);
			this.buttonEditBannerTextFont.EditValueChanged += new System.EventHandler(this.buttonEditBannerTextFont_EditValueChanged);
			this.buttonEditBannerTextFont.Click += new System.EventHandler(this.FontEdit_Click);
			// 
			// memoEditBannerText
			// 
			this.memoEditBannerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditBannerText.Enabled = false;
			this.memoEditBannerText.Location = new System.Drawing.Point(16, 546);
			this.memoEditBannerText.Name = "memoEditBannerText";
			this.memoEditBannerText.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.memoEditBannerText.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.memoEditBannerText.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditBannerText.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditBannerText.Size = new System.Drawing.Size(643, 49);
			this.memoEditBannerText.StyleController = this.styleController;
			this.memoEditBannerText.TabIndex = 42;
			// 
			// checkBoxBannerShowText
			// 
			this.checkBoxBannerShowText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxBannerShowText.AutoSize = true;
			this.checkBoxBannerShowText.BackColor = System.Drawing.Color.Transparent;
			this.checkBoxBannerShowText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.checkBoxBannerShowText.ForeColor = System.Drawing.Color.Black;
			this.checkBoxBannerShowText.Location = new System.Drawing.Point(16, 522);
			this.checkBoxBannerShowText.Name = "checkBoxBannerShowText";
			this.checkBoxBannerShowText.Size = new System.Drawing.Size(134, 20);
			this.checkBoxBannerShowText.TabIndex = 41;
			this.checkBoxBannerShowText.Text = "Show Link Label";
			this.checkBoxBannerShowText.UseVisualStyleBackColor = false;
			this.checkBoxBannerShowText.CheckedChanged += new System.EventHandler(this.checkBoxBannerShowText_CheckedChanged);
			// 
			// laTextFormat
			// 
			this.laTextFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.laTextFormat.AutoSize = true;
			this.laTextFormat.BackColor = System.Drawing.Color.Transparent;
			this.laTextFormat.Enabled = false;
			this.laTextFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTextFormat.ForeColor = System.Drawing.Color.Black;
			this.laTextFormat.Location = new System.Drawing.Point(674, 523);
			this.laTextFormat.Name = "laTextFormat";
			this.laTextFormat.Size = new System.Drawing.Size(84, 16);
			this.laTextFormat.TabIndex = 45;
			this.laTextFormat.Text = "Format Text";
			// 
			// xtraTabControlBanners
			// 
			this.xtraTabControlBanners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControlBanners.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraTabControlBanners.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlBanners.Appearance.Options.UseBackColor = true;
			this.xtraTabControlBanners.Appearance.Options.UseForeColor = true;
			this.xtraTabControlBanners.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlBanners.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlBanners.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlBanners.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlBanners.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlBanners.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlBanners.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlBanners.Location = new System.Drawing.Point(16, 80);
			this.xtraTabControlBanners.Name = "xtraTabControlBanners";
			this.xtraTabControlBanners.Size = new System.Drawing.Size(895, 423);
			this.xtraTabControlBanners.TabIndex = 46;
			// 
			// pnControls
			// 
			this.pnControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnControls.BackColor = System.Drawing.Color.Transparent;
			this.pnControls.Controls.Add(this.pnSearch);
			this.pnControls.Controls.Add(this.xtraTabControlBanners);
			this.pnControls.Controls.Add(this.pbSelectedBanner);
			this.pnControls.Controls.Add(this.laTextFormat);
			this.pnControls.Controls.Add(this.laBannerAligment);
			this.pnControls.Controls.Add(this.colorEditBannerTextColor);
			this.pnControls.Controls.Add(this.rbBannerAligmentLeft);
			this.pnControls.Controls.Add(this.buttonEditBannerTextFont);
			this.pnControls.Controls.Add(this.rbBannerAligmentCenter);
			this.pnControls.Controls.Add(this.memoEditBannerText);
			this.pnControls.Controls.Add(this.rbBannerAligmentRight);
			this.pnControls.Controls.Add(this.checkBoxBannerShowText);
			this.pnControls.Enabled = false;
			this.pnControls.ForeColor = System.Drawing.Color.Black;
			this.pnControls.Location = new System.Drawing.Point(-4, 3);
			this.pnControls.Name = "pnControls";
			this.pnControls.Size = new System.Drawing.Size(922, 608);
			this.pnControls.TabIndex = 47;
			// 
			// pnSearch
			// 
			this.pnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnSearch.BackColor = System.Drawing.Color.Transparent;
			this.pnSearch.Controls.Add(this.labelControlSearchTitle);
			this.pnSearch.Controls.Add(this.buttonXSearch);
			this.pnSearch.Controls.Add(this.textEditSearch);
			this.pnSearch.Enabled = false;
			this.pnSearch.ForeColor = System.Drawing.Color.Black;
			this.pnSearch.Location = new System.Drawing.Point(549, 41);
			this.pnSearch.Name = "pnSearch";
			this.pnSearch.Size = new System.Drawing.Size(362, 33);
			this.pnSearch.TabIndex = 47;
			// 
			// labelControlSearchTitle
			// 
			this.labelControlSearchTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSearchTitle.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSearchTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlSearchTitle.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSearchTitle.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.labelControlSearchTitle.Location = new System.Drawing.Point(7, 9);
			this.labelControlSearchTitle.Name = "labelControlSearchTitle";
			this.labelControlSearchTitle.Size = new System.Drawing.Size(60, 16);
			this.labelControlSearchTitle.StyleController = this.styleController;
			this.labelControlSearchTitle.TabIndex = 16;
			this.labelControlSearchTitle.Text = "Keyword:";
			// 
			// buttonXSearch
			// 
			this.buttonXSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSearch.Enabled = false;
			this.buttonXSearch.Location = new System.Drawing.Point(280, 5);
			this.buttonXSearch.Name = "buttonXSearch";
			this.buttonXSearch.Size = new System.Drawing.Size(77, 24);
			this.buttonXSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSearch.TabIndex = 18;
			this.buttonXSearch.Text = "Search";
			this.buttonXSearch.TextColor = System.Drawing.Color.Black;
			this.buttonXSearch.Click += new System.EventHandler(this.OnSearchButtonClick);
			// 
			// textEditSearch
			// 
			this.textEditSearch.Location = new System.Drawing.Point(72, 6);
			this.textEditSearch.Name = "textEditSearch";
			this.textEditSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditSearch.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditSearch.Properties.Appearance.Options.UseBackColor = true;
			this.textEditSearch.Properties.Appearance.Options.UseForeColor = true;
			this.textEditSearch.Size = new System.Drawing.Size(191, 22);
			this.textEditSearch.StyleController = this.styleController;
			this.textEditSearch.TabIndex = 17;
			this.textEditSearch.EditValueChanged += new System.EventHandler(this.OnSearchEditValueChanged);
			this.textEditSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyDown);
			// 
			// BannerSettingsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.checkBoxEnableBanner);
			this.Controls.Add(this.pnControls);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "BannerSettingsControl";
			this.Size = new System.Drawing.Size(916, 610);
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedBanner.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditBannerTextColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditBannerTextFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditBannerText.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlBanners)).EndInit();
			this.pnControls.ResumeLayout(false);
			this.pnControls.PerformLayout();
			this.pnSearch.ResumeLayout(false);
			this.pnSearch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.CheckBox checkBoxEnableBanner;
		private DevExpress.XtraEditors.PictureEdit pbSelectedBanner;
		private System.Windows.Forms.RadioButton rbBannerAligmentRight;
		private System.Windows.Forms.RadioButton rbBannerAligmentCenter;
		private System.Windows.Forms.RadioButton rbBannerAligmentLeft;
		private System.Windows.Forms.Label laBannerAligment;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.ColorEdit colorEditBannerTextColor;
		private DevExpress.XtraEditors.ButtonEdit buttonEditBannerTextFont;
		private DevExpress.XtraEditors.MemoEdit memoEditBannerText;
		private System.Windows.Forms.CheckBox checkBoxBannerShowText;
		private System.Windows.Forms.Label laTextFormat;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlBanners;
		private System.Windows.Forms.Panel pnControls;
		private System.Windows.Forms.Panel pnSearch;
		private DevExpress.XtraEditors.LabelControl labelControlSearchTitle;
		private DevComponents.DotNetBar.ButtonX buttonXSearch;
		private DevExpress.XtraEditors.TextEdit textEditSearch;
	}
}