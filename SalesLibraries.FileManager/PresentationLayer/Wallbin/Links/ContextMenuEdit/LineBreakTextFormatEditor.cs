using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LineBreakTextFormatEditor : BaseContextMenuEditor
	{
		public BarEditItem ItemFontColor { get; private set; }
		public BarEditItem ItemFont { get; private set; }

		private LineBreakTextFormatLoader LineBreakLoader => (LineBreakTextFormatLoader) _linkLoader;

		public LineBreakTextFormatEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		protected override ILinkLoader CreateLoader(IList<BaseLibraryLink> targetLinks)
		{
			return new LineBreakTextFormatLoader(this, targetLinks.OfType<LineBreak>().ToList());
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			ItemsContainer.LinksPersistInfo.Clear();
			ItemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(ItemFont),
				new LinkPersistInfo(ItemFontColor),
			});
			_barManager.EndInit();
		}

		private void InitContextMenu()
		{
			_barManager.BeginInit();

			var maxId = _barManager.MaxItemId++;

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
					dlgFont.Font = ItemFont.Tag as Font;
					if (dlgFont.ShowDialog() != DialogResult.OK) return;
					ItemFont.Tag = dlgFont.Font;
					ItemFont.EditValue = Utils.FontToString(dlgFont.Font);
				}
			});
			fontEditor.ButtonClick += (o, e) => fontEditHandler(o, e);
			fontEditor.Click += (o, e) => fontEditHandler(o, e);
			ItemFont = new BarEditItem
			{
				Id = maxId,
				Caption = "Font  ",
				Edit = fontEditor,
				Width = 150
			};
			ItemFont.EditValueChanged += (o, e) =>
			{
				LineBreakLoader.OnFontChanged();
				_barManager.CloseMenus();
			};
			maxId++;

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
				LineBreakLoader.OnColorChanged();
				_barManager.CloseMenus();
			};

			_barManager.Items.Add(ItemFontColor);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
