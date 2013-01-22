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

		private readonly string _autoSyncSettingsPath = string.Empty;
		private readonly string _ribbonSettingsFilePath = string.Empty;
		private readonly string _settingsFilePath = string.Empty;
		private readonly string _syncSettingsFilePath = string.Empty;

		private SettingsManager()
		{
			ApplicationRootsPath = Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location);

			DestinationPathLength = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries\Primary Root", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)).Length;

			string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\file_manager", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(settingsFolderPath))
				Directory.CreateDirectory(settingsFolderPath);
			_settingsFilePath = Path.Combine(settingsFolderPath, "LocalSettings.xml");
			_autoSyncSettingsPath = Path.Combine(settingsFolderPath, "AutoSyncSchedule.xml");

			ArhivePath = Path.Combine(settingsFolderPath, "Archives");
			if (Directory.Exists(ArhivePath))
				Directory.CreateDirectory(ArhivePath);
			_syncSettingsFilePath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings\syncfile.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			LogRootPath = Path.Combine(settingsFolderPath, "Log");
			if (!Directory.Exists(LogRootPath))
				Directory.CreateDirectory(LogRootPath);

			ClientLogosRootPath = string.Empty;
			SalesGalleryRootPath = string.Empty;
			WebArtRootPath = string.Empty;
			AdSpecsSamplesRootPath = string.Empty;
			ScreenshotLibraryRootPath = string.Empty;

			AutoFMSyncShorcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "AutoFMSync.exe - Shortcut.lnk");
			VideoConverterPath = Path.Combine(ApplicationRootsPath, "video converter");

			#region FM Settings
			BackupPath = string.Empty;
			NetworkPath = string.Empty;
			UseDirectAccessToFiles = false;
			SelectedLibrary = string.Empty;
			SelectedPage = string.Empty;
			FontSize = 12;
			CalendarFontSize = 10;
			TreeViewVisible = false;
			TreeViewDocked = true;
			MultitabView = true;
			#endregion

			#region Ribbon Settings
			_ribbonSettingsFilePath = Path.Combine(ApplicationRootsPath, "ribbontabs.xml");
			EnableOvernightsCalendarTab = true;
			EnableClipartTab = true;
			EnableProgramManagerTab = true;
			EnableIPadSettingsTab = true;
			EnableIPadUsersTab = true;
			EnableTagsTab = true;
			ShowTagsCategories = true;
			LoadRibbonSettings();
			#endregion

			HiddenObjects = new List<string>();
			HiddenObjects.Add("!Old");
			HiddenObjects.Add(Constants.RegularPreviewContainersRootFolderName);
			HiddenObjects.Add(Constants.FtpPreviewContainersRootFolderName);
			HiddenObjects.Add(Constants.OvernightsCalendarRootFolderName);
			HiddenObjects.Add(Constants.ProgramManagerRootFolderName);
			HiddenObjects.Add(Constants.ExtraFoldersRootFolderName);
			HiddenObjects.Add("thumbs.db");
			HiddenObjects.Add("SalesDepotCache.xml");
		}

		public string ApplicationRootsPath { get; private set; }
		public string ArhivePath { get; set; }
		public string LogRootPath { get; set; }
		public string ClientLogosRootPath { get; set; }
		public string SalesGalleryRootPath { get; set; }
		public string WebArtRootPath { get; set; }
		public string AdSpecsSamplesRootPath { get; set; }
		public string ScreenshotLibraryRootPath { get; set; }
		public string AutoFMSyncShorcutPath { get; set; }
		public string VideoConverterPath { get; set; }

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
		public int SelectedCalendarYear { get; set; }
		public int FontSize { get; set; }
		public int CalendarFontSize { get; set; }
		public bool TreeViewVisible { get; set; }
		public bool TreeViewDocked { get; set; }
		public bool MultitabView { get; set; }
		#endregion

		#region Ribbon Settings
		public bool EnableOvernightsCalendarTab { get; private set; }
		public bool EnableClipartTab { get; private set; }
		public bool EnableProgramManagerTab { get; private set; }
		public bool EnableIPadSettingsTab { get; private set; }
		public bool EnableIPadUsersTab { get; private set; }
		public bool EnableTagsTab { get; private set; }
		public bool ShowTagsCategories { get; set; }
		public bool ShowTagsKeywords { get; set; }
		public bool ShowTagsFileCards { get; set; }
		public bool ShowTagsAttachments { get; set; }
		public bool ShowTagsCleaner { get; set; }
		#endregion

		public void Load()
		{
			if (File.Exists(_settingsFilePath))
			{
				var document = new XmlDocument();
				document.Load(_settingsFilePath);

				#region FM Settings
				var node = document.SelectSingleNode(@"/LocalSettings/BackupPath");
				if (node != null)
					BackupPath = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/NetworkPath");
				if (node != null)
					NetworkPath = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/UseDirectAccessToFiles");
				var tempBool = false;
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
				node = document.SelectSingleNode(@"/LocalSettings/TreeViewDocked");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						TreeViewDocked = tempBool;
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
			xml.AppendLine(@"<UseDirectAccessToFiles>" + UseDirectAccessToFiles.ToString() + @"</UseDirectAccessToFiles>");
			xml.AppendLine(@"<DirectAccessFileAgeLimit>" + DirectAccessFileAgeLimit.ToString() + @"</DirectAccessFileAgeLimit>");
			xml.AppendLine(@"<SelectedLibrary>" + SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
			xml.AppendLine(@"<SelectedPage>" + SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
			xml.AppendLine(@"<SelectedCalendarYear>" + SelectedCalendarYear.ToString() + @"</SelectedCalendarYear>");
			xml.AppendLine(@"<FontSize>" + FontSize.ToString() + @"</FontSize>");
			xml.AppendLine(@"<CalendarFontSize>" + CalendarFontSize.ToString() + @"</CalendarFontSize>");
			xml.AppendLine(@"<TreeViewVisible>" + TreeViewVisible.ToString() + @"</TreeViewVisible>");
			xml.AppendLine(@"<TreeViewDocked>" + TreeViewDocked.ToString() + @"</TreeViewDocked>");
			xml.AppendLine(@"<MultitabView>" + MultitabView.ToString() + @"</MultitabView>");
			#endregion

			xml.AppendLine(@"</LocalSettings>");

			using (var sw = new StreamWriter(_settingsFilePath, false))
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
			}
		}

		private void LoadClipartPath()
		{
			if (File.Exists(_syncSettingsFilePath))
			{
				var document = new XmlDocument();
				document.Load(_syncSettingsFilePath);

				XmlNode node = document.SelectSingleNode(@"/Settings/MediaProperty/Path");
				if (node != null)
				{
					string path = node.InnerText.Replace("\"", string.Empty).Trim();
					ClientLogosRootPath = Path.Combine(path, @"outgoing\gallery\client logos");
					SalesGalleryRootPath = Path.Combine(path, @"outgoing\gallery\sales gallery");
					WebArtRootPath = Path.Combine(path, @"outgoing\gallery\web art");
					AdSpecsSamplesRootPath = Path.Combine(path, @"outgoing\gallery\web art\Ad Specs-Samples");
					ScreenshotLibraryRootPath = Path.Combine(path, @"outgoing\gallery\web art\Screenshot Library");
				}
			}
		}

		public void SaveAutoSyncSettings(string settings)
		{
			using (var sw = new StreamWriter(_autoSyncSettingsPath, false))
			{
				sw.Write(settings);
				sw.Flush();
			}
		}
	}
}