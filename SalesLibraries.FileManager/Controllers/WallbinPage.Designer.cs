namespace SalesLibraries.FileManager.Controllers
{
	sealed partial class WallbinPage
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
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.linkInfoControl = new SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.pnContentContainer = new System.Windows.Forms.Panel();
			this.retractableBar = new SalesLibraries.CommonGUI.RetractableBar.RetractableBarLeft();
			this.laEditorTitle = new System.Windows.Forms.Label();
			this.linkTagsInfoControl = new SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings.LinkTagsInfoControl();
			this.pnTagInfoContainer = new System.Windows.Forms.Panel();
			this.comboBoxEditPages = new DevExpress.XtraEditors.ComboBoxEdit();
			this.superFilterControl = new SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings.SuperFilterControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemSuperFilter = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemPages = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemTagInfoContainer = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemLinkInfoControl = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemContentContainer = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemLinksTagInfo = new DevExpress.XtraLayout.LayoutControlItem();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			this.pnContentContainer.SuspendLayout();
			this.retractableBar.Header.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPages.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.superFilterControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSuperFilter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTagInfoContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLinkInfoControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContentContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLinksTagInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// pnContainer
			// 
			this.pnContainer.BackColor = System.Drawing.Color.White;
			this.pnContainer.Location = new System.Drawing.Point(33, 49);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(199, 158);
			this.pnContainer.TabIndex = 0;
			// 
			// pnEmpty
			// 
			this.pnEmpty.BackColor = System.Drawing.Color.White;
			this.pnEmpty.Location = new System.Drawing.Point(33, 259);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(199, 141);
			this.pnEmpty.TabIndex = 1;
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pnContainer);
			this.pnMain.Controls.Add(this.pnEmpty);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(350, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(634, 590);
			this.pnMain.TabIndex = 2;
			// 
			// linkInfoControl
			// 
			this.linkInfoControl.AllowHtmlString = true;
			this.linkInfoControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkInfoControl.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
			this.linkInfoControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.linkInfoControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.linkInfoControl.Location = new System.Drawing.Point(205, 24);
			this.linkInfoControl.Name = "linkInfoControl";
			this.linkInfoControl.Size = new System.Drawing.Size(211, 1);
			this.linkInfoControl.StyleController = this.layoutControl;
			this.linkInfoControl.TabIndex = 2;
			// 
			// layoutControl
			// 
			this.layoutControl.AllowCustomization = false;
			this.layoutControl.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControl.Appearance.Control.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDisabled.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDown.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDownHeader.Options.UseFont = true;
			this.layoutControl.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlFocused.Options.UseFont = true;
			this.layoutControl.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlReadOnly.Options.UseFont = true;
			this.layoutControl.Controls.Add(this.pnContentContainer);
			this.layoutControl.Controls.Add(this.linkTagsInfoControl);
			this.layoutControl.Controls.Add(this.linkInfoControl);
			this.layoutControl.Controls.Add(this.pnTagInfoContainer);
			this.layoutControl.Controls.Add(this.comboBoxEditPages);
			this.layoutControl.Controls.Add(this.superFilterControl);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(988, 694);
			this.layoutControl.StyleController = this.styleController;
			this.layoutControl.TabIndex = 63;
			this.layoutControl.Text = "layoutControl1";
			// 
			// pnContentContainer
			// 
			this.pnContentContainer.Controls.Add(this.pnMain);
			this.pnContentContainer.Controls.Add(this.retractableBar);
			this.pnContentContainer.Location = new System.Drawing.Point(2, 52);
			this.pnContentContainer.Name = "pnContentContainer";
			this.pnContentContainer.Size = new System.Drawing.Size(984, 590);
			this.pnContentContainer.TabIndex = 64;
			// 
			// retractableBar
			// 
			this.retractableBar.AnimationDelay = 0;
			this.retractableBar.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBar.Content
			// 
			this.retractableBar.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBar.Content.Name = "Content";
			this.retractableBar.Content.Size = new System.Drawing.Size(346, 546);
			this.retractableBar.Content.TabIndex = 1;
			this.retractableBar.ContentSize = 350;
			this.retractableBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBar.Header
			// 
			this.retractableBar.Header.Controls.Add(this.laEditorTitle);
			this.retractableBar.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBar.Header.Name = "Header";
			this.retractableBar.Header.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.retractableBar.Header.Size = new System.Drawing.Size(295, 36);
			this.retractableBar.Header.TabIndex = 2;
			this.retractableBar.Location = new System.Drawing.Point(0, 0);
			this.retractableBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBar.Name = "retractableBar";
			this.retractableBar.Size = new System.Drawing.Size(350, 590);
			this.retractableBar.TabIndex = 7;
			// 
			// laEditorTitle
			// 
			this.laEditorTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laEditorTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laEditorTitle.Location = new System.Drawing.Point(10, 0);
			this.laEditorTitle.Name = "laEditorTitle";
			this.laEditorTitle.Size = new System.Drawing.Size(285, 36);
			this.laEditorTitle.TabIndex = 1;
			this.laEditorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// linkTagsInfoControl
			// 
			this.linkTagsInfoControl.Location = new System.Drawing.Point(2, 646);
			this.linkTagsInfoControl.Name = "linkTagsInfoControl";
			this.linkTagsInfoControl.Size = new System.Drawing.Size(984, 46);
			this.linkTagsInfoControl.TabIndex = 9;
			this.linkTagsInfoControl.Visible = false;
			// 
			// pnTagInfoContainer
			// 
			this.pnTagInfoContainer.Location = new System.Drawing.Point(420, 2);
			this.pnTagInfoContainer.Name = "pnTagInfoContainer";
			this.pnTagInfoContainer.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.pnTagInfoContainer.Size = new System.Drawing.Size(366, 46);
			this.pnTagInfoContainer.TabIndex = 1;
			// 
			// comboBoxEditPages
			// 
			this.comboBoxEditPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditPages.Location = new System.Drawing.Point(808, 14);
			this.comboBoxEditPages.Name = "comboBoxEditPages";
			this.comboBoxEditPages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditPages.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditPages.Size = new System.Drawing.Size(175, 22);
			this.comboBoxEditPages.StyleController = this.layoutControl;
			this.comboBoxEditPages.TabIndex = 2;
			// 
			// superFilterControl
			// 
			this.superFilterControl.Appearance.BackColor = System.Drawing.Color.White;
			this.superFilterControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superFilterControl.Appearance.Options.UseBackColor = true;
			this.superFilterControl.Appearance.Options.UseFont = true;
			this.superFilterControl.CheckOnClick = true;
			this.superFilterControl.ItemHeight = 23;
			this.superFilterControl.Location = new System.Drawing.Point(2, 2);
			this.superFilterControl.MultiColumn = true;
			this.superFilterControl.Name = "superFilterControl";
			this.superFilterControl.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.superFilterControl.Size = new System.Drawing.Size(196, 46);
			this.superFilterControl.StyleController = this.layoutControl;
			this.superFilterControl.TabIndex = 0;
			// 
			// layoutControlGroupRoot
			// 
			this.layoutControlGroupRoot.AllowHtmlStringInCaption = true;
			this.layoutControlGroupRoot.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRoot.GroupBordersVisible = false;
			this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSuperFilter,
            this.layoutControlItemPages,
            this.layoutControlItemTagInfoContainer,
            this.layoutControlItemLinkInfoControl,
            this.layoutControlItemContentContainer,
            this.layoutControlItemLinksTagInfo});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(988, 694);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemSuperFilter
			// 
			this.layoutControlItemSuperFilter.Control = this.superFilterControl;
			this.layoutControlItemSuperFilter.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemSuperFilter.FillControlToClientArea = false;
			this.layoutControlItemSuperFilter.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemSuperFilter.MaxSize = new System.Drawing.Size(200, 50);
			this.layoutControlItemSuperFilter.MinSize = new System.Drawing.Size(200, 50);
			this.layoutControlItemSuperFilter.Name = "layoutControlItemSuperFilter";
			this.layoutControlItemSuperFilter.Size = new System.Drawing.Size(200, 50);
			this.layoutControlItemSuperFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemSuperFilter.Text = "Super Filter";
			this.layoutControlItemSuperFilter.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemSuperFilter.TextVisible = false;
			this.layoutControlItemSuperFilter.TrimClientAreaToControl = false;
			// 
			// layoutControlItemPages
			// 
			this.layoutControlItemPages.Control = this.comboBoxEditPages;
			this.layoutControlItemPages.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemPages.FillControlToClientArea = false;
			this.layoutControlItemPages.Location = new System.Drawing.Point(788, 0);
			this.layoutControlItemPages.MaxSize = new System.Drawing.Size(200, 50);
			this.layoutControlItemPages.MinSize = new System.Drawing.Size(200, 50);
			this.layoutControlItemPages.Name = "layoutControlItemPages";
			this.layoutControlItemPages.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 5, 2, 2);
			this.layoutControlItemPages.Size = new System.Drawing.Size(200, 50);
			this.layoutControlItemPages.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemPages.Text = "Pages";
			this.layoutControlItemPages.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemPages.TextVisible = false;
			this.layoutControlItemPages.TrimClientAreaToControl = false;
			// 
			// layoutControlItemTagInfoContainer
			// 
			this.layoutControlItemTagInfoContainer.Control = this.pnTagInfoContainer;
			this.layoutControlItemTagInfoContainer.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemTagInfoContainer.FillControlToClientArea = false;
			this.layoutControlItemTagInfoContainer.Location = new System.Drawing.Point(418, 0);
			this.layoutControlItemTagInfoContainer.MaxSize = new System.Drawing.Size(370, 50);
			this.layoutControlItemTagInfoContainer.MinSize = new System.Drawing.Size(370, 50);
			this.layoutControlItemTagInfoContainer.Name = "layoutControlItemTagInfoContainer";
			this.layoutControlItemTagInfoContainer.Size = new System.Drawing.Size(370, 50);
			this.layoutControlItemTagInfoContainer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemTagInfoContainer.Text = "Tag Info Container";
			this.layoutControlItemTagInfoContainer.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTagInfoContainer.TextVisible = false;
			this.layoutControlItemTagInfoContainer.TrimClientAreaToControl = false;
			// 
			// layoutControlItemLinkInfoControl
			// 
			this.layoutControlItemLinkInfoControl.Control = this.linkInfoControl;
			this.layoutControlItemLinkInfoControl.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemLinkInfoControl.FillControlToClientArea = false;
			this.layoutControlItemLinkInfoControl.Location = new System.Drawing.Point(200, 0);
			this.layoutControlItemLinkInfoControl.Name = "layoutControlItemLinkInfoControl";
			this.layoutControlItemLinkInfoControl.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
			this.layoutControlItemLinkInfoControl.Size = new System.Drawing.Size(218, 50);
			this.layoutControlItemLinkInfoControl.Text = "Link Info Control";
			this.layoutControlItemLinkInfoControl.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLinkInfoControl.TextVisible = false;
			this.layoutControlItemLinkInfoControl.TrimClientAreaToControl = false;
			// 
			// layoutControlItemContentContainer
			// 
			this.layoutControlItemContentContainer.Control = this.pnContentContainer;
			this.layoutControlItemContentContainer.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemContentContainer.FillControlToClientArea = false;
			this.layoutControlItemContentContainer.Location = new System.Drawing.Point(0, 50);
			this.layoutControlItemContentContainer.Name = "layoutControlItemContentContainer";
			this.layoutControlItemContentContainer.Size = new System.Drawing.Size(988, 594);
			this.layoutControlItemContentContainer.Text = "Content Container";
			this.layoutControlItemContentContainer.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemContentContainer.TextVisible = false;
			this.layoutControlItemContentContainer.TrimClientAreaToControl = false;
			// 
			// layoutControlItemLinksTagInfo
			// 
			this.layoutControlItemLinksTagInfo.Control = this.linkTagsInfoControl;
			this.layoutControlItemLinksTagInfo.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemLinksTagInfo.FillControlToClientArea = false;
			this.layoutControlItemLinksTagInfo.Location = new System.Drawing.Point(0, 644);
			this.layoutControlItemLinksTagInfo.MaxSize = new System.Drawing.Size(0, 50);
			this.layoutControlItemLinksTagInfo.MinSize = new System.Drawing.Size(104, 50);
			this.layoutControlItemLinksTagInfo.Name = "layoutControlItemLinksTagInfo";
			this.layoutControlItemLinksTagInfo.Size = new System.Drawing.Size(988, 50);
			this.layoutControlItemLinksTagInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLinksTagInfo.Text = "Link Tags Info";
			this.layoutControlItemLinksTagInfo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLinksTagInfo.TextVisible = false;
			this.layoutControlItemLinksTagInfo.TrimClientAreaToControl = false;
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
			// WallbinPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WallbinPage";
			this.Size = new System.Drawing.Size(988, 694);
			this.pnMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			this.pnContentContainer.ResumeLayout(false);
			this.retractableBar.Header.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPages.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.superFilterControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSuperFilter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTagInfoContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLinkInfoControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContentContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLinksTagInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnContainer;
		private System.Windows.Forms.Panel pnEmpty;
		private System.Windows.Forms.Panel pnMain;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBar;
		private System.Windows.Forms.Label laEditorTitle;
		private PresentationLayer.Wallbin.Links.SingleSettings.SuperFilterControl superFilterControl;
		private PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl linkInfoControl;
		private DevExpress.XtraEditors.StyleController styleController;
		private PresentationLayer.Wallbin.Links.SingleSettings.LinkTagsInfoControl linkTagsInfoControl;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		public System.Windows.Forms.Panel pnTagInfoContainer;
		public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPages;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSuperFilter;
		public DevExpress.XtraLayout.LayoutControlItem layoutControlItemPages;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTagInfoContainer;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLinkInfoControl;
		private System.Windows.Forms.Panel pnContentContainer;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemContentContainer;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLinksTagInfo;
	}
}
