using System;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Browser.Controls.BusinessClasses.Enums;
using SalesLibraries.Browser.Controls.BusinessClasses.Helpers;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects.FileLinks;
using EO.WebBrowser;
using EO.WinForm;
using SalesLibraries.Browser.Controls.ToolForms;

namespace SalesLibraries.Browser.Controls.Controls.WebPage
{
	public partial class WebKitPage
	{
		private WebControl _extensionDownloadView;
		private ExtensionsManager _extensionsManager;
		private AfterDownloadAction _afterDownloadAction;

		private void InitExtensions()
		{
			_extensionDownloadView = new WebControl();
			_extensionDownloadView.WebView = new WebView();
			_extensionDownloadView.WebView.ShouldForceDownload += OnExtensionWebViewShouldForceDownload;
			_extensionDownloadView.WebView.BeforeDownload += OnExtensionWebViewBeforeDownload;
			_extensionDownloadView.WebView.DownloadCompleted += OnExtensionsWebViewDownloadCompleted;
			_extensionDownloadView.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_extensionDownloadView.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
			_extensionDownloadView.WebView.LoadFailed += OnExtensionWebViewLoadFailed;
			Controls.Add(_extensionDownloadView);

			_extensionsManager = new ExtensionsManager(_startUrl);
			_webKit.WebView.RegisterJSExtensionFunction(ExtensionsManager.ActivateFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(LinkOpenExtension.OpenLinkFunctionName, _extensionsManager.OnJavaScriptCall);

			InitSalesLibraryViewContentExtensions();
			InitSalesLibraryLinkOpenExtensions();
		}

		private void DownloadFile(string url, AfterDownloadAction afterDownloadAction = AfterDownloadAction.None)
		{
			_afterDownloadAction = afterDownloadAction;
			FormDownloadProgress.ShowProgress(_siteContainer.ParentBundle.MainForm);
			FormDownloadProgress.SetTitle("Downloading…");
			_siteContainer.SuspendPages();
			Application.DoEvents();
			_extensionDownloadView.WebView.LoadUrl(url.Replace(@"SalesLibraries/SalesLibraries", "SalesLibraries"));
		}

		private void OnExtensionWebViewBeforeDownload(Object sender, BeforeDownloadEventArgs e)
		{
			e.ShowDialog = false;
			e.FilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(e.FilePath));
		}

		private void OnExtensionWebViewShouldForceDownload(object sender, ShouldForceDownloadEventArgs e)
		{
			e.ForceDownload = true;
		}

		private void OnExtensionWebViewLoadFailed(object sender, LoadFailedEventArgs e)
		{
			if (e.ErrorCode == ErrorCode.ProceedAsDownload) return;
			_siteContainer.ResumePages();
			FormDownloadProgress.CloseProgress();
		}

		private void OnExtensionsWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			_siteContainer.ResumePages();
			FormDownloadProgress.CloseProgress();
		}
	}
}
