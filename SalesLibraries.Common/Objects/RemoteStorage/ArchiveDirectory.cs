using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.RemoteStorage
{
	public class ArchiveDirectory : StorageDirectory
	{
		private readonly ArchiveFile _sourceArchiveFile;

		public ArchiveDirectory(object[] relativePathParts)
			: base(relativePathParts)
		{
			_sourceArchiveFile = new ArchiveFile(GetParentFolder().RelativePathParts.Merge(String.Format("{0}.rar", Name)), this);
		}

		public async Task Download()
		{
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated) return;
			await _sourceArchiveFile.Download();
		}

		public void AddFiles(IEnumerable<string> files)
		{
			foreach (var file in files)
				File.Copy(file, Path.Combine(LocalPath, Path.GetFileName(file)), true);
		}

		public async Task Upload()
		{
			var tempArchiveFile = new ArchiveFile(RelativePathParts.Merge(String.Format("{0}.zip", Name)), this);
			Utils.CompressFiles(Directory.GetFiles(LocalPath), tempArchiveFile.LocalPath);
			await tempArchiveFile.AllocateParentFolder(true);
			await tempArchiveFile.Upload();
		}

		protected async override Task<bool> ExistsRemote()
		{
			return await _sourceArchiveFile.Exists(true);
		}
	}
}
