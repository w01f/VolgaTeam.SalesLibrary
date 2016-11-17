using System;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
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

		private LibraryObjectLinkSettings Settings => (LibraryObjectLinkSettings)TargetLink.Settings;

		public LibraryObjectNotesLoader(BaseContextMenuEditor editor, LibraryObjectLink targetLink) : base(editor, targetLink) { }

		protected override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemLinkNote.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemHoverNote.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLink()
		{
			base.LoadLink();

			_loading = true;
			if (!(String.IsNullOrEmpty(Settings.Note) || PredefinedNotes.Contains(Settings.Note)))
				TextFormatEditor.ItemLinkNoteCustom.EditValue = Settings.Note;
			else
				TextFormatEditor.ItemLinkNoteCustom.EditValue = null;
			TextFormatEditor.ItemHoverNote.EditValue = Settings.HoverNote;
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
			Settings.Note = itemNoteText;
			_loading = false;

			RaiseSettingsChanged();
		}

		public void OnHoverChanged()
		{
			if (_loading) return;

			Settings.HoverNote = TextFormatEditor.ItemHoverNote.EditValue as string;

			RaiseSettingsChanged();
		}
	}
}
