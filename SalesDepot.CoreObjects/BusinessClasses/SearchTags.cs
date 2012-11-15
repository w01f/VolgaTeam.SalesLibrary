using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class SearchTags
	{
		private string _listsFileName;
		public List<SearchGroup> SearchGroups { get; set; }

		public SearchTags()
		{
			_listsFileName = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "SDSearch.xml");
			this.SearchGroups = new List<SearchGroup>();
			Load();
		}

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				XmlDocument document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/SDSearch");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "Category":
								SearchGroup group = new SearchGroup();
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
															group.Tags.Add(attribute.Value);
														break;
												}
											}
											break;
									}
								}
								if (!string.IsNullOrEmpty(group.Name) && group.Tags.Count > 0)
									this.SearchGroups.Add(group);
								break;
						}
					}
				}
			}
		}
	}

	public class LibraryFileSearchTags
	{
		public List<SearchGroup> SearchGroups { get; set; }

		public string AllTags
		{
			get
			{
				List<string> allTags = new List<string>();
				foreach (SearchGroup group in this.SearchGroups)
					allTags.AddRange(group.Tags);
				return string.Join(", ", allTags.ToArray());
			}
		}

		public LibraryFileSearchTags()
		{
			this.SearchGroups = new List<SearchGroup>();
		}

		public string Serialize()
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine(@"<SearchTags>");
			foreach (SearchGroup group in this.SearchGroups)
				result.Append(group.Serialize());
			result.AppendLine(@"</SearchTags>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			this.SearchGroups.Clear();

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case SearchGroup.TagName:
						SearchGroup group = new SearchGroup();
						group.Deserialize(childNode);
						if (!string.IsNullOrEmpty(group.Name) && group.Tags.Count > 0)
							this.SearchGroups.Add(group);
						break;
				}
			}
		}
	}
}
