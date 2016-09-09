using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
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
			get { return String.Join(", ", Categories.SelectMany(sg => sg.Tags.Select(t => t.Name))); }
		}

		[JsonIgnore]
		public string AllKeywords
		{
			get { return String.Join(", ", Keywords.Select(t => t.Name)); }
		}

		[JsonIgnore]
		public string AllTags
		{
			get { return String.Join(", ", Categories.SelectMany(sg => sg.Tags.Select(t => t.Name)).Union(Keywords.Select(t => t.Name))); }
		}

		public TagSettings()
		{
			Categories = new List<SearchGroup>();
			Keywords = new List<SearchTag>();
			SuperFilters = new List<string>();
		}

		public void ResetToDefault(IList<LinkSettingsGroupType> groupsForReset)
		{
			foreach (var linkSettingsGroupType in groupsForReset)
			{
				switch (linkSettingsGroupType)
				{
					case LinkSettingsGroupType.SearchTags:
						Categories.Clear();
						SuperFilters.Clear();
						break;
					case LinkSettingsGroupType.Keywords:
						Keywords.Clear();
						break;
				}
			}
		}

		public IList<LinkSettingsGroupType> GetCustomizedSettigsGroups()
		{
			var customizedSettingsGroups = new List<LinkSettingsGroupType>();

			if (Categories.Any() || SuperFilters.Any())
				customizedSettingsGroups.Add(LinkSettingsGroupType.SearchTags);
			if (Keywords.Any())
				customizedSettingsGroups.Add(LinkSettingsGroupType.Keywords);

			return customizedSettingsGroups;
		}
	}
}
