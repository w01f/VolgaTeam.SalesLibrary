using System;
using System.Threading;
using System.Windows.Forms;

namespace FileManager
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
            _mutex = new Mutex(false, "Local\\FileManagerApplication", out firstInstance);
            if (firstInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                AppManager.Instance.RunForm();
            }
            else
                AppManager.Instance.ActivateMainForm();
        }
    }
}