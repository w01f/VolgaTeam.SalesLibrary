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
				Console.WriteLine("Fixing Video...");
				var videoPreviewStorage = Path.Combine(activeLibrary.StoragePath, Constants.FtpPreviewContainersRootFolderName, "video");
				if (Directory.Exists(videoPreviewStorage))
				{
					foreach (var linkContainerPath in Directory.GetDirectories(videoPreviewStorage))
					{
						var wmvFolder = Path.Combine(linkContainerPath, "wmv");
						if (Directory.Exists(wmvFolder))
						{
							Console.WriteLine("Deleting {0}...", wmvFolder);
							SyncManager.DeleteFolder(new DirectoryInfo(wmvFolder));
						}

						var ogvFolder = Path.Combine(linkContainerPath, "ogv");
						if (Directory.Exists(ogvFolder))
						{
							Console.WriteLine("Deleting {0}...", ogvFolder);
							SyncManager.DeleteFolder(new DirectoryInfo(ogvFolder));
						}
					}
				}
			}
			Console.WriteLine("Complited!");
		}
	}
}
