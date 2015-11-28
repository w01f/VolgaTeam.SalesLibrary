using System;

namespace SalesLibraries.Common.Synchronization
{
	public sealed class SynchronizedEventArgs : EventArgs
	{
		public string SourceFilePath { get; private set; }
		public string DestinationFilePath { get; private set; }
		public object State { get; set; }
		public SynchronizationOperation Operation { get; private set; }

		public SynchronizedEventArgs(string sourceFilePath, string destinationFilePath, SynchronizationOperation operation)
		{
			SourceFilePath = sourceFilePath;
			DestinationFilePath = destinationFilePath;
			Operation = operation;
		}

		public SynchronizedEventArgs(SynchronizingEventArgs args)
		{
			SourceFilePath = args.SourceFilePath;
			DestinationFilePath = args.DestinationFilePath;
			State = args.State;
			Operation = args.Operation;
		}
	}
}