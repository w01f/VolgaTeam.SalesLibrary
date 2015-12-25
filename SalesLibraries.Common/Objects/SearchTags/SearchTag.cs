using System;
using Newtonsoft.Json;

namespace SalesLibraries.Common.Objects.SearchTags
{
	public class SearchTag : IEquatable<SearchTag>
	{
		public string Name { get; set; }

		[JsonIgnore]
		public bool Selected { get; set; }

		public bool Equals(SearchTag other)
		{
			return String.Equals(Name, other.Name, StringComparison.Ordinal);
		}
	}
}
