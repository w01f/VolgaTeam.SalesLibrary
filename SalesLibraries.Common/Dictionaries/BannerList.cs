using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		public List<ImageSourceGroup> Items { get; }

		public SearchResultsImageGroup SearchResults => Items.OfType<SearchResultsImageGroup>().Single();

		public BannerList()
		{
			Items = new List<ImageSourceGroup>();
		}

		public void Load()
		{
			MainFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Banners"));
			AdditionalFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Banners_2"));
			FavsFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSharedSettingsFolder.RelativePathParts.Merge("Favorite_Banners"));

			Items.Clear();

			SourceFolderImageGroup sourceFolderImageGroup = new RegularImageGroup(this);
			sourceFolderImageGroup.Name = "Gallery";
			sourceFolderImageGroup.Order = -3;
			if (MainFolder.ExistsLocal())
				sourceFolderImageGroup.LoadImages<Banner>(MainFolder.LocalPath);
			Items.Add(sourceFolderImageGroup);

			var searchResultsimageGroup = new SearchResultsImageGroup(this);
			searchResultsimageGroup.Name = "Search Results";
			searchResultsimageGroup.Order = -2;
			Items.Add(searchResultsimageGroup);

			sourceFolderImageGroup = new FavoriteImageGroup(this);
			sourceFolderImageGroup.Name = "My Favorites";
			sourceFolderImageGroup.Order = -1;
			if (FavsFolder.ExistsLocal())
				sourceFolderImageGroup.LoadImages<Banner>(FavsFolder.LocalPath);
			Items.Add(sourceFolderImageGroup);

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
						sourceFolderImageGroup = new RegularImageGroup(this);
						sourceFolderImageGroup.Name = groupName;
						sourceFolderImageGroup.Order = groupIndex;
						sourceFolderImageGroup.LoadImages<Banner>(groupFolderPath);
						Items.Add(sourceFolderImageGroup);
						groupIndex++;
					}
				}
			}
			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}
}
