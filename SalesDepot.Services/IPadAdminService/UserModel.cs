using System;
using System.Linq;

namespace SalesDepot.Services.IPadAdminService
{
	public partial class UserModel
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string Role
		{
			get
			{
				switch (role)
				{
					case 1:
						return "Admin";
					case 3:
						return "Advanced";
					default:
						return "User";
				}
			}
		}

		public string LoginWithName
		{
			get { return string.Format("{0} ({1})", login, FullName); }
		}

		public string AssignedObjects
		{
			get
			{
				var result = string.Empty;
				result += "Assigned Groups: ";
				if (allGroups)
					result += "ALL";
				else if (groups != null)
				{
					if (groups.Length > 0)
						result += string.Join(", ", groups.Select(x => x.name).ToArray());
					else
						result += "None";
				}
				else
					result += "None";
				result += Environment.NewLine;
				result += "Assigned Libraries: ";
				if (allLibraries)
					result += "ALL";
				else if (libraries != null)
				{
					if (libraries.Length > 0)
						result += string.Join(", ", libraries.Select(x => x.name).ToArray());
					else
						result += "None";
				}
				else
					result += "None";
				return result;
			}
		}
	}
}
