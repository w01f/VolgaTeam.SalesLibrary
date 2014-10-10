using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;

namespace AutoSynchronizer.ConfigurationClasses
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();

		private readonly string _settingsFilePath = string.Empty;

		private SettingsManager()
		{
			DestinationPathLength = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries\Primary Root", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)).Length;

			string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\file_manager", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(settingsFolderPath))
				Directory.CreateDirectory(settingsFolderPath);
			_settingsFilePath = Path.Combine(settingsFolderPath, "LocalSettings.xml");
			ArhivePath = Path.Combine(settingsFolderPath, "Archives");
			if (Directory.Exists(ArhivePath))
				Directory.CreateDirectory(ArhivePath);
			LogRootPath = Path.Combine(settingsFolderPath, "Log");
			if (!Directory.Exists(LogRootPath))
				Directory.CreateDirectory(LogRootPath);

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

			HiddenFolders = new List<string>();
			HiddenFolders.Add("!Old");
			HiddenFolders.Add(Constants.RegularPreviewContainersRootFolderName);
			HiddenFolders.Add(Constants.FtpPreviewContainersRootFolderName);
			HiddenFolders.Add(Constants.OvernightsCalendarRootFolderName);
			HiddenFolders.Add(Constants.ProgramManagerRootFolderName);
			HiddenFolders.Add(Constants.ExtraFoldersRootFolderName);
		}

		public string ArhivePath { get; set; }
		public string LogRootPath { get; set; }

		public int DestinationPathLength { get; private set; }

		public List<string> HiddenFolders { get; private set; }

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
		public int FontSize { get; set; }
		public int CalendarFontSize { get; set; }
		public bool TreeViewVisible { get; set; }
		public bool TreeViewDocked { get; set; }
		public bool MultitabView { get; set; }
		#endregion

		public void Load()
		{
			XmlNode node;
			int tempInt = 0;
			bool tempBool = false;
			if (File.Exists(_settingsFilePath))
			{
				var document = new XmlDocument();
				document.Load(_settingsFilePath);

				#region FM Settings
				node = document.SelectSingleNode(@"/LocalSettings/BackupPath");
				if (node != null)
					BackupPath = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/NetworkPath");
				if (node != null)
					NetworkPath = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/UseDirectAccessToFiles");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						UseDirectAccessToFiles = tempBool;
				node = document.SelectSingleNode(@"/LocalSettings/DirectAccessFileAgeLimit");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						DirectAccessFileAgeLimit = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedLibrary");
				if (node != null)
					SelectedLibrary = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedPage");
				if (node != null)
					SelectedPage = node.InnerText;
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
	}

	public class SearchGroup
	{
		public SearchGroup()
		{
			Name = string.Empty;
			Description = string.Empty;
			Tags = new List<string>();
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public Image Logo { get; set; }
		public List<string> Tags { get; set; }
	}
}