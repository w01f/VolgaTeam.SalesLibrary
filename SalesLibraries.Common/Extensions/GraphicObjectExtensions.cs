using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Vintasoft.Imaging;
using Vintasoft.Imaging.ImageProcessing.Color;

namespace SalesLibraries.Common.Extensions
{
	public static class GraphicObjectExtensions
	{
		public static Color DefaultInversionColor = Color.White;

		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + (control.Width / 2), control.Y + (control.Height / 2));
		}

		public static Point GetOffset(this Point point, int x, int y)
		{
			return new Point(point.X + x, point.Y + y);
		}

		public static Image Resize(this Image image, Size size, Padding padding = new Padding())
		{
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var percentWidth = originalWidth != size.Width ? (float)size.Width / originalWidth : float.PositiveInfinity;
			var percentHeight = originalHeight != size.Height ? (float)size.Height / originalHeight : float.PositiveInfinity;
			var percent = Math.Min(percentHeight, percentWidth);
			if (float.IsInfinity(percent))
				percent = 1;
			var newWidth = (int)(originalWidth * percent);
			var newHeight = (int)(originalHeight * percent);
			Image newImage = new Bitmap(newWidth + padding.Left + padding.Right, newHeight + padding.Top + padding.Bottom);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(
					image,
					0 + padding.Left,
					0 + padding.Top,
					newWidth,
					newHeight);
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

		public static Image ReplaceColor(this Image image, Color color)
		{
			using (var vintasoftImage = new VintasoftImage(image, true))
			{
				var command = new ReplaceColorCommand(Color.Black, color, 512);
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
	}
}
