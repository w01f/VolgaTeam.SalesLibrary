using System;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	interface ILinksLoader
	{
		event EventHandler<EventArgs> OnSettingsChanged;
		void LoadLinks();
		void ApplyChanges();
	}
}
