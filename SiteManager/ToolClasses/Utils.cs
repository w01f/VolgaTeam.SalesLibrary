using System.Runtime.InteropServices;

namespace SalesDepot.SiteManager.ToolClasses
{
	static class Utils
	{
		public static void ReleaseComObject(object o)
		{
			try
			{
				Marshal.ReleaseComObject(o);
			}
			catch { }
			finally
			{
				o = null;
			}
		}
	}
}
