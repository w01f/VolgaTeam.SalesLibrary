using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesLibraries.SiteManager.ConfigurationClasses
{
	public class SettingsManager
	{
		#region Path
		private readonly string _settingsFilePath;
		public string ApplicationRootsPath { get; }
		public string SitesListPath { get; }
		public string IconPath { get; set; }
		public string LogoPath { get; set; }
		#endregion

		#region Local Settings
		public string SelectedSiteName { get; set; }
		public int? SelectedTab { get; set; }
		public List<UsersEmailSettings> UsersEmailSettingItems { get; }
		public InactiveUsersSettings InactiveUsersSettings { get; }
		public List<RibbonTabPageConfig> RibbonTabPageSettings { get; }
		public List<string> ApprovedUsers { get; }
		#endregion

		private SettingsManager()
		{
			ApplicationRootsPath = Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location);
			_settingsFilePath = Path.Combine(ApplicationRootsPath, "LocalSettings.xml");
			SitesListPath = Path.Combine(ApplicationRootsPath, "Sites.xml");
			LogoPath = Path.Combine(ApplicationRootsPath, "logo.png");
			IconPath = Path.Combine(ApplicationRootsPath, "icon.ico");
			UsersEmailSettingItems = new List<UsersEmailSettings>();
			InactiveUsersSettings = new InactiveUsersSettings();
			RibbonTabPageSettings = new List<RibbonTabPageConfig>();
			ApprovedUsers = new List<String>();
		}

		public static SettingsManager Instance { get; } = new SettingsManager();

		public void Load()
		{
			if (File.Exists(_settingsFilePath))
			{
				var document = new XmlDocument();
				document.Load(_settingsFilePath);

				#region Local Settings
				var node = document.SelectSingleNode(@"/LocalSettings/SelectedSiteName");
				if (node != null)
					SelectedSiteName = node.InnerText;

				node = document.SelectSingleNode(@"/LocalSettings/SelectedTab");
				if (node != null)
					if (int.TryParse(node.InnerText, out var tempInt))
						SelectedTab = tempInt;
				#endregion
			}

			LoadUsersEmailSettings();
			InactiveUsersSettings.Load();
			LoadRibbonTabSettings();
			LoadApprovedUsers();
		}

		private void LoadRibbonTabSettings()
		{
			if (RemoteResourceManager.Instance.SettingsFile == null || !RemoteResourceManager.Instance.SettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.SettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"//Config/TabSettings");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var tabPageConfig = new RibbonTabPageConfig();
				tabPageConfig.Deserialize(childNode);
				if (tabPageConfig.Visible)
					RibbonTabPageSettings.Add(tabPageConfig);
			}
			RibbonTabPageSettings.Sort((x, y) => x.Order.CompareTo(y.Order));
		}

		private void LoadApprovedUsers()
		{
			if (RemoteResourceManager.Instance.SettingsFile == null || !RemoteResourceManager.Instance.SettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.SettingsFile.LocalPath);
			var nodes = document.SelectNodes(@"//Config/ApprovedUsers/UserAccount");
			if (nodes == null) return;
			ApprovedUsers.AddRange(nodes.OfType<XmlNode>().Select(n => n.InnerText));
		}

		private void LoadUsersEmailSettings()
		{
			UsersEmailSettingItems.Clear();
			UsersEmailSettingItems.AddRange(UsersEmailSettings.LoadFromXml());
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