using System.Collections.Generic;
using System.IO;
using System.Linq;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Dictionaries
{
	public class WidgetList : IImageSourceList
	{
		public StorageDirectory MainFolder { get; private set; }
		public StorageDirectory AdditionalFolder { get; private set; }
		public StorageDirectory FavsFolder { get; private set; }
		public StorageDirectory ImportedFolder { get; set; }
		public List<ImageSourceGroup> Items { get; }

		public SearchResultsImageGroup SearchResults => Items.OfType<SearchResultsImageGroup>().Single();

		public ImportedWidgetImageGroup ImportedImages => Items.OfType<ImportedWidgetImageGroup>().Single();

		public WidgetList()
		{
			Items = new List<ImageSourceGroup>();
		}

		public void Load()
		{
			MainFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Widgets"));
			AdditionalFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("Widgets_2"));
			FavsFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSharedSettingsFolder.RelativePathParts.Merge("Favorite_Widgets"));
			FavsFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSharedSettingsFolder.RelativePathParts.Merge("Imported_Widgets"));
			ImportedFolder = new StorageDirectory(RemoteResourceManager.Instance.AppSharedSettingsFolder.RelativePathParts.Merge("Imported_Widgets"));

			Items.Clear();

			SourceFolderImageGroup sourceFolderImageGroup = new RegularImageGroup(this, MainFolder.LocalPath);
			sourceFolderImageGroup.Name = "Gallery";
			sourceFolderImageGroup.Order = -4;
			Items.Add(sourceFolderImageGroup);

			var searchResultsimageGroup = new SearchResultsImageGroup(this);
			searchResultsimageGroup.Name = "Search Results";
			searchResultsimageGroup.Order = -3;
			Items.Add(searchResultsimageGroup);

			sourceFolderImageGroup = new FavoriteImageGroup(this, FavsFolder.LocalPath);
			sourceFolderImageGroup.Name = "My Favorites";
			sourceFolderImageGroup.Order = -2;
			Items.Add(sourceFolderImageGroup);

			sourceFolderImageGroup = new ImportedWidgetImageGroup(this, ImportedFolder.LocalPath);
			sourceFolderImageGroup.Name = "Imported";
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
