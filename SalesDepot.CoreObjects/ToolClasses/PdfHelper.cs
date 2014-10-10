using System.IO;
using ceTe.DynamicPDF.Rasterizer;

namespace SalesDepot.CoreObjects.ToolClasses
{
	class PdfHelper
	{
		private static PdfHelper _instance;

		public static PdfHelper Instance
		{
			get
			{
				if (_instance == null)
					_instance = new PdfHelper();
				return _instance;
			}
		}

		private PdfHelper()
		{
			PdfRasterizer.AddLicense("RST20NXDGKFKDLYMw0fsJRu1DsUT9rc8r8C+PEemp2t8HAswYEWFAkaX7h63jGM6Ip9hLi15A4aSVky01IXC9+Tga7jNykC8BW7g");
		}

		public void ExportPdf(string sourceFilePath, string destinationPngPath, string destinationJpgPath, string destinationThumbsPath)
		{
			var rasterizer = new PdfRasterizer(sourceFilePath);
			// Save the image.
			var pngImageFormat = new PngImageFormat(PngColorFormat.Rgb);
			var jpgImageFormat = new JpegImageFormat(90);
			var imageSizeFormat = new PercentageImageSize(100);
			var thumbsSizeFormat = new PercentageImageSize(10);
			for (var i = 0; i < rasterizer.Pages.Count; i++)
			{
				rasterizer.Pages[i].Draw(Path.Combine(destinationPngPath, "Page" + (i + 1) + ".png"), pngImageFormat, imageSizeFormat);
				rasterizer.Pages[i].Draw(Path.Combine(destinationJpgPath, "Page" + (i + 1) + ".JPG"), jpgImageFormat, imageSizeFormat);
				rasterizer.Pages[i].Draw(Path.Combine(destinationThumbsPath, "Page" + (i + 1) + ".png"), pngImageFormat, thumbsSizeFormat);
			}

			rasterizer.Dispose();
		}

		public void ExportPdfPhone(string sourceFilePath, string destinationPngPath, string destinationJpgPath, string destinationThumbsPath)
		{
			var rasterizer = new PdfRasterizer(sourceFilePath);
			// Save the image.
			var pngImageFormat = new PngImageFormat(PngColorFormat.Rgb);
			var jpgImageFormat = new JpegImageFormat(90);
			var imageSizeFormat = new PercentageImageSize(60);
			var thumbsSizeFormat = new PercentageImageSize(30);
			for (var i = 0; i < rasterizer.Pages.Count; i++)
			{
				rasterizer.Pages[i].Draw(Path.Combine(destinationPngPath, "Page" + (i + 1) + ".png"), pngImageFormat, imageSizeFormat);
				rasterizer.Pages[i].Draw(Path.Combine(destinationJpgPath, "Page" + (i + 1) + ".JPG"), jpgImageFormat, imageSizeFormat);
				rasterizer.Pages[i].Draw(Path.Combine(destinationThumbsPath, "Page" + (i + 1) + ".png"), pngImageFormat, thumbsSizeFormat);
			}

			rasterizer.Dispose();
		}
	}
}
