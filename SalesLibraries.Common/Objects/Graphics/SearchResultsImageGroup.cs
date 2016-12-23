using System;
using System.Linq;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class SearchResultsImageGroup : ImageSourceGroup
	{
		public SearchResultsImageGroup(IImageSourceList parentList) : base(parentList) { }

		public void LoadImages(string keyword)
		{
			Images.Clear();
			Images.AddRange(ParentList.Items
				.OfType<SourceFolderImageGroup>()
				.SelectMany(group => group.Images)
				.Where(image => image.FileName.ToUpper().Contains(keyword.ToUpper())));

			Images.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.FileName, y.FileName));
			DataChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
