using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LineBreakNotesEditor : BaseContextMenuEditor
	{
		public BarEditItem ItemHoverNote { get; private set; }

		private LineBreakNotesLoader LineBreakLoader => (LineBreakNotesLoader) _linkLoader;

		public LineBreakNotesEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		public override void LoadLink(BaseLibraryLink targetLink)
		{
			if (!(targetLink is LineBreak)) return;
			base.LoadLink(targetLink);
		}

		protected override ILinkLoader CreateLoader(BaseLibraryLink targetLink)
		{
			return new LineBreakNotesLoader(this, (LineBreak)targetLink);
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			_itemsContainer.LinksPersistInfo.Clear();
			_itemsContainer.LinksPersistInfo.AddRange(new[]
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
