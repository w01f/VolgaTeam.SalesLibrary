using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.ToolClasses
{
	public class ActivityManager
	{
		private XDocument _activityStorage;
		private bool _queueMode;
		private readonly List<UserActivity> _activitiesInQueue = new List<UserActivity>();

		protected string ActivityFilePath
		{
			get { return Path.Combine(SettingsManager.Instance.ActivityFolder, String.Format("{0}.xml", DateTime.Now.ToString("MM-dd-yyyy"))); }
		}

		public ActivityManager()
		{
			OpenStorage();
		}

		private void OpenStorage()
		{
			if (File.Exists(ActivityFilePath))
				_activityStorage = XDocument.Load(ActivityFilePath);
			else
			{
				_activityStorage = new XDocument();
				var root = new XElement("UserActivities");
				_activityStorage.Add(root);
			}
		}

		private void SaveStorage()
		{
			_activityStorage.Save(ActivityFilePath);
		}

		public void AddActivity(UserActivity activity)
		{
			if (_queueMode)
				_activitiesInQueue.Add(activity);
			else
			{
				_activityStorage.Root.Add(activity.Serialize());
				SaveStorage();
			}
		}

		public void AddUserActivity(string activityType)
		{
			AddActivity(new UserActivity(activityType) { Order = 0 });
		}

		public void AddLinkAccessActivity(string activityType, string accessedLink, string linkType, string linkedFile, string libraryName, string pageName)
		{
			AddActivity(new LinkAccessActivity(activityType, accessedLink, linkType, linkedFile, libraryName, pageName));
		}

		public void StartQueue()
		{
			_queueMode = true;
		}

		public void CloseQueue()
		{
			_queueMode = false;
			foreach (var activity in _activitiesInQueue.OrderBy(a => a.Order))
				_activityStorage.Root.Add(activity.Serialize());
			_activitiesInQueue.Clear();
			SaveStorage();
		}
	}

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

	public class PageSelectActivity : UserActivity
	{
		public string LibraryName { get; private set; }
		public string PageName { get; private set; }

		public PageSelectActivity(string libraryName, string pageName)
			: base("Page Selected")
		{
			PageName = pageName;
			LibraryName = libraryName;
		}

		public override XElement Serialize()
		{
			var activityElement = base.Serialize();
			activityElement.Add(new XAttribute("Library", LibraryName));
			activityElement.Add(new XAttribute("Page", PageName));
			return activityElement;
		}
	}

	public class LinkAccessActivity : UserActivity
	{
		public LinkAccessActivity(string activityType, string accessedLink, string linkType, string linkedFile, string libraryName, string pageName)
			: base(activityType)
		{
			AccessedLink = accessedLink;
			LinkType = linkType;
			LinkedFile = linkedFile;
			LibraryName = libraryName;
			PageName = pageName;
		}

		public string AccessedLink { get; set; }
		public string LinkType { get; set; }
		public string LinkedFile { get; set; }
		public string LibraryName { get; set; }
		public string PageName { get; set; }

		public override XElement Serialize()
		{
			var activityElement = base.Serialize();
			activityElement.Add(new XAttribute("AccessedLink", AccessedLink));
			activityElement.Add(new XAttribute("LinkType", LinkType));
			activityElement.Add(new XAttribute("LinkedFile", LinkedFile));
			activityElement.Add(new XAttribute("LibraryName", LinkedFile));
			activityElement.Add(new XAttribute("PageName", PageName));
			return activityElement;
		}
	}
}