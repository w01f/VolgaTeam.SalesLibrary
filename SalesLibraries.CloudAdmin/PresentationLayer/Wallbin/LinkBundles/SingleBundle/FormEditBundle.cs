using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.CloudAdmin.Business.Services;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.ImageGallery;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.LinkBundles.SingleBundle
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

			barLargeButtonItemLinksAddLaunchScreen.Caption = LaunchScreenItem.ItemName;
			barLargeButtonItemLinksAddInfo.Caption = InfoItem.ItemName;
			barLargeButtonItemLinksAddRevenue.Caption = RevenueItem.ItemName;
			barLargeButtonItemLinksAddStrategy.Caption = StrategyItem.ItemName;

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				xtraTabControl.AppearancePage.HeaderActive.Font = new Font(xtraTabControl.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControl.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControl.AppearancePage.HeaderActive.Font.Style);
				xtraTabControl.AppearancePage.Header.Font = new Font(xtraTabControl.AppearancePage.Header.Font.FontFamily, xtraTabControl.AppearancePage.Header.Font.Size - 2, xtraTabControl.AppearancePage.Header.Font.Style);
				xtraTabControl.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControl.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControl.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControl.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControl.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControl.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Style);

				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			}
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
			barLargeButtonItemLinksAddLaunchScreen.Enabled = !_linkBundle.Settings.Items.OfType<LaunchScreenItem>().Any();
			barLargeButtonItemLinksAddInfo.Enabled = !_linkBundle.Settings.Items.OfType<InfoItem>().Any();
			barLargeButtonItemLinksAddRevenue.Enabled = !_linkBundle.Settings.Items.OfType<RevenueItem>().Any();
			barLargeButtonItemLinksAddStrategy.Enabled = !_linkBundle.Settings.Items.OfType<StrategyItem>().Any();
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
			if (e.Column != gridColumnBundleItemsName) return;
			var bundleItem = gridViewBundleItems.GetRow(e.RowHandle);
			if (bundleItem is UrlItem)
				e.RepositoryItem = repositoryItemTextEditBundleItems;
			else
				e.RepositoryItem = repositoryItemButtonEditBundleItemsDisabledText;
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
			using (var form = new FormImageGallery(MainController.Instance.Lists.LinkBundleImages.Items))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				SelectedBundleItem.Image = Image.FromFile(form.SelectedImageSource.FilePath);
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
		#endregion
	}
}
