using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesDepot.Services.FileManagerDataService;

namespace FileManager.BusinessClasses
{
	public class LibraryGroup
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

		public static LibraryGroup LoadFromCloudData(GroupModel cloudGroup)
		{
			var group = new LibraryGroup();
			group.Id = cloudGroup.id;
			group.Name = cloudGroup.name;
			group.AssignedLibraryIds.AddRange(cloudGroup.libraryIds);
			group.Users.AddRange(cloudGroup.users.Select(LibraryUser.LoadFromCloudData));
			return group;
		}
	}
}
