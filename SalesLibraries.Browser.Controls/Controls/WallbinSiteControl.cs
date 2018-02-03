using System;
using System.Diagnostics;
using System.Windows.Forms;
using SalesLibraries.Browser.Controls.BusinessClasses.Events;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects;
using SalesLibraries.Browser.Controls.Controls.WebPage;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace SalesLibraries.Browser.Controls.Controls
{
	public partial class WallbinSiteControl : UserControl, ISiteContainer
	{
		public SiteBundleControl ParentBundle { get; }
		public SiteSettings SiteSettings { get; private set; }
		public string CurrentUrl => SiteSettings?.BaseUrl;

		public WallbinSiteControl(SiteBundleControl parentBundle)
		{
			InitializeComponent();

			ParentBundle = parentBundle;
			xtraTabControl.SelectedPageChanged += OnSelectedPageChanged;
			xtraTabControl.CloseButtonClick += OnWebPageCloseButtonClick;
		}

		public void InitSite(SiteSettings siteSettings)
		{
			SiteSettings = siteSettings;

			var webPage = CreateWebPage(SiteSettings.BaseUrl);
			webPage.ShowCloseButton = DefaultBoolean.False;
			xtraTabControl.TabPages.Add(webPage);
			webPage.Navigate();

			UpdateTabControlState();
		}

		#region Web Page Management

		public WebKitPage SelectedWebPage => xtraTabControl.SelectedTabPage as WebKitPage;

		private WebKitPage CreateWebPage(string url)
		{
			var webPage = new WebKitPage(this, url);
			webPage.OnNavigateNewPage += OnNavigateNewPage;
			webPage.OnClosePage += OnClosePage;
			return webPage;
		}

		private void RemoveTabPage(ClosePageEventArgs args)
		{
			if (args.NeedReleasePage)
				args.Page.Release();
			xtraTabControl.TabPages.Remove(args.Page);
			UpdateTabControlState();
		}

		private void UpdateTabControlState()
		{
			xtraTabControl.ShowTabHeader = xtraTabControl.TabPages.Count > 1 ? DefaultBoolean.True : DefaultBoolean.False;
		}

		public void SuspendPages()
		{
			xtraTabControl.Enabled = false;
		}

		public void ResumePages()
		{
			xtraTabControl.Enabled = true;
		}

		private void OnNavigateNewPage(object sender, NewPageEventArgs e)
		{
			var webPage = CreateWebPage(e.Url);
			xtraTabControl.TabPages.Add(webPage);
			xtraTabControl.SelectedTabPage = webPage;
			UpdateTabControlState();

			webPage.Navigate();
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			UpdateNavigationButtons();
			UpdateExtensionsState();
			UpdateYouTubeState();
		}

		private void OnWebPageCloseButtonClick(object sender, EventArgs e)
		{
			var args = (ClosePageButtonEventArgs)e;
			RemoveTabPage(new ClosePageEventArgs
			{
				Page = (WebKitPage)args.Page,
				NeedReleasePage = true
			});
		}

		private void OnClosePage(object sender, ClosePageEventArgs e)
		{
			RemoveTabPage(e);
		}
		#endregion

		#region Navigation

		public void UpdateNavigationButtons()
		{
			SelectedWebPage?.UpdateNavigationButtonsState();
		}

		public void NavigateBack()
		{
			SelectedWebPage?.NavigateBack();
		}

		public void NavigateForward()
		{
			SelectedWebPage?.NavigateForward();
		}

		public void RefreshPage()
		{
			SelectedWebPage?.RefreshPage();
		}
		#endregion

		#region Url Details
		public void CopyUrl()
		{
			try
			{
				Clipboard.SetText(SelectedWebPage?.CurrentUrl ?? "empty");
				ParentBundle.PopupMessageHelper.ShowInfo("Url successfully copied");
			}
			catch
			{
				ParentBundle.PopupMessageHelper.ShowWarning("Url is not loaded");
			}
		}

		public void EmailUrl()
		{
			try
			{
				Process.Start(Uri.EscapeUriString(String.Format("mailto:{0}?Body={1}", String.Empty, SelectedWebPage?.CurrentUrl ?? "Empty")));
			}
			catch { }
		}
		#endregion

		#region Slide Content Extension
		public void UpdateExtensionsState()
		{
			SelectedWebPage?.UpdateViewContentState();
		}
		#endregion

		#region YouTube Extensions
		public void UpdateYouTubeState()
		{
			SelectedWebPage?.UpdateYouTubeState();
		}
		#endregion
	}
}
