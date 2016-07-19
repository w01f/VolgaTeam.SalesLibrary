using System;
using DevComponents.DotNetBar;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin
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
