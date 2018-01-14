using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	public class SelectionManager
	{
		private bool _suspended;

		public DateTime? LastUpdate { get; set; }
		public ClassicFolderBox SelectedFolder { get; private set; }
		public List<BaseLibraryLink> SelectedLinks { get; }
		public event EventHandler<SelectionEventArgs> SelectionChanged;

		public BaseLibraryLink SelectedLink => SelectedLinks.Count == 1 ? SelectedLinks.First() : null;
		public List<BaseLibraryLink> SelectedObjects => SelectedLinks.OfType<LibraryObjectLink>().OfType<BaseLibraryLink>().ToList();

		public SelectionManager()
		{
			SelectedLinks = new List<BaseLibraryLink>();
		}

		public void SelectLinks(IEnumerable<BaseLibraryLink> links, Keys modifierKeys)
		{
			if (_suspended) return;
			if (modifierKeys == Keys.None)
			{
				SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
				ResetLinksInternal();
			}
			else
				SelectedLinks.RemoveAll(link => link == null || link.Folder.ExtId == SelectedFolder.DataSource.ExtId);

			SelectedLinks.AddRange(links.Where(link => link != null && SelectedLinks.All(selectedLink => selectedLink.ExtId != link.ExtId)));
			LastUpdate = DateTime.Now;
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.LinkSelected));
		}

		public void SelectFolder(ClassicFolderBox folder)
		{
			if (_suspended) return;
			if (SelectedFolder == folder) return;
			SelectedFolder = folder;
			LastUpdate = DateTime.Now;
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.FolderSelected));
		}

		public void ResetAll()
		{
			if (_suspended) return;
			ResetFolderInternal();
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.FolderSelected));
			ResetLinksInternal();
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
			LastUpdate = DateTime.Now;
		}

		public void ResetLinks()
		{
			if (_suspended) return;
			ResetLinksInternal();
			LastUpdate = DateTime.Now;
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
		}

		private void ResetLinksInternal()
		{
			SelectedLinks.Clear();
		}

		private void ResetFolderInternal()
		{
			SelectedFolder = null;
		}

		public void Suspend()
		{
			_suspended = true;
		}

		public void Resume()
		{
			_suspended = false;
			ResetAll();
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
