using System;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.Preview
{
	public partial class CoverControl : UserControl, IBundleItemPreviewControl
	{
		public Guid BundleItemId { get; }

		public CoverControl(CoverItem bundleItem)
		{
			InitializeComponent();

			BundleItemId = bundleItem.Id;

			pictureEditImage.Image = bundleItem.Logo;
		}
	}
}
