using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class SearchGroup
    {
        public const string TagName = "Category";

        public virtual string TagNameObject { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Image Logo { get; set; }
        public List<string> Tags { get; private set; }

        public SearchGroup()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Tags = new List<string>();
            this.TagNameObject = TagName;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.Append(@"<" + this.TagNameObject + " ");
            result.Append("Name = \"" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
            result.AppendLine(@">");
            foreach (string tag in this.Tags)
            {
                result.Append(@"<Tag ");
                result.Append("Value = \"" + tag.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                result.AppendLine(@"/>");
            }
            result.AppendLine(@"</" + this.TagNameObject + ">");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        this.Name = attribute.Value;
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
                                        this.Tags.Add(attribute.Value);
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
        new public const string TagName = "CustomKeywords";
        public override string TagNameObject { get; set; }

        public CustomKeywords()
            : base()
        {
            this.Name = "Custom Keywords";
            this.TagNameObject = TagName;
        }
    }

}
