﻿using System;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries
{
	public partial class LibraryPageTagInfo : LabelControl
	{
		private LibraryPage _libraryPage;

		public LibraryPageTagInfo()
		{
			InitializeComponent();
		}

		public void LoadData(LibraryPage libraryPage)
		{
			_libraryPage = libraryPage;
			UpdateInfo();
		}

		public void UpdateInfo()
		{
			if (_libraryPage == null) return;
			TaggedLinksManager.Instance.Load(_libraryPage);
			var totalLibraryLinks = TaggedLinksManager.Instance.TotalLibraryLinks;
			var taggedLibraryLinks = TaggedLinksManager.Instance.TaggedLibraryLinks;

			var linksRequireToTag = totalLibraryLinks - taggedLibraryLinks;
			if (MainController.Instance.Lists.SearchTags.TagCount && MainController.Instance.Settings.EditorSettings.EnableTagsEdit)
				Text = String.Format("<size=-1><color={0}>Total Links: {1}    </color><color={0}>Tagged: {2}    </color><color=gray>Page Links: {3}    </color><color=gray>Tagged: {4}</color></size>",
					linksRequireToTag > 0 ? "red" : "green",
					totalLibraryLinks,
					taggedLibraryLinks,
					TaggedLinksManager.Instance.TotalPageLinks,
					TaggedLinksManager.Instance.TaggedPageLinks);
			else
				Text = String.Format("<size=-1><color={0}>Total Links: {1}    </color><color=gray>Page Links: {2}    </color></size>",
					linksRequireToTag > 0 ? "red" : "green",
					totalLibraryLinks,
					TaggedLinksManager.Instance.TotalPageLinks);
		}

		public void ReleaseControl()
		{
			Parent = null;
			_libraryPage = null;
		}
	}
}
