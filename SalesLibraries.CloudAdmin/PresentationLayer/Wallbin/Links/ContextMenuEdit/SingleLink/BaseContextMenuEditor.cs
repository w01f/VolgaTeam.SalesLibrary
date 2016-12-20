using System;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	abstract class BaseContextMenuEditor
	{
		protected readonly BarManager _barManager;
		protected readonly BarSubItem _itemsContainer;
		protected ILinkLoader _linkLoader;
		protected bool _isEditValueChanged;

		public event EventHandler<EventArgs> EditValueChanged;

		protected BaseContextMenuEditor(BarSubItem itemsContainer)
		{
			_itemsContainer = itemsContainer;
			_barManager = _itemsContainer.Manager;
		}

		protected abstract ILinkLoader CreateLoader(BaseLibraryLink targetLink);

		protected abstract void PopulateContextMenu();

		public virtual void LoadLink(BaseLibraryLink targetLink)
		{
			PopulateContextMenu();
			_linkLoader = CreateLoader(targetLink);
			_linkLoader.OnSettingsChanged += (o, e) =>
			{
				_isEditValueChanged = true;
			};
			_linkLoader.LoadLink();
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
