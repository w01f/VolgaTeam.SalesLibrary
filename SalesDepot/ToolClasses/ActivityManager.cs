using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.ToolClasses
{
    public class ActivityManager
    {
        private List<UserActivity> _activities = new List<UserActivity>();

        private string _activityFilePath
        {
            get
            {
                return Path.Combine(ConfigurationClasses.SettingsManager.Instance.ActivityFolder, DateTime.Now.ToString("MMyyyy") + ".xml");
            }
        }

        private string _userName
        {
            get
            {
                return Environment.UserName;
            }
        }


        public ActivityManager()
        {
            LoadActivities();
        }

        private void LoadActivities()
        {
            _activities.Clear();
            if (File.Exists(_activityFilePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_activityFilePath);

                XmlNode node = document.SelectSingleNode(@"/UserActivities");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Attributes.GetNamedItem("AccessedLink") != null)
                        {
                            LinkAccessActivity activity = new LinkAccessActivity();
                            activity.Deserialize(childNode);
                            _activities.Add(activity);
                        }
                        else
                        {
                            UserActivity activity = new UserActivity();
                            activity.Deserialize(childNode);
                            _activities.Add(activity);
                        }
                    }
                }
            }
        }

        private void SaveActivities()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<UserActivities>");
            foreach (UserActivity userActivity in _activities)
                if (!string.IsNullOrEmpty(userActivity.UserName))
                    xml.AppendLine(userActivity.Serialize());
            xml.AppendLine(@"</UserActivities>");

            using (StreamWriter sw = new StreamWriter(_activityFilePath, false))
            {
                sw.Write(xml);
                sw.Flush();
                sw.Close();
            }
        }

        public void AddUserActivity(string activityType)
        {
            UserActivity activity = new UserActivity();
            activity.UserName = _userName;
            activity.ActivityType = activityType;
            activity.ActivityTime = DateTime.Now;
            _activities.Add(activity);

            SaveActivities();
        }

        public void AddLinkAccessActivity(string activityType, string accessedLink, string linkType, string linkedFile, string libraryName, string pageName)
        {
            LinkAccessActivity activity = new LinkAccessActivity();
            activity.UserName = _userName;
            activity.ActivityType = activityType;
            activity.ActivityTime = DateTime.Now;
            activity.AccessedLink = accessedLink;
            activity.LinkType = linkType;
            activity.LinkedFile = linkedFile;
            activity.LibraryName = libraryName;
            activity.PageName = pageName;
            _activities.Add(activity);

            SaveActivities();
        }
    }

    public class UserActivity
    {
        public string UserName { get; set; }
        public string ActivityType { get; set; }
        public DateTime ActivityTime { get; set; }

        public virtual string Serialize()
        {
            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrEmpty(this.UserName))
            {
                result.Append(@"<UserActivity ");
                result.Append("UserName = \"" + this.UserName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("ActivityType = \"" + this.ActivityType.ToString() + "\" ");
                result.Append("ActivityTime = \"" + this.ActivityTime.ToString() + "\" ");
                result.AppendLine(@"/>");
            }
            return result.ToString();
        }

        public virtual void Deserialize(XmlNode node)
        {
            DateTime tempDateTime;

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "UserName":
                        this.UserName = attribute.InnerText;
                        break;
                    case "ActivityType":
                        this.ActivityType = attribute.InnerText;
                        break;
                    case "ActivityTime":
                        if (DateTime.TryParse(attribute.Value, out tempDateTime))
                            this.ActivityTime = tempDateTime;
                        break;
                }
            }
        }
    }

    public class LinkAccessActivity : UserActivity
    {
        public string AccessedLink { get; set; }
        public string LinkType { get; set; }
        public string LinkedFile { get; set; }
        public string LibraryName { get; set; }
        public string PageName { get; set; }

        public override string Serialize()
        {
            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrEmpty(this.UserName))
            {
                result.Append(@"<UserActivity ");
                result.Append("UserName = \"" + this.UserName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("ActivityType = \"" + this.ActivityType.ToString() + "\" ");
                result.Append("ActivityTime = \"" + this.ActivityTime.ToString() + "\" ");
                result.Append("AccessedLink = \"" + this.AccessedLink.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("LinkType = \"" + this.LinkType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("LinkedFile = \"" + this.LinkedFile.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("LibraryName = \"" + this.LibraryName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("PageName = \"" + this.PageName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.AppendLine(@"/>");
            }
            return result.ToString();
        }

        public override void Deserialize(XmlNode node)
        {
            DateTime tempDateTime;

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "UserName":
                        this.UserName = attribute.InnerText;
                        break;
                    case "ActivityType":
                        this.ActivityType = attribute.InnerText;
                        break;
                    case "ActivityTime":
                        if (DateTime.TryParse(attribute.Value, out tempDateTime))
                            this.ActivityTime = tempDateTime;
                        break;
                    case "AccessedLink":
                        this.AccessedLink = attribute.InnerText;
                        break;
                    case "LinkType":
                        this.LinkType = attribute.InnerText;
                        break;
                    case "LinkedFile":
                        this.LinkedFile = attribute.InnerText;
                        break;
                    case "LibraryName":
                        this.LibraryName = attribute.InnerText;
                        break;
                    case "PageName":
                        this.PageName = attribute.InnerText;
                        break;
                }
            }
        }
    }
}
