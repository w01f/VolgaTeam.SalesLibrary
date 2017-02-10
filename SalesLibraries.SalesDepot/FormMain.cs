using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.SalesDepot.Business.Services;
using SalesLibraries.SalesDepot.Controllers;
using RemoteResourceManager = SalesLibraries.Common.Helpers.RemoteResourceManager;

namespace SalesLibraries.SalesDepot
{
	public partial class FormMain : RibbonForm
	{
		public FormMain()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
			}
		}

		public Image FloaterLogo
		{
			get
			{
				var floaterLogo = labelItemSettingsLogo.Image;
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSearch)
					floaterLogo = labelItemHomeWallbinLogo.Image;
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSearch)
					floaterLogo = labelItemSearchLogo.Image;
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
					floaterLogo = new Bitmap(Configuration.RemoteResourceManager.Instance.CalendarRibbonLogoFile.LocalPath);
				return floaterLogo;
			}
		}

		public string FloaterText => ribbonBarHomeWallbin.Text;

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (Environment.OSVersion.Version.Major < 6) return;
			var attrValue = 1;
			var res = WinAPIHelper.DwmSetWindowAttribute(Handle, WinAPIHelper.DWMWA_TRANSITIONS_FORCEDISABLED, ref attrValue, sizeof(int));
			if (res < 0)
				throw new Exception("Can't disable aero animation");
		}

		public void InitForm()
		{
			Text = string.Format("{0} - User: {1}", MainController.Instance.Settings.SalesDepotName, Environment.UserName);

			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = true;

			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Sales Depot", false, true);
			ConfigureTabPages();

			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void ConfigureTabPages()
		{
			ribbonControl.Items.Clear();
			var tabPages = new List<BaseItem>();
			var configurator = new TabPageConfigurator(Configuration.RemoteResourceManager.Instance.TabSettingsFile.LocalPath);
			foreach (var tabPageConfig in configurator.TabPageSettings)
			{
				switch (tabPageConfig.Id)
				{
					case "Home":
						ribbonTabItemHome.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemHome);
						break;
					case "Search":
						ribbonTabItemSearch.Text = tabPageConfig.Name;
						//tabPages.Add(ribbonTabItemSearch);
						break;
					case "Calendar":
						ribbonTabItemCalendar.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemCalendar);
						break;
					case "Gallery1":
						ribbonTabItemGallery1.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemGallery1);
						break;
					case "Gallery2":
						ribbonTabItemGallery2.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemGallery2);
						break;
					case "Settings":
						ribbonTabItemSettings.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemSettings);
						break;
				}
			}
			ribbonControl.Items.AddRange(tabPages.ToArray());

			switch (MainController.Instance.ActiveTab)
			{
				case TabPageEnum.Home:
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
					break;
				case TabPageEnum.Search:
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemSearch;
					break;
				case TabPageEnum.Calendar:
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemCalendar;
					break;
			}
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			FloaterManager.Instance.ShowFloater(this, MainController.Instance.Settings.SalesDepotName, FloaterLogo, null);
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			var key = TabPageEnum.Home;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
				key = TabPageEnum.Home;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSearch)
				key = TabPageEnum.Search;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				key = TabPageEnum.Calendar;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				key = TabPageEnum.Settings;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGallery1)
				key = TabPageEnum.Gallery1;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGallery2)
				key = TabPageEnum.Gallery2;
			MainController.Instance.ShowTab(key);
		}

		#region Form Event Handlers
		private void FormMain_Shown(object sender, EventArgs e)
		{
			ribbonControl.Enabled = false;
			pnEmpty.BringToFront();

			MainController.Instance.LoadWallbinViews();

			ribbonControl.Enabled = true;
			pnContainer.BringToFront();

			MainController.Instance.ActivateApplication();
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainController.Instance.ActivityManager.AddUserActivity("Application started");
			PowerPointSingleton.Instance.Disconnect();
		}
		#endregion
	}
}