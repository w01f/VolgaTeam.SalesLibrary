using System;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class PresentationProperties
	{
		public double Height { get; set; }
		public double Width { get; set; }
		public DateTime LastUpdate { get; set; }

		public string Orientation => Height < Width ? "Landscape" : "Portrait";

		public string Size
		{
			get
			{
				if (Width == 10 && Height == 7.5)
					return "4x3";
				if (Width == 10.75 && Height == 8.25)
					return "5x4";
				if (Width == 13 && Height == 7.32)
					return "16x9";
				if (Width == 7.5 && Height == 10)
					return "3x4";
				if (Width == 8.25 && Height == 10.75)
					return "4x5";
				if (Width == 7.32 && Height == 13)
					return "9x16";
				return "4x3";
			}
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				double tempDouble;
				switch (childNode.Name)
				{
					case "Height":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							Height = tempDouble;
						break;
					case "Width":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							Width = tempDouble;
						break;
					case "LastUpdate":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							LastUpdate = tempDateTime;
						break;
				}
			}
		}
	}
}