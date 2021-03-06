﻿using System;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	interface ILinkLoader
	{
		event EventHandler<EventArgs> OnSettingsChanged;
		void SetMenuItemsViibility();
		void LoadLinks();
		void ApplyChanges();
	}
}
