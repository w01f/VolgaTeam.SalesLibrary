using System;

namespace SalesLibraries.ServiceConnector.AdminService
{
    public partial class LibraryPageViewModel
    {
        public override string ToString()
        {
            return name;
        }

        public bool selected { get; set; }

        public string AssignedObjects
        {
            get
            {
                string result = string.Empty;
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
                result += "Assigned Groups: ";
                if (allGroups)
                    result += "ALL";
                else if (assignedGroups != null)
                {
                    if (assignedGroups.Length > 0)
                        result += string.Join(", ", assignedGroups);
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
