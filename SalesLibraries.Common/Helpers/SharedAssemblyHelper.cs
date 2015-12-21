using System;
using System.IO;
using System.Reflection;

namespace SalesLibraries.Common.Helpers
{
	public class SharedAssemblyHelper
	{
		public static string SharedAssemblyLocationPath { get; private set; }

		static SharedAssemblyHelper()
		{
			SharedAssemblyLocationPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
				"adSalesApps SFX", 
				"SharedAssemblies");
		}

		public static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
		{
			var assemblyName = new AssemblyName(args.Name);
			var assemblyPath = Path.Combine(SharedAssemblyLocationPath, String.Format("{0}.dll", assemblyName.Name));
			return File.Exists(assemblyPath) ?
				Assembly.LoadFrom(assemblyPath) :
				null;
		}
	}
}
