using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using SalesDepot.Services.IPadAdminService;
using SalesDepot.SiteManager.ToolClasses;

namespace SalesDepot.SiteManager.BusinessClasses
{
	public class ImportManager
	{
		public static IEnumerable<UserInfo> ImportUsers(string filePath, UserRecord[] existedUsers, GroupRecord[] existedGroups)
		{
			var userInfo = new List<UserInfo>();
			var existedGroupList = new List<GroupRecord>(existedGroups);
			var existedUserLogins = new List<string>(existedUsers.Select(x => x.login));

			var connnectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";", filePath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
				var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Authorized Users$]", connection);
				var dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataRow row in dataTable.Rows)
					{
						var firtsName = dataTable.Rows.Count > 0 && row[0] != null ? row[0].ToString() : string.Empty;
						var lastName = dataTable.Rows.Count > 1 && row[1] != null ? row[1].ToString() : string.Empty;
						var email = dataTable.Rows.Count > 2 && row[2] != null ? row[2].ToString() : string.Empty;
						var login = dataTable.Rows.Count > 3 && row[3] != null ? row[3].ToString() : string.Empty;
						var groups = dataTable.Rows.Count > 4 && row[4] != null ? row[4].ToString() : string.Empty;

						if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(email)) continue;
						if (existedUserLogins.Any(x => x.ToLower().Equals(login.ToLower().Trim()))) continue;

						var user = new UserInfo();
						user.Login = login.ToLower().Trim();
						user.Password = (new PasswordGenerator()).Generate();
						user.FirstName = firtsName.ToLower().Trim();
						user.LastName = lastName.ToLower().Trim();
						user.Email = email.ToLower().Trim();

						foreach (var groupName in groups.Split(','))
						{
							var group = existedGroupList.FirstOrDefault(x => x.name.ToLower().Equals(groupName.Trim().ToLower()));
							if (group == null)
							{
								group = new GroupRecord();
								group.IsNew = true;
								group.id = Guid.NewGuid().ToString();
								group.name = groupName;
								existedGroupList.Add(group);
							}
							user.Groups.Add(group);
						}
						existedUserLogins.Add(user.Login);
						userInfo.Add(user);
					}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				connection.Close();
			}
			catch { }
			return userInfo;
		}
	}
}
