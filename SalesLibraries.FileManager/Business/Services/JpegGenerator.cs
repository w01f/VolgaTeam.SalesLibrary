using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.FileManager.Business.Services
{
	static class JpegGenerator
	{
		public static void GenerateDatatableJpegs(string sourcePath, string destinationPath)
		{
			foreach (var pngFilePath in Directory.GetFiles(sourcePath, "*.png"))
			{
				var jpegFilePath = Path.Combine(destinationPath,
					String.Format("{0}.jpg", Path.GetFileNameWithoutExtension(pngFilePath)));
				var originalBitmap = Image.FromFile(pngFilePath);
				originalBitmap = originalBitmap.Resize(new Size(originalBitmap.Width, 200));
				using (var newBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, PixelFormat.Format24bppRgb))
				{
					using (var gr = Graphics.FromImage(newBitmap))
						gr.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height));
					newBitmap.SetResolution(72.0F, 72.0F);
					newBitmap.Save(jpegFilePath, ImageFormat.Jpeg);
				}
				originalBitmap.Dispose();
			}
		}
	}
}
