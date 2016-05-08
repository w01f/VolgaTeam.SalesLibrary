using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders
{
	class FolderMovingEventArgs
	{
		public LibraryPage TargetPage { get; set; }
		public bool DeleteFromCurrent { get; set; }
	}
}
