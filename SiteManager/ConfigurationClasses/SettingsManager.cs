using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.SiteManager.ConfigurationClasses
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();

		#region Path
		private readonly string _settingsFilePath;
		public string ApplicationRootsPath { get; private set; }
		public string SitesListPath { get; private set; }
		public string IconPath { get; set; }
		public string LogoPath { get; set; }
		#endregion

		#region Local Settings
		public string SelectedSiteName { get; set; }
		public int SelectedTab { get; set; }
		public InactiveUsersSettings InactiveUsersSettings { get; private set; }
		public AppWindowSettings AppWindowSettings { get; private set; }
		#endregion

		private SettingsManager()
		{
			ApplicationRootsPath = Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location);
			_settingsFilePath = Path.Combine(ApplicationRootsPath, "LocalSettings.xml");
			SitesListPath = Path.Combine(ApplicationRootsPath, "Sites.xml");
			LogoPath = Path.Combine(ApplicationRootsPath, "logo.png");
			IconPath = Path.Combine(ApplicationRootsPath, "icon.ico");
			InactiveUsersSettings = new InactiveUsersSettings();
			AppWindowSettings = new AppWindowSettings();
			Load();
		}

		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		public void Load()
		{
			XmlNode node;
			if (File.Exists(_settingsFilePath))
			{
				var document = new XmlDocument();
				document.Load(_settingsFilePath);

				#region Local Settings
				node = document.SelectSingleNode(@"/LocalSettings/SelectedSiteName");
				if (node != null)
					SelectedSiteName = node.InnerText;

				int tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedTab");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						SelectedTab = tempInt;
				#endregion
			}

			InactiveUsersSettings.Load();
			AppWindowSettings.Load();
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");

			#region FM Settings
			xml.AppendLine(@"<SelectedSiteName>" + SelectedSiteName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedSiteName>");
			xml.AppendLine(@"<SelectedTab>" + SelectedTab + @"</SelectedTab>");
			#endregion

			xml.AppendLine(@"</LocalSettings>");

			using (var sw = new StreamWriter(_settingsFilePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}