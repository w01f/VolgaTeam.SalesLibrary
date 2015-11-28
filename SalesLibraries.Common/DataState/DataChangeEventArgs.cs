using System;

namespace SalesLibraries.Common.DataState
{
	public abstract class DataChangeEventArgs : EventArgs
	{
		public abstract DataChangeType ChangeType { get; }
	}
}
