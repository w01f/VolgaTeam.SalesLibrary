using System;
using System.IO;
using System.Text;
using System.Xml;

namespace OvernightsCalendarViewer.ConfigurationClasses
{
	public class SettingsManager
	{
		public const string NoLogoFileName = @"no_logo.png";
		private static readonly SettingsManager _instance = new SettingsManager();

		private readonly string _localLibraryLogoFolder = string.Empty;
		private readonly string _localLibraryRootFolder = string.Empty;
		private readonly string _localSettingsFilePath = string.Empty;

		private SettingsManager()
		{
			string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\sales depot\Settings", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_localSettingsFilePath = Path.Combine(settingsFolderPath, "OvernightsSettings.xml");
			_localLibraryRootFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_localLibraryLogoFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\!SD-Graphics\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\sdicon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LibraryLogoFolder = string.Empty;
			DisclaimerPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Nielsen Permissible Use.pdf", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		}

		public string IconPath { get; set; }
		public string LibraryRootFolder { get; set; }
		public string LibraryLogoFolder { get; set; }
		public string DisclaimerPath { get; set; }

		public string SelectedPackage { get; set; }
		public string SelectedLibrary { get; set; }
		public string SelectedCalendar { get; set; }
		public int SelectedCalendarYear { get; set; }
		public int FontSize { get; set; }
		public int RowSpace { get; set; }
		public int CalendarFontSize { get; set; }
		
		public Guid AppID { get; set; }


		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		public void UpdateSetingsAccordingConfiguration()
		{
			LibraryRootFolder = _localLibraryRootFolder;
			LibraryLogoFolder = _localLibraryLogoFolder;
		}

		public void LoadSettings()
		{
			SelectedPackage = string.Empty;
			SelectedLibrary = string.Empty;
			SelectedCalendar = string.Empty;
			SelectedCalendarYear = 0;
			FontSize = 12;
			RowSpace = 1;
			CalendarFontSize = 10;

			string settingsPath = _localSettingsFilePath;
			if (File.Exists(settingsPath))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(settingsPath);
				}
				catch { }

				var node = document.SelectSingleNode(@"/LocalSettings/SelectedPackage");
				if (node != null)
					SelectedPackage = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedLibrary");
				if (node != null)
					SelectedLibrary = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedCalendar");
				if (node != null)
					SelectedLibrary = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedCalendarYear");
				int tempInt = 0;
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						SelectedCalendarYear = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/FontSize");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						FontSize = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/RowSpace");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						RowSpace = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/CalendarFontSize");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						CalendarFontSize = tempInt;
			}

			UpdateSetingsAccordingConfiguration();
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");
			xml.AppendLine(@"<SelectedPackage>" + SelectedPackage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPackage>");
			xml.AppendLine(@"<SelectedLibrary>" + SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
			xml.AppendLine(@"<SelectedCalendar>" + SelectedCalendar.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedCalendar>");
			xml.AppendLine(@"<SelectedCalendarYear>" + SelectedCalendarYear + @"</SelectedCalendarYear>");
			xml.AppendLine(@"<FontSize>" + FontSize + @"</FontSize>");
			xml.AppendLine(@"<RowSpace>" + RowSpace + @"</RowSpace>");
			xml.AppendLine(@"<CalendarFontSize>" + CalendarFontSize + @"</CalendarFontSize>");
			xml.AppendLine(@"</LocalSettings>");

			string settingsPath = _localSettingsFilePath;
			using (var sw = new StreamWriter(settingsPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}


		public bool CheckLibraries()
		{
			bool result = false;
			if (Directory.Exists(LibraryRootFolder))
				result = ((new DirectoryInfo(LibraryRootFolder)).GetDirectories()).Length > 0;
			return result;
		}

		
	}
}