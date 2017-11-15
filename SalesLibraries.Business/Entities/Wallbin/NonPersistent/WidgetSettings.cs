using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
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

		private Color _inversionColor = GraphicObjectExtensions.DefaultReplaceColor;
		public Color InversionColor
		{
			get
			{
				if (WidgetHolder != null && WidgetHolder.UseTextColorForWidget)
					return WidgetHolder.TextColor;
				return _inversionColor;
			}
			set
			{
				if (WidgetHolder != null && WidgetHolder.UseTextColorForWidget)
					return;
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
				if (WidgetType == WidgetType.CustomWidget && Inverted)
				{
					if (_invertedImage == null)
						_invertedImage = ((Image)Image?.Clone()).ReplaceColor(InversionColor);
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

		[JsonIgnore]
		public IWidgetSetingsHolder WidgetHolder => Parent as IWidgetSetingsHolder;

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_widgetType = DefaultWidgetType;
		}

		public void ResetImage()
		{
			_invertedImage = null;
		}
	}
}
