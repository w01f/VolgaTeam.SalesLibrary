using System;
using System.IO;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class ImportedImageGroup : SourceFolderImageGroup
	{
		protected ImportedImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		public void AddImage<TImageSource>(string imageFilePath) where TImageSource : BaseImageSource
		{
			try
			{
				if (!Directory.Exists(_sourcePath))
					Directory.CreateDirectory(_sourcePath);

				AddImageInternal(imageFilePath);

				LoadImages<TImageSource>();
			}
			catch (Exception e)
			{
			}
		}

		protected abstract void AddImageInternal(string imageFilePath);
	}
}
