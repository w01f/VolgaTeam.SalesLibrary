using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LibraryObjectNotesLoader : BaseLibraryObjectLoader
	{
		public static readonly string[] PredefinedNotes =  {
				BaseLinkSettings.PredefinedNoteNone,
				BaseLinkSettings.PredefinedNoteNew,
				BaseLinkSettings.PredefinedNoteSold,
				BaseLinkSettings.PredefinedNoteUpdated,
				BaseLinkSettings.PredefinedNoteSellThis,
				BaseLinkSettings.PredefinedNoteAttention
			};

		private LibraryObjectNotesEditor TextFormatEditor => (LibraryObjectNotesEditor)_editor;

		public LibraryObjectNotesLoader(BaseContextMenuEditor editor, IList<LibraryObjectLink> targetLinks) : base(editor, targetLinks) { }

		public override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemsContainer.Visibility = TargetLinks.Any() ? BarItemVisibility.Always : BarItemVisibility.Never;
			TextFormatEditor.ItemLinkNote.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemHoverNote.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLinks()
		{
			if (!TargetLinks.Any()) return;

			_loading = true;

			var settings = TargetLinks.Select(link => link.Settings).OfType<LibraryObjectLinkSettings>().ToList();

			var defaultNote = settings.Select(s => s.Note).FirstOrDefault();
			defaultNote = settings.All(s => String.Equals(s.Note, defaultNote, StringComparison.OrdinalIgnoreCase))
				? defaultNote
				: null;
			if (!(String.IsNullOrEmpty(defaultNote) || PredefinedNotes.Contains(defaultNote)))
				TextFormatEditor.ItemLinkNoteCustom.EditValue = defaultNote;
			else
				TextFormatEditor.ItemLinkNoteCustom.EditValue = null;

			var defaultHoverNote = settings.Select(s => s.HoverNote).FirstOrDefault();
			defaultHoverNote = settings.All(s => String.Equals(s.HoverNote, defaultHoverNote, StringComparison.OrdinalIgnoreCase))
				? defaultHoverNote
				: null;
			TextFormatEditor.ItemHoverNote.EditValue = defaultHoverNote;

			_loading = false;
		}

		public void OnNoteChanged()
		{
			if (_loading) return;

			_loading = true;

			var itemNoteText = TextFormatEditor.ItemLinkNoteCustom.EditValue as String;
			if (itemNoteText == BaseLinkSettings.PredefinedNoteNone)
			{
				itemNoteText = null;
				TextFormatEditor.ItemLinkNoteCustom.EditValue = null;
			}
			if (PredefinedNotes.Contains(itemNoteText))
			{
				TextFormatEditor.ItemLinkNoteCustom.EditValue = null;
			}

			TargetLinks
				.Select(link => link.Settings)
				.OfType<LibraryObjectLinkSettings>()
				.ToList()
				.ForEach(s => s.Note = itemNoteText);

			_loading = false;

			RaiseSettingsChanged();
		}

		public void OnHoverChanged()
		{
			if (_loading) return;

			TargetLinks
				.Select(link => link.Settings)
				.OfType<LibraryObjectLinkSettings>()
				.ToList()
				.ForEach(s => s.HoverNote = TextFormatEditor.ItemHoverNote.EditValue as String);

			RaiseSettingsChanged();
		}
	}
}
