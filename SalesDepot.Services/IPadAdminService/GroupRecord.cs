using System;
using System.Linq;

namespace SalesDepot.Services.IPadAdminService
{
	public partial class GroupRecord
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
				string result = string.Empty;
				result += "Assigned Users: ";
				if (users != null)
				{
					if (users.Any(x => !x.selected))
					{
						if (users.Any(x => x.selected))
							result += string.Join(", ", users.Where(x => x.selected).Select(x => x.login).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "None";
				result += Environment.NewLine;
				result += "Assigned Libraries: ";
				if (libraries != null)
				{
					if (libraries.Any(x => !x.selected))
					{
						if (libraries.Any(x => x.selected))
							result += string.Join(", ", libraries.Where(x => x.selected).Select(x => x.name).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "ALL";
				return result;
			}
		}
	}
}
