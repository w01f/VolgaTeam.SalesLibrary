using System;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.GroupSettings
{
	interface IGroupSettingsEditor
	{
		string Title { get; }
		void UpdateData();
		void ResetData();
		event EventHandler<EventArgs> EditorChanged;
	}
}