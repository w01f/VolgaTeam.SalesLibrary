using System;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	interface ILinkLoader
	{
		event EventHandler<EventArgs> OnSettingsChanged;
		void LoadLink();
		void ApplyChanges();
	}
}
