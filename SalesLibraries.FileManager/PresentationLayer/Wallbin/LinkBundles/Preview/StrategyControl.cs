using System;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.Preview
{
	public partial class StrategyControl : UserControl, IBundleItemPreviewControl
	{
		public Guid BundleItemId { get; }

		public StrategyControl(StrategyItem bundleItem)
		{
			InitializeComponent();

			BundleItemId = bundleItem.Id;

			if (!String.IsNullOrEmpty(bundleItem.Header))
				simpleLabelItemHeader.Text = String.Format(simpleLabelItemHeader.Text, bundleItem.Header);
			else
			{
				simpleLabelItemHeader.Visibility = LayoutVisibility.Never;
				emptySpaceItem2.Visibility = LayoutVisibility.Never;
			}

			if (!String.IsNullOrEmpty(bundleItem.Body))
			{
				memoEditBody.EditValue = bundleItem.Body;
				memoEditBody.ForeColor = bundleItem.ForeColor;
				memoEditBody.BackColor = bundleItem.BackColor;
				memoEditBody.Font = bundleItem.Font;
				memoEditBody.Properties.Appearance.Font = bundleItem.Font;
				memoEditBody.Properties.AppearanceDisabled.Font = bundleItem.Font;
				memoEditBody.Properties.AppearanceFocused.Font = bundleItem.Font;
				memoEditBody.Properties.AppearanceReadOnly.Font = bundleItem.Font;
			}
			else
			{
				layoutControlItemBody.Visibility = LayoutVisibility.Never;
				emptySpaceItem2.Visibility = LayoutVisibility.Never;
			}
		}
	}
}
