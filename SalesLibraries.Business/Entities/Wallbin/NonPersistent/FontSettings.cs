using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class FontSettings
	{
		[JsonIgnore]
		public static Font DefaultFont { get; } = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);

		public SettingsContainer SettingsContainer { get; set; }

		private Font _font;
		public Font Font
		{
			get => _font ?? DefaultFont;
			set
			{
				if (value == null)
				{
					_font = null;
					SettingsContainer.OnSettingsChanged();
					return;
				}
				if (_font == null || _font.FontFamily?.Name != value.FontFamily?.Name || _font.Size != value.Size || _font.Bold != value.Bold || _font.Style != value.Style)
					SettingsContainer.OnSettingsChanged();
				if (DefaultFont.FontFamily?.Name != value.FontFamily?.Name || DefaultFont.Size != value.Size || DefaultFont.Bold != value.Bold || DefaultFont.Style != value.Style)
					_font = value;
			}
		}

		private Color? _color;
		public Color? Color
		{
			get => _color;
			set
			{
				if (_color != value)
					SettingsContainer.OnSettingsChanged();
				_color = value;
			}
		}
	}
}
