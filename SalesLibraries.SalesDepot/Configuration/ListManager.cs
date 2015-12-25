using SalesLibraries.SalesDepot.Business.Dictionaries;

namespace SalesLibraries.SalesDepot.Configuration
{
	class ListManager
	{
		public SearchTagList SearchTags { get; set; }

		public ListManager()
		{
			SearchTags = new SearchTagList();
		}

		public void Load()
		{
			SearchTags.Load();
		}
	}
}
