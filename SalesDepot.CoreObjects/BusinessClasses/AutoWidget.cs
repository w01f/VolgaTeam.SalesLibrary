using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class AutoWidget
	{
		public AutoWidget()
		{
			Extension = string.Empty;
		}

		public string Extension { get; set; }
		public Image Widget { get; set; }

		public string Serialize()
		{
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.AppendLine(@"<Extension>" + Extension + @"</Extension>");
			result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(Widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
			return result.ToString();
		}

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