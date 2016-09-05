using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Clipboard
{
	class FolderClipboardManager
	{
		private readonly LibraryFolder _dataSource;
		private readonly ToolStripMenuItem _copyContainer;
		private readonly ToolStripMenuItem _moveContainer;

		public event EventHandler<FolderMovingEventArgs> FolderMoved;

		public FolderClipboardManager(
			LibraryFolder dataSource,
			ToolStripMenuItem copyContainer,
			ToolStripMenuItem moveContainer)
		{
			_dataSource = dataSource;
			_copyContainer = copyContainer;
			_moveContainer = moveContainer;
		}

		public void UpdateTargets()
		{
			var pages = _dataSource.Page.Library.Pages.Where(page => page != _dataSource.Page).ToList();

			_copyContainer.DropDownItems.Clear();
			_moveContainer.DropDownItems.Clear();

			foreach (var libraryPage in pages.OrderBy(p => p.Order))
			{
				_copyContainer.DropDownItems.Add(
					new ToolStripMenuItem(
						libraryPage.Name.Replace("&", "&&"),
						null,
						(sender, args) => CopyFolder(libraryPage)));
				_moveContainer.DropDownItems.Add(
					new ToolStripMenuItem(
						libraryPage.Name.Replace("&", "&&"),
						null,
						(sender, args) => MoveFolder(libraryPage)));
			}
		}

		private void AddFolder(LibraryPage targetPage, bool moveFolder = false)
		{
			var newFolder = _dataSource.Copy(moveFolder);
			var newColumnOrder = targetPage.Folders.Any() ? targetPage.Folders.Max(f => f.ColumnOrder) : 0;
			var newRowOrder = targetPage.Folders.Any(f => f.ColumnOrder == newColumnOrder) ? targetPage.Folders.Where(f => f.ColumnOrder == newColumnOrder).Max(f => f.RowOrder) : 0;
			targetPage.AddFolder(newFolder, newColumnOrder, newRowOrder);
		}

		private void CopyFolder(LibraryPage targetPage)
		{
			AddFolder(targetPage);
			FolderMoved?.Invoke(this, new FolderMovingEventArgs { TargetPage = targetPage, DeleteFromCurrent = false });
		}

		private void MoveFolder(LibraryPage targetPage)
		{
			AddFolder(targetPage, true);
			FolderMoved?.Invoke(this, new FolderMovingEventArgs { TargetPage = targetPage, DeleteFromCurrent = true });
		}
	}
}
