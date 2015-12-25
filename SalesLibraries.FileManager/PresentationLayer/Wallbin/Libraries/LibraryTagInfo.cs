using System;
using System.Drawing;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Business.Services;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries
{
	public partial class LibraryTagInfo : LabelControl
	{
		private readonly Library _library;

		public LibraryTagInfo(Library library)
		{
			InitializeComponent();
			_library = library;
			UpdateInfo();
		}

		public void UpdateInfo()
		{
			TaggedLinksManager.Instance.Load(_library);
			var totalLinks = TaggedLinksManager.Instance.TotalLinks;
			var taggedLinks = TaggedLinksManager.Instance.TaggedLinks;
			if (totalLinks > 0 && taggedLinks == 0)
			{
				Text = "You need to start TAGGING your Links!";
				ForeColor = Color.Red;
			}
			else if (totalLinks > taggedLinks)
			{
				Text = String.Format("{0} Links Require Tags!", totalLinks - taggedLinks);
				ForeColor = Color.Red;
			}
			else if (totalLinks > 0 && taggedLinks == totalLinks)
			{
				Text = "Library 100% TAGGED!";
				ForeColor = Color.Green;
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
