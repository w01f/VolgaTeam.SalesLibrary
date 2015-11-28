using System.Drawing;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class WidgetSettings : SettingsContainer
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
	}
}
