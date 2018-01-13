using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Extensions;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class BannerSettings : SettingsContainer
	{
		private bool _enable;
		public bool Enable
		{
			get => _enable;
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
			get => _inverted;
			set
			{
				if (_inverted != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_inverted = value;
			}
		}

		private Color _inversionColor = GraphicObjectExtensions.DefaultReplaceColor;
		public Color InversionColor
		{
			get => _inversionColor;
			set
			{
				if (_inversionColor != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_inversionColor = value;
			}
		}

		private Image _image;
		public Image Image
		{
			get => _image;
			set
			{
				if (_image != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_image = value;
			}
		}

		private string _imageName;
		public string ImageName
		{
			get => _imageName;
			set
			{
				if (_imageName != value)
					OnSettingsChanged();
				_imageName = value;
			}
		}

		private Image _displayedImage;
		[JsonIgnore]
		public Image DisplayedImage
		{
			get
			{
				if (Image == null) return null;
				if (_displayedImage != null)
					return _displayedImage;
				_displayedImage = (Image)Image.Clone();
				if (ImageScaleFactor > 0 && ImageScaleFactor < 100)
				{
					var size = new Size(_displayedImage.Width * ImageScaleFactor/100, _displayedImage.Height * ImageScaleFactor / 100);
					_displayedImage = _displayedImage.Resize(size);
				}
				if (Inverted)
					_displayedImage = _displayedImage.ReplaceColor(InversionColor);
				_displayedImage = _displayedImage.DrawPadding(new Padding(ImagePaddingLeft, ImagePaddingTop, ImagePaddingRight, ImagePaddingBottom));
				return _displayedImage;
			}
		}

		private BannerTextMode _textMode;
		public BannerTextMode TextMode
		{
			get => _textMode;
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
			get => _imageAlignement;
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
			get => _imageVerticalAlignement;
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
			get => _text;
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
			get => _foreColor;
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
			get => _font;
			set
			{
				if (_font != value)
					OnSettingsChanged();
				_font = value;
			}
		}

		private int _imagePaddingLeft = 2;
		public int ImagePaddingLeft
		{
			get => _imagePaddingLeft;
			set
			{
				if (_imagePaddingLeft != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingLeft = value;
			}
		}

		private int _imagePaddingTop = 2;
		public int ImagePaddingTop
		{
			get => _imagePaddingTop;
			set
			{
				if (_imagePaddingTop != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingTop = value;
			}
		}

		private int _imagePaddingRight = 2;
		public int ImagePaddingRight
		{
			get => _imagePaddingRight;
			set
			{
				if (_imagePaddingRight != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingRight = value;
			}
		}

		private int _imagePaddingBottom = 2;
		public int ImagePaddingBottom
		{
			get => _imagePaddingBottom;
			set
			{
				if (_imagePaddingBottom != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingBottom = value;
			}
		}

		private int _imageScaleFactor = 100;
		public int ImageScaleFactor
		{
			get => _imageScaleFactor;
			set
			{
				if (_imageScaleFactor != value)
				{
					_displayedImage = null;
					OnSettingsChanged();
				}
				_imageScaleFactor = value;
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
