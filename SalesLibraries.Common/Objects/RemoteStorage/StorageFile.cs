using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using WebDAVClient.Helpers;
using WebDAVClient.Model;

namespace SalesLibraries.Common.Objects.RemoteStorage
{
	public class StorageFile : StorageItem
	{
		private readonly Item _remoteSource;
		protected bool _isOutdated;

		public string Extension
		{
			get { return Path.GetExtension(LocalPath); }
		}

		public StorageFile(object[] relativePathParts) : base(relativePathParts) { }

		public StorageFile(object[] parentPathParts, Item remoteSource)
			: base(parentPathParts.Merge(remoteSource.GetName()))
		{
			_remoteSource = remoteSource;
		}

		public static async Task<bool> Exists(string[] relativePathParts, bool checkRemoteToo = false)
		{
			var storageItem = new StorageFile(relativePathParts);
			return await storageItem.Exists(checkRemoteToo);
		}

		public override bool ExistsLocal()
		{
			return File.Exists(LocalPath);
		}

		protected override async Task<bool> ExistsRemote()
		{
			try
			{
				if (FileStorageManager.Instance.UseLocalMode) return false;
				await FileStorageManager.Instance.GetClient().GetFile(RemotePath);
				return true;
			}
			catch (WebDAVException exception)
			{
				return false;
			}
			catch (HttpRequestException e)
			{
				FileStorageManager.Instance.SwitchToLocalMode();
				return FileStorageManager.Instance.UseLocalMode;
			}
		}

		public async Task Upload()
		{
			if (FileStorageManager.Instance.UseLocalMode) return;
			var tempFile = Path.GetTempFileName();
			File.Copy(LocalPath, tempFile, true);
			var client = FileStorageManager.Instance.GetClient();
			await AllocateParentFolder(true);
			try
			{
				await client.Upload(RemotePath, File.OpenRead(tempFile), String.Empty);
			}
			catch (HttpRequestException e)
			{
				FileStorageManager.Instance.SwitchToLocalMode();
			}
			catch { }
		}

		public virtual async Task Download()
		{
			try
			{
				var client = FileStorageManager.Instance.GetClient();
				if ((ExistsLocal() && FileStorageManager.Instance.DataState == DataActualityState.Updated) || FileStorageManager.Instance.UseLocalMode)
					return;
				var remoteFile = _remoteSource ?? await client.GetFile(RemotePath);
				_isOutdated = !(ExistsLocal() && File.GetLastWriteTime(LocalPath) >= remoteFile.LastModified);
				if (_isOutdated)
				{
					AllocateParentFolder();
					using (var remoteStream = await client.Download(RemotePath))
					{
						if (remoteStream != null)
						{
							using (var localStream = File.Create(LocalPath))
							{
								var contentLenght = remoteFile.ContentLength.HasValue ? remoteFile.ContentLength.Value : 0;
								var buffer = new byte[1024];
								int bytesRead;
								int alreadyRead = 0;
								do
								{
									bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
									alreadyRead += bytesRead;
									FileStorageManager.Instance.ShowDownloadProgress(new FileProcessingProgressEventArgs(NameOnly, contentLenght, alreadyRead));
									localStream.Write(buffer, 0, bytesRead);
								}
								while (bytesRead > 0);
								localStream.Close();
							}
							remoteStream.Close();
						}
					}
				}
			}
			catch (WebDAVException exception)
			{
				throw new FileNotFoundException(String.Format("Error downloading file {0}", LocalPath));
			}
			catch (HttpRequestException e)
			{
				FileStorageManager.Instance.SwitchToLocalMode();
			}
		}

		public async Task AllocateParentFolder(bool checkRemoteToo)
		{
			await GetParentFolder().Allocate(checkRemoteToo);
		}

		public void AllocateParentFolder()
		{
			AsyncHelper.RunSync(() => GetParentFolder().Allocate(false));
		}
	}
}
