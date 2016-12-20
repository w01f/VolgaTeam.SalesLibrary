using System;
using System.Collections.Generic;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	abstract class BaseContextMenuEditor
	{
		protected readonly BarManager _barManager;
		protected ILinksLoader _linksLoader;
		protected bool _isEditValueChanged;

		public BarSubItem ItemsContainer { get; }
		public event EventHandler<EventArgs> EditValueChanged;

		protected BaseContextMenuEditor(BarSubItem itemsContainer)
		{
			ItemsContainer = itemsContainer;
			_barManager = ItemsContainer.Manager;
		}

		protected abstract ILinksLoader CreateLoader(IEnumerable<BaseLibraryLink> targetLinks);

		protected abstract void PopulateContextMenu();

		public virtual void LoadLinks(IEnumerable<BaseLibraryLink> targetLinks)
		{
			PopulateContextMenu();
			_linksLoader = CreateLoader(targetLinks);
			_linksLoader.OnSettingsChanged += (o, e) =>
			{
				_isEditValueChanged = true;
			};
			_linksLoader.LoadLinks();
			_isEditValueChanged = false;
		}

		public void ApplyChanges()
		{
			if (_linksLoader == null) return;
			if (!_isEditValueChanged) return;
			_isEditValueChanged = false;
			_linksLoader.ApplyChanges();
			EditValueChanged?.Invoke(this, EventArgs.Empty);
			_linksLoader = null;
		}
	}
}
