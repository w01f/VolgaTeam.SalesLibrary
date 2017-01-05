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
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.ckOpenOnSamePage = new System.Windows.Forms.CheckBox();
			this.labelControlShortcutLink = new DevExpress.XtraEditors.LabelControl();
			this.labelControlShortcutGroup = new DevExpress.XtraEditors.LabelControl();
			this.comboBoxEditShortcutGroup = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditShortcutLink = new DevExpress.XtraEditors.ComboBoxEdit();
			this.labelControlShortcutDescription = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShortcutGroup.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShortcutLink.Properties)).BeginInit();
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
			this.labelControlName.Location = new System.Drawing.Point(17, 165);
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
			this.textEditName.Location = new System.Drawing.Point(103, 162);
			this.textEditName.Name = "textEditName";
			this.textEditName.Size = new System.Drawing.Size(415, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 28;
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
			this.ckOpenOnSamePage.Location = new System.Drawing.Point(17, 309);
			this.ckOpenOnSamePage.Name = "ckOpenOnSamePage";
			this.ckOpenOnSamePage.Size = new System.Drawing.Size(284, 20);
			this.ckOpenOnSamePage.TabIndex = 51;
			this.ckOpenOnSamePage.Text = "Launch this shortcut in new Browser Tab";
			this.ckOpenOnSamePage.UseVisualStyleBackColor = true;
			// 
			// labelControlShortcutLink
			// 
			this.labelControlShortcutLink.AllowHtmlString = true;
			this.labelControlShortcutLink.Location = new System.Drawing.Point(17, 240);
			this.labelControlShortcutLink.Name = "labelControlShortcutLink";
			this.labelControlShortcutLink.Size = new System.Drawing.Size(13, 16);
			this.labelControlShortcutLink.StyleController = this.styleController;
			this.labelControlShortcutLink.TabIndex = 66;
			this.labelControlShortcutLink.Text = "<b>ID</b>";
			// 
			// labelControlShortcutGroup
			// 
			this.labelControlShortcutGroup.AllowHtmlString = true;
			this.labelControlShortcutGroup.Location = new System.Drawing.Point(17, 203);
			this.labelControlShortcutGroup.Name = "labelControlShortcutGroup";
			this.labelControlShortcutGroup.Size = new System.Drawing.Size(39, 16);
			this.labelControlShortcutGroup.StyleController = this.styleController;
			this.labelControlShortcutGroup.TabIndex = 65;
			this.labelControlShortcutGroup.Text = "<b>Group</b>";
			// 
			// comboBoxEditShortcutGroup
			// 
			this.comboBoxEditShortcutGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditShortcutGroup.Location = new System.Drawing.Point(103, 201);
			this.comboBoxEditShortcutGroup.Name = "comboBoxEditShortcutGroup";
			this.comboBoxEditShortcutGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditShortcutGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditShortcutGroup.Size = new System.Drawing.Size(415, 22);
			this.comboBoxEditShortcutGroup.StyleController = this.styleController;
			this.comboBoxEditShortcutGroup.TabIndex = 67;
			this.comboBoxEditShortcutGroup.EditValueChanged += new System.EventHandler(this.OnShortcutGroupChanged);
			// 
			// comboBoxEditShortcutLink
			// 
			this.comboBoxEditShortcutLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditShortcutLink.Location = new System.Drawing.Point(103, 238);
			this.comboBoxEditShortcutLink.Name = "comboBoxEditShortcutLink";
			this.comboBoxEditShortcutLink.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditShortcutLink.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditShortcutLink.Size = new System.Drawing.Size(415, 22);
			this.comboBoxEditShortcutLink.StyleController = this.styleController;
			this.comboBoxEditShortcutLink.TabIndex = 68;
			this.comboBoxEditShortcutLink.EditValueChanged += new System.EventHandler(this.OnShortcutLinkChanged);
			// 
			// labelControlShortcutDescription
			// 
			this.labelControlShortcutDescription.AllowHtmlString = true;
			this.labelControlShortcutDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlShortcutDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlShortcutDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlShortcutDescription.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlShortcutDescription.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlShortcutDescription.AppearanceHovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlShortcutDescription.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlShortcutDescription.AppearancePressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlShortcutDescription.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlShortcutDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlShortcutDescription.Location = new System.Drawing.Point(103, 266);
			this.labelControlShortcutDescription.Name = "labelControlShortcutDescription";
			this.labelControlShortcutDescription.Size = new System.Drawing.Size(415, 37);
			this.labelControlShortcutDescription.StyleController = this.styleController;
			this.labelControlShortcutDescription.TabIndex = 69;
			// 
			// LinkInternalShortcutOptions
			// 
			this.Controls.Add(this.labelControlShortcutDescription);
			this.Controls.Add(this.labelControlShortcutLink);
			this.Controls.Add(this.labelControlShortcutGroup);
			this.Controls.Add(this.comboBoxEditShortcutGroup);
			this.Controls.Add(this.comboBoxEditShortcutLink);
			this.Controls.Add(this.ckOpenOnSamePage);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.labelControlName);
			this.Name = "LinkInternalShortcutOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShortcutGroup.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShortcutLink.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlName;
		private DevExpress.XtraEditors.TextEdit textEditName;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.CheckBox ckOpenOnSamePage;
		private DevExpress.XtraEditors.LabelControl labelControlShortcutLink;
		private DevExpress.XtraEditors.LabelControl labelControlShortcutGroup;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditShortcutGroup;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditShortcutLink;
		private DevExpress.XtraEditors.LabelControl labelControlShortcutDescription;
	}
}
