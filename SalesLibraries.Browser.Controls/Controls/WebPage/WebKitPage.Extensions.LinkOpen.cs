using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Browser.Controls.BusinessClasses.Enums;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects.FileLinks;
using EO.WebBrowser;
using SalesLibraries.CommonGUI.Floater;

namespace SalesLibraries.Browser.Controls.Controls.WebPage
{
	public partial class WebKitPage
	{
		private void InitSalesLibraryLinkOpenExtensions()
		{
			_extensionDownloadView.WebView.DownloadCompleted += OnLinkOpenWebViewDownloadCompleted;

			_extensionsManager.LinkOpenExtension.LinkLoaded += OnLinkLoadedChanged;

			_webKit.WebView.RegisterJSExtensionFunction(LinkOpenExtension.OpenLinkFunctionName, _extensionsManager.OnJavaScriptCall);
		}

		private void OnLinkLoadedChanged(Object sender, EventArgs e)
		{
			if (_extensionsManager.LinkOpenExtension.LinkData?.Type == LinkType.Lan)
			{
				OpenLanLink(_extensionsManager.LinkOpenExtension.LinkData.FileUrl);
				_extensionsManager.LinkOpenExtension.ReleaseData();
			}
			else if (_extensionsManager.LinkOpenExtension.LinkData?.Type == LinkType.App)
			{
				OpenFileLink(((AppLinkData)_extensionsManager.LinkOpenExtension.LinkData).GetExecutablePaths());
				_extensionsManager.LinkOpenExtension.ReleaseData();
			}
			else
			{
				DownloadFile(_extensionsManager.LinkOpenExtension.LinkData.FileUrl);
			}
		}

		private void OpenLanLink(string linkPath)
		{
			var linkPathAvailable = false;
			_siteContainer.ParentBundle.ProcessManager.Run(
				"Scanning your network for this location…",
				(cancelletionToken, formProgress) =>
				{
					try
					{
						linkPathAvailable = Directory.Exists(linkPath);
						if (linkPathAvailable)
							Process.Start(linkPath);
					}
					catch { }
				});
			if (!linkPathAvailable)
				MessageBox.Show("Your Browser does not allow access to this network location…", "Warning", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
		}

		private void OpenFileLink(IEnumerable<string> executablePaths)
		{
			foreach (var executablePath in executablePaths)
			{
				try
				{
					Process.Start(executablePath);
					break;
				}
				catch { }
			}
		}

		private void OnLinkOpenWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			if (_extensionsManager.LinkOpenExtension.LinkData == null) return;
			_siteContainer.ParentBundle.ShowFloater(new FloaterRequestedEventArgs
			{
				AfterShow = () =>
				{

					switch (_extensionsManager.LinkOpenExtension.LinkData?.Type)
					{

						case LinkType.PowerPoint:
						case LinkType.Word:
						case LinkType.Excel:
						case LinkType.Pdf:
						case LinkType.Image:
							OpenFileLink(new[] { e.Item.FullPath });
							break;
					}
					_extensionsManager.LinkOpenExtension.ReleaseData();
				}
			});
		}
	}
}
