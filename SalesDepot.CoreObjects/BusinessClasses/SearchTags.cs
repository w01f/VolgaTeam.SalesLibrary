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
