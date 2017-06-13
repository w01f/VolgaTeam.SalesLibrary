using System;
using System.Windows.Forms;
using SHDocVw;
using WebBrowser = System.Windows.Forms.WebBrowser;

namespace SalesLibraries.FileManager.Business.Models.WebLinkThumbnails
{
	public delegate void DocumentLoaded(string url);

	class ExtendedWebBrowser : WebBrowser
	{
		private AxHost.ConnectionPointCookie cookie;
		private WebBrowserExtendedEvents events;
		public event DocumentLoaded Finished;

		public void ReallyDone(string url)
		{
			Application.DoEvents();
			try
			{
				Finished(url);
			}
			catch (Exception) { }
		}

		/// <summary>
		///     This method will be called to give
		///     you a chance to create your own event sink
		/// </summary>
		// [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		protected override void CreateSink()
		{
			// Make sure to call the base class or the normal events won't fire
			base.CreateSink();
			events = new WebBrowserExtendedEvents(this);
			cookie = new AxHost.ConnectionPointCookie(ActiveXInstance,
				events, typeof(DWebBrowserEvents2));
		}

		// [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		protected override void DetachSink()
		{
			if (null != cookie)
			{
				cookie.Disconnect();
				cookie = null;
			}
		}

		private class WebBrowserExtendedEvents :
			DWebBrowserEvents2
		{
			private readonly ExtendedWebBrowser _Browser;

			public WebBrowserExtendedEvents(ExtendedWebBrowser
				browser)
			{
				_Browser = browser;
			}

			#region DWebBrowserEvents2 Members
			public void DownloadBegin() { }

			public void DownloadComplete() { }
			#endregion

			#region DWebBrowserEvents2 Members
			public void BeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel) { }

			public void ClientToHostWindow(ref int CX, ref int CY) { }

			public void CommandStateChange(int Command, bool Enable) { }

			public void DocumentComplete(object pDisp, ref object URL)
			{
				if (pDisp == _Browser.ActiveXInstance)
					_Browser.ReallyDone((string)URL);
			}

			public void FileDownload(bool ActiveDocument, ref bool Cancel) { }

			public void NavigateComplete2(object pDisp, ref object URL) { }

			public void NavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel) { }

			public void NewWindow2(ref object ppDisp, ref bool Cancel)
			{
				ppDisp = null;
				Cancel = true;
			}

			public void NewWindow3(ref object ppDisp, ref bool Cancel, uint dwFlags, string bstrUrlContext, string bstrUrl) { }

			public void OnFullScreen(bool FullScreen) { }

			public void OnMenuBar(bool MenuBar) { }

			public void OnQuit() { }

			public void OnStatusBar(bool StatusBar) { }

			public void OnTheaterMode(bool TheaterMode) { }

			public void OnToolBar(bool ToolBar) { }

			public void OnVisible(bool Visible) { }

			public void PrintTemplateInstantiation(object pDisp) { }

			public void PrintTemplateTeardown(object pDisp) { }

			public void PrivacyImpactedStateChange(bool bImpacted) { }

			public void ProgressChange(int Progress, int ProgressMax) { }

			public void PropertyChange(string szProperty) { }

			public void SetPhishingFilterStatus(int PhishingFilterStatus) { }

			public void SetSecureLockIcon(int SecureLockIcon) { }

			public void StatusTextChange(string Text) { }

			public void TitleChange(string Text) { }

			public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone) { }

			public void WindowClosing(bool IsChildWindow, ref bool Cancel) { }

			public void WindowSetHeight(int Height) { }

			public void WindowSetLeft(int Left) { }

			public void WindowSetResizable(bool Resizable) { }

			public void WindowSetTop(int Top) { }

			public void WindowSetWidth(int Width) { }

			public void WindowStateChanged(uint dwWindowStateFlags, uint dwValidFlagsMask) { }
			#endregion
		}
	}
}
