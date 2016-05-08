using System;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool firstInstance;
			var mutex = new Mutex(false, "Local\\CloudAdminApplication", out firstInstance);
			GC.KeepAlive(mutex);
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
