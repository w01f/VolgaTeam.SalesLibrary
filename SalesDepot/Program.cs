using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace SalesDepot
{
    static class Program
    {
        public static Mutex mutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool firstInstance;
            mutex = new Mutex(false, "Local\\SalesDepotApplication", out firstInstance);

            if (!ConfigurationClasses.SettingsManager.Instance.CheckLibraries())
            {
                AppManager.Instance.ShowWarning("Sales Depot Unavailable: You do not have libraries in source folder....");
                return;
            }
            if (firstInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

                ConfigurationClasses.SettingsManager.Instance.CheckStaticFolders();
                ConfigurationClasses.SettingsManager.Instance.LoadSettings();
                if (ConfigurationClasses.SettingsManager.Instance.LaunchPPT)
                    AppManager.Instance.RunPowerPointLoader();
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
