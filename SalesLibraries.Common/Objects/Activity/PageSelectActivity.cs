using System.Xml.Linq;

namespace SalesLibraries.Common.Objects.Activity
{
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
}
