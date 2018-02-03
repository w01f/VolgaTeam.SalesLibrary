using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SalesLibraries.Browser.Controls.BusinessClasses.Enums;
using SalesLibraries.Browser.Controls.BusinessClasses.Objects;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.FileManager.Configuration
{
	public class BrowserManager
	{
		public List<BrowserSettings> Browsers { get; }

		public BrowserManager()
		{
			Browsers = new List<BrowserSettings>();
		}

		public void Init(StorageFile settingsFile)
		{
			if (!settingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(settingsFile.LocalPath);

			foreach (var browserNode in document.SelectNodes(@"//Root/Browser").OfType<XmlNode>())
			{
				var browserSettings = new BrowserSettings(browserNode);
				Browsers.Add(browserSettings);
			}
		}
	}

	public class BrowserSettings
	{
		public string Id { get; }
		public string StatusBarTitle { get; }
		public List<SiteSettings> Sites { get; }

		public BrowserSettings(XmlNode rootNode)
		{
			Sites = new List<SiteSettings>();

			Id = rootNode.SelectSingleNode(@"./Id")?.InnerText;
			StatusBarTitle = rootNode.SelectSingleNode(@"./Footer")?.InnerText ?? "Sales Cloud";

			foreach (var siteNode in rootNode.SelectNodes(@"./Site").OfType<XmlNode>())
			{
				var siteSettings = new SiteSettings();
				switch (siteNode.SelectSingleNode("./Type")?.InnerText.ToLower())
				{
					case "website":
						siteSettings.SiteType = SiteType.SimpleSite;
						break;
					case "salescloud":
						siteSettings.SiteType = SiteType.SalesCloud;
						break;
					default:
						siteSettings.SiteType = SiteType.SalesCloud;
						break;
				}
				siteSettings.BaseUrl = siteNode.SelectSingleNode("./Url")?.InnerText;
				siteSettings.Title = siteNode.SelectSingleNode("./ComboName")?.InnerText ?? siteSettings.BaseUrl;
				if (!String.IsNullOrEmpty(siteSettings.BaseUrl))
					Sites.Add(siteSettings);
			}
		}
	}
}
