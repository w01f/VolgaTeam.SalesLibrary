using System;

namespace SalesLibraries.ServiceConnector.Models.Rest.Common
{
	public class RestServiceException : Exception
	{
		public string ServiceErrorMessage { get; private set; }

		public RestServiceException(string message) : base(message)
		{
			ServiceErrorMessage = message;
		}

		public RestServiceException(string message, Exception exception) : base(message, exception)
		{
			ServiceErrorMessage = message;
		}
	}
}
