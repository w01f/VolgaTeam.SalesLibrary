using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.SalesDepot.Configuration
{
	public class RemoteResourceManager
	{
		public static RemoteResourceManager Instance { get; } = new RemoteResourceManager();

		#region Local
		public StorageDirectory LocalLibraryFolder { get; private set; }
		public StorageFile EmailBinFile { get; private set; }
		#endregion

		#region Remote
		public StorageFile NetworkPathFile { get; private set; }
		public StorageFile DefaultViewFile { get; private set; }
		public StorageFile ViewButtonsFile { get; private set; }
		public StorageFile TabSettingsFile { get; private set; }
		public StorageFile Gallery1ConfigFile { get; private set; }
		public StorageFile Gallery2ConfigFile { get; private set; }
		public StorageFile CalendarRibbonLogoFile { get; private set; }
		#endregion

		private RemoteResourceManager() { }

		public async Task Load()
		{
			await Common.Helpers.RemoteResourceManager.Instance.LoadLocal();
			await Common.Helpers.RemoteResourceManager.Instance.LoadRemote();

			#region Local
			LocalLibraryFolder = new StorageDirectory(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				"Libraries"
			});
			if (!await LocalLibraryFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, "Libraries");

			EmailBinFile = new StorageFile(new object[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppNameSet,
				"EmailBin.xml"
			});
			EmailBinFile.AllocateParentFolder();
			#endregion

			#region Remote
			var appOutgoingFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppNameSet,
				"AppSettings"
			});

			NetworkPathFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"NetworkPath.txt"
				));
			await NetworkPathFile.Download();

			DefaultViewFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"DefaultView.xml"
				));
			await DefaultViewFile.Download();

			ViewButtonsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"ViewButtons.xml"
				));
			await ViewButtonsFile.Download();

			TabSettingsFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"SDTabNames.xml"
				));
			await TabSettingsFile.Download();

			Gallery1ConfigFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"Gallery1.xml"
				));
			await Gallery1ConfigFile.Download();

			Gallery2ConfigFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"Gallery2.xml"
				));
			await Gallery2ConfigFile.Download();

			CalendarRibbonLogoFile = new StorageFile(appOutgoingFolder.RelativePathParts.Merge(
				"oc_logo.png"
				));
			await CalendarRibbonLogoFile.Download();
			#endregion
		}
	}
}
