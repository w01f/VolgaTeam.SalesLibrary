using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.Business.Models
{
	public class InactiveLink
	{
		public LibraryObjectLink Link { get; private set; }
		public string Name { get; private set; }
		public string FolderName { get; private set; }
		public string Path { get; private set; }
		public bool IsDead { get; private set; }
		public bool IsExpired { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsChanged { get; set; }
		public InactiveLink(LibraryFileLink link)
		{
			Link = link;
			Name = link.Name;
			FolderName = link.Folder.Name;
			Path = link.FullPath;
			IsDead = link.IsDead;
			IsExpired = link.ExpirationSettings.IsExpired;
		}

		public InactiveLink(LibraryObjectLink link)
		{
			Link = link;
			Name = link.Name;
			FolderName = link.Folder.Name;
			Path = link.FullPath;
			IsDead = false;
			IsExpired = link.ExpirationSettings.IsExpired;
		}
	}
}
