using System.IO;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.BatchTagger.Configuration
{
	public class ResourceManager
	{
		public string SiteConfigPath { get; }
		public string MetaDataCacheFolderPath { get; }

		public ResourceManager()
		{
			SiteConfigPath = Path.Combine(GlobalSettings.ApplicationRootPath, "Site.xml");

			MetaDataCacheFolderPath = Path.Combine(GlobalSettings.ApplicationRootPath, "Cache");
			if (!Directory.Exists(MetaDataCacheFolderPath))
				Directory.CreateDirectory(MetaDataCacheFolderPath);
		}
	}
}
