using System.Collections.Generic;
using System.IO;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Contexts.Wallbin
{
	public class WallbinManager
	{
		public List<LibraryContext> Libraries { get; set; }

		public WallbinManager()
		{
			Libraries = new List<LibraryContext>();
		}

		public void LoadLibraries(string rootPath)
		{
			Libraries.Clear();
			if (!Directory.Exists(rootPath)) return;
			if (rootPath.Equals(Path.GetPathRoot(rootPath)))
				Libraries.Add(new LibraryContext(Constants.WholeDriveFilesStorage, rootPath));
			else
			{
				foreach (var subFolderPath in Directory.GetDirectories(rootPath))
					Libraries.Add(new LibraryContext(Path.GetFileName(subFolderPath), subFolderPath));
			}
		}

		public void LoadLibrary(string rootPath)
		{
			Libraries.Clear();
			Libraries.Add(new LibraryContext(Constants.WholeDriveFilesStorage, rootPath));
		}
	}
}
