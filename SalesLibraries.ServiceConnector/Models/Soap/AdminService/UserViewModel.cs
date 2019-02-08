using System;

namespace SalesLibraries.ServiceConnector.AdminService
{
    public partial class UserViewModel
    {
        public string FullName => (firstName + " " + lastName).Trim();

        public bool selected { get; set; }

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

        public string LoginWithName => $"{login} ({FullName})";

        public string AssignedObjects
        {
            get
            {
                var result = string.Empty;
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
                    result += "None";
                return result;
            }
        }
    }
}
