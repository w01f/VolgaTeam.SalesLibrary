using System;

namespace SalesLibraries.ServiceConnector.Models.Rest.Wallbin
{
	public class CloudLibraryException : Exception
	{
		public string ServiceErrorMessage { get; private set; }

		public CloudLibraryException(string message) : base(message)
		{
			ServiceErrorMessage = message;
		}

		public CloudLibraryException(string message, Exception exception) : base(message, exception)
		{
			ServiceErrorMessage = message;
		}
	}
}
