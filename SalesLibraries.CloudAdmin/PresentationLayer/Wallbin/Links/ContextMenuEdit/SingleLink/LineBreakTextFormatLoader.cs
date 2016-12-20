using System.Drawing;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	class LineBreakTextFormatLoader : BaseLineBreakLoader
	{
		private LineBreakTextFormatEditor TextFormatEditor => (LineBreakTextFormatEditor)_editor;

		private LineBreakSettings Settings => (LineBreakSettings)TargetLink.Settings;

		public LineBreakTextFormatLoader(BaseContextMenuEditor editor, LineBreak targetLink) : base(editor, targetLink) { }

		protected override void SetMenuItemsViibility()
		{
			TextFormatEditor.ItemFont.Visibility = BarItemVisibility.Always;
			TextFormatEditor.ItemFontColor.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLink()
		{
			base.LoadLink();

			_loading = true;
			TextFormatEditor.ItemFont.Tag = Settings.Font;
			TextFormatEditor.ItemFont.EditValue = Utils.FontToString(Settings.Font);
			TextFormatEditor.ItemFontColor.EditValue = TargetLink.DisplayColor;
			_loading = false;
		}

		public void OnFontChanged()
		{
			if (_loading) return;

			Settings.Font = (Font)TextFormatEditor.ItemFont.Tag;

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
