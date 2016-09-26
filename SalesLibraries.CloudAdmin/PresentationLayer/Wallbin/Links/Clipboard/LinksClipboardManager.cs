using System;
using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.DataState;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.Clipboard
{
	class LinksClipboardManager
	{
		public LinkClipboardActionType LastActionType { get; private set; }
		public List<Guid> LinkIds { get; }

		public LinksClipboardManager()
		{
			LinkIds = new List<Guid>();
		}

		public void Copy(IEnumerable<Guid> linkIds)
		{
			LastActionType = LinkClipboardActionType.Copy;
			LinkIds.Clear();
			LinkIds.AddRange(linkIds);
		}

		public void Cut(IEnumerable<Guid> linkIds)
		{
			LastActionType = LinkClipboardActionType.Cut;
			LinkIds.Clear();
			LinkIds.AddRange(linkIds);
		}

		public IEnumerable<BaseLibraryLink> Paste()
		{
			var sourceLinks = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages
					.SelectMany(p => p.TopLevelLinks)
					.Where(link => LinkIds.Any(id => link.ExtId.Equals(id)))
					.ToList();
			var pastedLinks = sourceLinks.Select(libraryLink => libraryLink.Copy(LastActionType == LinkClipboardActionType.Cut)).ToList();

			if (LastActionType == LinkClipboardActionType.Cut)
			{
				sourceLinks.ForEach(link => link.DeleteLink(true));
				DataStateObserver.Instance.RaiseLinksDeleted(LinkIds);
				LastActionType = LinkClipboardActionType.None;
				LinkIds.Clear();
			}

			return pastedLinks;
		}

		public bool IsPasteAvailable(Guid targetLinkId)
		{
			return LinkIds.Any() && (LastActionType == LinkClipboardActionType.Copy || !LinkIds.Any(l => l.Equals(targetLinkId)));
		}
	}
}
