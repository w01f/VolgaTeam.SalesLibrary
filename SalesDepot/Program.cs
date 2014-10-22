using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace SalesDepot
{
	static class Program
	{
		private static Mutex _mutex;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			bool firstInstance;
			_mutex = new Mutex(false, "Local\\SalesDepotApplication", out firstInstance);

			ConfigurationClasses.SettingsManager.Instance.CheckStaticFolders();
			if (args != null && args.Length > 0)
				ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection = args[0].ToLower().Equals("-remote");
			else
				ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection = false;
			ConfigurationClasses.SettingsManager.Instance.GetDefaultWizard();
			ConfigurationClasses.SettingsManager.Instance.LoadSettings();
			if (!ConfigurationClasses.SettingsManager.Instance.CheckLibraries())
			{
				AppManager.Instance.ShowWarning("Your Local Sales Library is NOT Activated, OR it has been disabled for maintenance. Try Again Later…");
				AppManager.Instance.ActivityManager.AddUserActivity("Application not started");
				return;
			}
			if (ConfigurationClasses.PermissionsManager.Instance.Configured && !ConfigurationClasses.PermissionsManager.Instance.IsUserAutorized())
			{
				using (var form = new ToolForms.Settings.FormAuthorizeWarning())
				{
					form.ShowDialog();
				}
				return;
			}
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
				Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

				AppManager.Instance.RunForm();
			}
			else
			{
				AppManager.Instance.ActivatePowerPoint();
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}
