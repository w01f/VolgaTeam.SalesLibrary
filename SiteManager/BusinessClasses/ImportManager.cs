using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web.Security;
using SalesDepot.Services.IPadAdminService;
using SalesDepot.Services.TickerService;
using SalesDepot.SiteManager.ToolClasses;

namespace SalesDepot.SiteManager.BusinessClasses
{
	public class ImportManager
	{
		public static IEnumerable<UserInfo> ImportUsers(string filePath, UserRecord[] existedUsers, GroupRecord[] existedGroups, bool complexPassword, out string message)
		{
			message = string.Empty;
			var userInfo = new List<UserInfo>();
			var existedGroupList = new List<GroupRecord>(existedGroups);
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
							group = new GroupRecord();
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

		public static IEnumerable<TickerLink> ImportTickers(string filePath, out string message)
		{
			message = string.Empty;
			var tickerLinks = new List<TickerLink>();

			var connnectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";", filePath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
				var pageName = "Ticker Links";
				var dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", pageName), connection);
				var dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataRow row in dataTable.Rows)
					{
						var type = dataTable.Columns.Count > 0 && row[0] != null ? row[0].ToString().Trim() : string.Empty;
						var text = dataTable.Columns.Count > 1 && row[1] != null ? row[1].ToString().Trim() : string.Empty;
						var url = dataTable.Columns.Count > 2 && row[2] != null ? row[2].ToString().Trim() : string.Empty;
						var file = dataTable.Columns.Count > 3 && row[3] != null ? row[3].ToString().Trim() : string.Empty;
						var video = dataTable.Columns.Count > 4 && row[4] != null ? row[4].ToString().Trim() : string.Empty;
						var library = dataTable.Columns.Count > 5 && row[5] != null ? row[5].ToString().Trim() : string.Empty;
						var page = dataTable.Columns.Count > 6 && row[6] != null ? row[6].ToString().Trim() : string.Empty;
						var link = dataTable.Columns.Count > 6 && row[7] != null ? row[7].ToString().Trim() : string.Empty;

						if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(text))
						{
							var tickerLink = new TickerLink();
							tickerLink.text = text;
							switch (type)
							{
								case "Simple text":
									tickerLink.type = "text";
									tickerLinks.Add(tickerLink);
									break;
								case "Web link":
									tickerLink.type = "url";
									if (!string.IsNullOrEmpty(url))
									{
										var details = new List<KeyValuePair>();
										details.Add(new KeyValuePair { tag = "path", data = url });
										tickerLink.details = details.ToArray();
										tickerLinks.Add(tickerLink);
									}
									break;
								case "Video Link":
									tickerLink.type = "video";
									if (!string.IsNullOrEmpty(video))
									{
										var details = new List<KeyValuePair>();
										details.Add(new KeyValuePair { tag = "path", data = video });
										tickerLink.details = details.ToArray();
										tickerLinks.Add(tickerLink);
									}
									break;
								case "File Link":
									tickerLink.type = "file";
									if (!string.IsNullOrEmpty(file))
									{
										var details = new List<KeyValuePair>();
										details.Add(new KeyValuePair { tag = "path", data = file });
										tickerLink.details = details.ToArray();
										tickerLinks.Add(tickerLink);
									}
									break;
								case "Library Link":
									tickerLink.type = "link";
									if (!string.IsNullOrEmpty(library) && !string.IsNullOrEmpty(page) && !string.IsNullOrEmpty(link))
									{
										var details = new List<KeyValuePair>();
										details.Add(new KeyValuePair { tag = "library", data = library });
										details.Add(new KeyValuePair { tag = "page", data = page });
										details.Add(new KeyValuePair { tag = "link", data = link });
										tickerLink.details = details.ToArray();
										tickerLinks.Add(tickerLink);
									}
									break;
							}
						}
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
			return tickerLinks;
		}
	}
}
