using System;
using System.IO;
using System.Threading.Tasks;
using SalesLibraries.Common.Helpers;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace SalesLibraries.Common.Objects.RemoteStorage
{
	public class ArchiveFile : StorageFile
	{
		private readonly ArchiveDirectory _asociatedDirectory;

		public ArchiveFile(object[] relativePathParts, ArchiveDirectory asociatedDirectory)
			: base(relativePathParts)
		{
			_asociatedDirectory = asociatedDirectory;
		}

		public override async Task Download()
		{
			await base.Download();
			if (_isOutdated || !_asociatedDirectory.ExistsLocal())
			{
				if (_asociatedDirectory.ExistsLocal())
					Utils.DeleteFolder(_asociatedDirectory.LocalPath);
				var contentLenght = new FileInfo(LocalPath).Length;
				Int64 alreadyRead = 0;
				using (Stream stream = File.OpenRead(LocalPath))
				{
					var reader = ReaderFactory.Open(stream);
					while (reader.MoveToNextEntry())
					{
						if (reader.Entry.IsDirectory) continue;
						alreadyRead += reader.Entry.CompressedSize;
						reader.WriteEntryToDirectory(GetParentFolder().LocalPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
						FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, contentLenght, alreadyRead));
					}
					FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, 100, 100));
				}
			}
		}
	}
}
