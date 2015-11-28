using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Interop.Word;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.FileManager.Business.Services;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(WordPreviewContainer))]
	class WordPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var wordContainer = (WordPreviewContainer)previewContainer;

			var pdfDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Pdf);
			var updatePdf = !(Directory.Exists(pdfDestination) && Directory.GetFiles(pdfDestination).Any());
			if (updatePdf && !Directory.Exists(pdfDestination))
				Directory.CreateDirectory(pdfDestination);
			
			var pngDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Png);
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination).Any()) && 
				wordContainer.GenerateImages;
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);

			var pngPhoneDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.PngForMobile);
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination).Any()) &&
				wordContainer.GenerateImages;
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);

			var jpgDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Jpeg);
			var updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination).Any()) &&
				wordContainer.GenerateImages;
			if (updateJpg && !Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);

			var jpgPhoneDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.JpegForMobile);
			var updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination).Any()) &&
				wordContainer.GenerateImages;
			if (updateJpgPhone && !Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);

			var thumbsDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any()) &&
				wordContainer.GenerateImages;
			if (updateThumbs && !Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			var thumbsPhoneDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.ThumbnailsForMobile);
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination).Any()) &&
				wordContainer.GenerateImages;
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);

			var docxDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Word);
			var updateDocx = !(Directory.Exists(docxDestination) && Directory.GetFiles(docxDestination).Any());
			if (updateDocx && !Directory.Exists(docxDestination))
				Directory.CreateDirectory(docxDestination);

			var txtDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Text);
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any()) &&
				wordContainer.GenerateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			var updated = updatePdf || 
				updatePng || 
				updateJpg ||
				updateThumbs || 
				updateDocx ||
				updateTxt ||
				updatePngPhone ||
				updateJpgPhone ||
				updateThumbsPhone;
			
			if (!updated) return;
			
			try
			{
				if (!WordHelper.Instance.Connect()) return;
				MessageFilter.Register();

				var document = WordHelper.Instance.WordObject.Documents.Open(wordContainer.SourcePath);

				var pdfFileName = Path.Combine(pdfDestination, Path.ChangeExtension(Path.GetFileName(wordContainer.SourcePath), "pdf"));
				if (updatePdf)
					document.ExportAsFixedFormat(pdfFileName, WdExportFormat.wdExportFormatPDF);

				if (updateJpg || updatePng || updateThumbs)
					PdfHelper.ExportPdf(pdfFileName, pngDestination, jpgDestination, thumbsDestination);
				if (updateJpgPhone || updatePngPhone || updateThumbsPhone)
					PdfHelper.ExportPdfPhone(pdfFileName, pngPhoneDestination, jpgPhoneDestination, thumbsPhoneDestination);

				if (updateTxt)
				{
					var txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(wordContainer.SourcePath), "txt"));
					using (var sw = new StreamWriter(txtFileName, false))
					{
						sw.Write(document.Content.Text);
						sw.Flush();
					}
				}

				if (updateDocx)
				{
					WordHelper.Instance.WordObject.Browser.Target = WdBrowseTarget.wdBrowsePage;
					for (int i = 1; i <= document.ComputeStatistics(WdStatistic.wdStatisticPages); i++)
					{
						document.Bookmarks["\\page"].Range.Copy();

						var singlePageDocument = WordHelper.Instance.WordObject.Documents.Add();
						singlePageDocument.Activate();
						WordHelper.Instance.WordObject.Selection.Paste();
						WordHelper.Instance.WordObject.Selection.TypeBackspace();

						if (updateDocx)
							singlePageDocument.SaveAs(Path.Combine(docxDestination, string.Format("Page{0}.{1}", i, "docx" )), WdSaveFormat.wdFormatXMLDocument);

						singlePageDocument.Close();
						Utils.ReleaseComObject(singlePageDocument);
						document.Activate();
						WordHelper.Instance.WordObject.Browser.Next();
					}
				}

				document.Close(false);
				Utils.ReleaseComObject(document);
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
				WordHelper.Instance.Disconnect();
			}

			if (updated)
			{
				PngHelper.ConvertFiles(wordContainer.ContainerPath);
				previewContainer.MarkAsModified();
			}
		}
	}
}
