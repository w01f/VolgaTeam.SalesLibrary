using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using OvernightsCalendarViewer.ConfigurationClasses;

namespace OvernightsCalendarViewer
{
	internal static class Program
	{
		private static Mutex _mutex;
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			bool firstInstance;
			_mutex = new Mutex(false, "Local\\OvernightsApplication", out firstInstance);

			SettingsManager.Instance.LoadSettings();
			if (!SettingsManager.Instance.CheckLibraries())
			{
				AppManager.Instance.ShowWarning("Your Local Sales Library is NOT Activated, OR it has been disabled for maintenance. Try Again Later…");
				return;
			}
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
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