using System.IO;

namespace SalesLibraries.Common.Synchronization
{
	public sealed class SynchronizationOptions
	{
		public DirectoryInfo SourceDirectory { get; private set; }
		public DirectoryInfo DestinationDirectory { get; private set; }
		public bool DeleteExtraFilesInDestination { get; }
		public SyncFilterList FilterList { get; set; }


		public SynchronizationOptions(
			DirectoryInfo sourceDirectory,
			DirectoryInfo destinationDirectory,
			bool deleteExtraFilesInDestination = false)
		{
			SourceDirectory = sourceDirectory;
			DestinationDirectory = destinationDirectory;

			DeleteExtraFilesInDestination = deleteExtraFilesInDestination;
		}

		public static SynchronizationOptions CopyForChildFolder(
			SynchronizationOptions sourceOptions,
			DirectoryInfo newSourceDir,
			DirectoryInfo newDestinationDir
			)
		{
			var newOptions = new SynchronizationOptions(
				newSourceDir,
				newDestinationDir,
				sourceOptions.DeleteExtraFilesInDestination);
			newOptions.FilterList = sourceOptions.FilterList;
			return newOptions;
		}
	}
}