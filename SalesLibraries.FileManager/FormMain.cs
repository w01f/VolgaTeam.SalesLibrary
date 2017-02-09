using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin;

namespace SalesLibraries.FileManager
{
	public partial class FormMain : RibbonForm
	{
		public const string TitleTemplate = "Site Admin ({0})";
		public new Form ActiveForm { get; private set; }
		public FormCompactWallbin CompactWallbinForm { get; private set; }
		public bool IsCompactWallbinView => ActiveForm == CompactWallbinForm;

		public FormMain()
		{
			InitializeComponent();
			Text = String.Format(TitleTemplate, "First Time Setup");
			ribbonControl.Enabled = false;

			Opacity = 0;
			ActiveForm = this;
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

			ConfigureCompactWallbin();

			if (MainController.Instance.Settings.ShowCompactWallbin)
			{
				ShowCompactWallbin();
				DataStateObserver.Instance.DataChanged += (o, e) =>
				{
					if (e.ChangeType != DataChangeType.LibrarySelected) return;
					CompactWallbinForm.LoadData();
				};
			}
			else
				ShowRegularWallbin();
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

			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void ConfigureCompactWallbin()
		{
			CompactWallbinForm = new FormCompactWallbin();
			CompactWallbinForm.BackToRegularWallbin += OnBackToRegularFromCompact;
			CompactWallbinForm.CloseApplication += OnExitClick;
		}

		private void ShowRegularWallbin()
		{
			MainController.Instance.Settings.ShowCompactWallbin = false;
			MainController.Instance.Settings.Save();
			ActiveForm = this;
			Opacity = 1;
		}

		private void ShowCompactWallbin()
		{
			MainController.Instance.Settings.ShowCompactWallbin = true;
			MainController.Instance.Settings.Save();
			ActiveForm = CompactWallbinForm;
			Opacity = 0;
			CompactWallbinForm.Show(this);
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			MainController.Instance.ProcessClose();
		}

		private void OnFloaterClick(object sender, EventArgs e)
		{
			FloaterManager.Instance.ShowFloater(this, Text, MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image, null);
		}

		private void OnCompactWallbinClick(object sender, EventArgs e)
		{
			MainController.Instance.ProcessChanges();

			CompactWallbinForm.LoadData();

			ShowCompactWallbin();

			Utils.ActivateForm(CompactWallbinForm.Handle, false, false);
		}

		private void OnBackToRegularFromCompact(object sender, BackToRegularWallbinEventArgs e)
		{
			CompactWallbinForm.Hide();

			MainController.Instance.Settings.ShowCompactWallbin = true;
			MainController.Instance.Settings.Save();

			ShowRegularWallbin();

			if (e.DataChanged)
				MainController.Instance.ReloadWallbinViews();

			Utils.ActivateForm(Handle, WindowState == FormWindowState.Maximized, false);
		}

		private void OnExitClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OnHelpClick(object sender, EventArgs e)
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
