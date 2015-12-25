using System;
using System.Collections.Generic;

namespace SalesLibraries.SalesDepot.Business.Gallery
{
	public class SnapshotCollection
	{
		public string Name { get; private set; }

		public SnapshotCollection(string name)
		{
			Name = name;
			Screenshots = new List<SnapshotItem>();
		}

		public override String ToString()
		{
			return Name;
		}

		public List<SnapshotItem> Screenshots { get; private set; }
	}
}
