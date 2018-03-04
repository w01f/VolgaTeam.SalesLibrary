using System;
using System.Xml;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		public SoapServiceConnection() { }

		public SoapServiceConnection(XmlNode node)
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
						Website = value.StartsWith("http",StringComparison.OrdinalIgnoreCase) ? 
							value : 
							String.Format("http://{0}", value);
						break;
				}
			}
		}
	}
}
