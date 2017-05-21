using System.IO;
using System.Xml;
using SalesLibraries.ServiceConnector.Services.Rest;
using SalesLibraries.ServiceConnector.Services.Soap;

namespace SalesLibraries.BatchTagger.BusinessClasses.Helpers
{
	public class ConnectionManager
	{
		public SoapServiceConnection SoapConnection { get; private set; }

		public RestServiceConnection RestConnection { get; private set; }

		public void Load()
		{
			if (!File.Exists(AppManager.Instance.Resources.SiteConfigPath)) return;
			var document = new XmlDocument();
			document.Load(AppManager.Instance.Resources.SiteConfigPath);

			var node = document.SelectSingleNode(@"/Site");
			if (node != null)
			{
				SoapConnection = new SoapServiceConnection(node);

				RestConnection = new RestServiceConnection();
				RestConnection.Load(SoapConnection.Website, "FileManagerData");
			}
		}
	}
}
