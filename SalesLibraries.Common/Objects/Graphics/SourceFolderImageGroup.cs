using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class SourceFolderImageGroup : LinkImageGroup
	{
		protected const string IgnoredListFileName = "ignored.txt";

		protected SourceFolderImageGroup(IImageSourceList parentList) : base(parentList) { }

		public void LoadImages<T>(string sourcePath) where T : LinkImageSource
		{
			_sourcePath = sourcePath;

			Images.Clear();

			var ignoredFiles = new List<string>();

			var ignoredListPath = Path.Combine(sourcePath, IgnoredListFileName);
			if (File.Exists(ignoredListPath))
			{
				ignoredFiles.AddRange(File.ReadAllLines(ignoredListPath));
				try
				{
					foreach (var ignoredFile in ignoredFiles)
					{
						var targetFile = Path.Combine(sourcePath, ignoredFile);
						if (File.Exists(targetFile))
							File.Delete(targetFile);
					}
					File.Delete(ignoredListPath);
				}
				catch { }
			}

			foreach (var filePath in Directory.GetFiles(sourcePath, "*.png"))
			{
				if (ignoredFiles.Any(ignoredFile => ignoredFile.Equals(Path.GetFileName(filePath), StringComparison.OrdinalIgnoreCase)))
					continue;
				var linkImageSource = Activator.CreateInstance(typeof(T), filePath) as LinkImageSource;
				linkImageSource.AddToFavs += (o, e) =>
				{
					var favoritesImagesGroup = ParentList.Items.OfType<FavoriteImageGroup>().FirstOrDefault();
					if (favoritesImagesGroup == null) return;
					favoritesImagesGroup.AddImage<T>(linkImageSource.FilePath);
				};
				linkImageSource.RemoveFromFavs += (o, e) =>
				{
					var favoritesImagesGroup = ParentList.Items.OfType<FavoriteImageGroup>().FirstOrDefault();
					if (favoritesImagesGroup == null) return;
					favoritesImagesGroup.RemoveImage<T>(linkImageSource.FilePath);
				};
				Images.Add(linkImageSource);
			}

			Images.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.FileName, y.FileName));
			DataChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
