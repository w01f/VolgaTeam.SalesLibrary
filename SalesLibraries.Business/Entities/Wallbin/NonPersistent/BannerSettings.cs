using System;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class BannerSettings : SettingsContainer
	{
		private bool _enable;
		public bool Enable
		{
			get { return _enable; }
			set
			{
				if (_enable != value)
					OnSettingsChanged();
				_enable = value;
			}
		}

		private bool _inverted;
		public bool Inverted
		{
			get { return _inverted; }
			set
			{
				if (_inverted != value)
					OnSettingsChanged();
				_inverted = value;
			}
		}

		private Image _image;
		public Image Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
					OnSettingsChanged();
				_image = value;
				_invertedImage = null;
			}
		}

		private Image _invertedImage;
		[JsonIgnore]
		public Image DisplayedImage
		{
			get
			{
				if (Inverted)
				{
					if (_invertedImage == null)
						_invertedImage = ((Image)Image?.Clone()).Invert();
					return _invertedImage;
				}
				return Image;
			}
		}

		private bool _showText;
		public bool ShowText
		{
			get { return _showText; }
			set
			{
				if (_showText != value)
					OnSettingsChanged();
				_showText = value;
			}
		}

		private Alignment _imageAlignement = Alignment.Left;
		public Alignment ImageAlignement
		{
			get { return _imageAlignement; }
			set
			{
				if (_imageAlignement != value)
					OnSettingsChanged();
				_imageAlignement = value;
			}
		}

		private string _text = String.Empty;
		public string Text
		{
			get { return _text; }
			set
			{
				if (_text != value)
					OnSettingsChanged();
				_text = value;
			}
		}

		private Color _foreColor = Color.Black;
		public Color ForeColor
		{
			get { return _foreColor; }
			set
			{
				if (_foreColor != value)
					OnSettingsChanged();
				_foreColor = value;
			}
		}

		private Font _font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
		public Font Font
		{
			get { return _font; }
			set
			{
				if (_font != value)
					OnSettingsChanged();
				_font = value;
			}
		}

		protected override void AfterCreate()
		{
			if (Font.Unit != GraphicsUnit.Point)
				Font = new Font(Font.FontFamily, Font.Size, Font.Style, GraphicsUnit.Point);
		}
	}
}
