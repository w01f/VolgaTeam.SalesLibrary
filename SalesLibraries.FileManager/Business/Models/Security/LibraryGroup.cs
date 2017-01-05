using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.ServiceConnector.Models.Rest.Dictionaries;

namespace SalesLibraries.FileManager.Business.Models.Security
{
	class LibraryGroup
	{
		public string Id { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public bool Selected { get; set; }

		public List<LibraryUser> Users { get; set; }

		public List<string> AssignedLibraryIds { get; set; }

		public LibraryGroup()
		{
			Users = new List<LibraryUser>();
			AssignedLibraryIds = new List<string>();
		}

		public static LibraryGroup LoadFromCloudData(SiteUserGroup cloudGroup)
		{
			var group = new LibraryGroup();
			group.Id = cloudGroup.Id;
			group.Name = cloudGroup.Name;
			group.AssignedLibraryIds.AddRange(cloudGroup.LibraryIds);
			group.Users.AddRange(cloudGroup.Users.Select(LibraryUser.LoadFromCloudData));
			return group;
		}
	}
}
