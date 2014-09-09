using System;
using System.IO;
using System.Text;
using System.Xml;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class SettingsManager
	{
		public const string AppName = "Sales Library Addin for Outlook";
		public const string NoLogoFileName = @"no_logo.png";
		public const string PageLogoFileTemplate = @"page{0}.*";

		private static readonly SettingsManager _instance = new SettingsManager();
		private readonly string _localSettingsFilePath = String.Empty;

		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		public bool IsConfigured { get; set; }

		public string LibraryRootFolder { get; set; }
		public string TempPath { get; set; }
		public string PermissionsFilePath { get; private set; }
		public string LibraryLogoFolder { get; set; }

		public string SelectedPackage { get; set; }
		public string SelectedLibrary { get; set; }
		public string SelectedPage { get; set; }

		private SettingsManager()
		{
			var settingsFolderPath = String.Format(@"{0}\newlocaldirect.com\xml\sales depot\Settings", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(settingsFolderPath))
				Directory.CreateDirectory(settingsFolderPath);
			_localSettingsFilePath = Path.Combine(settingsFolderPath, "ApplicationSettings.xml");
			LibraryRootFolder = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LibraryLogoFolder = String.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\!SD-Graphics\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); ;
			TempPath = String.Format(@"{0}\newlocaldirect.com\Sync\Temp", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			PermissionsFilePath = String.Format(@"{0}\newlocaldirect.com\Sales Depot\Library_Security.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LoadSettings();
		}

		private void LoadSettings()
		{
			SelectedPackage = String.Empty;
			SelectedLibrary = String.Empty;
			SelectedPage = String.Empty;

			if (!File.Exists(_localSettingsFilePath)) return;
			var document = new XmlDocument();
			try
			{
				document.Load(_localSettingsFilePath);
				IsConfigured = true;
			}
			catch { }

			var node = document.SelectSingleNode(@"/LocalSettings/SelectedPackage");
			if (node != null)
				SelectedPackage = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/SelectedLibrary");
			if (node != null)
				SelectedLibrary = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/SelectedPage");
			if (node != null)
				SelectedPage = node.InnerText;
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");
			if (!String.IsNullOrEmpty(SelectedPackage))
				xml.AppendLine(@"<SelectedPackage>" + SelectedPackage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPackage>");
			if (!String.IsNullOrEmpty(SelectedLibrary))
				xml.AppendLine(@"<SelectedLibrary>" + SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
			if (!String.IsNullOrEmpty(SelectedPage))
				xml.AppendLine(@"<SelectedPage>" + SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
			xml.AppendLine(@"</LocalSettings>");

			using (var sw = new StreamWriter(_localSettingsFilePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
			IsConfigured = true;
		}
	}
}
