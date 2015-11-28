using System;

namespace SalesLibraries.Common.Synchronization
{
	public sealed class SynchronizationCompletingEventArgs : EventArgs
	{
		public SynchronizationResult Result { get; private set; }
		public Exception Exception { get; private set; }

		public SynchronizationCompletingEventArgs(SynchronizationResult result, Exception exception = null)
		{
			Result = result;
			Exception = exception;
		}
	}
}