using System;
using System.Drawing;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.SalesDepot.Business.Gallery
{
	public class GalleryImageSource
	{
		public const decimal BigHeight = 146;
		public const decimal SmallHeight = 75;
		public const decimal TinyHeight = 58;
		public const decimal XtraTinyHeight = 41;
		public const decimal BigWidth = 321;
		public const decimal SmallWidth = 164;
		public const decimal TinyWidth = 128;
		public const decimal XtraTinyWidth = 90;

		public Image OriginalImage { get; set; }
		public Image BigImage { get; set; }
		public Image SmallImage { get; set; }
		public Image TinyImage { get; set; }
		public Image XtraTinyImage { get; set; }
		public string Name { get; set; }
		public string FileName { get; set; }

		public GalleryImageSource Clone()
		{
			var result = new GalleryImageSource();
			if (OriginalImage != null && BigImage != null && SmallImage != null && TinyImage != null && XtraTinyImage != null)
			{
				result.OriginalImage = OriginalImage.Clone() as Image;
				result.BigImage = BigImage.Clone() as Image;
				result.SmallImage = SmallImage.Clone() as Image;
				result.TinyImage = TinyImage.Clone() as Image;
				result.XtraTinyImage = XtraTinyImage.Clone() as Image;
			}
			result.Name = Name;
			return result;
		}

		public void Dispose()
		{
			if (OriginalImage != null)
				OriginalImage.Dispose();
			OriginalImage = null;
			if (BigImage != null)
				BigImage.Dispose();
			BigImage = null;
			if (SmallImage != null)
				SmallImage.Dispose();
			SmallImage = null;
			if (TinyImage != null)
				TinyImage.Dispose();
			TinyImage = null;
			if (XtraTinyImage != null)
				XtraTinyImage.Dispose();
			XtraTinyImage = null;
		}

		public static GalleryImageSource FromImage(Image image)
		{
			var imageSource = new GalleryImageSource();
			if (image != null)
			{
				imageSource.OriginalImage = new Bitmap(image);
				imageSource.BigImage = image.Resize(new Size((Int32)BigWidth, (Int32)BigHeight));
				imageSource.SmallImage = image.Resize(new Size((Int32)SmallWidth, (Int32)SmallHeight));
				imageSource.TinyImage = image.Resize(new Size((Int32)TinyWidth, (Int32)TinyHeight));
				imageSource.XtraTinyImage = image.Resize(new Size((Int32)XtraTinyWidth, (Int32)XtraTinyHeight));
			};
			return imageSource;
		}
	}
}
