using System;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	interface IGroupSettingsEditor
	{
		string Title { get; }
		bool NeedToApply { get; }
		void UpdateData();
		void ApplyData();
		void ResetData();
		event EventHandler<EventArgs> EditorChanged;
	}
}