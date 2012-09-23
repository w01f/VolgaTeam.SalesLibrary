using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
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
            {
                result.Append(@"<Category ");
                result.Append("Name = \"" + group.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                result.AppendLine(@">");
                foreach (string tag in group.Tags)
                {
                    result.Append(@"<Tag ");
                    result.Append("Value = \"" + tag.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                    result.AppendLine(@"/>");
                }
                result.AppendLine(@"</Category>");
            }
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
                    case "Category":
                        SearchGroup group = new SearchGroup();
                        foreach (XmlAttribute attribute in childNode.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case "Name":
                                    group.Name = attribute.Value;
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
