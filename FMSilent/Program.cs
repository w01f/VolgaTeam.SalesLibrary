using System;
using System.Diagnostics;
using System.IO;

namespace FMSilent
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var fileManagerPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "FileManager.exe");
			if(!File.Exists(fileManagerPath)) return;
			try
			{
				var process = new Process();
				process.StartInfo.FileName = fileManagerPath;
				process.StartInfo.Arguments = "silent";
				process.Start();
			}
			catch {}
		}
	}
}
