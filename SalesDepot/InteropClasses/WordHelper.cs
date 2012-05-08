using System;
using System.Diagnostics;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace SalesDepot.InteropClasses
{
    class WordHelper
    {
        private static WordHelper _instance = new WordHelper();

        private Word.Application _wordObject;
        private bool _isFirstLaunch = false;
        private bool _isOpened;

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

        public bool IsOpened
        {
            get
            {
                Process[] proc = Process.GetProcessesByName("WINWORD");
                if (!(proc.GetLength(0) > 0))
                {
                    _wordObject = null;
                    _isOpened = false;
                }
                return _isOpened;
            }
        }

        public bool Open()
        {
            _isOpened = false;
            try
            {
                _wordObject =
                    System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application") as Word.Application;
                _isFirstLaunch = false;
            }
            catch
            {
                _wordObject = null;
            }
            if (_wordObject == null)
            {
                try
                {

                    _wordObject = new Word.Application();
                    _isFirstLaunch = true;
                }
                catch
                {
                    return false;
                }
            }
            if (_wordObject != null)
            {
                _wordObject.Visible = false;
                _wordObject.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
                _isOpened = true;
                return true;
            }
            else
                return false;
        }

        public void Close()
        {
            if (_isFirstLaunch)
            {
                Process[] proc = Process.GetProcessesByName("WINWORD");
                if (proc.GetLength(0) > 0)
                    proc[0].Kill();
                _isOpened = false;
            }
            _wordObject = null;
            GC.Collect();
        }

        public bool ConvertToPDF(string originalFileName, string pdfFileName)
        {
            bool result = false;
            try
            {
                MessageFilter.Register();
                Word.Document document = _wordObject.Documents.Open(FileName: originalFileName);
                document.ExportAsFixedFormat(OutputFileName: pdfFileName, ExportFormat: Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                ((Word._Document)document).Close(SaveChanges: false);
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

        public void ConvertToHtml(string oldFileName, string newFileName)
        {
            try
            {
                MessageFilter.Register();
                Word.Document document = _wordObject.Documents.Open(FileName: oldFileName);
                document.SaveAs(FileName: newFileName, FileFormat: Word.WdSaveFormat.wdFormatHTML);
                ((Word._Document)document).Close(SaveChanges: false);
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
