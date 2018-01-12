using System;
using System.Collections.Generic;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class ImageSourceGroup
	{
		protected string _sourcePath;
		protected IImageSourceList ParentList { get; }
		public string Name { get; set; }
		public int Order { get; set; }

		public List<BaseImageSource> Images { get; }

		public EventHandler<EventArgs> DataChanged;

		protected ImageSourceGroup(IImageSourceList parentList)
		{
			ParentList = parentList;
			Images = new List<BaseImageSource>();
		}
	}
}
