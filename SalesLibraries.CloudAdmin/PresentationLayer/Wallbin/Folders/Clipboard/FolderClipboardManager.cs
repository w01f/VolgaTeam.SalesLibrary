using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Utils;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Clipboard
{
	class FolderClipboardManager
	{
		private readonly LibraryFolder _dataSource;
		private readonly BarSubItem _copyContainer;
		private readonly BarSubItem _moveContainer;

		private readonly Dictionary<Guid, BarButtonItem> _pageForCopyBarItems = new Dictionary<Guid, BarButtonItem>();
		private readonly Dictionary<Guid, BarButtonItem> _pageForMoveBarItems = new Dictionary<Guid, BarButtonItem>();

		public event EventHandler<FolderMovingEventArgs> FolderMoved;

		public FolderClipboardManager(
			LibraryFolder dataSource,
			BarSubItem copyContainer,
			BarSubItem moveContainer)
		{
			_dataSource = dataSource;
			_copyContainer = copyContainer;
			_moveContainer = moveContainer;
		}

		public void UpdateTargets()
		{
			_copyContainer.LinksPersistInfo.Clear();
			_moveContainer.LinksPersistInfo.Clear();

			var barManager = _copyContainer.Manager;
			var maxId = _copyContainer.Manager.MaxItemId + 1;

			barManager.BeginInit();
			foreach (var libraryPage in _dataSource.Page.Library.Pages.OrderBy(p => p.Order))
			{
				BarButtonItem pageForCopyItem;
				if (_pageForCopyBarItems.ContainsKey(libraryPage.ExtId))
					pageForCopyItem = _pageForCopyBarItems[libraryPage.ExtId];
				else
				{
					pageForCopyItem = new BarButtonItem
					{
						Id = maxId,
						Caption = libraryPage.Name,
						ItemInMenuAppearance =
						{
							Normal = { TextOptions = { HotkeyPrefix = HKeyPrefix.None}},
							Disabled= { TextOptions = { HotkeyPrefix = HKeyPrefix.None}},
							Pressed= { TextOptions = { HotkeyPrefix = HKeyPrefix.None}},
							Hovered= { TextOptions = { HotkeyPrefix = HKeyPrefix.None}}
						}
					};
					pageForCopyItem.ItemClick += (o, e) => CopyFolder(libraryPage);
					barManager.Items.Add(pageForCopyItem);
					_pageForCopyBarItems.Add(libraryPage.ExtId, pageForCopyItem);
					maxId++;
				}
				_copyContainer.LinksPersistInfo.Add(new LinkPersistInfo(pageForCopyItem));

				if (libraryPage != _dataSource.Page)
				{
					BarButtonItem pageForMoveItem;
					if (_pageForMoveBarItems.ContainsKey(libraryPage.ExtId))
						pageForMoveItem = _pageForMoveBarItems[libraryPage.ExtId];
					else
					{
						pageForMoveItem = new BarButtonItem
						{
							Id = maxId,
							Caption = libraryPage.Name,
							ItemInMenuAppearance =
							{
								Normal = { TextOptions = { HotkeyPrefix = HKeyPrefix.None}},
								Disabled= { TextOptions = { HotkeyPrefix = HKeyPrefix.None}},
								Pressed= { TextOptions = { HotkeyPrefix = HKeyPrefix.None}},
								Hovered= { TextOptions = { HotkeyPrefix = HKeyPrefix.None}}
							}
						};
						pageForMoveItem.ItemClick += (o, e) => MoveFolder(libraryPage);
						barManager.Items.Add(pageForMoveItem);
						_pageForMoveBarItems.Add(libraryPage.ExtId, pageForMoveItem);
						maxId++;
					}
					_moveContainer.LinksPersistInfo.Add(new LinkPersistInfo(pageForMoveItem));
				}
			}
			barManager.MaxItemId = maxId;
			barManager.EndInit();
		}

		private void AddFolder(LibraryPage targetPage, bool moveFolder = false)
		{
			var isSampePage = targetPage == _dataSource.Page;
			var newFolder = _dataSource.Copy(moveFolder);
			var newColumnOrder = isSampePage ?
				_dataSource.ColumnOrder :
				targetPage.Folders.Any() ? targetPage.Folders.Max(f => f.ColumnOrder) : 0;
			var newRowOrder = isSampePage ?
				_dataSource.RowOrder :
				targetPage.Folders.Any(f => f.ColumnOrder == newColumnOrder) ? targetPage.Folders.Where(f => f.ColumnOrder == newColumnOrder).Max(f => f.RowOrder) : 0;
			if (isSampePage)
				targetPage.InsertFolder(newFolder, newColumnOrder, newRowOrder);
			else
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
