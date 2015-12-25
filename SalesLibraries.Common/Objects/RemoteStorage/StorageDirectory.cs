using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using WebDAVClient.Helpers;

namespace SalesLibraries.Common.Objects.RemoteStorage
{
	public class StorageDirectory : StorageItem
	{
		public StorageDirectory(object[] relativePathParts)
			: base(relativePathParts)
		{ }

		public static async Task<bool> Exists(string[] relativePathParts, bool checkRemoteToo = false)
		{
			var storageItem = new StorageDirectory(relativePathParts);
			return await storageItem.Exists(checkRemoteToo);
		}

		public static async Task CreateSubFolder(object[] relativePathParts, string name, bool remoteToo = false)
		{
			await CreateSubFolder(relativePathParts, new[] { name }, remoteToo);
		}

		public static async Task CreateSubFolder(object[] relativePathParts, string[] nameSet, bool remoteToo = false)
		{
			var storageItem = new StorageDirectory(relativePathParts);
			await storageItem.CreateSubFolder(nameSet, remoteToo);
		}

		public override bool ExistsLocal()
		{
			return Directory.Exists(LocalPath);
		}

		protected override async Task<bool> ExistsRemote()
		{
			try
			{
				if (FileStorageManager.Instance.UseLocalMode) return false;
				await FileStorageManager.Instance.GetClient().GetFolder(RemotePath);
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

		private async Task CreateSubFolder(string[] nameSet, bool remoteToo = false)
		{
			var subFolderLocalPath = Path.Combine(LocalPath, Path.Combine(nameSet));
			if (!Directory.Exists(subFolderLocalPath))
				Directory.CreateDirectory(subFolderLocalPath);
			if (remoteToo && !FileStorageManager.Instance.UseLocalMode)
			{
				var client = FileStorageManager.Instance.GetClient();
				try
				{
					await client.CreateDir(RemotePath, String.Format(@"/{0}", String.Join(@"/", nameSet)));
				}
				catch (HttpRequestException e)
				{
					FileStorageManager.Instance.SwitchToLocalMode();
				}
			}
		}

		public IEnumerable<StorageFile> GetFiles(Func<string, bool> filter = null, bool recursive = false)
		{
			if (filter == null)
				filter = item => true;

			var items = new List<StorageFile>();
			if (recursive)
			{
				foreach (var directoryPath in Directory.GetDirectories(LocalPath))
				{
					var subDirectory = new StorageDirectory(RelativePathParts.Merge(Path.GetFileName(directoryPath)));
					items.AddRange(subDirectory.GetFiles(filter, true));
				}
			}
			items.AddRange(Directory.GetFiles(LocalPath)
					.Where(filePath => filter(Path.GetFileName(filePath)))
					.Select(filePath => new StorageFile(RelativePathParts.Merge(Path.GetFileName(filePath)))));
			return items;
		}

		public IEnumerable<StorageDirectory> GetFolders(Func<string, bool> filter = null)
		{
			if (filter == null)
				filter = item => true;

			var items = new List<StorageDirectory>();
			items.AddRange(Directory.GetDirectories(LocalPath)
					.Where(directoryPath => filter(Path.GetFileName(directoryPath)))
					.Select(directoryPath => new StorageDirectory(RelativePathParts.Merge(Path.GetFileName(directoryPath)))));
			return items;
		}

		public async Task Allocate(bool remoteToo)
		{
			if (!Directory.Exists(LocalPath))
				Directory.CreateDirectory(LocalPath);
			if (remoteToo && !await ExistsRemote())
			{
				await GetParentFolder().Allocate(true);
				await CreateSubFolder(RelativePathParts, String.Empty, true);
			}
		}
	}
}
