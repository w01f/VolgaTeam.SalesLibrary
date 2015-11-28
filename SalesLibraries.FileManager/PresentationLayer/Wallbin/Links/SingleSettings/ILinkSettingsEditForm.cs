using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditForm
	{
		LinkSettingsType[] EditableSettings { get; }
		bool IsForEmbedded { get; }
		void InitForm(LinkSettingsType settingsType);
	}
}
