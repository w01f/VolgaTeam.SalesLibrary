using System;
using System.Diagnostics;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace SalesDepot.CoreObjects.InteropClasses
{
    class WordHelper
    {
        private static WordHelper _instance = new WordHelper();

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

        public void ExportDocumentAllFormats(string sourceFilePath, string destinationFolderPath)
        {
            string pdfDestination = Path.Combine(destinationFolderPath, "pdf");
            bool updatePdf = !(Directory.Exists(pdfDestination) && Directory.GetFiles(pdfDestination, "*.pdf").Length > 0);
            if (!Directory.Exists(pdfDestination))
                Directory.CreateDirectory(pdfDestination);
            string pngDestination = Path.Combine(destinationFolderPath, "png");
            bool updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0);
            if (!Directory.Exists(pngDestination))
                Directory.CreateDirectory(pngDestination);
            string jpgDestination = Path.Combine(destinationFolderPath, "jpg");
            bool updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0);
            if (!Directory.Exists(jpgDestination))
                Directory.CreateDirectory(jpgDestination);
            string thumbsDestination = Path.Combine(destinationFolderPath, "thumbs");
            bool updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination, "*.png").Length > 0);
            if (!Directory.Exists(thumbsDestination))
                Directory.CreateDirectory(thumbsDestination);
            string docDestination = Path.Combine(destinationFolderPath, "doc");
            bool updateDoc = !(Directory.Exists(docDestination) && Directory.GetFiles(docDestination, "*.doc").Length > 0);
            if (!Directory.Exists(docDestination))
                Directory.CreateDirectory(docDestination);
            string docxDestination = Path.Combine(destinationFolderPath, "docx");
            bool updateDocx = !(Directory.Exists(docxDestination) && Directory.GetFiles(docxDestination, "*.docx").Length > 0);
            if (!Directory.Exists(docxDestination))
                Directory.CreateDirectory(docxDestination);
            string txtDestination = Path.Combine(destinationFolderPath, "txt");
            bool updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0);
            if (!Directory.Exists(txtDestination))
                Directory.CreateDirectory(txtDestination);

            if (updatePdf || updatePng || updateJpg || updateThumbs || updateDoc || updateDocx || updateTxt)
            {
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

                        string txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "txt"));
                        if (updateTxt)
                        {
                            using (StreamWriter sw = new StreamWriter(txtFileName, false))
                            {
                                sw.Write(document.Content.Text);
                                sw.Flush();
                            }
                        }

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
}
