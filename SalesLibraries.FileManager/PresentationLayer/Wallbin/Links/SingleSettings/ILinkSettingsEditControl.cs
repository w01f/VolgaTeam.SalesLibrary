using System;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditControl
	{
		LinkSettingsType SettingsType { get; }
		int Order { get; }
		bool AvailableForEmbedded { get; }
		SettingsEditorHeaderInfo HeaderInfo { get; }
		event EventHandler<EventArgs> ForceCloseRequested;
		void LoadData();
		void SaveData();
	}
}
