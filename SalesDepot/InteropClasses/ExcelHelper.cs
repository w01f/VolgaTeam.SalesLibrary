using System;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesDepot.InteropClasses
{
    class ExcelHelper
    {
        private static ExcelHelper _instance = new ExcelHelper();

        private Excel.Application _excelObject;
        private bool _isFirstLaunch = false;
        private bool _isOpened;

        private ExcelHelper()
        {
        }

        public static ExcelHelper Instance
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
                Process[] proc = Process.GetProcessesByName("EXCEL");
                if (!(proc.GetLength(0) > 0))
                {
                    _excelObject = null;
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
                _excelObject =
                    System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application") as Excel.Application;
                _isFirstLaunch = false;
            }
            catch
            {
                _excelObject = null;
            }
            if (_excelObject == null)
            {
                try
                {

                    _excelObject = new Excel.Application();
                    _isFirstLaunch = true;
                }
                catch
                {
                    return false;
                }
            }
            if (_excelObject != null)
            {
                _excelObject.Visible = false;
                _excelObject.DisplayAlerts = false;
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
                Process[] proc = Process.GetProcessesByName("EXCEL");
                if (proc.GetLength(0) > 0)
                    proc[0].Kill();
                _isOpened = false;
            }
            _excelObject = null;
            GC.Collect();
        }

        public void Print(FileInfo file)
        {
            Excel.Workbook workBook = _excelObject.Workbooks.Open(Filename: file.FullName);
            _excelObject.Visible = true;
            workBook.Application.Dialogs[Excel.XlBuiltInDialog.xlDialogPrint].Show();
        }

        public bool ConvertToPDF(string originalFileName, string pdfFileName)
        {
            bool result = false;
            try
            {
                MessageFilter.Revoke();
                Excel.Workbook workbook = _excelObject.Workbooks.Open(Filename: originalFileName);
                workbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, pdfFileName, Excel.XlFixedFormatQuality.xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
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
                Excel.Workbook workbook = _excelObject.Workbooks.Open(Filename: oldFileName, ReadOnly: true);
                workbook.SaveAs(Filename: newFileName, FileFormat: Excel.XlFileFormat.xlHtml);
                workbook.Close(SaveChanges: false);
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
