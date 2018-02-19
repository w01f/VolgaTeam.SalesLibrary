using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Manina.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.Preview
{
	class BundleItemIconAdaptor : ImageListView.ImageListViewItemAdaptor
	{
		private readonly List<BaseBundleItem> _bundleItems = new List<BaseBundleItem>();

		public BundleItemIconAdaptor(IEnumerable<BaseBundleItem> bundleItems)
		{
			_bundleItems.AddRange(bundleItems);
		}

		public override Image GetThumbnail(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation,
			bool useWIC)
		{
			var guid = (Guid)key;
			return _bundleItems.Where(i => i.Id == guid).Select(i => (Image)i.Image.Clone()).FirstOrDefault();
		}

		public override string GetUniqueIdentifier(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails,
			bool useExifOrientation, bool useWIC)
		{
			var guid = (Guid)key;
			return guid.ToString();
		}

		public override string GetSourceImage(object key)
		{
			var guid = (Guid)key;
			return _bundleItems.Where(i => i.Id == guid).Select(i => i.Id.ToString()).FirstOrDefault();
		}

		public override Utility.Tuple<ColumnType, string, object>[] GetDetails(Object key, bool useWIC)
		{
			return null;
		}

		public override void Dispose()
		{
			_bundleItems.Clear();
		}
	}
}
