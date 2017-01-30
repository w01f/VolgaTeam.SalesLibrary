using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public abstract class SourceLink
	{
		public Guid RootId { get; set; }
		public string Path { get; set; }
		public bool IsExternal { get; set; }

		public string Name => System.IO.Path.GetFileName(Path);

		public string NameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(Path);

		public static SourceLink FromExternalPath(string path, IDataSource dataSource)
		{
			SourceLink sourceLink;
			if (Directory.Exists(path))
				sourceLink = new FolderLink();
			else if (File.Exists(path))
				sourceLink = new FileLink();
			else
			{
				throw new ArgumentOutOfRangeException("Undefined path");
			}
			sourceLink.Path = path;
			sourceLink.RootId = dataSource.DataSourceId;
			sourceLink.IsExternal = true;
			return sourceLink;
		}
	}

	public class FileLink : SourceLink { }

	public class FolderLink : SourceLink
	{
		public List<FileInfo> SelectedFiles { get; } = new List<FileInfo>();

		public bool IsDrive
		{
			get
			{
				var directoryInfo = new DirectoryInfo(Path);
				return directoryInfo.FullName.Equals(directoryInfo.Root.FullName);
			}
		}

		public IList<FileInfo> GetAllFiles()
		{
			return GetAllFiles(Path);
		}

		private static IList<FileInfo> GetAllFiles(string rootDirectoryPath)
		{
			var files = new List<FileInfo>();

			foreach (var directoryPath in Directory.GetDirectories(rootDirectoryPath))
			{
				files.AddRange(GetAllFiles(directoryPath));
			}

			files.AddRange(Directory.GetFiles(rootDirectoryPath).Select(filePath => new FileInfo(filePath)));

			return files;
		}
	}
}
