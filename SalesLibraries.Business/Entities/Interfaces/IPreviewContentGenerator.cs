using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IPreviewContentGenerator
	{
		void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken);
	}
}
