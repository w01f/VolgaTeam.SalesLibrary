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
		public static Color DefaultReplaceColor = ColorTranslator.FromHtml("#0000FF");

		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + control.Width / 2, control.Y + control.Height / 2);
		}

		public static Point GetOffset(this Point point, int x, int y)
		{
			return new Point(point.X + x, point.Y + y);
		}

		public static Image Resize(this Image image, Size size)
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
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(
					image,
					0,
					0,
					newWidth,
					newHeight);
			}
			return newImage;
		}

		public static Image DrawPadding(this Image image, Padding padding)
		{
			Image newImage = new Bitmap(
				image.Width + padding.Left + padding.Right,
				image.Height + padding.Top + padding.Bottom
				);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(
					image,
					0 + padding.Left,
					0 + padding.Top,
					image.Width,
					image.Height);
			}
			return newImage;
		}

		public static Image DrawBorder(this Image image, int borderSize = 1, Color? borderColor = null)
		{
			if (!borderColor.HasValue)
				borderColor = Color.Black;
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var newWidth = originalWidth + borderSize * 2;
			var newHeight = originalHeight + borderSize * 2;
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(
					image,
					borderSize,
					borderSize,
					originalWidth,
					originalHeight);
				using (var pen = new Pen(borderColor.Value, borderSize))
					graphicsHandle.DrawRectangle(pen, borderSize / 2, borderSize / 2, originalWidth + borderSize,
						originalHeight + borderSize);
			}
			return newImage;
		}

		public static Image DrawShadow(this Image image, int shadowSize = 1, Color? shadowColor = null)
		{
			if (!shadowColor.HasValue)
				shadowColor = Color.Black;
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var newWidth = originalWidth + shadowSize;
			var newHeight = originalHeight + shadowSize;
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				var alphaIncreemet = 32 / shadowSize;
				var alpha = alphaIncreemet;
				for (var shadowX = 0; shadowX < shadowSize; shadowX++)
				{
					using (Brush brush = new SolidBrush(Color.FromArgb(alpha, shadowColor.Value)))
						graphicsHandle.FillRectangle(
							brush,
							shadowX,
							shadowSize - shadowX,
							originalWidth,
							originalHeight);
					alpha += alphaIncreemet;
				}

				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(
					image,
					shadowSize,
					0,
					originalWidth,
					originalHeight);
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
