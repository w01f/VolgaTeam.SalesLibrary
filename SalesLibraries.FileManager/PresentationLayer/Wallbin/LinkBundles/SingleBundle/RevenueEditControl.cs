using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	[IntendForClass(typeof(RevenueItem))]
	//public partial class RevenueEditControl : UserControl, ILinkBundleInfoEditControl
	public partial class RevenueEditControl : XtraTabPage, ILinkBundleInfoEditControl
	{
		private RevenueItem _bundleItem;
		private readonly List<BarItem> _infoTypeBarButtons = new List<BarItem>();
		private GridDragDropHelper _revenueInfoDragDropHelper;
		private RevenueInfo SelectedInfoItem => gridViewInfoItems.GetFocusedRow() as RevenueInfo;

		public RevenueEditControl(RevenueItem bundleItem)
		{
			_bundleItem = bundleItem;
			InitializeComponent();

			Text = RevenueItem.ItemName;

			var itemId = 100;
			foreach (var infoType in RevenueInfo.AvailableInfoTypes)
			{
				var barButton = new BarLargeButtonItem();
				barButton.Caption = infoType;
				barButton.Tag = infoType;
				barButton.Id = itemId;
				barButton.ItemClick += OnRevenueInfoItemAdd;

				_infoTypeBarButtons.Add(barButton);

				itemId++;
				barSubItemInfoAdd.LinksPersistInfo.Add(new LinkPersistInfo(barButton));
			}

			buttonEditTextFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditTextFont.Click += EditorHelper.OnFontEditClick;

			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void LoadData()
		{
			switch (_bundleItem.RevenueType)
			{
				case LinkBundleRevenueType.Generated:
					checkEditRevenueGenerated.Checked = true;
					checkEditRevenueGoal.Checked = false;
					break;
				case LinkBundleRevenueType.Goal:
					checkEditRevenueGenerated.Checked = false;
					checkEditRevenueGoal.Checked = true;
					break;
			}

			checkEditAdditionalInfo.Checked = !String.IsNullOrEmpty(_bundleItem.AdditionalInfo);
			memoEditAdditionalInfo.EditValue = _bundleItem.AdditionalInfo;

			colorEditTextColor.Color = _bundleItem.ForeColor;
			colorEditBackColor.Color = _bundleItem.BackColor;
			buttonEditTextFont.Tag = _bundleItem.Font;
			buttonEditTextFont.EditValue = Utils.FontToString(_bundleItem.Font);

			LoadInfoItems();
		}

		public void SaveData()
		{
			if (checkEditRevenueGenerated.Checked)
				_bundleItem.RevenueType = LinkBundleRevenueType.Generated;
			else if (checkEditRevenueGoal.Checked)
				_bundleItem.RevenueType = LinkBundleRevenueType.Goal;
			_bundleItem.AdditionalInfo = memoEditAdditionalInfo.EditValue as String;

			_bundleItem.ForeColor = colorEditTextColor.Color;
			_bundleItem.BackColor = colorEditBackColor.Color;
			_bundleItem.Font = buttonEditTextFont.Tag as Font;

			SaveInfoItems();
		}

		public void Release()
		{
			gridControlInfoItems.DataSource = null;
			_bundleItem = null;
		}

		private void LoadInfoItems()
		{
			gridControlInfoItems.DataSource = _bundleItem.InfoItems;
			gridViewInfoItems.RefreshData();

			if (_revenueInfoDragDropHelper == null && _bundleItem != null && _bundleItem.InfoItems.Any())
			{
				_revenueInfoDragDropHelper = new GridDragDropHelper(gridViewInfoItems, true, 45);
				_revenueInfoDragDropHelper.AfterDrop += OnRevenueInfoRowAfterDrop;
			}

			foreach (var barButton in _infoTypeBarButtons)
				barButton.Enabled = !_bundleItem.InfoItems.Any(item => String.Equals(item.Title, barButton.Tag as String));
		}

		private void SaveInfoItems()
		{
			gridViewInfoItems.CloseEditor();
		}

		private void DeleteInfoItem()
		{
			if (SelectedInfoItem == null) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Do you want to remove Revenue info item?") == DialogResult.Yes)
			{
				_bundleItem.InfoItems.Remove(SelectedInfoItem);
				LoadInfoItems();
			}
		}

		private void OnRevenueInfoItemAdd(object sender, ItemClickEventArgs e)
		{
			var infoType = e.Item.Tag as String;
			_bundleItem.AddInfo(infoType);
			LoadInfoItems();
		}

		private void OnRevenueInfoRowAfterDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid?.MainView as GridView;
			var hitInfo = view?.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			if (hitInfo == null) return;
			var downHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
			if (downHitInfo == null) return;
			var sourceBundleItem = view.GetRow(downHitInfo.RowHandle) as RevenueInfo;
			var targetRowIndex = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			_bundleItem.InfoItems.ChangeItemPosition(sourceBundleItem, targetRowIndex);
			LoadInfoItems();
		}

		private void OnAdditionalInfoCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemAdditionalInfoEditor.Enabled = checkEditAdditionalInfo.Checked;
			layoutControlItemTextColor.Enabled = checkEditAdditionalInfo.Checked;
			layoutControlItemBackColor.Enabled = checkEditAdditionalInfo.Checked;
			layoutControlItemTextFont.Enabled = checkEditAdditionalInfo.Checked;
			if (!checkEditAdditionalInfo.Checked)
				memoEditAdditionalInfo.EditValue = null;
		}

		private void OnInfoItemsActionsButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch ((String)e.Button.Tag)
			{
				case "Delete":
					DeleteInfoItem();
					break;
			}
		}

		private void OnTextColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditAdditionalInfo.ForeColor = colorEditTextColor.Color;
		}

		private void OnBackColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditAdditionalInfo.BackColor = colorEditBackColor.Color;
		}

		private void OnFontEditValueChanged(object sender, EventArgs e)
		{
			memoEditAdditionalInfo.Font = (Font)buttonEditTextFont.Tag;
			memoEditAdditionalInfo.Properties.Appearance.Font = memoEditAdditionalInfo.Font;
			memoEditAdditionalInfo.Properties.AppearanceDisabled.Font = memoEditAdditionalInfo.Font;
			memoEditAdditionalInfo.Properties.AppearanceFocused.Font = memoEditAdditionalInfo.Font;
			memoEditAdditionalInfo.Properties.AppearanceReadOnly.Font = memoEditAdditionalInfo.Font;
		}
	}
}
