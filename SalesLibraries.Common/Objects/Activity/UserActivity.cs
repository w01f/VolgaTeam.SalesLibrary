using System;
using System.Xml.Linq;

namespace SalesLibraries.Common.Objects.Activity
{
	public class UserActivity
	{
		public string UserName { get; private set; }
		public string ActivityType { get; private set; }
		public DateTime ActivityTime { get; private set; }
		public int Order { get; set; }

		public UserActivity(string activityType)
		{
			ActivityTime = DateTime.Now;
			UserName = Environment.UserName;
			ActivityType = activityType;
			Order = 999;
		}

		public virtual XElement Serialize()
		{
			var activityElement = new XElement("UserActivity");
			activityElement.Add(new XAttribute("UserName", UserName));
			activityElement.Add(new XAttribute("ActivityType", ActivityType));
			activityElement.Add(new XAttribute("ActivityTime", ActivityTime));
			return activityElement;
		}
	}
}
