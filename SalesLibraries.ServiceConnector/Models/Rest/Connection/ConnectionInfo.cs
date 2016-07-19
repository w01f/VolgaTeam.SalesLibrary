using System;

namespace SalesLibraries.ServiceConnector.Models.Rest.Connection
{
	public class ConnectionInfo
	{
		public ConnectionState State { get; set; }
		public string User { get; set; }
		public DateTime ConnectionTime { get; set; }
		public Guid LibraryId { get; set; }
		public string LibraryName { get; set; }
	}
}
