namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	partial class WidgetSettingsControl
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
			this.pbCustomWidget = new System.Windows.Forms.PictureBox();
			this.radioButtonWidgetTypeCustom = new System.Windows.Forms.RadioButton();
			this.radioButtonWidgetTypeDisabled = new System.Windows.Forms.RadioButton();
			this.pnSearch = new System.Windows.Forms.Panel();
			this.labelControlSearchTitle = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonXSearch = new DevComponents.DotNetBar.ButtonX();
			this.textEditSearch = new DevExpress.XtraEditors.TextEdit();
			this.pnGallery = new System.Windows.Forms.Panel();
			this.xtraTabControlGallery = new DevExpress.XtraTab.XtraTabControl();
			this.retractableBarGallery = new SalesLibraries.CommonGUI.RetractableBar.RetractableBarLeft();
			this.treeViewGallery = new SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.ImageGallery.GalleryTreeView();
			this.labelControlSelectedGalleryName = new DevExpress.XtraEditors.LabelControl();
			this.colorEditInversionColor = new SalesLibraries.CommonGUI.Common.HtmlColorEdit();
			this.checkEditInvert = new DevExpress.XtraEditors.CheckEdit();
			this.contextMenuStripImage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemImageAddToFavorites = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pbCustomWidget)).BeginInit();
			this.pnSearch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).BeginInit();
			this.pnGallery.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGallery)).BeginInit();
			this.retractableBarGallery.Content.SuspendLayout();
			this.retractableBarGallery.Header.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorEditInversionColor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditInvert.Properties)).BeginInit();
			this.contextMenuStripImage.SuspendLayout();
			this.SuspendLayout();
			// 
			// pbCustomWidget
			// 
			this.pbCustomWidget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pbCustomWidget.BackColor = System.Drawing.Color.Transparent;
			this.pbCustomWidget.ContextMenuStrip = this.contextMenuStripImage;
			this.pbCustomWidget.Enabled = false;
			this.pbCustomWidget.ForeColor = System.Drawing.Color.Black;
			this.pbCustomWidget.Location = new System.Drawing.Point(293, 497);
			this.pbCustomWidget.Name = "pbCustomWidget";
			this.pbCustomWidget.Size = new System.Drawing.Size(36, 36);
			this.pbCustomWidget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbCustomWidget.TabIndex = 8;
			this.pbCustomWidget.TabStop = false;
			// 
			// radioButtonWidgetTypeCustom
			// 
			this.radioButtonWidgetTypeCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonWidgetTypeCustom.AutoSize = true;
			this.radioButtonWidgetTypeCustom.BackColor = System.Drawing.Color.White;
			this.radioButtonWidgetTypeCustom.ForeColor = System.Drawing.Color.Black;
			this.radioButtonWidgetTypeCustom.Location = new System.Drawing.Point(172, 505);
			this.radioButtonWidgetTypeCustom.Name = "radioButtonWidgetTypeCustom";
			this.radioButtonWidgetTypeCustom.Size = new System.Drawing.Size(120, 20);
			this.radioButtonWidgetTypeCustom.TabIndex = 12;
			this.radioButtonWidgetTypeCustom.TabStop = true;
			this.radioButtonWidgetTypeCustom.Text = "Custom Widget:";
			this.radioButtonWidgetTypeCustom.UseVisualStyleBackColor = false;
			this.radioButtonWidgetTypeCustom.CheckedChanged += new System.EventHandler(this.OnWidgetTypeChanged);
			// 
			// radioButtonWidgetTypeDisabled
			// 
			this.radioButtonWidgetTypeDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonWidgetTypeDisabled.AutoSize = true;
			this.radioButtonWidgetTypeDisabled.BackColor = System.Drawing.Color.White;
			this.radioButtonWidgetTypeDisabled.ForeColor = System.Drawing.Color.Black;
			this.radioButtonWidgetTypeDisabled.Location = new System.Drawing.Point(6, 505);
			this.radioButtonWidgetTypeDisabled.Name = "radioButtonWidgetTypeDisabled";
			this.radioButtonWidgetTypeDisabled.Size = new System.Drawing.Size(87, 20);
			this.radioButtonWidgetTypeDisabled.TabIndex = 13;
			this.radioButtonWidgetTypeDisabled.TabStop = true;
			this.radioButtonWidgetTypeDisabled.Text = "No Widget";
			this.radioButtonWidgetTypeDisabled.UseVisualStyleBackColor = false;
			this.radioButtonWidgetTypeDisabled.CheckedChanged += new System.EventHandler(this.OnWidgetTypeChanged);
			// 
			// pnSearch
			// 
			this.pnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pnSearch.BackColor = System.Drawing.Color.Transparent;
			this.pnSearch.Controls.Add(this.labelControlSearchTitle);
			this.pnSearch.Controls.Add(this.buttonXSearch);
			this.pnSearch.Controls.Add(this.textEditSearch);
			this.pnSearch.Enabled = false;
			this.pnSearch.ForeColor = System.Drawing.Color.Black;
			this.pnSearch.Location = new System.Drawing.Point(555, 497);
			this.pnSearch.Name = "pnSearch";
			this.pnSearch.Size = new System.Drawing.Size(362, 36);
			this.pnSearch.TabIndex = 21;
			this.pnSearch.Click += new System.EventHandler(this.OnFormClick);
			// 
			// labelControlSearchTitle
			// 
			this.labelControlSearchTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSearchTitle.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSearchTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlSearchTitle.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSearchTitle.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.labelControlSearchTitle.Location = new System.Drawing.Point(7, 10);
			this.labelControlSearchTitle.Name = "labelControlSearchTitle";
			this.labelControlSearchTitle.Size = new System.Drawing.Size(60, 16);
			this.labelControlSearchTitle.StyleController = this.styleController;
			this.labelControlSearchTitle.TabIndex = 16;
			this.labelControlSearchTitle.Text = "Keyword:";
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
			// buttonXSearch
			// 
			this.buttonXSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSearch.Enabled = false;
			this.buttonXSearch.Location = new System.Drawing.Point(280, 6);
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
			this.textEditSearch.Location = new System.Drawing.Point(72, 7);
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
			// pnGallery
			// 
			this.pnGallery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnGallery.Controls.Add(this.xtraTabControlGallery);
			this.pnGallery.Controls.Add(this.retractableBarGallery);
			this.pnGallery.ForeColor = System.Drawing.Color.Black;
			this.pnGallery.Location = new System.Drawing.Point(0, 0);
			this.pnGallery.Name = "pnGallery";
			this.pnGallery.Size = new System.Drawing.Size(920, 491);
			this.pnGallery.TabIndex = 58;
			// 
			// xtraTabControlGallery
			// 
			this.xtraTabControlGallery.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraTabControlGallery.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlGallery.Appearance.Options.UseBackColor = true;
			this.xtraTabControlGallery.Appearance.Options.UseForeColor = true;
			this.xtraTabControlGallery.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGallery.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlGallery.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGallery.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlGallery.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGallery.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlGallery.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGallery.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlGallery.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGallery.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlGallery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlGallery.Location = new System.Drawing.Point(249, 0);
			this.xtraTabControlGallery.Name = "xtraTabControlGallery";
			this.xtraTabControlGallery.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
			this.xtraTabControlGallery.Size = new System.Drawing.Size(671, 491);
			this.xtraTabControlGallery.TabIndex = 46;
			// 
			// retractableBarGallery
			// 
			this.retractableBarGallery.BackColor = System.Drawing.Color.White;
			// 
			// retractableBarGallery.Content
			// 
			this.retractableBarGallery.Content.Controls.Add(this.treeViewGallery);
			this.retractableBarGallery.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarGallery.Content.ForeColor = System.Drawing.Color.Black;
			this.retractableBarGallery.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBarGallery.Content.Name = "Content";
			this.retractableBarGallery.Content.Size = new System.Drawing.Size(245, 447);
			this.retractableBarGallery.Content.TabIndex = 1;
			this.retractableBarGallery.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBarGallery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.retractableBarGallery.ForeColor = System.Drawing.Color.Black;
			// 
			// retractableBarGallery.Header
			// 
			this.retractableBarGallery.Header.Controls.Add(this.labelControlSelectedGalleryName);
			this.retractableBarGallery.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarGallery.Header.ForeColor = System.Drawing.Color.Black;
			this.retractableBarGallery.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBarGallery.Header.Name = "Header";
			this.retractableBarGallery.Header.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.retractableBarGallery.Header.Size = new System.Drawing.Size(194, 36);
			this.retractableBarGallery.Header.TabIndex = 2;
			this.retractableBarGallery.Location = new System.Drawing.Point(0, 0);
			this.retractableBarGallery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBarGallery.Name = "retractableBarGallery";
			this.retractableBarGallery.Size = new System.Drawing.Size(249, 491);
			this.retractableBarGallery.TabIndex = 47;
			// 
			// treeViewGallery
			// 
			this.treeViewGallery.BackColor = System.Drawing.Color.White;
			this.treeViewGallery.Cursor = System.Windows.Forms.Cursors.Hand;
			this.treeViewGallery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewGallery.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.treeViewGallery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeViewGallery.ForeColor = System.Drawing.Color.Black;
			this.treeViewGallery.HideSelection = false;
			this.treeViewGallery.Indent = 16;
			this.treeViewGallery.ItemHeight = 25;
			this.treeViewGallery.Location = new System.Drawing.Point(0, 0);
			this.treeViewGallery.Name = "treeViewGallery";
			this.treeViewGallery.ShowLines = false;
			this.treeViewGallery.Size = new System.Drawing.Size(245, 447);
			this.treeViewGallery.TabIndex = 2;
			// 
			// labelControlSelectedGalleryName
			// 
			this.labelControlSelectedGalleryName.AllowHtmlString = true;
			this.labelControlSelectedGalleryName.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlSelectedGalleryName.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlSelectedGalleryName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlSelectedGalleryName.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.labelControlSelectedGalleryName.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlSelectedGalleryName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlSelectedGalleryName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlSelectedGalleryName.Location = new System.Drawing.Point(10, 0);
			this.labelControlSelectedGalleryName.Name = "labelControlSelectedGalleryName";
			this.labelControlSelectedGalleryName.Size = new System.Drawing.Size(184, 36);
			this.labelControlSelectedGalleryName.StyleController = this.styleController;
			this.labelControlSelectedGalleryName.TabIndex = 54;
			this.labelControlSelectedGalleryName.UseMnemonic = false;
			// 
			// colorEditInversionColor
			// 
			this.colorEditInversionColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.colorEditInversionColor.Color = System.Drawing.Color.Empty;
			this.colorEditInversionColor.EditValue = System.Drawing.Color.Empty;
			this.colorEditInversionColor.Enabled = false;
			this.colorEditInversionColor.Location = new System.Drawing.Point(409, 504);
			this.colorEditInversionColor.Name = "colorEditInversionColor";
			this.colorEditInversionColor.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.colorEditInversionColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditInversionColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditInversionColor.Properties.Appearance.Options.UseForeColor = true;
			this.colorEditInversionColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.colorEditInversionColor.Properties.Color = System.Drawing.Color.Empty;
			this.colorEditInversionColor.Size = new System.Drawing.Size(88, 22);
			this.colorEditInversionColor.StyleController = this.styleController;
			this.colorEditInversionColor.TabIndex = 62;
			this.colorEditInversionColor.EditValueChanged += new System.EventHandler(this.colorEditInversionColor_EditValueChanged);
			// 
			// checkEditInvert
			// 
			this.checkEditInvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditInvert.Enabled = false;
			this.checkEditInvert.Location = new System.Drawing.Point(335, 505);
			this.checkEditInvert.Name = "checkEditInvert";
			this.checkEditInvert.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditInvert.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditInvert.Properties.Caption = "Colorize";
			this.checkEditInvert.Size = new System.Drawing.Size(73, 20);
			this.checkEditInvert.StyleController = this.styleController;
			this.checkEditInvert.TabIndex = 61;
			this.checkEditInvert.CheckedChanged += new System.EventHandler(this.OnWidgetTypeChanged);
			// 
			// contextMenuStripImage
			// 
			this.contextMenuStripImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemImageAddToFavorites});
			this.contextMenuStripImage.Name = "contextMenuStripImage";
			this.contextMenuStripImage.Size = new System.Drawing.Size(163, 26);
			this.contextMenuStripImage.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripImage_Opening);
			// 
			// toolStripMenuItemImageAddToFavorites
			// 
			this.toolStripMenuItemImageAddToFavorites.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.Favorites;
			this.toolStripMenuItemImageAddToFavorites.Name = "toolStripMenuItemImageAddToFavorites";
			this.toolStripMenuItemImageAddToFavorites.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItemImageAddToFavorites.Text = "Add To Favorites";
			this.toolStripMenuItemImageAddToFavorites.Click += new System.EventHandler(this.toolStripMenuItemImageAddToFavorites_Click);
			// 
			// WidgetSettingsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.colorEditInversionColor);
			this.Controls.Add(this.checkEditInvert);
			this.Controls.Add(this.pnGallery);
			this.Controls.Add(this.pnSearch);
			this.Controls.Add(this.radioButtonWidgetTypeDisabled);
			this.Controls.Add(this.radioButtonWidgetTypeCustom);
			this.Controls.Add(this.pbCustomWidget);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WidgetSettingsControl";
			this.Size = new System.Drawing.Size(920, 542);
			this.Click += new System.EventHandler(this.OnFormClick);
			((System.ComponentModel.ISupportInitialize)(this.pbCustomWidget)).EndInit();
			this.pnSearch.ResumeLayout(false);
			this.pnSearch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).EndInit();
			this.pnGallery.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGallery)).EndInit();
			this.retractableBarGallery.Content.ResumeLayout(false);
			this.retractableBarGallery.Header.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.colorEditInversionColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditInvert.Properties)).EndInit();
			this.contextMenuStripImage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.PictureBox pbCustomWidget;
		private System.Windows.Forms.RadioButton radioButtonWidgetTypeCustom;
		private System.Windows.Forms.RadioButton radioButtonWidgetTypeDisabled;
		private System.Windows.Forms.Panel pnSearch;
		private DevExpress.XtraEditors.LabelControl labelControlSearchTitle;
		private DevComponents.DotNetBar.ButtonX buttonXSearch;
		private DevExpress.XtraEditors.TextEdit textEditSearch;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnGallery;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlGallery;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBarGallery;
		private ImageGallery.GalleryTreeView treeViewGallery;
		private DevExpress.XtraEditors.LabelControl labelControlSelectedGalleryName;
		public CommonGUI.Common.HtmlColorEdit colorEditInversionColor;
		public DevExpress.XtraEditors.CheckEdit checkEditInvert;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemImageAddToFavorites;
	}
}