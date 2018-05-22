using System;
using System.Linq;

namespace SalesLibraries.ServiceConnector.InactiveUsersService
{
	public partial class UserModel
	{
		public bool Selected { get; set; }

		public string FullName => (firstName + " " + lastName).Trim();

		public DateTime? LastActivityDate
		{
			get
			{
				if (String.IsNullOrEmpty(dateLastActivity))
					return null;
				if (DateTime.TryParse(dateLastActivity, out var temp))
					return temp;
				return null;
			}
		}

		public string[] GroupNameList
		{
			get
			{
				return !string.IsNullOrEmpty(groupNames) ? groupNames.Split(',').Select(x => x.Trim()).ToArray() : new string[] { };
			}
		}
	}
}
