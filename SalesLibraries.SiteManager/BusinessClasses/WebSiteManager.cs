using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.ServiceConnector.Services.Soap;

namespace SalesLibraries.SiteManager.BusinessClasses
{
	public class WebSiteManager
	{
		private SoapServiceConnection _selectedSite;

		public List<SoapServiceConnection> Sites { get; }

		public SoapServiceConnection SelectedSite
		{
			get { return _selectedSite ?? (_selectedSite = !string.IsNullOrEmpty(SettingsManager.Instance.SelectedSiteName) ? Sites.FirstOrDefault(x => x.Website == SettingsManager.Instance.SelectedSiteName) ?? Sites.FirstOrDefault() : Sites.FirstOrDefault()); }
		}

		public static WebSiteManager Instance { get; } = new WebSiteManager();

		private WebSiteManager()
		{
			Sites = new List<SoapServiceConnection>();
		}

		public void Load(string siteListFilePath)
		{
			Sites.Clear();
			if (!File.Exists(siteListFilePath)) return;
			var document = new XmlDocument();
			document.Load(siteListFilePath);

			var node = document.SelectSingleNode(@"//Config/Sites") ?? document.SelectSingleNode(@"//Sites");
			if (node != null)
				foreach (XmlNode childNode in node.ChildNodes)
					Sites.Add(new SoapServiceConnection(childNode));
		}

		public void SelectSite(SoapServiceConnection site)
		{
			if (site == null) return;
			_selectedSite = site;
			SettingsManager.Instance.SelectedSiteName = _selectedSite.Website;
			SettingsManager.Instance.Save();
		}
	}
}