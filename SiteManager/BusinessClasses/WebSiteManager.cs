using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SalesDepot.Services;
using SalesDepot.SiteManager.ConfigurationClasses;

namespace SalesDepot.SiteManager.BusinessClasses
{
	public class WebSiteManager
	{
		private static readonly WebSiteManager _instance = new WebSiteManager();

		private SiteClient _selectedSite;

		private WebSiteManager()
		{
			Sites = new List<SiteClient>();
			Load();
		}

		public List<SiteClient> Sites { get; private set; }

		public SiteClient SelectedSite
		{
			get { return _selectedSite ?? (_selectedSite = !string.IsNullOrEmpty(SettingsManager.Instance.SelectedSiteName) ? Sites.FirstOrDefault(x => x.Website == SettingsManager.Instance.SelectedSiteName) ?? Sites.FirstOrDefault() : Sites.FirstOrDefault()); }
		}

		public static WebSiteManager Instance
		{
			get { return _instance; }
		}

		private void Load()
		{
			Sites.Clear();
			if (!File.Exists(SettingsManager.Instance.SitesListPath)) return;
			var document = new XmlDocument();
			document.Load(SettingsManager.Instance.SitesListPath);

			var node = document.SelectSingleNode(@"/Sites");
			if (node != null)
				foreach (XmlNode childNode in node.ChildNodes)
					Sites.Add(new SiteClient(childNode));
		}

		public void SelectSite(SiteClient site)
		{
			if (site == null) return;
			_selectedSite = site;
			SettingsManager.Instance.SelectedSiteName = _selectedSite.Website;
			SettingsManager.Instance.Save();
		}
	}
}