using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders
{
	class FolderMovingEventArgs
	{
		public LibraryPage TargetPage { get; set; }
		public bool DeleteFromCurrent { get; set; }
	}
}
