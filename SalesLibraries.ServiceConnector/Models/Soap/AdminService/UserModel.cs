using System;
using System.Linq;

namespace SalesLibraries.ServiceConnector.AdminService
{
	public partial class UserModel
	{
		public string FullName => (firstName + " " + lastName).Trim();

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

		public DateTime? CreateDate
		{
			get
			{
				if (DateTime.TryParse(dateModify, out var temp))
					return temp;
				if (DateTime.TryParse(dateAdd, out temp))
					return temp;
				return null;
			}
		}

		public bool IsModified => DateTime.TryParse(dateModify, out DateTime _);

		public string LoginWithName => string.Format("{0} ({1})", login, FullName);

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
