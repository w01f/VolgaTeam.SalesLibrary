using System.Threading.Tasks;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Helpers
{
	public class RemoteResourceManager
	{
		public static RemoteResourceManager Instance { get; } = new RemoteResourceManager();

		#region Local
		public StorageDirectory AppSettingsFolder { get; private set; }
		public StorageDirectory TempFolder { get; private set; }

		public StorageFile AppSettingsFile { get; private set; }
		#endregion

		#region Remote
		public ArchiveDirectory ArtworkFolder { get; private set; }
		public ArchiveDirectory ThemesFolder { get; private set; }
		public ArchiveDirectory LauncherTemplatesFolder { get; private set; }

		public StorageFile DefaultSlideSettingsFile { get; private set; }
		public StorageFile HelpFile { get; private set; }
		public StorageFile HelpBrowserFile { get; private set; }
		#endregion

		private RemoteResourceManager() { }

		public async Task Load()
		{
			#region Local
			TempFolder = new StorageDirectory(new[]
			{
				"Temp"
			});
			if (TempFolder.ExistsLocal())
				Utils.CleanFolder(TempFolder.LocalPath);
			if (!TempFolder.ExistsLocal())
				await StorageDirectory.CreateSubFolder(new string[] { }, "Temp");

			AppSettingsFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
			});
			if (!await AppSettingsFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, AppProfileManager.Instance.AppNameSet);

			AppSettingsFile = new StorageFile(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
				"Settings.xml"
			});
			AppSettingsFile.AllocateParentFolder();
			#endregion

			#region Remote
			ArtworkFolder = new ArchiveDirectory(new object[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppNameSet,
				"Artwork"
			});
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
			#endregion
		}
	}
}
