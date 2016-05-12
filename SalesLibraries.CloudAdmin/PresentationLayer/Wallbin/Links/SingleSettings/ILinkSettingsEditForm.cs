using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditForm
	{
		LinkSettingsType[] EditableSettings { get; }
		void InitForm(LinkSettingsType settingsType);
	}
}
