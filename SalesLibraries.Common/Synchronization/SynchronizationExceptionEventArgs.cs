using System;

namespace SalesLibraries.Common.Synchronization
{
	public sealed class SynchronizationExceptionEventArgs : EventArgs
	{
		public string SourceFilePath { get; private set; }
		public string DestinationFilePath { get; private set; }
		public object State { get; set; }
		public Exception Exception { get; private set; }

		public SynchronizationExceptionEventArgs(string sourceFilePath, string destinationFilePath, Exception exception)
		{
			SourceFilePath = sourceFilePath;
			DestinationFilePath = destinationFilePath;
			Exception = exception;
		}

		public SynchronizationExceptionEventArgs(SynchronizingEventArgs args, Exception exception)
		{
			SourceFilePath = args.SourceFilePath;
			DestinationFilePath = args.DestinationFilePath;
			State = args.State;
			Exception = exception;
		}

		public SynchronizationExceptionEventArgs(SynchronizedEventArgs args, Exception exception)
		{
			SourceFilePath = args.SourceFilePath;
			DestinationFilePath = args.DestinationFilePath;
			State = args.State;
			Exception = exception;
		}
	}
}