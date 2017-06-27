using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Models.WebLinkThumbnails;
using Timer = System.Threading.Timer;

namespace SalesLibraries.FileManager.Business.Services
{
	class WebLinkThumbnailGenerator
	{
		private bool _complited;
		private readonly List<Link> _loadingLinks = new List<Link>();
		private readonly ExtendedWebBrowser _webBrowser;
		private Timer _compliteTimer;

		public WebLinkThumbnailGenerator()
		{
			_webBrowser = new ExtendedWebBrowser();
			_webBrowser.ScrollBarsEnabled = false;
			_webBrowser.Navigating += OnWebBrowserNavigating;
			_webBrowser.Navigated += OnWebBrowserNavigated;
			_webBrowser.ScriptErrorsSuppressed = true;
			_webBrowser.DocumentCompleted += OnWebBrowserDocumentCompleted;
		}

		public void GenerateThumbnail(string url, string destinationPath)
		{
			var width = 1280;
			var height = 1024;
			var thumbWidth = 800;
			var thumbHeight = 600;

			try
			{
				_complited = false;
				_compliteTimer = null;
				_loadingLinks.Clear();
				_webBrowser.Navigate(url);
				long timeStart = Environment.TickCount;
				const long timeout = 6000;
				while (!_complited)
				{
					Application.DoEvents();
					if (Environment.TickCount - timeStart <= timeout) continue;
					break;
				}
			}
			catch { }
			finally
			{
				_webBrowser.Stop();
				if (_webBrowser?.Document?.Body != null)
				{
					_webBrowser.Height = height;
					_webBrowser.Width = width;

					var rectangle = new Rectangle(0, 0, thumbWidth, thumbHeight);
					var bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
					using (var graphics = Graphics.FromImage(bitmap))
					{
						graphics.SmoothingMode = SmoothingMode.AntiAlias;
						graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
						graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
						graphics.CompositingQuality = CompositingQuality.HighQuality;
						var hdc = graphics.GetHdc();
						var unknown = Marshal.GetIUnknownForObject(_webBrowser.ActiveXInstance);
						WinAPIHelper.OleDraw(unknown, 1, hdc, ref rectangle);
						Marshal.Release(unknown);
						graphics.ReleaseHdc(hdc);
						bitmap.Save(Path.Combine(destinationPath, "thumbnail.png"), ImageFormat.Png);
					}
				}
			}
		}

		private void OnWebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.ToString().Equals("about:blank")) return;
			var loadingLink = _loadingLinks.FirstOrDefault(l => l.IsUrlEqual(e.Url.ToString()));
			if (loadingLink != null) return;
			loadingLink = new Link { Url = e.Url.ToString(), Loaded = e.Url.ToString().StartsWith("javascript:") };
			_loadingLinks.Add(loadingLink);
		}

		private void OnWebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			if (_webBrowser.ScriptErrorsSuppressed)
				_webBrowser.Document.Window.Error += OnWebBrowserWindowError;
		}

		private void OnWebBrowserWindowError(object sender, HtmlElementErrorEventArgs e)
		{
			e.Handled = true;
		}

		private void OnWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			var loadingLink = _loadingLinks.FirstOrDefault(l => l.IsUrlEqual(e.Url.ToString()));
			if (loadingLink == null) return;
			loadingLink.Loaded = true;
			if (_loadingLinks.Any(l => !l.Loaded)) return;
			if (_compliteTimer == null)
				_compliteTimer = new Timer(state =>
				{
					_complited = _loadingLinks.All(l => l.Loaded);
				}, null, 1000, Timeout.Infinite);
			else
				_compliteTimer.Change(1000, Timeout.Infinite);
		}
	}
}
