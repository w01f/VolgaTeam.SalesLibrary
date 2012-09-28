using System;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class LinkAttachment : IPreviewable
    {
        public AttachmentProperties Parent { get; private set; }
        public Guid Identifier { get; set; }
        public AttachmentType Type { get; set; }
        public string OriginalPath { get; set; }
        public IPreviewContainer UniversalPreviewContainer { get; set; }

        public string DestinationPath
        {
            get
            {
                switch (this.Type)
                {
                    case AttachmentType.File:
                        return Path.Combine(this.Parent.Parent.Parent.Parent.Parent.Folder.FullName, Constants.AttachmentsRootFolderName, this.Identifier.ToString(), Path.GetFileName(this.OriginalPath));
                    default:
                        return this.OriginalPath;
                }
            }
        }

        public string Name
        {
            get
            {
                switch (this.Type)
                {
                    case AttachmentType.File:
                        return Path.GetFileName(this.OriginalPath);
                    default:
                        return this.OriginalPath;
                }
            }
        }

        public bool IsSourceAvailable
        {
            get
            {
                switch (this.Type)
                {
                    case AttachmentType.File:
                        return File.Exists(this.OriginalPath);
                    default:
                        return true;
                }
            }
        }

        public bool IsDestinationAvailable
        {
            get
            {
                switch (this.Type)
                {
                    case AttachmentType.File:
                        return File.Exists(this.DestinationPath);
                    default:
                        return true;
                }
            }
        }

        public DateTime LastChanged
        {
            get
            {
                return this.Parent.Parent.LastChanged;
            }
            set
            {
                this.Parent.Parent.LastChanged = value;
            }
        }

        public string PreviewStoragePath
        {
            get
            {
                return this.Parent.Parent.PreviewStoragePath;
            }
        }

        public string Format
        {
            get
            {
                string format = "other";
                if (this.Type == AttachmentType.File && !string.IsNullOrEmpty(this.OriginalPath))
                {
                    switch (Path.GetExtension(this.OriginalPath).Replace(".",string.Empty).ToLower())
                    {
                        case "ppt":
                        case "pptx":
                            format = "ppt";
                            break;
                        case "doc":
                        case "docx":
                            format = "doc";
                            break;
                        case "xls":
                        case "xlsx":
                            format = "xls";
                            break;
                        case "pdf":
                            format = "pdf";
                            break;
                        case "mpeg":
                        case "wmv":
                        case "avi":
                        case "wmz":
                        case "mpg":
                        case "asf":
                        case "mov":
                        case "m4v":
                        case "flv":
                        case "ogv":
                        case "ogm":
                        case "ogx":
                            format = "video";
                            break;
                        case "mp4":
                            format = "mp4";
                            break;
                        case "png":
                            format = "png";
                            break;
                        case "jpg":
                        case "jpeg":
                            format = "jpeg";
                            break;
                        case "url":
                            format = "url";
                            break;
                        default:
                            format = "other";
                            break;
                    }
                }
                return format;
            }
        }

        public LinkAttachment(AttachmentProperties parent)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<Type>" + (int)this.Type + @"</Type>");
            if (!string.IsNullOrEmpty(this.OriginalPath))
                result.AppendLine(@"<OriginalPath>" + this.OriginalPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</OriginalPath>");
            if (this.UniversalPreviewContainer != null)
                result.AppendLine(@"<UniversalPreviewContainer>" + this.UniversalPreviewContainer.Serialize() + @"</UniversalPreviewContainer>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            Guid tempGuid;
            int tempInt;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "Type":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Type = (AttachmentType)tempInt;
                        break;
                    case "OriginalPath":
                        this.OriginalPath = childNode.InnerText;
                        break;
                    case "UniversalPreviewContainer":
                        this.UniversalPreviewContainer = new UniversalPreviewContainer(this);
                        this.UniversalPreviewContainer.Deserialize(childNode);
                        break;
                }
            }
        }

        public SalesDepot.CoreObjects.BusinessClasses.IPreviewGenerator GetPreviewGenerator(string extension = "")
        {
            if (this.IsSourceAvailable)
            {
                return this.Parent.Parent.GetPreviewGenerator(Path.GetExtension(this.OriginalPath));
            }
            else
                return null;
        }
    }
}
