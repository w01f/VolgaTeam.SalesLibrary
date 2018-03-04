using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.SiteManager.ConfigurationClasses
{
	public class RemoteResourceManager
	{
		public static RemoteResourceManager Instance { get; } = new RemoteResourceManager();

		#region Remote
		public StorageFile SettingsFile { get; private set; }
		#endregion

		private RemoteResourceManager() { }

		public async Task Load()
		{
			#region Remote
			var appOutgoingFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppNameSet,
				"AppSettings"
			});

			SettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"site_manager_settings.xml"));
			await SettingsFile.Download();
			#endregion
		}
	}
}
