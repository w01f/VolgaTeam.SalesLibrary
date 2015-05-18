using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ConfigurationClasses
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();

		private readonly string _categoryRequestSettingsPath = string.Empty;
		private readonly string _ribbonSettingsFilePath = string.Empty;
		private readonly string _dashboardSyncSettingsFilePath = string.Empty;
		private readonly string _salesDepotSyncSettingsFilePath = string.Empty;
		private readonly string _serviceConnectionSettingsFilePath = string.Empty;

		private SettingsManager()
		{
			ApplicationRootPath = Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location);

			DestinationPathLength = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries\Primary Root", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)).Length;

			string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\file_manager", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(settingsFolderPath))
				Directory.CreateDirectory(settingsFolderPath);
			SettingsFilePath = Path.Combine(settingsFolderPath, "LocalSettings.xml");
			_categoryRequestSettingsPath = Path.Combine(ApplicationRootPath, "category_request.xml");

			_dashboardSyncSettingsFilePath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings\syncfile.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			_salesDepotSyncSettingsFilePath = Path.Combine(ApplicationRootPath, "synclock.xml");

			_serviceConnectionSettingsFilePath = Path.Combine(ApplicationRootPath, "credentials.xml");

			LogRootPath = Path.Combine(settingsFolderPath, "Log");
			if (!Directory.Exists(LogRootPath))
				Directory.CreateDirectory(LogRootPath);

			ClientLogosRootPath = string.Empty;
			SalesGalleryRootPath = string.Empty;
			WebArtRootPath = string.Empty;
			AdSpecsSamplesRootPath = string.Empty;
			ScreenshotLibraryRootPath = string.Empty;

			FFMpegPath = Path.Combine(ApplicationRootPath, "assets", "ffmpeg");

			TempPath = String.Format(@"{0}\newlocaldirect.com\Sync\Temp", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(TempPath))
				Directory.CreateDirectory(TempPath);

			#region FM Settings
			BackupPath = string.Empty;
			NetworkPath = string.Empty;
			UseDirectAccessToFiles = false;
			SelectedLibrary = string.Empty;
			SelectedPage = string.Empty;
			SelectedCalendar = string.Empty;
			FontSize = 12;
			CalendarFontSize = 10;
			TreeViewVisible = false;
			MultitabView = true;
			#endregion

			#region Ribbon Settings
			_ribbonSettingsFilePath = Path.Combine(ApplicationRootPath, "ribbontabs.xml");
			EnableOvernightsCalendarTab = true;
			EnableClipartTab = true;
			EnableProgramManagerTab = true;
			EnableIPadSettingsTab = true;
			EnableIPadUsersTab = true;
			EnableTagsTab = true;
			EnableSecurityTab = true;
			ShowTagsCategories = true;
			LoadRibbonSettings();
			#endregion

			LoadCategoryRequestSettings();
			LoadSalesDepotSyncSettings();
			LoadServiceConnectionSettings();

			HiddenObjects = new List<string>();
			HiddenObjects.Add("!Old");
			HiddenObjects.Add("_gsdata_");
			HiddenObjects.Add(Constants.RegularPreviewContainersRootFolderName);
			HiddenObjects.Add(Constants.FtpPreviewContainersRootFolderName);
			HiddenObjects.Add(Constants.OvernightsCalendarRootFolderName);
			HiddenObjects.Add(Constants.ProgramManagerRootFolderName);
			HiddenObjects.Add(Constants.ExtraFoldersRootFolderName);
			HiddenObjects.Add("thumbs.db");
			HiddenObjects.Add("SalesDepotCache.xml");
			HiddenObjects.Add("SalesDepotCacheLight.xml");
			HiddenObjects.Add("SalesDepotCache.json");
			HiddenObjects.Add("SalesDepotReferences.json");
		}

		public string ApplicationRootPath { get; private set; }
		public string LogRootPath { get; set; }
		public string ClientLogosRootPath { get; set; }
		public string SalesGalleryRootPath { get; set; }
		public string WebArtRootPath { get; set; }
		public string AdSpecsSamplesRootPath { get; set; }
		public string ScreenshotLibraryRootPath { get; set; }
		public string FFMpegPath { get; set; }
		public string TempPath { get; set; }
		public string SettingsFilePath { get; private set; }

		public int DestinationPathLength { get; private set; }

		public List<string> HiddenObjects { get; private set; }

		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		#region FM Settings
		public string BackupPath { get; set; }
		public string NetworkPath { get; set; }
		public bool UseDirectAccessToFiles { get; set; }
		public int DirectAccessFileAgeLimit { get; set; }
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
		public bool EnableClipartTab { get; private set; }
		public bool EnableProgramManagerTab { get; private set; }
		public bool EnableIPadSettingsTab { get; private set; }
		public bool EnableIPadUsersTab { get; private set; }
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
		public bool WebServiceConnected
		{
			get { return !String.IsNullOrEmpty(WebServiceSite) && !String.IsNullOrEmpty(WebServiceLogin) && !String.IsNullOrEmpty(WebServicePassword); }
		}
		#endregion

		public void Load()
		{
			if (File.Exists(SettingsFilePath))
			{
				var document = new XmlDocument();
				document.Load(SettingsFilePath);

				#region FM Settings
				XmlNode node = document.SelectSingleNode(@"/LocalSettings/BackupPath");
				if (node != null)
					BackupPath = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/NetworkPath");
				if (node != null)
					NetworkPath = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/UseDirectAccessToFiles");
				bool tempBool = false;
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						UseDirectAccessToFiles = tempBool;
				node = document.SelectSingleNode(@"/LocalSettings/DirectAccessFileAgeLimit");
				int tempInt = 0;
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						DirectAccessFileAgeLimit = tempInt;
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
			LoadClipartPath();
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");

			#region FM Settings
			xml.AppendLine(@"<BackupPath>" + BackupPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BackupPath>");
			xml.AppendLine(@"<NetworkPath>" + NetworkPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</NetworkPath>");
			xml.AppendLine(@"<UseDirectAccessToFiles>" + UseDirectAccessToFiles + @"</UseDirectAccessToFiles>");
			xml.AppendLine(@"<DirectAccessFileAgeLimit>" + DirectAccessFileAgeLimit + @"</DirectAccessFileAgeLimit>");
			xml.AppendLine(@"<SelectedLibrary>" + SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
			xml.AppendLine(@"<SelectedPage>" + SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
			xml.AppendLine(@"<SelectedCalendar>" + SelectedCalendar.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedCalendar>");
			xml.AppendLine(@"<SelectedCalendarYear>" + SelectedCalendarYear + @"</SelectedCalendarYear>");
			xml.AppendLine(@"<FontSize>" + FontSize + @"</FontSize>");
			xml.AppendLine(@"<CalendarFontSize>" + CalendarFontSize + @"</CalendarFontSize>");
			xml.AppendLine(@"<TreeViewVisible>" + TreeViewVisible + @"</TreeViewVisible>");
			xml.AppendLine(@"<MultitabView>" + MultitabView + @"</MultitabView>");
			#endregion

			xml.AppendLine(@"</LocalSettings>");

			using (var sw = new StreamWriter(SettingsFilePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void LoadRibbonSettings()
		{
			if (File.Exists(_ribbonSettingsFilePath))
			{
				var document = new XmlDocument();
				document.Load(_ribbonSettingsFilePath);

				XmlNode node = document.SelectSingleNode(@"/ribbon/OvernightsCalendar");
				bool tempBool;
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableOvernightsCalendarTab = tempBool;

				node = document.SelectSingleNode(@"/ribbon/Clipart");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableClipartTab = tempBool;

				node = document.SelectSingleNode(@"/ribbon/ProgramSchedule");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableProgramManagerTab = tempBool;

				node = document.SelectSingleNode(@"/ribbon/iPadsettings");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableIPadSettingsTab = tempBool;

				node = document.SelectSingleNode(@"/ribbon/Users");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableIPadUsersTab = tempBool;
				node = document.SelectSingleNode(@"/ribbon/Tags");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableTagsTab = tempBool;
				node = document.SelectSingleNode(@"/ribbon/Security");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableSecurityTab = tempBool;
			}
		}

		private void LoadCategoryRequestSettings()
		{
			if (File.Exists(_categoryRequestSettingsPath))
			{
				var document = new XmlDocument();
				document.Load(_categoryRequestSettingsPath);

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

		private void LoadClipartPath()
		{
			if (!File.Exists(_dashboardSyncSettingsFilePath)) return;
			var document = new XmlDocument();
			document.Load(_dashboardSyncSettingsFilePath);

			var node = document.SelectSingleNode(@"/Settings/MediaProperty/Path");
			if (node == null) return;
			string path = node.InnerText.Replace("\"", string.Empty).Trim();
			ClientLogosRootPath = Path.Combine(path, @"outgoing\gallery\client logos");
			SalesGalleryRootPath = Path.Combine(path, @"outgoing\gallery\sales gallery");
			WebArtRootPath = Path.Combine(path, @"outgoing\gallery\web art");
			AdSpecsSamplesRootPath = Path.Combine(path, @"outgoing\gallery\web art\Ad Specs-Samples");
			ScreenshotLibraryRootPath = Path.Combine(path, @"outgoing\gallery\web art\Screenshot Library");
		}

		private void LoadSalesDepotSyncSettings()
		{
			if (!File.Exists(_salesDepotSyncSettingsFilePath)) return;
			var document = new XmlDocument();
			document.Load(_salesDepotSyncSettingsFilePath);
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
			if (!File.Exists(_serviceConnectionSettingsFilePath)) return;
			var document = new XmlDocument();
			document.Load(_serviceConnectionSettingsFilePath);
			var node = document.SelectSingleNode(@"/ipadsite/site");
			if (node != null)
				WebServiceSite = node.InnerText;
			node = document.SelectSingleNode(@"/ipadsite/login");
			if (node != null)
				WebServiceLogin = node.InnerText;
			node = document.SelectSingleNode(@"/ipadsite/password");
			if (node != null)
				WebServicePassword = node.InnerText;
		}
	}
}