using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class LinkImageGroup
	{
		protected const string IgnoredListFileName = "ignored.txt";

		protected string _sourcePath;
		protected IImageSourceList ParentList { get; private set; }
		public string Name { get; set; }
		public int Order { get; set; }

		public List<LinkImageSource> Images { get; private set; }

		public EventHandler<EventArgs> OnDataChanged;

		public LinkImageGroup(IImageSourceList parentList)
		{
			ParentList = parentList;
			Images = new List<LinkImageSource>();
		}

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
			if (OnDataChanged != null)
				OnDataChanged(this, EventArgs.Empty);
		}
	}

	public class FavoriteImageGroup : LinkImageGroup
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
