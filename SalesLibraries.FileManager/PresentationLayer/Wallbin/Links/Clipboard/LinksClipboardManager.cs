using System;
using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.Clipboard
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

		public IEnumerable<BaseLibraryLink> Paste(LibraryFolder parentFolder, int position)
		{
			var sourceLinks = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages
					.SelectMany(p => p.TopLevelLinks)
					.Where(link => LinkIds.Any(id => link.ExtId.Equals(id)))
					.ToList();

			var pastedLinks = new List<BaseLibraryLink>();
			foreach (var sourceLink in sourceLinks)
			{
				var pastedLink = sourceLink.Copy();
				pastedLink.Folder = parentFolder;
				if (position >= 0)
					((List<BaseLibraryLink>)parentFolder.Links).InsertItem(pastedLink, position);
				else
					parentFolder.Links.Add(pastedLink);
				pastedLinks.Add(pastedLink);
				if (position != -1)
					position++;
			}

			if (LastActionType == LinkClipboardActionType.Cut)
			{
				DataStateObserver.Instance.RaiseLinksDeleted(LinkIds);
				sourceLinks.ForEach(link => link.DeleteLink());
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
