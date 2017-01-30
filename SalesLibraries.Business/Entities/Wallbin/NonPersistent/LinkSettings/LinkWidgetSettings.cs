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
		public override WidgetType DefaultWidgetType => !String.IsNullOrEmpty(ParentObjectLink?.AutoWidgetKey) ?
			WidgetType.AutoWidget :
			WidgetType.NoWidget;

		[JsonIgnore]
		protected LibraryObjectLink ParentObjectLink => Parent as LibraryObjectLink;

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

		private Image _currentAutoWidget;
		private string _currentAutoWidgetKey;
		[JsonIgnore]
		public Image AutoWidget
		{
			get
			{
				if (ParentObjectLink == null)
					return null;
				if (ParentFileLink != null && ParentFileLink.IsDead)
					return null;
				var autoWidget = ParentObjectLink?.ParentLibrary.Settings.AutoWidgets
					.FirstOrDefault(widget => String.Equals(widget.Extension, ParentObjectLink.AutoWidgetKey, StringComparison.OrdinalIgnoreCase));
				if (autoWidget != null)
				{
					if (_currentAutoWidget == null || _currentAutoWidgetKey != autoWidget.WidgetKey)
					{
						_currentAutoWidgetKey = autoWidget.WidgetKey;
						_currentAutoWidget = (Image)autoWidget.DisplayedImage?.Clone();
					}
				}
				else
				{
					_currentAutoWidget = null;
					_currentAutoWidgetKey = null;
				}
				return _currentAutoWidget;
			}
		}
	}
}
