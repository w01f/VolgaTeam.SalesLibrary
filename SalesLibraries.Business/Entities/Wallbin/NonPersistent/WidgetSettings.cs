using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

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

		protected Image _image;
		public virtual Image Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
					OnSettingsChanged();
				_image = value;
			}
		}

		[JsonIgnore]
		public bool Enabled
		{
			get { return _widgetType == WidgetType.CustomWidget; }
		}

		[JsonIgnore]
		public bool Disabled
		{
			get { return _widgetType == WidgetType.NoWidget; }
		}

		[JsonIgnore]
		public virtual WidgetType DefaultWidgetType
		{
			get { return WidgetType.NoWidget; }
		}

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_widgetType = DefaultWidgetType;
		}
	}
}
