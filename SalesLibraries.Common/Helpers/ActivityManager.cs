using System;
using System.Xml.Linq;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.Activity;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Helpers
{
	public class ActivityManager
	{
		private XDocument _activityStorage;
		private readonly StorageFile _activityFile;

		protected ActivityManager()
		{
			var activityFolder = AppProfileManager.Instance.UserDataFolder;
			_activityFile = new StorageFile(activityFolder.RelativePathParts.Merge(String.Format("{0}.xml", DateTime.Now.ToString("MM-dd-yyyy"))));
		}

		public static TActivityManager OpenStorage<TActivityManager>() where TActivityManager : ActivityManager
		{
			var instance = Activator.CreateInstance<TActivityManager>();
			instance.Init();
			return instance;
		}

		public static ActivityManager OpenStorage()
		{
			return OpenStorage<ActivityManager>();
		}

		private void Init()
		{
			if (_activityFile.ExistsLocal())
				_activityStorage = XDocument.Load(_activityFile.LocalPath);
			else
			{
				_activityStorage = new XDocument();
				var root = new XElement("UserActivities");
				_activityStorage.Add(root);
			}
		}

		private void SaveStorage()
		{
			_activityStorage.Save(_activityFile.LocalPath);
			_activityFile.Upload();
		}

		public void AddActivity(UserActivity activity)
		{
			_activityStorage.Root.Add(activity.Serialize());
			SaveStorage();
		}

		public void AddUserActivity(string activityType)
		{
			AddActivity(new UserActivity(activityType));
		}
	}
}
