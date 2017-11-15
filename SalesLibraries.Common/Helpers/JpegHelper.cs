using System;
using System.Diagnostics;
using System.IO;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Common.Helpers
{
	public static class JpegHelper
	{
		private static readonly string _utilityPath;

		static JpegHelper()
		{
			_utilityPath = Path.Combine(GlobalSettings.ApplicationRootPath, "assets", "image magick", "magick.exe");
		}

		public static void ConvertFiles(string sourceFolderPath, string targetFolderPath)
		{
			if (!Directory.Exists(sourceFolderPath)) return;
			if (!Directory.Exists(targetFolderPath)) return;
			if (!File.Exists(_utilityPath)) return;

			var processInfo = new ProcessStartInfo(
					_utilityPath,
					String.Format("mogrify -units PixelsPerInch -quality 80% -density 72 -resize 400x200 -format jpg -path \"{1}\"  \"{0}\\*.png\"", sourceFolderPath, targetFolderPath))
			{
				CreateNoWindow = true,
				UseShellExecute = false
			};
			try
			{
				var process = Process.Start(processInfo);
				process.WaitForExit();
				process.Close();
			}
			catch { }
		}
	}
}
