using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Vintasoft.Imaging;
using Vintasoft.Imaging.ImageProcessing.Color;

namespace SalesLibraries.Common.Extensions
{
	public static class GraphicObjectExtensions
	{
		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + (control.Width / 2), control.Y + (control.Height / 2));
		}

		public static Point GetOffset(this Point point, int x, int y)
		{
			return new Point(point.X + x, point.Y + y);
		}

		public static Image Resize(this Image image, Size size)
		{
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var percentWidth = (float)size.Width / originalWidth;
			var percentHeight = (float)size.Height / originalHeight;
			var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
			var newWidth = (int)(originalWidth * percent);
			var newHeight = (int)(originalHeight * percent);
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
			}
			return newImage;
		}

		public static Image DrawBorder(this Image image)
		{
			const int borderWidth = 1;
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var newWidth = (originalWidth + borderWidth * 4);
			var newHeight = (originalHeight + borderWidth * 4);
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			using (var pen = new Pen(Color.DimGray, borderWidth))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(image, borderWidth, borderWidth);
				graphicsHandle.DrawRectangle(pen, 0, 0, originalWidth, originalHeight);
			}
			return newImage;
		}

		public static Image Invert(this Image image)
		{
			using (var vintasoftImage = new VintasoftImage(image, true))
			{
				var command = new InvertCommand();
				try
				{
					command.ExecuteInPlace(vintasoftImage);
				}
				catch
				{
				}
				return vintasoftImage.GetAsBitmap();
			}
		}

		public static ImageFormat GetImageFormat(Image image)
		{
			var img = image.RawFormat;
			if (img.Equals(ImageFormat.Jpeg))
				return ImageFormat.Jpeg;
			if (img.Equals(ImageFormat.Bmp))
				return ImageFormat.Bmp;
			if (img.Equals(ImageFormat.Png))
				return ImageFormat.Png;
			if (img.Equals(ImageFormat.Emf))
				return ImageFormat.Emf;
			if (img.Equals(ImageFormat.Exif))
				return ImageFormat.Exif;
			if (img.Equals(ImageFormat.Gif))
				return ImageFormat.Gif;
			if (img.Equals(ImageFormat.Icon))
				return ImageFormat.Icon;
			if (img.Equals(ImageFormat.MemoryBmp))
				return ImageFormat.MemoryBmp;
			if (img.Equals(ImageFormat.Tiff))
				return ImageFormat.Tiff;
			return ImageFormat.Wmf;
		}
	}
}
