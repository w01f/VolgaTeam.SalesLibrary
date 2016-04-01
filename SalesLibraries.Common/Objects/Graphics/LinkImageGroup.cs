using System;
using System.Collections.Generic;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class LinkImageGroup
	{
		protected const string IgnoredListFileName = "ignored.txt";

		protected string _sourcePath;
		protected IImageSourceList ParentList { get; private set; }
		public string Name { get; set; }
		public int Order { get; set; }

		public List<LinkImageSource> Images { get; private set; }

		public EventHandler<EventArgs> DataChanged;

		protected LinkImageGroup(IImageSourceList parentList)
		{
			ParentList = parentList;
			Images = new List<LinkImageSource>();
		}
	}
}
