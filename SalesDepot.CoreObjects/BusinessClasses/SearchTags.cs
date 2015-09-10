using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class LibraryFileSearchTags
	{
		public LibraryFileSearchTags()
		{
			SearchGroups = new List<SearchGroup>();
		}

		public List<SearchGroup> SearchGroups { get; set; }

		public string AllTags
		{
			get
			{
				return string.Join(", ", SearchGroups.Where(g => g.Tags.Any()).Select(x => x.AllTags));
			}
		}

		public bool Compare(LibraryFileSearchTags anotherTags)
		{
			return SearchGroups.All(tag => anotherTags.SearchGroups.Any(x => x.Name.Equals(tag.Name) && x.Compare(tag))) && SearchGroups.Count == anotherTags.SearchGroups.Count;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<SearchTags>");
			foreach (var group in SearchGroups)
				result.Append(group.Serialize());
			result.AppendLine(@"</SearchTags>");
			return result.ToString();
		}

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

		public LibraryFileSearchTags Clone()
		{
			var result = new LibraryFileSearchTags();
			foreach (var searchGroup in SearchGroups)
			{
				result.SearchGroups.Add(searchGroup.Clone());
			}
			return result;
		}
	}
}