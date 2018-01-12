using System;
using System.Collections.Generic;
using System.IO;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class SourceFolderImageGroup : ImageSourceGroup
	{
		protected SourceFolderImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList)
		{
			_sourcePath = sourcePath;
		}

		public virtual void LoadImages<TImageSource>() where TImageSource : BaseImageSource
		{
			Images.Clear();

			foreach (var filePath in GetSourceFiles())
			{
				var linkImageSource = (TImageSource)Activator.CreateInstance(typeof(TImageSource), filePath);
				Images.Add(linkImageSource);
			}

			Images.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.FileName, y.FileName));
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		protected virtual IList<string> GetSourceFiles()
		{
			if (!Directory.Exists(_sourcePath))
				return new string[] { };
			var sourceFiles = Directory.GetFiles(_sourcePath, "*.png");
			return sourceFiles;
		}
	}
}
