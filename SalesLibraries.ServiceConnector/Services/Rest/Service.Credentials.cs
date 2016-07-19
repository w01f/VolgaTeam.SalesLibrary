using System;

namespace SalesLibraries.ServiceConnector.Services.Rest
{
	public partial class RestServiceConnection
	{
		public string Website { get; private set; }

		public bool IsConnected => !String.IsNullOrEmpty(Website);

		public void Load(string website)
		{
			Website = website;
		}
	}
}
