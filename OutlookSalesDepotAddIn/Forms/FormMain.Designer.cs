namespace OutlookSalesDepotAddIn.Forms
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelHome = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarHomeExit = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarHomeStations = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerHomeStations = new DevComponents.DotNetBar.ItemContainer();
			this.comboBoxItemPackages = new DevComponents.DotNetBar.ComboBoxItem();
			this.comboBoxItemStations = new DevComponents.DotNetBar.ComboBoxItem();
			this.ribbonTabItemHome = new DevComponents.DotNetBar.RibbonTabItem();
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.ribbonBarHomeAttach = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeExit = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeAttach = new DevComponents.DotNetBar.ButtonItem();
			this.labelItemHomePackageLogo = new DevComponents.DotNetBar.LabelItem();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelHome.SuspendLayout();
			this.SuspendLayout();
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
			// ribbonControl
			// 
			this.ribbonControl.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.CanCustomize = false;
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Controls.Add(this.ribbonPanelHome);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.EnableQatPlacement = false;
			this.ribbonControl.ForeColor = System.Drawing.Color.Black;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItemHome});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(5, 1);
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Size = new System.Drawing.Size(858, 185);
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
			this.ribbonControl.TabIndex = 0;
			this.ribbonControl.Text = "ribbonControl1";
			this.ribbonControl.UseCustomizeDialog = false;
			// 
			// ribbonPanelHome
			// 
			this.ribbonPanelHome.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeExit);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeAttach);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeStations);
			this.ribbonPanelHome.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelHome.Location = new System.Drawing.Point(0, 53);
			this.ribbonPanelHome.Name = "ribbonPanelHome";
			this.ribbonPanelHome.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
			this.ribbonPanelHome.Size = new System.Drawing.Size(858, 132);
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
			this.ribbonBarHomeExit.AutoOverflowEnabled = false;
			this.ribbonBarHomeExit.AutoSizeItems = false;
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
			this.ribbonBarHomeExit.DragDropSupport = true;
			this.ribbonBarHomeExit.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarHomeExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeExit});
			this.ribbonBarHomeExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeExit.Location = new System.Drawing.Point(473, 0);
			this.ribbonBarHomeExit.Name = "ribbonBarHomeExit";
			this.ribbonBarHomeExit.ResizeItemsToFit = false;
			this.ribbonBarHomeExit.Size = new System.Drawing.Size(92, 130);
			this.ribbonBarHomeExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeExit.TabIndex = 10;
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
			// ribbonBarHomeStations
			// 
			this.ribbonBarHomeStations.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeStations.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeStations.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeStations.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeStations.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeStations.DragDropSupport = true;
			this.ribbonBarHomeStations.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemHomePackageLogo,
            this.itemContainerHomeStations});
			this.ribbonBarHomeStations.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeStations.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarHomeStations.Name = "ribbonBarHomeStations";
			this.ribbonBarHomeStations.Size = new System.Drawing.Size(376, 130);
			this.ribbonBarHomeStations.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeStations.TabIndex = 9;
			this.ribbonBarHomeStations.Text = "Sales Depot";
			// 
			// 
			// 
			this.ribbonBarHomeStations.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeStations.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerHomeStations
			// 
			// 
			// 
			// 
			this.itemContainerHomeStations.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeStations.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeStations.Name = "itemContainerHomeStations";
			this.itemContainerHomeStations.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.comboBoxItemPackages,
            this.comboBoxItemStations});
			// 
			// 
			// 
			this.itemContainerHomeStations.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeStations.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// comboBoxItemPackages
			// 
			this.comboBoxItemPackages.ComboWidth = 160;
			this.comboBoxItemPackages.DropDownHeight = 106;
			this.comboBoxItemPackages.ItemHeight = 14;
			this.comboBoxItemPackages.Name = "comboBoxItemPackages";
			// 
			// comboBoxItemStations
			// 
			this.comboBoxItemStations.ComboWidth = 160;
			this.comboBoxItemStations.DropDownHeight = 106;
			this.comboBoxItemStations.ItemHeight = 14;
			this.comboBoxItemStations.Name = "comboBoxItemStations";
			this.comboBoxItemStations.Visible = false;
			// 
			// ribbonTabItemHome
			// 
			this.ribbonTabItemHome.Checked = true;
			this.ribbonTabItemHome.Name = "ribbonTabItemHome";
			this.ribbonTabItemHome.Panel = this.ribbonPanelHome;
			this.ribbonTabItemHome.Text = "Home";
			// 
			// pnContainer
			// 
			this.pnContainer.Location = new System.Drawing.Point(17, 202);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.pnContainer.Size = new System.Drawing.Size(276, 287);
			this.pnContainer.TabIndex = 3;
			// 
			// pnEmpty
			// 
			this.pnEmpty.Location = new System.Drawing.Point(316, 202);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(270, 287);
			this.pnEmpty.TabIndex = 4;
			// 
			// ribbonBarHomeAttach
			// 
			this.ribbonBarHomeAttach.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeAttach.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeAttach.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeAttach.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeAttach.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeAttach.DragDropSupport = true;
			this.ribbonBarHomeAttach.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeAttach});
			this.ribbonBarHomeAttach.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeAttach.Location = new System.Drawing.Point(379, 0);
			this.ribbonBarHomeAttach.Name = "ribbonBarHomeAttach";
			this.ribbonBarHomeAttach.Size = new System.Drawing.Size(94, 130);
			this.ribbonBarHomeAttach.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeAttach.TabIndex = 11;
			this.ribbonBarHomeAttach.Text = "Attach";
			// 
			// 
			// 
			this.ribbonBarHomeAttach.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeAttach.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeExit
			// 
			this.buttonItemHomeExit.Image = global::OutlookSalesDepotAddIn.Properties.Resources.Exit;
			this.buttonItemHomeExit.Name = "buttonItemHomeExit";
			this.buttonItemHomeExit.SubItemsExpandWidth = 14;
			this.buttonItemHomeExit.Click += new System.EventHandler(this.buttonItemHomeExit_Click);
			// 
			// buttonItemHomeAttach
			// 
			this.buttonItemHomeAttach.Image = global::OutlookSalesDepotAddIn.Properties.Resources.Attach;
			this.buttonItemHomeAttach.Name = "buttonItemHomeAttach";
			this.buttonItemHomeAttach.SubItemsExpandWidth = 14;
			this.buttonItemHomeAttach.Text = "buttonItem1";
			// 
			// labelItemHomePackageLogo
			// 
			this.labelItemHomePackageLogo.Image = global::OutlookSalesDepotAddIn.Properties.Resources.PackageLogo;
			this.labelItemHomePackageLogo.Name = "labelItemHomePackageLogo";
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(868, 649);
			this.Controls.Add(this.pnEmpty);
			this.Controls.Add(this.pnContainer);
			this.Controls.Add(this.ribbonControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormMain";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelHome.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.RibbonControl ribbonControl;
		public DevComponents.DotNetBar.RibbonPanel ribbonPanelHome;
		private DevComponents.DotNetBar.RibbonTabItem ribbonTabItemHome;
		public DevComponents.DotNetBar.RibbonBar ribbonBarHomeExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeExit;
		public DevComponents.DotNetBar.RibbonBar ribbonBarHomeStations;
		public DevComponents.DotNetBar.LabelItem labelItemHomePackageLogo;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeStations;
		public DevComponents.DotNetBar.ComboBoxItem comboBoxItemPackages;
		public DevComponents.DotNetBar.ComboBoxItem comboBoxItemStations;
		private System.Windows.Forms.Panel pnContainer;
		private System.Windows.Forms.Panel pnEmpty;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeAttach;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeAttach;
	}
}