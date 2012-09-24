using System;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesDepot.CoreObjects.InteropClasses
{
    public partial class ExcelHelper
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
            ToolClasses.Utils.ReleaseComObject(_excelObject);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExportBookAllFormats(string sourceFilePath, string destinationFolderPath, out bool update)
        {
            string txtDestination = Path.Combine(destinationFolderPath, "txt");
            bool updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0);
            if (!Directory.Exists(txtDestination))
                Directory.CreateDirectory(txtDestination);

            update = false;
            if (updateTxt)
            {
                update = true;
                try
                {
                    if (Connect())
                    {
                        MessageFilter.Register();
                        Excel.Workbook workbook = _excelObject.Workbooks.Open(Filename: sourceFilePath,ReadOnly: true);
                        string txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "txt"));
                        workbook.SaveAs(Filename: txtFileName, FileFormat: Excel.XlFileFormat.xlTextWindows);
                        workbook.Close();
                        ToolClasses.Utils.ReleaseComObject(workbook);
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
