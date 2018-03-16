namespace SalesLibraries.BatchTagger
{
	partial class FormMain
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
			this.pnCustomFilter = new System.Windows.Forms.Panel();
			this.buttonXLoadData = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabControlLibraries = new DevExpress.XtraTab.XtraTabControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemLoadData = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemFilterContainer = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem = new DevExpress.XtraLayout.SplitterItem();
			this.layoutControlItemLibraries = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroupFilter = new DevExpress.XtraLayout.LayoutControlGroup();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLoadData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFilterContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilter)).BeginInit();
			this.SuspendLayout();
			// 
			// pnCustomFilter
			// 
			this.pnCustomFilter.BackColor = System.Drawing.Color.Transparent;
			this.pnCustomFilter.ForeColor = System.Drawing.Color.Black;
			this.pnCustomFilter.Location = new System.Drawing.Point(2, 52);
			this.pnCustomFilter.Name = "pnCustomFilter";
			this.pnCustomFilter.Size = new System.Drawing.Size(320, 451);
			this.pnCustomFilter.TabIndex = 18;
			// 
			// buttonXLoadData
			// 
			this.buttonXLoadData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLoadData.CausesValidation = false;
			this.buttonXLoadData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLoadData.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXLoadData.Location = new System.Drawing.Point(2, 2);
			this.buttonXLoadData.Name = "buttonXLoadData";
			this.buttonXLoadData.Size = new System.Drawing.Size(320, 36);
			this.buttonXLoadData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLoadData.TabIndex = 16;
			this.buttonXLoadData.Text = "Load Data";
			this.buttonXLoadData.TextColor = System.Drawing.Color.Black;
			this.buttonXLoadData.Click += new System.EventHandler(this.buttonXLoadData_Click);
			// 
			// xtraTabControlLibraries
			// 
			this.xtraTabControlLibraries.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.xtraTabControlLibraries.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlLibraries.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlLibraries.Appearance.Options.UseBackColor = true;
			this.xtraTabControlLibraries.Appearance.Options.UseFont = true;
			this.xtraTabControlLibraries.Appearance.Options.UseForeColor = true;
			this.xtraTabControlLibraries.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlLibraries.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlLibraries.Location = new System.Drawing.Point(338, 2);
			this.xtraTabControlLibraries.Name = "xtraTabControlLibraries";
			this.xtraTabControlLibraries.Size = new System.Drawing.Size(530, 501);
			this.xtraTabControlLibraries.TabIndex = 3;
			this.xtraTabControlLibraries.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.OnSelectedPageChanged);
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
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013";
			// 
			// layoutControl
			// 
			this.layoutControl.AllowCustomization = false;
			this.layoutControl.Controls.Add(this.xtraTabControlLibraries);
			this.layoutControl.Controls.Add(this.pnCustomFilter);
			this.layoutControl.Controls.Add(this.buttonXLoadData);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(652, 245, 450, 400);
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(870, 505);
			this.layoutControl.StyleController = this.styleController;
			this.layoutControl.TabIndex = 19;
			this.layoutControl.Text = "layoutControl1";
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
            this.splitterItem,
            this.layoutControlItemLibraries,
            this.layoutControlGroupFilter});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(870, 505);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 40);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(324, 10);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemLoadData
			// 
			this.layoutControlItemLoadData.Control = this.buttonXLoadData;
			this.layoutControlItemLoadData.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemLoadData.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemLoadData.MaxSize = new System.Drawing.Size(0, 40);
			this.layoutControlItemLoadData.MinSize = new System.Drawing.Size(10, 40);
			this.layoutControlItemLoadData.Name = "layoutControlItemLoadData";
			this.layoutControlItemLoadData.Size = new System.Drawing.Size(324, 40);
			this.layoutControlItemLoadData.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLoadData.Text = "Load Data";
			this.layoutControlItemLoadData.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLoadData.TextVisible = false;
			// 
			// layoutControlItemFilterContainer
			// 
			this.layoutControlItemFilterContainer.Control = this.pnCustomFilter;
			this.layoutControlItemFilterContainer.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemFilterContainer.FillControlToClientArea = false;
			this.layoutControlItemFilterContainer.Location = new System.Drawing.Point(0, 50);
			this.layoutControlItemFilterContainer.MinSize = new System.Drawing.Size(300, 10);
			this.layoutControlItemFilterContainer.Name = "layoutControlItemFilterContainer";
			this.layoutControlItemFilterContainer.Size = new System.Drawing.Size(324, 455);
			this.layoutControlItemFilterContainer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemFilterContainer.Text = "Filter Container";
			this.layoutControlItemFilterContainer.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemFilterContainer.TextVisible = false;
			this.layoutControlItemFilterContainer.TrimClientAreaToControl = false;
			// 
			// splitterItem
			// 
			this.splitterItem.AllowHotTrack = true;
			this.splitterItem.FixedStyle = DevExpress.XtraLayout.SplitterItemFixedStyles.LeftTop;
			this.splitterItem.Location = new System.Drawing.Point(324, 0);
			this.splitterItem.Name = "splitterItem";
			this.splitterItem.Size = new System.Drawing.Size(12, 505);
			// 
			// layoutControlItemLibraries
			// 
			this.layoutControlItemLibraries.Control = this.xtraTabControlLibraries;
			this.layoutControlItemLibraries.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemLibraries.FillControlToClientArea = false;
			this.layoutControlItemLibraries.Location = new System.Drawing.Point(336, 0);
			this.layoutControlItemLibraries.MinSize = new System.Drawing.Size(104, 24);
			this.layoutControlItemLibraries.Name = "layoutControlItemLibraries";
			this.layoutControlItemLibraries.Size = new System.Drawing.Size(534, 505);
			this.layoutControlItemLibraries.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLibraries.Text = "Libraries";
			this.layoutControlItemLibraries.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLibraries.TextVisible = false;
			this.layoutControlItemLibraries.TrimClientAreaToControl = false;
			// 
			// layoutControlGroupFilter
			// 
			this.layoutControlGroupFilter.GroupBordersVisible = false;
			this.layoutControlGroupFilter.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLoadData,
            this.emptySpaceItem2,
            this.layoutControlItemFilterContainer});
			this.layoutControlGroupFilter.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupFilter.Name = "layoutControlGroupFilter";
			this.layoutControlGroupFilter.Size = new System.Drawing.Size(324, 505);
			this.layoutControlGroupFilter.Text = "Filter";
			this.layoutControlGroupFilter.TextVisible = false;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(870, 505);
			this.Controls.Add(this.layoutControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Batch Tagger";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Shown += new System.EventHandler(this.OnFormShown);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLoadData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFilterContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilter)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel pnCustomFilter;
		private DevComponents.DotNetBar.ButtonX buttonXLoadData;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlLibraries;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLoadData;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFilterContainer;
		private DevExpress.XtraLayout.SplitterItem splitterItem;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLibraries;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFilter;
	}
}

