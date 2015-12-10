using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	class QuickEditManager
	{
		private readonly string[] _predefinedNotes =  { 
				BaseLinkSettings.PredefinedNoteNone, 
				BaseLinkSettings.PredefinedNoteNew, 
				BaseLinkSettings.PredefinedNoteUpdated, 
				BaseLinkSettings.PredefinedNoteSellThis, 
				BaseLinkSettings.PredefinedNoteAttention 
			};

		private readonly BarManager _barManager;
		private readonly BarSubItem _itemsContainer;
		private BaseLibraryLink _targetLink;
		private bool _loading;
		private bool _changesMade;

		private BarCheckItem _itemBoldFont;
		private BarEditItem _itemLinkNoteCustom;
		private BarEditItem _itemHoverNote;
		private BarEditItem _itemFontColor;

		public event EventHandler<EventArgs> OnSettingsChanged;

		private LibraryObjectLinkSettings Settings
		{
			get { return (LibraryObjectLinkSettings)_targetLink.Settings; }
		}

		public QuickEditManager(BarSubItem itemsContainer)
		{
			_itemsContainer = itemsContainer;
			_barManager = _itemsContainer.Manager;
			InitItemsContainer();
		}

		public void LoadLinkSettings(BaseLibraryLink targetLink)
		{
			_targetLink = targetLink;

			if (!(_targetLink is LibraryObjectLink))
			{
				_itemsContainer.Visibility = BarItemVisibility.Never;
				return;
			}

			_itemsContainer.Visibility = BarItemVisibility.Always;

			_loading = true;

			_itemBoldFont.Checked = Settings.IsBold;
			if (!(String.IsNullOrEmpty(Settings.Note) || _predefinedNotes.Contains(Settings.Note)))
				_itemLinkNoteCustom.EditValue = Settings.Note;
			_itemHoverNote.EditValue = Settings.HoverNote;
			_itemFontColor.EditValue = _targetLink.DisplayColor;

			_loading = false;
			_changesMade = false;
		}

		public void ApplySettings()
		{
			if (!_changesMade) return;
			_changesMade = false;
			if (OnSettingsChanged != null)
				OnSettingsChanged(this, EventArgs.Empty);
		}

		private void InitItemsContainer()
		{
			_barManager.BeginInit();

			var buttonEditor = new RepositoryItemButtonEdit { AutoHeight = false };
			buttonEditor.Buttons.Clear();
			buttonEditor.Buttons.AddRange(new[] {
				new EditorButton(ButtonPredefines.Delete)
			});
			buttonEditor.ButtonClick += (o, e) =>
			{
				((ButtonEdit)o).EditValue = null;
			};

			var colorEditor = new RepositoryItemColorEdit
			{
				AutoHeight = false,
				ColorAlignment = HorzAlignment.Center,
				ShowSystemColors = false,
				ShowWebColors = false
			};
			colorEditor.Buttons.Clear();
			colorEditor.Buttons.AddRange(new[] { new EditorButton(ButtonPredefines.Combo) });

			var maxId = _barManager.MaxItemId++;

			var linkNoteControlButtonCollection = new List<BarItem>();
			_itemLinkNoteCustom = new BarEditItem
			{
				Id = maxId,
				Caption = "Custom",
				Edit = buttonEditor,
				Width = 150
			};
			_itemLinkNoteCustom.EditValueChanged += (o, e) =>
			{
				if (_loading) return;
				var itemNoteText = _itemLinkNoteCustom.EditValue as String;
				Settings.Note = itemNoteText != BaseLinkSettings.PredefinedNoteNone ? itemNoteText : null;
				_changesMade = true;
			};
			maxId++;


			foreach (var noteText in _predefinedNotes)
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
					_itemLinkNoteCustom.EditValue = null;
					Settings.Note = itemNoteText != BaseLinkSettings.PredefinedNoteNone ? itemNoteText : null;
					_changesMade = true;
				};
				linkNoteControlButtonCollection.Add(itemLinkNote);
			}
			linkNoteControlButtonCollection.Add(_itemLinkNoteCustom);
			var itemLinkNoteContainer = new BarSubItem();
			itemLinkNoteContainer.Caption = "Link Note";
			itemLinkNoteContainer.Id = maxId;
			_barManager.Items.AddRange(linkNoteControlButtonCollection.ToArray());
			itemLinkNoteContainer.LinksPersistInfo.AddRange(linkNoteControlButtonCollection
				.Select(barItem => new LinkPersistInfo(barItem)).ToArray());
			maxId++;


			_itemBoldFont = new BarCheckItem
			{
				Id = maxId,
				Caption = "Bold Text",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			maxId++;
			_itemBoldFont.CheckedChanged += (o, e) =>
			{
				if (_loading) return;
				_changesMade = true;
				Settings.IsBold = _itemBoldFont.Checked;
				if (!Settings.IsBold) return;
				Settings.IsSpecialFormat = false;
				Settings.Font = null;
			};

			_itemHoverNote = new BarEditItem
			{
				Id = maxId,
				Caption = "Hover Note",
				Edit = buttonEditor,
				Width = 150
			};
			maxId++;
			_itemHoverNote.EditValueChanged += (o, e) =>
			{
				if (_loading) return;
				Settings.HoverNote = _itemHoverNote.EditValue as String;
				_changesMade = true;
			};

			_itemFontColor = new BarEditItem
			{
				Id = maxId,
				Caption = "Font Color  ",
				Edit = colorEditor
			};
			maxId++;
			_itemFontColor.EditValueChanged += (o, e) =>
			{
				if (_loading) return;
				var color = (Color)_itemFontColor.EditValue;
				if (color != _targetLink.DisplayColor)
					Settings.ForeColor = color;
				_changesMade = true;
			};

			_barManager.Items.Add(_itemBoldFont);
			_barManager.Items.Add(itemLinkNoteContainer);
			_barManager.Items.Add(_itemHoverNote);
			_barManager.Items.Add(_itemFontColor);
			_barManager.MaxItemId = maxId;

			_itemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(_itemBoldFont),
				new LinkPersistInfo(itemLinkNoteContainer),
				new LinkPersistInfo(_itemHoverNote),
				new LinkPersistInfo(_itemFontColor),
			});
			_barManager.EndInit();
		}
	}
}
