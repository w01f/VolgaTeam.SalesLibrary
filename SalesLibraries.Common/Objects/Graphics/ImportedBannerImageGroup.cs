using System;
using System.Drawing;
using System.IO;
using SalesLibraries.Common.Extensions;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class ImportedBannerImageGroup : ImportedImageGroup
	{
		public ImportedBannerImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		protected override void AddImageInternal(string imageFilePath)
		{
			var imageName = Path.GetFileNameWithoutExtension(imageFilePath);
			File.Copy(imageFilePath, Path.Combine(_sourcePath, String.Format("{0}_xl.png", imageName)));
			using (var originalImage = Image.FromFile(imageFilePath))
			{
				using (var largeImage = originalImage.Resize(new Size((Int32)(originalImage.Width * 0.7), originalImage.Height)))
				{
					largeImage.Save(Path.Combine(_sourcePath, String.Format("{0}_l.png", imageName)));
				}
				using (var mediumImage = originalImage.Resize(new Size((Int32)(originalImage.Width * 0.5), originalImage.Height)))
				{
					mediumImage.Save(Path.Combine(_sourcePath, String.Format("{0}_md.png", imageName)));
				}
				using (var smallImage = originalImage.Resize(new Size((Int32)(originalImage.Width * 0.3), originalImage.Height)))
				{
					smallImage.Save(Path.Combine(_sourcePath, String.Format("{0}_sm.png", imageName)));
				}
				using (var extraSmallImage = originalImage.Resize(new Size((Int32)(originalImage.Width * 0.15), originalImage.Height)))
				{
					extraSmallImage.Save(Path.Combine(_sourcePath, String.Format("{0}_xs.png", imageName)));
				}
			}
		}
	}
}
