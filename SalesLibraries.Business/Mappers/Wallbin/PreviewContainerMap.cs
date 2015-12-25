using System.Data.Entity.ModelConfiguration;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Mappers.Wallbin
{
	class PreviewContainerMap : EntityTypeConfiguration<BasePreviewContainer>
	{
		public PreviewContainerMap()
		{
			ToTable("PreviewContainer");
		}
	}
}
