using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LineBreakNotesLoader : BaseLineBreakLoader
	{
		private LineBreakNotesEditor TextFormatEditor => (LineBreakNotesEditor)_editor;

		private LineBreakSettings Settings => (LineBreakSettings)TargetLink.Settings;

		public LineBreakNotesLoader(BaseContextMenuEditor editor, LineBreak targetLink) : base(editor, targetLink) { }

		protected override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemHoverNote.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLink()
		{
			base.LoadLink();

			_loading = true;
			TextFormatEditor.ItemHoverNote.EditValue = Settings.Note;
			_loading = false;
		}

		public void OnHoverChanged()
		{
			if (_loading) return;

			Settings.Note = TextFormatEditor.ItemHoverNote.EditValue as string;

			RaiseSettingsChanged();
		}
	}
}
