using System;
using System.Drawing;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
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
				{
					_invertedImage = null;
					OnSettingsChanged();
				}
				_inverted = value;
			}
		}

		private Color _inversionColor = GraphicObjectExtensions.DefaultInversionColor;
		public Color InversionColor
		{
			get { return _inversionColor; }
			set
			{
				if (_inversionColor != value)
				{
					_invertedImage = null;
					OnSettingsChanged();
				}
				_inversionColor = value;
			}
		}

		private Image _image;
		public Image Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
				{
					_invertedImage = null;
					OnSettingsChanged();
				}
				_image = value;
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
					{
						_invertedImage = InversionColor != GraphicObjectExtensions.DefaultInversionColor ?
							((Image)Image?.Clone()).ReplaceColor(InversionColor) :
							((Image)Image?.Clone()).Invert();
					}
					return _invertedImage;
				}
				return Image;
			}
		}

		private BannerTextMode _textMode;
		public BannerTextMode TextMode
		{
			get { return _textMode; }
			set
			{
				if (_textMode != value)
					OnSettingsChanged();
				_textMode = value;
			}
		}

		private HorizontalAlignment _imageAlignement = HorizontalAlignment.Left;
		public HorizontalAlignment ImageAlignement
		{
			get { return _imageAlignement; }
			set
			{
				if (_imageAlignement != value)
					OnSettingsChanged();
				_imageAlignement = value;
			}
		}

		private VerticalAlignment _imageVerticalAlignement = VerticalAlignment.Middle;
		public VerticalAlignment ImageVerticalAlignement
		{
			get { return _imageVerticalAlignement; }
			set
			{
				if (_imageVerticalAlignement != value)
					OnSettingsChanged();
				_imageVerticalAlignement = value;
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

		[JsonIgnore]
		public bool TextEnabled => TextMode == BannerTextMode.LinkName || TextMode == BannerTextMode.CustomText;

		[JsonIgnore]
		public string DisplayedText => TextMode == BannerTextMode.CustomText ?
			Text :
			(Parent as BaseLibraryLink)?.Name;


		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_textMode = BannerTextMode.NoText;
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			if (Font.Unit != GraphicsUnit.Point)
				_font = new Font(Font.FontFamily, Font.Size, Font.Style, GraphicsUnit.Point);
			if (TextMode == BannerTextMode.Undefined)
				_textMode = !String.IsNullOrEmpty(Text) ? BannerTextMode.CustomText : BannerTextMode.NoText;
		}

		public BannerSettings SaveAsTemplate()
		{
			var templateBanner = SettingsContainer.CreateInstance<BannerSettings>(Parent, null);
			templateBanner.Enable = Enable;
			templateBanner.Inverted = Inverted;
			templateBanner.TextMode = TextMode;
			templateBanner.ImageAlignement = ImageAlignement;
			templateBanner.ImageVerticalAlignement = ImageVerticalAlignement;
			templateBanner.ForeColor = ForeColor;
			templateBanner.Font = Font;
			return templateBanner;
		}
	}
}
