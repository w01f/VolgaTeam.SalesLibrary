using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	class ImageSource
	{
		public const decimal BigHeight = 146;
		public const decimal SmallHeight = 75;
		public const decimal TinyHeight = 58;
		public const decimal XtraTinyHeight = 41;
		public const decimal BigWidth = 321;
		public const decimal SmallWidth = 164;
		public const decimal TinyWidth = 128;
		public const decimal XtraTinyWidth = 90;

		public bool IsDefault { get; set; }
		public Image OriginalImage { get; set; }
		public Image BigImage { get; set; }
		public Image SmallImage { get; set; }
		public Image TinyImage { get; set; }
		public Image XtraTinyImage { get; set; }
		public string Name { get; set; }
		public string FileName { get; set; }
		public string OutputFilePath { get; set; }

		public bool ContainsData => XtraTinyImage != null;

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsDefault":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsDefault = tempBool;
						break;
					case "OriginalImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							OriginalImage = null;
						else
							OriginalImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "BigImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							BigImage = null;
						else
							BigImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "SmallImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							SmallImage = null;
						else
							SmallImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "TinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							TinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "XtraTinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							XtraTinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
				}
			}
		}
	}
}
