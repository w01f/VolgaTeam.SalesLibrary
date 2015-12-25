using System;

namespace SalesLibraries.Common.Synchronization
{
	public sealed class SynchronizingEventArgs : EventArgs
	{
		public string SourceFilePath { get; private set; }
		public string DestinationFilePath { get; private set; }
		public bool Cancel { get; set; }
		public object State { get; set; }
		public SynchronizationOperation Operation { get; private set; }

		public SynchronizingEventArgs(string sourceFilePath, SynchronizationOperation operation)
		{
			SourceFilePath = sourceFilePath;
			Operation = operation;
		}

		public SynchronizingEventArgs(string sourceFilePath, string destinationFilePath, SynchronizationOperation operation)
		{
			SourceFilePath = sourceFilePath;
			DestinationFilePath = destinationFilePath;
			Operation = operation;
		}
	}
}