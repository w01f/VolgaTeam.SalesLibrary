using System.Linq;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkGroupSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Helpers
{
	public static class LinkGroupSettingsProcessor
	{
		public static TSettings GetSettingsTemplate< TSettings>(
			this ILinkGroupSettingsContainer container,
			LinkSettingsGroupType settingsType,
			LinkType? linkType)
			where TSettings : BaseLinkSettings
		{
			var template = container.LinkSettingsTemplates.FirstOrDefault(t => t.SettingsType == settingsType && t.LinkType == linkType);
			if (template == null && container.ParentLinkSettingsContainer != null)
				template = container.ParentLinkSettingsContainer.LinkSettingsTemplates.FirstOrDefault(t => t.SettingsType == settingsType && t.LinkType == linkType);
			return template != null ?
				SettingsContainer.CreateInstance<TSettings>(null, template.TemplateEncoded) :
				default(TSettings);
		}

		public static void SaveSettingsTemplate<TSettings>(
			this ILinkGroupSettingsContainer container,
			LinkSettingsGroupType settingsType,
			LinkType? linkType,
			TSettings settings)
			where TSettings : BaseLinkSettings
		{
			if (settings == null)
				container.LinkSettingsTemplates.RemoveAll(t => t.SettingsType == settingsType && t.LinkType == linkType);
			else
			{
				var template =
					container.LinkSettingsTemplates.FirstOrDefault(t => t.SettingsType == settingsType && t.LinkType == linkType);
				if (template == null)
				{
					template = new LinkGroupSettingsTemplate
					{
						SettingsType = settingsType,
						LinkType = linkType
					};
					container.LinkSettingsTemplates.Add(template);
				}
				template.TemplateEncoded = settings.Serialize();
			}
			container.OnSettingsChanged();
		}
	}
}
