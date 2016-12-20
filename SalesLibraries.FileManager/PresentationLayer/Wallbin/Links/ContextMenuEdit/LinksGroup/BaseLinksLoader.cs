using System;
using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	abstract class BaseLinksLoader<TLink> : ILinksLoader where TLink : BaseLibraryLink
	{
		protected readonly BaseContextMenuEditor _editor;
		protected bool _loading;

		public List<TLink> TargetLinks { get; }

		public event EventHandler<EventArgs> OnSettingsChanged;

		protected BaseLinksLoader(BaseContextMenuEditor editor, IEnumerable<TLink> targetLinks)
		{
			_editor = editor;

			TargetLinks = new List<TLink>();
			TargetLinks.AddRange(targetLinks);
		}

		protected abstract void SetMenuItemsViibility();

		protected void RaiseSettingsChanged()
		{
			OnSettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		public virtual void LoadLinks()
		{
			SetMenuItemsViibility();
		}

		public void ApplyChanges()
		{
			TargetLinks.ForEach(link => link.MarkAsModified());
		}
	}
}
