using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LibraryObjectNotesEditor : BaseContextMenuEditor
	{
		public BarSubItem ItemLinkNote { get; private set; }
		public BarEditItem ItemLinkNoteCustom { get; private set; }
		public BarEditItem ItemHoverNote { get; private set; }

		private LibraryObjectNotesLoader LibraryObjectLoader => (LibraryObjectNotesLoader)_linkLoader;

		public LibraryObjectNotesEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		protected override ILinkLoader CreateLoader(IList<BaseLibraryLink> targetLinks)
		{
			return new LibraryObjectNotesLoader(this, targetLinks.OfType<LibraryObjectLink>().ToList());
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			ItemsContainer.LinksPersistInfo.Clear();
			ItemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(ItemLinkNote),
				new LinkPersistInfo(ItemHoverNote),
			});
			_barManager.EndInit();
		}

		private void InitContextMenu()
		{
			_barManager.BeginInit();

			var buttonEditor = new RepositoryItemButtonEdit { AutoHeight = false };
			buttonEditor.Buttons.Clear();
			buttonEditor.Buttons.AddRange(new[]
			{
				new EditorButton(ButtonPredefines.Delete)
			});
			buttonEditor.ButtonClick += (o, e) =>
			{
				((ButtonEdit)o).EditValue = null;
			};

			var colorEditor = new RepositoryItemHtmlColorEdit
			{
				AutoHeight = false,
			};
			colorEditor.Buttons.Clear();
			colorEditor.Buttons.AddRange(new[] { new EditorButton(ButtonPredefines.Ellipsis) });
			colorEditor.OnColorSelected += (o, e) =>
			{
				_barManager.CloseMenus();
			};

			var maxId = _barManager.MaxItemId++;

			var linkNoteControlButtonCollection = new List<BarItem>();
			ItemLinkNoteCustom = new BarEditItem
			{
				Id = maxId,
				Caption = "Custom",
				Edit = buttonEditor,
				Width = 150
			};
			ItemLinkNoteCustom.EditValueChanged += (o, e) => LibraryObjectLoader.OnNoteChanged();
			maxId++;
			foreach (var noteText in LibraryObjectNotesLoader.PredefinedNotes)
			{
				var itemLinkNote = new BarButtonItem
				{
					Caption = noteText,
					Id = maxId,
					Tag = noteText
				};
				maxId++;
				itemLinkNote.ItemClick += (o, e) =>
				{
					var itemNoteText = e.Item.Tag as String;
					ItemLinkNoteCustom.EditValue = itemNoteText;
				};
				linkNoteControlButtonCollection.Add(itemLinkNote);
			}
			linkNoteControlButtonCollection.Add(ItemLinkNoteCustom);
			ItemLinkNote = new BarSubItem();
			ItemLinkNote.Caption = "Link Note";
			ItemLinkNote.Id = maxId;
			_barManager.Items.AddRange(linkNoteControlButtonCollection.ToArray());
			ItemLinkNote.LinksPersistInfo.AddRange(linkNoteControlButtonCollection
				.Select(barItem => new LinkPersistInfo(barItem)).ToArray());
			maxId++;

			ItemHoverNote = new BarEditItem
			{
				Id = maxId,
				Caption = "Hover Note",
				Edit = buttonEditor,
				Width = 150
			};
			maxId++;
			ItemHoverNote.EditValueChanged += (o, e) => LibraryObjectLoader.OnHoverChanged();

			_barManager.Items.Add(ItemLinkNote);
			_barManager.Items.Add(ItemHoverNote);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
