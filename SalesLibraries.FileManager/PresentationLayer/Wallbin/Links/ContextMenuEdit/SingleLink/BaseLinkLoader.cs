using System;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	abstract class BaseLinkLoader<TLink> : ILinkLoader where TLink : BaseLibraryLink
	{
		protected readonly BaseContextMenuEditor _editor;
		protected bool _loading;

		public TLink TargetLink { get; }

		public event EventHandler<EventArgs> OnSettingsChanged;

		protected BaseLinkLoader(BaseContextMenuEditor editor, TLink targetLink)
		{
			_editor = editor;
			TargetLink = targetLink;
		}

		protected abstract void SetMenuItemsViibility();

		protected void RaiseSettingsChanged()
		{
			OnSettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		public virtual void LoadLink()
		{
			SetMenuItemsViibility();
		}

		public void ApplyChanges()
		{
			TargetLink.MarkAsModified();
		}
	}
}
