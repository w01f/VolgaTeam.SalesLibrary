using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	public class SelectionManager
	{
		private bool _suspended;
		private readonly List<BaseLibraryLink> _selectedLinks = new List<BaseLibraryLink>();

		public DateTime? LastUpdate { get; set; }
		public ClassicFolderBox SelectedFolder { get; private set; }
		public List<LibraryObjectLink> SelectedFiles { get; }
		public event EventHandler<SelectionEventArgs> SelectionChanged;

		public BaseLibraryLink SelectedLink => _selectedLinks.Count == 1 ? _selectedLinks.First() : null;

		public SelectionManager()
		{
			SelectedFiles = new List<LibraryObjectLink>();
		}

		public void SelectLinks(IEnumerable<BaseLibraryLink> links, Keys modifierKeys)
		{
			if (_suspended) return;
			if (modifierKeys == Keys.None)
			{
				SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
				ResetLinks();
			}
			else
				_selectedLinks.RemoveAll(link => link.Folder.ExtId == SelectedFolder.DataSource.ExtId);

			if (!links.Any()) return;

			_selectedLinks.AddRange(links.Where(link => _selectedLinks.All(selectedLink => selectedLink.ExtId != link.ExtId)));
			SelectedFiles.AddRange(_selectedLinks.OfType<LibraryObjectLink>());
			LastUpdate = DateTime.Now;
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.LinkSelected));
		}

		public void SelectFolder(ClassicFolderBox folder)
		{
			if (_suspended) return;
			if (SelectedFolder == folder) return;
			SelectedFolder = folder;
			if (SelectedFolder.FormatState?.AllowMultiSelect != true)
				ResetLinks();
			LastUpdate = DateTime.Now;
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.FolderSelected));
		}

		public void Reset()
		{
			if (_suspended) return;
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
			ResetFolder();
			ResetLinks();
			LastUpdate = DateTime.Now;
			SelectionChanged?.Invoke(this, new SelectionEventArgs(SelectionEventType.SelectionReset));
		}

		private void ResetLinks()
		{
			_selectedLinks.Clear();
			SelectedFiles.Clear();
		}

		private void ResetFolder()
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
			Reset();
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
