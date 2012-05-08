using System;
using System.Windows.Forms;

namespace SalesDepot.ToolClasses
{
    public static class ExceptionManager
    {
        public static void HandleException(Exception e)
        {
            if(e.GetType() == typeof(ExcelReadException))
                AppManager.Instance.ShowWarning("Error occured while reading Excel file." + "\n" + e.Message + "\n" + e.StackTrace);
            else
                AppManager.Instance.ShowWarning(e.Message + "\n" + e.StackTrace);
        }
    }

    public class ExcelReadException : Exception
    {
        public ExcelReadException(string message, Exception e)
            : base(message, e)
        { 
        }
    }
}
