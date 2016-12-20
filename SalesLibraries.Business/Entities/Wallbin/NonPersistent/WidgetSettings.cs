using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class WidgetSettings : SettingsContainer
	{
		private WidgetType _widgetType;
		public WidgetType WidgetType
		{
			get { return _widgetType; }
			set
			{
				if (_widgetType != value)
					OnSettingsChanged();
				_widgetType = value;
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

		protected Image _image;
		public virtual Image Image
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
				if (WidgetType == WidgetType.CustomWidget && Inverted)
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

		[JsonIgnore]
		public bool Enabled => _widgetType == WidgetType.CustomWidget;

		[JsonIgnore]
		public bool Disabled => _widgetType == WidgetType.NoWidget;

		[JsonIgnore]
		public virtual WidgetType DefaultWidgetType => WidgetType.NoWidget;

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_widgetType = DefaultWidgetType;
		}
	}
}
