using System;
using System.IO;
using System.Text.RegularExpressions;
using EO.WebBrowser;

namespace SalesLibraries.FileManager.Business.Services
{
	class EOBrowserThumbnailGenerator
	{
		public void GenerateThumbnail(string url, string destinationPath, string alternativeUrl = null)
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
							var isDocumentWrong = webView.Title?.Contains("Sign in is not complete") ?? true;
							if (!isDocumentWrong)
							{
								var html = webView.GetHtml();
								var matchGroups = new Regex("(?s)<body[^>]*>(.*)</body>", RegexOptions.IgnoreCase).Match(html).Groups;
								var htmlBody = matchGroups.Count > 1 ? matchGroups[1].Value : null;
								isDocumentWrong = String.IsNullOrWhiteSpace(htmlBody);
							}
							if (isDocumentWrong && !String.IsNullOrEmpty(alternativeUrl))
								GenerateThumbnail(alternativeUrl, destinationPath);
							else
								webView.Capture().Save(Path.Combine(destinationPath, "thumbnail.png"));
						});
					}
					catch (Exception ex)
					{
						if (!String.IsNullOrEmpty(alternativeUrl))
							GenerateThumbnail(alternativeUrl, destinationPath);
					}
				}
			}
		}
	}
}
