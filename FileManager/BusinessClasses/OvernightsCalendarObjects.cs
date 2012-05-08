using System.IO;
using System.Text;
using System.Xml;

namespace FileManager.BusinessClasses
{
    public class OvernightsCalendar
    {
        public Library Parent { get; set; }
        public bool Enabled { get; set; }
        public DirectoryInfo RootFolder { get; set; }

        public OvernightsCalendar(Library parent)
        {
            this.Parent = parent;

            this.RootFolder = new DirectoryInfo(@"z:\");
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Enabled>" + this.Enabled.ToString() + @"</Enabled>");
            result.AppendLine(@"<RootFolder>" + this.RootFolder.FullName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RootFolder>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Enabled":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Enabled = tempBool;
                        break;
                    case "RootFolder":
                        if (Directory.Exists(childNode.InnerText))
                            this.RootFolder = new DirectoryInfo(childNode.InnerText);
                        break;
                }
            }
        }
    }
}
