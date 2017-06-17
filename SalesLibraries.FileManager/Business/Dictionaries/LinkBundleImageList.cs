using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.FileManager.Business.Dictionaries
{
	class LinkBundleImageList : IImageSourceList
	{
		public StorageDirectory MainFolder { get; private set; }
		public StorageDirectory AdditionalFolder { get; private set; }
		public StorageDirectory FavsFolder { get; private set; }
		public List<ImageSourceGroup> Items { get; }
		public SearchResultsImageGroup SearchResults => Items.OfType<SearchResultsImageGroup>().Single();

		public StorageFile DefaultPowerPointLogo { get; private set; }
		public StorageFile DefaultWordLogo { get; private set; }
		public StorageFile DefaultExcelLogo { get; private set; }
		public StorageFile DefaultPdfLogo { get; private set; }
		public StorageFile DefaultVideoLogo { get; private set; }
		public StorageFile DefaultImageLogo { get; private set; }

		public StorageFile DefaultCoverLogo { get; private set; }
		public StorageFile DefaultInfoLogo { get; private set; }
		public StorageFile DefaultRevenueLogo { get; private set; }
		public StorageFile DefaultStrategyLogo { get; private set; }
		public StorageFile DefaultUrlLogo { get; private set; }

		public LinkBundleImageList()
		{
			Items = new List<ImageSourceGroup>();
		}

		public void Load()
		{
			MainFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("link_bundle_icons"));
			AdditionalFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("link_bundle_icons_2"));
			FavsFolder = new StorageDirectory(RemoteResourceManager.Instance.AppAliasSettingsFolder.RelativePathParts.Merge("Favorite Link Bundle Icons"));

			DefaultPowerPointLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_pptx.png"));
			DefaultWordLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_docx.png"));
			DefaultExcelLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_xlsx.png"));
			DefaultPdfLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_pdf.png"));
			DefaultVideoLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_mp4.png"));
			DefaultImageLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_image.png"));

			DefaultCoverLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_cover.png"));
			DefaultInfoLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_info.png"));
			DefaultRevenueLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_revenue.png"));
			DefaultStrategyLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_sales_strategy.png"));
			DefaultUrlLogo = new StorageFile(MainFolder.RelativePathParts.Merge("default_url.png"));

			Items.Clear();

			SourceFolderImageGroup sourceFolderImageGroup = new RegularImageGroup(this);
			sourceFolderImageGroup.Name = "Gallery";
			sourceFolderImageGroup.Order = -2;
			if (MainFolder.ExistsLocal())
				sourceFolderImageGroup.LoadImages<Widget>(MainFolder.LocalPath);
			Items.Add(sourceFolderImageGroup);

			var searchResultsimageGroup = new SearchResultsImageGroup(this);
			searchResultsimageGroup.Name = "Search Results";
			searchResultsimageGroup.Order = -2;
			Items.Add(searchResultsimageGroup);

			sourceFolderImageGroup = new FavoriteImageGroup(this);
			sourceFolderImageGroup.Name = "My Favorites";
			sourceFolderImageGroup.Order = -1;
			if (FavsFolder.ExistsLocal())
				sourceFolderImageGroup.LoadImages<Widget>(FavsFolder.LocalPath);
			Items.Add(sourceFolderImageGroup);

			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}
}
