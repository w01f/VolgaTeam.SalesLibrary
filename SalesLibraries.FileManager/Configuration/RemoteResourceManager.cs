﻿using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.FileManager.Configuration
{
	public class RemoteResourceManager
	{
		public static RemoteResourceManager Instance { get; } = new RemoteResourceManager();

		#region Local
		public StorageDirectory MetaDataCacheFolder { get; private set; }
		public StorageFile VideoConverterGridLayoutFile { get; private set; }
		#endregion

		#region Remote
		public StorageFile SiteFile { get; private set; }
		public StorageFile CategoryRequestSettingsFile { get; private set; }
		public StorageFile ErrorEmailSettingsFile { get; private set; }
		public StorageFile TabSettingsFile { get; private set; }
		public StorageFile ArchiveLinksSettingsFile { get; private set; }

		public ArchiveDirectory ImageResourcesFolder { get; private set; }
		public ArchiveDirectory InternalLinkTemplatesFolder { get; private set; }
		#endregion

		private RemoteResourceManager() { }

		public async Task LoadLocal()
		{
			await Common.Helpers.RemoteResourceManager.Instance.LoadLocal();

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

			VideoConverterGridLayoutFile = new StorageFile(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
				"Video-Converter-Layout.xml"
			});
		}

		public async Task LoadRemote()
		{
			await Common.Helpers.RemoteResourceManager.Instance.LoadRemote();

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

			ArchiveLinksSettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
			"ArchiveLinksSettings.xml"
			));
			if (await ArchiveLinksSettingsFile.Exists(true))
				await ArchiveLinksSettingsFile.Download();

			ImageResourcesFolder = new ArchiveDirectory(new object[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppNameSet,
				"Resources"
			});
			if (await ImageResourcesFolder.Exists(true))
				await ImageResourcesFolder.Download();

			InternalLinkTemplatesFolder = new ArchiveDirectory(new object[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"shared",
				"InternlalLinkTemplates"
			});
			await InternalLinkTemplatesFolder.Download();
		}
	}
}
