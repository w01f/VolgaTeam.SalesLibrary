using System;
using System.IO;
using System.Text;
using System.Xml;

namespace FileManager.BusinessClasses
{
    public class FolderLink
    {
        public Guid RootId { get; set; }
        public DirectoryInfo Folder { get; set; }

        public FolderLink()
        {
            this.RootId = Guid.Empty;
        }

        public bool IsDrive
        {
            get
            {
                return this.Folder.FullName.Equals(this.Folder.Root.FullName);
            }
        }

        public virtual string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (this.Folder != null)
            {
                result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
                result.AppendLine(@"<Folder>" + this.Folder.FullName.ToString() + @"</Folder>");
            }
            return result.ToString();
        }

        public virtual void Deserialize(XmlNode node)
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
                    case "Folder":
                        this.Folder = new DirectoryInfo(childNode.InnerText);
                        break;
                }
            }
        }
    }

    public class RootFolder : FolderLink
    {
        public Library Parent { get; private set; }
        public int Order { get; set; }

        public int Index
        {
            get
            {
                return this.Order + 1;
            }
        }

        public string Path
        {
            get
            {
                return this.Folder != null ? this.Folder.FullName : null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && Directory.Exists(value))
                    this.Folder = new DirectoryInfo(value);
            }
        }

        public RootFolder(Library parent)
        {
            this.Parent = parent;
            this.Order = 0;
        }

        public override string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (this.Folder != null)
            {
                result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
                result.AppendLine(@"<Order>" + this.Order.ToString() + @"</Order>");
                result.AppendLine(@"<Folder>" + this.Folder.FullName.ToString() + @"</Folder>");
            }
            return result.ToString();
        }

        public override void Deserialize(XmlNode node)
        {
            Guid tempGuid = Guid.Empty;
            int tempInt;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "RootId":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.RootId = tempGuid;
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Order = tempInt;
                        break;
                    case "Folder":
                        this.Folder = new DirectoryInfo(childNode.InnerText);
                        break;
                }
            }
        }
    }
}
