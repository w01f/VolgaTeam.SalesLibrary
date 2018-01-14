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
	class RegularBrowserThumbnailGenerator
	{
		private const int BrowserWidth = 1280;
		private const int BrowserHeight = 1024;
		private const int ThumbWidth = 800;
		private const int ThumbHeight = 600;


		private bool _complited;
		private readonly List<Link> _loadingLinks = new List<Link>();
		private readonly ExtendedWebBrowser _webBrowser;
		private Timer _compliteTimer;

		public RegularBrowserThumbnailGenerator()
		{
			_webBrowser = new ExtendedWebBrowser();
			_webBrowser.ScrollBarsEnabled = false;
			_webBrowser.Navigating += OnWebBrowserNavigating;
			_webBrowser.Navigated += OnWebBrowserNavigated;
			_webBrowser.ScriptErrorsSuppressed = true;
			_webBrowser.DocumentCompleted += OnWebBrowserDocumentCompleted;
		}

		public void GenerateThumbnail(string url, string destinationPath, int delaySeconds = 10, string alternativeUrl = null)
		{
			try
			{
				_complited = false;
				_compliteTimer = null;
				_loadingLinks.Clear();
				_webBrowser.Height = BrowserHeight;
				_webBrowser.Width = BrowserWidth;
				_webBrowser.Navigate(url);
				long timeStart = Environment.TickCount;
				const long timeout = 20000;
				while (!_complited)
				{
					Application.DoEvents();
					if (Environment.TickCount - timeStart <= timeout) continue;

					if (!String.IsNullOrEmpty(alternativeUrl))
					{
						GenerateThumbnail(alternativeUrl, destinationPath, delaySeconds);
						return;
					}
					break;
				}

				while (delaySeconds > 0)
				{
					Thread.Sleep(1000);
					Application.DoEvents();
					delaySeconds--;
				}

				_webBrowser.Stop();
				var isTitleIncorrect = _webBrowser?.Document?.Title?.Contains("Sign in is not complete");
				if ((isTitleIncorrect.HasValue && isTitleIncorrect.Value) || _webBrowser?.Document?.Body == null)
				{
					GenerateThumbnail(alternativeUrl, destinationPath, delaySeconds);
				}
				else
				{
					var rectangle = new Rectangle(0, 0, ThumbWidth, ThumbHeight);
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
			catch (Exception ex)
			{
				_webBrowser.Stop();
			}
			finally
			{
				_webBrowser.Dispose();
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
