using System;
using System.Xml;

namespace SalesLibraries.ServiceConnector.Services
{
	public partial class ServiceConnection
	{
		public ServiceConnection() { }

		public ServiceConnection(XmlNode node)
		{
			Deserialize(node);
		}

		public override string ToString()
		{
			return Website;
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Url":
						var value = childNode.InnerText;
						if (value.StartsWith("http",StringComparison.OrdinalIgnoreCase))
							Website = value;
						else
							Website = String.Format("http://{0}", value);
						break;
				}
			}
		}
	}
}
