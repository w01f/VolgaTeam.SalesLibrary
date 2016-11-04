namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	partial class InfoEditControl
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
			this.pnTop = new DevExpress.XtraEditors.PanelControl();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.checkEditHeader = new DevExpress.XtraEditors.CheckEdit();
			this.textEditHeader = new DevExpress.XtraEditors.TextEdit();
			this.checkEditBody = new DevExpress.XtraEditors.CheckEdit();
			this.memoEditBody = new DevExpress.XtraEditors.MemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnTop)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditHeader.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditHeader.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditBody.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditBody.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.ForeColor = System.Drawing.Color.Black;
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.Appearance.Options.UseForeColor = true;
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
			// pnTop
			// 
			this.pnTop.Appearance.BackColor = System.Drawing.Color.White;
			this.pnTop.Appearance.ForeColor = System.Drawing.Color.Black;
			this.pnTop.Appearance.Options.UseBackColor = true;
			this.pnTop.Appearance.Options.UseForeColor = true;
			this.pnTop.Controls.Add(this.labelControlTitle);
			this.pnTop.Controls.Add(this.pictureBoxLogo);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(492, 77);
			this.pnTop.TabIndex = 5;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.AllowHtmlString = true;
			this.labelControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.labelControlTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTitle.Location = new System.Drawing.Point(87, 6);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(397, 64);
			this.labelControlTitle.StyleController = this.styleController;
			this.labelControlTitle.TabIndex = 1;
			this.labelControlTitle.Text = "<size=+2>Do you want to share any basic details of info about this Link Bundle? M" +
    "aybe a brief overview of a sales initiative, or general facts about a project or" +
    " sponsorship...</size>";
			this.labelControlTitle.UseMnemonic = false;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxLogo.ForeColor = System.Drawing.Color.Black;
			this.pictureBoxLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.BundlesEditorInfoLogo;
			this.pictureBoxLogo.Location = new System.Drawing.Point(7, 6);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(74, 64);
			this.pictureBoxLogo.TabIndex = 0;
			this.pictureBoxLogo.TabStop = false;
			// 
			// checkEditHeader
			// 
			this.checkEditHeader.Location = new System.Drawing.Point(7, 109);
			this.checkEditHeader.Name = "checkEditHeader";
			this.checkEditHeader.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditHeader.Properties.AutoWidth = true;
			this.checkEditHeader.Properties.Caption = "Header Bullet Point:";
			this.checkEditHeader.Size = new System.Drawing.Size(138, 20);
			this.checkEditHeader.StyleController = this.styleController;
			this.checkEditHeader.TabIndex = 6;
			this.checkEditHeader.CheckedChanged += new System.EventHandler(this.OnHeaderCheckedChanged);
			// 
			// textEditHeader
			// 
			this.textEditHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditHeader.Enabled = false;
			this.textEditHeader.Location = new System.Drawing.Point(7, 135);
			this.textEditHeader.Name = "textEditHeader";
			this.textEditHeader.Size = new System.Drawing.Size(477, 22);
			this.textEditHeader.StyleController = this.styleController;
			this.textEditHeader.TabIndex = 7;
			// 
			// checkEditBody
			// 
			this.checkEditBody.Location = new System.Drawing.Point(7, 199);
			this.checkEditBody.Name = "checkEditBody";
			this.checkEditBody.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditBody.Properties.AutoWidth = true;
			this.checkEditBody.Properties.Caption = "Body:";
			this.checkEditBody.Size = new System.Drawing.Size(56, 20);
			this.checkEditBody.StyleController = this.styleController;
			this.checkEditBody.TabIndex = 8;
			this.checkEditBody.CheckedChanged += new System.EventHandler(this.OnBodyCheckedChanged);
			// 
			// memoEditBody
			// 
			this.memoEditBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditBody.Enabled = false;
			this.memoEditBody.Location = new System.Drawing.Point(7, 225);
			this.memoEditBody.Name = "memoEditBody";
			this.memoEditBody.Size = new System.Drawing.Size(477, 285);
			this.memoEditBody.StyleController = this.styleController;
			this.memoEditBody.TabIndex = 9;
			// 
			// InfoEditControl
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.memoEditBody);
			this.Controls.Add(this.checkEditBody);
			this.Controls.Add(this.textEditHeader);
			this.Controls.Add(this.checkEditHeader);
			this.Controls.Add(this.pnTop);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(492, 523);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnTop)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditHeader.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditHeader.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditBody.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditBody.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.PanelControl pnTop;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.CheckEdit checkEditHeader;
		private DevExpress.XtraEditors.TextEdit textEditHeader;
		private DevExpress.XtraEditors.CheckEdit checkEditBody;
		private DevExpress.XtraEditors.MemoEdit memoEditBody;
	}
}
