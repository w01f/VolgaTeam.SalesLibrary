using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class IPadManager
	{
		public string SyncDestinationPath { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SyncDestinationPath":
						SyncDestinationPath = childNode.InnerText;
						break;
				}
			}
		}
	}
}
