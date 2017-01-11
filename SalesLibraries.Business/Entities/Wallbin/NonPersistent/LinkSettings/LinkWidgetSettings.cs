using System;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LinkWidgetSettings : WidgetSettings
	{
		[JsonIgnore]
		public override WidgetType DefaultWidgetType => ParentFileLink != null ?
			WidgetType.AutoWidget :
			WidgetType.NoWidget;

		[JsonIgnore]
		protected LibraryFileLink ParentFileLink => Parent as LibraryFileLink;

		public override Image Image
		{
			get
			{
				switch (WidgetType)
				{
					case WidgetType.AutoWidget:
						return AutoWidget;
					case WidgetType.CustomWidget:
						return base.Image;
					default:
						return null;
				}
			}
			set
			{
				if (_image != value)
					OnSettingsChanged();
				_image = value;
			}
		}

		[JsonIgnore]
		public bool HasAutoWidget => AutoWidget != null;

		[JsonIgnore]
		public Image AutoWidget
		{
			get
			{
				if (ParentFileLink == null)
					return null;
				if (ParentFileLink.IsDead)
					return null;
				return ParentFileLink?.ParentLibrary.Settings.AutoWidgets
					.Where(autoWidget =>
						String.Compare(autoWidget.Extension, ParentFileLink.AutoWidgetKey, StringComparison.OrdinalIgnoreCase) == 0)
					.Select(a => a.DisplayedImage).FirstOrDefault();
			}
		}
	}
}
