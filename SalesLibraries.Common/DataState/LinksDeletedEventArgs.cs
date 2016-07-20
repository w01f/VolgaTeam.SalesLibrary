using System;
using System.Collections.Generic;

namespace SalesLibraries.Common.DataState
{
	public class LinksDeletedEventArgs : DataChangeEventArgs
	{
		public List<Guid> LinkIds { get; private set; }

		public override DataChangeType ChangeType => DataChangeType.LinksDeleted;

		public LinksDeletedEventArgs(IEnumerable<Guid> linkIds)
		{
			LinkIds = new List<Guid>();
			LinkIds.AddRange(linkIds);
		}
	}
}
