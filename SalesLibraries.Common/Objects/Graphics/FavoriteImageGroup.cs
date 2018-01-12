using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class FavoriteImageGroup : SourceFolderImageGroup
	{
		private const string IgnoredListFileName = "ignored.txt";

		public FavoriteImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		public override void LoadImages<TImageSource>()
		{
			base.LoadImages<TImageSource>();

			foreach (var imageSource in Images)
			{
				imageSource.RemoveFromFavs += (o, e) =>
				{
					RemoveImageFile<TImageSource>(imageSource.FilePath);
				};
			}
		}

		protected override IList<string> GetSourceFiles()
		{
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

			var sourceFiles = base.GetSourceFiles()
				.Where(filePath => !ignoredFiles.Any(ignoredFile => ignoredFile.Equals(Path.GetFileName(filePath), StringComparison.OrdinalIgnoreCase)))
				.ToList();

			return sourceFiles;
		}

		public void AddImageSource<TImageSource>(TImageSource imageSource) where TImageSource : BaseImageSource
		{
			var filePath = imageSource.FilePath;
			AddImageFile<TImageSource>(filePath);
		}

		public void AddImage<TImageSource>(Image image, string name) where TImageSource : BaseImageSource
		{
			if (image == null) return;
			var filePath = Path.Combine(Path.GetTempPath(), String.Format("{0}.png", name));
			image.Save(filePath);
			AddImageFile<TImageSource>(filePath);
		}

		public void AddImageFile<TImageSource>(string filePath) where TImageSource : BaseImageSource
		{
			if (!File.Exists(filePath)) return;
			if (!ParentList.FavsFolder.ExistsLocal())
				ParentList.FavsFolder.Allocate(false);
			File.Copy(filePath, Path.Combine(ParentList.FavsFolder.LocalPath, Path.GetFileName(filePath)), true);
			LoadImages<TImageSource>();
		}

		public void RemoveImageFile<TImageSource>(string filePath) where TImageSource : BaseImageSource
		{
			if (!File.Exists(filePath)) return;

			var ignoredFiles = new List<string>();
			var ignoredListPath = Path.Combine(_sourcePath, IgnoredListFileName);
			if (File.Exists(ignoredListPath))
				ignoredFiles.AddRange(File.ReadAllLines(ignoredListPath));
			ignoredFiles.Add(Path.GetFileName(filePath));

			File.WriteAllLines(ignoredListPath, ignoredFiles);

			LoadImages<TImageSource>();
		}
	}
}