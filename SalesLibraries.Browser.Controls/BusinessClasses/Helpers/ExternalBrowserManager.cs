using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Helpers
{
	public class ExternalBrowserManager
	{
		public const string BrowserChromeTag = "chrome";
		public const string BrowserFirefoxTag = "firefox";
		public const string BrowserIETag = "iexplore";
		public const string BrowserEdgeTag = "edge";

		private static readonly string[] ProcessedBrowsers =
		{
			BrowserChromeTag,
			BrowserFirefoxTag,
			BrowserIETag
		};

		public static Dictionary<string, string> AvailableBrowsers { get; }

		static ExternalBrowserManager()
		{
			AvailableBrowsers = new Dictionary<string, string>();
		}

		public static void Load()
		{
			if (AvailableBrowsers.Any()) return;
			try
			{
				// Check browser availability
				var browsers = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
				if (browsers == null) return;
				foreach (var browserTag in ProcessedBrowsers)
				{
					foreach (var key in browsers.GetSubKeyNames())
					{
						var tag = browserTag;
						if (!key.ToLower().Contains(tag)) continue;
						var browserKey = browsers.OpenSubKey(key).OpenSubKey(@"shell\open\command");
						var browserPath = browserKey?.GetValue(null) as String;
						if (browserPath == null) continue;
						var path = browserPath.Replace("\"", "");
						if (!File.Exists(path)) continue;
						if (AvailableBrowsers.ContainsKey(browserTag)) continue;
						AvailableBrowsers.Add(browserTag, path);
					}
				}

				if (Utils.IsWindows10())
					AvailableBrowsers.Add(BrowserEdgeTag, "microsoft-edge:{0}");
			}
			catch { }
		}

		public static void OpenUrl(string browserTag, string url)
		{
			var browserPath = ExternalBrowserManager.AvailableBrowsers[browserTag];
			try
			{
				if (browserTag == BrowserEdgeTag)
					Process.Start(String.Format(browserPath, url));
				else
					Process.Start(browserPath, url);
			}
			catch { }
		}
	}
}
