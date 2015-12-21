using System.Drawing;
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

		private DataGridView.HitTestInfo _hitTest;
		private Rectangle _dragBox;

		public virtual void Initialize(BaseFolderBox folderBox)
		{
			_folderBox = folderBox;

			_storedCursor = _folderBox.Cursor;

			_folderBox.grFiles.CellMouseEnter += OnGridCellMouseEnter;
			_folderBox.grFiles.CellMouseLeave += OnGridCellMouseLeave;
			_folderBox.grFiles.MouseUp += OnGridCellMouseUp;
			_folderBox.grFiles.MouseDown += OnGridCellMouseDown;
			_folderBox.grFiles.MouseMove += OnGridCellMouseMove;
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

		private void OnGridCellMouseUp(object sender, MouseEventArgs e)
		{
			var ht = _folderBox.grFiles.HitTest(e.X, e.Y);
			if (ht.Type != DataGridViewHitTestType.Cell) return;
			var linkRow = (LinkRow)_folderBox.grFiles.Rows[ht.RowIndex];
			if (linkRow.Source is LibraryObjectLink)
				LinkManager.OpenLink((LibraryObjectLink)linkRow.Source, e.Button == MouseButtons.Right);
		}

		private void OnGridCellMouseDown(object sender, MouseEventArgs e)
		{
			var ht = _folderBox.grFiles.HitTest(e.X, e.Y);
			if (ht.Type == DataGridViewHitTestType.Cell)
			{
				_hitTest = ht;
				_dragBox = new Rectangle(
					new Point(e.X - (SystemInformation.DragSize.Width / 2),
						e.Y - (SystemInformation.DragSize.Height / 2)),
					SystemInformation.DragSize);
			}
			else
				_hitTest = null;
		}

		private void OnGridCellMouseMove(object sender, MouseEventArgs e)
		{
			if (_hitTest == null || _dragBox.Contains(e.X, e.Y)) return;
			var linkRow = (LinkRow)_folderBox.grFiles.Rows[_hitTest.RowIndex];
			var fileLink = linkRow.Source as LibraryFileLink;
			if (fileLink == null || fileLink.IsFolder)
				return;
			var data = new DataObject();
			data.SetData(DataFormats.FileDrop, new[] { fileLink.FullPath });
			data.SetData(DataFormats.StringFormat, fileLink.FullPath);
			data.SetData(DataFormats.Serializable, fileLink);
			_folderBox.DoDragDrop(data, DragDropEffects.Copy);
			_hitTest = null;
		}
	}
}
