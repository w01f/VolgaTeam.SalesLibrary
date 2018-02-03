using System;
using System.Text.RegularExpressions;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Helpers
{
	static class YouTubeHelper
	{
		public static bool IsUrlYouTubeWatch(string targetUrl)
		{
			var regexp = new Regex(UrlParseHelper.UrlParseRegExp);
			var urlMatch = regexp.Match(targetUrl);
			var domain = urlMatch.Success && urlMatch.Groups.Count >= 5 ? urlMatch.Groups[4].Value : null;
			var path = urlMatch.Success && urlMatch.Groups.Count >= 9 ? urlMatch.Groups[8].Value : null;
			return "youtube.com".Equals(domain, StringComparison.OrdinalIgnoreCase) && "watch".Equals(path, StringComparison.OrdinalIgnoreCase);
		}
	}
}
