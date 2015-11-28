using System;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class UniversalPreviewContainer
	{
		public string OriginalPath { get; private set; }

		public UniversalPreviewContainer()
		{
			Identifier = Guid.NewGuid().ToString();
		}

		public string Identifier { get; private set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Identifier":
						Identifier = childNode.InnerText;
						break;
					case "OriginalPath":
						OriginalPath = childNode.InnerText;
						break;
				}
			}
		}
	}

	public class PresentationPreviewContainer
	{
		public PresentationPreviewContainer()
		{
			Identifier = Guid.NewGuid().ToString();
		}

		public string Identifier { get; private set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "FolderName":
						Identifier = childNode.InnerText;
						break;
				}
			}
		}
	}
}
