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
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelUsers = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarUsersExit = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarUsersImport = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarUsersRefresh = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarUsersDelete = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarUsersEdit = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarUsersAdd = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarUsersSite = new DevComponents.DotNetBar.RibbonBar();
			this.comboBoxEditUsersSite = new DevExpress.XtraEditors.ComboBoxEdit();
			this.itemContainerUsersSite = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemUsersSite = new DevComponents.DotNetBar.LabelItem();
			this.controlContainerItemUsersSite = new DevComponents.DotNetBar.ControlContainerItem();
			this.ribbonBarUsersLogo = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerUsersLogo = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemUsersLogo = new DevComponents.DotNetBar.LabelItem();
			this.ribbonPanelActivities = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarActivitiesExit = new DevComponents.DotNetBar.RibbonBar();
			this.ribbonBarActivitiesSite = new DevComponents.DotNetBar.RibbonBar();
			this.comboBoxEditActivitiesSite = new DevExpress.XtraEditors.ComboBoxEdit();
			this.itemContainerActivitiesSite = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemActivitiesSite = new DevComponents.DotNetBar.LabelItem();
			this.controlContainerItemActivittiesSite = new DevComponents.DotNetBar.ControlContainerItem();
			this.ribbonBarActivitiesLogo = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerActivitiesLogo = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemActivitiesLogo = new DevComponents.DotNetBar.LabelItem();
			this.ribbonTabItemUsers = new DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabItemActivities = new DevComponents.DotNetBar.RibbonTabItem();
			this.pnMain = new System.Windows.Forms.Panel();
			this.buttonItemUsersExit = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemUsersImport = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemUsersRefresh = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemUsersDelete = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemUsersEdit = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemUsersAdd = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemActivitiesExit = new DevComponents.DotNetBar.ButtonItem();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelUsers.SuspendLayout();
			this.ribbonBarUsersSite.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditUsersSite.Properties)).BeginInit();
			this.ribbonPanelActivities.SuspendLayout();
			this.ribbonBarActivitiesSite.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditActivitiesSite.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// superTooltip
			// 
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Lilian";
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
			this.ribbonControl.Controls.Add(this.ribbonPanelUsers);
			this.ribbonControl.Controls.Add(this.ribbonPanelActivities);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.EnableQatPlacement = false;
			this.ribbonControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItemUsers,
            this.ribbonTabItemActivities});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(0, 0);
			this.ribbonControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.ribbonControl.Size = new System.Drawing.Size(993, 146);
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
			// ribbonPanelUsers
			// 
			this.ribbonPanelUsers.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersExit);
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersImport);
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersRefresh);
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersDelete);
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersEdit);
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersAdd);
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersSite);
			this.ribbonPanelUsers.Controls.Add(this.ribbonBarUsersLogo);
			this.ribbonPanelUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelUsers.Location = new System.Drawing.Point(0, 25);
			this.ribbonPanelUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ribbonPanelUsers.Name = "ribbonPanelUsers";
			this.ribbonPanelUsers.Padding = new System.Windows.Forms.Padding(3, 0, 3, 4);
			this.ribbonPanelUsers.Size = new System.Drawing.Size(993, 119);
			// 
			// 
			// 
			this.ribbonPanelUsers.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelUsers.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelUsers.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelUsers.TabIndex = 1;
			// 
			// ribbonBarUsersExit
			// 
			this.ribbonBarUsersExit.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersExit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersExit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersExit.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersExit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemUsersExit});
			this.ribbonBarUsersExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersExit.Location = new System.Drawing.Point(883, 0);
			this.ribbonBarUsersExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ribbonBarUsersExit.Name = "ribbonBarUsersExit";
			this.ribbonBarUsersExit.Size = new System.Drawing.Size(77, 115);
			this.ribbonBarUsersExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersExit.TabIndex = 22;
			this.ribbonBarUsersExit.Text = "EXIT";
			// 
			// 
			// 
			this.ribbonBarUsersExit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersExit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarUsersImport
			// 
			this.ribbonBarUsersImport.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersImport.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersImport.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersImport.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersImport.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersImport.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemUsersImport});
			this.ribbonBarUsersImport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersImport.Location = new System.Drawing.Point(793, 0);
			this.ribbonBarUsersImport.Name = "ribbonBarUsersImport";
			this.ribbonBarUsersImport.Size = new System.Drawing.Size(90, 115);
			this.ribbonBarUsersImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersImport.TabIndex = 29;
			this.ribbonBarUsersImport.Text = "Import Users";
			// 
			// 
			// 
			this.ribbonBarUsersImport.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersImport.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarUsersRefresh
			// 
			this.ribbonBarUsersRefresh.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersRefresh.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersRefresh.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersRefresh.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersRefresh.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersRefresh.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemUsersRefresh});
			this.ribbonBarUsersRefresh.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersRefresh.Location = new System.Drawing.Point(697, 0);
			this.ribbonBarUsersRefresh.Name = "ribbonBarUsersRefresh";
			this.ribbonBarUsersRefresh.Size = new System.Drawing.Size(96, 115);
			this.ribbonBarUsersRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersRefresh.TabIndex = 25;
			this.ribbonBarUsersRefresh.Text = "Refresh";
			// 
			// 
			// 
			this.ribbonBarUsersRefresh.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersRefresh.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarUsersDelete
			// 
			this.ribbonBarUsersDelete.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersDelete.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersDelete.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersDelete.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersDelete.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersDelete.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemUsersDelete});
			this.ribbonBarUsersDelete.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersDelete.Location = new System.Drawing.Point(617, 0);
			this.ribbonBarUsersDelete.Name = "ribbonBarUsersDelete";
			this.ribbonBarUsersDelete.Size = new System.Drawing.Size(80, 115);
			this.ribbonBarUsersDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersDelete.TabIndex = 26;
			this.ribbonBarUsersDelete.Text = "Delete";
			// 
			// 
			// 
			this.ribbonBarUsersDelete.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersDelete.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarUsersEdit
			// 
			this.ribbonBarUsersEdit.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersEdit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersEdit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersEdit.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersEdit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersEdit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemUsersEdit});
			this.ribbonBarUsersEdit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersEdit.Location = new System.Drawing.Point(531, 0);
			this.ribbonBarUsersEdit.Name = "ribbonBarUsersEdit";
			this.ribbonBarUsersEdit.Size = new System.Drawing.Size(86, 115);
			this.ribbonBarUsersEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersEdit.TabIndex = 24;
			this.ribbonBarUsersEdit.Text = "Edit";
			// 
			// 
			// 
			this.ribbonBarUsersEdit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersEdit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarUsersAdd
			// 
			this.ribbonBarUsersAdd.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersAdd.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersAdd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersAdd.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersAdd.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersAdd.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemUsersAdd});
			this.ribbonBarUsersAdd.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersAdd.Location = new System.Drawing.Point(451, 0);
			this.ribbonBarUsersAdd.Name = "ribbonBarUsersAdd";
			this.ribbonBarUsersAdd.Size = new System.Drawing.Size(80, 115);
			this.ribbonBarUsersAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersAdd.TabIndex = 23;
			this.ribbonBarUsersAdd.Text = "Add";
			// 
			// 
			// 
			this.ribbonBarUsersAdd.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersAdd.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarUsersSite
			// 
			this.ribbonBarUsersSite.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersSite.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersSite.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersSite.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersSite.Controls.Add(this.comboBoxEditUsersSite);
			this.ribbonBarUsersSite.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersSite.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerUsersSite});
			this.ribbonBarUsersSite.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersSite.Location = new System.Drawing.Point(216, 0);
			this.ribbonBarUsersSite.Name = "ribbonBarUsersSite";
			this.ribbonBarUsersSite.Size = new System.Drawing.Size(235, 115);
			this.ribbonBarUsersSite.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersSite.TabIndex = 27;
			this.ribbonBarUsersSite.Text = "Site";
			// 
			// 
			// 
			this.ribbonBarUsersSite.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersSite.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// comboBoxEditUsersSite
			// 
			this.comboBoxEditUsersSite.Location = new System.Drawing.Point(4, 50);
			this.comboBoxEditUsersSite.Name = "comboBoxEditUsersSite";
			this.comboBoxEditUsersSite.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditUsersSite.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditUsersSite.Size = new System.Drawing.Size(218, 20);
			this.comboBoxEditUsersSite.StyleController = this.styleController;
			this.comboBoxEditUsersSite.TabIndex = 0;
			// 
			// itemContainerUsersSite
			// 
			// 
			// 
			// 
			this.itemContainerUsersSite.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerUsersSite.ItemSpacing = 5;
			this.itemContainerUsersSite.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerUsersSite.Name = "itemContainerUsersSite";
			this.itemContainerUsersSite.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemUsersSite,
            this.controlContainerItemUsersSite});
			this.itemContainerUsersSite.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemUsersSite
			// 
			this.labelItemUsersSite.Name = "labelItemUsersSite";
			this.labelItemUsersSite.Text = " Select Site:";
			// 
			// controlContainerItemUsersSite
			// 
			this.controlContainerItemUsersSite.AllowItemResize = false;
			this.controlContainerItemUsersSite.Control = this.comboBoxEditUsersSite;
			this.controlContainerItemUsersSite.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemUsersSite.Name = "controlContainerItemUsersSite";
			// 
			// ribbonBarUsersLogo
			// 
			this.ribbonBarUsersLogo.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarUsersLogo.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarUsersLogo.ContainerControlProcessDialogKey = true;
			this.ribbonBarUsersLogo.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarUsersLogo.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerUsersLogo});
			this.ribbonBarUsersLogo.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarUsersLogo.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarUsersLogo.Name = "ribbonBarUsersLogo";
			this.ribbonBarUsersLogo.Size = new System.Drawing.Size(213, 115);
			this.ribbonBarUsersLogo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarUsersLogo.TabIndex = 28;
			this.ribbonBarUsersLogo.Text = "Site Manager 1.0";
			// 
			// 
			// 
			this.ribbonBarUsersLogo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarUsersLogo.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerUsersLogo
			// 
			// 
			// 
			// 
			this.itemContainerUsersLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerUsersLogo.BackgroundStyle.MarginTop = 5;
			this.itemContainerUsersLogo.ItemSpacing = 3;
			this.itemContainerUsersLogo.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerUsersLogo.Name = "itemContainerUsersLogo";
			this.itemContainerUsersLogo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemUsersLogo});
			// 
			// labelItemUsersLogo
			// 
			this.labelItemUsersLogo.Name = "labelItemUsersLogo";
			// 
			// ribbonPanelActivities
			// 
			this.ribbonPanelActivities.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelActivities.Controls.Add(this.ribbonBarActivitiesExit);
			this.ribbonPanelActivities.Controls.Add(this.ribbonBarActivitiesSite);
			this.ribbonPanelActivities.Controls.Add(this.ribbonBarActivitiesLogo);
			this.ribbonPanelActivities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelActivities.Location = new System.Drawing.Point(0, 25);
			this.ribbonPanelActivities.Name = "ribbonPanelActivities";
			this.ribbonPanelActivities.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.ribbonPanelActivities.Size = new System.Drawing.Size(993, 119);
			// 
			// 
			// 
			this.ribbonPanelActivities.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelActivities.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelActivities.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelActivities.TabIndex = 2;
			this.ribbonPanelActivities.Visible = false;
			// 
			// ribbonBarActivitiesExit
			// 
			this.ribbonBarActivitiesExit.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarActivitiesExit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarActivitiesExit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarActivitiesExit.ContainerControlProcessDialogKey = true;
			this.ribbonBarActivitiesExit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarActivitiesExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemActivitiesExit});
			this.ribbonBarActivitiesExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarActivitiesExit.Location = new System.Drawing.Point(451, 0);
			this.ribbonBarActivitiesExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ribbonBarActivitiesExit.Name = "ribbonBarActivitiesExit";
			this.ribbonBarActivitiesExit.Size = new System.Drawing.Size(77, 116);
			this.ribbonBarActivitiesExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarActivitiesExit.TabIndex = 23;
			this.ribbonBarActivitiesExit.Text = "EXIT";
			// 
			// 
			// 
			this.ribbonBarActivitiesExit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarActivitiesExit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// ribbonBarActivitiesSite
			// 
			this.ribbonBarActivitiesSite.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarActivitiesSite.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarActivitiesSite.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarActivitiesSite.ContainerControlProcessDialogKey = true;
			this.ribbonBarActivitiesSite.Controls.Add(this.comboBoxEditActivitiesSite);
			this.ribbonBarActivitiesSite.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarActivitiesSite.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerActivitiesSite});
			this.ribbonBarActivitiesSite.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarActivitiesSite.Location = new System.Drawing.Point(216, 0);
			this.ribbonBarActivitiesSite.Name = "ribbonBarActivitiesSite";
			this.ribbonBarActivitiesSite.Size = new System.Drawing.Size(235, 116);
			this.ribbonBarActivitiesSite.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarActivitiesSite.TabIndex = 28;
			this.ribbonBarActivitiesSite.Text = "Site";
			// 
			// 
			// 
			this.ribbonBarActivitiesSite.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarActivitiesSite.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// comboBoxEditActivitiesSite
			// 
			this.comboBoxEditActivitiesSite.Location = new System.Drawing.Point(4, 51);
			this.comboBoxEditActivitiesSite.Name = "comboBoxEditActivitiesSite";
			this.comboBoxEditActivitiesSite.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditActivitiesSite.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditActivitiesSite.Size = new System.Drawing.Size(218, 20);
			this.comboBoxEditActivitiesSite.StyleController = this.styleController;
			this.comboBoxEditActivitiesSite.TabIndex = 0;
			// 
			// itemContainerActivitiesSite
			// 
			// 
			// 
			// 
			this.itemContainerActivitiesSite.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerActivitiesSite.ItemSpacing = 5;
			this.itemContainerActivitiesSite.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerActivitiesSite.Name = "itemContainerActivitiesSite";
			this.itemContainerActivitiesSite.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemActivitiesSite,
            this.controlContainerItemActivittiesSite});
			this.itemContainerActivitiesSite.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemActivitiesSite
			// 
			this.labelItemActivitiesSite.Name = "labelItemActivitiesSite";
			this.labelItemActivitiesSite.Text = " Select Site:";
			// 
			// controlContainerItemActivittiesSite
			// 
			this.controlContainerItemActivittiesSite.AllowItemResize = false;
			this.controlContainerItemActivittiesSite.Control = this.comboBoxEditActivitiesSite;
			this.controlContainerItemActivittiesSite.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemActivittiesSite.Name = "controlContainerItemActivittiesSite";
			// 
			// ribbonBarActivitiesLogo
			// 
			this.ribbonBarActivitiesLogo.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarActivitiesLogo.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarActivitiesLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarActivitiesLogo.ContainerControlProcessDialogKey = true;
			this.ribbonBarActivitiesLogo.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarActivitiesLogo.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerActivitiesLogo});
			this.ribbonBarActivitiesLogo.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarActivitiesLogo.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarActivitiesLogo.Name = "ribbonBarActivitiesLogo";
			this.ribbonBarActivitiesLogo.Size = new System.Drawing.Size(213, 116);
			this.ribbonBarActivitiesLogo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarActivitiesLogo.TabIndex = 29;
			this.ribbonBarActivitiesLogo.Text = "Site Manager 1.0";
			// 
			// 
			// 
			this.ribbonBarActivitiesLogo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarActivitiesLogo.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerActivitiesLogo
			// 
			// 
			// 
			// 
			this.itemContainerActivitiesLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerActivitiesLogo.BackgroundStyle.MarginTop = 5;
			this.itemContainerActivitiesLogo.ItemSpacing = 3;
			this.itemContainerActivitiesLogo.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerActivitiesLogo.Name = "itemContainerActivitiesLogo";
			this.itemContainerActivitiesLogo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemActivitiesLogo});
			// 
			// labelItemActivitiesLogo
			// 
			this.labelItemActivitiesLogo.Name = "labelItemActivitiesLogo";
			// 
			// ribbonTabItemUsers
			// 
			this.ribbonTabItemUsers.Checked = true;
			this.ribbonTabItemUsers.Name = "ribbonTabItemUsers";
			this.ribbonTabItemUsers.Panel = this.ribbonPanelUsers;
			this.ribbonTabItemUsers.Text = "Users";
			// 
			// ribbonTabItemActivities
			// 
			this.ribbonTabItemActivities.Name = "ribbonTabItemActivities";
			this.ribbonTabItemActivities.Panel = this.ribbonPanelActivities;
			this.ribbonTabItemActivities.Text = "Activity Tracker";
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 146);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(993, 387);
			this.pnMain.TabIndex = 3;
			// 
			// buttonItemUsersExit
			// 
			this.buttonItemUsersExit.Image = global::SalesDepot.SiteManager.Properties.Resources.Exit;
			this.buttonItemUsersExit.Name = "buttonItemUsersExit";
			this.buttonItemUsersExit.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemUsersExit, new DevComponents.DotNetBar.SuperTooltipInfo("Exit", "", "Close Site Manager", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemUsersExit.Text = "buttonItemHomeExit";
			this.buttonItemUsersExit.Click += new System.EventHandler(this.buttonItemExit_Click);
			// 
			// buttonItemUsersImport
			// 
			this.buttonItemUsersImport.Image = global::SalesDepot.SiteManager.Properties.Resources.ImportUsers;
			this.buttonItemUsersImport.Name = "buttonItemUsersImport";
			this.buttonItemUsersImport.SubItemsExpandWidth = 14;
			this.buttonItemUsersImport.Text = "buttonItem1";
			// 
			// buttonItemUsersRefresh
			// 
			this.buttonItemUsersRefresh.Image = global::SalesDepot.SiteManager.Properties.Resources.RefreshUsers;
			this.buttonItemUsersRefresh.Name = "buttonItemUsersRefresh";
			this.buttonItemUsersRefresh.SubItemsExpandWidth = 14;
			this.buttonItemUsersRefresh.Text = "buttonItem1";
			// 
			// buttonItemUsersDelete
			// 
			this.buttonItemUsersDelete.Image = global::SalesDepot.SiteManager.Properties.Resources.DeleteUser;
			this.buttonItemUsersDelete.Name = "buttonItemUsersDelete";
			this.buttonItemUsersDelete.SubItemsExpandWidth = 14;
			this.buttonItemUsersDelete.Text = "buttonItem1";
			// 
			// buttonItemUsersEdit
			// 
			this.buttonItemUsersEdit.Image = global::SalesDepot.SiteManager.Properties.Resources.ShowInfo;
			this.buttonItemUsersEdit.Name = "buttonItemUsersEdit";
			this.buttonItemUsersEdit.SubItemsExpandWidth = 14;
			this.buttonItemUsersEdit.Text = "buttonItem1";
			// 
			// buttonItemUsersAdd
			// 
			this.buttonItemUsersAdd.Image = global::SalesDepot.SiteManager.Properties.Resources.AddUser;
			this.buttonItemUsersAdd.Name = "buttonItemUsersAdd";
			this.buttonItemUsersAdd.SubItemsExpandWidth = 14;
			this.buttonItemUsersAdd.Text = "buttonItem1";
			// 
			// buttonItemActivitiesExit
			// 
			this.buttonItemActivitiesExit.Image = global::SalesDepot.SiteManager.Properties.Resources.Exit;
			this.buttonItemActivitiesExit.Name = "buttonItemActivitiesExit";
			this.buttonItemActivitiesExit.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemActivitiesExit, new DevComponents.DotNetBar.SuperTooltipInfo("Exit", "", "Close Site Manager", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemActivitiesExit.Text = "buttonItemHomeExit";
			this.buttonItemActivitiesExit.Click += new System.EventHandler(this.buttonItemExit_Click);
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.ClientSize = new System.Drawing.Size(993, 533);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.ribbonControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Site Manager";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelUsers.ResumeLayout(false);
			this.ribbonBarUsersSite.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditUsersSite.Properties)).EndInit();
			this.ribbonPanelActivities.ResumeLayout(false);
			this.ribbonBarActivitiesSite.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditActivitiesSite.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.StyleController styleController;
		public DevComponents.DotNetBar.RibbonControl ribbonControl;
		public DevComponents.DotNetBar.RibbonPanel ribbonPanelUsers;
		private DevComponents.DotNetBar.RibbonBar ribbonBarUsersExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemUsersExit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarUsersRefresh;
		private DevComponents.DotNetBar.RibbonBar ribbonBarUsersDelete;
		public DevComponents.DotNetBar.ButtonItem buttonItemUsersDelete;
		private DevComponents.DotNetBar.RibbonBar ribbonBarUsersEdit;
		public DevComponents.DotNetBar.ButtonItem buttonItemUsersEdit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarUsersAdd;
		public DevComponents.DotNetBar.ButtonItem buttonItemUsersAdd;
		private DevComponents.DotNetBar.RibbonBar ribbonBarUsersSite;
		private DevComponents.DotNetBar.ItemContainer itemContainerUsersSite;
		private DevComponents.DotNetBar.LabelItem labelItemUsersSite;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemUsersSite;
		public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditUsersSite;
		public DevComponents.DotNetBar.RibbonBar ribbonBarUsersLogo;
		private DevComponents.DotNetBar.ItemContainer itemContainerUsersLogo;
		public DevComponents.DotNetBar.LabelItem labelItemUsersLogo;
		private DevComponents.DotNetBar.RibbonBar ribbonBarUsersImport;
		private DevComponents.DotNetBar.RibbonPanel ribbonPanelActivities;
		private DevComponents.DotNetBar.RibbonBar ribbonBarActivitiesExit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarActivitiesSite;
		public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditActivitiesSite;
		private DevComponents.DotNetBar.ItemContainer itemContainerActivitiesSite;
		private DevComponents.DotNetBar.LabelItem labelItemActivitiesSite;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemActivittiesSite;
		private DevComponents.DotNetBar.ButtonItem buttonItemActivitiesExit;
		public DevComponents.DotNetBar.RibbonBar ribbonBarActivitiesLogo;
		private DevComponents.DotNetBar.ItemContainer itemContainerActivitiesLogo;
		public DevComponents.DotNetBar.LabelItem labelItemActivitiesLogo;
		public DevComponents.DotNetBar.ButtonItem buttonItemUsersRefresh;
		public DevComponents.DotNetBar.ButtonItem buttonItemUsersImport;
		public System.Windows.Forms.Panel pnMain;
		public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemUsers;
		public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemActivities;
	}
}

