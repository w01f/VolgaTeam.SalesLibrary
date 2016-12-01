namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkInternalShortcutOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkInternalShortcutOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlName = new DevExpress.XtraEditors.LabelControl();
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			this.textEditShortcutId = new DevExpress.XtraEditors.TextEdit();
			this.labelControlLibraryName = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.ckOpenOnSamePage = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditShortcutId.Properties)).BeginInit();
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
			this.labelControlName.Location = new System.Drawing.Point(17, 140);
			this.labelControlName.Name = "labelControlName";
			this.labelControlName.Size = new System.Drawing.Size(68, 16);
			this.labelControlName.StyleController = this.styleController;
			this.labelControlName.TabIndex = 27;
			this.labelControlName.Text = "<b>Link Name</b>";
			// 
			// textEditName
			// 
			this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditName.Location = new System.Drawing.Point(17, 162);
			this.textEditName.Name = "textEditName";
			this.textEditName.Size = new System.Drawing.Size(501, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 28;
			// 
			// textEditShortcutId
			// 
			this.textEditShortcutId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditShortcutId.Location = new System.Drawing.Point(17, 228);
			this.textEditShortcutId.Name = "textEditShortcutId";
			this.textEditShortcutId.Size = new System.Drawing.Size(501, 22);
			this.textEditShortcutId.StyleController = this.styleController;
			this.textEditShortcutId.TabIndex = 30;
			// 
			// labelControlLibraryName
			// 
			this.labelControlLibraryName.AllowHtmlString = true;
			this.labelControlLibraryName.Location = new System.Drawing.Point(17, 206);
			this.labelControlLibraryName.Name = "labelControlLibraryName";
			this.labelControlLibraryName.Size = new System.Drawing.Size(53, 16);
			this.labelControlLibraryName.StyleController = this.styleController;
			this.labelControlLibraryName.TabIndex = 29;
			this.labelControlLibraryName.Text = "<b>Static ID</b>";
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
			// ckOpenOnSamePage
			// 
			this.ckOpenOnSamePage.AutoSize = true;
			this.ckOpenOnSamePage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckOpenOnSamePage.ForeColor = System.Drawing.Color.Black;
			this.ckOpenOnSamePage.Location = new System.Drawing.Point(17, 285);
			this.ckOpenOnSamePage.Name = "ckOpenOnSamePage";
			this.ckOpenOnSamePage.Size = new System.Drawing.Size(284, 20);
			this.ckOpenOnSamePage.TabIndex = 51;
			this.ckOpenOnSamePage.Text = "Launch this shortcut in new Browser Tab";
			this.ckOpenOnSamePage.UseVisualStyleBackColor = true;
			// 
			// LinkInternalShortcutOptions
			// 
			this.Controls.Add(this.ckOpenOnSamePage);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.textEditShortcutId);
			this.Controls.Add(this.labelControlLibraryName);
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.labelControlName);
			this.Name = "LinkInternalShortcutOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditShortcutId.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlName;
		private DevExpress.XtraEditors.TextEdit textEditName;
		private DevExpress.XtraEditors.TextEdit textEditShortcutId;
		private DevExpress.XtraEditors.LabelControl labelControlLibraryName;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.CheckBox ckOpenOnSamePage;
	}
}
