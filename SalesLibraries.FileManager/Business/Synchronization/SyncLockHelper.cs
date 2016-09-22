using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.Synchronization
{
	static class SyncLockHelper
	{
		public static bool IsLockedForSync(this Library library, 
			out int uncompletedTags, 
			out int unconvertedVideos, 
			out int inactiveLinks)
		{
			uncompletedTags = 0;
			if (MainController.Instance.Settings.SyncLockByUntaggedLinks)
			{
				var totalLinks = TaggedLinksManager.Instance.TotalLibraryLinks;
				var taggedLinks = TaggedLinksManager.Instance.TaggedLibraryLinks;
				uncompletedTags = totalLinks - taggedLinks;
			}
			unconvertedVideos = MainController.Instance.Settings.SyncLockByUnconvertedVideo ?
				library.PreviewContainers
					.OfType<VideoPreviewContainer>()
					.Where(videContainer => library.GetPreviewableLinksBySourcePath(videContainer.SourcePath).Any())
					.Count(pc => !pc.IsConverted) :
				0;
			inactiveLinks = 0;
			if (MainController.Instance.Settings.SyncLockByInactiveLinks)
				inactiveLinks = InactiveLinkManager.Instance.DeadLinks.Count;

			var locked = uncompletedTags > 0 || unconvertedVideos > 0 || inactiveLinks > 0;
			return locked;
		}
	}
}
