using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesLibraries.Common.Objects.SearchTags
{
	public class SearchSuperGroup : IEquatable<SearchSuperGroup>
	{
		public string Name { get; set; }

		public List<SearchGroup> Groups { get; private set; }

		public SearchSuperGroup()
		{
			Groups = new List<SearchGroup>();
		}

		public bool Equals(SearchSuperGroup other)
		{
			return String.Equals(Name, other.Name, StringComparison.Ordinal);
		}

		public SearchSuperGroup Clone()
		{
			var result = new SearchSuperGroup();
			result.Name = Name;
			result.Groups.AddRange(Groups.Select(searchGroup => searchGroup.Clone()));
			return result;
		}
	}
}
