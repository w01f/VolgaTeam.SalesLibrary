using System;
using DevComponents.DotNetBar;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin
{
	public partial class FormMain : RibbonForm
	{
		private const string TitleTemplate = "Cloud Admin ({0})";

		public FormMain()
		{
			InitializeComponent();
			Text = String.Format(TitleTemplate, "First Time Setup");
			ribbonControl.Enabled = false;
		}

		public void InitForm()
		{
			Text = String.Format(TitleTemplate, AppProfileManager.Instance.LibraryAlias);
			Icon = MainController.Instance.ImageResources.AppIcon ?? Icon;

			labelItemHomeLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarHomeWallbin.RecalcLayout();
			ribbonPanelHome.PerformLayout();

			labelItemSecurityLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarSecurityLogo.RecalcLayout();
			ribbonPanelSecurity.PerformLayout();

			ConfigureRibbon();
		}

		private void ConfigureRibbon()
		{
			ribbonTabItemVideo.Visible = MainController.Instance.Settings.EnableIPadSettingsTab;
			ribbonTabItemTags.Visible = MainController.Instance.Settings.EnableTagsTab;
			ribbonTabItemSecurity.Visible = MainController.Instance.Settings.EnableSecurityTab;
		}

		private void OnFormMainShown(object sender, EventArgs e)
		{
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonItemHelp_Click(object sender, EventArgs e)
		{
			MainController.Instance.HelpManager.OpenHelpLink("Ribbon");
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			var key = TabPageEnum.Home;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
				key = TabPageEnum.Home;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTags)
				key = TabPageEnum.Tags;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSecurity)
				key = TabPageEnum.Security;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPreferences)
				key = TabPageEnum.Preferences;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				key = TabPageEnum.Settings;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemVideo)
				key = TabPageEnum.VideoManager;
			MainController.Instance.ShowTab(key);
		}
	}
}
