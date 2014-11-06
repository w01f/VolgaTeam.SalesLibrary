using System.IO;
using FileManager.ConfigurationClasses;
using FileManager.ToolClasses;

namespace FileManager.BusinessClasses
{
	public class SilentSyncManager
	{
		public static void Run()
		{
			LibraryManager.Instance.LoadLibraries(new DirectoryInfo(SettingsManager.Instance.BackupPath));
			foreach (var activeLibrary in LibraryManager.Instance.LibraryCollection)
			{
				if (activeLibrary == null || activeLibrary.IsSyncLocked(true)) continue;
				activeLibrary.Save();
				LibraryManager.Instance.Synchronize(activeLibrary);
			}
		}
	}
}
