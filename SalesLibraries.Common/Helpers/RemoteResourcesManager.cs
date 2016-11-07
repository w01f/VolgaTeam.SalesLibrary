using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Helpers
{
	public class RemoteResourceManager
	{
		public static RemoteResourceManager Instance { get; } = new RemoteResourceManager();

		#region Local
		public StorageDirectory AppSharedSettingsFolder { get; private set; }
		public StorageDirectory AppAliasSettingsFolder { get; private set; }
		public StorageDirectory TempFolder { get; private set; }

		public StorageFile AppSettingsFile { get; private set; }
		#endregion

		#region Remote
		public ArchiveDirectory ArtworkFolder { get; private set; }
		public ArchiveDirectory ThemesFolder { get; private set; }
		public ArchiveDirectory LauncherTemplatesFolder { get; private set; }

		public StorageFile DefaultSlideSettingsFile { get; private set; }
		public StorageFile SlideSizeSettingsFile { get; private set; }
		public StorageFile HelpFile { get; private set; }
		public StorageFile HelpBrowserFile { get; private set; }
		#endregion

		private RemoteResourceManager() { }

		public async Task LoadLocal()
		{
			TempFolder = new StorageDirectory(new[]
			{
				"Temp"
			});
			if (TempFolder.ExistsLocal())
				Utils.CleanFolder(TempFolder.LocalPath);
			if (!TempFolder.ExistsLocal())
				await StorageDirectory.CreateSubFolder(new string[] { }, "Temp");

			AppSharedSettingsFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppName,
				"shared"
			});
			if (!await AppSharedSettingsFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, new[] { AppProfileManager.Instance.AppName, "shared" });

			AppAliasSettingsFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
			});
			if (!await AppAliasSettingsFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, AppProfileManager.Instance.AppNameSet);

			AppSettingsFile = new StorageFile(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
				"Settings.xml"
			});
			AppSettingsFile.AllocateParentFolder();
		}

		public async Task LoadRemote()
		{
			ArtworkFolder = new ArchiveDirectory(new object[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"shared_artwork",
				"Artwork"
			});
			if (!await ArtworkFolder.Exists(true))
			{
				ArtworkFolder = new ArchiveDirectory(new object[]
				{
					FileStorageManager.IncomingFolderName,
					AppProfileManager.Instance.AppNameSet,
					"Artwork"
				});
			}
			await ArtworkFolder.Download();

			ThemesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SellerPointThemes"
			});
			await ThemesFolder.Download();

			LauncherTemplatesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"LauncherTemplates"
			});
			await LauncherTemplatesFolder.Download();

			DefaultSlideSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"AppSettings",
				"DefaultSlideSettings.xml"
			});
			await DefaultSlideSettingsFile.Download();

			SlideSizeSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"AppSettings",
				"SlideSizeSettings.xml"
			});
			await SlideSizeSettingsFile.Download();

			HelpFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"HelpUrls",
				HelpManager.GetFileName()
			});
			await HelpFile.Download();

			HelpBrowserFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"HelpUrls",
				"!Help_Browser.xml"
			});
			await HelpBrowserFile.Download();
		}
	}
}
