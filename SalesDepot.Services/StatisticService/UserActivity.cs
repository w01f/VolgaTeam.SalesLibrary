using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDepot.Services.StatisticService
{
	public partial class UserActivity
	{
		public DateTime? ActivityDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(date, out temp))
					return temp;
				return null;
			}
		}

		public string File
		{
			get
			{
				if ((subType.Equals("Open") ||
					subType.Equals("Open") ||
					subType.Equals("Preview Options") ||
					subType.Equals("Preview Page") ||
					subType.Equals("Play Video") ||
					subType.Equals("Send Email") ||
					subType.Equals("Email Activity") ||
					subType.Equals("Create Email")
					)
					&& details != null)
					return details.Where(d => d.tag.ToLower().Equals("file")).Select(d =>
																						 {
																							 if (d.value.Contains("?version="))
																								 return d.value.Substring(0, d.value.IndexOf("?version="));
																							 return d.value;
																						 }).FirstOrDefault();
				return String.Empty;
			}
		}

		public string Details
		{
			get
			{
				var result = new StringBuilder();

				var userString = new List<string>();
				userString.Add("First Name: " + firstName);
				userString.Add("Last Name: " + lastName);
				userString.Add("Email: " + email);
				if (!string.IsNullOrEmpty(phone))
					userString.Add("Phone: " + phone);
				userString.Add("Groups: " + groups);
				userString.Add("IP: " + ip);
				userString.Add("OS: " + os);
				userString.Add("Device: " + device);
				userString.Add("Browser: " + browser);
				result.AppendLine("User Detail - " + string.Join("; ", userString.ToArray()));

				if (details != null)
				{
					result.AppendLine();
					result.AppendLine("Activity Detail - " + string.Join("; ", details.Select(detail => detail.tag + ": " + detail.value).ToArray()));
				}

				return result.ToString();
			}
		}

		public string[] GroupList
		{
			get
			{
				return !string.IsNullOrEmpty(groups) ? groups.Split(',').Select(x => x.Trim()).ToArray() : new string[] { };
			}
		}
	}
}