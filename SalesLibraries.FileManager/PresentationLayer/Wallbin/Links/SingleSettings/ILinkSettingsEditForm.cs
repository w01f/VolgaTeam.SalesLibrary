using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditForm
	{
		LinkSettingsType[] EditableSettings { get; }
		void InitForm(LinkSettingsType settingsType);
	}
}
