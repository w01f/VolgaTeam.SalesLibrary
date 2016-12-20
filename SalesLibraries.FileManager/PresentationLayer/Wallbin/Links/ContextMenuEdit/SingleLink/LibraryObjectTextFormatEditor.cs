using System.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	class LibraryObjectTextFormatEditor : BaseContextMenuEditor
	{
		public BarCheckItem ItemBoldFont { get; private set; }
		public BarCheckItem ItemItalicFont { get; private set; }
		public BarCheckItem ItemUnderlinedFont { get; private set; }
		public BarEditItem ItemFontColor { get; private set; }

		private LibraryObjectTextFormatLoader LibraryObjectLoader => (LibraryObjectTextFormatLoader)_linkLoader;

		public LibraryObjectTextFormatEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		public override void LoadLink(BaseLibraryLink targetLink)
		{
			if (!(targetLink is LibraryObjectLink)) return;
			base.LoadLink(targetLink);
		}

		protected override ILinkLoader CreateLoader(BaseLibraryLink targetLink)
		{
			return new LibraryObjectTextFormatLoader(this, (LibraryObjectLink)targetLink);
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			_itemsContainer.LinksPersistInfo.Clear();
			_itemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(ItemBoldFont),
				new LinkPersistInfo(ItemItalicFont),
				new LinkPersistInfo(ItemUnderlinedFont),
				new LinkPersistInfo(ItemFontColor),
			});
			_barManager.EndInit();
		}

		private void InitContextMenu()
		{
			_barManager.BeginInit();

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

			ItemBoldFont = new BarCheckItem
			{
				Id = maxId,
				Caption = "Bold",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};

			var boldFont = new Font(ItemBoldFont.ItemInMenuAppearance.Normal.Font.Name,
				ItemBoldFont.ItemInMenuAppearance.Normal.Font.Size, FontStyle.Bold);
			ItemBoldFont.ItemInMenuAppearance.Normal.Font = boldFont;
			ItemBoldFont.ItemInMenuAppearance.Pressed.Font = boldFont;
			ItemBoldFont.ItemInMenuAppearance.Hovered.Font = boldFont;

			ItemBoldFont.CheckedChanged += (o, e) => LibraryObjectLoader.OnFontChanged();
			maxId++;

			ItemItalicFont = new BarCheckItem
			{
				Id = maxId,
				Caption = "Italics",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};

			var italicFont = new Font(ItemItalicFont.ItemInMenuAppearance.Normal.Font.Name,
				ItemItalicFont.ItemInMenuAppearance.Normal.Font.Size, FontStyle.Italic);
			ItemItalicFont.ItemInMenuAppearance.Normal.Font = italicFont;
			ItemItalicFont.ItemInMenuAppearance.Pressed.Font = italicFont;
			ItemItalicFont.ItemInMenuAppearance.Hovered.Font = italicFont;

			ItemItalicFont.CheckedChanged += (o, e) => LibraryObjectLoader.OnFontChanged();
			maxId++;

			ItemUnderlinedFont = new BarCheckItem
			{
				Id = maxId,
				Caption = "Underline",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};

			var underlineFont = new Font(ItemUnderlinedFont.ItemInMenuAppearance.Normal.Font.Name,
				ItemUnderlinedFont.ItemInMenuAppearance.Normal.Font.Size, FontStyle.Underline);
			ItemUnderlinedFont.ItemInMenuAppearance.Normal.Font = underlineFont;
			ItemUnderlinedFont.ItemInMenuAppearance.Pressed.Font = underlineFont;
			ItemUnderlinedFont.ItemInMenuAppearance.Hovered.Font = underlineFont;

			ItemUnderlinedFont.CheckedChanged += (o, e) => LibraryObjectLoader.OnFontChanged();
			maxId++;

			ItemFontColor = new BarEditItem
			{
				Id = maxId,
				Caption = "Font Color  ",
				Edit = colorEditor,
				Width = 150
			};
			maxId++;
			ItemFontColor.EditValueChanged += (o, e) =>
			{
				LibraryObjectLoader.OnColorChanged();
				_barManager.CloseMenus();
			};

			_barManager.Items.Add(ItemBoldFont);
			_barManager.Items.Add(ItemItalicFont);
			_barManager.Items.Add(ItemUnderlinedFont);
			_barManager.Items.Add(ItemFontColor);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
