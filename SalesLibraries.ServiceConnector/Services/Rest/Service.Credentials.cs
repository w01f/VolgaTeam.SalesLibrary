using System;

namespace SalesLibraries.ServiceConnector.Services.Rest
{
	public partial class RestServiceConnection
	{
		public string Website { get; private set; }
		public string Controller { get; private set; }

		public bool IsConnected => !String.IsNullOrEmpty(Website);

		public void Load(string website, string controller)
		{
			Website = website;//"http://localhost/SalesLibraries"
			Controller = controller;
		}
	}
}
