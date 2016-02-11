using System;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	interface IGroupSettingsEditor
	{
		string Title { get; }
		void UpdateData();
		void ResetData();
		event EventHandler<EventArgs> EditorChanged;
	}
}