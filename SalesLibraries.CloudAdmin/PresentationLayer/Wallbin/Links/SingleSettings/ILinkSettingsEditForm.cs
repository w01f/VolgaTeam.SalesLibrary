namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditForm
	{
		LinkSettingsType[] EditableSettings { get; }
		bool IsForEmbedded { get; }
		void InitForm(LinkSettingsType settingsType);
	}
}
