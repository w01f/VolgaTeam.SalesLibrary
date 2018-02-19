using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using Manina.Windows.Forms;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.Preview
{
	public partial class FormPreview : MetroForm
	{
		private readonly LinkBundle _linkBundle;
		private readonly BundleItemIconAdaptor _bundleItemIconAdaptor;

		private ImageListViewItem _selectedItem;

		public FormPreview(LinkBundle linkBundle)
		{
			_linkBundle = linkBundle;
			_bundleItemIconAdaptor = new BundleItemIconAdaptor(_linkBundle.Settings.Items);

			InitializeComponent();

			Load += OnFormLoad;
			imageListViewList.ItemClick += OnListItemClick;

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnFormLoad(Object sender, EventArgs e)
		{
			LoadItems();
		}

		private void LoadItems()
		{
			imageListViewList.Items.Clear();

			var imageListViewItems = _linkBundle.Settings.Items
				.Where(bundleItem => bundleItem.Visible)
				.OrderBy(bundleItem => bundleItem.CollectionOrder)
				.Select(bundleItem => new ImageListViewItem(bundleItem.Id)
				{
					Tag = bundleItem,
					Text = bundleItem.Name,
				}
				)
				.ToArray();
			if (!imageListViewItems.Any()) return;

			imageListViewList.Items.AddRange(imageListViewItems, _bundleItemIconAdaptor);
			OnListItemClick(imageListViewList, new ItemClickEventArgs(imageListViewList.Items.First(), -1, Cursor.Position, MouseButtons.None));
		}

		private void OnListItemClick(Object sender, ItemClickEventArgs e)
		{
			if (e.Item == _selectedItem) return;
			imageListViewList.ClearSelection();
			var bundleItem = (BaseBundleItem)e.Item.Tag;

			var bundleItemControl = panelContainer.Controls.OfType<IBundleItemPreviewControl>()
				.FirstOrDefault(control => control.BundleItemId == bundleItem.Id);
			if (bundleItemControl == null)
			{
				switch (bundleItem)
				{
					case CoverItem coverItem:
						bundleItemControl = new CoverControl(coverItem);
						break;
					case LaunchScreenItem launchScreenItem:
						bundleItemControl = new LaunchScreenControl(launchScreenItem);
						break;
					case InfoItem infoItem:
						bundleItemControl = new InfoControl(infoItem);
						break;
					case StrategyItem strategyItem:
						bundleItemControl = new StrategyControl(strategyItem);
						break;
					case LibraryLinkItem libraryLinkItem:
						if (libraryLinkItem.TargetLink is IThumbnailSettingsHolder)
							bundleItemControl = new LibraryLinkControl(libraryLinkItem);
						break;
				}
				if (bundleItemControl != null)
				{
					((Control)bundleItemControl).Dock = DockStyle.Fill;
					panelContainer.Controls.Add((Control)bundleItemControl);
				}
			}
			if (bundleItemControl != null)
			{
				((Control)bundleItemControl).BringToFront();

				e.Item.Selected = true;
				_selectedItem = e.Item;
			}
			else
				_selectedItem.Selected = true;

			panelContainer.Focus();
		}
	}
}