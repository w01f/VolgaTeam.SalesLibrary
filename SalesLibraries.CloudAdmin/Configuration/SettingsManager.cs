using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
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
		#endregion

		#region Ribbon Settings
		public bool EnableIPadSettingsTab { get; private set; }
		public bool EnableTagsTab { get; private set; }
		public bool EnableSecurityTab { get; private set; }
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

		#region Sales Depot Sync Settings
		public bool SyncLockByUntaggedLinks { get; private set; }
		public bool SyncLockByUnconvertedVideo { get; private set; }
		public string SyncSupportEmail { get; private set; }
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
			ShowTagsCategories = true;
			#endregion

			EditorSettings = new EditorsSettings();
		}

		public void Load()
		{
			LoadRibbonSettings();
			LoadCategoryRequestSettings();
			LoadSalesDepotSyncSettings();
			LoadServiceConnectionSettings();

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
			node = document.SelectSingleNode(@"/LocalSettings/CalendarFontSize");
			node = document.SelectSingleNode(@"/LocalSettings/TreeViewVisible");
			node = document.SelectSingleNode(@"/LocalSettings/MultitabView");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					MultitabView = tempBool;
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

		private void LoadSalesDepotSyncSettings()
		{
			if (!RemoteResourceManager.Instance.SyncLockSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.SyncLockSettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"/synclock/untaggedlink");
			{
				bool temp;
				if (node != null && Boolean.TryParse(node.InnerText, out temp))
					SyncLockByUntaggedLinks = temp;
			}
			node = document.SelectSingleNode(@"/synclock/unconvertedvideo");
			{
				bool temp;
				if (node != null && Boolean.TryParse(node.InnerText, out temp))
					SyncLockByUnconvertedVideo = temp;
			}
			node = document.SelectSingleNode(@"/synclock/email");
			if (node != null)
				SyncSupportEmail = node.InnerText;
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
