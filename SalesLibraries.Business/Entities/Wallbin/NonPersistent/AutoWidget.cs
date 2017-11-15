using System;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class AutoWidget
	{
		public string Extension { get; set; }

		private bool _inverted;
		public bool Inverted
		{
			get { return _inverted; }
			set
			{
				if (_inverted != value)
				{
					WidgetKey = DateTime.Now.ToString("yyyyMMddhhmmss");
					_invertedImage = null;
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
					WidgetKey = DateTime.Now.ToString("yyyyMMddhhmmss");
					_invertedImage = null;
				}
				_inversionColor = value;
			}
		}

		private Image _widget;
		public Image Widget
		{
			get { return _widget; }
			set
			{
				if (_widget != value)
				{
					WidgetKey = DateTime.Now.ToString("yyyyMMddhhmmss");
					_invertedImage = null;
				}
				_widget = value;
			}
		}

		public string WidgetName { get; set; }

		[JsonIgnore]
		public string WidgetKey { get; set; }

		private Image _invertedImage;
		[JsonIgnore]
		public Image DisplayedImage
		{
			get
			{
				if (Inverted)
				{
					if (_invertedImage == null)
						_invertedImage = ((Image)Widget?.Clone()).ReplaceColor(InversionColor);
					return _invertedImage;
				}
				return Widget;
			}
		}
	}
}
