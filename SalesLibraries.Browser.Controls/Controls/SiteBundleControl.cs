using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Browser.Controls.BusinessClasses.Enums;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects;
using SalesLibraries.Browser.Controls.Properties;
using DevComponents.DotNetBar;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.Floater;

namespace SalesLibraries.Browser.Controls.Controls
{
	public abstract partial class SiteBundleControl : UserControl
	{
		public abstract PowerPointSingleton PowerPointSingleton { get; }
		public abstract PopupMessageHelper PopupMessageHelper { get; }
		public abstract BackgroundProcessManager ProcessManager { get; }
		public abstract Form MainForm { get; }
		public abstract void ShowFloater(FloaterRequestedEventArgs args);
		public abstract bool CheckPowerPointRunning(Action afterRun);

		public virtual Image SplashLogo => Resources.ProgressLogo;

		protected SiteBundleControl()
		{
			InitializeComponent();
		}

		private void OnFloaterClick(object sender, EventArgs e)
		{
			ShowFloater(new FloaterRequestedEventArgs());
		}

		public abstract void UpdateMainStatusBarInfo();

		#region Sites Management

		public ISiteContainer SelectedSite
		{
			get
			{
				var siteSettings = comboBoxItemSites.SelectedItem as SiteSettings;
				return panelMain.Controls.OfType<ISiteContainer>().FirstOrDefault(sc => sc.SiteSettings.Id == siteSettings?.Id);
			}
		}

		private WallbinSiteControl SelectedWallbinSite => SelectedSite as WallbinSiteControl;

		public void LoadSites(IList<SiteSettings> siteSettings)
		{
			if (comboBoxItemSites.Items.Count > 0) return;

			comboBoxItemSites.Items.AddRange(siteSettings.ToArray());
			if (siteSettings.Any())
				comboBoxItemSites.SelectedIndex = 0;

			if (comboBoxItemSites.Items.Count == 1)
				comboBoxItemSites.Visible = false;
		}

		private void OnSelectedSiteChanged(object sender, EventArgs e)
		{
			var comboBox = sender as ComboBoxItem;
			if (!(comboBox?.SelectedItem is SiteSettings selectedSiteSettings)) return;

			UpdateMainStatusBarInfo();

			var siteContainer = panelMain.Controls.OfType<ISiteContainer>().FirstOrDefault(sc => sc.SiteSettings.Id == selectedSiteSettings.Id);
			if (siteContainer == null)
			{
				switch (selectedSiteSettings.SiteType)
				{
					case SiteType.SalesCloud:
						var mediaSiteContainer = new WallbinSiteControl(this);
						mediaSiteContainer.Dock = DockStyle.Fill;
						panelMain.Controls.Add(mediaSiteContainer);
						mediaSiteContainer.InitSite(selectedSiteSettings);
						siteContainer = mediaSiteContainer;
						break;
					case SiteType.SimpleSite:
						var simpleSiteControl = new SimpleSiteControl(selectedSiteSettings, this);
						simpleSiteControl.Dock = DockStyle.Fill;
						panelMain.Controls.Add(simpleSiteControl);
						simpleSiteControl.InitSite();
						siteContainer = simpleSiteControl;
						break;
					default:
						throw new ArgumentOutOfRangeException("Undefined site type");
				}
			}
			((Control)siteContainer).BringToFront();
			siteContainer.UpdateNavigationButtons();
			siteContainer.UpdateExtensionsState();
			siteContainer.UpdateYouTubeState();
		}
		#endregion

		#region Navigation
		public ButtonItem ButtonNavigationRefresh => buttonItemMenuNavigationRefresh;
		public ButtonItem ButtonNavigationBack => buttonItemMenuNavigationBack;
		public ButtonItem ButtonNavigationForward => buttonItemMenuNavigationForward;

		private void OnMenuNavigationBackClick(object sender, EventArgs e)
		{
			SelectedSite?.NavigateBack();
		}

		private void OnMenuNavigationForwardClick(object sender, EventArgs e)
		{
			SelectedSite?.NavigateForward();
		}

		private void OnMenuNavigationRefreshClick(object sender, EventArgs e)
		{
			SelectedSite?.RefreshPage();
		}
		#endregion

		#region Url Details
		public void CopyUrl()
		{
			try
			{
				Clipboard.SetText(SelectedSite?.CurrentUrl ?? "empty");
				PopupMessageHelper.ShowInfo("Url successfully copied");
			}
			catch
			{
				PopupMessageHelper.ShowWarning("Url is not loaded");
			}
		}

		public void EmailUrl()
		{
			try
			{
				Process.Start(Uri.EscapeUriString(String.Format("mailto:{0}?Body={1}", String.Empty, SelectedSite?.CurrentUrl ?? "Empty")));
			}
			catch { }
		}
		#endregion

		#region Slide Content Extension
		public ButtonItem ButtonExtensionsAddSlide => buttonItemMenuExtensionsAddSlide;
		public ButtonItem ButtonExtensionsAddSlides => buttonItemMenuExtensionsAddSlides;
		public ButtonItem ButtonExtensionsPrint => buttonItemMenuExtensionsPrint;
		public ButtonItem ButtonExtensionsAddVideo => buttonItemMenuExtensionsAddVideo;
		public LabelItem LabelExtensionsWarning => labelItemMenuWarning;

		private void OnMenuExtensionsAddSlideClick(object sender, EventArgs e)
		{
			SelectedWallbinSite?.SelectedWebPage?.AddSlide();
		}

		private void OnMenuExtensionsAddSlidesClick(object sender, EventArgs e)
		{
			SelectedWallbinSite?.SelectedWebPage?.AddSlides();
		}

		private void OnMenuExtensionsPrintClick(object sender, EventArgs e)
		{
			SelectedWallbinSite?.SelectedWebPage?.Print();
		}

		private void OnMenuExtensionsAddVideoClick(object sender, EventArgs e)
		{
			SelectedWallbinSite?.SelectedWebPage?.AddVideo();
		}
		#endregion

		#region YouTube Extensions
		public ButtonItem ButtonExtensionsDownloadYouTube => buttonItemMenuExtensionsDownloadYouTube;

		private void OnMenuExtensionsDownloadYouTubeClick(object sender, EventArgs e)
		{
			SelectedWallbinSite?.SelectedWebPage?.DownloadYouTube();
		}
		#endregion
	}
}
