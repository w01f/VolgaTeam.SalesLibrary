﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web.Security;
using SalesDepot.Services.IPadAdminService;
using SalesDepot.SiteManager.ToolClasses;

namespace SalesDepot.SiteManager.BusinessClasses
{
	public class ImportManager
	{
		public static IEnumerable<UserInfo> ImportUsers(string filePath, UserModel[] existedUsers, GroupModel[] existedGroups, bool complexPassword, out string message)
		{
			message = string.Empty;
			var userInfo = new List<UserInfo>();
			var existedGroupList = new List<GroupModel>(existedGroups);
			var existedUserLogins = new List<string>(existedUsers.Select(x => x.login));

			var connnectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";", filePath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
				var groupName = string.Empty;
				var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				foreach (DataRow row in dataTable.Rows)
				{
					groupName = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "");
					break;
				}
				var dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", groupName), connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataRow row in dataTable.Rows)
					{
						var firtsName = dataTable.Columns.Count > 0 && row[0] != null ? row[0].ToString() : string.Empty;
						var lastName = dataTable.Columns.Count > 1 && row[1] != null ? row[1].ToString() : string.Empty;
						var email = dataTable.Columns.Count > 2 && row[2] != null ? row[2].ToString() : string.Empty;
						var phone = dataTable.Columns.Count > 3 && row[3] != null ? row[3].ToString() : string.Empty;
						var login = dataTable.Columns.Count > 4 && row[4] != null ? row[4].ToString() : string.Empty;

						if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(email)) continue;
						var newUser = !existedUserLogins.Any(x => x.ToLower().Equals(login.ToLower().Trim()));

						var user = new UserInfo();
						user.Login = login.ToLower().Trim();
						user.Password = newUser ? (complexPassword ? Membership.GeneratePassword(10, 3) : (new PasswordGenerator()).Generate()) : String.Empty;
						user.FirstName = firtsName.Trim();
						user.LastName = lastName.Trim();
						user.Email = email.ToLower().Trim();
						user.Phone = phone.ToLower().Trim();

						var group = existedGroupList.FirstOrDefault(x => x.name.ToLower().Equals(groupName.Trim().ToLower()));
						if (group == null)
						{
							group = new GroupModel();
							group.IsNew = true;
							group.id = Guid.NewGuid().ToString();
							group.name = groupName.Trim();
							existedGroupList.Add(group);
						}
						user.Groups.Add(group);

						existedUserLogins.Add(user.Login);
						userInfo.Add(user);
					}
				}
				catch
				{
					message = "Couldn't read file";
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				connection.Close();
			}
			catch
			{
				message = "Couldn't connect to file";
			}
			return userInfo;
		}
	}
}
