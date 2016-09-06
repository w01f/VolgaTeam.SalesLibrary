using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Helpers
{
	public class AppProfileManager
	{
		public const string UserDataFolderName = "user_data";

		private string _appName;
		private Guid _appID;
		private StorageFile _localAppIdFile;

		public static AppProfileManager Instance { get; } = new AppProfileManager();

		public AppTypeEnum AppType { get; private set; }

		public string LibraryAlias { get; private set; }

		public string[] AppNameSet
		{
			get
			{
				if (String.IsNullOrEmpty(LibraryAlias))
					return new[] { _appName };
				return new[] { _appName, LibraryAlias };
			}
		}

		private string ProfileName => String.Format("AppID-{0}", _appID);

		public StorageDirectory ProfilesRootFolder { get; private set; }
		public StorageDirectory ProfileFolder { get; private set; }
		public StorageDirectory UserDataFolder { get; private set; }

		private AppProfileManager() { }

		public void InitApplication(AppTypeEnum appType)
		{
			AppType = appType;
			switch (AppType)
			{
				case AppTypeEnum.FileManager:
					_appName = "app_site_admin";
					break;
				case AppTypeEnum.CloudAdmin:
					_appName = "app_cloud_admin";
					break;
				case AppTypeEnum.SalesDepot:
					_appName = "app_sales_library";
					break;
				default:
					throw new InvalidEnumArgumentException("Storage Type Undefined");
			}
		}

		public async Task LoadProfile()
		{
			_localAppIdFile = String.IsNullOrEmpty(LibraryAlias) ?
				new StorageFile(new[] { String.Format("{0}_app_id.xml", _appName) }) :
				new StorageFile(new[] { String.Format("{0}_{1}_app_id.xml", _appName, LibraryAlias) });

			_appID = Guid.Empty;
			if (File.Exists(_localAppIdFile.LocalPath))
			{
				var document = new XmlDocument();
				document.Load(_localAppIdFile.LocalPath);

				var node = document.SelectSingleNode(@"/AppID");
				if (!String.IsNullOrEmpty(node?.InnerText))
					_appID = new Guid(node.InnerText);
			}

			if (_appID.Equals(Guid.Empty))
				CreateProfile();

			ProfilesRootFolder = new StorageDirectory(new object[]
			{
				FileStorageManager.OutgoingFolderName,
				AppNameSet
			});
			if (!await ProfilesRootFolder.Exists(true))
				if (AppNameSet.Length > 1)
				{
					await StorageDirectory.CreateSubFolder(new[]
					{
						FileStorageManager.OutgoingFolderName
					}, _appName, true);
					await StorageDirectory.CreateSubFolder(new[]
					{
						FileStorageManager.OutgoingFolderName
					}, AppNameSet, true);
				}
				else
				{
					await StorageDirectory.CreateSubFolder(new[]
					{
						FileStorageManager.OutgoingFolderName
					}, AppNameSet, true);
				}

			ProfileFolder = new StorageDirectory(ProfilesRootFolder.RelativePathParts.Merge(ProfileName));
			if (!await ProfileFolder.Exists(true))
				await StorageDirectory.CreateSubFolder(ProfilesRootFolder.RelativePathParts, ProfileName, true);

			UserDataFolder = new StorageDirectory(ProfileFolder.RelativePathParts.Merge(new[] { UserDataFolderName }));
			if (!await UserDataFolder.Exists(true))
				await StorageDirectory.CreateSubFolder(ProfileFolder.RelativePathParts, UserDataFolderName, true);
		}

		private void CreateProfile()
		{
			_appID = Guid.NewGuid();
			var xml = new StringBuilder();

			xml.AppendLine(@"<AppID>" + _appID + @"</AppID>");

			_localAppIdFile.AllocateParentFolder();
			using (var sw = new StreamWriter(_localAppIdFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void SetLibraryAlias(string libraryAlias)
		{
			LibraryAlias = libraryAlias;
		}
	}
}
