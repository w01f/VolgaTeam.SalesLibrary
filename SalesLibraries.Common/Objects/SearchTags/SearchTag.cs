using System;

namespace SalesLibraries.Common.Objects.SearchTags
{
	public class SearchTag : IEquatable<SearchTag>
	{
		public string Name { get; set; }

		public bool Equals(SearchTag other)
		{
			return String.Equals(Name, other.Name, StringComparison.Ordinal);
		}
	}
}
