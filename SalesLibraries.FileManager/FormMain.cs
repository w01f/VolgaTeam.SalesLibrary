using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.FileManager.Configuration;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin;

namespace SalesLibraries.FileManager
{
	public partial class FormMain : RibbonForm
	{
		public const string TitleTemplate = "{0} ({1})";
		public new Form ActiveForm { get; private set; }
		public FormCompactWallbin CompactWallbinForm { get; private set; }
		public bool IsCompactWallbinView => ActiveForm == CompactWallbinForm;

		public Bar StatusBar => barBottom;
		public ItemContainer MainInfoContainer => itemContainerStatusBarMainContentInfo;
		public ItemContainer AdditionalInfoContainer => itemContainerStatusBarAdditionalContentInfo;

		public FormMain()
		{
			InitializeComponent();
			Text = String.Format(TitleTemplate, "Site Admin", "First Time Setup");
			ribbonControl.Enabled = false;

			Opacity = 0;
			ActiveForm = this;

			KeyPreview = true;
			KeyUp += OnFormKeyUp;

			Width = (Int32)(Screen.PrimaryScreen.Bounds.Width * 0.7);
			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.7);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;
		}

		public void InitForm()
		{
			Text = String.Format(TitleTemplate, MainController.Instance.Settings.AppTitle, AppProfileManager.Instance.LibraryAlias);
			Icon = MainController.Instance.ImageResources.AppIcon ?? Icon;

			labelItemHomeLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarHomeLogo.RecalcLayout();
			ribbonPanelHome.PerformLayout();

			labelItemPreferencesLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarPreferencesLogo.RecalcLayout();
			ribbonPanelPreferences.PerformLayout();

			labelItemCalendarLogo.Image = MainController.Instance.ImageResources.AppRibbonLogo ?? labelItemHomeLogo.Image;
			ribbonBarCalendarLogo.RecalcLayout();
			ribbonPanelCalendar.PerformLayout();

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

			if (MainController.Instance.Settings.MainFormStyle.AccentColor.HasValue)
				styleManager.MetroColorParameters = new MetroColorGeneratorParameters(
					styleManager.MetroColorParameters.CanvasColor,
					MainController.Instance.Settings.MainFormStyle.AccentColor.Value);

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

			ribbonBarHomeLogo.Text =
			ribbonBarPreferencesLogo.Text =
			ribbonBarCalendarLogo.Text =
			ribbonBarVideoLogo.Text =
			ribbonBarTagsLogo.Text =
			ribbonBarSecurityLogo.Text =
			ribbonBarBundlesLogo.Text =
			ribbonBarSettingsLogo.Text = title;

			ribbonBarHomeLogo.RecalcLayout();
			ribbonPanelHome.PerformLayout();
			ribbonBarPreferencesLogo.RecalcLayout();
			ribbonPanelPreferences.PerformLayout();
			ribbonBarCalendarLogo.RecalcLayout();
			ribbonPanelCalendar.PerformLayout();
			ribbonBarVideoLogo.RecalcLayout();
			ribbonPanelVideo.PerformLayout();
			ribbonBarTagsLogo.RecalcLayout();
			ribbonPanelTags.PerformLayout();
			ribbonBarSecurityLogo.RecalcLayout();
			ribbonPanelSecurity.PerformLayout();
			ribbonBarBundlesLogo.RecalcLayout();
			ribbonPanelBundles.PerformLayout();
			ribbonBarSettingsLogo.RecalcLayout();
			ribbonPanelSettings.PerformLayout();

			ribbonBarHomeLinkSettings.OverflowButtonText = "Link Tools";
			ribbonBarHomeLinkSettings.RecalcLayout();
			ribbonPanelHome.PerformLayout();
		}

