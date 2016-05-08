using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	class QuickEditManager
	{
		private readonly BarManager _barManager;
		private readonly BarSubItem _itemsContainer;

		private SettingsManager _settingsManager;

		private bool _changesMade;

		private BarCheckItem _itemBoldFont;
		private BarCheckItem _itemItalicFont;
		private BarCheckItem _itemUnderlinedFont;
		private BarSubItem _itemLinkNote;
		private BarEditItem _itemLinkNoteCustom;
		private BarEditItem _itemHoverNote;
		private BarEditItem _itemFontColor;
		private BarEditItem _itemLineBreakFont;

		public event EventHandler<EventArgs> OnSettingsChanged;

		public QuickEditManager(BarSubItem itemsContainer)
		{
			_itemsContainer = itemsContainer;
			_barManager = _itemsContainer.Manager;
			InitItemsContainer();
		}

		public void LoadLinkSettings(BaseLibraryLink targetLink)
		{
			_settingsManager = SettingsManager.Create(this, targetLink);
			_settingsManager.OnSettingsChanged += (o, e) =>
			{
				_changesMade = true;
			};
			_settingsManager.LoadSettings();
			_changesMade = false;
		}

		public void ApplySettings()
		{
			if (!_changesMade) return;
			_changesMade = false;
			_settingsManager.TargetLink.MarkAsModified();
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

			var fontEditor = new RepositoryItemButtonEdit()
			{
				AutoHeight = false,
				TextEditStyle = TextEditStyles.DisableTextEditor
			};
			fontEditor.Buttons.Clear();
			fontEditor.Buttons.AddRange(new[] { new EditorButton(ButtonPredefines.Ellipsis) });
			var fontEditHandler = new Action<object, EventArgs>((o, e) =>
			{
				using (var dlgFont = new FontDialog())
				{
					dlgFont.Font = _itemLineBreakFont.Tag as Font;
					if (dlgFont.ShowDialog() != DialogResult.OK) return;
					_itemLineBreakFont.Tag = dlgFont.Font;
					_itemLineBreakFont.EditValue = Utils.FontToString(dlgFont.Font);
				}
			});
			fontEditor.ButtonClick += (o, e) => fontEditHandler(o, e);
			fontEditor.Click += (o, e) => fontEditHandler(o, e);

			var maxId = _barManager.MaxItemId++;

			var linkNoteControlButtonCollection = new List<BarItem>();
			_itemLinkNoteCustom = new BarEditItem
			{
				Id = maxId,
				Caption = "Custom",
				Edit = buttonEditor,
				Width = 150
			};
			_itemLinkNoteCustom.EditValueChanged += (o, e) => _settingsManager.OnNoteChanged();
			maxId++;
			foreach (var noteText in ObjectLinkSettingsManager.PredefinedNotes)
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
					_itemLinkNoteCustom.EditValue = itemNoteText;
				};
				linkNoteControlButtonCollection.Add(itemLinkNote);
			}
			linkNoteControlButtonCollection.Add(_itemLinkNoteCustom);
			_itemLinkNote = new BarSubItem();
			_itemLinkNote.Caption = "Link Note";
			_itemLinkNote.Id = maxId;
			_barManager.Items.AddRange(linkNoteControlButtonCollection.ToArray());
			_itemLinkNote.LinksPersistInfo.AddRange(linkNoteControlButtonCollection
				.Select(barItem => new LinkPersistInfo(barItem)).ToArray());
			maxId++;

			_itemBoldFont = new BarCheckItem
			{
				Id = maxId,
				Caption = "Bold",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};

			var boldFont = new Font(_itemBoldFont.ItemInMenuAppearance.Normal.Font.Name, _itemBoldFont.ItemInMenuAppearance.Normal.Font.Size, FontStyle.Bold);
			_itemBoldFont.ItemInMenuAppearance.Normal.Font = boldFont;
			_itemBoldFont.ItemInMenuAppearance.Pressed.Font = boldFont;
			_itemBoldFont.ItemInMenuAppearance.Hovered.Font = boldFont;

			_itemBoldFont.CheckedChanged += (o, e) => _settingsManager.OnFontChanged();
			maxId++;

			_itemItalicFont = new BarCheckItem
			{
				Id = maxId,
				Caption = "Italics",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};

			var italicFont = new Font(_itemItalicFont.ItemInMenuAppearance.Normal.Font.Name, _itemItalicFont.ItemInMenuAppearance.Normal.Font.Size, FontStyle.Italic);
			_itemItalicFont.ItemInMenuAppearance.Normal.Font = italicFont;
			_itemItalicFont.ItemInMenuAppearance.Pressed.Font = italicFont;
			_itemItalicFont.ItemInMenuAppearance.Hovered.Font = italicFont;

			_itemItalicFont.CheckedChanged += (o, e) => _settingsManager.OnFontChanged();
			maxId++;

			_itemUnderlinedFont = new BarCheckItem
			{
				Id = maxId,
				Caption = "Underline",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};

			var underlineFont = new Font(_itemUnderlinedFont.ItemInMenuAppearance.Normal.Font.Name, _itemUnderlinedFont.ItemInMenuAppearance.Normal.Font.Size, FontStyle.Underline);
			_itemUnderlinedFont.ItemInMenuAppearance.Normal.Font = underlineFont;
			_itemUnderlinedFont.ItemInMenuAppearance.Pressed.Font = underlineFont;
			_itemUnderlinedFont.ItemInMenuAppearance.Hovered.Font = underlineFont;

			_itemUnderlinedFont.CheckedChanged += (o, e) => _settingsManager.OnFontChanged();
			maxId++;

			_itemLineBreakFont = new BarEditItem
			{
				Id = maxId,
				Caption = "Font  ",
				Edit = fontEditor
			};
			_itemLineBreakFont.EditValueChanged += (o, e) =>
			{
				_settingsManager.OnFontChanged();
				_barManager.CloseMenus();
			};
			maxId++;

			_itemHoverNote = new BarEditItem
			{
				Id = maxId,
				Caption = "Hover Note",
				Edit = buttonEditor,
				Width = 150
			};
			maxId++;
			_itemHoverNote.EditValueChanged += (o, e) => _settingsManager.OnHoverChanged();

			_itemFontColor = new BarEditItem
			{
				Id = maxId,
				Caption = "Font Color  ",
				Edit = colorEditor
			};
			maxId++;
			_itemFontColor.EditValueChanged += (o, e) =>
			{
				_settingsManager.OnColorChanged();
				_barManager.CloseMenus();
			};

			_barManager.Items.Add(_itemBoldFont);
			_barManager.Items.Add(_itemItalicFont);
			_barManager.Items.Add(_itemUnderlinedFont);
			_barManager.Items.Add(_itemLineBreakFont);
			_barManager.Items.Add(_itemLinkNote);
			_barManager.Items.Add(_itemHoverNote);
			_barManager.Items.Add(_itemFontColor);
			_barManager.MaxItemId = maxId;

			_itemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(_itemBoldFont),
				new LinkPersistInfo(_itemItalicFont),
				new LinkPersistInfo(_itemUnderlinedFont),
				new LinkPersistInfo(_itemLineBreakFont),
				new LinkPersistInfo(_itemLinkNote),
				new LinkPersistInfo(_itemHoverNote),
				new LinkPersistInfo(_itemFontColor),
			});
			_barManager.EndInit();
		}

		abstract class SettingsManager
		{
			protected readonly QuickEditManager _editManager;
			protected bool _loading;

			public BaseLibraryLink TargetLink { get; private set; }

			public event EventHandler<EventArgs> OnSettingsChanged;

			protected SettingsManager(QuickEditManager editManager, BaseLibraryLink targetLink)
			{
				_editManager = editManager;
				TargetLink = targetLink;
			}

			protected abstract void SetMenuItemsViibility();

			public abstract void OnNoteChanged();
			public abstract void OnFontChanged();
			public abstract void OnHoverChanged();

			public virtual void OnColorChanged()
			{
				if (_loading) return;

				var color = (Color)_editManager._itemFontColor.EditValue;
				if (color != TargetLink.DisplayColor)
					TargetLink.Settings.ForeColor = color;

				RaiseSettingsChanged();
			}

			protected void RaiseSettingsChanged()
			{
				if (OnSettingsChanged != null)
					OnSettingsChanged(this, EventArgs.Empty);
			}

			public virtual void LoadSettings()
			{
				SetMenuItemsViibility();
			}

			public static SettingsManager Create(QuickEditManager editManager, BaseLibraryLink targetLink)
			{
				if (targetLink is LibraryObjectLink)
					return new ObjectLinkSettingsManager(editManager, targetLink);
				else
					return new LineBreakSettingsManager(editManager, targetLink);
			}
		}

		class ObjectLinkSettingsManager : SettingsManager
		{
			public static readonly string[] PredefinedNotes =  { 
				BaseLinkSettings.PredefinedNoteNone, 
				BaseLinkSettings.PredefinedNoteNew, 
				BaseLinkSettings.PredefinedNoteUpdated, 
				BaseLinkSettings.PredefinedNoteSellThis, 
				BaseLinkSettings.PredefinedNoteAttention 
			};


			public ObjectLinkSettingsManager(QuickEditManager editManager, BaseLibraryLink targetLink) : base(editManager, targetLink) { }

			private LibraryObjectLinkSettings Settings
			{
				get { return (LibraryObjectLinkSettings)TargetLink.Settings; }
			}

			protected override void SetMenuItemsViibility()
			{
				_editManager._itemBoldFont.Visibility = BarItemVisibility.Always;
				_editManager._itemItalicFont.Visibility = BarItemVisibility.Always;
				_editManager._itemUnderlinedFont.Visibility = BarItemVisibility.Always;
				_editManager._itemLineBreakFont.Visibility = BarItemVisibility.Never;
				_editManager._itemLinkNote.Visibility = BarItemVisibility.Always;
				_editManager._itemHoverNote.Visibility = BarItemVisibility.Always;
				_editManager._itemFontColor.Visibility = BarItemVisibility.Always;
			}

			public override void LoadSettings()
			{
				base.LoadSettings();

				_loading = true;
				_editManager._itemBoldFont.Checked = (Settings.RegularFontStyle & FontStyle.Bold) == FontStyle.Bold;
				_editManager._itemItalicFont.Checked = (Settings.RegularFontStyle & FontStyle.Italic) == FontStyle.Italic;
				_editManager._itemUnderlinedFont.Checked = (Settings.RegularFontStyle & FontStyle.Underline) == FontStyle.Underline;
				if (!(String.IsNullOrEmpty(Settings.Note) || PredefinedNotes.Contains(Settings.Note)))
					_editManager._itemLinkNoteCustom.EditValue = Settings.Note;
				_editManager._itemHoverNote.EditValue = Settings.HoverNote;
				_editManager._itemFontColor.EditValue = TargetLink.DisplayColor;
				_loading = false;
			}

			public override void OnNoteChanged()
			{
				if (_loading) return;

				_loading = true;
				var itemNoteText = _editManager._itemLinkNoteCustom.EditValue as String;
				if (itemNoteText == BaseLinkSettings.PredefinedNoteNone)
				{
					itemNoteText = null;
					_editManager._itemLinkNoteCustom.EditValue = null;
				}
				if (PredefinedNotes.Contains(itemNoteText))
				{
					_editManager._itemLinkNoteCustom.EditValue = null;
				}
				Settings.Note = itemNoteText;
				_loading = false;

				RaiseSettingsChanged();
			}

			public override void OnFontChanged()
			{
				if (_loading) return;

				var regularFontStyle = FontStyle.Regular;
				if (_editManager._itemBoldFont.Checked)
					regularFontStyle = regularFontStyle | FontStyle.Bold;
				if (_editManager._itemItalicFont.Checked)
					regularFontStyle = regularFontStyle | FontStyle.Italic;
				if (_editManager._itemUnderlinedFont.Checked)
					regularFontStyle = regularFontStyle | FontStyle.Underline;
				Settings.RegularFontStyle = regularFontStyle;
				if (regularFontStyle != FontStyle.Regular)
				{
					Settings.IsSpecialFormat = false;
					Settings.Font = null;
				}

				RaiseSettingsChanged();
			}

			public override void OnHoverChanged()
			{
				if (_loading) return;

				Settings.HoverNote = _editManager._itemHoverNote.EditValue as String;

				RaiseSettingsChanged();
			}
		}

		class LineBreakSettingsManager : SettingsManager
		{
			public LineBreakSettingsManager(QuickEditManager editManager, BaseLibraryLink targetLink) : base(editManager, targetLink) { }

			private LineBreakSettings Settings
			{
				get { return (LineBreakSettings)TargetLink.Settings; }
			}

			protected override void SetMenuItemsViibility()
			{
				_editManager._itemBoldFont.Visibility = BarItemVisibility.Never;
				_editManager._itemItalicFont.Visibility = BarItemVisibility.Never;
				_editManager._itemUnderlinedFont.Visibility = BarItemVisibility.Never;
				_editManager._itemLineBreakFont.Visibility = BarItemVisibility.Always;
				_editManager._itemLinkNote.Visibility = BarItemVisibility.Never;
				_editManager._itemHoverNote.Visibility = BarItemVisibility.Always;
				_editManager._itemFontColor.Visibility = BarItemVisibility.Always;
			}

			public override void LoadSettings()
			{
				base.LoadSettings();

				_loading = true;
				_editManager._itemLineBreakFont.Tag = Settings.Font;
				_editManager._itemLineBreakFont.EditValue = Utils.FontToString(Settings.Font);
				_editManager._itemFontColor.EditValue = TargetLink.DisplayColor;
				_editManager._itemHoverNote.EditValue = Settings.Note;
				_loading = false;
			}

			public override void OnNoteChanged() { }

			public override void OnFontChanged()
			{
				if (_loading) return;

				Settings.Font = (Font)_editManager._itemLineBreakFont.Tag;

				RaiseSettingsChanged();
			}

			public override void OnHoverChanged()
			{
				if (_loading) return;

				Settings.Note = _editManager._itemHoverNote.EditValue as string;

				RaiseSettingsChanged();
			}
		}
	}
}
