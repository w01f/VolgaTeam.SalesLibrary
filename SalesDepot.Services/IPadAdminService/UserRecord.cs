using System;
using System.Linq;

namespace SalesDepot.Services.IPadAdminService
{
	public partial class UserRecord
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string LoginWithName
		{
			get { return string.Format("{0} ({1})", login, FullName); }
		}

		public string AssignedObjects
		{
			get
			{
				string result = string.Empty;
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
					result += "None";
				return result;
			}
		}
	}
}
