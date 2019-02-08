using System;
using System.Linq;

namespace SalesLibraries.ServiceConnector.InactiveUsersService
{
    public partial class UserViewModel
    {
        public bool Selected { get; set; }

        public string FullName => (firstName + " " + lastName).Trim();

        public DateTime? LastActivityDate
        {
            get
            {
                if (String.IsNullOrEmpty(dateLastActivity))
                    return null;
                if (DateTime.TryParse(dateLastActivity, out var temp))
                    return temp;
                return null;
            }
        }

        public string[] GroupNameList => assignedGroups ?? new string[] { };
    }
}
