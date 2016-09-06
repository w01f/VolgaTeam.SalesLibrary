using System;
using DevComponents.DotNetBar;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager
{
	public partial class FormMain : RibbonForm
	{
		public FormMain()
		{
			InitializeComponent();
			ribbonControl.Enabled = false;
		}

		public void InitForm()
		{
			Text = String.Format(Text, AppProfileManager.Instance.LibraryAlias);
			ConfigureRibbon();
		}

		private void ConfigureRibbon()
		{
			ribbonTabItemCalendar.Visible = MainController.Instance.Settings.EnableOvernightsCalendarTab;
			ribbonTabItemProgramManager.Visible = MainController.Instance.Settings.EnableProgramManagerTab;
			ribbonTabItemVideo.Visible = MainController.Instance.Settings.EnableIPadSettingsTab;
			ribbonTabItemTags.Visible = MainController.Instance.Settings.EnableTagsTab;
			ribbonTabItemSecurity.Visible = MainController.Instance.Settings.EnableSecurityTab;
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
			MainController.Instance.ShowTab(key);
		}
	}
}
