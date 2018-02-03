using System;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Browser.Controls.BusinessClasses.Enums;
using SalesLibraries.Browser.Controls.BusinessClasses.Helpers;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects.LinkViewContent;
using SalesLibraries.Browser.Controls.ToolForms;
using EO.WebBrowser;
using SalesLibraries.Browser.Controls.InteropClasses;
using SalesLibraries.CommonGUI.Floater;

namespace SalesLibraries.Browser.Controls.Controls.WebPage
{
	public partial class WebKitPage
	{
		private void InitSalesLibraryViewContentExtensions()
		{
			_extensionDownloadView.WebView.DownloadCompleted += OnSlideContentWebViewDownloadCompleted;

			_extensionsManager.LinkViewContentExtension.ContetChanged += OnViewContentChanged;

			_webKit.WebView.RegisterJSExtensionFunction(LinkViewContentExtension.SendLinkDataFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(LinkViewContentExtension.ReleaseLinkDataFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(LinkViewContentExtension.SwitchDataFunctionName, _extensionsManager.OnJavaScriptCall);
		}

		private void OnViewContentChanged(Object sender, EventArgs e)
		{
			UpdateViewContentState();
		}

		public void UpdateViewContentState()
		{
			_siteContainer.ParentBundle.ButtonExtensionsAddSlide.Visible = false;
			_siteContainer.ParentBundle.ButtonExtensionsAddSlides.Visible = false;

			_siteContainer.ParentBundle.ButtonExtensionsPrint.Visible = false;

			_siteContainer.ParentBundle.ButtonExtensionsAddVideo.Visible = false;

			_siteContainer.ParentBundle.LabelExtensionsWarning.Text = String.Empty;

			if (_extensionsManager.LinkViewContentExtension.PowerPointEnabled)
			{
				_siteContainer.ParentBundle.ButtonExtensionsAddSlide.Visible = true;
				_siteContainer.ParentBundle.ButtonExtensionsAddSlides.Visible = true;
				_siteContainer.ParentBundle.ButtonExtensionsPrint.Visible = true;

				_siteContainer.ParentBundle.PowerPointSingleton.Connect();
				var slideSettings = _siteContainer.ParentBundle.PowerPointSingleton.GetSlideSettings();
				var currentPageContent = _extensionsManager.LinkViewContentExtension.CurrentPowerPointContent;
				if (slideSettings != null && !currentPageContent.IsFitToInsert(slideSettings))
					_siteContainer.ParentBundle.LabelExtensionsWarning.Text = "Slide Size Conflict: The slides may not insert correctly…";

				_siteContainer.ParentBundle.ButtonExtensionsAddSlides.Text = String.Format("   ({0})", _extensionsManager.LinkViewContentExtension.CurrentPowerPointContent.PartsCount);
			}
			if (_extensionsManager.LinkViewContentExtension.PrintEnabled)
			{
				_siteContainer.ParentBundle.ButtonExtensionsPrint.Visible = true;
			}
			if (_extensionsManager.LinkViewContentExtension.VideoEnabled)
			{
				_siteContainer.ParentBundle.PowerPointSingleton.Connect();

				var activePresentation = _siteContainer.ParentBundle.PowerPointSingleton.GetActivePresentation();
				var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);

				_siteContainer.ParentBundle.ButtonExtensionsAddVideo.Visible = allowVideoInsert;

				if (activePresentation != null && !allowVideoInsert)
					_siteContainer.ParentBundle.LabelExtensionsWarning.Text = "Save your presentation if you want to add this video…";
			}
			_siteContainer.ParentBundle.barMain.RecalcLayout();
		}

		public void AddVideo()
		{
			if (!_siteContainer.ParentBundle.CheckPowerPointRunning(UpdateViewContentState)) return;
			var activePresentation = _siteContainer.ParentBundle.PowerPointSingleton.GetActivePresentation();
			if (activePresentation != null && File.Exists(activePresentation.FullName))
			{
				DownloadFile(_extensionsManager.LinkViewContentExtension.CurrentVideoContent.GetMp4Url());
			}
		}

		public void AddSlide()
		{
			if (!_siteContainer.ParentBundle.CheckPowerPointRunning(UpdateViewContentState)) return;
			DownloadFile(
				_extensionsManager.LinkViewContentExtension.CurrentPageContent.GetPartFileUrl(),
				AfterDownloadAction.Open);
		}

		public void AddSlides()
		{
			if (!_siteContainer.ParentBundle.CheckPowerPointRunning(UpdateViewContentState)) return;
			DownloadFile(
				_extensionsManager.LinkViewContentExtension.CurrentPageContent.OriginalFileUrl,
				AfterDownloadAction.Open);
		}

		public void Print()
		{
			DownloadFile(
				_extensionsManager.LinkViewContentExtension.CurrentPrintableContent.PrintableFileUrl,
				AfterDownloadAction.Print);
		}

		private bool HandleVideoDownloaded(string filePath)
		{
			_siteContainer.ParentBundle.PowerPointSingleton.Connect();
			var activePresentation = _siteContainer.ParentBundle.PowerPointSingleton.GetActivePresentation();
			var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);
			if (allowVideoInsert)
			{
				using (var formComplete = new FormVideoDownloadComplete(filePath))
				{
					var result = formComplete.ShowDialog(_siteContainer.ParentBundle.MainForm);
					if (result == DialogResult.Abort)
						_siteContainer.ParentBundle.ShowFloater(new FloaterRequestedEventArgs
						{
							AfterShow = () =>
							{
								_siteContainer.ParentBundle.ProcessManager.Run(
									"Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!",
									(cancelletionToken, formProgress) =>
									{
										_siteContainer.ParentBundle.PowerPointSingleton.InsertVideoIntoActivePresentation(filePath);
									});
							}
						});
					return true;
				}
			}
			return false;
		}

		private void OnSlideContentWebViewDownloadCompleted(object sender, DownloadEventArgs e)
		{
			if (!_extensionsManager.LinkViewContentExtension.ContentEnabled) return;
			switch (_afterDownloadAction)
			{
				case AfterDownloadAction.Print:
					ExtensionsManager.PrintFile(
						e.Item.FullPath,
						(_extensionsManager.LinkViewContentExtension.CurrentPrintableContent.CurrentPage + 1) ?? 1);
					break;
				default:
					_siteContainer.ParentBundle.ShowFloater(new FloaterRequestedEventArgs
					{
						AfterShow = () =>
						{
							if (_extensionsManager.LinkViewContentExtension.CurrentLinkViewContent?.ContentType == LinkContentType.Video)
							{
								_siteContainer.ParentBundle.ProcessManager.Run(
									"Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!",
									(cancelletionToken, formProgress) =>
									{
										_siteContainer.ParentBundle.PowerPointSingleton.InsertVideoIntoActivePresentation(e.Item.FullPath);
									});
							}
							else if (_extensionsManager.LinkViewContentExtension.CurrentLinkViewContent?.ContentType == LinkContentType.PowerPoint)
							{
								_siteContainer.ParentBundle.ProcessManager.Run(
									"Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!",
									(cancelletionToken, formProgress) =>
									{
										_siteContainer.ParentBundle.PowerPointSingleton.AppendSlidesFromFile(e.Item.FullPath);
									});
							}
						}
					});
					break;
			}
		}
	}
}
