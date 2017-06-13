using System;
using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IPreviewableLink
	{
		string PreviewContainerPath { get; }
		string PreviewContainerName { get; }
		string FullPath { get; }
		bool IsDead { get; }
		string PreviewName { get; }
		BasePreviewContainer GetPreviewContainer();
		void ClearPreviewContainer();
		void UpdatePreviewContainer(IPreviewGenerator generator, CancellationToken cancelationToken);
	}
}