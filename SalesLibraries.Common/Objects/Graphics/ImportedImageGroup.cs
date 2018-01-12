using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class ImportedImageGroup : RegularImageGroup
	{
		private const string IgnoredListFileName = "ignored.txt";

		protected ImportedImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		public override void LoadImages<TImageSource>()
		{
			base.LoadImages<TImageSource>();

			foreach (var imageSource in Images)
			{
				imageSource.RemoveFromImported += (o, e) =>
				{
					RemoveImageFile<TImageSource>(imageSource.FilePath);
				};
			}
		}

		public void AddImage<TImageSource>(string imageFilePath) where TImageSource : BaseImageSource
		{
			try
			{
				if (!Directory.Exists(_sourcePath))
					Directory.CreateDirectory(_sourcePath);

				AddImageInternal(imageFilePath);

				LoadImages<TImageSource>();
			}
			catch (Exception e)
			{
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

		protected abstract void AddImageInternal(string imageFilePath);

		private void RemoveImageFile<TImageSource>(string filePath) where TImageSource : BaseImageSource
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
