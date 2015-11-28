using System;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager
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
			_mutex = new Mutex(false, "Local\\FileManagerApplication", out firstInstance);
			if (firstInstance)
			{
				var silent = args != null && args.Length > 0 && args[0].ToLower().Equals("silent");
				if (!silent)
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					MainController.Instance.RunApplication();
				}
				else
					MainController.Instance.RunConsole();
			}
			else
				MainController.Instance.ActivateApplication();
		}
	}
}
