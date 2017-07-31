using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class SourceFolderImageGroup : ImageSourceGroup
	{
		protected const string IgnoredListFileName = "ignored.txt";

		protected SourceFolderImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList)
		{
			_sourcePath = sourcePath;
		}

		public void LoadImages<T>() where T : BaseImageSource
		{
			Images.Clear();

			var ignoredFiles = new List<string>();

			var ignoredListPath = Path.Combine(_sourcePath, IgnoredListFileName);
			if (File.Exists(ignoredListPath))
			{
				ignoredFiles.AddRange(File.ReadAllLines(ignoredListPath));
				try
				{
					foreach (var ignoredFile in ignoredFiles)
					{
						var targetFile = Path.Combine(_sourcePath, ignoredFile);
						if (File.Exists(targetFile))
							File.Delete(targetFile);
					}
					File.Delete(ignoredListPath);
				}
				catch { }
			}

			if (Directory.Exists(_sourcePath))
				foreach (var filePath in Directory.GetFiles(_sourcePath, "*.png"))
				{
					if (ignoredFiles.Any(ignoredFile => ignoredFile.Equals(Path.GetFileName(filePath), StringComparison.OrdinalIgnoreCase)))
						continue;
					var linkImageSource = (T)Activator.CreateInstance(typeof(T), filePath);
					linkImageSource.AddToFavs += (o, e) =>
					{
						var favoritesImagesGroup = ParentList.Items.OfType<FavoriteImageGroup>().FirstOrDefault();
						if (favoritesImagesGroup == null) return;
						favoritesImagesGroup.AddImageSource(linkImageSource);
					};
					linkImageSource.RemoveFromFavs += (o, e) =>
					{
						var favoritesImagesGroup = ParentList.Items.OfType<FavoriteImageGroup>().FirstOrDefault();
						if (favoritesImagesGroup == null) return;
						favoritesImagesGroup.RemoveImageFile<T>(linkImageSource.FilePath);
					};
					Images.Add(linkImageSource);
				}

			Images.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.FileName, y.FileName));
			DataChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
