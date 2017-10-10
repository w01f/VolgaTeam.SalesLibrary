using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkGroupSettings
{
	public class LinkGroupSettingsTemplate
	{
		public LinkSettingsGroupType SettingsType { get; set; }
		public LinkType? LinkType { get; set; }
		public string TemplateEncoded { get; set; }

		public void GetSettings()
		{
		}
	}
}
