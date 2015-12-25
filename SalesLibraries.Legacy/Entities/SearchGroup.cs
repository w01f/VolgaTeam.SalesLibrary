using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class SearchGroup
	{
		public const string TagName = "Category";

		public string AllTags
		{
			get
			{
				return string.Join(", ", Tags.Where(t => !String.IsNullOrEmpty(t.Name)).Select(x => x.Name));
			}
		}

		public SearchGroup()
		{
			Name = string.Empty;
			Description = string.Empty;
			Tags = new List<SearchTag>();
			TagNameObject = TagName;
		}

		public string TagNameObject { get; protected set; }

		public string Name { get; set; }
		public bool Selected { get; set; }
		public string Description { get; set; }
		public List<SearchTag> Tags { get; private set; }

		public void Deserialize(XmlNode node)
		{foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
				}
			}
			foreach (XmlNode tagNode in node.ChildNodes)
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
										Tags.Add(new SearchTag(Name) { Name = attribute.Value });
									break;
							}
						}
						break;
				}
			}
		}
	}

	class CustomKeywords : SearchGroup
	{
		public new const string TagName = "CustomKeywords";

		public CustomKeywords()
		{
			Name = "Custom Keywords";
			TagNameObject = TagName;
		}
	}

	public class SearchTag
	{
		public string Name { get; set; }
		public string Parent { get; private set; }
		public bool Selected { get; set; }

		public SearchTag(string parentGroup)
		{
			Parent = parentGroup;
		}
	}
}