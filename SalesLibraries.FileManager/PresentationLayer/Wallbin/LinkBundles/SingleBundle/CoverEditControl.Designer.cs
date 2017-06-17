namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	partial class CoverEditControl
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
			this.pictureEditImage = new DevExpress.XtraEditors.PictureEdit();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnTop)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditImage.Properties)).BeginInit();
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
			this.pnTop.Size = new System.Drawing.Size(632, 77);
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
			this.labelControlTitle.Size = new System.Drawing.Size(540, 64);
			this.labelControlTitle.StyleController = this.styleController;
			this.labelControlTitle.TabIndex = 1;
			this.labelControlTitle.Text = "<size=+2>Add a Cover Image to this Link Bundle…</size>";
			this.labelControlTitle.UseMnemonic = false;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxLogo.ForeColor = System.Drawing.Color.Black;
			this.pictureBoxLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.BundlesEditorCoverLogo;
			this.pictureBoxLogo.Location = new System.Drawing.Point(7, 6);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(67, 64);
			this.pictureBoxLogo.TabIndex = 0;
			this.pictureBoxLogo.TabStop = false;
			// 
			// pictureEditImage
			// 
			this.pictureEditImage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureEditImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureEditImage.Location = new System.Drawing.Point(0, 77);
			this.pictureEditImage.Name = "pictureEditImage";
			this.pictureEditImage.Properties.NullText = "Click to add Image...";
			this.pictureEditImage.Properties.ReadOnly = true;
			this.pictureEditImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditImage.Properties.ShowMenu = false;
			this.pictureEditImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			this.pictureEditImage.Size = new System.Drawing.Size(632, 430);
			this.pictureEditImage.StyleController = this.styleController;
			this.pictureEditImage.TabIndex = 12;
			this.pictureEditImage.Click += new System.EventHandler(this.OnImageEditClick);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "png";
			this.openFileDialog.Filter = "Png Files|*.png|Bitmap|*.bmp|Jpeg Files|*.jpg, *.jpeg";
			// 
			// CoverEditControl
			// 
			this.Controls.Add(this.pictureEditImage);
			this.Controls.Add(this.pnTop);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(632, 507);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnTop)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditImage.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.PanelControl pnTop;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.PictureEdit pictureEditImage;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}
