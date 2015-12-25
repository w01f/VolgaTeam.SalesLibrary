using SalesLibraries.Common.Dictionaries;
using SalesLibraries.FileManager.Business.Dictionaries;

namespace SalesLibraries.FileManager.Configuration
{
	class ListManager
	{
		public WidgetList Widgets { get; private set; }
		public BannerList Banners { get; private set; }
		public SearchTagList SearchTags { get; set; }
		public SuperFilterList SuperFilters { get; private set; }
		public SecurityLists Security { get; private set; }

		public ListManager()
		{
			Widgets = new WidgetList();
			Banners = new BannerList();
			SearchTags = new SearchTagList();
			SuperFilters = new SuperFilterList();
			Security = new SecurityLists();
		}

		public void Load()
		{
			Widgets.Load();
			Banners.Load();
			SearchTags.Load();
			SuperFilters.Load();
			Security.Load();
		}
	}
}
