using System;
using System.Collections.Generic;

namespace SalesLibraries.Common.DataState
{
	public class DataStateObserver
	{
		private readonly static DataStateObserver _instance = new DataStateObserver();

		public static DataStateObserver Instance
		{
			get { return _instance; }
		}

		public event EventHandler<DataChangeEventArgs> DataChanged;

		private DataStateObserver() { }

		public void RaiseLibrarySelected()
		{
			if (DataChanged != null)
				DataChanged(this, new LibrarySelectedEventArgs());
		}

		public void RaiseLinksDeleted(IEnumerable<Guid> linkIds)
		{
			if (DataChanged != null)
				DataChanged(this, new LinksDeletedEventArgs(linkIds));
		}
	}
}
