using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SalesLibraries.Common.Authorization;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Helpers
{
	public class FileStorageManager
	{
		private const string RemoteStorageUrlTemplate = @"{0}/remote.php/webdav";

		private const string LocalAppSettingsFolderName = "adSalesApps Data";

		public const string IncomingFolderName = "outgoing";
		public const string OutgoingFolderName = "incoming";
		public const string CommonIncomingFolderName = "common";
		public const string LocalFilesFolderName = "local";

		private string _url;
		private string _login;
		private string _password;
		private string _dataFolderName;
		private string _authServer;

		private StorageFile _versionFile;

		public bool Activated { get; private set; }
		public string Version { get; private set; }
		public bool UseLocalMode { get; private set; }

		public event EventHandler<FileProcessingProgressEventArgs> Downloading;
		public event EventHandler<FileProcessingProgressEventArgs> Extracting;
		public event EventHandler<EventArgs> UsingLocalMode;
		public event EventHandler<AuthorizingEventArgs> Authorizing;

		public static FileStorageManager Instance { get; } = new FileStorageManager();

		public DataActualityState DataState { get; set; }

		private string RemoteStorageUrl
		{
			get { return String.Format(RemoteStorageUrlTemplate, _url); }
		}

		public string LocalStoragePath
		{
			get
			{
				if (Activated)
					return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LocalAppSettingsFolderName, _dataFolderName);
				return Path.GetTempPath();
			}
		}

		private FileStorageManager() { }

		public WebDAVClient.Client GetClient()
		{
			return GetClient(RemoteStorageUrl, _login, _password);
		}

		private WebDAVClient.Client GetClient(string url, string login, string password)
		{
			return new WebDAVClient.Client(
				new NetworkCredential
				{
					UserName = login,
					Password = password
				})
			{
				Server = url
			};
		}

		public async Task Init()
		{
			await InitCredentials();

			_versionFile = new ConfigFile(new object[]
			{
				IncomingFolderName,
				AppProfileManager.Instance.AppNameSet,
				"version.txt"
			});
			if (Activated)
				await CheckDataSate();
			if (Activated)
				Authorize();
		}

		private async Task InitCredentials()
		{
			var clientConfigPath = Path.Combine(Path.GetDirectoryName(typeof(FileStorageManager).Assembly.Location), "client.txt");
			if (!File.Exists(clientConfigPath))
				throw new FileNotFoundException("File client.txt not found");

			var clientInfo = File.ReadAllLines(clientConfigPath).Select(item => item.Trim()).ToList();

			_url = "https://adsalescloud.com";
			_login = "acct_cred";
			_password = "SzwX&4.2QF>~q!^L";

			AppProfileManager.Instance.SetLibraryAlias(clientInfo.ElementAtOrDefault(1));

			var credentialsFile = new ConfigFile(new[] { clientInfo.First(), "credentials.txt" });
			await credentialsFile.Download();
			if (!credentialsFile.ExistsLocal()) return;
			foreach (var configLine in File.ReadAllLines(credentialsFile.LocalPath))
			{
				if (configLine.Contains("Site:"))
					_url = configLine.Replace("Site:", "").Trim();
				else if (configLine.Contains("Login:"))
					_login = configLine.Replace("Login:", "").Trim();
				else if (configLine.Contains("Password:"))
					_password = configLine.Replace("Password:", "").Trim();
				else if (configLine.Contains("DataFolderName:"))
					_dataFolderName = configLine.Replace("DataFolderName:", "").Trim();
				else if (configLine.Contains("AuthService:"))
					_authServer = configLine.Replace("AuthService:", "").Trim();
			}

			Activated = true;

			if (!Directory.Exists(LocalStoragePath))
				Directory.CreateDirectory(LocalStoragePath);
		}

		private void Authorize()
		{
			if (Authorizing == null) return;
			var args = new AuthorizingEventArgs(_authServer);
			Authorizing(this, args);
			Activated = args.Authorized;
		}

		private async Task CheckDataSate()
		{
			DataState = DataActualityState.NotExisted;

			if (!_versionFile.ExistsLocal())
			{
				DataState = DataActualityState.NotExisted;
			}
			else
			{
				try
				{
					var remoteFile = await GetClient().GetFile(_versionFile.RemotePath);
					DataState = File.GetLastWriteTime(_versionFile.LocalPath) < remoteFile.LastModified ?
						DataActualityState.Outdated :
						DataActualityState.Updated;
				}
				catch (HttpRequestException e)
				{
					DataState = DataActualityState.Updated;
					SwitchToLocalMode();
				}
			}
		}

		public async Task FixDataState()
		{
			if (DataState != DataActualityState.Updated)
				await _versionFile.Download();

			if (_versionFile.ExistsLocal())
			{
				Version = File.ReadAllText(_versionFile.LocalPath);
				DataState = DataActualityState.Updated;
			}
			else
				Activated = false;

		}

		public void ShowDownloadProgress(FileProcessingProgressEventArgs eventArgs)
		{
			Downloading?.Invoke(this, eventArgs);
		}

		public void ShowExtractionProgress(FileProcessingProgressEventArgs eventArgs)
		{
			Extracting?.Invoke(this, eventArgs);
		}

		public void SwitchToLocalMode()
		{
			UsingLocalMode?.Invoke(this, EventArgs.Empty);
			UseLocalMode = true;
		}
	}
}
