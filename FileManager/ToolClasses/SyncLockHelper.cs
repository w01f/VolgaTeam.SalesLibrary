using System;
using System.Linq;
using FileManager.ConfigurationClasses;
using FileManager.ToolForms.WallBin;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolClasses
{
	public static class SyncLockHelper
	{
		public static bool IsSyncLocked(this Library library)
		{
			var uncompletedTags = 0;
			if (SettingsManager.Instance.SyncLockByUntaggedLinks)
			{
				var totalLinks = library.Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).Count(l => l.Type != FileTypes.LineBreak);
				var taggedLinks = library.Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).Count(l => l.Type != FileTypes.LineBreak && l.SearchTags != null && !String.IsNullOrEmpty(l.SearchTags.AllTags));
				uncompletedTags = totalLinks - taggedLinks;
			}
			var unconvertedVideos = SettingsManager.Instance.SyncLockByUnconvertedVideo ?
				library.PreviewContainers.Count(pc => (pc.Type == FileTypes.MediaPlayerVideo || pc.Type == FileTypes.QuickTimeVideo) && !pc.Ready) :
				0;

			var inactiveLinks = 0;
			if (SettingsManager.Instance.SyncLockByUnconvertedVideo)
			{
				library.ProcessDeadLinks();
				inactiveLinks = library.Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).Count(l => l.IsDead);
			}

			var locked = uncompletedTags > 0 || unconvertedVideos > 0 || inactiveLinks > 0;
			if (locked)
			{
				using (var form = new FormSyncLock(uncompletedTags, inactiveLinks, unconvertedVideos))
				{
					form.ShowDialog();
				}
			}
			return locked;
		}
	}
}
