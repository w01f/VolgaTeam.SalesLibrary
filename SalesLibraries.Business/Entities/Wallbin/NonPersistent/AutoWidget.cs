using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class AutoWidget
	{
		public string Extension { get; set; }
		public bool Inverted { get; set; }

		public Image Widget
		{
			get { return _widget; }
			set
			{
				_widget = value;
				_invertedImage = null;
			}
		}

		private Image _invertedImage;
		private Image _widget;

		[JsonIgnore]
		public Image DisplayedImage
		{
			get
			{
				if (Inverted)
				{
					if (_invertedImage == null)
						_invertedImage = ((Image)Widget?.Clone()).Invert();
					return _invertedImage;
				}
				return Widget;
			}
		}
	}
}
