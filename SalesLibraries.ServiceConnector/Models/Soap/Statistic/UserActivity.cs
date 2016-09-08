using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesLibraries.ServiceConnector.StatisticService
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

		public bool IsUrl
		{
			get
			{
				return
					"QSite".Equals(type, StringComparison.OrdinalIgnoreCase) ||
					(details != null && "url".Equals((details
						.Where(d => "Original Format".Equals(d.tag, StringComparison.OrdinalIgnoreCase))
						.Select(d => d.value)
						.FirstOrDefault() ?? String.Empty), StringComparison.OrdinalIgnoreCase)) ||
					(details != null && details.Any(d => "url".Equals(d.tag, StringComparison.OrdinalIgnoreCase)));
			}
		}

		public string File
		{
			get
			{
				if (details == null) return String.Empty;
				Func<ActivityDetail, bool> selector = d =>
							"name".Equals(d.tag, StringComparison.OrdinalIgnoreCase) ||
							"file".Equals(d.tag, StringComparison.OrdinalIgnoreCase) ||
							"link".Equals(d.tag, StringComparison.OrdinalIgnoreCase) ||
							"url".Equals(d.tag, StringComparison.OrdinalIgnoreCase) ||
							"file name".Equals(d.tag, StringComparison.OrdinalIgnoreCase);
				return details
					.OrderBy(d => new[]
						{
							"file",
							"link",
							"file name",
							"url",
						}.Any(item => item.Equals(d.tag, StringComparison.OrdinalIgnoreCase)) ? 1 : 2)
					.ThenBy(d => d.tag)
					.Where(selector)
					.Select(d =>
						{
							if (d.value.Contains("?version="))
								return d.value.Substring(0, d.value.IndexOf("?version="));
							return d.value;
						})
					.FirstOrDefault() ?? String.Empty;
			}
		}

		public string Details
		{
			get
			{
				var result = new StringBuilder();

				var userString = new List<string>();
				if (!String.IsNullOrEmpty(firstName))
					userString.Add("First Name: " + firstName);
				if (!String.IsNullOrEmpty(lastName))
					userString.Add("Last Name: " + lastName);
				if (!String.IsNullOrEmpty(email))
					userString.Add("Email: " + email);
				if (!String.IsNullOrEmpty(phone))
					userString.Add("Phone: " + phone);
				if (!String.IsNullOrEmpty(groups))
					userString.Add("Groups: " + groups);
				if (!String.IsNullOrEmpty(ip))
					userString.Add("IP: " + ip);
				if (!String.IsNullOrEmpty(os))
					userString.Add("OS: " + os);
				if (!String.IsNullOrEmpty(device))
					userString.Add("Device: " + device);
				if (!String.IsNullOrEmpty(browser))
					userString.Add("Browser: " + browser);
				if (userString.Any())
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