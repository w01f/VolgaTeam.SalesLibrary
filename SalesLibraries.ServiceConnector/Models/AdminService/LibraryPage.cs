using System;
using System.Linq;

namespace SalesLibraries.ServiceConnector.AdminService
{
	public partial class LibraryPage
	{
		public string AssignedObjects
		{
			get
			{
				string result = string.Empty;
				result += "Assigned Users: ";
				if (allUsers)
					result += "ALL";
				else if (users != null)
				{
					if (users.Length > 0)
						result += string.Join(", ", users.Select(x => x.login).ToArray());
					else
						result += "None";
				}
				else
					result += "None";
				result += Environment.NewLine;
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
				return result;
			}
		}
	}
}
