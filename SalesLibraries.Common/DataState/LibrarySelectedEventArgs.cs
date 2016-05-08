namespace SalesLibraries.Common.DataState
{
	class LibrarySelectedEventArgs : DataChangeEventArgs
	{
		public override DataChangeType ChangeType => DataChangeType.LibrarySelected;
	}
}
