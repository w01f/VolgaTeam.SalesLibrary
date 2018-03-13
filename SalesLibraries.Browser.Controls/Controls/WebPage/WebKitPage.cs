using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Browser.Controls.BusinessClasses.Events;
using SalesLibraries.Browser.Controls.BusinessClasses.Helpers;
using DevExpress.Utils;
using DevExpress.XtraTab;
using EO.WebBrowser;
using EO.WinForm;
using SalesLibraries.Browser.Controls.ToolForms;
using Padding = System.Windows.Forms.Padding;

namespace SalesLibraries.Browser.Controls.Controls.WebPage
{
	public sealed partial class WebKitPage : XtraTabPage
		//public sealed partial class WebKitPage : UserControl
	{
		private readonly WallbinSiteControl _siteContainer;
		private readonly WebControl _webKit;
		private readonly string _startUrl;
		private bool _initialLoadComplete;

		public event EventHandler<NewPageEventArgs> OnNavigateNewPage;
		public event EventHandler<ClosePageEventArgs> OnClosePage;

		public string CurrentUrl => _webKit.WebView.Url;

		private WebKitPage()
		{
			InitializeComponent();
			_webKit = new WebControl();
			_webKit.Dock = DockStyle.Fill;
			Controls.Add(_webKit);
			_webKit.WebView = new WebView();
			_webKit.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Text = _webKit.WebView.Title;
		}

		public WebKitPage(WallbinSiteControl siteContainer, string url) : this()
		{
			_siteContainer = siteContainer;
			_startUrl = url;
			pbProgressLogo.Image = _siteContainer.ParentBundle.SplashLogo;
			ExtensionsManager.MakeUrlTrusted(_startUrl);
			InitSiteLoading();
			InitDownloading();
			InitExternalBrowsersCommandIds();
			InitExtensions();
		}

		#region Site Loading
		public void Navigate()
		{
			ResizeProgressBar();
			pnProgress.BringToFront();
			circularProgress.IsRunning = true;
			Application.DoEvents();
			_webKit.WebView.LoadUrl(_startUrl);
		}

		private void InitSiteLoading()
		{
			_webKit.WebView.TitleChanged += OnWebViewTitleChanged;
			_webKit.WebView.LoadCompleted += OnWebViewLoadCompleted;
			_webKit.WebView.LoadFailed += OnWebViewLoadFailed;
			_webKit.WebView.CertificateError += OnWebViewCertificateError;
			_webKit.WebView.UrlChanged += OnWebViewUrlChanged;
			_webKit.WebView.BeforeContextMenu += OnWebViewBeforeContextMenu;
			_webKit.WebView.NewWindow += OnWebViewNewWindow;
			_webKit.WebView.LaunchUrl += OnWebViewLaunchUrl;
			_webKit.WebView.CanGoBackChanged += OnWebViewNavigationStateChaged;
			_webKit.WebView.CanGoForwardChanged += OnWebViewNavigationStateChaged;
			_webKit.WebView.BeforeContextMenu += OnWebViewBeforeContextMenu;
			_webKit.WebView.Command += OnWebViewExternalBrowserOpenCommand;
		}

		public void Release()
		{
			_webKit.WebView.Close(true);
		}

		private void OnWebViewTitleChanged(object sender, EventArgs eventArgs)
		{
			Text = ((WebView)sender).Title;
		}

		private void OnWebViewBeforeContextMenu(object sender, BeforeContextMenuEventArgs e)
		{
			if (_siteContainer.SiteSettings.EnableMenu)
			{
				if (!String.IsNullOrEmpty(e.MenuInfo.LinkUrl) &&
					_extensionsManager.IsExtensionsActive &&
					_extensionsManager.IsUrlExternal(e.MenuInfo.LinkUrl))
				{
					e.Menu.Items.Clear();
					foreach (var commandId in _externalBrowsersCommandIds)
						e.Menu.Items.Add(new EO.WebBrowser.MenuItem(commandId.Key, commandId.Value));
				}
			}
			else
				e.Menu.Items.Clear();
		}

		private void OnWebViewLoadFailed(object sender, LoadFailedEventArgs e)
		{
			if (!_initialLoadComplete && ShowCloseButton != DefaultBoolean.False && OnClosePage != null)
				OnClosePage(this, new ClosePageEventArgs { Page = this, NeedReleasePage = e.ErrorCode != ErrorCode.ProceedAsDownload });
			else
			{
				circularProgress.IsRunning = false;
				pnProgress.SendToBack();
				_webKit.BringToFront();
			}
		}

		private void OnWebViewCertificateError(Object sender, CertificateErrorEventArgs e)
		{
			e.Continue();
		}

		private void OnWebViewLoadCompleted(object sender, LoadCompletedEventArgs e)
		{
			circularProgress.IsRunning = false;
			pnProgress.SendToBack();
			_webKit.BringToFront();
			UpdateYouTubeState();
			_initialLoadComplete = true;
		}

		private void OnWebViewUrlChanged(object sender, EventArgs e)
		{
			UpdateYouTubeState();
		}

		private void OnWebViewNewWindow(object sender, NewWindowEventArgs e)
		{
			OnNavigateNewPage?.Invoke(this, new NewPageEventArgs() { Url = e.TargetUrl });
			e.Accepted = false;
		}

		private void OnWebViewLaunchUrl(object sender, LaunchUrlEventArgs e)
		{
			e.UseOSHandler = true;
		}
		#endregion

		#region Download handling
		private void InitDownloading()
		{
			_webKit.WebView.FileDialog += OnProcessFileDialog;
			_webKit.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_webKit.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_webKit.WebView.DownloadCompleted += OnWebViewDownloadCompleted;
			_webKit.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
		}

		private void OnProcessFileDialog(object sender, FileDialogEventArgs e)
		{
			switch (e.Mode)
			{
				case FileDialogMode.Save:
					using (var saveDialog = new SaveFileDialog())
					{
						saveDialog.Title = e.Title;
						saveDialog.Filter = e.Filter;
						saveDialog.FileName = Path.GetFileName(e.DefaultFileName);
						saveDialog.InitialDirectory = Path.Combine(
							Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
							"Downloads");
						if (saveDialog.ShowDialog(_siteContainer.ParentBundle.MainForm) != DialogResult.Cancel)
						{
							FormDownloadProgress.ShowProgress(_siteContainer.ParentBundle.MainForm);
							Application.DoEvents();
							FormDownloadProgress.SetTitle("Downloading…");
							Application.DoEvents();
							FormDownloadProgress.SetDetails(Path.GetFileName(saveDialog.FileName));
							Application.DoEvents();
							_siteContainer.SuspendPages();
							Application.DoEvents();
							e.Continue(saveDialog.FileName);
						}
						else
							e.Cancel();
					}
					break;
			}
			e.Handled = true;
		}

		private void OnWebViewBeforeDownload(object sender, BeforeDownloadEventArgs e)
		{
			e.FilePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				"Downloads",
				Path.GetFileName(e.FilePath));
		}

