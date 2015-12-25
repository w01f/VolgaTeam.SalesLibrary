using System.Data.Entity.ModelConfiguration;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Mappers.Wallbin
{
	class BaseLibraryLinkMap : EntityTypeConfiguration<BaseLibraryLink>
	{
		public BaseLibraryLinkMap()
		{
			ToTable("LibraryLink");
			HasOptional(link => link.Folder).WithMany(folder => folder.Links);
		}
	}
}
