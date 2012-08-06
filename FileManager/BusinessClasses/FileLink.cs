using System;
using System.IO;
using System.Text;
using System.Xml;

namespace FileManager.BusinessClasses
{
    public class FileLink
    {
        public Guid RootId { get; set; }
        public FileInfo File { get; set; }

        public FileLink()
        {
            this.RootId = Guid.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (this.File != null)
            {
                result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
                result.AppendLine(@"<File>" + this.File.FullName.ToString() + @"</File>");
            }
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            Guid tempGuid = Guid.Empty;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "RootId":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.RootId = tempGuid;
                        break;
                    case "File":
                        this.File = new FileInfo(childNode.InnerText);
                        break;
                }
            }
        }
    }
}
