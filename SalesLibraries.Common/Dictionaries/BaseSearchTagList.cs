using System.Collections.Generic;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.Common.Dictionaries
{
	public abstract class BaseSearchTagList
	{
		public int MaxTags { get; set; }
		public bool TagCount { get; set; }
		public List<SearchSuperGroup> SearchSuperGroups { get; set; }

		protected BaseSearchTagList()
		{
			SearchSuperGroups = new List<SearchSuperGroup>();
		}

		public abstract void Load();
	}
}
