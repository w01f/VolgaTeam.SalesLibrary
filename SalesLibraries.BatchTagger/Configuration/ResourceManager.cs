using System.IO;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.BatchTagger.Configuration
{
	public class ResourceManager
	{
		public string SiteConfigPath { get; }
		public string MetaDataCacheFolderPath { get; }
		public string TotalControlLayoutConfigPath { get; }
		public string LibraryControlLayoutConfigPath { get; }

		public ResourceManager()
		{
			SiteConfigPath = Path.Combine(GlobalSettings.ApplicationRootPath, "Site.xml");
			TotalControlLayoutConfigPath = Path.Combine(GlobalSettings.ApplicationRootPath, "TotalLayoutConfig.xml");
			LibraryControlLayoutConfigPath = Path.Combine(GlobalSettings.ApplicationRootPath, "LibraryLayoutConfig.xml");

			MetaDataCacheFolderPath = Path.Combine(GlobalSettings.ApplicationRootPath, "Cache");
			if (!Directory.Exists(MetaDataCacheFolderPath))
				Directory.CreateDirectory(MetaDataCacheFolderPath);
		}
	}
}
