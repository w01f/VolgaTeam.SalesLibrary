using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDepot.Services.IPadAdminService
{
	public class UserInfo
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public List<GroupRecord> Groups { get; private set; }
		public List<LibraryPage> Pages { get; private set; }

		public UserInfo()
		{
			Groups = new List<GroupRecord>();
			Pages = new List<LibraryPage>();
		}
	}
}
