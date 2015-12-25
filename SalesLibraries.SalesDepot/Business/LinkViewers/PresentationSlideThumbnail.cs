using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace SalesLibraries.SalesDepot.Business.LinkViewers
{
	public class PresentationSlideThumbnail
	{
		public int Index { get; private set; }
		public Image SlideImage { get; private set; }

		public PresentationSlideThumbnail(string thumbnailPath)
		{
			Index = Int32.Parse(Regex.Match(Path.GetFileNameWithoutExtension(thumbnailPath), @"\d+").Value);
			SlideImage = Image.FromFile(thumbnailPath);
		}

		public override string ToString()
		{
			return Index.ToString(CultureInfo.InvariantCulture);
		}

		public void Release()
		{
			SlideImage.Dispose();
		}
	}
}
