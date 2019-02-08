using System;
using System.Linq;

namespace SalesLibraries.ServiceConnector.AdminService
{
    public partial class GroupViewModel
    {
        public override string ToString()
        {
            return name;
        }

        public bool IsNew { get; set; }
        public bool selected { get; set; }

        public string AssignedObjects
        {
            get
            {
                var result = string.Empty;
                result += "Assigned Users: ";
                if (allUsers)
                    result += "ALL";
                else if (assignedUsers != null)
                {
                    if (assignedUsers.Length > 0)
                        result += string.Join(", ", assignedUsers);
                    else
                        result += "None";
                }
                else
                    result += "None";
                result += Environment.NewLine;
                result += "Assigned Libraries: ";
                if (allLibraries)
                    result += "ALL";
                else if (assignedLibraries != null)
                {
                    if (assignedLibraries.Length > 0)
                        result += string.Join(", ", assignedLibraries);
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
