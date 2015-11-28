using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.SalesDepot.Business.LinkViewers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Folders
{
	class FolderBoxInitializer
	{
		protected BaseFolderBox _folderBox;

		private Cursor _storedCursor;

		public virtual void Initialize(BaseFolderBox folderBox)
		{
			_folderBox = folderBox;

			_storedCursor = _folderBox.Cursor;

			_folderBox.grFiles.CellMouseEnter += OnGridCellMouseEnter;
			_folderBox.grFiles.CellMouseLeave += OnGridCellMouseLeave;
			_folderBox.grFiles.CellClick += OnGridCellClick;
			_folderBox.grFiles.CellMouseUp += OnGridCellMouseUp;
		}

		private void OnGridCellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			var linkRow = (LinkRow)_folderBox.grFiles.Rows[e.RowIndex];
			if (linkRow.Source is LibraryObjectLink)
				_folderBox.Cursor = Cursors.Hand;
		}

		private void OnGridCellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			_folderBox.Cursor = _storedCursor;
		}

		private void OnGridCellClick(object sender, DataGridViewCellEventArgs e)
		{
			var linkRow = (LinkRow)_folderBox.grFiles.Rows[e.RowIndex];
			if (linkRow.Source is LibraryObjectLink)
				LinkManager.OpenLink((LibraryObjectLink)linkRow.Source);
		}

		private void OnGridCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var linkRow = (LinkRow)_folderBox.grFiles.Rows[e.RowIndex];
			LinkManager.OpenLink((LibraryObjectLink)linkRow.Source, true);
		}
	}
}
