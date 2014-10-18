using System.Drawing;
using System.Drawing.Drawing2D;

namespace SalesDepot.CoreObjects.ToolClasses
{
	public class ImageHelper
	{
		public static Image GetThumbnail(Image source, int newHeight, int newWidth)
		{
			try
			{
				var newImage = new Bitmap(newWidth, newHeight);
				using (var gr = Graphics.FromImage(newImage))
				{
					gr.SmoothingMode = SmoothingMode.AntiAlias;
					gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
					gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
					gr.DrawImage(source, new Rectangle(0, 0, newWidth, newHeight));
				}
				return newImage;
			}
			catch
			{
				return null;
			}
		}
	}
}
