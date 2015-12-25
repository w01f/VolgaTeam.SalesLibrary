using System.IO;
using System.Threading.Tasks;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.RemoteStorage
{
	public class ConfigFile : StorageFile
	{
		public ConfigFile(object[] relativePathParts) : base(relativePathParts) { }

		protected override async Task<bool> ExistsRemote()
		{
			try
			{
				await FileStorageManager.Instance.GetClient().GetFile(RemotePath);
				return true;
			}
			catch
			{
				return ExistsLocal();
			}
		}

		public override async Task Download()
		{
			try
			{
				var client = FileStorageManager.Instance.GetClient();
				var remoteFile = await client.GetFile(RemotePath);
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
			catch
			{
			}
		}
	}
}
