using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LinkFileSettingsTemplate
	{
		public LinkSettingsType SettingsType { get; set; }
		public FileTypes FileType { get; set; }

		public static LinkFileSettingsTemplate Create(LinkSettingsType settingsType, FileTypes fileType)
		{
			return new LinkFileSettingsTemplate { SettingsType = settingsType, FileType = fileType };
		}
	}
}
