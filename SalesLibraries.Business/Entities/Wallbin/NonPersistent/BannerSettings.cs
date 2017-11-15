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

		private Color _inversionColor = GraphicObjectExtensions.DefaultReplaceColor;
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

		private string _imageName;
		public string ImageName
		{
			get { return _imageName; }
			set
			{
				if (_imageName != value)
					OnSettingsChanged();
				_imageName = value;
			}
		}

		private Image _invertedImage;
		[JsonIgnore]
		public Image DisplayedImage
		{
			get
			{
				if (Image == null) return null;
				if (Inverted)
				{
					if (_invertedImage == null)
						_invertedImage = ((Image)Image?.Clone()).ReplaceColor(InversionColor).DrawPadding(new Padding(ImagePaddingLeft, ImagePaddingTop, ImagePaddingRight, ImagePaddingBottom));
					return _invertedImage;
				}
				return ((Image)Image?.Clone()).DrawPadding(new Padding(ImagePaddingLeft, ImagePaddingTop, ImagePaddingRight, ImagePaddingBottom));
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

		private int _imagePaddingLeft = 2;
		public int ImagePaddingLeft
		{
			get { return _imagePaddingLeft; }
			set
			{
				if (_imagePaddingLeft != value)
				{
					_invertedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingLeft = value;
			}
		}

		private int _imagePaddingTop = 2;
		public int ImagePaddingTop
		{
			get { return _imagePaddingTop; }
			set
			{
				if (_imagePaddingTop != value)
				{
					_invertedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingTop = value;
			}
		}

		private int _imagePaddingRight = 2;
		public int ImagePaddingRight
		{
			get { return _imagePaddingRight; }
			set
			{
				if (_imagePaddingRight != value)
				{
					_invertedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingRight = value;
			}
		}

		private int _imagePaddingBottom = 2;
		public int ImagePaddingBottom
		{
			get { return _imagePaddingBottom; }
			set
			{
				if (_imagePaddingBottom != value)
				{
					_invertedImage = null;
					OnSettingsChanged();
				}
				_imagePaddingBottom = value;
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
