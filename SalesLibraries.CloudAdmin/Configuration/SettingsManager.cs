using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.CloudAdmin.Configuration
{
	class SettingsManager
	{
		public string FFMpegPackagePath { get; set; }

		#region FM Settings

		//----------------------------------------
		public string SelectedPage { get; set; }
		public int FontSize { get; set; }
		public bool MultitabView { get; set; }
		public string DefaultBannerSettingsEncoded { get; set; }
		#endregion

		#region Ribbon Settings
		public bool EnableIPadSettingsTab { get; private set; }
		public bool EnableTagsTab { get; private set; }
		public bool EnableSecurityTab { get; private set; }
		public bool EnableLinkBundlesTab { get; private set; }
		public bool ShowTagsCategories { get; set; }
		public bool ShowTagsSuperFilters { get; set; }
		public bool ShowTagsKeywords { get; set; }
		public bool ShowTagsCleaner { get; set; }
		#endregion

		#region Category Request Settings
		public string CategoryRequestRecipients { get; set; }
		public string CategoryRequestSubject { get; set; }
		public string CategoryRequestBody { get; set; }
		#endregion

		#region Service Connection Settings
		public string WebServiceSite { get; private set; }
		public string SiteLibrary { get; private set; }
		#endregion

		public EditorsSettings EditorSettings { get; private set; }

		public SettingsManager()
		{
			FFMpegPackagePath = Path.Combine(GlobalSettings.ApplicationRootPath, "assets", "ffmpeg");

			#region FM Settings
			SelectedPage = String.Empty;
			FontSize = 12;
			MultitabView = true;
			#endregion

			#region Ribbon Settings
			EnableIPadSettingsTab = true;
			EnableTagsTab = true;
			EnableSecurityTab = true;
			EnableLinkBundlesTab = true;
			ShowTagsCategories = true;
			#endregion

			EditorSettings = new EditorsSettings();
		}

		public void Load()
		{
			LoadRibbonSettings();
			LoadCategoryRequestSettings();
			LoadServiceConnectionSettings();
			LoadArchiveLinksSettings();

			EditorSettings.Load();

			if (!Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.ExistsLocal()) return;
			int tempInt;
			bool tempBool;
			var document = new XmlDocument();
			document.Load(Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.LocalPath);

			#region FM Settings
			var node = document.SelectSingleNode(@"/LocalSettings/SelectedPage");
			if (node != null)
				SelectedPage = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/FontSize");
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					FontSize = tempInt;
			//node = document.SelectSingleNode(@"/LocalSettings/CalendarFontSize");
			//node = document.SelectSingleNode(@"/LocalSettings/TreeViewVisible");
			node = document.SelectSingleNode(@"/LocalSettings/MultitabView");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					MultitabView = tempBool;
			node = document.SelectSingleNode(@"/LocalSettings/DefaultLinkBannerSettingsEncoded");
			if (node != null)
				DefaultBannerSettingsEncoded = Encoding.UTF8.GetString(Convert.FromBase64String(node.InnerText));
			#endregion
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");

			#region FM Settings
			if (!String.IsNullOrEmpty(SelectedPage))
				xml.AppendLine(@"<SelectedPage>" + SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
			xml.AppendLine(@"<FontSize>" + FontSize + @"</FontSize>");
			xml.AppendLine(@"<MultitabView>" + MultitabView + @"</MultitabView>");
			if (!String.IsNullOrEmpty(DefaultBannerSettingsEncoded))
				xml.AppendLine(@"<DefaultLinkBannerSettingsEncoded>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(DefaultBannerSettingsEncoded)) + @"</DefaultLinkBannerSettingsEncoded>");
			#endregion

			xml.AppendLine(@"</LocalSettings>");

			using (var sw = new StreamWriter(Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void LoadRibbonSettings()
		{
			if (!RemoteResourceManager.Instance.TabSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.TabSettingsFile.LocalPath);

			var node = document.SelectSingleNode(@"/ribbon/iPadsettings");
			bool tempBool;
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableIPadSettingsTab = tempBool;

			node = document.SelectSingleNode(@"/ribbon/Tags");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableTagsTab = tempBool;
			node = document.SelectSingleNode(@"/ribbon/Security");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableSecurityTab = tempBool;
			node = document.SelectSingleNode(@"/ribbon/LinkBundles");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableLinkBundlesTab = tempBool;
		}

		private void LoadCategoryRequestSettings()
		{
			if (RemoteResourceManager.Instance.CategoryRequestSettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(RemoteResourceManager.Instance.CategoryRequestSettingsFile.LocalPath);

				XmlNode node = document.SelectSingleNode(@"/catrequest/recipients");
				if (node != null)
					CategoryRequestRecipients = node.InnerText;

				node = document.SelectSingleNode(@"/catrequest/subject");
				if (node != null)
					CategoryRequestSubject = node.InnerText;

				node = document.SelectSingleNode(@"/catrequest/body");
				if (node != null)
					CategoryRequestBody = node.InnerText;
			}
		}

		private void LoadArchiveLinksSettings()
		{
			if (!RemoteResourceManager.Instance.ArchiveLinksSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.ArchiveLinksSettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"/Config/MaxPdfPages");
			{
				int temp;
				if (node != null && Int32.TryParse(node.InnerText, out temp))
					WallbinConfiguration.MaxPreviewPdfPagesCount = temp;
			}
		}

		private void LoadServiceConnectionSettings()
		{
			if (!RemoteResourceManager.Instance.SiteFile.ExistsLocal()) return;
			var configLines =File.ReadAllLines(RemoteResourceManager.Instance.SiteFile.LocalPath)
				.Select(line => line.Trim())
				.ToList();
			if(configLines.Count<2) return;
			WebServiceSite = configLines.ElementAt(0);
			SiteLibrary = configLines.ElementAt(1);
		}
	}
}
