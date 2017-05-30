using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SalesLibraries.BatchTagger
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
			const string uniqueIdentifier = "Local\\BatchTaggerApplication";
			_mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
				Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				AppManager.Instance.RunForm();
			}
			else
			{
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}
