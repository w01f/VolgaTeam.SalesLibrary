using System;
using Newtonsoft.Json;
using SalesLibraries.ServiceConnector.FileManagerResourcesService;

namespace SalesLibraries.FileManager.Business.Models.Security
{
	class LibraryUser
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[JsonIgnore]
		public bool Selected { get; set; }

		public string FullName => String.Format("{0} {1}", FirstName, LastName);

		public static LibraryUser LoadFromCloudData(UserModel cloudUser)
		{
			var user = new LibraryUser();
			user.Id = cloudUser.id;
			user.Login = cloudUser.login;
			user.FirstName = cloudUser.firstName;
			user.LastName = cloudUser.lastName;
			return user;
		}
	}
}
