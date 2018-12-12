using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.Preview
{
	public partial class LibraryLinkControl : UserControl, IBundleItemPreviewControl
	{
		public Guid BundleItemId { get; }

		public LibraryLinkControl(LibraryLinkItem bundleItem)
		{
			InitializeComponent();

			BundleItemId = bundleItem.Id;
			if (bundleItem.TargetLink is InternalLibraryObjectLink internalLibraryObjectLink)
			{
				pictureEditImage.Image = internalLibraryObjectLink
					.GetThumbnailSourceUrl()
					.Select(url =>
					{
						var request = System.Net.WebRequest.Create(url);
						var response = request.GetResponse();
						var responseStream = response.GetResponseStream();
						var bmp = responseStream!= null?
							new Bitmap(responseStream):
							null;
						responseStream?.Dispose();
						return bmp;
					})
					.FirstOrDefault();
			}
			else if (bundleItem.TargetLink is IThumbnailSettingsHolder thumbnailLink)
				pictureEditImage.Image = thumbnailLink.GetThumbnailSourceFiles(null).Select(Image.FromFile).FirstOrDefault();
		}
	}
}
