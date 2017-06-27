using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Office.Interop.Word;
using SalesLibraries.Business.Entities.Helpers;
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

			var log = new StringBuilder();
			log.AppendLine(String.Format("Process started at {0:hh:mm:ss tt zz}", DateTime.Now));

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

			var thumbsDatatableDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any()) &&
				wordContainer.GenerateImages;
			if (updateThumbsDatatable && !Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			var docxDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Word);
			var updateDocx = !(Directory.Exists(docxDestination) && Directory.GetFiles(docxDestination).Any());
			if (updateDocx && !Directory.Exists(docxDestination))
				Directory.CreateDirectory(docxDestination);

			var txtDestination = Path.Combine(wordContainer.ContainerPath, PreviewFormats.Text);
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any()) &&
				wordContainer.GenerateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			var needToUpdate = updatePdf ||
				updatePng ||
				updateThumbs ||
				updateDocx ||
				updateTxt ||
				updatePngPhone ||
				updateThumbsPhone ||
				updateThumbsDatatable;

			if (!needToUpdate) return;
			var updated = false;
			var tryCount = 0;
			do
			{
				using (var wordProcessor = new WordHidden())
				{
					try
					{
						if (!wordProcessor.Connect()) continue;

						MessageFilter.Register();

						var sourceFileName = Path.GetFileName(wordContainer.SourcePath);
						var sourceFilePath = wordContainer.SourcePath;
						var document = wordProcessor.WordObject.Documents.Open(sourceFilePath);
						document.Final = false;

						var pdfFileName = Path.Combine(pdfDestination, Path.ChangeExtension(sourceFileName, "pdf"));
						if (updatePdf)
						{
							document.ExportAsFixedFormat(pdfFileName, WdExportFormat.wdExportFormatPDF);
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Pdf, DateTime.Now));
						}

						if (updatePng || updateThumbs || updateThumbsDatatable)
						{
							PdfHelper.ExportPdf(pdfFileName, pngDestination, thumbsDestination);
							JpegGenerator.GenerateDatatableJpegs(pngDestination, thumbsDatatableDestination);
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Png, DateTime.Now));
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Thumbnails, DateTime.Now));
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.ThumbnailsForDatatable, DateTime.Now));
						}
						if (updatePngPhone || updateThumbsPhone)
						{
							PdfHelper.ExportPdfPhone(pdfFileName, pngPhoneDestination, thumbsPhoneDestination);
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.PngForMobile, DateTime.Now));
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.ThumbnailsForMobile, DateTime.Now));
						}

						if (updateTxt)
						{
							var txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(sourceFileName, "txt"));
							using (var sw = new StreamWriter(txtFileName, false))
							{
								sw.Write(document.Content.Text);
								sw.Flush();
							}
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Text, DateTime.Now));
						}

						if (updateDocx)
						{
							var documentSlitted = false;
							wordProcessor.WordObject.Browser.Target = WdBrowseTarget.wdBrowsePage;
							var pageCount = document.ComputeStatistics(WdStatistic.wdStatisticPages);
							try
							{
								for (var i = 1; i <= pageCount; i++)
								{
									document.Bookmarks["\\page"].Range.Copy();

									var singlePageDocument = wordProcessor.WordObject.Documents.Add();
									singlePageDocument.Activate();
									wordProcessor.WordObject.Selection.Paste();
									wordProcessor.WordObject.Selection.TypeBackspace();

									singlePageDocument.SaveAs(Path.Combine(docxDestination, string.Format("Page{0}.{1}", i, "docx")), WdSaveFormat.wdFormatXMLDocument);

									singlePageDocument.Close();
									Utils.ReleaseComObject(singlePageDocument);
									document.Activate();
									wordProcessor.WordObject.Browser.Next();
								}
								documentSlitted = true;
							}
							catch { }
							if (!documentSlitted)
								for (var i = 1; i <= pageCount; i++)
									document.SaveAs(Path.Combine(docxDestination, string.Format("Page{0}.{1}", i, "docx")), WdSaveFormat.wdFormatXMLDocument);
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Word, DateTime.Now));
						}
						document.Close(false);
						Utils.ReleaseComObject(document);
						updated = true;
					}
					catch
					{
					}
					finally
					{
						tryCount++;
						MessageFilter.Revoke();
					}
				}

			} while (!updated && tryCount < 10);


			if (needToUpdate)
			{
				PngHelper.ConvertFiles(wordContainer.ContainerPath);
				previewContainer.MarkAsModified();
			}

			log.AppendLine(String.Format("Process finished at {0:hh:mm:ss tt zz}", DateTime.Now));
			if (Directory.Exists(previewContainer.ContainerPath))
				File.WriteAllText(Path.Combine(previewContainer.ContainerPath, String.Format("log_{0:MMddyy_hhmmsstt}.txt", DateTime.Now)), log.ToString());
		}
	}
}
