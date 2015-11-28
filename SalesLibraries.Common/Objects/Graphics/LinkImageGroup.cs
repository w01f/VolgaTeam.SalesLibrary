using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class LinkImageGroup
	{
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
			Images.Clear();
			foreach (var filePath in Directory.GetFiles(sourcePath, "*.png"))
			{
				var linkImageSource = Activator.CreateInstance(typeof(T), filePath) as LinkImageSource;
				linkImageSource.AddToFavs += (o, e) =>
				{
					var favoritesImagesGroup = ParentList.Items.OfType<FavoriteImageGroup>().FirstOrDefault();
					if (favoritesImagesGroup == null) return;
					favoritesImagesGroup.AddImage<T>(linkImageSource.FilePath);
				};
				if (linkImageSource.Index > 0 && linkImageSource.Image != null)
					Images.Add(linkImageSource);
			}
			Images.Sort((x, y) => x.Index.CompareTo(y.Index));
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
	}

}
