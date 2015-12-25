using System;
using System.IO;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public abstract class SourceLink
	{
		public Guid RootId { get; set; }
		public string Path { get; set; }
		
		public string Name
		{
			get { return System.IO.Path.GetFileName(Path); }
		}

		public string NameWithoutExtension
		{
			get { return System.IO.Path.GetFileNameWithoutExtension(Path); }
		}
	}

	public class FileLink : SourceLink { }

	public class FolderLink : SourceLink
	{
		public bool IsDrive
		{
			get
			{
				var directoryInfo = new DirectoryInfo(Path);
				return directoryInfo.FullName.Equals(directoryInfo.Root.FullName);
			}
		}
	}
}
