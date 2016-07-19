using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.CloudAdmin.Configuration
{
	public class RemoteResourceManager
	{
		public static RemoteResourceManager Instance { get; } = new RemoteResourceManager();

		#region Local
		public StorageDirectory MetaDataCacheFolder { get; private set; }
		public StorageDirectory LocalLibraryCacheFolder { get; private set; }
		#endregion

		#region Remote
		public StorageFile SiteFile { get; private set; }
		public StorageFile CategoryRequestSettingsFile { get; private set; }
		public StorageFile ErrorEmailSettingsFile { get; private set; }
		public StorageFile TabSettingsFile { get; private set; }
		public StorageFile SyncLockSettingsFile { get; private set; }
		#endregion

		private RemoteResourceManager() { }

		public async Task Load()
		{
			await Common.Helpers.RemoteResourceManager.Instance.Load();

			#region Local
			MetaDataCacheFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
				"Cache"
			});
			if (!await MetaDataCacheFolder.Exists())
				await StorageDirectory.CreateSubFolder(new object[]
				{
					FileStorageManager.LocalFilesFolderName, 
					AppProfileManager.Instance.AppNameSet
				}, "Cache");

			LocalLibraryCacheFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
				"Library"
			});
			if (!await LocalLibraryCacheFolder.Exists())
				await StorageDirectory.CreateSubFolder(new object[]
				{
					FileStorageManager.LocalFilesFolderName,
					AppProfileManager.Instance.AppNameSet
				}, "Library");
			#endregion

			#region Remote
			var appOutgoingFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppNameSet,
				"AppSettings"
			});

			SiteFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"Site.txt"
				));
			await SiteFile.Download();

			CategoryRequestSettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"CategoryRequest.xml"
				));
			await CategoryRequestSettingsFile.Download();

			ErrorEmailSettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"ErrorEmail.xml"
				));
			await ErrorEmailSettingsFile.Download();

			TabSettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"RibbonTabs.xml"
				));
			await TabSettingsFile.Download();

			SyncLockSettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"SyncLock.xml"
				));
			await SyncLockSettingsFile.Download();
			#endregion
		}
	}
}
