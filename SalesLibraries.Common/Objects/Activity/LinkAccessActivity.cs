using System.Xml.Linq;

namespace SalesLibraries.Common.Objects.Activity
{
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
			activityElement.Add(new XAttribute("LibraryName", LibraryName));
			activityElement.Add(new XAttribute("PageName", PageName));
			return activityElement;
		}
	}
}
