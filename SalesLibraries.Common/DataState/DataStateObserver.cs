using System;
using System.Collections.Generic;

namespace SalesLibraries.Common.DataState
{
	public class DataStateObserver
	{
		public static DataStateObserver Instance { get; } = new DataStateObserver();

		public event EventHandler<DataChangeEventArgs> DataChanged;

		private DataStateObserver() { }

		public void RaiseLibrarySelected()
		{
			DataChanged?.Invoke(this, new LibrarySelectedEventArgs());
		}

		public void RaiseLinksDeleted(IEnumerable<Guid> linkIds)
		{
			DataChanged?.Invoke(this, new LinksDeletedEventArgs(linkIds));
		}
	}
}
