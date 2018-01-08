using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class ImportedWidgetImageGroup : ImportedImageGroup
	{
		public ImportedWidgetImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		protected override void AddImageInternal(string imageFilePath)
		{
			var imageName = Path.GetFileNameWithoutExtension(imageFilePath);
			using (var originalImage = Image.FromFile(imageFilePath))
			{
				using (var largeImage = originalImage.Resize(new Size(32, 32)))
				{
					if (largeImage.Width < 32)
					{
						var paddingSize = (Int32)Math.Floor(((Double)32 - largeImage.Width) / 2);
						using (var largePaddingImage = largeImage.DrawPadding(new Padding(paddingSize, 0, paddingSize, 0)))
						{
							largePaddingImage.Save(Path.Combine(_sourcePath, String.Format("{0}_l.png", imageName)));
						}
					}
					else if (largeImage.Height < 32)
					{
						var paddingSize = (Int32)Math.Floor(((Double)32 - largeImage.Height) / 2);
						using (var largePaddingImage = largeImage.DrawPadding(new Padding(0, paddingSize, 0, paddingSize)))
						{
							largePaddingImage.Save(Path.Combine(_sourcePath, String.Format("{0}_l.png", imageName)));
						}
					}
					else
						largeImage.Save(Path.Combine(_sourcePath, String.Format("{0}_l.png", imageName)));
				}
				using (var smallImage = originalImage.Resize(new Size(24, originalImage.Height)))
				{
					var basePaddingSize = 4;
					if (smallImage.Width < 24)
					{
						var paddingSize = basePaddingSize + (Int32)Math.Floor(((Double)24 - smallImage.Width) / 2);
						using (var smallPaddingImage = smallImage.DrawPadding(new Padding(paddingSize, basePaddingSize, paddingSize, basePaddingSize)))
						{
							smallPaddingImage.Save(Path.Combine(_sourcePath, String.Format("{0}_sm.png", imageName)));
						}
					}
					else if (smallImage.Height < 24)
					{
						var paddingSize = basePaddingSize + (Int32)Math.Floor(((Double)24 - smallImage.Height) / 2);
						using (var smallPaddingImage = smallImage.DrawPadding(new Padding(basePaddingSize, paddingSize, basePaddingSize, paddingSize)))
						{
							smallPaddingImage.Save(Path.Combine(_sourcePath, String.Format("{0}_sm.png", imageName)));
						}
					}
					else
						using (var smallPaddingImage = smallImage.DrawPadding(new Padding(basePaddingSize)))
						{
							smallPaddingImage.Save(Path.Combine(_sourcePath, String.Format("{0}_sm.png", imageName)));
						}
				}
			}
		}
	}
}
