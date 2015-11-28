using System.Collections.Generic;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class LibraryFileSearchTags
	{
		public LibraryFileSearchTags()
		{
			SearchGroups = new List<SearchGroup>();
		}

		public List<SearchGroup> SearchGroups { get; set; }

		public void Deserialize(XmlNode node)
		{
			SearchGroups.Clear();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case SearchGroup.TagName:
						var group = new SearchGroup();
						group.Deserialize(childNode);
						if (!string.IsNullOrEmpty(group.Name) && group.Tags.Count > 0)
							SearchGroups.Add(group);
						break;
				}
			}
		}
	}
}