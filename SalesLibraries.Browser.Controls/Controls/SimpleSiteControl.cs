using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;
using SalesLibraries.Browser.Controls.ToolForms;

namespace SalesLibraries.Browser.Controls.Controls
{
	public partial class SimpleSiteControl : UserControl, ISiteContainer
	{
		private bool _loaded;

		private readonly WebControl _browser;
		private readonly WebControl _childBrowser;

		public SiteBundleControl ParentBundle { get; }
		public SiteSettings SiteSettings { get; }
		public string CurrentUrl => SiteSettings?.BaseUrl;

		public SimpleSiteControl(SiteSettings siteSettings, SiteBundleControl parentBundle)
		{
			InitializeComponent();

			SiteSettings = siteSettings;
			ParentBundle = parentBundle;

			_childBrowser = new WebControl();
			_childBrowser.WebView = new WebView();
			_childBrowser.WebView.FileDialog += OnProcessFileDialog;
			_childBrowser.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_childBrowser.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_childBrowser.WebView.DownloadCompleted += OnWebViewDownloadCompleted;
			_childBrowser.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
			_childBrowser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Controls.Add(_childBrowser);

			_browser = new WebControl();
			_browser.WebView = new WebView();
			_browser.Dock = DockStyle.Fill;
			_browser.WebView.LoadCompleted += OnMainWebViewLoadComplete;
			_browser.WebView.NewWindow += OnMainWebViewNewWindow;
			_browser.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_browser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Controls.Add(_browser);

			_browser.BringToFront();
		}

		public void InitSite()
		{
			if (_loaded) return;
			Application.DoEvents();
			_browser.WebView.LoadUrl(SiteSettings.BaseUrl);
		}

		public void CopyUrl()
		{
			Clipboard.SetText(SiteSettings.BaseUrl);
			ParentBundle.PopupMessageHelper.ShowInfo("Url successfully copied");
		}

		public void EmailUrl()
		{
			try
			{
				Process.Start(Uri.EscapeUriString(String.Format("mailto:{0}?Body={1}", String.Empty, SiteSettings.BaseUrl)));
			}
			catch { }
		}

		public void UpdateExtensionsState()
		{
			ParentBundle.ButtonExtensionsAddSlide.Visible = false;
			ParentBundle.ButtonExtensionsAddSlides.Visible = false;
			ParentBundle.ButtonExtensionsPrint.Visible = false;
			ParentBundle.ButtonExtensionsAddVideo.Visible = false;
			ParentBundle.LabelExtensionsWarning.Text = String.Empty;
		}

		public void UpdateYouTubeState()
		{
			ParentBundle.ButtonExtensionsDownloadYouTube.Visible = false;
			ParentBundle.barMain.RecalcLayout();
		}

		public void UpdateNavigationButtons()
		{
			ParentBundle.ButtonNavigationBack.Enabled = _browser.WebView.CanGoBack;
			ParentBundle.ButtonNavigationForward.Enabled = _browser.WebView.CanGoForward;
			ParentBundle.barMain.RecalcLayout();
		}

		public void NavigateBack()
		{
			_browser.WebView.GoBack();
		}

		public void NavigateForward()
		{
			_browser.WebView.GoForward();
		}

		public void RefreshPage()
		{
			_browser.WebView.Reload();
		}

		public void UpdateNavigationButtonsState()
		{
			ParentBundle.ButtonNavigationBack.Enabled = _browser.WebView.CanGoBack;
			ParentBundle.ButtonNavigationForward.Enabled = _browser.WebView.CanGoForward;
			ParentBundle.barMain.RecalcLayout();
		}

		private void OnMainWebViewLoadComplete(Object sender, LoadCompletedEventArgs e)
		{
			_loaded = true;
		}

		private void OnMainWebViewNewWindow(object sender, NewWindowEventArgs e)
		{
			_childBrowser.WebView.LoadUrl(e.TargetUrl);
			e.Accepted = false;
		}

		private void OnProcessFileDialog(Object sender, FileDialogEventArgs e)
		{
			switch (e.Mode)
			{
				case FileDialogMode.Save:
					using (var saveDialog = new SaveFileDialog())
					{
						saveDialog.Title = e.Title;
						saveDialog.Filter = e.Filter;
						saveDialog.FileName = e.DefaultFileName;
						if (saveDialog.ShowDialog() != DialogResult.Cancel)
						{
							FormDownloadProgress.ShowProgress(ParentBundle.MainForm);
							FormDownloadProgress.SetTitle("Downloading…");
							FormDownloadProgress.SetDetails(Path.GetFileName(saveDialog.FileName));
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

		private void OnWebViewDownloadUpdated(Object sender, DownloadEventArgs e)
		{
			FormDownloadProgress.SetDetails(String.Format("{0} - {1}%", Path.GetFileName(e.Item.FullPath), e.Item.PercentageComplete));
			Application.DoEvents();
		}

		private void OnWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			FormDownloadProgress.CloseProgress();
			using (var formComplete = new FormFileDownloadComplete(e.Item.FullPath))
			{
				formComplete.StartPosition = FormStartPosition.CenterParent;
				formComplete.ShowDialog();
			}
		}

		private void OnWebViewDownloadCanceled(Object sender, DownloadEventArgs e)
		{
			FormDownloadProgress.CloseProgress();
		}
	}
}
