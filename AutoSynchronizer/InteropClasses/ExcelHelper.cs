using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoSynchronizer.InteropClasses
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
                SalesDepot.CoreObjects.InteropClasses.WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_excelObject.Hwnd), out processId);
                Process.GetProcessById((int)processId).Kill();
            }
            AppManager.Instance.ReleaseComObject(_excelObject);
            GC.Collect();
        }

        public DateTime GetOvernightsDate(string filePath)
        {
            DateTime result =DateTime.MinValue;
            lock (AppManager.Locker)
            {
                if (Connect())
                {
                    try
                    {
                        Excel.Workbook workBook = _excelObject.Workbooks.Open(filePath);
                        foreach (Excel.Worksheet worksheet in workBook.Worksheets)
                        {
                            object value = worksheet.Range["O1"].Value;
                            if (value != null)
                            {
                                Match m = Regex.Match(value.ToString(), @"\d{1,2}[-/\.]\d{1,2}[-/\.]\d{2,4}");
                                if (m.Success)
                                    if (!DateTime.TryParse(m.Value, out result))
                                        result = DateTime.Now;
                            }
                            break;
                        }
                    }
                    catch
                    {
                    }
                    Disconnect();
                }
                return result;
            }
        }
    }
}
