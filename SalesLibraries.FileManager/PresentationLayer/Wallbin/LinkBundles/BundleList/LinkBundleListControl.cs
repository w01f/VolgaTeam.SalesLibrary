using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.BundleList
{
	public partial class LinkBundleListControl : UserControl
	{
		private Library _library;
		private bool _loading;
		private GridDragDropHelper _bundlesDragDropHelper;
		private GridDragDropHelper _bundleItemsDragDropHelper;

		private LinkBundle SelectedBundle => gridViewBundles.GetFocusedRow() as LinkBundle;
		private BaseBundleItem SelectedBundleItem => gridViewBundleItems.GetFocusedRow() as BaseBundleItem;

		public LinkBundleListControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			DataStateObserver.Instance.DataChanged += (o, e) =>
			{
				if (e.ChangeType != DataChangeType.LinksDeleted) return;
				LoadBundleItems();
			};
		}

		public void LoadData(Library library)
		{
			_library = library;
			LoadBundles();
		}

		private void RaiseDataChanged()
		{
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
		}

		#region Bundles

		private void LoadBundles(int selectedBundleIndex = 0)
		{
			_loading = true;

			_library.LinkBundles.Sort();
			gridControlBundles.DataSource = _library.LinkBundles;
			gridControlBundles.RefreshDataSource();
			gridViewBundles.FocusedRowHandle = selectedBundleIndex;
			gridViewBundles.RefreshRow(gridViewBundles.FocusedRowHandle);

			if (_bundlesDragDropHelper == null && _library.LinkBundles.Any())
			{
				_bundlesDragDropHelper = new GridDragDropHelper(gridViewBundles, true);
				_bundlesDragDropHelper.AfterDrop += OnBundlesRowAfterDrop;
			}

			_loading = false;

			LoadBundleItems();
		}

		public void AddBundle()
		{
			using (var form = new FormBundleName())
			{
				form.Text = "New Link Bundle";
				form.Title = "Link Bundle Name";
				if (form.ShowDialog() != DialogResult.OK) return;
				_library.AddLinkBundle(form.BundleName);
				LoadBundles(_library.LinkBundles.Count - 1);
				RaiseDataChanged();
			}
		}

		private void EditBundle()
		{
			var currentBundleIndex = gridViewBundles.FocusedRowHandle;
			if (FormEditBundle.Run(SelectedBundle) != DialogResult.OK) return;
			LoadBundles(currentBundleIndex);
			RaiseDataChanged();
		}

		private void RenameBundle(LinkBundle targetBundle)
		{
			using (var form = new FormBundleName())
			{
				form.Text = "Rename Link Bundle";
				form.Title = "Link Bundle Name";
				form.BundleName = targetBundle.Name;
				if (form.ShowDialog() != DialogResult.OK) return;
				targetBundle.Name = form.BundleName;
				var currentBundleIndex = gridViewBundles.FocusedRowHandle;
				LoadBundles(currentBundleIndex);
				RaiseDataChanged();
			}
		}

		private void CloneBundle(LinkBundle targetBundle)
		{
			using (var form = new FormBundleName())
			{
				form.Text = "Clone Link Bundle";
				form.Title = "Link Bundle Name";
				form.BundleName = String.Format("{0}(Clone)", targetBundle.Name);
				if (form.ShowDialog() != DialogResult.OK) return;
				targetBundle.Clone(form.BundleName);
				var currentBundleIndex = gridViewBundles.FocusedRowHandle + 1;
				LoadBundles(currentBundleIndex);
				RaiseDataChanged();
			}
		}

		private void DeleteBundle()
		{
			if (SelectedBundle == null) return;
			var bundleToDelete = SelectedBundle;
			using (var form = new FormCustomDialog(
				String.Format("{0}{1}{2}",
					"<size=+4>Are you SURE you want to DELETE this Link Bundle?</size><br>",
					String.Format("<size=+2>{0}</size>", bundleToDelete.Name),
					"<br><br>*All Links to this bundle will be removed from your site"
				),
				new[]
				{
					new CustomDialogButtonInfo {Title = "DELETE", DialogResult = DialogResult.OK, Width = 100},
					new CustomDialogButtonInfo {Title = "CANCEL", DialogResult = DialogResult.Cancel, Width = 100}
				}
			))
			{
				form.Width = 500;
				form.Height = 160;
				if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
				{
					var currentBundleIndex = gridViewBundles.FocusedRowHandle;
					var bundleLinks = bundleToDelete.GetLibraryLinks();
					DataStateObserver.Instance.RaiseLinksDeleted(bundleLinks.Select(l => l.ExtId));
					_library.LinkBundles.RemoveItem(bundleToDelete);
					bundleToDelete.Delete(_library.Context);
					LoadBundles(currentBundleIndex);
					RaiseDataChanged();
				}
			}
		}

		private IEnumerable<DXMenuItem> GetBundleContextMenuItems(LinkBundle targetBundle)
		{
			var items = new List<DXMenuItem>();
			items.Add(new DXMenuItem("Rename", (o, args) =>
			{
				RenameBundle(targetBundle);
			}));
			items.Add(new DXMenuItem("Edit", (o, args) =>
			{
				EditBundle();
			}));
			items.Add(new DXMenuItem("Clone", (o, args) =>
			{
				CloneBundle(targetBundle);
			}));
			items.Add(new DXMenuItem("Delete", (o, args) =>
			{
				DeleteBundle();
			}));
			return items;
		}

		private void OnSwitchBundleItemsPanel(object sender, EventArgs e)
		{
			var isLinksVisible = splitContainerControl.PanelVisibility == SplitPanelVisibility.Both;
			if (isLinksVisible)
			{
				splitContainerControl.PanelVisibility = SplitPanelVisibility.Panel1;
				buttonXSwitchBundleItems.Text = "Show Bundle Items";
			}
			else
			{
				splitContainerControl.PanelVisibility = SplitPanelVisibility.Both;
				buttonXSwitchBundleItems.Text = "Hide Bundle Items";
			}
		}

		private void OnFocusedBundleRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			if (_loading) return;
			LoadBundleItems();
		}

		private void OnBundlesRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Clicks > 1 && e.Column == gridColumnBundlesName)
				EditBundle();
		}

		private void OnGridBundlesButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch ((String) e.Button.Tag)
			{
				case "Edit":
					EditBundle();
					break;
				case "Delete":
					DeleteBundle();
					break;
			}
		}

		private void OnBundlesRowAfterDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid?.MainView as GridView;
			var hitInfo = view?.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			if (hitInfo == null) return;
			var downHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
			if (downHitInfo == null) return;
			var sourceBundle = view.GetRow(downHitInfo.RowHandle) as LinkBundle;
			var targetRowIndex = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			((IList<LinkBundle>) _library.LinkBundles).ChangeItemPosition(sourceBundle, targetRowIndex);
			LoadBundles(targetRowIndex);
			RaiseDataChanged();
		}

		private void OnBundlesPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!(e.HitInfo.InRowCell || e.HitInfo.InRow || e.HitInfo.InDataRow)) return;
			e.Allow = true;
			var targetBundle = gridViewBundles.GetRow(e.HitInfo.RowHandle) as LinkBundle;
			if (targetBundle == null) return;
			e.Menu.Items.Clear();
			var items = GetBundleContextMenuItems(targetBundle).ToList();
			foreach (var menuItem in items)
				e.Menu.Items.Add(menuItem);
		}

		private void OnBundlesCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column == gridColumnBundlesActions)
			{
				if (e.RowHandle == gridViewBundles.FocusedRowHandle)
					e.RepositoryItem = repositoryItemButtonEditBundles;
				else
					e.RepositoryItem = null;
			}
		}
		#endregion

		#region Bundle Items

		private void LoadBundleItems(int selectedItemIndex = 0)
		{
			gridControlBundleItems.DataSource = SelectedBundle?.Settings.Items;
			gridViewBundleItems.RefreshData();
			gridViewBundleItems.FocusedRowHandle = selectedItemIndex;

			if (_bundleItemsDragDropHelper == null && SelectedBundle != null && SelectedBundle.Settings.Items.Any())
			{
				_bundleItemsDragDropHelper = new GridDragDropHelper(gridViewBundleItems, true, 40);
				_bundleItemsDragDropHelper.AfterDrop += OnBundleItemsRowAfterDrop;
			}
		}

		private void AddLibraryLinkBundleItem(LibraryObjectLink libraryObjectLink)
		{
			SelectedBundle.AddLibraryLink(libraryObjectLink).AssignDefaultImage();
			LoadBundleItems(SelectedBundle.Settings.Items.Count - 1);
			RaiseDataChanged();
		}

		private void DeleteBundleItem()
		{
			if (SelectedBundleItem == null) return;
			using (var form = new FormCustomDialog(
				String.Format("{0}{1}",
					"<size=+4>Are you SURE you want to DELETE this bundle item?</size><br>",
					String.Format("<size=+2>{0}</size>", SelectedBundleItem.Name)),
				new[]
				{
					new CustomDialogButtonInfo {Title = "DELETE", DialogResult = DialogResult.OK, Width = 100},
					new CustomDialogButtonInfo {Title = "CANCEL", DialogResult = DialogResult.Cancel, Width = 100}
				}
			))
			{
				form.Width = 500;
				form.Height = 160;
				if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
				{
					var currentItemIndex = gridViewBundleItems.FocusedRowHandle;
					SelectedBundle.Settings.Items.RemoveItem(SelectedBundleItem);
					LoadBundleItems(currentItemIndex);
					RaiseDataChanged();
				}
			}
		}

		private void OnBundleItemsEditorDoubleClick(object sender, EventArgs e)
		{
			EditBundle();
		}

		private void OnGridBundleItemsButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch ((String) e.Button.Tag)
			{
				case "Edit":
					EditBundle();
					break;
				case "Delete":
					DeleteBundleItem();
					break;
			}
		}

		private void OnBundleItemsRowAfterDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid?.MainView as GridView;
			var hitInfo = view?.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			if (hitInfo == null) return;
			var downHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
			if (downHitInfo == null) return;
			var sourceBundleItem = view.GetRow(downHitInfo.RowHandle) as BaseBundleItem;
			var targetRowIndex = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			SelectedBundle.Settings.Items.ChangeItemPosition(sourceBundleItem, targetRowIndex);
			LoadBundleItems(targetRowIndex);
			RaiseDataChanged();
		}

		private void OnGridBundleItemsDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data != null &&
			    e.Data.GetDataPresent(typeof(LinkRow)) &&
			    ((LinkRow) e.Data.GetData(typeof(LinkRow))).IsLinkBundleCompatible)
				e.Effect = DragDropEffects.Copy;
		}

		private void OnGridBundleItemsDragDrop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(typeof(LinkRow))) return;
			var libraryObjectLink = (e.Data.GetData(typeof(LinkRow)) as LinkRow)?.SourceObject;
			if (libraryObjectLink == null) return;
			AddLibraryLinkBundleItem(libraryObjectLink);
		}

		private void OnBundleItemsRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != gridColumnBundleItemsImage) return;
			if (e.Clicks < 2) return;
			using (var form = new FormImageGallery(MainController.Instance.Lists.LinkBundleImages))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				SelectedBundleItem.Image = (Image) form.OriginalImage.Clone();
				gridViewBundleItems.UpdateCurrentRow();
				RaiseDataChanged();
			}
		}

		private void OnGridBundleItemsRowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var bundleItem = ((GridView) sender).GetRow(e.RowHandle) as LibraryLinkItem;
			if (bundleItem != null && bundleItem.IsDead)
				e.Appearance.ForeColor = Color.Red;
		}

		#endregion
	}
}
