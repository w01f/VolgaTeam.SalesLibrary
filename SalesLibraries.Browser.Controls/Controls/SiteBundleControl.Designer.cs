namespace SalesLibraries.Browser.Controls.Controls
{
	partial class SiteBundleControl
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
			this.buttonItemMenuNavigationBack = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuNavigationForward = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuNavigationRefresh = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemFloater = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddSlide = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddSlides = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsPrint = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddVideo = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsDownloadYouTube = new DevComponents.DotNetBar.ButtonItem();
			this.barMain = new DevComponents.DotNetBar.Bar();
			this.comboBoxItemSites = new DevComponents.DotNetBar.ComboBoxItem();
			this.labelItemMenuWarning = new DevComponents.DotNetBar.LabelItem();
			this.panelMain = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.barMain)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonItemMenuNavigationBack
			// 
			this.buttonItemMenuNavigationBack.BeginGroup = true;
			this.buttonItemMenuNavigationBack.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.NavigationBack;
			this.buttonItemMenuNavigationBack.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemMenuNavigationBack.Name = "buttonItemMenuNavigationBack";
			this.buttonItemMenuNavigationBack.Text = "buttonItem2";
			this.buttonItemMenuNavigationBack.Tooltip = "Back";
			this.buttonItemMenuNavigationBack.Click += new System.EventHandler(this.OnMenuNavigationBackClick);
			// 
			// buttonItemMenuNavigationForward
			// 
			this.buttonItemMenuNavigationForward.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.NavigationForward;
			this.buttonItemMenuNavigationForward.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemMenuNavigationForward.Name = "buttonItemMenuNavigationForward";
			this.buttonItemMenuNavigationForward.Text = "buttonItem1";
			this.buttonItemMenuNavigationForward.Tooltip = "Forward";
			this.buttonItemMenuNavigationForward.Click += new System.EventHandler(this.OnMenuNavigationForwardClick);
			// 
			// buttonItemMenuNavigationRefresh
			// 
			this.buttonItemMenuNavigationRefresh.BeginGroup = true;
			this.buttonItemMenuNavigationRefresh.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.NavigationRefresh;
			this.buttonItemMenuNavigationRefresh.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemMenuNavigationRefresh.Name = "buttonItemMenuNavigationRefresh";
			this.buttonItemMenuNavigationRefresh.Text = "buttonItem1";
			this.buttonItemMenuNavigationRefresh.Tooltip = "Reload Page";
			this.buttonItemMenuNavigationRefresh.Click += new System.EventHandler(this.OnMenuNavigationRefreshClick);
			// 
			// buttonItemFloater
			// 
			this.buttonItemFloater.BeginGroup = true;
			this.buttonItemFloater.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.FloaterMenu;
			this.buttonItemFloater.Name = "buttonItemFloater";
			this.buttonItemFloater.Text = "buttonItem1";
			this.buttonItemFloater.Tooltip = "Floater";
			this.buttonItemFloater.Visible = false;
			this.buttonItemFloater.Click += new System.EventHandler(this.OnFloaterClick);
			// 
			// buttonItemMenuExtensionsAddSlide
			// 
			this.buttonItemMenuExtensionsAddSlide.BeginGroup = true;
			this.buttonItemMenuExtensionsAddSlide.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.ExtensionsAddSlide;
			this.buttonItemMenuExtensionsAddSlide.Name = "buttonItemMenuExtensionsAddSlide";
			this.buttonItemMenuExtensionsAddSlide.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddSlide.Tooltip = "Insert Slide";
			this.buttonItemMenuExtensionsAddSlide.Visible = false;
			this.buttonItemMenuExtensionsAddSlide.Click += new System.EventHandler(this.OnMenuExtensionsAddSlideClick);
			// 
			// buttonItemMenuExtensionsAddSlides
			// 
			this.buttonItemMenuExtensionsAddSlides.BeginGroup = true;
			this.buttonItemMenuExtensionsAddSlides.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemMenuExtensionsAddSlides.ForeColor = System.Drawing.Color.LightGray;
			this.buttonItemMenuExtensionsAddSlides.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.ExtensionsAddSlides;
			this.buttonItemMenuExtensionsAddSlides.Name = "buttonItemMenuExtensionsAddSlides";
			this.buttonItemMenuExtensionsAddSlides.Text = "   test";
			this.buttonItemMenuExtensionsAddSlides.Tooltip = "Add all slides";
			this.buttonItemMenuExtensionsAddSlides.Visible = false;
			this.buttonItemMenuExtensionsAddSlides.Click += new System.EventHandler(this.OnMenuExtensionsAddSlidesClick);
			// 
			// buttonItemMenuExtensionsPrint
			// 
			this.buttonItemMenuExtensionsPrint.BeginGroup = true;
			this.buttonItemMenuExtensionsPrint.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.ExtensionsPrint;
			this.buttonItemMenuExtensionsPrint.Name = "buttonItemMenuExtensionsPrint";
			this.buttonItemMenuExtensionsPrint.Tooltip = "Print file";
			this.buttonItemMenuExtensionsPrint.Visible = false;
			this.buttonItemMenuExtensionsPrint.Click += new System.EventHandler(this.OnMenuExtensionsPrintClick);
			// 
			// buttonItemMenuExtensionsAddVideo
			// 
			this.buttonItemMenuExtensionsAddVideo.BeginGroup = true;
			this.buttonItemMenuExtensionsAddVideo.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.ExtensionsAddVideo;
			this.buttonItemMenuExtensionsAddVideo.Name = "buttonItemMenuExtensionsAddVideo";
			this.buttonItemMenuExtensionsAddVideo.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddVideo.Tooltip = "Add this video to your slide";
			this.buttonItemMenuExtensionsAddVideo.Visible = false;
			this.buttonItemMenuExtensionsAddVideo.Click += new System.EventHandler(this.OnMenuExtensionsAddVideoClick);
			// 
			// buttonItemMenuExtensionsDownloadYouTube
			// 
			this.buttonItemMenuExtensionsDownloadYouTube.BeginGroup = true;
			this.buttonItemMenuExtensionsDownloadYouTube.Image = global::SalesLibraries.Browser.Controls.Properties.Resources.ExtensionsDownloadYouTube;
			this.buttonItemMenuExtensionsDownloadYouTube.Name = "buttonItemMenuExtensionsDownloadYouTube";
			this.buttonItemMenuExtensionsDownloadYouTube.Text = "buttonItem1";
			this.buttonItemMenuExtensionsDownloadYouTube.Tooltip = "Save a copy of this MP4 file";
			this.buttonItemMenuExtensionsDownloadYouTube.Visible = false;
			this.buttonItemMenuExtensionsDownloadYouTube.Click += new System.EventHandler(this.OnMenuExtensionsDownloadYouTubeClick);
			// 
			// barMain
			// 
			this.barMain.AntiAlias = true;
			this.barMain.AutoCreateCaptionMenu = false;
			this.barMain.CanCustomize = false;
			this.barMain.CanDockBottom = false;
			this.barMain.CanDockLeft = false;
			this.barMain.CanDockRight = false;
			this.barMain.CanDockTab = false;
			this.barMain.CanDockTop = false;
			this.barMain.CanMaximizeFloating = false;
			this.barMain.CanMove = false;
			this.barMain.CanReorderTabs = false;
			this.barMain.CanUndock = false;
			this.barMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.barMain.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.barMain.IsMaximized = false;
			this.barMain.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.comboBoxItemSites,
            this.buttonItemFloater,
            this.buttonItemMenuExtensionsAddSlide,
            this.buttonItemMenuExtensionsAddSlides,
            this.buttonItemMenuExtensionsPrint,
            this.buttonItemMenuExtensionsAddVideo,
            this.buttonItemMenuExtensionsDownloadYouTube,
            this.labelItemMenuWarning,
            this.buttonItemMenuNavigationRefresh,
            this.buttonItemMenuNavigationBack,
            this.buttonItemMenuNavigationForward});
			this.barMain.ItemSpacing = 10;
			this.barMain.Location = new System.Drawing.Point(0, 0);
			this.barMain.Name = "barMain";
			this.barMain.Size = new System.Drawing.Size(1800, 33);
			this.barMain.Stretch = true;
			this.barMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barMain.TabIndex = 1;
			this.barMain.TabStop = false;
			this.barMain.Text = "Main Menu";
			// 
			// comboBoxItemSites
			// 
			this.comboBoxItemSites.ComboWidth = 200;
			this.comboBoxItemSites.DropDownHeight = 106;
			this.comboBoxItemSites.ItemHeight = 18;
			this.comboBoxItemSites.Name = "comboBoxItemSites";
			this.comboBoxItemSites.Visible = false;
			this.comboBoxItemSites.SelectedIndexChanged += new System.EventHandler(this.OnSelectedSiteChanged);
			// 
			// labelItemMenuWarning
			// 
			this.labelItemMenuWarning.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelItemMenuWarning.ForeColor = System.Drawing.Color.Gray;
			this.labelItemMenuWarning.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.labelItemMenuWarning.Name = "labelItemMenuWarning";
			// 
			// panelMain
			// 
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 33);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(1800, 474);
			this.panelMain.TabIndex = 2;
			// 
			// SiteBundleControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.barMain);
			this.Name = "SiteBundleControl";
			this.Size = new System.Drawing.Size(1800, 507);
			((System.ComponentModel.ISupportInitialize)(this.barMain)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		public DevComponents.DotNetBar.Bar barMain;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationBack;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationForward;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationRefresh;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlide;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlides;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsPrint;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddVideo;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsDownloadYouTube;
		private DevComponents.DotNetBar.LabelItem labelItemMenuWarning;
		public DevComponents.DotNetBar.ButtonItem buttonItemFloater;
		private System.Windows.Forms.Panel panelMain;
		protected DevComponents.DotNetBar.ComboBoxItem comboBoxItemSites;
	}
}
