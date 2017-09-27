using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ceTe.DynamicPDF.Rasterizer;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using SalesLibraries.Common.Configuration;
using Path = System.IO.Path;

namespace SalesLibraries.FileManager.Business.Services
{
	static class PdfHelper
	{
		public static void ExportPdf(string sourceFilePath, string destinationPngPath, string destinationThumbsPath, bool onlyOneFile)
		{
			var rasterizer = new PdfRasterizer(sourceFilePath);
			// Save the image.
			var pngImageFormat = new PngImageFormat(PngColorFormat.Rgb);
			var imageSizeFormat = new PercentageImageSize(100);
			var thumbsSizeFormat = new PercentageImageSize(10);
			if (onlyOneFile && rasterizer.Pages.Count > 0)
			{
				if (!String.IsNullOrEmpty(destinationPngPath))
					rasterizer.Pages[0].Draw(Path.Combine(destinationPngPath, Constants.SinglePreviewFilePrefixName + "Page" + ".png"), pngImageFormat, imageSizeFormat);
				if (!String.IsNullOrEmpty(destinationThumbsPath))
					rasterizer.Pages[0].Draw(Path.Combine(destinationThumbsPath, Constants.SinglePreviewFilePrefixName + "Page" + ".png"), pngImageFormat, thumbsSizeFormat);
			}
			else
				for (var i = 0; i < rasterizer.Pages.Count; i++)
				{
					if (!String.IsNullOrEmpty(destinationPngPath))
						rasterizer.Pages[i].Draw(Path.Combine(destinationPngPath, "Page" + (i + 1) + ".png"), pngImageFormat, imageSizeFormat);
					if (!String.IsNullOrEmpty(destinationThumbsPath))
						rasterizer.Pages[i].Draw(Path.Combine(destinationThumbsPath, "Page" + (i + 1) + ".png"), pngImageFormat, thumbsSizeFormat);
				}
			rasterizer.Dispose();
		}

		public static void ExportPdfPhone(string sourceFilePath, string destinationPngPath, string destinationThumbsPath, bool onlyOneFile)
		{
			var rasterizer = new PdfRasterizer(sourceFilePath);
			// Save the image.
			var pngImageFormat = new PngImageFormat(PngColorFormat.Rgb);
			var imageSizeFormat = new PercentageImageSize(60);
			var thumbsSizeFormat = new PercentageImageSize(30);

			if (onlyOneFile && rasterizer.Pages.Count > 0)
			{
				if (!String.IsNullOrEmpty(destinationPngPath))
					rasterizer.Pages[0].Draw(Path.Combine(destinationPngPath, Constants.SinglePreviewFilePrefixName + "Page" + ".png"), pngImageFormat, imageSizeFormat);
				if (!String.IsNullOrEmpty(destinationThumbsPath))
					rasterizer.Pages[0].Draw(Path.Combine(destinationThumbsPath, Constants.SinglePreviewFilePrefixName + "Page" + ".png"), pngImageFormat, thumbsSizeFormat);
			}
			else
				for (var i = 0; i < rasterizer.Pages.Count; i++)
				{
					if (!String.IsNullOrEmpty(destinationPngPath))
						rasterizer.Pages[i].Draw(Path.Combine(destinationPngPath, "Page" + (i + 1) + ".png"), pngImageFormat, imageSizeFormat);
					if (!String.IsNullOrEmpty(destinationThumbsPath))
						rasterizer.Pages[i].Draw(Path.Combine(destinationThumbsPath, "Page" + (i + 1) + ".png"), pngImageFormat, thumbsSizeFormat);
				}
			rasterizer.Dispose();
		}

		public static void ExtractText(string sourceFilePath, string destinationPath)
		{
			var text = new StringBuilder();
			var pdfReader = new PdfReader(sourceFilePath);
			for (int page = 1; page <= pdfReader.NumberOfPages; page++)
			{
				ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
				var currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
				currentText = Encoding.UTF8.GetString(
					Encoding.Convert(
						Encoding.Default,
						Encoding.UTF8,
						Encoding.Default.GetBytes(currentText))
					);
				text.Append(currentText);
			}
			pdfReader.Close();

			var temp = text.ToString();
			var regexp = new Regex(@"[ ]{2,}", RegexOptions.None);
			temp = regexp.Replace(temp, @" ");
			temp = Regex.Replace(temp, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

			File.WriteAllText(destinationPath, temp);
		}
	}
}
