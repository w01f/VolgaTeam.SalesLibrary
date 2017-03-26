using System;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditControl
	{
		LinkSettingsType[] SupportedSettingsTypes { get; }
		int Order { get; }
		bool AvailableForEmbedded { get; }
		SettingsEditorHeaderInfo HeaderInfo { get; }
		event EventHandler<EventArgs> ForceCloseRequested;
		void LoadData(BaseLibraryLink sourceLink);
		void SaveData();
	}
}
