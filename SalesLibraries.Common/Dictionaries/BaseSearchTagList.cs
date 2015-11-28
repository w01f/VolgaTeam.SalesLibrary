using System.Collections.Generic;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.Common.Dictionaries
{
	public abstract class BaseSearchTagList
	{
		public int MaxTags { get; set; }
		public bool TagCount { get; set; }
		public List<SearchGroup> SearchGroups { get; set; }

		protected BaseSearchTagList()
		{
			SearchGroups = new List<SearchGroup>();
		}

		public abstract void Load();
	}
}
