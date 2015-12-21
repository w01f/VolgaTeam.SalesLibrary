using System;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot
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
			_mutex = new Mutex(false, "Local\\SalesDepotApplication", out firstInstance);

			if (firstInstance)
			{
				AppDomain.CurrentDomain.AssemblyResolve += SharedAssemblyHelper.OnAssemblyResolve;

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				MainController.Instance.RunApplication();
			}
			else
				MainController.Instance.ActivateApplication();
		}
	}
}
