using System;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Business.Services;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries
{
	public partial class LibraryPageTagInfo : LabelControl
	{
		private readonly LibraryPage _libraryPage;

		public LibraryPageTagInfo(LibraryPage libraryPage)
		{
			InitializeComponent();

			_libraryPage = libraryPage;
			UpdateInfo();
		}

		public void UpdateInfo()
		{
			TaggedLinksManager.Instance.Load(_libraryPage);
			var totalLibraryLinks = TaggedLinksManager.Instance.TotalLibraryLinks;
			var taggedLibraryLinks = TaggedLinksManager.Instance.TaggedLibraryLinks;
			if (totalLibraryLinks > 0 && taggedLibraryLinks == 0)
			{
				Text = String.Format("<size=-1><color=red><b>You need to start TAGGING your links</b></color><br><br><color=gray>Links: {0}    Tagged: {1}</color></size>",
					TaggedLinksManager.Instance.TotalPageLinks,
					TaggedLinksManager.Instance.TaggedPageLinks);
			}
			else if (totalLibraryLinks > taggedLibraryLinks)
			{
				var linksRequireToTag = totalLibraryLinks - taggedLibraryLinks;
				Text = String.Format("<size=-1><color=red><b>{0}</b></color><br><br><color=gray>Links: {1}    Tagged: {2}</color></size>",
					linksRequireToTag == 1 ?
						"1 Link Requires TAGS" :
						String.Format("{0} Links Require TAGS", linksRequireToTag),
					TaggedLinksManager.Instance.TotalPageLinks,
					TaggedLinksManager.Instance.TaggedPageLinks);
			}
			else if (totalLibraryLinks > 0 && taggedLibraryLinks == totalLibraryLinks)
			{
				Text = String.Format("<size=-1><color=green><b>Library 100% TAGGED</b></color><br><br><color=gray>Links: {0}    Tagged: {1}</color></size>",
					TaggedLinksManager.Instance.TotalPageLinks,
					TaggedLinksManager.Instance.TaggedPageLinks);
			}
			else
				Text = String.Empty;
		}

		public void ReleaseControl()
		{
			Parent = null;
		}
	}
}
