namespace SalesLibraries.Common.Authorization
{
	public class AuthorizingEventArgs
	{
		public bool Authorized { get; set; }
		public string AuthServer { get; private set; }
		public AuthorizingEventArgs(string authService)
		{
			Authorized = true;
			AuthServer = authService;
		}
	}
}
