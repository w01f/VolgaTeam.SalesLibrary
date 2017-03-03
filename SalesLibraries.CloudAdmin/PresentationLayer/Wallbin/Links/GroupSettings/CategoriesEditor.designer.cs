namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.GroupSettings
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.treeListCategories = new SalesLibraries.CommonGUI.CustomTreeList.EmptyImageTreeList();
			this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExpand = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCollapse = new DevComponents.DotNetBar.ButtonX();
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListCategories)).BeginInit();
			this.pnButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Controls.Add(this.pnButtons);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(350, 656);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.treeListCategories);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 98);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(350, 558);
			this.pnData.TabIndex = 1;
			// 
			// treeListCategories
			// 
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
			this.treeListCategories.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListCategories.Location = new System.Drawing.Point(0, 0);
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
			this.treeListCategories.Size = new System.Drawing.Size(350, 558);
			this.treeListCategories.StateImageList = this.imageList;
			this.treeListCategories.TabIndex = 2;
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
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.buttonXCollapse);
			this.pnButtons.Controls.Add(this.buttonXExpand);
			this.pnButtons.Controls.Add(this.buttonXReset);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(350, 98);
			this.pnButtons.TabIndex = 0;
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Location = new System.Drawing.Point(5, 8);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(340, 30);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 0;
			this.buttonXReset.Text = "RESET ALL TAGS for the Selected Links";
			this.buttonXReset.TextColor = System.Drawing.Color.Black;
			this.buttonXReset.Click += new System.EventHandler(this.OnResetClick);
			// 
			// buttonXExpand
			// 
			this.buttonXExpand.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExpand.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExpand.Location = new System.Drawing.Point(5, 55);
			this.buttonXExpand.Name = "buttonXExpand";
			this.buttonXExpand.Size = new System.Drawing.Size(139, 30);
			this.buttonXExpand.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExpand.TabIndex = 1;
			this.buttonXExpand.Text = "Expand All";
			this.buttonXExpand.TextColor = System.Drawing.Color.Black;
			this.buttonXExpand.Click += new System.EventHandler(this.OnExpandClick);
			// 
			// buttonXCollapse
			// 
			this.buttonXCollapse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCollapse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCollapse.Location = new System.Drawing.Point(206, 55);
			this.buttonXCollapse.Name = "buttonXCollapse";
			this.buttonXCollapse.Size = new System.Drawing.Size(139, 30);
			this.buttonXCollapse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCollapse.TabIndex = 2;
			this.buttonXCollapse.Text = "Collapse All";
			this.buttonXCollapse.TextColor = System.Drawing.Color.Black;
			this.buttonXCollapse.Click += new System.EventHandler(this.OnCollapseClick);
			// 
			// CategoriesEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "CategoriesEditor";
			this.Size = new System.Drawing.Size(350, 656);
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListCategories)).EndInit();
			this.pnButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private System.Windows.Forms.ImageList imageList;
		private CommonGUI.CustomTreeList.EmptyImageTreeList treeListCategories;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
		private DevComponents.DotNetBar.ButtonX buttonXCollapse;
		private DevComponents.DotNetBar.ButtonX buttonXExpand;
	}
}
