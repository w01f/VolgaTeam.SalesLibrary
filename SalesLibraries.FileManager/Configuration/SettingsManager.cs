using System;
using System.IO;
using System.Text;
using System.Xml;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.FileManager.Configuration
{
	class SettingsManager
	{
		public string FFMpegPackagePath { get; set; }

		#region FM Settings
		public string BackupPath { get; set; }
		public string NetworkPath { get; set; }
		public string WebPath { get; set; }
		//----------------------------------------
		public string SelectedLibrary { get; set; }
		public string SelectedPage { get; set; }
		public string SelectedCalendar { get; set; }
		public int SelectedCalendarYear { get; set; }
		public int FontSize { get; set; }
		public int CalendarFontSize { get; set; }
		public bool TreeViewVisible { get; set; }
		public bool MultitabView { get; set; }
		#endregion

		#region Ribbon Settings
		public bool EnableOvernightsCalendarTab { get; private set; }
		public bool EnableProgramManagerTab { get; private set; }
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
		public bool SyncLockByInactiveLinks { get; private set; }
		public bool SyncLockByUnconvertedVideo { get; private set; }
		public string SyncSupportEmail { get; private set; }
		#endregion

		#region Service Connection Settings
		public string WebServiceSite { get; private set; }
		public string WebServiceLogin { get; private set; }
		public string WebServicePassword { get; private set; }
		#endregion

		public EditorsSettings EditorSettings { get; private set; }

		public SettingsManager()
		{
			FFMpegPackagePath = Path.Combine(GlobalSettings.ApplicationRootPath, "assets", "ffmpeg");

			#region FM Settings
			BackupPath = String.Empty;
			NetworkPath = String.Empty;
			WebPath = String.Empty;
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
			XmlNode node = document.SelectSingleNode(@"/LocalSettings/BackupPath");
			if (node != null)
				BackupPath = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/NetworkPath");
			if (node != null)
				NetworkPath = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/WebPath");
			if (node != null)
				WebPath = node.InnerText;
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
			#endregion
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");

			#region FM Settings
			xml.AppendLine(@"<BackupPath>" + BackupPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BackupPath>");
			xml.AppendLine(@"<NetworkPath>" + NetworkPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</NetworkPath>");
			xml.AppendLine(@"<WebPath>" + WebPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</WebPath>");
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

			XmlNode node = document.SelectSingleNode(@"/ribbon/OvernightsCalendar");
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
			node = document.SelectSingleNode(@"/synclock/inactivelink");
			{
				bool temp;
				if (node != null && Boolean.TryParse(node.InnerText, out temp))
					SyncLockByInactiveLinks = temp;
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
			WebServiceSite = File.ReadAllText(RemoteResourceManager.Instance.SiteFile.LocalPath).Trim();
		}
	}
}
