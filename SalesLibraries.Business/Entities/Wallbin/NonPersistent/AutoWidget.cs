using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class AutoWidget
	{
		public string Extension { get; set; }
		public Image Widget { get; set; }
		public bool Inverted { get; set; }

		private Image _invertedImage;
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
