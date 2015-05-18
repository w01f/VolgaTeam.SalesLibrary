using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SalesDepot.CoreObjects.ToolClasses
{
	public class PngHelper
	{
		private static readonly string _utilityPath;

		static PngHelper()
		{
			_utilityPath = Path.Combine(Path.GetDirectoryName(typeof(PngHelper).Assembly.Location), "assets", "pngquant", "pngquant.exe");
		}

		public static void ConvertFiles(string targetPath)
		{
			if (!Directory.Exists(targetPath)) return;
			if (!File.Exists(_utilityPath)) return;
			foreach (var pngFilePath in GetPngFiles(targetPath))
			{
				var filePath = pngFilePath;
				if (Path.GetExtension(filePath) == ".PNG")
					filePath = Path.ChangeExtension(filePath, ".png");

				var processInfo = new ProcessStartInfo(
					_utilityPath,
					String.Format("--force --ext .png --quality=45-85 --speed 10 \"{0}\"", filePath))
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

		private static IEnumerable<string> GetPngFiles(string targetFolderPath)
		{
			var filePaths = new List<string>();
			foreach (var subFolderPath in Directory.GetDirectories(targetFolderPath))
				filePaths.AddRange(GetPngFiles(subFolderPath));
			foreach (var filePath in Directory.GetFiles(targetFolderPath, "*.png"))
				filePaths.Add(filePath);
			return filePaths;
		}
	}
}