		private void ConfigureRibbon()
		{
			RibbonTabItem defaultTab = null;
			ribbonControl.Items.Clear();
			foreach (var tabPageConfig in MainController.Instance.Settings.RibbonTabPageSettings)
			{
				ButtonItem currentTab = null;

				if (!tabPageConfig.Visible) continue;

				switch (tabPageConfig.Id)
				{
					case RibbonTabIdentifiers.MainMenu:
						currentTab = applicationButtonApplicationMenu;
						break;
					case RibbonTabIdentifiers.Home:
						currentTab = ribbonTabItemHome;
						break;
					case RibbonTabIdentifiers.Preferences:
						currentTab = ribbonTabItemPreferences;
						break;
					case RibbonTabIdentifiers.Tags:
						currentTab = ribbonTabItemTags;
						break;
					case RibbonTabIdentifiers.Security:
						currentTab = ribbonTabItemSecurity;
						break;
					case RibbonTabIdentifiers.Video:
						currentTab = ribbonTabItemVideo;
						break;
					case RibbonTabIdentifiers.LinkBundles:
						currentTab = ribbonTabItemBundles;
						break;
					case RibbonTabIdentifiers.Settings:
						currentTab = ribbonTabItemSettings;
						break;
					case RibbonTabIdentifiers.Calendar:
						currentTab = ribbonTabItemCalendar;
						break;
					case RibbonTabIdentifiers.Browser:
						currentTab = ribbonTabItemBrowser;
						break;
				}

				if (currentTab == null) continue;

				currentTab.Visible = true;
				currentTab.Enabled = tabPageConfig.Enabled;
				currentTab.Text = tabPageConfig.Name;
				ribbonControl.Items.Add(currentTab);
				if (defaultTab == null)
					defaultTab = currentTab as RibbonTabItem;
			}

			ribbonControl.SelectedRibbonTabItem = defaultTab;

			ribbonControl.Items.Add(buttonItemExpand);
			ribbonControl.Items.Add(buttonItemCollapse);
			ribbonControl.Items.Add(buttonItemPin);

			ribbonControl.SelectedRibbonTabChanged += OnSelectedRibbonTabChanged;

			ribbonControl.Expanded = true;
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

		private void OnSaveClick(object sender, EventArgs e)
		{
			MainController.Instance.ProcessChanges();
		}

		private void OnFormKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				MainController.Instance.WallbinViews.Selection.ResetAll();
		}

		private void OnSelectedRibbonTabChanged(object sender, EventArgs e)
		{
			var key = TabPageEnum.Home;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
				key = TabPageEnum.Home;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTags)
				key = TabPageEnum.Tags;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSecurity)
				key = TabPageEnum.Security;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPreferences)
				key = TabPageEnum.Preferences;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				key = TabPageEnum.Settings;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				key = TabPageEnum.Calendar;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemVideo)
				key = TabPageEnum.VideoManager;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemBundles)
				key = TabPageEnum.Bundles;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemBrowser)
				key = TabPageEnum.Browser;

			MainController.Instance.ShowTab(key);

			switch (key)
			{
				case TabPageEnum.Browser:
					if (!_lastRibbonExpanded.HasValue)
						_lastRibbonExpanded = ribbonControl.Expanded;
					_allowRibbonStateChangeProcessing = false;
					ribbonControl.AutoExpand = false;
					ribbonControl.Expanded = false;
					buttonItemExpand.Visible = false;
					buttonItemCollapse.Visible = false;
					buttonItemPin.Visible = false;
					ribbonControl.RecalcLayout();
					Application.DoEvents();
					_allowRibbonStateChangeProcessing = true;
					break;
				default:
					if (_lastRibbonExpanded.HasValue)
					{
						ribbonControl.Expanded = _lastRibbonExpanded.Value;
						_lastRibbonExpanded = null;
					}
					ribbonControl.AutoExpand = true;
					OnRibbonExpandedChanged(sender, e);
					break;
			}
		}

		#region Expand/Collapse Processing
		private bool? _lastRibbonExpanded = true;
		private bool _allowRibbonStateChangeProcessing = true;

		private void OnRibbonExpandedChanged(object sender, EventArgs e)
		{
			if (!_allowRibbonStateChangeProcessing) return;
			buttonItemExpand.Visible = !ribbonControl.Expanded;
			buttonItemCollapse.Visible = ribbonControl.Expanded;
			buttonItemPin.Visible = false;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonAfterPanelPopup(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = false;
			buttonItemCollapse.Visible = false;
			buttonItemPin.Visible = true;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonAfterPanelPopupClose(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = !ribbonControl.Expanded;
			buttonItemCollapse.Visible = ribbonControl.Expanded;
			buttonItemPin.Visible = false;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonExpandClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = true;
		}

		private void OnRibbonCollapseClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = false;
		}

		private void OnRibbonPinClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = true;
		}
		#endregion
	}
}
