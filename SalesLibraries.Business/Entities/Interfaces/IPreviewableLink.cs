using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IPreviewableLink
	{
		string PreviewSourcePath { get; }
		string PreviewContainerPath { get; }
		string PreviewContainerName { get; }
		bool IsDead { get; }
		string PreviewName { get; }
		BasePreviewContainer GetPreviewContainer();
		void ClearPreviewContainer();
		void UpdatePreviewContainer(IPreviewContentGenerator generator, CancellationToken cancelationToken);
	}
}