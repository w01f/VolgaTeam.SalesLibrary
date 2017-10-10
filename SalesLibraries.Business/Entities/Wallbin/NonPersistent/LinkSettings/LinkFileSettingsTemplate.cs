using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LinkFileSettingsTemplate
	{
		public LinkSettingsType SettingsType { get; set; }
		public LinkType LinkType { get; set; }

		public static LinkFileSettingsTemplate Create(LinkSettingsType settingsType, LinkType linkType)
		{
			return new LinkFileSettingsTemplate { SettingsType = settingsType, LinkType = linkType };
		}
	}
}
