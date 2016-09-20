using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesLibraries.Common.Objects.SearchTags
{
	public class SearchGroup : IEquatable<SearchGroup>
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public List<SearchTag> Tags { get; private set; }

		public SearchGroup()
		{
			Tags = new List<SearchTag>();
		}

		public bool Equals(SearchGroup other)
		{
			return String.Equals(Name, other.Name, StringComparison.Ordinal);
		}

		public SearchGroup Clone()
		{
			var result = new SearchGroup();
			result.Name = Name;
			result.Description = Description;
			result.Tags.AddRange(Tags.Select(searchTag => new SearchTag() { Name = searchTag.Name }));
			return result;
		}
	}
}
