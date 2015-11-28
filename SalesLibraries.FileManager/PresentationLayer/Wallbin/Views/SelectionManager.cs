using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	public class SelectionManager
	{
		public DateTime? LastUpdate { get; set; }
		public ClassicFolderBox SelectedFolder { get; private set; }
		public List<BaseLibraryLink> SelectedLinks { get; private set; }

		public event EventHandler<SelectionEventArgs> SelectionChanged;

		public BaseLibraryLink SelectedLink
		{
			get { return SelectedLinks.Count == 1 ? SelectedLinks.First() : null; }
		}

		public SelectionManager()
		{
			SelectedLinks = new List<BaseLibraryLink>();
		}

		public void SelectLinks(IEnumerable<BaseLibraryLink> links, Keys modifierKeys)
		{
			if (modifierKeys == Keys.None)
			{
				if (SelectionChanged != null)
					SelectionChanged(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
				ResetLinks();
			}
			else
				SelectedLinks.RemoveAll(link => link.Folder.ExtId == SelectedFolder.DataSource.ExtId);

			SelectedLinks.AddRange(links.Where(link => SelectedLinks.All(selectedLink => selectedLink.ExtId != link.ExtId)));
			LastUpdate = DateTime.Now;
			if (SelectionChanged != null)
				SelectionChanged(this, new SelectionEventArgs(SelectionEventType.LinkSelected));
		}

		public void SelectFolder(ClassicFolderBox folder)
		{
			if (SelectedFolder == folder) return;
			SelectedFolder = folder;
			LastUpdate = DateTime.Now;
			if (SelectionChanged != null)
				SelectionChanged(this, new SelectionEventArgs(SelectionEventType.FolderSelected));
		}

		public void Reset()
		{
			if (SelectionChanged != null)
				SelectionChanged(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
			ResetFolder();
			ResetLinks();
			LastUpdate = DateTime.Now;
			if (SelectionChanged != null)
				SelectionChanged(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
		}

		private void ResetLinks()
		{
			SelectedLinks.Clear();
		}

		private void ResetFolder()
		{
			SelectedFolder = null;
		}
	}

	public enum SelectionEventType
	{
		None = 0,
		LinkSelected,
		FolderSelected,
		SelectionReset
	}

	public class SelectionEventArgs : EventArgs
	{
		public SelectionEventType SelectionType { get; private set; }

		public SelectionEventArgs(SelectionEventType selectionType)
		{
			SelectionType = selectionType;
		}
	}


}
