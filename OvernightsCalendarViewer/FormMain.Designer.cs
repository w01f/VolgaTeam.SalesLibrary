namespace OvernightsCalendarViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelCalendar = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarCalendarExit = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarCalendarHelp = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarCalendarFloater = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarCalendarFontSize = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerCalendarFontSize = new DevComponents.DotNetBar.ItemContainer();
			this.ribbonBarCalendarDisclaimer = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerCalendarDisclaimer = new DevComponents.DotNetBar.ItemContainer();
			this.ribbonBarStations = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerHomeBrandLogo = new DevComponents.DotNetBar.ItemContainer();
			this.itemContainerStations = new DevComponents.DotNetBar.ItemContainer();
			this.comboBoxItemPackages = new DevComponents.DotNetBar.ComboBoxItem();
			this.comboBoxItemStations = new DevComponents.DotNetBar.ComboBoxItem();
			this.ribbonTabItemCalendar = new DevComponents.DotNetBar.RibbonTabItem();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.galleryGroup = new DevComponents.DotNetBar.GalleryGroup();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.styleManager = new DevComponents.DotNetBar.StyleManager();
			this.ribbonBarCalendarParts = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemCalendarExit = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemCalendarHelp = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemCalendarFloater = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemCalendarFontSizeLarger = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemCalendarFontSizeSmaler = new DevComponents.DotNetBar.ButtonItem();
			this.labelItemCalendarDisclaimerLogo = new DevComponents.DotNetBar.LabelItem();
			this.labelItemPackageLogo = new DevComponents.DotNetBar.LabelItem();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelCalendar.SuspendLayout();
			this.SuspendLayout();
			// 
			// ribbonControl
			// 
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.Controls.Add(this.ribbonPanelCalendar);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItemCalendar});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(0, 0);
			this.ribbonControl.MdiSystemItemVisible = false;
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.ribbonControl.Size = new System.Drawing.Size(1008, 144);
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
			// 
			// ribbonPanelCalendar
			// 
			this.ribbonPanelCalendar.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarExit);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarHelp);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarFloater);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarFontSize);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarDisclaimer);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarCalendarParts);
			this.ribbonPanelCalendar.Controls.Add(this.ribbonBarStations);
			this.ribbonPanelCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelCalendar.Location = new System.Drawing.Point(0, 26);
			this.ribbonPanelCalendar.Name = "ribbonPanelCalendar";
			this.ribbonPanelCalendar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.ribbonPanelCalendar.Size = new System.Drawing.Size(1008, 115);
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
			this.ribbonPanelCalendar.TabIndex = 3;
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
			this.ribbonBarCalendarExit.Location = new System.Drawing.Point(860, 0);
			this.ribbonBarCalendarExit.Name = "ribbonBarCalendarExit";
			this.ribbonBarCalendarExit.ResizeItemsToFit = false;
			this.ribbonBarCalendarExit.Size = new System.Drawing.Size(80, 112);
			this.ribbonBarCalendarExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarExit.TabIndex = 15;
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
			// ribbonBarCalendarHelp
			// 
			this.ribbonBarCalendarHelp.AutoOverflowEnabled = false;
			this.ribbonBarCalendarHelp.AutoSizeItems = false;
			// 
			// 
			// 
			this.ribbonBarCalendarHelp.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarHelp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarCalendarHelp.ContainerControlProcessDialogKey = true;
			this.ribbonBarCalendarHelp.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarCalendarHelp.DragDropSupport = true;
			this.ribbonBarCalendarHelp.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarCalendarHelp.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemCalendarHelp});
			this.ribbonBarCalendarHelp.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarCalendarHelp.Location = new System.Drawing.Point(775, 0);
			this.ribbonBarCalendarHelp.Name = "ribbonBarCalendarHelp";
			this.ribbonBarCalendarHelp.ResizeItemsToFit = false;
			this.ribbonBarCalendarHelp.Size = new System.Drawing.Size(85, 112);
			this.ribbonBarCalendarHelp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarHelp.TabIndex = 14;
			this.ribbonBarCalendarHelp.Text = "HELP";
			// 
			// 
			// 
			this.ribbonBarCalendarHelp.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarHelp.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarCalendarFloater
			// 
			this.ribbonBarCalendarFloater.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarCalendarFloater.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarFloater.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarCalendarFloater.ContainerControlProcessDialogKey = true;
			this.ribbonBarCalendarFloater.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarCalendarFloater.DragDropSupport = true;
			this.ribbonBarCalendarFloater.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemCalendarFloater});
			this.ribbonBarCalendarFloater.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarCalendarFloater.Location = new System.Drawing.Point(686, 0);
			this.ribbonBarCalendarFloater.Name = "ribbonBarCalendarFloater";
			this.ribbonBarCalendarFloater.Size = new System.Drawing.Size(89, 112);
			this.ribbonBarCalendarFloater.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarFloater.TabIndex = 19;
			this.ribbonBarCalendarFloater.Text = "Floater";
			// 
			// 
			// 
			this.ribbonBarCalendarFloater.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarFloater.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarCalendarFontSize
			// 
			this.ribbonBarCalendarFontSize.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarCalendarFontSize.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarFontSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarCalendarFontSize.ContainerControlProcessDialogKey = true;
			this.ribbonBarCalendarFontSize.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarCalendarFontSize.DragDropSupport = true;
			this.ribbonBarCalendarFontSize.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarCalendarFontSize.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerCalendarFontSize});
			this.ribbonBarCalendarFontSize.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarCalendarFontSize.Location = new System.Drawing.Point(632, 0);
			this.ribbonBarCalendarFontSize.Name = "ribbonBarCalendarFontSize";
			this.ribbonBarCalendarFontSize.Size = new System.Drawing.Size(54, 112);
			this.ribbonBarCalendarFontSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarFontSize.TabIndex = 16;
			this.ribbonBarCalendarFontSize.Text = "Text";
			// 
			// 
			// 
			this.ribbonBarCalendarFontSize.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarCalendarFontSize.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerCalendarFontSize
			// 
			// 
			// 
			// 
			this.itemContainerCalendarFontSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerCalendarFontSize.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerCalendarFontSize.Name = "itemContainerCalendarFontSize";
			this.itemContainerCalendarFontSize.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemCalendarFontSizeLarger,
            this.buttonItemCalendarFontSizeSmaler});
			// 
			// 
			// 
			this.itemContainerCalendarFontSize.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerCalendarFontSize.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
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
			this.ribbonBarCalendarDisclaimer.Location = new System.Drawing.Point(479, 0);
			this.ribbonBarCalendarDisclaimer.Name = "ribbonBarCalendarDisclaimer";
			this.ribbonBarCalendarDisclaimer.Size = new System.Drawing.Size(153, 112);
			this.ribbonBarCalendarDisclaimer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarDisclaimer.TabIndex = 18;
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
			// ribbonBarStations
			// 
			this.ribbonBarStations.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarStations.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarStations.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarStations.ContainerControlProcessDialogKey = true;
			this.ribbonBarStations.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarStations.DragDropSupport = true;
			this.ribbonBarStations.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomeBrandLogo,
            this.itemContainerStations});
			this.ribbonBarStations.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarStations.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarStations.Name = "ribbonBarStations";
			this.ribbonBarStations.Size = new System.Drawing.Size(376, 112);
			this.ribbonBarStations.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarStations.TabIndex = 20;
			this.ribbonBarStations.Text = "Station";
			// 
			// 
			// 
			this.ribbonBarStations.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarStations.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerHomeBrandLogo
			// 
			// 
			// 
			// 
			this.itemContainerHomeBrandLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeBrandLogo.BackgroundStyle.MarginTop = 5;
			this.itemContainerHomeBrandLogo.ItemSpacing = 3;
			this.itemContainerHomeBrandLogo.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeBrandLogo.Name = "itemContainerHomeBrandLogo";
			this.itemContainerHomeBrandLogo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemPackageLogo});
			// 
			// 
			// 
			this.itemContainerHomeBrandLogo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerStations
			// 
			// 
			// 
			// 
			this.itemContainerStations.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStations.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerStations.Name = "itemContainerStations";
			this.itemContainerStations.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.comboBoxItemPackages,
            this.comboBoxItemStations});
			// 
			// 
			// 
			this.itemContainerStations.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStations.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
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
			// ribbonTabItemCalendar
			// 
			this.ribbonTabItemCalendar.Checked = true;
			this.ribbonTabItemCalendar.Name = "ribbonTabItemCalendar";
			this.ribbonTabItemCalendar.Panel = this.ribbonPanelCalendar;
			this.ribbonTabItemCalendar.Text = "Home";
			this.ribbonTabItemCalendar.Visible = false;
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// galleryGroup
			// 
			this.galleryGroup.Name = "galleryGroup";
			this.galleryGroup.Text = "galleryGroup1";
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// pnContainer
			// 
			this.pnContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContainer.Location = new System.Drawing.Point(0, 144);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(1008, 568);
			this.pnContainer.TabIndex = 1;
			// 
			// pnEmpty
			// 
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(0, 0);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(1008, 712);
			this.pnEmpty.TabIndex = 2;
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
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
			this.ribbonBarCalendarParts.Location = new System.Drawing.Point(379, 0);
			this.ribbonBarCalendarParts.Name = "ribbonBarCalendarParts";
			this.ribbonBarCalendarParts.Size = new System.Drawing.Size(100, 112);
			this.ribbonBarCalendarParts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarCalendarParts.TabIndex = 21;
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
			// buttonItemCalendarExit
			// 
			this.buttonItemCalendarExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemCalendarExit.Image")));
			this.buttonItemCalendarExit.Name = "buttonItemCalendarExit";
			this.buttonItemCalendarExit.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemCalendarExit, new DevComponents.DotNetBar.SuperTooltipInfo("EXIT", "", "Close Application", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemCalendarExit.Click += new System.EventHandler(this.buttonItemExit_Click);
			// 
			// buttonItemCalendarHelp
			// 
			this.buttonItemCalendarHelp.Image = global::OvernightsCalendarViewer.Properties.Resources.Help;
			this.buttonItemCalendarHelp.Name = "buttonItemCalendarHelp";
			this.buttonItemCalendarHelp.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemCalendarHelp, new DevComponents.DotNetBar.SuperTooltipInfo("Help", "", "Learn more about how to view overnights", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			// 
			// buttonItemCalendarFloater
			// 
			this.buttonItemCalendarFloater.Image = global::OvernightsCalendarViewer.Properties.Resources.Floater;
			this.buttonItemCalendarFloater.Name = "buttonItemCalendarFloater";
			this.buttonItemCalendarFloater.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemCalendarFloater, new DevComponents.DotNetBar.SuperTooltipInfo("Floater", "", "Send to desktop floater toobar", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemCalendarFloater.Text = "buttonItem1";
			this.buttonItemCalendarFloater.Click += new System.EventHandler(this.buttonItemFloater_Click);
			// 
			// buttonItemCalendarFontSizeLarger
			// 
			this.buttonItemCalendarFontSizeLarger.Image = global::OvernightsCalendarViewer.Properties.Resources.Format_Font_Larger;
			this.buttonItemCalendarFontSizeLarger.Name = "buttonItemCalendarFontSizeLarger";
			this.superTooltip.SetSuperTooltip(this.buttonItemCalendarFontSizeLarger, new DevComponents.DotNetBar.SuperTooltipInfo("Increase Font Size", "", "Click to make the font larger", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemCalendarFontSizeLarger.Text = "Large Text";
			// 
			// buttonItemCalendarFontSizeSmaler
			// 
			this.buttonItemCalendarFontSizeSmaler.Image = global::OvernightsCalendarViewer.Properties.Resources.Format_Font_Smaller;
			this.buttonItemCalendarFontSizeSmaler.Name = "buttonItemCalendarFontSizeSmaler";
			this.superTooltip.SetSuperTooltip(this.buttonItemCalendarFontSizeSmaler, new DevComponents.DotNetBar.SuperTooltipInfo("Decrease Font Size", "", "Click to make the font smaller", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemCalendarFontSizeSmaler.Text = "Small Text";
			// 
			// labelItemCalendarDisclaimerLogo
			// 
			this.labelItemCalendarDisclaimerLogo.Image = global::OvernightsCalendarViewer.Properties.Resources.CalendarDisclaimerLogo;
			this.labelItemCalendarDisclaimerLogo.Name = "labelItemCalendarDisclaimerLogo";
			// 
			// labelItemPackageLogo
			// 
			this.labelItemPackageLogo.Image = global::OvernightsCalendarViewer.Properties.Resources.CalendarLogo;
			this.labelItemPackageLogo.Name = "labelItemPackageLogo";
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(1008, 712);
			this.Controls.Add(this.pnContainer);
			this.Controls.Add(this.ribbonControl);
			this.Controls.Add(this.pnEmpty);
			this.MinimumSize = new System.Drawing.Size(1024, 750);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Overnights";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelCalendar.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.GalleryGroup galleryGroup;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		public DevComponents.DotNetBar.SuperTooltip superTooltip;
        private System.Windows.Forms.Panel pnContainer;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanelCalendar;
        public DevComponents.DotNetBar.RibbonBar ribbonBarCalendarExit;
        private DevComponents.DotNetBar.ButtonItem buttonItemCalendarExit;
        private DevComponents.DotNetBar.RibbonBar ribbonBarCalendarHelp;
        public DevComponents.DotNetBar.ButtonItem buttonItemCalendarHelp;
        public DevComponents.DotNetBar.RibbonControl ribbonControl;
        public DevComponents.DotNetBar.RibbonBar ribbonBarCalendarFontSize;
        private DevComponents.DotNetBar.ItemContainer itemContainerCalendarFontSize;
        public DevComponents.DotNetBar.ButtonItem buttonItemCalendarFontSizeLarger;
        public DevComponents.DotNetBar.ButtonItem buttonItemCalendarFontSizeSmaler;
        public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemCalendar;
        private DevComponents.DotNetBar.RibbonBar ribbonBarCalendarDisclaimer;
        private DevComponents.DotNetBar.ItemContainer itemContainerCalendarDisclaimer;
		public DevComponents.DotNetBar.LabelItem labelItemCalendarDisclaimerLogo;
		private System.Windows.Forms.Panel pnEmpty;
        public DevComponents.DotNetBar.RibbonBar ribbonBarCalendarFloater;
		private DevComponents.DotNetBar.ButtonItem buttonItemCalendarFloater;
		private DevComponents.DotNetBar.StyleManager styleManager;
		public DevComponents.DotNetBar.RibbonBar ribbonBarStations;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeBrandLogo;
		public DevComponents.DotNetBar.LabelItem labelItemPackageLogo;
		private DevComponents.DotNetBar.ItemContainer itemContainerStations;
		public DevComponents.DotNetBar.ComboBoxItem comboBoxItemPackages;
		public DevComponents.DotNetBar.ComboBoxItem comboBoxItemStations;
		public DevComponents.DotNetBar.RibbonBar ribbonBarCalendarParts;
    }
}