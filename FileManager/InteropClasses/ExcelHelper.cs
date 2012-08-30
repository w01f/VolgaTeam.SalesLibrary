using System;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace FileManager.InteropClasses
{
    class ExcelHelper
    {
        private static ExcelHelper _instance = new ExcelHelper();

        private Excel.Application _excelObject;

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

        public bool Connect()
        {
            bool result = false;
            MessageFilter.Register();
            try
            {
                _excelObject = new Microsoft.Office.Interop.Excel.Application();
                _excelObject.DisplayAlerts = false;
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
            AppManager.Instance.ReleaseComObject(_excelObject);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExportBookAllFormats(string sourceFilePath, string destinationFolderPath)
        {
            string txtDestination = Path.Combine(destinationFolderPath, "txt");
            bool updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0);
            if (!Directory.Exists(txtDestination))
                Directory.CreateDirectory(txtDestination);

            if (updateTxt)
            {
                try
                {
                    if (Connect())
                    {
                        MessageFilter.Register();
                        Excel.Workbook workbook = _excelObject.Workbooks.Open(Filename: sourceFilePath);
                        string txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "txt"));
                        workbook.SaveAs(Filename: txtFileName, FileFormat: Excel.XlFileFormat.xlTextWindows);
                        workbook.Close();
                        AppManager.Instance.ReleaseComObject(workbook);
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
