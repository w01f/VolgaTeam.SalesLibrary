using System.Linq;

namespace SalesLibraries.ServiceConnector.InactiveUsersService
{
	public partial class UserModel
	{
		public bool Selected { get; set; }

		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string[] GroupNameList
		{
			get
			{
				return !string.IsNullOrEmpty(groupNames) ? groupNames.Split(',').Select(x => x.Trim()).ToArray() : new string[] { };
			}
		}
	}
}
