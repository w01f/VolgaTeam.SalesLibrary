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
		public StorageFile UsersEditPermissionsSettingsFile { get; private set; }
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

			UsersEditPermissionsSettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"site_manager_account_controls.xml"));
			if (await UsersEditPermissionsSettingsFile.Exists(true))
				await UsersEditPermissionsSettingsFile.Download();
			#endregion
		}
	}
}
