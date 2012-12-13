namespace SalesDepot.SiteManager
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
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonItemHomeExit = new DevComponents.DotNetBar.ButtonItem();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelHome = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarHomeExit = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarHomeRefresh = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeRefresh = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeDelete = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeDelete = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeEdit = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeEdit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeAdd = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeAdd = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeSite = new DevComponents.DotNetBar.RibbonBar();
			this.comboBoxEditSite = new DevExpress.XtraEditors.ComboBoxEdit();
			this.itemContainerHomeSite = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemHomeSite = new DevComponents.DotNetBar.LabelItem();
			this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
			this.ribbonTabItemHome = new DevComponents.DotNetBar.RibbonTabItem();
			this.pnMain = new System.Windows.Forms.Panel();
			this.ribbonBarHomeLogo = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerHomeLogo = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemHomeLogo = new DevComponents.DotNetBar.LabelItem();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelHome.SuspendLayout();
			this.ribbonBarHomeSite.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSite.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// superTooltip
			// 
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonItemHomeExit
			// 
			this.buttonItemHomeExit.Image = global::SalesDepot.SiteManager.Properties.Resources.Exit;
			this.buttonItemHomeExit.Name = "buttonItemHomeExit";
			this.buttonItemHomeExit.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeExit, new DevComponents.DotNetBar.SuperTooltipInfo("Exit", "", "Close Site Manager", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemHomeExit.Text = "buttonItemHomeExit";
			this.buttonItemHomeExit.Click += new System.EventHandler(this.buttonItemHomeExit_Click);
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// ribbonControl
			// 
			this.ribbonControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.Controls.Add(this.ribbonPanelHome);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.EnableQatPlacement = false;
			this.ribbonControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItemHome});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(0, 0);
			this.ribbonControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.ribbonControl.Size = new System.Drawing.Size(769, 146);
			this.ribbonControl.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonControl.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
			this.ribbonControl.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
			this.ribbonControl.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
			this.ribbonControl.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
			this.ribbonControl.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
			this.ribbonControl.SystemText.QatDialogAddButton = "&Add >>";
			this.ribbonControl.SystemText.QatDialogCancelButton = "Cancel";
			this.ribbonControl.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
			this.ribbonControl.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
			this.ribbonControl.SystemText.QatDialogOkButton = "OK";
			this.ribbonControl.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
			this.ribbonControl.SystemText.QatDialogRemoveButton = "&Remove";
			this.ribbonControl.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
			this.ribbonControl.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
			this.ribbonControl.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
			this.ribbonControl.TabGroupHeight = 14;
			this.ribbonControl.TabIndex = 1;
			this.ribbonControl.Text = "ribbonControl";
			// 
			// ribbonPanelHome
			// 
			this.ribbonPanelHome.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeExit);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeRefresh);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeDelete);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeEdit);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeAdd);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeSite);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeLogo);
			this.ribbonPanelHome.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelHome.Location = new System.Drawing.Point(0, 25);
			this.ribbonPanelHome.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ribbonPanelHome.Name = "ribbonPanelHome";
			this.ribbonPanelHome.Padding = new System.Windows.Forms.Padding(3, 0, 3, 4);
			this.ribbonPanelHome.Size = new System.Drawing.Size(769, 119);
			// 
			// 
			// 
			this.ribbonPanelHome.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelHome.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelHome.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelHome.TabIndex = 1;
			// 
			// ribbonBarHomeExit
			// 
			this.ribbonBarHomeExit.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeExit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeExit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeExit.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeExit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeExit});
			this.ribbonBarHomeExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeExit.Location = new System.Drawing.Point(793, 0);
			this.ribbonBarHomeExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ribbonBarHomeExit.Name = "ribbonBarHomeExit";
			this.ribbonBarHomeExit.Size = new System.Drawing.Size(77, 115);
			this.ribbonBarHomeExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeExit.TabIndex = 22;
			this.ribbonBarHomeExit.Text = "EXIT";
			// 
			// 
			// 
			this.ribbonBarHomeExit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeExit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarHomeRefresh
			// 
			this.ribbonBarHomeRefresh.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeRefresh.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeRefresh.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeRefresh.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeRefresh.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeRefresh.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeRefresh});
			this.ribbonBarHomeRefresh.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeRefresh.Location = new System.Drawing.Point(697, 0);
			this.ribbonBarHomeRefresh.Name = "ribbonBarHomeRefresh";
			this.ribbonBarHomeRefresh.Size = new System.Drawing.Size(96, 115);
			this.ribbonBarHomeRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeRefresh.TabIndex = 25;
			this.ribbonBarHomeRefresh.Text = "Refresh";
			// 
			// 
			// 
			this.ribbonBarHomeRefresh.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeRefresh.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeRefresh
			// 
			this.buttonItemHomeRefresh.Image = global::SalesDepot.SiteManager.Properties.Resources.RefreshUsers;
			this.buttonItemHomeRefresh.Name = "buttonItemHomeRefresh";
			this.buttonItemHomeRefresh.SubItemsExpandWidth = 14;
			this.buttonItemHomeRefresh.Text = "buttonItem1";
			// 
			// ribbonBarHomeDelete
			// 
			this.ribbonBarHomeDelete.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeDelete.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeDelete.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeDelete.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeDelete.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeDelete.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeDelete});
			this.ribbonBarHomeDelete.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeDelete.Location = new System.Drawing.Point(617, 0);
			this.ribbonBarHomeDelete.Name = "ribbonBarHomeDelete";
			this.ribbonBarHomeDelete.Size = new System.Drawing.Size(80, 115);
			this.ribbonBarHomeDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeDelete.TabIndex = 26;
			this.ribbonBarHomeDelete.Text = "Delete";
			// 
			// 
			// 
			this.ribbonBarHomeDelete.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeDelete.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeDelete
			// 
			this.buttonItemHomeDelete.Image = global::SalesDepot.SiteManager.Properties.Resources.DeleteUser;
			this.buttonItemHomeDelete.Name = "buttonItemHomeDelete";
			this.buttonItemHomeDelete.SubItemsExpandWidth = 14;
			this.buttonItemHomeDelete.Text = "buttonItem1";
			// 
			// ribbonBarHomeEdit
			// 
			this.ribbonBarHomeEdit.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeEdit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeEdit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeEdit.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeEdit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeEdit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeEdit});
			this.ribbonBarHomeEdit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeEdit.Location = new System.Drawing.Point(531, 0);
			this.ribbonBarHomeEdit.Name = "ribbonBarHomeEdit";
			this.ribbonBarHomeEdit.Size = new System.Drawing.Size(86, 115);
			this.ribbonBarHomeEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeEdit.TabIndex = 24;
			this.ribbonBarHomeEdit.Text = "Edit";
			// 
			// 
			// 
			this.ribbonBarHomeEdit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeEdit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeEdit
			// 
			this.buttonItemHomeEdit.Image = global::SalesDepot.SiteManager.Properties.Resources.ShowInfo;
			this.buttonItemHomeEdit.Name = "buttonItemHomeEdit";
			this.buttonItemHomeEdit.SubItemsExpandWidth = 14;
			this.buttonItemHomeEdit.Text = "buttonItem1";
			// 
			// ribbonBarHomeAdd
			// 
			this.ribbonBarHomeAdd.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeAdd.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeAdd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeAdd.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeAdd.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeAdd.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeAdd});
			this.ribbonBarHomeAdd.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeAdd.Location = new System.Drawing.Point(451, 0);
			this.ribbonBarHomeAdd.Name = "ribbonBarHomeAdd";
			this.ribbonBarHomeAdd.Size = new System.Drawing.Size(80, 115);
			this.ribbonBarHomeAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeAdd.TabIndex = 23;
			this.ribbonBarHomeAdd.Text = "Add";
			// 
			// 
			// 
			this.ribbonBarHomeAdd.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeAdd.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeAdd
			// 
			this.buttonItemHomeAdd.Image = global::SalesDepot.SiteManager.Properties.Resources.AddUser;
			this.buttonItemHomeAdd.Name = "buttonItemHomeAdd";
			this.buttonItemHomeAdd.SubItemsExpandWidth = 14;
			this.buttonItemHomeAdd.Text = "buttonItem1";
			// 
			// ribbonBarHomeSite
			// 
			this.ribbonBarHomeSite.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeSite.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeSite.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeSite.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeSite.Controls.Add(this.comboBoxEditSite);
			this.ribbonBarHomeSite.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeSite.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomeSite});
			this.ribbonBarHomeSite.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeSite.Location = new System.Drawing.Point(216, 0);
			this.ribbonBarHomeSite.Name = "ribbonBarHomeSite";
			this.ribbonBarHomeSite.Size = new System.Drawing.Size(235, 115);
			this.ribbonBarHomeSite.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeSite.TabIndex = 27;
			this.ribbonBarHomeSite.Text = "Site";
			// 
			// 
			// 
			this.ribbonBarHomeSite.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeSite.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// comboBoxEditSite
			// 
			this.comboBoxEditSite.Location = new System.Drawing.Point(4, 49);
			this.comboBoxEditSite.Name = "comboBoxEditSite";
			this.comboBoxEditSite.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSite.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditSite.Size = new System.Drawing.Size(218, 20);
			this.comboBoxEditSite.StyleController = this.styleController;
			this.comboBoxEditSite.TabIndex = 0;
			// 
			// itemContainerHomeSite
			// 
			// 
			// 
			// 
			this.itemContainerHomeSite.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeSite.ItemSpacing = 5;
			this.itemContainerHomeSite.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeSite.Name = "itemContainerHomeSite";
			this.itemContainerHomeSite.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemHomeSite,
            this.controlContainerItem1});
			this.itemContainerHomeSite.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemHomeSite
			// 
			this.labelItemHomeSite.Name = "labelItemHomeSite";
			this.labelItemHomeSite.Text = " Select Site:";
			// 
			// controlContainerItem1
			// 
			this.controlContainerItem1.AllowItemResize = false;
			this.controlContainerItem1.Control = this.comboBoxEditSite;
			this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItem1.Name = "controlContainerItem1";
			// 
			// ribbonTabItemHome
			// 
			this.ribbonTabItemHome.Checked = true;
			this.ribbonTabItemHome.Name = "ribbonTabItemHome";
			this.ribbonTabItemHome.Panel = this.ribbonPanelHome;
			this.ribbonTabItemHome.Text = "Home";
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 146);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(769, 387);
			this.pnMain.TabIndex = 3;
			// 
			// ribbonBarHomeLogo
			// 
			this.ribbonBarHomeLogo.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeLogo.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeLogo.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeLogo.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeLogo.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomeLogo});
			this.ribbonBarHomeLogo.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeLogo.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarHomeLogo.Name = "ribbonBarHomeLogo";
			this.ribbonBarHomeLogo.Size = new System.Drawing.Size(213, 115);
			this.ribbonBarHomeLogo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeLogo.TabIndex = 28;
			this.ribbonBarHomeLogo.Text = "Site Manager 1.0";
			// 
			// 
			// 
			this.ribbonBarHomeLogo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeLogo.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerHomeLogo
			// 
			// 
			// 
			// 
			this.itemContainerHomeLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeLogo.BackgroundStyle.MarginTop = 5;
			this.itemContainerHomeLogo.ItemSpacing = 3;
			this.itemContainerHomeLogo.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeLogo.Name = "itemContainerHomeLogo";
			this.itemContainerHomeLogo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemHomeLogo});
			// 
			// labelItemHomeLogo
			// 
			this.labelItemHomeLogo.Name = "labelItemHomeLogo";
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.ClientSize = new System.Drawing.Size(769, 533);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.ribbonControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Site Manager";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelHome.ResumeLayout(false);
			this.ribbonBarHomeSite.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSite.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.StyleController styleController;
		public DevComponents.DotNetBar.RibbonControl ribbonControl;
		public DevComponents.DotNetBar.RibbonPanel ribbonPanelHome;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeExit;
		private DevComponents.DotNetBar.RibbonTabItem ribbonTabItemHome;
		private System.Windows.Forms.Panel pnMain;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeRefresh;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeRefresh;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeDelete;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeDelete;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeEdit;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeEdit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeAdd;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeAdd;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeSite;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeSite;
		private DevComponents.DotNetBar.LabelItem labelItemHomeSite;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
		public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSite;
		public DevComponents.DotNetBar.RibbonBar ribbonBarHomeLogo;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeLogo;
		public DevComponents.DotNetBar.LabelItem labelItemHomeLogo;
	}
}

