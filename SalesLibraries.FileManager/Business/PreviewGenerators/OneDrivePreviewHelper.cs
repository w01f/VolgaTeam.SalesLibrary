using System.IO;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	class OneDrivePreviewHelper
	{
		public static void GenerateShortcutFiles(string url, string fileName, string destinationFilePath)
		{
			using (StreamWriter writer = new StreamWriter(Path.Combine(destinationFilePath, Path.ChangeExtension(fileName, ".url"))))
			{
				writer.WriteLine("[InternetShortcut]");
				writer.WriteLine("URL=" + url);
				writer.Flush();
			}

			File.WriteAllText(Path.Combine(destinationFilePath, "url.txt"), url);
		}
	}
}
