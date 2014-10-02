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
			this.ribbonPanelCalendar = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarCalendarExit = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemCalendarExit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarCalendarDisclaimer = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerCalendarDisclaimer = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemCalendarDisclaimerLogo = new DevComponents.DotNetBar.LabelItem();
			this.ribbonBarCalendarParts = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonPanelHome = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarHomeExit = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeExit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeAttach = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeAttach = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeStations = new DevComponents.DotNetBar.RibbonBar();
			this.labelItemHomePackageLogo = new DevComponents.DotNetBar.LabelItem();
			this.itemContainerHomeStations = new DevComponents.DotNetBar.ItemContainer();
			this.comboBoxItemPackages = new DevComponents.DotNetBar.ComboBoxItem();
			this.comboBoxItemStations = new DevComponents.DotNetBar.ComboBoxItem();
			this.ribbonTabItemHome = new DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabItemCalendar = new DevComponents.DotNetBar.RibbonTabItem();
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelCalendar.SuspendLayout();
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
			this.ribbonControl.Controls.Add(this.ribbonPanelCalendar);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.EnableQatPlacement = false;
			this.ribbonControl.ForeColor = System.Drawing.Color.Black;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItemHome,
            this.ribbonTabItemCalendar});
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
			// ribbonPanelCalendar
			// 
			this.ribbonPanelCalendar.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarExit);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarDisclaimer);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarParts);
			this.ribbonPanelCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelCalendar.Location = new System.Drawing.Point(0, 53);
			this.ribbonPanelCalendar.Name = "ribbonPanelCalendar";
			this.ribbonPanelCalendar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
			this.ribbonPanelCalendar.Size = new System.Drawing.Size(858, 132);
			// 
			// 
			// 
			this.ribbonPanelCalendar.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelCalendar.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelCalendar.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelCalendar.TabIndex = 2;
			this.ribbonPanelCalendar.Visible = false;
			// 
			// ribbonBarCalendarExit
			// 
			this.ribbonBarCalendarExit.AutoOverflowEnabled = false;
			this.ribbonBarCalendarExit.AutoSizeItems = false;
			// 
			// 
			// 
			this.ribbonBarCalendarExit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarExit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarCalendarExit.ContainerControlProcessDialogKey = true;
			this.ribbonBarCalendarExit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarCalendarExit.DragDropSupport = true;
			this.ribbonBarCalendarExit.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarCalendarExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemCalendarExit});
			this.ribbonBarCalendarExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarCalendarExit.Location = new System.Drawing.Point(256, 0);
			this.ribbonBarCalendarExit.Name = "ribbonBarCalendarExit";
			this.ribbonBarCalendarExit.ResizeItemsToFit = false;
			this.ribbonBarCalendarExit.Size = new System.Drawing.Size(80, 130);
			this.ribbonBarCalendarExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarExit.TabIndex = 23;
			this.ribbonBarCalendarExit.Text = "EXIT";
			// 
			// 
			// 
			this.ribbonBarCalendarExit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarExit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemCalendarExit
			// 
			this.buttonItemCalendarExit.Image = global::OutlookSalesDepotAddIn.Properties.Resources.Exit;
			this.buttonItemCalendarExit.Name = "buttonItemCalendarExit";
			this.buttonItemCalendarExit.SubItemsExpandWidth = 14;
			this.buttonItemCalendarExit.Click += new System.EventHandler(this.buttonItemHomeExit_Click);
			// 
			// ribbonBarCalendarDisclaimer
			// 
			this.ribbonBarCalendarDisclaimer.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarCalendarDisclaimer.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarDisclaimer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarCalendarDisclaimer.ContainerControlProcessDialogKey = true;
			this.ribbonBarCalendarDisclaimer.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarCalendarDisclaimer.DragDropSupport = true;
			this.ribbonBarCalendarDisclaimer.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerCalendarDisclaimer});
			this.ribbonBarCalendarDisclaimer.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarCalendarDisclaimer.Location = new System.Drawing.Point(103, 0);
			this.ribbonBarCalendarDisclaimer.Name = "ribbonBarCalendarDisclaimer";
			this.ribbonBarCalendarDisclaimer.Size = new System.Drawing.Size(153, 130);
			this.ribbonBarCalendarDisclaimer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarDisclaimer.TabIndex = 24;
			this.ribbonBarCalendarDisclaimer.Text = "Disclaimer";
			// 
			// 
			// 
			this.ribbonBarCalendarDisclaimer.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarDisclaimer.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerCalendarDisclaimer
			// 
			// 
			// 
			// 
			this.itemContainerCalendarDisclaimer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerCalendarDisclaimer.BackgroundStyle.MarginTop = 5;
			this.itemContainerCalendarDisclaimer.ItemSpacing = 3;
			this.itemContainerCalendarDisclaimer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerCalendarDisclaimer.Name = "itemContainerCalendarDisclaimer";
			this.itemContainerCalendarDisclaimer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemCalendarDisclaimerLogo});
			// 
			// 
			// 
			this.itemContainerCalendarDisclaimer.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// labelItemCalendarDisclaimerLogo
			// 
			this.labelItemCalendarDisclaimerLogo.Image = global::OutlookSalesDepotAddIn.Properties.Resources.CalendarDisclaimerLogo;
			this.labelItemCalendarDisclaimerLogo.Name = "labelItemCalendarDisclaimerLogo";
			// 
			// ribbonBarCalendarParts
			// 
			this.ribbonBarCalendarParts.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarCalendarParts.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarParts.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarCalendarParts.ContainerControlProcessDialogKey = true;
			this.ribbonBarCalendarParts.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarCalendarParts.DragDropSupport = true;
			this.ribbonBarCalendarParts.ItemSpacing = 2;
			this.ribbonBarCalendarParts.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.ribbonBarCalendarParts.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarCalendarParts.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarCalendarParts.Name = "ribbonBarCalendarParts";
			this.ribbonBarCalendarParts.Size = new System.Drawing.Size(100, 130);
			this.ribbonBarCalendarParts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarParts.TabIndex = 25;
			this.ribbonBarCalendarParts.Text = "Calendar";
			// 
			// 
			// 
			this.ribbonBarCalendarParts.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarParts.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
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
			// buttonItemHomeExit
			// 
			this.buttonItemHomeExit.Image = global::OutlookSalesDepotAddIn.Properties.Resources.Exit;
			this.buttonItemHomeExit.Name = "buttonItemHomeExit";
			this.buttonItemHomeExit.SubItemsExpandWidth = 14;
			this.buttonItemHomeExit.Click += new System.EventHandler(this.buttonItemHomeExit_Click);
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
			// buttonItemHomeAttach
			// 
			this.buttonItemHomeAttach.Image = global::OutlookSalesDepotAddIn.Properties.Resources.Attach;
			this.buttonItemHomeAttach.Name = "buttonItemHomeAttach";
			this.buttonItemHomeAttach.SubItemsExpandWidth = 14;
			this.buttonItemHomeAttach.Text = "buttonItem1";
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
			// labelItemHomePackageLogo
			// 
			this.labelItemHomePackageLogo.Image = global::OutlookSalesDepotAddIn.Properties.Resources.PackageLogo;
			this.labelItemHomePackageLogo.Name = "labelItemHomePackageLogo";
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
			// ribbonTabItemCalendar
			// 
			this.ribbonTabItemCalendar.Name = "ribbonTabItemCalendar";
			this.ribbonTabItemCalendar.Panel = this.ribbonPanelCalendar;
			this.ribbonTabItemCalendar.Text = "Overnights";
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
			this.ribbonPanelCalendar.ResumeLayout(false);
			this.ribbonPanelHome.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		public DevComponents.DotNetBar.RibbonControl ribbonControl;
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
		public DevComponents.DotNetBar.RibbonPanel ribbonPanelCalendar;
		public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemCalendar;
		public DevComponents.DotNetBar.RibbonBar ribbonBarCalendarExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemCalendarExit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarCalendarDisclaimer;
		private DevComponents.DotNetBar.ItemContainer itemContainerCalendarDisclaimer;
		public DevComponents.DotNetBar.LabelItem labelItemCalendarDisclaimerLogo;
		public DevComponents.DotNetBar.RibbonBar ribbonBarCalendarParts;
	}
}