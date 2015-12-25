using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class AutoWidget
	{
		public AutoWidget()
		{
			Extension = string.Empty;
		}

		public string Extension { get; set; }
		public Image Widget { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Extension":
						Extension = childNode.InnerText;
						break;
					case "Widget":
						if (string.IsNullOrEmpty(childNode.InnerText))
							Widget = null;
						else
							Widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
				}
			}
		}
	}
}