using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditForm
	{
		LinkSettingsType[] EditableSettings { get; }
		void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl;
	}
}
