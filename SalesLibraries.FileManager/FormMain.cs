using System;
using System.IO;
using DevComponents.DotNetBar;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager
{
	public partial class FormMain : RibbonForm
	{
		private const string TitleTemplate = "Site Admin ({0})";

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

			labelItemPreferencesLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarPreferencesLogo.RecalcLayout();
			ribbonPanelPreferences.PerformLayout();

			labelItemCalendarLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarCalendarLogo.RecalcLayout();
			ribbonPanelCalendar.PerformLayout();

			labelItemProgramManagerLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarProgramManagerLogo.RecalcLayout();
			ribbonPanelProgramManager.PerformLayout();

			labelItemVideoLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarVideoLogo.RecalcLayout();
			ribbonPanelVideo.PerformLayout();

			labelItemTagsLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarTagsLogo.RecalcLayout();
			ribbonPanelTags.PerformLayout();

			labelItemSecurityLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarSecurityLogo.RecalcLayout();
			ribbonPanelSecurity.PerformLayout();

			labelItemBundlesLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarBundlesLogo.RecalcLayout();
			ribbonPanelBundles.PerformLayout();

			labelItemSettingsLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarSettingsLogo.RecalcLayout();
			ribbonPanelSettings.PerformLayout();

			ConfigureRibbon();
		}

		public void UpdateAppTitle()
		{
			var libraryDirectory = new DirectoryInfo(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.DataSourceFolderPath);

			var title = !String.IsNullOrEmpty(libraryDirectory.Name) ? libraryDirectory.Name : "Site Admin";

			ribbonBarHomeWallbin.Text = title;
			ribbonBarPreferencesLogo.Text = title;
			ribbonBarCalendarLogo.Text = title;
			ribbonBarProgramManagerLogo.Text = title;
			ribbonBarVideoLogo.Text = title;
			ribbonBarTagsLogo.Text = title;
			ribbonBarSecurityLogo.Text = title;
			ribbonBarBundlesLogo.Text = title;
			ribbonBarSettingsLogo.Text = title;
		}

		private void ConfigureRibbon()
		{
			ribbonTabItemCalendar.Visible = MainController.Instance.Settings.EnableOvernightsCalendarTab;
			ribbonTabItemProgramManager.Visible = MainController.Instance.Settings.EnableProgramManagerTab;
			ribbonTabItemVideo.Visible = MainController.Instance.Settings.EnableIPadSettingsTab;
			ribbonTabItemTags.Visible = MainController.Instance.Settings.EnableTagsTab;
			ribbonTabItemSecurity.Visible = MainController.Instance.Settings.EnableSecurityTab;
			ribbonTabItemBundles.Visible = MainController.Instance.Settings.EnableLinkBundlesTab;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void OnFormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			MainController.Instance.ProcessClose();
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
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramManager)
				key = TabPageEnum.ProgramManager;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				key = TabPageEnum.Calendar;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemVideo)
				key = TabPageEnum.VideoManager;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemBundles)
				key = TabPageEnum.Bundles;
			MainController.Instance.ShowTab(key);
		}
	}
}
