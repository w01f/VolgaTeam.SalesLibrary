using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IOneDriveContentGenerator
	{
		void GenerateOneDriveContent(FilePreviewContainer previewContainer, CancellationToken cancellationToken);
	}
}
