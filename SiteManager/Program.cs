using System;
using System.Threading;
using System.Windows.Forms;

namespace SalesDepot.SiteManager
{
	static class Program
	{
		private static Mutex _mutex;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool firstInstance;
			const string uniqueIdentifier = "Local\\SiteManagerApplication";
			_mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
				System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
				System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
				System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
				AppManager.Instance.RunForm();
			}
			else
			{
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}
