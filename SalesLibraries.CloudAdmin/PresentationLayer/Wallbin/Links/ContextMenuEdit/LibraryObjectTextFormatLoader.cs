using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LibraryObjectTextFormatLoader : BaseLibraryObjectLoader
	{
		private LibraryObjectTextFormatEditor TextFormatEditor => (LibraryObjectTextFormatEditor)_editor;

		public LibraryObjectTextFormatLoader(BaseContextMenuEditor editor, IList<LibraryObjectLink> targetLinks) : base(editor, targetLinks) { }

		public override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemsContainer.Visibility = TargetLinks.Any() ? BarItemVisibility.Always : BarItemVisibility.Never;
			TextFormatEditor.ItemBoldFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemItalicFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemUnderlinedFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemFontColor.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLinks()
		{
			if (!TargetLinks.Any()) return;

			_loading = true;

			var linkSettings = TargetLinks.Select(link => link.Settings).OfType<LibraryObjectLinkSettings>().ToList();

			foreach (var linkSetting in linkSettings)
			{
				TextFormatEditor.ItemBoldFont.Checked = (linkSetting.RegularFontStyle & FontStyle.Bold) == FontStyle.Bold;
				TextFormatEditor.ItemItalicFont.Checked = (linkSetting.RegularFontStyle & FontStyle.Italic) == FontStyle.Italic;
				TextFormatEditor.ItemUnderlinedFont.Checked = (linkSetting.RegularFontStyle & FontStyle.Underline) == FontStyle.Underline;
			}

			var defaultColor = TargetLinks.Select(link => link.DisplayColor).FirstOrDefault();
			defaultColor = TargetLinks.All(link => link.DisplayColor == defaultColor) ? defaultColor : Color.Black;
			TextFormatEditor.ItemFontColor.EditValue = defaultColor;

			_loading = false;
		}

		public void OnFontChanged()
		{
			if (_loading) return;

			var regularFontStyle = FontStyle.Regular;
			if (TextFormatEditor.ItemBoldFont.Checked)
				regularFontStyle = regularFontStyle | FontStyle.Bold;
			if (TextFormatEditor.ItemItalicFont.Checked)
				regularFontStyle = regularFontStyle | FontStyle.Italic;
			if (TextFormatEditor.ItemUnderlinedFont.Checked)
				regularFontStyle = regularFontStyle | FontStyle.Underline;

			var linkSettings = TargetLinks.Select(link => link.Settings).OfType<LibraryObjectLinkSettings>().ToList();
			foreach (var linkSetting in linkSettings)
			{
				linkSetting.RegularFontStyle = regularFontStyle;
				if (regularFontStyle != FontStyle.Regular)
				{
					linkSetting.IsSpecialFormat = false;
					linkSetting.Font = null;
				}
			}

			RaiseSettingsChanged();
		}

		public void OnColorChanged()
		{
			if (_loading) return;

			var color = (Color)TextFormatEditor.ItemFontColor.EditValue;
			foreach (var link in TargetLinks)
			{
				if (color != link.DisplayColor)
					link.Settings.ForeColor = color;
			}

			RaiseSettingsChanged();
		}
	}
}
