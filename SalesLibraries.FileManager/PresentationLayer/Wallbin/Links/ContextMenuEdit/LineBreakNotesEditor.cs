using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LineBreakNotesEditor : BaseContextMenuEditor
	{
		public BarEditItem ItemHoverNote { get; private set; }

		private LineBreakNotesLoader LineBreakLoader => (LineBreakNotesLoader) _linkLoader;

		public LineBreakNotesEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		protected override ILinkLoader CreateLoader(IList<BaseLibraryLink> targetLinks)
		{
			return new LineBreakNotesLoader(this, targetLinks.OfType<LineBreak>().ToList());
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			ItemsContainer.LinksPersistInfo.Clear();
			ItemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(ItemHoverNote),
			});
			_barManager.EndInit();
		}

		private void InitContextMenu()
		{
			_barManager.BeginInit();

			var maxId = _barManager.MaxItemId++;

			var buttonEditor = new RepositoryItemButtonEdit { AutoHeight = false };
			buttonEditor.Buttons.Clear();
			buttonEditor.Buttons.AddRange(new[] {
				new EditorButton(ButtonPredefines.Delete)
			});
			buttonEditor.ButtonClick += (o, e) =>
			{
				((ButtonEdit)o).EditValue = null;
			};
			ItemHoverNote = new BarEditItem
			{
				Id = maxId,
				Caption = "Hover Note",
				Edit = buttonEditor,
				Width = 150
			};
			maxId++;
			ItemHoverNote.EditValueChanged += (o, e) => LineBreakLoader.OnHoverChanged();

			_barManager.Items.Add(ItemHoverNote);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
