using System.Runtime.InteropServices;

namespace SalesLibraries.SiteManager.ToolClasses
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
