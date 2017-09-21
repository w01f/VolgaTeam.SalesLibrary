namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	sealed partial class CategoriesEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoriesEditor));
			this.treeListCategories = new SalesLibraries.CommonGUI.CustomTreeList.EmptyImageTreeList();
			this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.buttonXCollapse = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExpand = new DevComponents.DotNetBar.ButtonX();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemCategories = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemReset = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemExpand = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCollapse = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.treeListCategories)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCategories)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExpand)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCollapse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			this.SuspendLayout();
			// 
			// treeListCategories
			// 
			this.treeListCategories.AllowCheckMinLevel = 0;
			this.treeListCategories.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListCategories.Appearance.FocusedCell.Options.UseFont = true;
			this.treeListCategories.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListCategories.Appearance.FocusedRow.Options.UseFont = true;
			this.treeListCategories.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeListCategories.Appearance.Row.Options.UseFont = true;
			this.treeListCategories.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListCategories.Appearance.SelectedRow.Options.UseFont = true;
			this.treeListCategories.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName});
			this.treeListCategories.Location = new System.Drawing.Point(12, 102);
			this.treeListCategories.Name = "treeListCategories";
			this.treeListCategories.OptionsBehavior.AutoChangeParent = false;
			this.treeListCategories.OptionsBehavior.Editable = false;
			this.treeListCategories.OptionsBehavior.ResizeNodes = false;
			this.treeListCategories.OptionsLayout.AddNewColumns = false;
			this.treeListCategories.OptionsMenu.EnableColumnMenu = false;
			this.treeListCategories.OptionsMenu.EnableFooterMenu = false;
			this.treeListCategories.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeListCategories.OptionsSelection.MultiSelect = true;
			this.treeListCategories.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.treeListCategories.OptionsView.ShowCheckBoxes = true;
			this.treeListCategories.OptionsView.ShowColumns = false;
			this.treeListCategories.OptionsView.ShowHorzLines = false;
			this.treeListCategories.OptionsView.ShowIndicator = false;
			this.treeListCategories.OptionsView.ShowVertLines = false;
			this.treeListCategories.RowHeight = 20;
			this.treeListCategories.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
			this.treeListCategories.Size = new System.Drawing.Size(326, 542);
			this.treeListCategories.StateImageList = this.imageList;
			this.treeListCategories.TabIndex = 2;
			this.treeListCategories.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.OnCategoriesNodeCellStyle);
			this.treeListCategories.BeforeCollapse += new DevExpress.XtraTreeList.BeforeCollapseEventHandler(this.OnCategoriesBeforeCollapse);
			this.treeListCategories.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler(this.OnCategoriesAfterExpand);
			this.treeListCategories.AfterCollapse += new DevExpress.XtraTreeList.NodeEventHandler(this.OnCategoriesAfterCollapse);
			this.treeListCategories.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.OnCategoriesBeforeCheckNode);
			this.treeListCategories.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.OnCategoriesAfterCheckNode);
			// 
			// treeListColumnName
			// 
			this.treeListColumnName.Caption = "Name";
			this.treeListColumnName.FieldName = "Name";
			this.treeListColumnName.MinWidth = 49;
			this.treeListColumnName.Name = "treeListColumnName";
			this.treeListColumnName.Visible = true;
			this.treeListColumnName.VisibleIndex = 0;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList.Images.SetKeyName(0, "DataSourceListClosedFolder.png");
			this.imageList.Images.SetKeyName(1, "DataSourceListOpenedFolder.png");
			// 
			// buttonXCollapse
			// 
			this.buttonXCollapse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCollapse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCollapse.Location = new System.Drawing.Point(192, 57);
			this.buttonXCollapse.Name = "buttonXCollapse";
			this.buttonXCollapse.Size = new System.Drawing.Size(146, 31);
			this.buttonXCollapse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCollapse.TabIndex = 2;
			this.buttonXCollapse.Text = "Collapse All";
			this.buttonXCollapse.TextColor = System.Drawing.Color.Black;
			this.buttonXCollapse.Click += new System.EventHandler(this.OnCollapseClick);
			// 
			// buttonXExpand
			// 
			this.buttonXExpand.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExpand.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExpand.Location = new System.Drawing.Point(12, 57);
			this.buttonXExpand.Name = "buttonXExpand";
			this.buttonXExpand.Size = new System.Drawing.Size(146, 31);
			this.buttonXExpand.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExpand.TabIndex = 1;
			this.buttonXExpand.Text = "Expand All";
			this.buttonXExpand.TextColor = System.Drawing.Color.Black;
			this.buttonXExpand.Click += new System.EventHandler(this.OnExpandClick);
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Location = new System.Drawing.Point(12, 12);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(326, 31);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 0;
			this.buttonXReset.Text = "RESET ALL TAGS for the Selected Links";
			this.buttonXReset.TextColor = System.Drawing.Color.Black;
			this.buttonXReset.Click += new System.EventHandler(this.OnResetClick);
			// 
			// layoutControl
			// 
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
			this.layoutControl.Controls.Add(this.treeListCategories);
			this.layoutControl.Controls.Add(this.buttonXCollapse);
			this.layoutControl.Controls.Add(this.buttonXExpand);
			this.layoutControl.Controls.Add(this.buttonXReset);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(350, 656);
			this.layoutControl.TabIndex = 63;
			this.layoutControl.Text = "layoutControl1";
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
            this.layoutControlItemCategories,
            this.layoutControlGroup1});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(350, 656);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemCategories
			// 
			this.layoutControlItemCategories.Control = this.treeListCategories;
			this.layoutControlItemCategories.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCategories.FillControlToClientArea = false;
			this.layoutControlItemCategories.Location = new System.Drawing.Point(0, 90);
			this.layoutControlItemCategories.Name = "layoutControlItemCategories";
			this.layoutControlItemCategories.Size = new System.Drawing.Size(330, 546);
			this.layoutControlItemCategories.Text = "Categories";
			this.layoutControlItemCategories.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCategories.TextVisible = false;
			this.layoutControlItemCategories.TrimClientAreaToControl = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemReset,
            this.emptySpaceItem1,
            this.layoutControlItemExpand,
            this.emptySpaceItem2,
            this.layoutControlItemCollapse,
            this.emptySpaceItem3});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(330, 90);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItemReset
			// 
			this.layoutControlItemReset.Control = this.buttonXReset;
			this.layoutControlItemReset.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemReset.FillControlToClientArea = false;
			this.layoutControlItemReset.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemReset.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItemReset.MinSize = new System.Drawing.Size(104, 35);
			this.layoutControlItemReset.Name = "layoutControlItemReset";
			this.layoutControlItemReset.Size = new System.Drawing.Size(330, 35);
			this.layoutControlItemReset.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemReset.Text = "Reset";
			this.layoutControlItemReset.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemReset.TextVisible = false;
			this.layoutControlItemReset.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 35);
			this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(330, 10);
			this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemExpand
			// 
			this.layoutControlItemExpand.Control = this.buttonXExpand;
			this.layoutControlItemExpand.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemExpand.FillControlToClientArea = false;
			this.layoutControlItemExpand.Location = new System.Drawing.Point(0, 45);
			this.layoutControlItemExpand.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItemExpand.MinSize = new System.Drawing.Size(150, 35);
			this.layoutControlItemExpand.Name = "layoutControlItemExpand";
			this.layoutControlItemExpand.Size = new System.Drawing.Size(150, 35);
			this.layoutControlItemExpand.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemExpand.Text = "Expand";
			this.layoutControlItemExpand.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemExpand.TextVisible = false;
			this.layoutControlItemExpand.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(150, 45);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(30, 35);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCollapse
			// 
			this.layoutControlItemCollapse.Control = this.buttonXCollapse;
			this.layoutControlItemCollapse.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCollapse.FillControlToClientArea = false;
			this.layoutControlItemCollapse.Location = new System.Drawing.Point(180, 45);
			this.layoutControlItemCollapse.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItemCollapse.MinSize = new System.Drawing.Size(150, 35);
			this.layoutControlItemCollapse.Name = "layoutControlItemCollapse";
			this.layoutControlItemCollapse.Size = new System.Drawing.Size(150, 35);
			this.layoutControlItemCollapse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemCollapse.Text = "Collapse";
			this.layoutControlItemCollapse.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCollapse.TextVisible = false;
			this.layoutControlItemCollapse.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 80);
			this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(330, 10);
			this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// CategoriesEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "CategoriesEditor";
			this.Size = new System.Drawing.Size(350, 656);
			((System.ComponentModel.ISupportInitialize)(this.treeListCategories)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCategories)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExpand)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCollapse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private System.Windows.Forms.ImageList imageList;
		private CommonGUI.CustomTreeList.EmptyImageTreeList treeListCategories;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
		private DevComponents.DotNetBar.ButtonX buttonXCollapse;
		private DevComponents.DotNetBar.ButtonX buttonXExpand;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemReset;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemExpand;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCollapse;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCategories;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
	}
}
