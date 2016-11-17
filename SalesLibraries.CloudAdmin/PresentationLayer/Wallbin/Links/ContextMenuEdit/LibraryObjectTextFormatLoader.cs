using System.Drawing;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LibraryObjectTextFormatLoader : BaseLibraryObjectLoader
	{
		private LibraryObjectTextFormatEditor TextFormatEditor => (LibraryObjectTextFormatEditor)_editor;

		private LibraryObjectLinkSettings Settings => (LibraryObjectLinkSettings)TargetLink.Settings;

		public LibraryObjectTextFormatLoader(BaseContextMenuEditor editor, LibraryObjectLink targetLink) : base(editor, targetLink) { }

		protected override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemBoldFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemItalicFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemUnderlinedFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemFontColor.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLink()
		{
			base.LoadLink();

			_loading = true;
			TextFormatEditor.ItemBoldFont.Checked = (Settings.RegularFontStyle & FontStyle.Bold) == FontStyle.Bold;
			TextFormatEditor.ItemItalicFont.Checked = (Settings.RegularFontStyle & FontStyle.Italic) == FontStyle.Italic;
			TextFormatEditor.ItemUnderlinedFont.Checked = (Settings.RegularFontStyle & FontStyle.Underline) == FontStyle.Underline;
			TextFormatEditor.ItemFontColor.EditValue = TargetLink.DisplayColor;
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
			Settings.RegularFontStyle = regularFontStyle;
			if (regularFontStyle != FontStyle.Regular)
			{
				Settings.IsSpecialFormat = false;
				Settings.Font = null;
			}

			RaiseSettingsChanged();
		}

		public void OnColorChanged()
		{
			if (_loading) return;

			var color = (Color)TextFormatEditor.ItemFontColor.EditValue;
			if (color != TargetLink.DisplayColor)
				TargetLink.Settings.ForeColor = color;

			RaiseSettingsChanged();
		}
	}
}
