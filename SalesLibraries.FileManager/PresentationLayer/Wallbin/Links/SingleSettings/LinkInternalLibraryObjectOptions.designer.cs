namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkInternalLibraryObjectOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkInternalLibraryObjectOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlName = new DevExpress.XtraEditors.LabelControl();
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			this.labelControlLibraryName = new DevExpress.XtraEditors.LabelControl();
			this.labelControlPageName = new DevExpress.XtraEditors.LabelControl();
			this.labelControlWindowName = new DevExpress.XtraEditors.LabelControl();
			this.labelControlLinkName = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.comboBoxEditLibraryName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditLibraryLinkName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditWindowName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditPageName = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryLinkName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditWindowName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageName.Properties)).BeginInit();
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
			// labelControlLibraryName
			// 
			this.labelControlLibraryName.AllowHtmlString = true;
			this.labelControlLibraryName.Location = new System.Drawing.Point(17, 206);
			this.labelControlLibraryName.Name = "labelControlLibraryName";
			this.labelControlLibraryName.Size = new System.Drawing.Size(90, 16);
			this.labelControlLibraryName.StyleController = this.styleController;
			this.labelControlLibraryName.TabIndex = 29;
			this.labelControlLibraryName.Text = "<b>Target Library</b>";
			// 
			// labelControlPageName
			// 
			this.labelControlPageName.AllowHtmlString = true;
			this.labelControlPageName.Location = new System.Drawing.Point(17, 256);
			this.labelControlPageName.Name = "labelControlPageName";
			this.labelControlPageName.Size = new System.Drawing.Size(78, 16);
			this.labelControlPageName.StyleController = this.styleController;
			this.labelControlPageName.TabIndex = 31;
			this.labelControlPageName.Text = "<b>Target Page</b>";
			// 
			// labelControlWindowName
			// 
			this.labelControlWindowName.AllowHtmlString = true;
			this.labelControlWindowName.Location = new System.Drawing.Point(17, 306);
			this.labelControlWindowName.Name = "labelControlWindowName";
			this.labelControlWindowName.Size = new System.Drawing.Size(97, 16);
			this.labelControlWindowName.StyleController = this.styleController;
			this.labelControlWindowName.TabIndex = 33;
			this.labelControlWindowName.Text = "<b>Target Window</b>";
			// 
			// labelControlLinkName
			// 
			this.labelControlLinkName.AllowHtmlString = true;
			this.labelControlLinkName.Location = new System.Drawing.Point(17, 356);
			this.labelControlLinkName.Name = "labelControlLinkName";
			this.labelControlLinkName.Size = new System.Drawing.Size(72, 16);
			this.labelControlLinkName.StyleController = this.styleController;
			this.labelControlLinkName.TabIndex = 35;
			this.labelControlLinkName.Text = "<b>Target Link</b>";
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
			// comboBoxEditLibraryName
			// 
			this.comboBoxEditLibraryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditLibraryName.Location = new System.Drawing.Point(17, 228);
			this.comboBoxEditLibraryName.Name = "comboBoxEditLibraryName";
			this.comboBoxEditLibraryName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditLibraryName.Size = new System.Drawing.Size(501, 22);
			this.comboBoxEditLibraryName.StyleController = this.styleController;
			this.comboBoxEditLibraryName.TabIndex = 56;
			this.comboBoxEditLibraryName.EditValueChanged += new System.EventHandler(this.OnLibraryChanged);
			// 
			// comboBoxEditLibraryLinkName
			// 
			this.comboBoxEditLibraryLinkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditLibraryLinkName.Location = new System.Drawing.Point(17, 378);
			this.comboBoxEditLibraryLinkName.Name = "comboBoxEditLibraryLinkName";
			this.comboBoxEditLibraryLinkName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditLibraryLinkName.Size = new System.Drawing.Size(501, 22);
			this.comboBoxEditLibraryLinkName.StyleController = this.styleController;
			this.comboBoxEditLibraryLinkName.TabIndex = 59;
			// 
			// comboBoxEditWindowName
			// 
			this.comboBoxEditWindowName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditWindowName.Location = new System.Drawing.Point(17, 328);
			this.comboBoxEditWindowName.Name = "comboBoxEditWindowName";
			this.comboBoxEditWindowName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditWindowName.Size = new System.Drawing.Size(501, 22);
			this.comboBoxEditWindowName.StyleController = this.styleController;
			this.comboBoxEditWindowName.TabIndex = 58;
			this.comboBoxEditWindowName.EditValueChanged += new System.EventHandler(this.OnLibraryFolderChanged);
			// 
			// comboBoxEditPageName
			// 
			this.comboBoxEditPageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditPageName.Location = new System.Drawing.Point(17, 278);
			this.comboBoxEditPageName.Name = "comboBoxEditPageName";
			this.comboBoxEditPageName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditPageName.Size = new System.Drawing.Size(501, 22);
			this.comboBoxEditPageName.StyleController = this.styleController;
			this.comboBoxEditPageName.TabIndex = 57;
			this.comboBoxEditPageName.EditValueChanged += new System.EventHandler(this.OnLibraryPageChanged);
			// 
			// LinkInternalLibraryObjectOptions
			// 
			this.Controls.Add(this.comboBoxEditLibraryName);
			this.Controls.Add(this.comboBoxEditLibraryLinkName);
			this.Controls.Add(this.comboBoxEditWindowName);
			this.Controls.Add(this.comboBoxEditPageName);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.labelControlLinkName);
			this.Controls.Add(this.labelControlWindowName);
			this.Controls.Add(this.labelControlPageName);
			this.Controls.Add(this.labelControlLibraryName);
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.labelControlName);
			this.Name = "LinkInternalLibraryObjectOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryLinkName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditWindowName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlName;
		private DevExpress.XtraEditors.TextEdit textEditName;
		private DevExpress.XtraEditors.LabelControl labelControlLibraryName;
		private DevExpress.XtraEditors.LabelControl labelControlPageName;
		private DevExpress.XtraEditors.LabelControl labelControlWindowName;
		private DevExpress.XtraEditors.LabelControl labelControlLinkName;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLibraryName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLibraryLinkName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditWindowName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPageName;
	}
}
