using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LineBreakNotesLoader : BaseLineBreakLoader
	{
		private LineBreakNotesEditor TextFormatEditor => (LineBreakNotesEditor)_editor;

		public LineBreakNotesLoader(BaseContextMenuEditor editor, IList<LineBreak> targetLinks) : base(editor, targetLinks) { }

		public override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemsContainer.Visibility = TargetLinks.Any() ? BarItemVisibility.Always : BarItemVisibility.Never;
			TextFormatEditor.ItemHoverNote.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLinks()
		{
			if (!TargetLinks.Any()) return;

			_loading = true;

			var settings = TargetLinks.Select(link => link.Settings).OfType<LineBreakSettings>().ToList();
			var defaultHoverNote = settings.Select(s => s.Note).FirstOrDefault();
			defaultHoverNote = settings.All(s => String.Equals(s.Note, defaultHoverNote, StringComparison.OrdinalIgnoreCase))
				? defaultHoverNote
				: null;
			TextFormatEditor.ItemHoverNote.EditValue = defaultHoverNote;

			_loading = false;
		}

		public void OnHoverChanged()
		{
			if (_loading) return;

			TargetLinks
				.Select(link => link.Settings)
				.OfType<LineBreakSettings>()
				.ToList()
				.ForEach(s => s.Note = TextFormatEditor.ItemHoverNote.EditValue as String);

			RaiseSettingsChanged();
		}
	}
}
