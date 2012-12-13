using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.SiteManager.BusinessClasses
{
	public class SiteManager
	{
		private static readonly SiteManager _instance = new SiteManager();

		public List<SitePermissionsManager> Sites { get; private set; }

		private SitePermissionsManager _selectedSite;
		public SitePermissionsManager SelectedSite
		{
			get { return _selectedSite ?? (_selectedSite = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SelectedSiteName) ? this.Sites.FirstOrDefault(x => x.Website == ConfigurationClasses.SettingsManager.Instance.SelectedSiteName) ?? this.Sites.FirstOrDefault() : this.Sites.FirstOrDefault()); }
		}

		public static SiteManager Instance
		{
			get { return _instance; }
		}

		private SiteManager()
		{
			this.Sites = new List<SitePermissionsManager>();
			Load();
		}

		private void Load()
		{
			this.Sites.Clear();
			if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SitesListPath))
			{
				var document = new XmlDocument();
				document.Load(ConfigurationClasses.SettingsManager.Instance.SitesListPath);

				var node = document.SelectSingleNode(@"/Sites");
				if (node != null)
					foreach (XmlNode childNode in node.ChildNodes)
						this.Sites.Add(new SitePermissionsManager(childNode));
			}
		}

		public void SelectSite(string siteName)
		{
			if (!string.IsNullOrEmpty(siteName))
			{
				_selectedSite = this.Sites.FirstOrDefault(x => x.Website == siteName);
				if (_selectedSite != null)
				{
					ConfigurationClasses.SettingsManager.Instance.SelectedSiteName = _selectedSite.Website;
					ConfigurationClasses.SettingsManager.Instance.Save();
				}
			}
		}
	}
}
