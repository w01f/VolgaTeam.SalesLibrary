using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Common;
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

		public static void ApplyExpirationSettings(this IEnumerable<BaseLibraryLink> links, LinkExpirationSettings securitySettings = null)
		{
			foreach (var libraryLink in links.OfType<LibraryObjectLink>())
			{
				libraryLink.ExpirationSettings = securitySettings != null ?
					securitySettings.Clone<LinkExpirationSettings>(libraryLink) :
					SettingsContainer.CreateInstance<LinkExpirationSettings>(libraryLink);
				libraryLink.MarkAsModified();
			}
		}

		public static void ResetExpirationSettings(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyExpirationSettings();
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

		public static void ApplyHoverNote(this IEnumerable<BaseLibraryLink> links, string hoverNote = null)
		{
			foreach (var libraryLink in links.OfType<LibraryObjectLink>())
				((LibraryObjectLinkSettings)libraryLink.Settings).HoverNote = hoverNote;
			foreach (var lineBreak in links.OfType<LineBreak>())
				lineBreak.Settings.Note = hoverNote;
		}

		public static void ResetHoverNote(this IEnumerable<BaseLibraryLink> links)
		{
			links.ApplyHoverNote();
		}

		public static void ResetToDefault(this IEnumerable<BaseLibraryLink> links)
		{
			foreach (var libraryLink in links)
				libraryLink.ResetToDefault();
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
