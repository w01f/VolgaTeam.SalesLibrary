using System;
using System.Xml;

namespace SalesLibraries.FileManager.Configuration
{
	public class RibbonTabPageConfig
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool Visible { get; set; }
		public bool Enabled { get; set; }
		public int Order { get; set; }

		public RibbonTabPageConfig()
		{
			Enabled = true;
		}

		public virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Id":
						Id = childNode.InnerText;
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Enabled":
					{
						if (Boolean.TryParse(childNode.InnerText, out var temp))
							Enabled = temp;
						break;
					}
					case "Visible":
					{
						if (Boolean.TryParse(childNode.InnerText, out var temp))
							Visible = temp;
						break;
					}
					case "Order":
					{
						if (Int32.TryParse(childNode.InnerText, out var temp))
							Order = temp;
						break;
					}
				}
			}
		}
	}
}
