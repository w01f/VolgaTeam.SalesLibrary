using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.BusinessClasses
{
	public class LinkImageGroup
	{
		public string Name { get; set; }
		public int Order { get; set; }

		public List<LinkImageSource> Images { get; private set; }

		public EventHandler<EventArgs> OnDataChanged;

		public LinkImageGroup()
		{
			Images = new List<LinkImageSource>();
		}

		public void LoadImages<T>(string sourcePath) where T : LinkImageSource
		{
			Images.Clear();
			foreach (var filePath in Directory.GetFiles(sourcePath, "*.png"))
			{
				var linkImageSource = Activator.CreateInstance(typeof(T), filePath) as LinkImageSource;
				if (linkImageSource.Index > 0 && linkImageSource.Image != null)
					Images.Add(linkImageSource);
			}
			Images.Sort((x, y) => x.Index.CompareTo(y.Index));
			if (OnDataChanged != null)
				OnDataChanged(this, EventArgs.Empty);
		}
	}

	public class FavoriteImageGroup : LinkImageGroup { }
}
