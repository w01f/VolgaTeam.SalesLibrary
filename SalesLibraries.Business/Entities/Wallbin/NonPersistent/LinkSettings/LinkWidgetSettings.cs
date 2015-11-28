using System;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LinkWidgetSettings : WidgetSettings
	{
		[JsonIgnore]
		protected LibraryFileLink ParentFileLink
		{
			get { return Parent as LibraryFileLink; }
		}

		public override Image Image
		{
			get
			{
				if (Enable && _image != null)
					return _image;
				return GetAutoWidget();
			}
			set
			{
				if (_image != value)
					OnSettingsChanged();
				_image = value;
			}
		}

		[JsonIgnore]
		public bool HasAutoWidget
		{
			get { return GetAutoWidget() != null; }
		}

		private Image GetAutoWidget()
		{
			if (ParentFileLink == null) return null;
			return ParentFileLink.ParentLibrary.Settings.AutoWidgets
					.Where(autoWidget =>
						String.Compare(autoWidget.Extension, ParentFileLink.Extension.Replace(".", String.Empty), StringComparison.OrdinalIgnoreCase) == 0)
					.Select(a => a.Widget).FirstOrDefault();
		}
	}
}
