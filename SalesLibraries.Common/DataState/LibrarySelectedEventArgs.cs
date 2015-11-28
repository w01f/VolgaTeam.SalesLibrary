namespace SalesLibraries.Common.DataState
{
	class LibrarySelectedEventArgs : DataChangeEventArgs
	{
		public override DataChangeType ChangeType
		{
			get { return DataChangeType.LibrarySelected; }
		}
	}
}
