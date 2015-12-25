using System;
using System.Collections.Generic;
using System.IO;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Dictionaries
{
	public class BannerList : IImageSourceList
	{
		public StorageDirectory MainFolder { get; set; }
		public StorageDirectory AdditionalFolder { get; set; }
		public StorageDirectory FavsFolder { get; set; }
		public List<LinkImageGroup> Items { get; private set; }

		public BannerList()
		{
			Items = new List<LinkImageGroup>();
		}

		public void Load()
		{
			MainFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Banners"));
			AdditionalFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Banners_2"));
			FavsFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSettingsFolder.RelativePathParts.Merge("Favorite_Banners"));

			Items.Clear();
			var imageGroup = new LinkImageGroup(this);
			imageGroup.Name = "Gallery";
			imageGroup.Order = -2;
			if (MainFolder.ExistsLocal())
				imageGroup.LoadImages<Banner>(MainFolder.LocalPath);
			Items.Add(imageGroup);
			imageGroup = new FavoriteImageGroup(this);
			imageGroup.Name = "My Favorites";
			imageGroup.Order = -1;
			if (FavsFolder.ExistsLocal())
				imageGroup.LoadImages<Banner>(FavsFolder.LocalPath);
			Items.Add(imageGroup);
			if (AdditionalFolder.ExistsLocal())
			{
				var contentDescriptionPath = Path.Combine(AdditionalFolder.LocalPath, "order.txt");
				if (File.Exists(contentDescriptionPath))
				{
					var groupNames = File.ReadAllLines(contentDescriptionPath);
					var groupIndex = 0;
					foreach (var groupName in groupNames)
					{
						if (String.IsNullOrEmpty(groupName)) continue;
						var groupFolderPath = Path.Combine(AdditionalFolder.LocalPath, groupName);
						if (!Directory.Exists(groupFolderPath)) continue;
						imageGroup = new LinkImageGroup(this);
						imageGroup.Name = groupName;
						imageGroup.Order = groupIndex;
						imageGroup.LoadImages<Banner>(groupFolderPath);
						Items.Add(imageGroup);
						groupIndex++;
					}
				}
			}
			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}
}
