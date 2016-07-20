using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class TagSettings : SettingsContainer
	{
		public List<SearchGroup> Categories { get; private set; }
		public List<SearchTag> Keywords { get; private set; }
		public List<string> SuperFilters { get; private set; }

		[JsonIgnore]
		public bool HasTags => HasCategories || HasKeywords || HasSuperFilters;

		[JsonIgnore]
		public bool HasCategories => Categories.Any();

		[JsonIgnore]
		public bool HasSuperFilters => SuperFilters.Any();

		[JsonIgnore]
		public bool HasKeywords => Keywords.Any();

		[JsonIgnore]
		public string AllCategories
		{
			get { return string.Join(", ", Categories.SelectMany(sg => sg.Tags.Select(t => t.Name))); }
		}

		[JsonIgnore]
		public string AllKeywords
		{
			get { return string.Join(", ", Keywords.Select(t => t.Name)); }
		}

		public TagSettings()
		{
			Categories = new List<SearchGroup>();
			Keywords = new List<SearchTag>();
			SuperFilters = new List<string>();
		}
	}
}
