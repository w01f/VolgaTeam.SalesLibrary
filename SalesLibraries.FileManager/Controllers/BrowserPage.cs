using System;
using System.Drawing;
using System.Linq;
using DevComponents.DotNetBar;
using SalesLibraries.Browser.Controls.BusinessClasses.Helpers;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Configuration;
using SalesLibraries.FileManager.PresentationLayer.Browser;

namespace SalesLibraries.FileManager.Controllers
{
	class BrowserPage : IPageController
	{
		private BrowserControl _browser;
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public BrowserSettings BrowserSettings { get; private set; }

		public BrowserPage()
		{
			NeedToUpdate = true;
		}

		public void InitController()
		{
			ExternalBrowserManager.Load();
			BrowserSettings = MainController.Instance.Settings.BrowserSettings.Browsers.FirstOrDefault();
			_browser = new BrowserControl(this);
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;

			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(_browser))
				MainController.Instance.MainForm.pnContainer.Controls.Add(_browser);

			_browser.BringToFront();

			if (NeedToUpdate)
			{
				NeedToUpdate = false;
				PowerPointManager.Instance.Init();
				_browser.LoadSites(BrowserSettings?.Sites ?? new SiteSettings[] { }.ToList());
			}
		}

		public void ProcessChanges() { }

		public void OnLibraryChanged(object sender, EventArgs e) { }

		public void UpdateStatusBar()
		{
			_browser.UpdateMainStatusBarInfo();

			MainController.Instance.MainForm.AdditionalInfoContainer.SubItems.Clear();

			foreach (var browserTag in ExternalBrowserManager.AvailableBrowsers.Keys)
			{
				var browserButton = new ButtonItem();
				browserButton.Tag = browserTag;
				browserButton.Click += OnExternalBrowserOpenClick;
				Image buttonImage = null;
				switch (browserTag)
				{
					case ExternalBrowserManager.BrowserChromeTag:
						buttonImage = MainController.Instance.ImageResources.BrowserExternalChrome;
						browserButton.Tooltip = "Chrome";
						break;
					case ExternalBrowserManager.BrowserFirefoxTag:
						buttonImage = MainController.Instance.ImageResources.BrowserExternalFirefox;
						browserButton.Tooltip = "Firefox";
						break;
					case ExternalBrowserManager.BrowserIETag:
						buttonImage = MainController.Instance.ImageResources.BrowserExternalIE;
						browserButton.Tooltip = "IE";
						break;
					case ExternalBrowserManager.BrowserEdgeTag:
						buttonImage = MainController.Instance.ImageResources.BrowserExternalEdge;
						browserButton.Tooltip = "Edge";
						break;
				}
				if (buttonImage != null && buttonImage.Height > 16)
					buttonImage = buttonImage.Resize(new Size(buttonImage.Width, 16));
				browserButton.Image = buttonImage;
				MainController.Instance.MainForm.AdditionalInfoContainer.SubItems.Add(browserButton);
			}

			var emailUrlButton = new ButtonItem();
			emailUrlButton.BeginGroup = true;
			emailUrlButton.Image = MainController.Instance.ImageResources.BrowserUrlEmail;
			emailUrlButton.Click += OnUrlEmail;
			MainController.Instance.MainForm.AdditionalInfoContainer.SubItems.Add(emailUrlButton);

			var copyUrlButton = new ButtonItem();
			copyUrlButton.Image = MainController.Instance.ImageResources.BrowserUrlCopy;
			copyUrlButton.Click += OnUrlCopy;
			MainController.Instance.MainForm.AdditionalInfoContainer.SubItems.Add(copyUrlButton);

			MainController.Instance.MainForm.AdditionalInfoContainer.RecalcSize();
			MainController.Instance.MainForm.StatusBar.RecalcLayout();
		}

		private void OnUrlEmail(object sender, EventArgs e)
		{
			_browser.EmailUrl();
		}

		private void OnUrlCopy(object sender, EventArgs e)
		{
			_browser.CopyUrl();
		}

		private void OnExternalBrowserOpenClick(object sender, EventArgs e)
		{
			var browserButton = (ButtonItem)sender;
			var browserTag = browserButton.Tag as String;
			ExternalBrowserManager.OpenUrl(browserTag, _browser.SelectedSite?.CurrentUrl);
		}
	}
}
