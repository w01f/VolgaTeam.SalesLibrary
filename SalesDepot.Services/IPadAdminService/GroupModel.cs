using System;
using System.Linq;

namespace SalesDepot.Services.IPadAdminService
{
	public partial class GroupModel
	{
		public override string ToString()
		{
			return name;
		}

		public bool IsNew { get; set; }

		public string AssignedObjects
		{
			get
			{
				var result = string.Empty;
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
					result += "ALL";
				return result;
			}
		}
	}
}
