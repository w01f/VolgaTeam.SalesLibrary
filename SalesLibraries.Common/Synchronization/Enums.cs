namespace SalesLibraries.Common.Synchronization
{
	public enum SynchronizationOperation
	{
		None,
		Add,
		Delete,
		Update,
	}

	public enum SynchronizationResult
	{
		Completed,
		AbortedDueToShutDown,
		AbortedDueToError
	}

	public enum SyncFilterType
	{
		ByWhiteList,
		ByExtensions,
	}
}
