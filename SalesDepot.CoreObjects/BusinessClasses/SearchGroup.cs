using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class SearchGroup
	{
		public const string TagName = "Category";

		public string AllTags
		{
			get
			{
				return string.Join(", ", Tags.Select(x => x.Name));
			}
		}

		public SearchGroup()
		{
			Name = string.Empty;
			Description = string.Empty;
			Tags = new List<SearchTag>();
			TagNameObject = TagName;
		}

		public virtual string TagNameObject { get; set; }

		public string Name { get; set; }
		public bool Selected { get; set; }
		public string Description { get; set; }
		public Image Logo { get; set; }
		public List<SearchTag> Tags { get; private set; }

		public bool Compare(SearchGroup anotherGroup)
		{
			return Tags.All(tag => anotherGroup.Tags.Select(x => x.Name).Contains(tag.Name)) && Tags.Count == anotherGroup.Tags.Count;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.Append(@"<" + TagNameObject + " ");
			result.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
			result.AppendLine(@">");
			foreach (var tag in Tags)
			{
				result.Append(@"<Tag ");
				result.Append("Value = \"" + tag.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
				result.AppendLine(@"/>");
			}
			result.AppendLine(@"</" + TagNameObject + ">");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlAttribute attribute in node.Attributes)
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

	public class CustomKeywords : SearchGroup
	{
		public new const string TagName = "CustomKeywords";

		public CustomKeywords()
		{
			Name = "Custom Keywords";
			TagNameObject = TagName;
		}

		public override string TagNameObject { get; set; }
	}

	public class SearchTag
	{
		public string Name { get; set; }
		public string Parent { get; set; }
		public bool Selected { get; set; }

		public SearchTag(string parentGroup)
		{
			Parent = parentGroup;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}