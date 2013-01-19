using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class AttachmentProperties
	{
		public AttachmentProperties(ILibraryLink parent)
		{
			Parent = parent;
			Identifier = Guid.NewGuid();
			FilesAttachments = new List<LinkAttachment>();
			WebAttachments = new List<LinkAttachment>();
		}

		public ILibraryLink Parent { get; private set; }
		public Guid Identifier { get; set; }
		public bool Enable { get; set; }
		public List<LinkAttachment> FilesAttachments { get; private set; }
		public List<LinkAttachment> WebAttachments { get; private set; }

		public virtual AttachmentProperties Clone(ILibraryLink parent)
		{
			var attachmentProperties = new AttachmentProperties(parent);
			attachmentProperties.Enable = Enable;
			attachmentProperties.FilesAttachments.AddRange(FilesAttachments.Select(x => x.Clone(attachmentProperties)));
			attachmentProperties.WebAttachments.AddRange(WebAttachments.Select(x => x.Clone(attachmentProperties)));
			return attachmentProperties;
		}


		public virtual string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier.ToString() + @"</Identifier>");
			result.AppendLine(@"<Enable>" + Enable.ToString() + @"</Enable>");
			foreach (LinkAttachment attachment in FilesAttachments)
				result.AppendLine(@"<Attachment>" + attachment.Serialize() + @"</Attachment>");
			foreach (LinkAttachment attachment in WebAttachments)
				result.AppendLine(@"<Attachment>" + attachment.Serialize() + @"</Attachment>");
			return result.ToString();
		}

		public virtual void Deserialize(XmlNode node)
		{
			Guid tempGuid;
			bool tempBool;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Identifier":
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "Enable":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Enable = tempBool;
						break;
					case "Attachment":
						var attachment = new LinkAttachment(this);
						attachment.Deserialize(childNode);
						switch (attachment.Type)
						{
							case AttachmentType.File:
								FilesAttachments.Add(attachment);
								break;
							case AttachmentType.Url:
								WebAttachments.Add(attachment);
								break;
						}
						break;
				}
			}
		}
	}
}