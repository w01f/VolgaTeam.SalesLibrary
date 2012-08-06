using System;
using System.Diagnostics;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace FileManager.InteropClasses
{
    class WordHelper
    {
        private static WordHelper _instance = new WordHelper();

        private Word.Application _wordObject;
        private bool _isFirstLaunch = false;

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
            AppManager.Instance.ReleaseComObject(_wordObject);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExportDocumentAllFormats(string sourceFilePath, string destinationFolderPath)
        {
            try
            {
                MessageFilter.Register();
                Word.Document document = _wordObject.Documents.Open(FileName: sourceFilePath);

                string pdfDestination = Path.Combine(destinationFolderPath, "pdf");
                if (!Directory.Exists(pdfDestination))
                    Directory.CreateDirectory(pdfDestination);
                string pngDestination = Path.Combine(destinationFolderPath, "png");
                if (!Directory.Exists(pngDestination))
                    Directory.CreateDirectory(pngDestination);
                string jpgDestination = Path.Combine(destinationFolderPath, "jpg");
                if (!Directory.Exists(jpgDestination))
                    Directory.CreateDirectory(jpgDestination);
                string thumbsDestination = Path.Combine(destinationFolderPath, "thumbs");
                if (!Directory.Exists(thumbsDestination))
                    Directory.CreateDirectory(thumbsDestination);
                string docDestination = Path.Combine(destinationFolderPath, "doc");
                if (!Directory.Exists(docDestination))
                    Directory.CreateDirectory(docDestination);
                string docxDestination = Path.Combine(destinationFolderPath, "docx");
                if (!Directory.Exists(docxDestination))
                    Directory.CreateDirectory(docxDestination);

                string pdfFileName = Path.Combine(pdfDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "pdf"));
                document.ExportAsFixedFormat(OutputFileName: pdfFileName, ExportFormat: Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);

                ToolClasses.PdfHelper.Instance.ExportPdf(pdfFileName, pngDestination, jpgDestination, thumbsDestination);

                _wordObject.Browser.Target = Word.WdBrowseTarget.wdBrowsePage;
                for (int i = 1; i <= document.ComputeStatistics(Word.WdStatistic.wdStatisticPages); i++)
                {
                    document.Bookmarks["\\page"].Range.Copy();

                    Word.Document singlePageDocument = _wordObject.Documents.Add();
                    singlePageDocument.Activate();
                    _wordObject.Selection.Paste();
                    _wordObject.Selection.TypeBackspace();

                    singlePageDocument.SaveAs(Path.Combine(docDestination, string.Format("Page{0}.{1}", new string[] { i.ToString(), "doc" })), Word.WdSaveFormat.wdFormatDocument);
                    singlePageDocument.SaveAs(Path.Combine(docxDestination, string.Format("Page{0}.{1}", new string[] { i.ToString(), "docx" })), Word.WdSaveFormat.wdFormatXMLDocument);

                    ((Word._Document)singlePageDocument).Close();
                    AppManager.Instance.ReleaseComObject(singlePageDocument);
                    document.Activate();
                    _wordObject.Browser.Next();
                }

                ((Word._Document)document).Close(false);
                AppManager.Instance.ReleaseComObject(document);
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }
    }
}
