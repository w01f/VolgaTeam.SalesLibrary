namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.LinksTreeSelector
{
	partial class LinksTreeSelectorControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinksTreeSelectorControl));
			this.treeList = new SalesLibraries.CommonGUI.CustomTreeList.EmptyImageTreeList();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.imageListFiles = new System.Windows.Forms.ImageList(this.components);
			this.buttonXCollapseAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExpandAll = new DevComponents.DotNetBar.ButtonX();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemTreeList = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroupButtons = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemExpandAll = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCollapseAll = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTreeList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupButtons)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExpandAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCollapseAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			this.SuspendLayout();
			// 
			// treeList
			// 
			this.treeList.AllowCheckMinLevel = 0;
			this.treeList.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeList.Appearance.FocusedCell.Options.UseFont = true;
			this.treeList.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeList.Appearance.FocusedRow.Options.UseFont = true;
			this.treeList.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeList.Appearance.Row.Options.UseFont = true;
			this.treeList.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeList.Appearance.SelectedRow.Options.UseFont = true;
			this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
			this.treeList.Location = new System.Drawing.Point(0, 42);
			this.treeList.Name = "treeList";
			this.treeList.OptionsBehavior.AutoChangeParent = false;
			this.treeList.OptionsBehavior.Editable = false;
			this.treeList.OptionsBehavior.ResizeNodes = false;
			this.treeList.OptionsLayout.AddNewColumns = false;
			this.treeList.OptionsMenu.EnableColumnMenu = false;
			this.treeList.OptionsMenu.EnableFooterMenu = false;
			this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.treeList.OptionsView.ShowColumns = false;
			this.treeList.OptionsView.ShowHorzLines = false;
			this.treeList.OptionsView.ShowIndicator = false;
			this.treeList.OptionsView.ShowVertLines = false;
			this.treeList.RowHeight = 25;
			this.treeList.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
			this.treeList.Size = new System.Drawing.Size(325, 636);
			this.treeList.StateImageList = this.imageListFiles;
			this.treeList.TabIndex = 54;
			this.treeList.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.OnTreeViewNodeCellStyle);
			this.treeList.BeforeCollapse += new DevExpress.XtraTreeList.BeforeCollapseEventHandler(this.OnTreeViewBeforeCollapse);
			this.treeList.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler(this.OnTreeViewAfterExpand);
			this.treeList.AfterCollapse += new DevExpress.XtraTreeList.NodeEventHandler(this.OnTreeViewAfterCollapse);
			this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.OnTreeViewFocusedNodeChanged);
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.Caption = "Name";
			this.treeListColumn1.FieldName = "Name";
			this.treeListColumn1.MinWidth = 49;
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 0;
			// 
			// imageListFiles
			// 
			this.imageListFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFiles.ImageStream")));
			this.imageListFiles.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListFiles.Images.SetKeyName(0, "DataSourceListClosedFolder.png");
			this.imageListFiles.Images.SetKeyName(1, "DataSourceListOpenedFolder.png");
			this.imageListFiles.Images.SetKeyName(2, "DataSourceListOther.png");
			this.imageListFiles.Images.SetKeyName(3, "DataSourceListDoc.png");
			this.imageListFiles.Images.SetKeyName(4, "DataSourceListMp4.png");
			this.imageListFiles.Images.SetKeyName(5, "DataSourceListPdf.png");
			this.imageListFiles.Images.SetKeyName(6, "DataSourceListPng.png");
			this.imageListFiles.Images.SetKeyName(7, "DataSourceListPpt.png");
			this.imageListFiles.Images.SetKeyName(8, "DataSourceListUrl.png");
			this.imageListFiles.Images.SetKeyName(9, "DataSourceListXls.png");
			// 
			// buttonXCollapseAll
			// 
			this.buttonXCollapseAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCollapseAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCollapseAll.Location = new System.Drawing.Point(205, 0);
			this.buttonXCollapseAll.Name = "buttonXCollapseAll";
			this.buttonXCollapseAll.Size = new System.Drawing.Size(120, 31);
			this.buttonXCollapseAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCollapseAll.TabIndex = 7;
			this.buttonXCollapseAll.Text = "Collapse All";
			this.buttonXCollapseAll.TextColor = System.Drawing.Color.Black;
			this.buttonXCollapseAll.Click += new System.EventHandler(this.buttonXCollapseAll_Click);
			// 
			// buttonXExpandAll
			// 
			this.buttonXExpandAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExpandAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExpandAll.Location = new System.Drawing.Point(0, 0);
			this.buttonXExpandAll.Name = "buttonXExpandAll";
			this.buttonXExpandAll.Size = new System.Drawing.Size(120, 32);
			this.buttonXExpandAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExpandAll.TabIndex = 6;
			this.buttonXExpandAll.Text = "Expand All";
			this.buttonXExpandAll.TextColor = System.Drawing.Color.Black;
			this.buttonXExpandAll.Click += new System.EventHandler(this.buttonXExpandAll_Click);
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
			this.layoutControl.Controls.Add(this.treeList);
			this.layoutControl.Controls.Add(this.buttonXCollapseAll);
			this.layoutControl.Controls.Add(this.buttonXExpandAll);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(749, 185, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(325, 678);
			this.layoutControl.TabIndex = 61;
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
            this.layoutControlItemTreeList,
            this.layoutControlGroupButtons});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(325, 678);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemTreeList
			// 
			this.layoutControlItemTreeList.Control = this.treeList;
			this.layoutControlItemTreeList.CustomizationFormText = "Tree List";
			this.layoutControlItemTreeList.Location = new System.Drawing.Point(0, 42);
			this.layoutControlItemTreeList.Name = "layoutControlItemTreeList";
			this.layoutControlItemTreeList.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemTreeList.Size = new System.Drawing.Size(325, 636);
			this.layoutControlItemTreeList.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTreeList.TextVisible = false;
			// 
			// layoutControlGroupButtons
			// 
			this.layoutControlGroupButtons.CustomizationFormText = "Buttons";
			this.layoutControlGroupButtons.GroupBordersVisible = false;
			this.layoutControlGroupButtons.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemExpandAll,
            this.emptySpaceItem2,
            this.layoutControlItemCollapseAll,
            this.emptySpaceItem3});
			this.layoutControlGroupButtons.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupButtons.Name = "layoutControlGroupButtons";
			this.layoutControlGroupButtons.Size = new System.Drawing.Size(325, 42);
			this.layoutControlGroupButtons.TextVisible = false;
			// 
			// layoutControlItemExpandAll
			// 
			this.layoutControlItemExpandAll.Control = this.buttonXExpandAll;
			this.layoutControlItemExpandAll.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemExpandAll.CustomizationFormText = "Expand All";
			this.layoutControlItemExpandAll.FillControlToClientArea = false;
			this.layoutControlItemExpandAll.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemExpandAll.MaxSize = new System.Drawing.Size(120, 32);
			this.layoutControlItemExpandAll.MinSize = new System.Drawing.Size(120, 32);
			this.layoutControlItemExpandAll.Name = "layoutControlItemExpandAll";
			this.layoutControlItemExpandAll.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemExpandAll.Size = new System.Drawing.Size(120, 32);
			this.layoutControlItemExpandAll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemExpandAll.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemExpandAll.TextVisible = false;
			this.layoutControlItemExpandAll.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(120, 0);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(85, 32);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCollapseAll
			// 
			this.layoutControlItemCollapseAll.Control = this.buttonXCollapseAll;
			this.layoutControlItemCollapseAll.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemCollapseAll.CustomizationFormText = "Collapse All";
			this.layoutControlItemCollapseAll.FillControlToClientArea = false;
			this.layoutControlItemCollapseAll.Location = new System.Drawing.Point(205, 0);
			this.layoutControlItemCollapseAll.MaxSize = new System.Drawing.Size(120, 31);
			this.layoutControlItemCollapseAll.MinSize = new System.Drawing.Size(120, 31);
			this.layoutControlItemCollapseAll.Name = "layoutControlItemCollapseAll";
			this.layoutControlItemCollapseAll.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemCollapseAll.Size = new System.Drawing.Size(120, 32);
			this.layoutControlItemCollapseAll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemCollapseAll.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCollapseAll.TextVisible = false;
			this.layoutControlItemCollapseAll.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 32);
			this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(325, 10);
			this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// LinksTreeSelectorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "LinksTreeSelectorControl";
			this.Size = new System.Drawing.Size(325, 678);
			((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTreeList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupButtons)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExpandAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCollapseAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private CommonGUI.CustomTreeList.EmptyImageTreeList treeList;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private System.Windows.Forms.ImageList imageListFiles;
		private DevComponents.DotNetBar.ButtonX buttonXCollapseAll;
		private DevComponents.DotNetBar.ButtonX buttonXExpandAll;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemExpandAll;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCollapseAll;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTreeList;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupButtons;
	}
}
