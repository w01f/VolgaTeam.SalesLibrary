using System;
using System.IO;
using EO.WebBrowser;

namespace SalesLibraries.FileManager.Business.Services
{
	class EOBrowserThumbnailGenerator
	{
		public void GenerateThumbnail(string url, string destinationPath)
		{
			const int thumbWidth = 800;
			const int thumbHeight = 600;

			using (var threadRunner = new ThreadRunner())
			{
				using (var webView = threadRunner.CreateWebView())
				{
					webView.Resize(thumbWidth, thumbHeight);
					try
					{
						threadRunner.Send(() =>
						{
							webView.LoadUrlAndWait(url);
							webView.Capture().Save(Path.Combine(destinationPath, "thumbnail.png"));
						});
					}
					catch (Exception ex)
					{
						//throw ex;
					}
				}
			}
		}
	}
}
