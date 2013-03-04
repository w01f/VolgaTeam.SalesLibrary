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

		public string Details
		{
			get
			{
				var result = new StringBuilder();

				var userString = new List<string>();
				userString.Add("First Name: " + firstName);
				userString.Add("Last Name: " + lastName);
				userString.Add("Email: " + email);
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
	}
}
