using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class AttachmentProperties
    {
        public ILibraryFile Parent { get; private set; }
        public Guid Identifier { get; set; }
        public bool Enable { get; set; }
        public List<LinkAttachment> FilesAttachments { get; private set; }
        public List<LinkAttachment> WebAttachments { get; private set; }

        public AttachmentProperties(ILibraryFile parent)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid();
            this.FilesAttachments = new List<LinkAttachment>();
            this.WebAttachments = new List<LinkAttachment>();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<Enable>" + this.Enable.ToString() + @"</Enable>");
            foreach (LinkAttachment attachment in this.FilesAttachments)
                result.AppendLine(@"<Attachment>" + attachment.Serialize() + @"</Attachment>");
            foreach (LinkAttachment attachment in this.WebAttachments)
                result.AppendLine(@"<Attachment>" + attachment.Serialize() + @"</Attachment>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            Guid tempGuid;
            bool tempBool;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "Enable":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Enable = tempBool;
                        break;
                    case "Attachment":
                        LinkAttachment attachment = new LinkAttachment(this);
                        attachment.Deserialize(childNode);
                        switch (attachment.Type)
                        { 
                            case AttachmentType.File:
                                this.FilesAttachments.Add(attachment);
                                break;
                            case AttachmentType.Url:
                                this.WebAttachments.Add(attachment);
                                break;
                        }
                        break;
                }
            }
        }
    }
}
