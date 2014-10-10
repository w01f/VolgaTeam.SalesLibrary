using System;
using System.Diagnostics;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace SalesDepot.CoreObjects.InteropClasses
{
	class WordHelper
	{
		private static readonly WordHelper _instance = new WordHelper();

		private Word.Application _wordObject;

		private WordHelper()
		{
		}

		public static WordHelper Instance
		{
			get
			{
				return _instance;
			}
		}

		public bool Connect()
		{
			bool result = false;
			MessageFilter.Register();
			try
			{
				_wordObject = new Microsoft.Office.Interop.Word.Application();
				_wordObject.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
				result = true;
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void Disconnect()
		{
			try
			{
				_wordObject.Quit(false);
			}
			catch
			{
			}
			ToolClasses.Utils.ReleaseComObject(_wordObject);
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public void ExportDocumentAllFormats(string sourceFilePath, string destinationFolderPath, out bool update)
		{
			var pdfDestination = Path.Combine(destinationFolderPath, "pdf");
			var updatePdf = !(Directory.Exists(pdfDestination) && Directory.GetFiles(pdfDestination, "*.pdf").Length > 0);
			if (updatePdf && !Directory.Exists(pdfDestination))
				Directory.CreateDirectory(pdfDestination);
			var pngDestination = Path.Combine(destinationFolderPath, "png");
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0);
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);
			var pngPhoneDestination = Path.Combine(destinationFolderPath, "png_phone");
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination, "*.png").Length > 0);
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);
			var jpgDestination = Path.Combine(destinationFolderPath, "jpg");
			var updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0);
			if (updateJpg && !Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);
			var jpgPhoneDestination = Path.Combine(destinationFolderPath, "jpg_phone");
			var updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination, "*.jpg").Length > 0);
			if (updateJpgPhone && !Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);
			var thumbsDestination = Path.Combine(destinationFolderPath, "thumbs");
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination, "*.png").Length > 0);
			if (updateThumbs && !Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);
			var thumbsPhoneDestination = Path.Combine(destinationFolderPath, "thumbs_phone");
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination, "*.png").Length > 0);
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);
			var docDestination = Path.Combine(destinationFolderPath, "doc");
			var updateDoc = !(Directory.Exists(docDestination) && Directory.GetFiles(docDestination, "*.doc").Length > 0);
			if (updateDoc && !Directory.Exists(docDestination))
				Directory.CreateDirectory(docDestination);
			var docxDestination = Path.Combine(destinationFolderPath, "docx");
			var updateDocx = !(Directory.Exists(docxDestination) && Directory.GetFiles(docxDestination, "*.docx").Length > 0);
			if (updateDocx && !Directory.Exists(docxDestination))
				Directory.CreateDirectory(docxDestination);

			update = false;
			if (!updatePdf && !updatePng && !updateJpg && !updateThumbs && !updateDoc && !updateDocx && !updatePngPhone && !updateJpgPhone && !updateThumbsPhone) return;
			update = true;
			try
			{
				if (Connect())
				{
					MessageFilter.Register();

					Word.Document document = _wordObject.Documents.Open(FileName: sourceFilePath);

					string pdfFileName = Path.Combine(pdfDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "pdf"));
					if (updatePdf)
						document.ExportAsFixedFormat(OutputFileName: pdfFileName, ExportFormat: Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);

					if (updateJpg || updatePng || updateThumbs)
						ToolClasses.PdfHelper.Instance.ExportPdf(pdfFileName, pngDestination, jpgDestination, thumbsDestination);
					if (updateJpgPhone || updatePngPhone || updateThumbsPhone)
						ToolClasses.PdfHelper.Instance.ExportPdfPhone(pdfFileName, pngPhoneDestination, jpgPhoneDestination, thumbsPhoneDestination);

					if (updateDoc || updateDocx)
					{
						_wordObject.Browser.Target = Word.WdBrowseTarget.wdBrowsePage;
						for (int i = 1; i <= document.ComputeStatistics(Word.WdStatistic.wdStatisticPages); i++)
						{
							document.Bookmarks["\\page"].Range.Copy();

							Word.Document singlePageDocument = _wordObject.Documents.Add();
							singlePageDocument.Activate();
							_wordObject.Selection.Paste();
							_wordObject.Selection.TypeBackspace();

							if (updateDoc)
								singlePageDocument.SaveAs(Path.Combine(docDestination, string.Format("Page{0}.{1}", new string[] { i.ToString(), "doc" })), Word.WdSaveFormat.wdFormatDocument);
							if (updateDocx)
								singlePageDocument.SaveAs(Path.Combine(docxDestination, string.Format("Page{0}.{1}", new string[] { i.ToString(), "docx" })), Word.WdSaveFormat.wdFormatXMLDocument);

							((Word._Document)singlePageDocument).Close();
							ToolClasses.Utils.ReleaseComObject(singlePageDocument);
							document.Activate();
							_wordObject.Browser.Next();
						}
					}

					((Word._Document)document).Close(false);
					ToolClasses.Utils.ReleaseComObject(document);
				}
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
				Disconnect();
			}
		}
	}
}
