using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class SearchTags
	{
		private readonly string _listsFileName;

		public SearchTags()
		{
			_listsFileName = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "SDSearch.xml");
			SearchGroups = new List<SearchGroup>();
			Load();
		}

		public List<SearchGroup> SearchGroups { get; set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/SDSearch");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "Category":
								var group = new SearchGroup();
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Name":
											group.Name = attribute.Value;
											break;
										case "Description":
											group.Description = attribute.Value;
											break;
									}
								}
								foreach (XmlNode tagNode in childNode.ChildNodes)
								{
									switch (tagNode.Name)
									{
										case "Tag":
											foreach (XmlAttribute attribute in tagNode.Attributes)
											{
												switch (attribute.Name)
												{
													case "Value":
														if (!string.IsNullOrEmpty(attribute.Value))
															group.Tags.Add(new SearchTag(group.Name) { Name = attribute.Value });
														break;
												}
											}
											break;
									}
								}
								if (!string.IsNullOrEmpty(group.Name) && group.Tags.Count > 0)
									SearchGroups.Add(group);
								break;
						}
					}
				}
			}
		}
	}

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
				return string.Join(", ", SearchGroups.Select(x => x.AllTags));
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
	}
}