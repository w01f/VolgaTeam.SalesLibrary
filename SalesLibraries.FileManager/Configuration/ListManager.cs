using SalesLibraries.Common.Dictionaries;
using SalesLibraries.FileManager.Business.Dictionaries;

namespace SalesLibraries.FileManager.Configuration
{
	class ListManager
	{
		public WidgetList Widgets { get; }
		public BannerList Banners { get; }
		public SearchTagList SearchTags { get; set; }
		public SuperFilterList SuperFilters { get; }
		public SecurityLists Security { get; }
		public LinkBundleImageList LinkBundleImages { get; }
		public ExternalLibraryLinksList ExternalLibraryLinks { get; }
		public ExternalShortcutsList ExternalShortcuts { get; }
		public InternalLinkTemplateList InternalLinkTemplates { get; }

		public ListManager()
		{
			Widgets = new WidgetList();
			Banners = new BannerList();
			SearchTags = new SearchTagList();
			SuperFilters = new SuperFilterList();
			Security = new SecurityLists();
			LinkBundleImages = new LinkBundleImageList();
			ExternalLibraryLinks = new ExternalLibraryLinksList();
			ExternalShortcuts = new ExternalShortcutsList();
			InternalLinkTemplates = new InternalLinkTemplateList();
		}

		public void Load()
		{
			Widgets.Load();
			Banners.Load();
			SearchTags.Load();
			SuperFilters.Load();
			LinkBundleImages.Load();
			InternalLinkTemplates.Load();
		}
	}
}
