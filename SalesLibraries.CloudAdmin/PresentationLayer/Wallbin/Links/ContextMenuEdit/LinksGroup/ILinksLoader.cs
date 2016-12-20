using System;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	interface ILinksLoader
	{
		event EventHandler<EventArgs> OnSettingsChanged;
		void LoadLinks();
		void ApplyChanges();
	}
}
