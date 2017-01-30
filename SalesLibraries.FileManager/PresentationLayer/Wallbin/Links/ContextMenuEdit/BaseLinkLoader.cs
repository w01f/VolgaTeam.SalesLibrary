using System;
using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	abstract class BaseLinkLoader<TLink> : ILinkLoader where TLink : BaseLibraryLink
	{
		protected readonly BaseContextMenuEditor _editor;
		protected bool _loading;

		public IList<TLink> TargetLinks { get; }

		public event EventHandler<EventArgs> OnSettingsChanged;

		protected BaseLinkLoader(BaseContextMenuEditor editor, IList<TLink> targetLinks)
		{
			_editor = editor;
			TargetLinks = targetLinks;
		}

		public abstract void SetMenuItemsViibility();

		protected void RaiseSettingsChanged()
		{
			OnSettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		public abstract void LoadLinks();

		public void ApplyChanges()
		{
			foreach (var link in TargetLinks)
				link.MarkAsModified();
		}
	}
}
