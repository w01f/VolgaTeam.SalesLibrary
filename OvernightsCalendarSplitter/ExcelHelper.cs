using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace OvernightsCalendarSplitter
{
    public class ExcelHelper
    {
        private static object _locker = new object();
        private Excel.Application _excelObject;

        public ExcelHelper()
        {
        }

        public bool Connect()
        {
            bool result = false;
            try
            {
                _excelObject = new Excel.Application();
                _excelObject.Visible = false;
                _excelObject.DisplayAlerts = false;
                result = true;

            }
            catch
            {
                _excelObject = null;
            }
            return result;
        }

        public void Disconnect()
        {
            if (_excelObject != null)
            {
                foreach (Excel.Workbook workbook in _excelObject.Workbooks)
                    workbook.Close();
                uint processId = 0;
                WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_excelObject.Hwnd), out processId);
                Process.GetProcessById((int)processId).Kill();
            }
            ReleaseComObject(_excelObject);
            GC.Collect();
        }

        public void SplitFile(string filePath)
        {
            if (File.Exists(filePath) && Connect())
            {
                try
                {
                    Excel.Workbook sourceWorkBook = _excelObject.Workbooks.Open(filePath);
                    foreach (Excel.Worksheet worksheet in sourceWorkBook.Worksheets)
                    {
                        Excel.Workbook destinationWorkBook = _excelObject.Workbooks.Add();
                        int worksheetsCount = destinationWorkBook.Worksheets.Count;
                        for (int i = worksheetsCount; i >= 0; i--)
                            try
                            {
                                destinationWorkBook.Worksheets[i].Delete();
                            }
                            catch
                            {
                                break;
                            }
                        Excel.Worksheet firstWorksheet = destinationWorkBook.Worksheets[1];
                        worksheet.Copy(After: firstWorksheet);
                        firstWorksheet.Delete();
                        destinationWorkBook.SaveAs(Filename: Path.Combine(SettingsManager.Instance.DestinationPath, worksheet.Name + ".xls"), FileFormat: Excel.XlFileFormat.xlWorkbookNormal);
                        destinationWorkBook.Close();

                    }
                    sourceWorkBook.Close();
                }
                catch
                {
                }
                Disconnect();
            }
        }

        public void ReleaseComObject(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }
    }
}
