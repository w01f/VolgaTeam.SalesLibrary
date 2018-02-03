using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects.FileLinks;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects.LinkViewContent;
using EO.WebBrowser;
using Microsoft.Win32;
using SalesLibraries.Common.OfficeInterops;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Helpers
{
	class ExtensionsManager
	{
		public const string ActivateFunctionName = "SalesLibraryExtensions_activate";

		private readonly string _url;

		public bool IsExtensionsActive { get; private set; }
		public LinkViewContentExtension LinkViewContentExtension { get; }
		public LinkOpenExtension LinkOpenExtension { get; }

		public ExtensionsManager(string url)
		{
			_url = url;
			LinkViewContentExtension = new LinkViewContentExtension();
			LinkOpenExtension = new LinkOpenExtension();
		}

		public void Activate()
		{
			IsExtensionsActive = true;
		}

		public void OnJavaScriptCall(object sender, JSExtInvokeArgs e)
		{
			switch (e.FunctionName)
			{
				case ActivateFunctionName:
					Activate();
					break;
				default:
					LinkViewContentExtension.OnJavaScriptCall(sender, e);
					LinkOpenExtension.OnJavaScriptCall(sender, e);
					break;
			}
		}

		public bool IsUrlExternal(string targetUrl)
		{
			var regexp = new Regex(UrlParseHelper.UrlParseRegExp);

			var currentUrlMatch = regexp.Match(_url);
			var targetUrlMatch = regexp.Match(targetUrl);

			var currentDomain = currentUrlMatch.Success && currentUrlMatch.Groups.Count >= 5 ? currentUrlMatch.Groups[4].Value : null;
			var targetDomain = targetUrlMatch.Success && targetUrlMatch.Groups.Count >= 5 ? targetUrlMatch.Groups[4].Value : null;
			if (String.IsNullOrEmpty(targetDomain))
				return false;
			if (!targetDomain.Equals(currentDomain, StringComparison.OrdinalIgnoreCase))
				return true;
			var targetPath = targetUrlMatch.Success && targetUrlMatch.Groups.Count >= 9 ? targetUrlMatch.Groups[8].Value : null;
			return !String.IsNullOrEmpty(targetPath) &&
				(targetPath.StartsWith("qpage", StringComparison.OrdinalIgnoreCase) ||
				targetPath.Contains("public_links") ||
				targetPath.StartsWith("shortcuts/getSinglePage", StringComparison.OrdinalIgnoreCase));
		}

		public static void MakeUrlTrusted(string targetUrl)
		{
			const string domainsKeyLocation = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains";
			const int trustedSiteZone = 0x2;

			var regexp = new Regex(UrlParseHelper.UrlParseRegExp);
			var targetUrlMatch = regexp.Match(targetUrl);
			var targetDomain = targetUrlMatch.Success && targetUrlMatch.Groups.Count >= 5 ? targetUrlMatch.Groups[4].Value : null;

			try
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey(domainsKeyLocation, RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey(targetDomain, RegistryKeyPermissionCheck.ReadWriteSubTree);
				key?.SetValue("http", trustedSiteZone, RegistryValueKind.DWord);
			}
			catch { }
		}

		public static void PrintFile(string filePath, int currentPage = 0)
		{
			var printProcess = new Process();
			if (FileFormatHelper.IsPowerPointFile(filePath))
			{
				try
				{
					using (var powerPointProcessor = new PowerPointHidden())
					{
						if (!powerPointProcessor.Connect(true)) return;
						powerPointProcessor.PrintPresentation(
							filePath,
							currentPage,
							printAction =>
							{
								try
								{
									printAction();
								}
								catch
								{
								}
							});
					}
				}
				catch
				{
				}
			}
			else if (FileFormatHelper.IsWordFile(filePath))
			{
				try
				{
					printProcess.StartInfo.FileName = "winword.exe";
					printProcess.StartInfo.Arguments = '"' + filePath + '"' + " /mFilePrint";
					printProcess.Start();
				}
				catch { }
			}
			else if (FileFormatHelper.IsExcelFile(filePath))
			{
				if (ExcelHelper.Instance.Connect())
				{
					ExcelHelper.Instance.Print(new FileInfo(filePath));
					ExcelHelper.Instance.Disconnect();
				}
			}
			else if (FileFormatHelper.IsPdfFile(filePath))
			{
				try
				{
					printProcess.StartInfo.FileName = "AcroRd32.exe";
					printProcess.StartInfo.Arguments = " /p " + '"' + filePath + '"';
					printProcess.Start();
				}
				catch
				{
				}
			}
		}
	}
}
