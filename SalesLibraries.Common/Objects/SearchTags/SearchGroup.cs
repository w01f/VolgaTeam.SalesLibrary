using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Common.Objects.SearchTags
{
	public class SearchGroup : IEquatable<SearchGroup>
	{
		public string Name { get; set; }

		public string Description { get; set; }

		[JsonIgnore]
		public bool Selected { get; set; }

		public List<SearchTag> Tags { get; private set; }

		public SearchGroup()
		{
			Tags = new List<SearchTag>();
		}

		public bool Equals(SearchGroup other)
		{
			return Tags.Compare(other.Tags);
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
