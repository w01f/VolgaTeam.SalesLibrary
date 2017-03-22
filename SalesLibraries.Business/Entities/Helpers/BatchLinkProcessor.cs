using System;
using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.Business.Entities.Helpers
{
	public static class BatchLinkProcessor
	{
		public static void ApplyCategories(this IEnumerable<BaseLibraryLink> links, SearchGroup[] sharedSearchGroups, SearchGroup[] partialSearchGroups = null)
		{
			foreach (var libraryLink in links)
			{
				if (partialSearchGroups == null)
				{
					libraryLink.Tags.Categories.Clear();
					libraryLink.Tags.Categories.AddRange(sharedSearchGroups.Select(searchGroup => searchGroup.Clone()));
				}
				else
				{
					libraryLink.Tags.Categories.RemoveAll(groupForRemove =>
						!sharedSearchGroups.Any(g => g.Equals(groupForRemove)) &&
						!partialSearchGroups.Any(g => g.Equals(groupForRemove)));

					foreach (var searchGroup in libraryLink.Tags.Categories)
					{
						var partialGroup = partialSearchGroups.FirstOrDefault(g => g.Equals(searchGroup));
						var sharedGroup = sharedSearchGroups.FirstOrDefault(g => g.Equals(searchGroup));

						searchGroup.Tags.RemoveAll(tagForRemove =>
							(sharedGroup == null || !sharedGroup.Tags.Any(t => t.Equals(tagForRemove))) &&
							(partialGroup == null || !partialGroup.Tags.Any(t => t.Equals(tagForRemove))));

						if (sharedGroup != null)
							searchGroup.Tags.AddRange(sharedGroup.Tags.Where(tagToAdd => !searchGroup.Tags.Any(t => t.Equals(tagToAdd))));
					}

					libraryLink.Tags.Categories.AddRange(sharedSearchGroups.Where(groupForAdd => !libraryLink.Tags.Categories.Any(g => g.Equals(groupForAdd))));
				}

				libraryLink.MarkAsModified();
			}
		}

		public static void ApplyKeywords(this IEnumerable<BaseLibraryLink> links, SearchTag[] keywords)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Tags.Keywords.Clear();
				libraryLink.Tags.Keywords.AddRange(keywords.Select(tag => new SearchTag { Name = tag.Name }));
				libraryLink.MarkAsModified();
			}
		}

		public static void ApplyKeywords(this IEnumerable<BaseLibraryLink> links, KeywordModel[] keywords)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Tags.Keywords.RemoveAll(tagForRemove =>
					!keywords.Any(k => String.Equals(k.Name, tagForRemove.Name, StringComparison.OrdinalIgnoreCase)));
				libraryLink.Tags.Keywords.AddRange(keywords
					.Where(tagForAdd => tagForAdd.IsShared && !libraryLink.Tags.Keywords.Any(k => String.Equals(k.Name, tagForAdd.Name, StringComparison.OrdinalIgnoreCase)))
					.Select(tag => new SearchTag { Name = tag.Name }));
				libraryLink.MarkAsModified();
			}
		}

		public static void ApplySuperFilters(this IEnumerable<BaseLibraryLink> links, string[] superFilters)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Tags.SuperFilters.Clear();
				libraryLink.Tags.SuperFilters.AddRange(superFilters);
				libraryLink.MarkAsModified();
			}
		}

		public static void ApplySecurity(this IEnumerable<BaseLibraryLink> links, SecuritySettings securitySettings = null)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Security = securitySettings != null ?
					securitySettings.Clone<SecuritySettings>(libraryLink) :
					SettingsContainer.CreateInstance<SecuritySettings>(libraryLink);
				libraryLink.MarkAsModified();
			}
		}

		public static void ResetSecurity(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplySecurity();
		}

		public static void ApplyExpirationSettings(this IEnumerable<BaseLibraryLink> links, LinkExpirationSettings expirationSettings = null)
		{
			foreach (var libraryLink in links.OfType<LibraryObjectLink>())
			{
				libraryLink.ExpirationSettings = expirationSettings != null ?
					expirationSettings.Clone<LinkExpirationSettings>(libraryLink) :
					SettingsContainer.CreateInstance<LinkExpirationSettings>(libraryLink);
				libraryLink.MarkAsModified();
			}
		}

		public static void ResetExpirationSettings(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyExpirationSettings();
		}

		public static void ApplyQuickLinkSettings(this IEnumerable<BaseLibraryLink> links, QuickLinkSettings quickLinkSettings = null)
		{
			foreach (var libraryLink in links.OfType<LibraryObjectLink>())
			{
				libraryLink.QuickLinkSettings = quickLinkSettings != null ?
					quickLinkSettings.Clone<QuickLinkSettings>(libraryLink) :
					SettingsContainer.CreateInstance<QuickLinkSettings>(libraryLink);
				libraryLink.MarkAsModified();
			}
		}

		public static void ResetQuickLinkSettings(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyQuickLinkSettings();
		}

		public static void ApplyWidgets(this IEnumerable<BaseLibraryLink> links, WidgetSettings widgetSettings = null)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Widget = widgetSettings != null ?
					widgetSettings.Clone<LinkWidgetSettings>(libraryLink) :
					SettingsContainer.CreateInstance<LinkWidgetSettings>(libraryLink);
				libraryLink.MarkAsModified();
			}
		}

		public static void ResetWidgets(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyWidgets();
		}

		public static void ApplyBanners(this IEnumerable<BaseLibraryLink> links, BannerSettings bannerSettings = null)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Banner = bannerSettings != null ?
					bannerSettings.Clone<BannerSettings>(libraryLink) :
					SettingsContainer.CreateInstance<BannerSettings>(libraryLink);
				libraryLink.MarkAsModified();
			}
		}

		public static void ApplyThumbnails(this IEnumerable<BaseLibraryLink> links, ThumbnailSettings thumbnailSettings = null)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Thumbnail = thumbnailSettings != null ?
					thumbnailSettings.Clone<ThumbnailSettings>(libraryLink) :
					SettingsContainer.CreateInstance<ThumbnailSettings>(libraryLink);
				libraryLink.MarkAsModified();
			}
		}

		public static void ResetBanners(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyBanners();
		}

		public static void ApplyNote(this IEnumerable<BaseLibraryLink> links, string note = null)
		{
			foreach (var libraryLink in links.OfType<LibraryObjectLink>())
				libraryLink.Settings.Note = note;
		}

		public static void ResetNote(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyNote();
		}

		public static void ApplyHoverNote(this IEnumerable<BaseLibraryLink> links, string hoverNote = null, bool showOnlyCustomHoverNote = false)
		{
			foreach (var libraryLink in links.OfType<LibraryObjectLink>())
			{
				((LibraryObjectLinkSettings)libraryLink.Settings).HoverNote = hoverNote;
				((LibraryObjectLinkSettings)libraryLink.Settings).ShowOnlyCustomHoverNote = showOnlyCustomHoverNote;
			}
			foreach (var lineBreak in links.OfType<LineBreak>())
				lineBreak.Settings.Note = hoverNote;
		}

		public static void ResetHoverNote(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyHoverNote();
		}

		public static void ResetToDefault(this IEnumerable<BaseLibraryLink> links, IList<LinkSettingsGroupType> groupsForReset = null)
		{
			foreach (var libraryLink in links)
				libraryLink.ResetToDefault(groupsForReset);
		}

		public static IEnumerable<LibraryObjectLink> GetExpiredLinks(this IEnumerable<BaseLibraryLink> links)
		{
			return links.OfType<LibraryObjectLink>().Where(link => link.ExpirationSettings.IsExpired);
		}

		public static IEnumerable<LibraryObjectLink> GetDisplayedLinks(this IEnumerable<BaseLibraryLink> links)
		{
			return links.OfType<LibraryObjectLink>().Where(link => !link.Security.IsForbidden);
		}

		public static IList<SearchGroup> GetCommonCategories(this IList<BaseLibraryLink> links)
		{
			var commonCategories = new List<SearchGroup>();
			var allCategories = links.SelectMany(l => l.Tags.Categories).ToList();
			foreach (var searchGroup in allCategories)
			{
				if (commonCategories.Any(c => c.Equals(searchGroup))) continue;
				if (!links.All(l => l.Tags.Categories.Any(c => c.Equals(searchGroup)))) continue;

				var commonCategory = new SearchGroup
				{
					Name = searchGroup.Name,
					SuperGroup = searchGroup.SuperGroup
				};
				foreach (var searchTag in allCategories
					.Where(c => c.Equals(commonCategory))
					.SelectMany(c => c.Tags)
					.ToList())
				{
					if (commonCategory.Tags.Any(t => t.Equals(searchTag))) continue;
					if (!links.All(l => l.Tags.Categories
						.Where(c => c.Equals(commonCategory))
						.SelectMany(c => c.Tags)
						.Any(t => t.Equals(searchTag)))) continue;
					commonCategory.Tags.Add(new SearchTag { Name = searchTag.Name });
				}
				if (commonCategory.Tags.Any())
					commonCategories.Add(commonCategory);
			}
			return commonCategories;
		}

		public static IList<SearchTag> GetCommonKeywords(this IList<BaseLibraryLink> links)
		{
			var commonKeywords = new List<SearchTag>();
			var allKeywords = links.SelectMany(l => l.Tags.Keywords).ToList();
			foreach (var searchTag in allKeywords)
			{
				if (commonKeywords.Any(t => t.Equals(searchTag))) continue;
				if (!links.All(l => l.Tags.Keywords.Any(t => t.Equals(searchTag)))) continue;
				commonKeywords.Add(new SearchTag { Name = searchTag.Name });
			}
			return commonKeywords;
		}

		public static string GetCommonTags(this IList<BaseLibraryLink> links)
		{
			var commonCategories = GetCommonCategories(links);
			var commonKeywords = GetCommonKeywords(links);
			return String.Join(", ",
				commonCategories
				.SelectMany(sg => sg.Tags.Select(t => t.Name))
				.Union(commonKeywords.Select(t => t.Name)));
		}

		public static void SetLinkTextWordWrap(this IEnumerable<BaseLibraryLink> links)
		{
			foreach (var libraryLink in links)
				libraryLink.Settings.TextWordWrap = true;
		}

		public static MultiLinkSet ToMultiLinkSet(this IEnumerable<BaseLibraryLink> links)
		{
			return new MultiLinkSet(links);
		}
	}
}
