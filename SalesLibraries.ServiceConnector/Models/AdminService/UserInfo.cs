using System.Collections.Generic;

namespace SalesLibraries.ServiceConnector.AdminService
{
	public class UserInfo
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public List<GroupModel> Groups { get; private set; }
		public List<LibraryPage> Pages { get; private set; }

		public UserInfo()
		{
			Groups = new List<GroupModel>();
			Pages = new List<LibraryPage>();
		}
	}
}
