using System;

namespace SalesLibraries.SalesDepot.Business.Gallery
{
	public class SnapshotItem
	{
		public string Name { get; private set; }
		public string Url { get; private set; }

		public SnapshotItem(string name, string url)
		{
			Url = url;
			Name = name.Trim().TrimStart(new[] { '-' }).Replace(" - ", "-").Replace("-", " - ");
		}

		public override String ToString()
		{
			return Name;
		}
	}
}
