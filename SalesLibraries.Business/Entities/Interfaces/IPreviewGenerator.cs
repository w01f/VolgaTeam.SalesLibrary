using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IPreviewGenerator
	{
		void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken);
	}
}
