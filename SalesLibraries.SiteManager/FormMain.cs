using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.SiteManager.Controllers;

namespace SalesLibraries.SiteManager
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;
		public static FormMain Instance => _instance ?? (_instance = new FormMain());

		public FormMain()
		{
			InitializeComponent();

			Width = (Int32)(Screen.PrimaryScreen.Bounds.Width * 0.8);
			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			FormStateHelper.Init(this, Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "Site Manager", false, true);
		}

		private void ConfigureRibbon()
		{
			RibbonTabItem defaultTab = null;
			TabPageEnum defaultTabType;
			if (SettingsManager.Instance.RibbonTabPageSettings.Any())
			{
				ribbonControl.Items.Clear();
				foreach (var tabPageConfig in SettingsManager.Instance.RibbonTabPageSettings)
				{
					ButtonItem currentTab = null;

					if (!tabPageConfig.Visible) continue;

					switch (tabPageConfig.Id)
					{
						case RibbonTabIdentifiers.Users:
							currentTab = ribbonTabItemUsers;
							break;
						case RibbonTabIdentifiers.Activities:
							currentTab = ribbonTabItemActivities;
							break;
						case RibbonTabIdentifiers.ActiveLibraries:
							currentTab = ribbonTabItemLibraries;
							break;
						case RibbonTabIdentifiers.InactiveUsers:
							currentTab = ribbonTabItemInactiveUsers;
							break;
						case RibbonTabIdentifiers.LinkConfigProfiles:
							currentTab = ribbonTabItemLinkConfigProfiles;
							break;
						case RibbonTabIdentifiers.QuickSites:
							currentTab = ribbonTabItemQBuilder;
							break;
						case RibbonTabIdentifiers.Utilities:
							currentTab = ribbonTabItemUtilities;
							break;
					}

					if (currentTab == null) continue;

					currentTab.Visible = true;
					currentTab.Enabled = tabPageConfig.Enabled;
					currentTab.Text = tabPageConfig.Name;
					ribbonControl.Items.Add(currentTab);
					if (defaultTab == null)
						defaultTab = (RibbonTabItem)currentTab;
				}
			}

			if (SettingsManager.Instance.SelectedTab.HasValue)
			{
				defaultTabType = (TabPageEnum)SettingsManager.Instance.SelectedTab.Value;
				switch (defaultTabType)
				{
					case TabPageEnum.Users:
						defaultTab = ribbonTabItemUsers;
						break;
					case TabPageEnum.Activities:
						defaultTab = ribbonTabItemActivities;
						break;
					case TabPageEnum.LibraryFiles:
						defaultTab = ribbonTabItemLibraries;
						break;
					case TabPageEnum.InactiveUsers:
						defaultTab = ribbonTabItemInactiveUsers;
						break;
					case TabPageEnum.LinkConfigProfiles:
						defaultTab = ribbonTabItemLinkConfigProfiles;
						break;
					case TabPageEnum.QBuilder:
						defaultTab = ribbonTabItemQBuilder;
						break;
					case TabPageEnum.Utilities:
						defaultTab = ribbonTabItemUtilities;
						break;
				}
			}
			else
			{
				if (defaultTab == ribbonTabItemUsers)
					defaultTabType = TabPageEnum.Users;
				else if (defaultTab == ribbonTabItemActivities)
					defaultTabType = TabPageEnum.Activities;
				else if (defaultTab == ribbonTabItemLibraries)
					defaultTabType = TabPageEnum.LibraryFiles;
				else if (defaultTab == ribbonTabItemInactiveUsers)
					defaultTabType = TabPageEnum.InactiveUsers;
				else if (defaultTab == ribbonTabItemLinkConfigProfiles)
					defaultTabType = TabPageEnum.LinkConfigProfiles;
				else if (defaultTab == ribbonTabItemQBuilder)
					defaultTabType = TabPageEnum.QBuilder;
				else if (defaultTab == ribbonTabItemUtilities)
					defaultTabType = TabPageEnum.Utilities;
				else
					defaultTabType = TabPageEnum.Utilities;
			}

			ribbonControl.SelectedRibbonTabItem = defaultTab;
			MainController.Instance.ShowTab(defaultTabType);

			ribbonControl.Expanded = true;
		}

		private void OnFormMainLoad(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.IconPath))
				Icon = new Icon(SettingsManager.Instance.IconPath);

			if (File.Exists(SettingsManager.Instance.LogoPath))
			{
				var image = new Bitmap(SettingsManager.Instance.LogoPath);
				labelItemUsersLogo.Image = image;
				ribbonBarUsersLogo.RecalcLayout();
				ribbonPanelUsers.PerformLayout();

				labelItemActivitiesLogo.Image = image;
				ribbonBarActivitiesLogo.RecalcLayout();
				ribbonPanelActivities.PerformLayout();

				labelItemLinkConfigProfilesLogo.Image = image;
				ribbonBarLinkConfigProfilesLogo.RecalcLayout();
				ribbonPanelLinkConfigProfiles.PerformLayout();

				labelItemLibrariesLogo.Image = image;
				ribbonBarLibrariesLogo.RecalcLayout();
				ribbonPanelLibraries.PerformLayout();

				labelItemInactiveUsersLogo.Image = image;
				ribbonBarInactiveUsersLogo.RecalcLayout();
				ribbonPanelInactiveUsers.PerformLayout();

				labelItemQBuilderLogo.Image = image;
				ribbonBarQBuilderLogo.RecalcLayout();
				ribbonPanelQBuilder.PerformLayout();

				labelItemUtilitiesLogo.Image = image;
				ribbonBarUtilitiesLogo.RecalcLayout();
				ribbonPanelUtilities.PerformLayout();
			}

			MainController.Instance.InitializeControllers();
			MainController.Instance.LoadDataAndGUI();
			ConfigureRibbon();

			ribbonControl.SelectedRibbonTabChanged += OnSelectedRibbonTabChanged;
		}

		private void OnSelectedRibbonTabChanged(object sender, EventArgs e)
		{
			var key = TabPageEnum.Users;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemUsers)
				key = TabPageEnum.Users;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemActivities)
				key = TabPageEnum.Activities;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemLinkConfigProfiles)
				key = TabPageEnum.LinkConfigProfiles;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemLibraries)
				key = TabPageEnum.LibraryFiles;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemInactiveUsers)
				key = TabPageEnum.InactiveUsers;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemQBuilder)
				key = TabPageEnum.QBuilder;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemUtilities)
				key = TabPageEnum.Utilities;
			MainController.Instance.ShowTab(key);
		}

		private void OnExitClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}