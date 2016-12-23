using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.FileManager.Configuration
{
	class SettingsManager
	{
		public string FFMpegPackagePath { get; set; }

		#region FM Settings
		public string BackupPath { get; set; }
		public List<string> NetworkPaths { get; }
		public List<string> WebPaths { get; }

		public bool EnableLocalSync => NetworkPaths.Any();
		public bool EnableWebSync => WebPaths.Any();

		public string SelectedLibrary { get; set; }
		public string SelectedPage { get; set; }
		public string SelectedCalendar { get; set; }
		public int SelectedCalendarYear { get; set; }
		public int FontSize { get; set; }
		public int CalendarFontSize { get; set; }
		public bool TreeViewVisible { get; set; }
		public bool MultitabView { get; set; }
		public string DefaultBannerSettingsEncoded { get; set; }
		#endregion

		#region Ribbon Settings
		public bool EnableOvernightsCalendarTab { get; private set; }
		public bool EnableProgramManagerTab { get; private set; }
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
		public string WebServiceLogin { get; private set; }
		public string WebServicePassword { get; private set; }
		#endregion

		public EditorsSettings EditorSettings { get; }

		public SettingsManager()
		{
			FFMpegPackagePath = Path.Combine(GlobalSettings.ApplicationRootPath, "assets", "ffmpeg");

			#region FM Settings
			BackupPath = String.Empty;
			NetworkPaths = new List<String>();
			WebPaths = new List<String>();
			SelectedLibrary = String.Empty;
			SelectedPage = String.Empty;
			SelectedCalendar = String.Empty;
			FontSize = 12;
			CalendarFontSize = 10;
			TreeViewVisible = false;
			MultitabView = true;
			#endregion

			#region Ribbon Settings
			EnableOvernightsCalendarTab = true;
			EnableProgramManagerTab = true;
			EnableIPadSettingsTab = true;
			EnableTagsTab = true;
			EnableSecurityTab = true;
			EnableLinkBundlesTab = true;
			ShowTagsCategories = true;
			#endregion

			EditorSettings = new EditorsSettings();
		}

		public void LoadLocal()
		{
			if (!Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.ExistsLocal()) return;
			int tempInt;
			bool tempBool;

			NetworkPaths.Clear();
			WebPaths.Clear();

			var document = new XmlDocument();
			document.Load(Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.LocalPath);

			#region FM Settings
			XmlNode node = document.SelectSingleNode(@"/LocalSettings/BackupPath");
			if (node != null)
				BackupPath = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/NetworkPath");
			if (node != null && !String.IsNullOrEmpty(node.InnerText))
				NetworkPaths.AddRange(node.InnerText.Split('|'));
			node = document.SelectSingleNode(@"/LocalSettings/WebPath");
			if (node != null && !String.IsNullOrEmpty(node.InnerText))
				WebPaths.AddRange(node.InnerText.Split('|'));
			node = document.SelectSingleNode(@"/LocalSettings/SelectedLibrary");
			if (node != null)
				SelectedLibrary = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/SelectedPage");
			if (node != null)
				SelectedPage = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/SelectedCalendar");
			if (node != null)
				SelectedCalendar = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/SelectedCalendarYear");
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					SelectedCalendarYear = tempInt;
			node = document.SelectSingleNode(@"/LocalSettings/FontSize");
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					FontSize = tempInt;
			node = document.SelectSingleNode(@"/LocalSettings/CalendarFontSize");
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					CalendarFontSize = tempInt;
			node = document.SelectSingleNode(@"/LocalSettings/TreeViewVisible");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					TreeViewVisible = tempBool;
			node = document.SelectSingleNode(@"/LocalSettings/MultitabView");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					MultitabView = tempBool;
			node = document.SelectSingleNode(@"/LocalSettings/DefaultBannerSettingsEncoded");
			if (node != null)
				DefaultBannerSettingsEncoded = Encoding.UTF8.GetString(Convert.FromBase64String(node.InnerText));
			#endregion
		}

		public void LoadRemote()
		{
			LoadRibbonSettings();
			LoadCategoryRequestSettings();
			LoadServiceConnectionSettings();
			LoadArchiveLinksSettings();

			EditorSettings.Load();
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");

			#region FM Settings
			xml.AppendLine(@"<BackupPath>" + BackupPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BackupPath>");
			if (EnableLocalSync)
				xml.AppendLine(@"<NetworkPath>" + String.Join("|", NetworkPaths.Select(p => p.Replace(@"&", "&#38;").Replace("\"", "&quot;"))) + @"</NetworkPath>");
			if (EnableWebSync)
				xml.AppendLine(@"<WebPath>" + String.Join("|", WebPaths.Select(p => p.Replace(@"&", "&#38;").Replace("\"", "&quot;"))) + @"</WebPath>");
			if (!String.IsNullOrEmpty(SelectedLibrary))
				xml.AppendLine(@"<SelectedLibrary>" + SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
			if (!String.IsNullOrEmpty(SelectedPage))
				xml.AppendLine(@"<SelectedPage>" + SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
			if (!String.IsNullOrEmpty(SelectedCalendar))
				xml.AppendLine(@"<SelectedCalendar>" + SelectedCalendar.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedCalendar>");
			xml.AppendLine(@"<SelectedCalendarYear>" + SelectedCalendarYear + @"</SelectedCalendarYear>");
			xml.AppendLine(@"<FontSize>" + FontSize + @"</FontSize>");
			xml.AppendLine(@"<CalendarFontSize>" + CalendarFontSize + @"</CalendarFontSize>");
			xml.AppendLine(@"<TreeViewVisible>" + TreeViewVisible + @"</TreeViewVisible>");
			xml.AppendLine(@"<MultitabView>" + MultitabView + @"</MultitabView>");
			if (!String.IsNullOrEmpty(DefaultBannerSettingsEncoded))
				xml.AppendLine(@"<DefaultBannerSettingsEncoded>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(DefaultBannerSettingsEncoded)) + @"</DefaultBannerSettingsEncoded>");
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

			var node = document.SelectSingleNode(@"/ribbon/OvernightsCalendar");
			bool tempBool;
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableOvernightsCalendarTab = tempBool;

			node = document.SelectSingleNode(@"/ribbon/ProgramSchedule");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableProgramManagerTab = tempBool;

			node = document.SelectSingleNode(@"/ribbon/iPadsettings");
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
			WebServiceSite = File.ReadAllText(RemoteResourceManager.Instance.SiteFile.LocalPath).Trim();
		}
	}
}
