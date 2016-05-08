using SalesLibraries.CloudAdmin.Business.Dictionaries;
using SalesLibraries.Common.Dictionaries;

namespace SalesLibraries.CloudAdmin.Configuration
{
	class ListManager
	{
		public WidgetList Widgets { get; }
		public BannerList Banners { get; }
		public SearchTagList SearchTags { get; set; }
		public SuperFilterList SuperFilters { get; }
		public SecurityLists Security { get; }

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
