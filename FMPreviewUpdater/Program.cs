using System;
using System.IO;
using FileManager.BusinessClasses;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FMPreviewUpdater
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Loading Data...");
			SettingsManager.Instance.Load();
			ListManager.Instance.Init();
			if (String.IsNullOrEmpty(SettingsManager.Instance.BackupPath) ||
				!Directory.Exists(SettingsManager.Instance.BackupPath)) return;
			LibraryManager.Instance.LoadLibraries(new DirectoryInfo(SettingsManager.Instance.BackupPath));
			foreach (var activeLibrary in LibraryManager.Instance.LibraryCollection)
			{
				Console.WriteLine("Fixing Png Images...");
				var videoPreviewStorage = Path.Combine(activeLibrary.StoragePath, Constants.FtpPreviewContainersRootFolderName, "video");
				if (Directory.Exists(videoPreviewStorage))
				{
					foreach (var linkContainerPath in Directory.GetDirectories(videoPreviewStorage))
					{
						Console.WriteLine("Fixing Images in {0}...", linkContainerPath);
						PngHelper.ConvertFiles(linkContainerPath);
					}
				}

				var webPreviewStorage = Path.Combine(activeLibrary.StoragePath, Constants.FtpPreviewContainersRootFolderName, "files");
				if (Directory.Exists(webPreviewStorage))
				{
					foreach (var linkContainerPath in Directory.GetDirectories(webPreviewStorage))
					{
						Console.WriteLine("Fixing Images in {0}...", linkContainerPath);
						PngHelper.ConvertFiles(linkContainerPath);
					}
				}
				
				var localPreviewStorage = Path.Combine(activeLibrary.StoragePath, Constants.RegularPreviewContainersRootFolderName);
				if (Directory.Exists(localPreviewStorage))
				{
					foreach (var linkContainerPath in Directory.GetDirectories(localPreviewStorage))
					{
						Console.WriteLine("Fixing Images in {0}...", linkContainerPath);
						PngHelper.ConvertFiles(linkContainerPath);
					}
				}
			}
			Console.WriteLine("Complited!");
		}
	}
}
