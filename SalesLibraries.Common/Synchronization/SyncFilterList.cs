using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesLibraries.Common.Synchronization
{
	public abstract class SyncFilterList
	{
		protected List<string> _sourceItems = new List<string>();
		public SyncFilterType FilterType { get; private set; }

		protected SyncFilterList(IEnumerable<string> items, SyncFilterType filterType)
		{
			FilterType = filterType;
			_sourceItems.AddRange(items.Distinct());
		}

		protected abstract void Init();

		public abstract bool IsMatchFile(string targetItem);
		public abstract bool IsMatchDirectory(string targetItem);

		public static SyncFilterList Create(IEnumerable<string> items, SyncFilterType filterType)
		{
			SyncFilterList filterList = null;
			switch (filterType)
			{
				case SyncFilterType.ByWhiteList:
					filterList = new WhiteListFilter(items, filterType);
					break;
				case SyncFilterType.ByExtensions:
					filterList = new ExtensionsListFilter(items, filterType);
					break;
			}
			if (filterList != null)
			{
				filterList.Init();
				return filterList;
			}
			throw new NotImplementedException("Filter list of this type is not realized");
		}
	}
}
