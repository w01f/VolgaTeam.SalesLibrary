﻿using System;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class FolderLink
	{
		public FolderLink()
		{
			RootId = Guid.Empty;
		}

		public Guid RootId { get; set; }
		public DirectoryInfo Folder { get; set; }

		public bool IsDrive
		{
			get { return Folder.FullName.Equals(Folder.Root.FullName); }
		}

		public virtual string Serialize()
		{
			var result = new StringBuilder();
			if (Folder != null)
			{
				result.AppendLine(@"<RootId>" + RootId.ToString() + @"</RootId>");
				result.AppendLine(@"<Folder>" + Folder.FullName + @"</Folder>");
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
							RootId = tempGuid;
						break;
					case "Folder":
						Folder = new DirectoryInfo(childNode.InnerText);
						break;
				}
			}
		}
	}

	public class RootFolder : FolderLink
	{
		public RootFolder(ILibrary parent)
		{
			Parent = parent;
			Order = 0;
		}

		public ILibrary Parent { get; private set; }
		public int Order { get; set; }

		public int Index
		{
			get { return Order + 1; }
		}

		public string Path
		{
			get { return Folder != null ? Folder.FullName : null; }
			set
			{
				if (!string.IsNullOrEmpty(value) && Directory.Exists(value))
					Folder = new DirectoryInfo(value);
			}
		}

		public override string Serialize()
		{
			var result = new StringBuilder();
			if (Folder != null)
			{
				result.AppendLine(@"<RootId>" + RootId.ToString() + @"</RootId>");
				result.AppendLine(@"<Order>" + Order.ToString() + @"</Order>");
				result.AppendLine(@"<Folder>" + Folder.FullName + @"</Folder>");
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
							RootId = tempGuid;
						break;
					case "Order":
						if (int.TryParse(childNode.InnerText, out tempInt))
							Order = tempInt;
						break;
					case "Folder":
						Folder = new DirectoryInfo(childNode.InnerText);
						break;
				}
			}
		}

		public RootFolder Clone(ILibrary parent)
		{
			var folder = new RootFolder(parent);
			folder.Folder = Folder;
			folder.Order = Order;
			return folder;
		}
	}
}