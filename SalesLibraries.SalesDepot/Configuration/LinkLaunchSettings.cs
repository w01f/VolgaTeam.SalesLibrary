using System;
using System.Text;
using System.Xml;

namespace SalesLibraries.SalesDepot.Configuration
{
	class LinkLaunchSettings
	{
		public LinkLaunchOptionsEnum PowerPoint { get; set; }
		public LinkLaunchOptionsEnum PDF { get; set; }
		public LinkLaunchOptionsEnum Word { get; set; }
		public LinkLaunchOptionsEnum Excel { get; set; }
		public LinkLaunchOptionsEnum Video { get; set; }
		public LinkLaunchOptionsEnum Folder { get; set; }
		public bool OldStyleQuickView { get; set; }

		public LinkLaunchSettings()
		{
			PowerPoint = LinkLaunchOptionsEnum.Viewer;
			PDF = LinkLaunchOptionsEnum.Viewer;
			Word = LinkLaunchOptionsEnum.Menu;
			Excel = LinkLaunchOptionsEnum.Menu;
			Video = LinkLaunchOptionsEnum.Viewer;
			Folder = LinkLaunchOptionsEnum.Viewer;
			OldStyleQuickView = false;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<PowerPoint>" + PowerPoint + @"</PowerPoint>");
			result.AppendLine(@"<PDF>" + PDF + @"</PDF>");
			result.AppendLine(@"<Word>" + Word + @"</Word>");
			result.AppendLine(@"<Excel>" + Excel + @"</Excel>");
			result.AppendLine(@"<Video>" + Video + @"</Video>");
			result.AppendLine(@"<Folder>" + Folder + @"</Folder>");
			result.AppendLine(@"<OldStyleQuickView>" + OldStyleQuickView + @"</OldStyleQuickView>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				LinkLaunchOptionsEnum tempLaunchOptions;
				switch (childNode.Name)
				{
					case "PowerPoint":
						if (Enum.TryParse(childNode.InnerText, out tempLaunchOptions))
							PowerPoint = tempLaunchOptions;
						break;
					case "PDF":
						if (Enum.TryParse(childNode.InnerText, out tempLaunchOptions))
							PDF = tempLaunchOptions;
						break;
					case "Word":
						if (Enum.TryParse(childNode.InnerText, out tempLaunchOptions))
							Word = tempLaunchOptions;
						break;
					case "Excel":
						if (Enum.TryParse(childNode.InnerText, out tempLaunchOptions))
							Excel = tempLaunchOptions;
						break;
					case "Video":
						if (Enum.TryParse(childNode.InnerText, out tempLaunchOptions))
							Video = tempLaunchOptions;
						break;
					case "Folder":
						if (Enum.TryParse(childNode.InnerText, out tempLaunchOptions))
							Folder = tempLaunchOptions;
						break;
					case "OldStyleQuickView":
						bool tempBool;
						if (Boolean.TryParse(childNode.InnerText, out tempBool))
							OldStyleQuickView = tempBool;
						break;
				}
			}
		}
	}
}
