using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		#endregion

		public static SettingsManager Instance
		{
			get
			{
				return _instance;
			}
		}

		private SettingsManager()
		{
			this.ApplicationRootsPath = Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location);
			_settingsFilePath = Path.Combine(this.ApplicationRootsPath, "LocalSettings.xml");
			this.SitesListPath = Path.Combine(this.ApplicationRootsPath, "Sites.xml");
			this.LogoPath = Path.Combine(this.ApplicationRootsPath, "logo.png");
			this.IconPath = Path.Combine(this.ApplicationRootsPath, "icon.ico");
			Load();
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
					this.SelectedSiteName = node.InnerText;
				#endregion
			}
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");

			#region FM Settings
			xml.AppendLine(@"<SelectedSiteName>" + this.SelectedSiteName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedSiteName>");
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
