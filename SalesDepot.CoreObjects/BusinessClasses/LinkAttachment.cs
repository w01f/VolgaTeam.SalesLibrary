using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class LinkAttachment
	{
		public LinkAttachment(AttachmentProperties parent)
		{
			Parent = parent;
			Identifier = Guid.NewGuid();
		}

		public AttachmentProperties Parent { get; private set; }
		public Guid Identifier { get; set; }
		public AttachmentType Type { get; set; }
		public string OriginalPath { get; set; }

		public string DestinationPath
		{
			get
			{
				switch (Type)
				{
					case AttachmentType.File:
						return Path.Combine(Parent.Parent.Parent.Parent.Parent.Folder.FullName, Constants.AttachmentsRootFolderName, Identifier.ToString(), Path.GetFileName(OriginalPath));
					default:
						return OriginalPath;
				}
			}
		}

		public string DestinationRelativePath
		{
			get
			{
				switch (Type)
				{
					case AttachmentType.File:
						return Path.Combine(Constants.AttachmentsRootFolderName, Identifier.ToString(), Path.GetFileName(OriginalPath));
					default:
						return OriginalPath;
				}
			}
		}

		public string Name
		{
			get
			{
				switch (Type)
				{
					case AttachmentType.File:
						return Path.GetFileName(OriginalPath);
					default:
						return OriginalPath;
				}
			}
		}

		public bool IsSourceAvailable
		{
			get
			{
				switch (Type)
				{
					case AttachmentType.File:
						return File.Exists(OriginalPath);
					default:
						return true;
				}
			}
		}

		public bool IsDestinationAvailable
		{
			get
			{
				switch (Type)
				{
					case AttachmentType.File:
						return File.Exists(DestinationPath);
					default:
						return true;
				}
			}
		}

		public DateTime LastChanged
		{
			get { return Parent.Parent.LastChanged; }
			set { Parent.Parent.LastChanged = value; }
		}

		public string Format
		{
			get
			{
				string format = "other";
				if ((Type == AttachmentType.File) && !string.IsNullOrEmpty(OriginalPath))
				{
					switch (Path.GetExtension(OriginalPath).Replace(".", string.Empty).ToLower())
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
						case "key":
							format = "key";
							break;
						default:
							format = "other";
							break;
					}
				}
				return format;
			}
		}

		public LinkAttachment Clone(AttachmentProperties parent)
		{
			var linkAttachment = new LinkAttachment(parent);
			linkAttachment.Type = Type;
			linkAttachment.OriginalPath = OriginalPath;
			return linkAttachment;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier.ToString() + @"</Identifier>");
			result.AppendLine(@"<Type>" + (int)Type + @"</Type>");
			if (!string.IsNullOrEmpty(OriginalPath))
				result.AppendLine(@"<OriginalPath>" + OriginalPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</OriginalPath>");
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
							Identifier = tempGuid;
						break;
					case "Type":
						if (int.TryParse(childNode.InnerText, out tempInt))
							Type = (AttachmentType)tempInt;
						break;
					case "OriginalPath":
						OriginalPath = childNode.InnerText;
						break;

					#region Compatibility with old version of Sales Depot
					case "UniversalPreviewContainer":
						var universalPreviewContainer = new UniversalPreviewContainer(Parent.Parent.Parent.Parent.Parent);
						universalPreviewContainer.Deserialize(childNode);
						universalPreviewContainer.OriginalPath = OriginalPath;
						if (!Parent.Parent.Parent.Parent.Parent.PreviewContainers.Any(x => x.OriginalPath.Equals(OriginalPath)))
							Parent.Parent.Parent.Parent.Parent.PreviewContainers.Add(universalPreviewContainer);
						break;
					#endregion
				}
			}
		}
	}
}