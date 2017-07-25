namespace SalesLibraries.Common.Configuration
{
	public class Constants
	{
		public const string LocalStorageFileName = @"z_library_data_local.sqlite";
		public const string RemoteStorageFileName = @"z_library_data_cloud.sqlite";
		public const string ShortLibraryInfoFileName = @"z_library_data_info.xml";
		public const string LegacyStorageFileName = @"SalesDepotCache.xml";
		public const string LibrariesJsonFileName = @"z_library_data_cloud.json";
		public const string OldPrimaryFileStorageName = @"Primary Root";
		public const string PrimaryFileStorageName = @"Source Directory";
		public const string RegularPreviewContainersRootFolderName = @"!QV";
		public const string WebPreviewContainersRootFolderName = @"!WV";
		public const string ExternalFilesRootFolderName = @"!WV_Wildcards";
		public const string ExternalFilesStorageFolderName = @"files";
		public const string ExternalFilesTrashFolderName = @"trash";
		public const string LibraryLogoFolder = @"!SD-Graphics";
		public const string OvernightsCalendarRootFolderName = @"!OC";
		public const string WindowsThumbnailFile = "thumbs.db";
		public const string GoodSyncServiceFolderName = "_gsdata_";
		public const string FilesDeletedFromFolderArchiveName = "z_archive";
		public const string DatabaseConnectionStateInfoFileName = "z_library_LOCKED.txt";
		public const string DeadLinkInfoFileName = @"z_dead_links.txt";
		public const string LogArchiveFolderName = @"zzzz_logs_never_delete";
		public const string OfficeTempFilePrefixName = @"~$";
		public const string SinglePreviewFilePrefixName = "Single";
		public const string OriginalVideoInfoFileNameTemplate = @"{0}_original";
		public const string OutputVideoInfoFileNameTemplate = @"{0}_output";
	}
}
