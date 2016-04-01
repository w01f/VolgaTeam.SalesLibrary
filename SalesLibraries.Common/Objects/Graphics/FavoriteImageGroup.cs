using System.Collections.Generic;
using System.IO;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class FavoriteImageGroup : SourceFolderImageGroup
	{
		public FavoriteImageGroup(IImageSourceList parentList) : base(parentList) { }

		public void AddImage<T>(string filePath) where T : LinkImageSource
		{
			if (!File.Exists(filePath)) return;
			if (!ParentList.FavsFolder.ExistsLocal())
				ParentList.FavsFolder.Allocate(false);
			File.Copy(filePath, Path.Combine(ParentList.FavsFolder.LocalPath, Path.GetFileName(filePath)), true);
			LoadImages<T>(ParentList.FavsFolder.LocalPath);
		}

		public void RemoveImage<T>(string filePath) where T : LinkImageSource
		{
			if (!File.Exists(filePath)) return;
			if (!ParentList.FavsFolder.ExistsLocal())
				ParentList.FavsFolder.Allocate(false);

			var ignoredFiles = new List<string>();
			var ignoredListPath = Path.Combine(_sourcePath, IgnoredListFileName);
			if (File.Exists(ignoredListPath))
				ignoredFiles.AddRange(File.ReadAllLines(ignoredListPath));
			ignoredFiles.Add(Path.GetFileName(filePath));

			File.WriteAllLines(ignoredListPath, ignoredFiles);

			LoadImages<T>(ParentList.FavsFolder.LocalPath);
		}
	}
}