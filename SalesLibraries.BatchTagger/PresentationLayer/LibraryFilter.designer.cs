namespace SalesLibraries.BatchTagger.PresentationLayer
{
	partial class LibraryFilter
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
			this.checkEditEnableFilter = new DevExpress.XtraEditors.CheckEdit();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.checkEditNoKeywordLinks = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditUntaggedLinks = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditAllFiles = new DevExpress.XtraEditors.CheckEdit();
			this.checkedListBoxControlGroups = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.buttonXGroupsNone = new DevComponents.DotNetBar.ButtonX();
			this.buttonXGroupsAll = new DevComponents.DotNetBar.ButtonX();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemEnableFilter = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroupControls = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroupToggles = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemAllFiles = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemUntaggedLinks = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemNoKeywordLinks = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.simpleLabelItemLibraries = new DevExpress.XtraLayout.SimpleLabelItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemGroupsAll = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemGroupsNone = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemGroupList = new DevExpress.XtraLayout.LayoutControlItem();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditNoKeywordLinks.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditUntaggedLinks.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAllFiles.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEnableFilter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupControls)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupToggles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUntaggedLinks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNoKeywordLinks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGroupsAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGroupsNone)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGroupList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// checkEditEnableFilter
			// 
			this.checkEditEnableFilter.Location = new System.Drawing.Point(12, 12);
			this.checkEditEnableFilter.Name = "checkEditEnableFilter";
			this.checkEditEnableFilter.Properties.Caption = "Enable Filter";
			this.checkEditEnableFilter.Size = new System.Drawing.Size(214, 20);
			this.checkEditEnableFilter.StyleController = this.layoutControl;
			this.checkEditEnableFilter.TabIndex = 0;
			this.checkEditEnableFilter.CheckedChanged += new System.EventHandler(this.checkEditFilterEnable_CheckedChanged);
			// 
			// layoutControl
			// 
			this.layoutControl.AllowCustomization = false;
			this.layoutControl.Controls.Add(this.checkEditNoKeywordLinks);
			this.layoutControl.Controls.Add(this.checkEditUntaggedLinks);
			this.layoutControl.Controls.Add(this.checkEditAllFiles);
			this.layoutControl.Controls.Add(this.checkedListBoxControlGroups);
			this.layoutControl.Controls.Add(this.buttonXGroupsNone);
			this.layoutControl.Controls.Add(this.checkEditEnableFilter);
			this.layoutControl.Controls.Add(this.buttonXGroupsAll);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(273, 185, 450, 400);
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(238, 429);
			this.layoutControl.StyleController = this.styleController;
			this.layoutControl.TabIndex = 22;
			this.layoutControl.Text = "layoutControl1";
			// 
			// checkEditNoKeywordLinks
			// 
			this.checkEditNoKeywordLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditNoKeywordLinks.Location = new System.Drawing.Point(12, 397);
			this.checkEditNoKeywordLinks.Name = "checkEditNoKeywordLinks";
			this.checkEditNoKeywordLinks.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditNoKeywordLinks.Properties.Caption = "No Keyword Links";
			this.checkEditNoKeywordLinks.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditNoKeywordLinks.Properties.RadioGroupIndex = 1;
			this.checkEditNoKeywordLinks.Size = new System.Drawing.Size(214, 20);
			this.checkEditNoKeywordLinks.StyleController = this.layoutControl;
			this.checkEditNoKeywordLinks.TabIndex = 21;
			this.checkEditNoKeywordLinks.TabStop = false;
			this.checkEditNoKeywordLinks.CheckedChanged += new System.EventHandler(this.checkEditLinkTagFilter_CheckedChanged);
			// 
			// checkEditUntaggedLinks
			// 
			this.checkEditUntaggedLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditUntaggedLinks.Location = new System.Drawing.Point(12, 363);
			this.checkEditUntaggedLinks.Name = "checkEditUntaggedLinks";
			this.checkEditUntaggedLinks.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditUntaggedLinks.Properties.Caption = "Un-Tagged Links";
			this.checkEditUntaggedLinks.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditUntaggedLinks.Properties.RadioGroupIndex = 1;
			this.checkEditUntaggedLinks.Size = new System.Drawing.Size(214, 20);
			this.checkEditUntaggedLinks.StyleController = this.layoutControl;
			this.checkEditUntaggedLinks.TabIndex = 20;
			this.checkEditUntaggedLinks.TabStop = false;
			this.checkEditUntaggedLinks.CheckedChanged += new System.EventHandler(this.checkEditLinkTagFilter_CheckedChanged);
			// 
			// checkEditAllFiles
			// 
			this.checkEditAllFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditAllFiles.EditValue = true;
			this.checkEditAllFiles.Location = new System.Drawing.Point(12, 329);
			this.checkEditAllFiles.Name = "checkEditAllFiles";
			this.checkEditAllFiles.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditAllFiles.Properties.Caption = "All Links";
			this.checkEditAllFiles.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditAllFiles.Properties.RadioGroupIndex = 1;
			this.checkEditAllFiles.Size = new System.Drawing.Size(214, 20);
			this.checkEditAllFiles.StyleController = this.layoutControl;
			this.checkEditAllFiles.TabIndex = 19;
			this.checkEditAllFiles.CheckedChanged += new System.EventHandler(this.checkEditLinkTagFilter_CheckedChanged);
			// 
			// checkedListBoxControlGroups
			// 
			this.checkedListBoxControlGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxControlGroups.CheckOnClick = true;
			this.checkedListBoxControlGroups.Cursor = System.Windows.Forms.Cursors.Default;
			this.checkedListBoxControlGroups.ItemHeight = 35;
			this.checkedListBoxControlGroups.Location = new System.Drawing.Point(12, 126);
			this.checkedListBoxControlGroups.Name = "checkedListBoxControlGroups";
			this.checkedListBoxControlGroups.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControlGroups.Size = new System.Drawing.Size(214, 189);
			this.checkedListBoxControlGroups.StyleController = this.layoutControl;
			this.checkedListBoxControlGroups.TabIndex = 1;
			this.checkedListBoxControlGroups.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlGroups_ItemCheck);
			// 
			// buttonXGroupsNone
			// 
			this.buttonXGroupsNone.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroupsNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXGroupsNone.CausesValidation = false;
			this.buttonXGroupsNone.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroupsNone.Location = new System.Drawing.Point(130, 76);
			this.buttonXGroupsNone.Name = "buttonXGroupsNone";
			this.buttonXGroupsNone.Size = new System.Drawing.Size(96, 36);
			this.buttonXGroupsNone.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroupsNone.TabIndex = 18;
			this.buttonXGroupsNone.Text = "Clear All";
			this.buttonXGroupsNone.TextColor = System.Drawing.Color.Black;
			this.buttonXGroupsNone.Click += new System.EventHandler(this.buttonXGroupsNone_Click);
			// 
			// buttonXGroupsAll
			// 
			this.buttonXGroupsAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroupsAll.CausesValidation = false;
			this.buttonXGroupsAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroupsAll.Location = new System.Drawing.Point(12, 76);
			this.buttonXGroupsAll.Name = "buttonXGroupsAll";
			this.buttonXGroupsAll.Size = new System.Drawing.Size(96, 36);
			this.buttonXGroupsAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroupsAll.TabIndex = 17;
			this.buttonXGroupsAll.Text = "Select All";
			this.buttonXGroupsAll.TextColor = System.Drawing.Color.Black;
			this.buttonXGroupsAll.Click += new System.EventHandler(this.buttonXGroupsAll_Click);
			// 
			// layoutControlGroupRoot
			// 
			this.layoutControlGroupRoot.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControlGroupRoot.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.PageClient.Options.UseFont = true;
			this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRoot.GroupBordersVisible = false;
			this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemEnableFilter,
            this.layoutControlGroupControls});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(238, 429);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemEnableFilter
			// 
			this.layoutControlItemEnableFilter.Control = this.checkEditEnableFilter;
			this.layoutControlItemEnableFilter.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemEnableFilter.Name = "layoutControlItemEnableFilter";
			this.layoutControlItemEnableFilter.Size = new System.Drawing.Size(218, 24);
			this.layoutControlItemEnableFilter.Text = "Enable Filter";
			this.layoutControlItemEnableFilter.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemEnableFilter.TextVisible = false;
			// 
			// layoutControlGroupControls
			// 
			this.layoutControlGroupControls.Enabled = false;
			this.layoutControlGroupControls.GroupBordersVisible = false;
			this.layoutControlGroupControls.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupToggles,
            this.emptySpaceItem1,
            this.simpleLabelItemLibraries,
            this.emptySpaceItem2,
            this.layoutControlItemGroupsAll,
            this.emptySpaceItem3,
            this.layoutControlItemGroupsNone,
            this.emptySpaceItem4,
            this.layoutControlItemGroupList});
			this.layoutControlGroupControls.Location = new System.Drawing.Point(0, 24);
			this.layoutControlGroupControls.Name = "layoutControlGroupControls";
			this.layoutControlGroupControls.Size = new System.Drawing.Size(218, 385);
			this.layoutControlGroupControls.Text = "Controls";
			this.layoutControlGroupControls.TextVisible = false;
			// 
			// layoutControlGroupToggles
			// 
			this.layoutControlGroupToggles.GroupBordersVisible = false;
			this.layoutControlGroupToggles.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem5,
            this.layoutControlItemAllFiles,
            this.emptySpaceItem6,
            this.layoutControlItemUntaggedLinks,
            this.emptySpaceItem7,
            this.layoutControlItemNoKeywordLinks});
			this.layoutControlGroupToggles.Location = new System.Drawing.Point(0, 283);
			this.layoutControlGroupToggles.Name = "layoutControlGroupToggles";
			this.layoutControlGroupToggles.Size = new System.Drawing.Size(218, 102);
			this.layoutControlGroupToggles.Text = "Toggles";
			this.layoutControlGroupToggles.TextVisible = false;
			this.layoutControlGroupToggles.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// emptySpaceItem5
			// 
			this.emptySpaceItem5.AllowHotTrack = false;
			this.emptySpaceItem5.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem5.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem5.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem5.Name = "emptySpaceItem5";
			this.emptySpaceItem5.Size = new System.Drawing.Size(218, 10);
			this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemAllFiles
			// 
			this.layoutControlItemAllFiles.Control = this.checkEditAllFiles;
			this.layoutControlItemAllFiles.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemAllFiles.FillControlToClientArea = false;
			this.layoutControlItemAllFiles.Location = new System.Drawing.Point(0, 10);
			this.layoutControlItemAllFiles.Name = "layoutControlItemAllFiles";
			this.layoutControlItemAllFiles.Size = new System.Drawing.Size(218, 24);
			this.layoutControlItemAllFiles.Text = "All Files";
			this.layoutControlItemAllFiles.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemAllFiles.TextVisible = false;
			this.layoutControlItemAllFiles.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem6
			// 
			this.emptySpaceItem6.AllowHotTrack = false;
			this.emptySpaceItem6.Location = new System.Drawing.Point(0, 34);
			this.emptySpaceItem6.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem6.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem6.Name = "emptySpaceItem6";
			this.emptySpaceItem6.Size = new System.Drawing.Size(218, 10);
			this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemUntaggedLinks
			// 
			this.layoutControlItemUntaggedLinks.Control = this.checkEditUntaggedLinks;
			this.layoutControlItemUntaggedLinks.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemUntaggedLinks.FillControlToClientArea = false;
			this.layoutControlItemUntaggedLinks.Location = new System.Drawing.Point(0, 44);
			this.layoutControlItemUntaggedLinks.Name = "layoutControlItemUntaggedLinks";
			this.layoutControlItemUntaggedLinks.Size = new System.Drawing.Size(218, 24);
			this.layoutControlItemUntaggedLinks.Text = "Untagged Links";
			this.layoutControlItemUntaggedLinks.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemUntaggedLinks.TextVisible = false;
			this.layoutControlItemUntaggedLinks.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem7
			// 
			this.emptySpaceItem7.AllowHotTrack = false;
			this.emptySpaceItem7.Location = new System.Drawing.Point(0, 68);
			this.emptySpaceItem7.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem7.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem7.Name = "emptySpaceItem7";
			this.emptySpaceItem7.Size = new System.Drawing.Size(218, 10);
			this.emptySpaceItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemNoKeywordLinks
			// 
			this.layoutControlItemNoKeywordLinks.Control = this.checkEditNoKeywordLinks;
			this.layoutControlItemNoKeywordLinks.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemNoKeywordLinks.FillControlToClientArea = false;
			this.layoutControlItemNoKeywordLinks.Location = new System.Drawing.Point(0, 78);
			this.layoutControlItemNoKeywordLinks.Name = "layoutControlItemNoKeywordLinks";
			this.layoutControlItemNoKeywordLinks.Size = new System.Drawing.Size(218, 24);
			this.layoutControlItemNoKeywordLinks.Text = "No Keyword Links";
			this.layoutControlItemNoKeywordLinks.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemNoKeywordLinks.TextVisible = false;
			this.layoutControlItemNoKeywordLinks.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(218, 10);
			this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// simpleLabelItemLibraries
			// 
			this.simpleLabelItemLibraries.AllowHotTrack = false;
			this.simpleLabelItemLibraries.Location = new System.Drawing.Point(0, 10);
			this.simpleLabelItemLibraries.Name = "simpleLabelItemLibraries";
			this.simpleLabelItemLibraries.Size = new System.Drawing.Size(218, 20);
			this.simpleLabelItemLibraries.Text = "Libraries:";
			this.simpleLabelItemLibraries.TextSize = new System.Drawing.Size(53, 16);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 30);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(218, 10);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemGroupsAll
			// 
			this.layoutControlItemGroupsAll.Control = this.buttonXGroupsAll;
			this.layoutControlItemGroupsAll.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemGroupsAll.FillControlToClientArea = false;
			this.layoutControlItemGroupsAll.Location = new System.Drawing.Point(0, 40);
			this.layoutControlItemGroupsAll.MaxSize = new System.Drawing.Size(100, 40);
			this.layoutControlItemGroupsAll.MinSize = new System.Drawing.Size(100, 40);
			this.layoutControlItemGroupsAll.Name = "layoutControlItemGroupsAll";
			this.layoutControlItemGroupsAll.Size = new System.Drawing.Size(100, 40);
			this.layoutControlItemGroupsAll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemGroupsAll.Text = "Groups All";
			this.layoutControlItemGroupsAll.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemGroupsAll.TextVisible = false;
			this.layoutControlItemGroupsAll.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(100, 40);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(18, 40);
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemGroupsNone
			// 
			this.layoutControlItemGroupsNone.Control = this.buttonXGroupsNone;
			this.layoutControlItemGroupsNone.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemGroupsNone.FillControlToClientArea = false;
			this.layoutControlItemGroupsNone.Location = new System.Drawing.Point(118, 40);
			this.layoutControlItemGroupsNone.MaxSize = new System.Drawing.Size(100, 40);
			this.layoutControlItemGroupsNone.MinSize = new System.Drawing.Size(100, 40);
			this.layoutControlItemGroupsNone.Name = "layoutControlItemGroupsNone";
			this.layoutControlItemGroupsNone.Size = new System.Drawing.Size(100, 40);
			this.layoutControlItemGroupsNone.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemGroupsNone.Text = "Groups None";
			this.layoutControlItemGroupsNone.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemGroupsNone.TextVisible = false;
			this.layoutControlItemGroupsNone.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.Location = new System.Drawing.Point(0, 80);
			this.emptySpaceItem4.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem4.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(218, 10);
			this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemGroupList
			// 
			this.layoutControlItemGroupList.Control = this.checkedListBoxControlGroups;
			this.layoutControlItemGroupList.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemGroupList.FillControlToClientArea = false;
			this.layoutControlItemGroupList.Location = new System.Drawing.Point(0, 90);
			this.layoutControlItemGroupList.Name = "layoutControlItemGroupList";
			this.layoutControlItemGroupList.Size = new System.Drawing.Size(218, 193);
			this.layoutControlItemGroupList.Text = "Group List";
			this.layoutControlItemGroupList.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemGroupList.TextVisible = false;
			this.layoutControlItemGroupList.TrimClientAreaToControl = false;
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
			// LibraryFilter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LibraryFilter";
			this.Size = new System.Drawing.Size(238, 429);
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditNoKeywordLinks.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditUntaggedLinks.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAllFiles.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEnableFilter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupControls)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupToggles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUntaggedLinks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNoKeywordLinks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGroupsAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGroupsNone)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGroupList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.CheckEdit checkEditEnableFilter;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlGroups;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsAll;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsNone;
		private DevExpress.XtraEditors.CheckEdit checkEditAllFiles;
		private DevExpress.XtraEditors.CheckEdit checkEditUntaggedLinks;
		private DevExpress.XtraEditors.CheckEdit checkEditNoKeywordLinks;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemEnableFilter;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemLibraries;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGroupsAll;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGroupsNone;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGroupList;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAllFiles;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUntaggedLinks;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNoKeywordLinks;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupToggles;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupControls;
	}
}
