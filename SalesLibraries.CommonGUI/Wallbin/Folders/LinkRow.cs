using System;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CommonGUI.Wallbin.Folders
{
	public class LinkRow : DataGridViewRow
	{
		public BaseLibraryLink Source { get; private set; }
		public BaseFolderBox FolderBox { get; private set; }
		public LinkRowInfo Info { get; private set; }

		public event EventHandler<EventArgs> InfoChanged;

		public LibraryObjectLink SourceObject => Source as LibraryObjectLink;

		public bool AllowEditLinkText => !Source.Banner.Enable || (Source.Banner.Enable && Source.Banner.TextMode == BannerTextMode.CustomText);

		public bool AllowEditImageSettings => Source.Banner.Enable || Source.Widget.Enabled || Source.Widget.HasAutoWidget;

		public bool IsTop => Index == 0;

		public bool IsBottom => DataGridView != null && Index == DataGridView.RowCount - 1;

		public bool Inaccessable => Source is LibraryFileLink && ((LibraryFileLink)Source).IsDead;
		public bool IsOpenable => Source is LibraryObjectLink && !(Source is LinkBundleLink) && !Inaccessable;

		public bool IsLinkBundleCompatible => Source is LibraryObjectLink && !(Source is LibraryFolderLink);

		public LinkRow()
		{
			Info = new LinkRowInfo(this);
		}

		public void Init(BaseLibraryLink source, BaseFolderBox folderBox)
		{
			Source = source;
			FolderBox = folderBox;
		}

		public void ChangeFolder(BaseFolderBox folderBox)
		{
			FolderBox = folderBox;
		}

		public void DeleteWithSourceLink()
		{
			RemoveFromGrid();
			Source.DeleteLink();
		}

		public void DeleteFromFolder()
		{
			RemoveFromGrid();
			Source.UnlinkLink();
		}

		public void RemoveFromGrid()
		{
			DataGridView.Rows.Remove(this);
			FolderBox = null;
			InfoChanged = null;
		}

		public void OnInfoChanged()
		{
			InfoChanged?.Invoke(this, EventArgs.Empty);
		}

		protected override void Dispose(bool disposing)
		{
			Info.Dispose();
			Info = null;
			Source = null;
			FolderBox = null;
			InfoChanged = null;
			base.Dispose(disposing);
		}
	}
}
