using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class LineBreakTextFormatLoader : BaseLineBreakLoader
	{
		private LineBreakTextFormatEditor TextFormatEditor => (LineBreakTextFormatEditor)_editor;

		public LineBreakTextFormatLoader(BaseContextMenuEditor editor, IList<LineBreak> targetLinks) : base(editor, targetLinks) { }

		public override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemsContainer.Visibility = TargetLinks.Any() ? BarItemVisibility.Always : BarItemVisibility.Never;
			TextFormatEditor.ItemFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemFontColor.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLinks()
		{
			if (!TargetLinks.Any()) return;

			_loading = true;

			var settings = TargetLinks.Select(link => link.Settings).OfType<LineBreakSettings>().ToList();

			var defaultFont = settings.Select(s => s.Font).FirstOrDefault();
			defaultFont = settings.All(s => s.Font.Equals(defaultFont)) ? defaultFont : BaseLinkSettings.DefaultFont;
			TextFormatEditor.ItemFont.Tag = defaultFont;
			TextFormatEditor.ItemFont.EditValue = Utils.FontToString(defaultFont);

			var defaultColor = TargetLinks.Select(link => link.DisplayColor).FirstOrDefault();
			defaultColor = TargetLinks.All(link => link.DisplayColor == defaultColor) ? defaultColor : Color.Black;
			TextFormatEditor.ItemFontColor.EditValue = defaultColor;

			_loading = false;
		}

		public void OnFontChanged()
		{
			if (_loading) return;

			var font = (Font)TextFormatEditor.ItemFont.Tag;
			var linkSettings = TargetLinks.Select(link => link.Settings).OfType<LineBreakSettings>().ToList();
			foreach (var linkSetting in linkSettings)
				linkSetting.Font = font;

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
