using System;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class ThumbnailSettings : SettingsContainer
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

		private Image _image;
		public Image Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
					OnSettingsChanged();
				_image = value;
			}
		}

		private string _sourcePath;
		public string SourcePath
		{
			get { return _sourcePath; }
			set
			{
				if (_sourcePath != value)
					OnSettingsChanged();
				_sourcePath = value;
			}
		}

		private int _imageWidth = 300;
		public int ImageWidth
		{
			get { return _imageWidth; }
			set
			{
				if (_imageWidth != value)
					OnSettingsChanged();
				_imageWidth = value;
			}
		}

		private int _imagePadding = 10;
		public int ImagePadding
		{
			get { return _imagePadding; }
			set
			{
				if (_imagePadding != value)
					OnSettingsChanged();
				_imagePadding = value;
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

		private ThumbnailTextMode _textMode;
		public ThumbnailTextMode TextMode
		{
			get { return _textMode; }
			set
			{
				if (_textMode != value)
					OnSettingsChanged();
				_textMode = value;
			}
		}

		private ThumbnailTextPosition _textPosition;
		public ThumbnailTextPosition TextPosition
		{
			get { return _textPosition; }
			set
			{
				if (_textPosition != value)
					OnSettingsChanged();
				_textPosition = value;
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

		private HorizontalAlignment _textAlignement = HorizontalAlignment.Left;
		public HorizontalAlignment TextAlignement
		{
			get { return _textAlignement; }
			set
			{
				if (_textAlignement != value)
					OnSettingsChanged();
				_textAlignement = value;
			}
		}

		[JsonIgnore]
		public bool TextEnabled => TextMode == ThumbnailTextMode.LinkName || TextMode == ThumbnailTextMode.CustomText;

		[JsonIgnore]
		public string DisplayedText => TextMode == ThumbnailTextMode.CustomText ?
			Text :
			(Parent as BaseLibraryLink)?.Name;

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_textMode = ThumbnailTextMode.NoText;
		}
	}
}
