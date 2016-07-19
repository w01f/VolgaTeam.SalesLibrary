using System;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private const string Login = "fm_admin";
		private const string Password = "M>f0tcGwK>c4'[V";

		public string Website { get; private set; }

		public bool IsConnected => !String.IsNullOrEmpty(Website);

		public void Load(string website)
		{
			Website = website;
		}
	}
}
