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

		protected Font _font;
		public Font Font
		{
			get => _font ?? DefaultFont;
			set
			{
				if (_font != value)
					SettingsContainer.OnSettingsChanged();
				if (value != DefaultFont)
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
