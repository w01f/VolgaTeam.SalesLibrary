using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.Business.Entities.Helpers
{
	public static class BatchLinkProcessor
	{
		public static void ApplyCategories(this IEnumerable<BaseLibraryLink> links, SearchGroup[] searchGroups)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Tags.Categories.Clear();
				libraryLink.Tags.Categories.AddRange(searchGroups.Select(searchGroup => searchGroup.Clone()));
			}
		}

		public static void ApplyKeywords(this IEnumerable<BaseLibraryLink> links, SearchTag[] keywords)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Tags.Keywords.Clear();
				libraryLink.Tags.Keywords.AddRange(keywords.Select(tag => new SearchTag { Name = tag.Name }));
			}
		}

		public static void ApplySuperFilters(this IEnumerable<BaseLibraryLink> links, string[] superFilters)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Tags.SuperFilters.Clear();
				libraryLink.Tags.SuperFilters.AddRange(superFilters);
			}
		}

		public static void ApplySecurity(this IEnumerable<BaseLibraryLink> links, SecuritySettings securitySettings)
		{
			foreach (var libraryLink in links)
			{
				libraryLink.Security.NoShare = securitySettings.NoShare;
				libraryLink.Security.IsForbidden = securitySettings.IsForbidden;
				libraryLink.Security.IsRestricted = securitySettings.IsRestricted;
				libraryLink.Security.AssignedUsers = securitySettings.AssignedUsers;
				libraryLink.Security.DeniedUsers = securitySettings.DeniedUsers;
			}
		}

		public static void ApplyWidgets(this IEnumerable<BaseLibraryLink> links, WidgetSettings widgetSettings)
		{
			foreach (var libraryLink in links)
				libraryLink.Widget = widgetSettings.Clone<LinkWidgetSettings>(libraryLink);
		}

		public static void ApplyBanners(this IEnumerable<BaseLibraryLink> links, BannerSettings bannerSettings)
		{
			foreach (var libraryLink in links)
				libraryLink.Banner = bannerSettings.Clone<BannerSettings>(libraryLink);
		}

		public static IEnumerable<LibraryFileLink> GetDeadLinks(this IEnumerable<BaseLibraryLink> links)
		{
			foreach (var libraryLink in links.OfType<LibraryFileLink>())
			{
				var isDead = libraryLink.CheckIfDead();
				if (isDead || libraryLink.IsDead)
					libraryLink.IsDead = isDead;
				if (isDead)
					yield return libraryLink;
			}
		}
		public static IEnumerable<LibraryObjectLink> GetExpiredLinks(this IEnumerable<BaseLibraryLink> links)
		{
			return links.OfType<LibraryObjectLink>().Where(link => link.ExpirationSettings.IsExpired);
		}

		public static IEnumerable<LibraryObjectLink> GetDisplayedLinks(this IEnumerable<BaseLibraryLink> links)
		{
			return links.OfType<LibraryObjectLink>().Where(link => !link.Security.IsForbidden);
		}
	}
}
