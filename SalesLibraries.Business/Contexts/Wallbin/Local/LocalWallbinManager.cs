using System.Collections.Generic;
using System.IO;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Contexts.Wallbin.Local
{
	public class LocalWallbinManager
	{
		public List<LocalLibraryContext> Libraries { get; set; }

		public LocalWallbinManager()
		{
			Libraries = new List<LocalLibraryContext>();
		}

		public void LoadLibraries(string rootPath)
		{
			Libraries.Clear();
			if (!Directory.Exists(rootPath)) return;
			if (rootPath.Equals(Path.GetPathRoot(rootPath)))
				Libraries.Add(new LocalLibraryContext(Constants.PrimaryFileStorageName, rootPath));
			else
			{
				foreach (var subFolderPath in Directory.GetDirectories(rootPath))
					Libraries.Add(new LocalLibraryContext(Path.GetFileName(subFolderPath), subFolderPath));
			}
		}

		public void LoadLibrary(string rootPath)
		{
			Libraries.Clear();
			Libraries.Add(new LocalLibraryContext(Constants.PrimaryFileStorageName, rootPath));
		}
	}
}
