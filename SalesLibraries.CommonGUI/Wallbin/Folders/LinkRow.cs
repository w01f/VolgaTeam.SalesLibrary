using System;
using System.Windows.Forms;
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

		public bool AllowEdit => !Source.Banner.Enable;

		public bool IsTop => Index == 0;

		public bool IsBottom => DataGridView != null && Index == DataGridView.RowCount - 1;

		public bool IsOpenable => Source is LibraryObjectLink;

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

		public void DeleteWithSourceLink(bool fullDelete = false)
		{
			RemoveFromGrid();
			Source.DeleteLink(fullDelete);
		}

		public void DeleteWithAllRelatedLinks()
		{
			RemoveFromGrid();
			SourceObject?.DeleteLinkAndRelatedLinks();
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
