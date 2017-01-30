using System;
using System.Collections.Generic;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	abstract class BaseContextMenuEditor
	{
		protected readonly BarManager _barManager;
		protected ILinkLoader _linkLoader;
		protected bool _isEditValueChanged;

		public BarSubItem ItemsContainer { get; }

		public event EventHandler<EventArgs> EditValueChanged;

		protected BaseContextMenuEditor(BarSubItem itemsContainer)
		{
			ItemsContainer = itemsContainer;
			_barManager = ItemsContainer.Manager;
		}

		protected abstract ILinkLoader CreateLoader(IList<BaseLibraryLink> targetLinks);

		protected abstract void PopulateContextMenu();

		public void LoadLinks(IList<BaseLibraryLink> targetLinks)
		{
			_linkLoader = CreateLoader(targetLinks);
			_linkLoader.OnSettingsChanged += (o, e) =>
			{
				_isEditValueChanged = true;
			};

			PopulateContextMenu();

			_linkLoader.SetMenuItemsViibility();
			_linkLoader.LoadLinks();
			_isEditValueChanged = false;
		}

		public void ApplyChanges()
		{
			if (_linkLoader == null) return;
			if (!_isEditValueChanged) return;
			_isEditValueChanged = false;
			_linkLoader.ApplyChanges();
			EditValueChanged?.Invoke(this, EventArgs.Empty);
			_linkLoader = null;
		}
	}
}
