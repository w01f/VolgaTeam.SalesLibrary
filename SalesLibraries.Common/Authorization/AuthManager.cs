﻿using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.ServiceConnector.Services;

namespace SalesLibraries.Common.Authorization
{
	public class AuthManager
	{
		public StorageFile SettingsFile { get; private set; }
		public AuthSettings Settings { get; private set; }

		public void Init()
		{
			SettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Credentials.xml"
			});
			SettingsFile.AllocateParentFolder();

			Settings = AuthSettings.Load(SettingsFile);
		}

		public void Auth(AuthorizingEventArgs authArgs)
		{
			authArgs.Authorized = Settings.HasCredentials &&
				(FileStorageManager.Instance.UseLocalMode || 
					ServiceConnection.IsAuthorized(authArgs.AuthServer, Settings.Login, Settings.GetPassword()));
		}
	}
}