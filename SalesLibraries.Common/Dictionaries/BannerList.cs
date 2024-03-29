﻿using System;
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
		public StorageDirectory ImportedFolder { get; set; }
		public StorageDirectory ResizedFolder { get; set; }
		public List<ImageSourceGroup> Items { get; }

		public SearchResultsImageGroup SearchResults => Items.OfType<SearchResultsImageGroup>().Single();

		public ImportedBannerImageGroup ImportedImages => Items.OfType<ImportedBannerImageGroup>().Single();

		public ResizedImageGroup ResizedImages => Items.OfType<ResizedImageGroup>().Single();

		public BannerList()
		{
			Items = new List<ImageSourceGroup>();
		}

		public void Load()
		{
			MainFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Banners"));
			AdditionalFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Banners_2"));
			FavsFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSharedSettingsFolder.RelativePathParts.Merge("Favorite_Banners"));
			ImportedFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSharedSettingsFolder.RelativePathParts.Merge("Imported_Banners"));
			ResizedFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSharedSettingsFolder.RelativePathParts.Merge("Resized_Banners"));

			Items.Clear();

			SourceFolderImageGroup sourceFolderImageGroup = new RegularImageGroup(this, MainFolder.LocalPath);
			sourceFolderImageGroup.Name = "Gallery";
			sourceFolderImageGroup.Order = -5;
			Items.Add(sourceFolderImageGroup);

			var searchResultsimageGroup = new SearchResultsImageGroup(this);
			searchResultsimageGroup.Name = "Search Results";
			searchResultsimageGroup.Order = -4;
			Items.Add(searchResultsimageGroup);

			sourceFolderImageGroup = new FavoriteImageGroup(this, FavsFolder.LocalPath);
			sourceFolderImageGroup.Name = "My Favorites";
			sourceFolderImageGroup.Order = -3;
			Items.Add(sourceFolderImageGroup);

			sourceFolderImageGroup = new ImportedBannerImageGroup(this, ImportedFolder.LocalPath);
			sourceFolderImageGroup.Name = "Imported";
			sourceFolderImageGroup.Order = -2;
			Items.Add(sourceFolderImageGroup);

			sourceFolderImageGroup = new ResizedImageGroup(this, ResizedFolder.LocalPath);
			sourceFolderImageGroup.Name = "Re-Sized";
			sourceFolderImageGroup.Order = -1;
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
						sourceFolderImageGroup = new RegularImageGroup(this, groupFolderPath);
						sourceFolderImageGroup.Name = groupName;
						sourceFolderImageGroup.Order = groupIndex;
						Items.Add(sourceFolderImageGroup);
						groupIndex++;
					}
				}
			}
			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}
}
