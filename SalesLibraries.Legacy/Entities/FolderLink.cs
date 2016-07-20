using System;
using System.IO;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class FolderLink
	{
		public FolderLink()
		{
			RootId = Guid.Empty;
		}

		public Guid RootId { get; set; }
		public DirectoryInfo Folder { get; set; }

		public bool IsDrive => Folder.FullName.Equals(Folder.Root.FullName);

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
		public RootFolder(Library parent)
		{
			Parent = parent;
			Order = 0;
		}

		public Library Parent { get; private set; }
		public int Order { get; set; }

		public int Index => Order + 1;

		public string Path
		{
			get { return Folder != null ? Folder.FullName : null; }
			set
			{
				if (!string.IsNullOrEmpty(value) && Directory.Exists(value))
					Folder = new DirectoryInfo(value);
			}
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
	}
}
