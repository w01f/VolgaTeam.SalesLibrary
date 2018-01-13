using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class ResizedImageGroup : RegularImageGroup
	{
		private const string IgnoredListFileName = "ignored.txt";

		public ResizedImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		public override void LoadImages<TImageSource>()
		{
			base.LoadImages<TImageSource>();

			foreach (var imageSource in Images)
			{
				imageSource.RemoveFromResized += (o, e) =>
				{
					RemoveImageFile<TImageSource>(imageSource.FilePath);
				};
			}
		}

		public void AddImage<TImageSource>(Image image, string name, int scaleFactor) where TImageSource : BaseImageSource
		{
			if (image == null) return;
			try
			{
				if (!Directory.Exists(_sourcePath))
					Directory.CreateDirectory(_sourcePath);

				var filePath = Path.Combine(_sourcePath, String.Format("{0}-{1}%.png", name, scaleFactor));
				var size = new Size(image.Width * scaleFactor / 100, image.Height * scaleFactor / 100);
				using (var resizedImage = image.Resize(size))
				{
					resizedImage.Save(filePath);
				}
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
