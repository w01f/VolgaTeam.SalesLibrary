using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	public partial class FormEditBundle : MetroForm
	{
		private readonly LinkBundle _linkBundle;

		private FormEditBundle(LinkBundle linkBundle)
		{
			InitializeComponent();

			_linkBundle = linkBundle;

			Text = _linkBundle.Name;

			repositoryItemTextEditBundleItems.EnableSelectAll();
			repositoryItemMemoEditBundleItems.EnableSelectAll();

			barLargeButtonItemLinksAddCover.Caption = CoverItem.ItemName;
			barLargeButtonItemLinksAddLaunchScreen.Caption = LaunchScreenItem.ItemName;
			barLargeButtonItemLinksAddInfo.Caption = InfoItem.ItemName;
			barLargeButtonItemLinksAddRevenue.Caption = RevenueItem.ItemName;
			barLargeButtonItemLinksAddStrategy.Caption = StrategyItem.ItemName;

			gridColumnBundleItemsImage.Width = (Int32)(gridColumnBundleItemsImage.Width * Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
			gridColumnBundleItemsUseAsThumbnail.Width = (Int32)(gridColumnBundleItemsUseAsThumbnail.Width * Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
			gridColumnBundleItemsVisible.Width = (Int32)(gridColumnBundleItemsVisible.Width * Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
			gridColumnBundleItemsActions.Width = (Int32)(gridColumnBundleItemsActions.Width * Utils.GetScaleFactor(CreateGraphics().DpiX).Width);

			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemBarDockControlTop.MaxSize = RectangleHelper.ScaleSize(layoutControlItemBarDockControlTop.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public static DialogResult Run(LinkBundle linkBundle)
		{
			var dilogResult = DialogResult.Cancel;
			linkBundle.PerformTransaction(linkBundle.Library.Context,
				linkBundleCopy =>
				{
					using (var form = new FormEditBundle(linkBundleCopy))
					{
						dilogResult = form.ShowDialog(MainController.Instance.MainForm);
						if (dilogResult == DialogResult.OK)
							linkBundleCopy.MarkAsModified();
						return dilogResult == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...",
					(cancelationToken, formProgess) =>
					{
						original.Save(context, current, false);
					}));
			return dilogResult;
		}

		private void LoadData()
		{
			LoadBundleItems();
		}

		private void SaveData()
		{
			SaveBundleItems();
			xtraTabControl.TabPages.OfType<ILinkBundleInfoEditControl>().ToList().ForEach(infoEditor => infoEditor.SaveData());
		}

		private void OnFormEditBundleLoad(object sender, EventArgs e)
		{
			LoadData();
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveData();
		}

		#region Bundle Items
		private GridDragDropHelper _bundleItemsDragDropHelper;

		private BaseBundleItem SelectedBundleItem => gridViewBundleItems.GetFocusedRow() as BaseBundleItem;

		private void LoadBundleItems(int selectedItemIndex = 0)
		{
			gridControlBundleItems.DataSource = _linkBundle.Settings.Items;
			gridViewBundleItems.RefreshData();
			gridViewBundleItems.FocusedRowHandle = selectedItemIndex;

			if (_bundleItemsDragDropHelper == null && _linkBundle.Settings.Items.Any())
			{
				_bundleItemsDragDropHelper = new GridDragDropHelper(gridViewBundleItems, true, 45);
				_bundleItemsDragDropHelper.AfterDrop += OnBundleItemsRowAfterDrop;
			}
			UpdateInfoEditors();
			UpdateItemsAvailableToAdd();
		}

		private void SaveBundleItems()
		{
			gridViewBundleItems.CloseEditor();
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
				form.Size = RectangleHelper.ScaleSize(form.Size, Utils.GetScaleFactor(CreateGraphics().DpiX));
				if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
				{
					var currentItemIndex = gridViewBundleItems.FocusedRowHandle;
					_linkBundle.Settings.Items.RemoveItem(SelectedBundleItem);
					LoadBundleItems(currentItemIndex);
				}
			}
		}

		private void UpdateInfoEditors()
		{
			xtraTabControl.SuspendLayout();

			var existedInfoEditors = xtraTabControl.TabPages.OfType<ILinkBundleInfoEditControl>().ToList();
			foreach (var infoEditControl in existedInfoEditors)
			{
				infoEditControl.SaveData();
				infoEditControl.Release();
				xtraTabControl.TabPages.Remove((XtraTabPage)infoEditControl);
			}

			var newInfoEditors = new List<ILinkBundleInfoEditControl>();
			foreach (var bundleInfoItem in _linkBundle.Settings.Items.OfType<ILinkBundleInfoItem>().ToList())
			{
				newInfoEditors.AddRange(ObjectIntendHelper.GetObjectInstances(
						typeof(ILinkBundleInfoEditControl),
						bundleInfoItem.GetType(),
						bundleInfoItem)
					.OfType<ILinkBundleInfoEditControl>());
			}
			foreach (var infoEditControl in newInfoEditors)
				infoEditControl.LoadData();
			xtraTabControl.TabPages.AddRange(newInfoEditors.OfType<XtraTabPage>().ToArray());

			xtraTabControl.ResumeLayout();
		}

		private void UpdateItemsAvailableToAdd()
		{
			barLargeButtonItemLinksAddCover.Enabled = !_linkBundle.Settings.Items.OfType<CoverItem>().Any();
			barLargeButtonItemLinksAddLaunchScreen.Enabled = !_linkBundle.Settings.Items.OfType<LaunchScreenItem>().Any();
			barLargeButtonItemLinksAddInfo.Enabled = !_linkBundle.Settings.Items.OfType<InfoItem>().Any();
			barLargeButtonItemLinksAddRevenue.Enabled = !_linkBundle.Settings.Items.OfType<RevenueItem>().Any();
			barLargeButtonItemLinksAddStrategy.Enabled = !_linkBundle.Settings.Items.OfType<StrategyItem>().Any();
		}

		private void OnAddCoverItemClick(object sender, ItemClickEventArgs e)
		{
			_linkBundle.AddBundleItem<CoverItem>().AssignDefaultImage();
			LoadBundleItems();
		}

		private void OnAddLaunchScreebItemClick(object sender, ItemClickEventArgs e)
		{
			_linkBundle.AddBundleItem<LaunchScreenItem>().AssignDefaultImage();
			LoadBundleItems();
		}

		private void OnAddInfoItemClick(object sender, ItemClickEventArgs e)
		{
			_linkBundle.AddBundleItem<InfoItem>().AssignDefaultImage();
			LoadBundleItems();
		}

		private void OnAddRevenueItemClick(object sender, ItemClickEventArgs e)
		{
			_linkBundle.AddBundleItem<RevenueItem>().AssignDefaultImage();
			LoadBundleItems();
		}

		private void OnAddStrategyItemClick(object sender, ItemClickEventArgs e)
		{
			_linkBundle.AddBundleItem<StrategyItem>().AssignDefaultImage();
			LoadBundleItems();
		}

		private void OnAddUrlsItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormAddUrl())
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				_linkBundle.AddBundleItem<UrlItem>(form.Url).AssignDefaultImage();
				LoadBundleItems();
			}
		}

		private void OnBundleItemsCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			var bundleItem = gridViewBundleItems.GetRow(e.RowHandle);
			if (e.Column == gridColumnBundleItemsName)
			{
				if (bundleItem is UrlItem)
					e.RepositoryItem = repositoryItemTextEditBundleItems;
				else
					e.RepositoryItem = repositoryItemButtonEditBundleItemsDisabledText;
			}
			if (e.Column == gridColumnBundleItemsUseAsThumbnail)
			{
				if (bundleItem is LibraryLinkItem && ((LibraryLinkItem)bundleItem).ThumbnailAvailable)
					e.RepositoryItem = repositoryItemCheckEditBundleItems;
				else
				{
					repositoryItemCheckEditBundleItemsDisabled.Enabled = false;
					e.RepositoryItem = repositoryItemCheckEditBundleItemsDisabled;
				}
			}
		}

		private void OnBundleItemsShowingEditor(object sender, CancelEventArgs e)
		{
			if (gridViewBundleItems.FocusedColumn == gridColumnBundleItemsUseAsThumbnail)
			{
				var bundleItem = gridViewBundleItems.GetFocusedRow();
				e.Cancel = !(bundleItem is LibraryLinkItem && ((LibraryLinkItem)bundleItem).ThumbnailAvailable);
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
			_linkBundle.Settings.Items.ChangeItemPosition(sourceBundleItem, targetRowIndex);
			LoadBundleItems(targetRowIndex);
		}

		private void OnBundleItemsRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Clicks < 2) return;
			if (e.Column != gridColumnBundleItemsImage) return;
			using (var form = new FormImageGallery<Widget>(MainController.Instance.Lists.LinkBundleImages))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				SelectedBundleItem.Image = (Image)form.OriginalImage.Clone();
				gridViewBundleItems.UpdateCurrentRow();
			}
		}

		private void OnGridBundleItemsRowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var bundleItem = ((GridView)sender).GetRow(e.RowHandle) as LibraryLinkItem;
			if (bundleItem != null && bundleItem.IsDead)
				e.Appearance.ForeColor = Color.Red;
		}

		private void OnGridBundleItemsActionsButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch ((String)e.Button.Tag)
			{
				case "Delete":
					DeleteBundleItem();
					break;
			}
		}

		private void OnBundleItemsDisabledTextDoubleClick(object sender, EventArgs e)
		{
			switch (SelectedBundleItem.ItemType)
			{
				case LinkBundleItemType.LaunchScreen:
					xtraTabControl.SelectedTabPage = xtraTabControl.TabPages.OfType<LaunchScreenEditControl>().FirstOrDefault();
					break;
				case LinkBundleItemType.Cover:
					xtraTabControl.SelectedTabPage = xtraTabControl.TabPages.OfType<CoverEditControl>().FirstOrDefault();
					break;
				case LinkBundleItemType.Info:
					xtraTabControl.SelectedTabPage = xtraTabControl.TabPages.OfType<InfoEditControl>().FirstOrDefault();
					break;
				case LinkBundleItemType.Revenue:
					xtraTabControl.SelectedTabPage = xtraTabControl.TabPages.OfType<RevenueEditControl>().FirstOrDefault();
					break;
				case LinkBundleItemType.Strategy:
					xtraTabControl.SelectedTabPage = xtraTabControl.TabPages.OfType<StrategyEditControl>().FirstOrDefault();
					break;
			}
		}

		private void OnBundleItemsCellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == gridColumnBundleItemsUseAsThumbnail)
			{
				var bundleItem = gridViewBundleItems.GetRow(e.RowHandle) as BaseBundleItem;
				if (bundleItem != null && bundleItem.UseAsThumbnail)
				{
					foreach (var item in _linkBundle.Settings.Items.Where(item => item != bundleItem).ToList())
						item.UseAsThumbnail = false;
					gridViewBundleItems.RefreshData();
				}
			}
		}

		private void repositoryItemCheckEditBundleItems_CheckedChanged(object sender, EventArgs e)
		{
			gridViewBundleItems.CloseEditor();
		}
		#endregion
	}
}