		private void OnWebViewDownloadUpdated(object sender, DownloadEventArgs e)
		{
			Application.DoEvents();
			FormDownloadProgress.SetDetails(String.Format("{0} - {1}%", Path.GetFileName(e.Item.FullPath), e.Item.PercentageComplete));
			Application.DoEvents();
		}

		private void OnWebViewDownloadCompleted(object sender, DownloadEventArgs e)
		{
			_siteContainer.ResumePages();
			FormDownloadProgress.CloseProgress();
			if (".MP4".Equals(Path.GetExtension(e.Item.FullPath), StringComparison.OrdinalIgnoreCase))
			{
				if (HandleVideoDownloaded(e.Item.FullPath))
					return;
			}
			using (var formComplete = new FormFileDownloadComplete(e.Item.FullPath))
			{
				formComplete.ShowDialog(_siteContainer.ParentBundle.MainForm);
			}
		}

		private void OnWebViewDownloadCanceled(object sender, DownloadEventArgs e)
		{
			_siteContainer.ResumePages();
			FormDownloadProgress.CloseProgress();
		}
		#endregion

		#region Navigation
		public void UpdateNavigationButtonsState()
		{
			_siteContainer.ParentBundle.ButtonNavigationBack.Enabled = _webKit.WebView.CanGoBack;
			_siteContainer.ParentBundle.ButtonNavigationForward.Enabled = _webKit.WebView.CanGoForward;
			_siteContainer.ParentBundle.barMain.RecalcLayout();
		}

		private void OnWebViewNavigationStateChaged(object sender, EventArgs e)
		{
			UpdateNavigationButtonsState();
		}

		public void NavigateBack()
		{
			_webKit.WebView.GoBack();
		}

		public void NavigateForward()
		{
			_webKit.WebView.GoForward();
		}

		public void RefreshPage()
		{
			pnProgress.BringToFront();
			circularProgress.IsRunning = true;
			_webKit.WebView.Reload();
		}
		#endregion

		#region External Web Browsers
		private readonly Dictionary<string, int> _externalBrowsersCommandIds = new Dictionary<String, Int32>();

		private int _commandIdOpenChrome;
		private int _commandIdOpenFirefox;
		private int _commandIdOpenIE;
		private int _commandIdOpenEdge;

		private void InitExternalBrowsersCommandIds()
		{
			foreach (var browserTag in ExternalBrowserManager.AvailableBrowsers.Keys)
			{
				switch (browserTag)
				{
					case ExternalBrowserManager.BrowserChromeTag:
						_commandIdOpenChrome = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserChromeTag);
						_externalBrowsersCommandIds.Add("Open in Chrome", _commandIdOpenChrome);
						break;
					case ExternalBrowserManager.BrowserFirefoxTag:
						_commandIdOpenFirefox = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserFirefoxTag);
						_externalBrowsersCommandIds.Add("Open in Firefox", _commandIdOpenFirefox);
						break;
					case ExternalBrowserManager.BrowserIETag:
						_commandIdOpenIE = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserIETag);
						_externalBrowsersCommandIds.Add("Open in Internet Explorer", _commandIdOpenIE);
						break;
					case ExternalBrowserManager.BrowserEdgeTag:
						_commandIdOpenEdge = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserEdgeTag);
						_externalBrowsersCommandIds.Add("Open in Edge", _commandIdOpenEdge);
						break;
				}
			}
		}

		private void OnWebViewExternalBrowserOpenCommand(object sender, CommandEventArgs e)
		{
			if (e.CommandId == _commandIdOpenChrome)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserChromeTag, e.MenuInfo.LinkUrl);
			else if (e.CommandId == _commandIdOpenFirefox)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserFirefoxTag, e.MenuInfo.LinkUrl);
			else if (e.CommandId == _commandIdOpenEdge)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserEdgeTag, e.MenuInfo.LinkUrl);
			else if (e.CommandId == _commandIdOpenIE)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserIETag, e.MenuInfo.LinkUrl);
		}
		#endregion

		#region Splash Processing
		private void ResizeProgressBar()
		{
			var padding = (Width - 420) / 2;
			pnProgress.Padding = new Padding(padding, 50, padding, 0);
		}

		private void OnWebPageResize(object sender, EventArgs e)
		{
			ResizeProgressBar();
		}
		#endregion
	}
}
