using System;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.Preview
{
	public partial class LaunchScreenControl : UserControl, IBundleItemPreviewControl
	{
		public Guid BundleItemId { get; }

		public LaunchScreenControl(LaunchScreenItem bundleItem)
		{
			InitializeComponent();

			BundleItemId = bundleItem.Id;

			if (bundleItem.Logo != null)
				pictureEditLaunchScreenLogo.Image = bundleItem.Logo;
			else
			{
				layoutControlItemLaunchScreenLogo.Visibility = LayoutVisibility.Never;
				emptySpaceItemLogo1.Visibility = LayoutVisibility.Never;
				emptySpaceItemLogo2.Visibility = LayoutVisibility.Never;
			}

			if (bundleItem.Banner != null)
				pictureEditBanner.Image = bundleItem.Banner;
			else
				layoutControlItemBanner.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(bundleItem.Header))
			{
				memoEditHeader.EditValue = bundleItem.Header;
				memoEditHeader.ForeColor = bundleItem.HeaderForeColor;
				memoEditHeader.BackColor = bundleItem.HeaderBackColor;
				memoEditHeader.Font = bundleItem.HeaderFont;
				memoEditHeader.Properties.Appearance.Font = bundleItem.HeaderFont;
				memoEditHeader.Properties.AppearanceDisabled.Font = bundleItem.HeaderFont;
				memoEditHeader.Properties.AppearanceFocused.Font = bundleItem.HeaderFont;
				memoEditHeader.Properties.AppearanceReadOnly.Font = memoEditHeader.Font;
			}
			else
			{
				layoutControlItemHeader.Visibility = LayoutVisibility.Never;
				emptySpaceItemHeader.Visibility = LayoutVisibility.Never;
			}

			if (!String.IsNullOrEmpty(bundleItem.Footer))
			{
				memoEditFooter.EditValue = bundleItem.Footer;
				memoEditFooter.ForeColor = bundleItem.FooterForeColor;
				memoEditFooter.BackColor = bundleItem.FooterBackColor;
				memoEditFooter.Font = bundleItem.FooterFont;
				memoEditFooter.Properties.Appearance.Font = bundleItem.FooterFont;
				memoEditFooter.Properties.AppearanceDisabled.Font = bundleItem.FooterFont;
				memoEditFooter.Properties.AppearanceFocused.Font = bundleItem.FooterFont;
				memoEditFooter.Properties.AppearanceReadOnly.Font = bundleItem.FooterFont;
			}
			else
			{
				layoutControlItemFooter.Visibility = LayoutVisibility.Never;
				emptySpaceItemFooter.Visibility = LayoutVisibility.Never;
			}

			layoutControlItemLaunchScreenLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLaunchScreenLogo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLaunchScreenLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLaunchScreenLogo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
