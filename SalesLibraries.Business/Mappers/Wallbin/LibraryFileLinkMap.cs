using System.Data.Entity.ModelConfiguration;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Mappers.Wallbin
{
	class LibraryFileLinkMap : EntityTypeConfiguration<LibraryFileLink>
	{
		public LibraryFileLinkMap()
		{
			HasOptional(link => link.FolderLink).WithMany(folderLink => folderLink.Links);
		}
	}
}
