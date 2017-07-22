using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class FavoriteImageGroup : SourceFolderImageGroup
	{
		public FavoriteImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		public void AddImageSource<T>(T imageSource) where T : BaseImageSource
		{
			var filePath = imageSource.FilePath;
			AddImageFile<T>(filePath);
		}

		public void AddImage<T>(Image image, string name) where T : BaseImageSource
		{
			if (image == null) return;
			var filePath = Path.Combine(Path.GetTempPath(), String.Format("{0}.png", name));
			image.Save(filePath);
			AddImageFile<T>(filePath);
		}

		public void AddImageFile<T>(string filePath) where T : BaseImageSource
		{
			if (!File.Exists(filePath)) return;
			if (!ParentList.FavsFolder.ExistsLocal())
				ParentList.FavsFolder.Allocate(false);
			File.Copy(filePath, Path.Combine(ParentList.FavsFolder.LocalPath, Path.GetFileName(filePath)), true);
			LoadImages<T>();
		}

		public void RemoveImageFile<T>(string filePath) where T : BaseImageSource
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

			LoadImages<T>();
		}
	}
}