using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.Preview
{
	public partial class LibraryLinkControl : UserControl, IBundleItemPreviewControl
	{
		public Guid BundleItemId { get; }

		public LibraryLinkControl(LibraryLinkItem bundleItem)
		{
			InitializeComponent();

			BundleItemId = bundleItem.Id;

			if(bundleItem.TargetLink is IThumbnailSettingsHolder thumbnailLink)
				pictureEditImage.Image = thumbnailLink.GetThumbnailSourceFiles().Select(Image.FromFile).FirstOrDefault();
		}
	}
}
