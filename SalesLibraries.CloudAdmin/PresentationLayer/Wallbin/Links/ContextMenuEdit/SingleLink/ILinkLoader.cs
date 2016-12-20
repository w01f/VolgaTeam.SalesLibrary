using System;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	interface ILinkLoader
	{
		event EventHandler<EventArgs> OnSettingsChanged;
		void LoadLink();
		void ApplyChanges();
	}
}
