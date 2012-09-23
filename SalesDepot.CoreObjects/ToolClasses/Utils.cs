using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDepot.CoreObjects.ToolClasses
{
    public class Utils
    {
        public static void ReleaseComObject(object o)
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
