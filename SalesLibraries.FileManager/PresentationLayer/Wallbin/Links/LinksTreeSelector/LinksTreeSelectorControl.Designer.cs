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
			this.panelButtons = new System.Windows.Forms.Panel();
			this.buttonXCollapseAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExpandAll = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
			this.panelButtons.SuspendLayout();
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
			this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList.Location = new System.Drawing.Point(0, 38);
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
			this.treeList.Size = new System.Drawing.Size(325, 640);
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
			// panelButtons
			// 
			this.panelButtons.BackColor = System.Drawing.Color.Transparent;
			this.panelButtons.Controls.Add(this.buttonXCollapseAll);
			this.panelButtons.Controls.Add(this.buttonXExpandAll);
			this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelButtons.Location = new System.Drawing.Point(0, 0);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(325, 38);
			this.panelButtons.TabIndex = 55;
			// 
			// buttonXCollapseAll
			// 
			this.buttonXCollapseAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCollapseAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCollapseAll.Location = new System.Drawing.Point(214, 0);
			this.buttonXCollapseAll.Name = "buttonXCollapseAll";
			this.buttonXCollapseAll.Size = new System.Drawing.Size(111, 32);
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
			this.buttonXExpandAll.Size = new System.Drawing.Size(111, 32);
			this.buttonXExpandAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExpandAll.TabIndex = 6;
			this.buttonXExpandAll.Text = "Expand All";
			this.buttonXExpandAll.TextColor = System.Drawing.Color.Black;
			this.buttonXExpandAll.Click += new System.EventHandler(this.buttonXExpandAll_Click);
			// 
			// LinksTreeSelectorControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.treeList);
			this.Controls.Add(this.panelButtons);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "LinksTreeSelectorControl";
			this.Size = new System.Drawing.Size(325, 678);
			((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
			this.panelButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private CommonGUI.CustomTreeList.EmptyImageTreeList treeList;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private System.Windows.Forms.ImageList imageListFiles;
		private System.Windows.Forms.Panel panelButtons;
		private DevComponents.DotNetBar.ButtonX buttonXCollapseAll;
		private DevComponents.DotNetBar.ButtonX buttonXExpandAll;
	}
}
