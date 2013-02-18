using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDepot.Services.IPadAdminService
{
	public partial class LibraryPage
	{
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
				result += "Assigned Groups: ";
				if (groups != null)
				{
					if (groups.Any(x => !x.selected))
					{
						if (groups.Any(x => x.selected))
							result += string.Join(", ", groups.Where(x => x.selected).Select(x => x.name).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "None";
				return result;
			}
		}
	}
}
