using System.Text;
using System.Xml;

namespace SalesLibraries.SalesDepot.Configuration
{
	class KeyWordFileFilters
	{
		public KeyWordFileFilters()
		{
			AllFiles = true;
			PowerPoint = true;
			PDF = true;
			Excel = true;
			Word = true;
			Video = true;
			Url = true;
			Network = true;
			Folder = true;
		}

		public bool AllFiles { get; set; }
		public bool PowerPoint { get; set; }
		public bool PDF { get; set; }
		public bool Excel { get; set; }
		public bool Word { get; set; }
		public bool Video { get; set; }
		public bool Url { get; set; }
		public bool Network { get; set; }
		public bool Folder { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<AllFiles>" + AllFiles + @"</AllFiles>");
			result.AppendLine(@"<PowerPoint>" + PowerPoint + @"</PowerPoint>");
			result.AppendLine(@"<PDF>" + PDF + @"</PDF>");
			result.AppendLine(@"<Excel>" + Excel + @"</Excel>");
			result.AppendLine(@"<Word>" + Word + @"</Word>");
			result.AppendLine(@"<Video>" + Video + @"</Video>");
			result.AppendLine(@"<Url>" + Url + @"</Url>");
			result.AppendLine(@"<Network>" + Network + @"</Network>");
			result.AppendLine(@"<Folder>" + Folder + @"</Folder>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "AllFiles":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							AllFiles = tempBool;
						break;
					case "PowerPoint":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							PowerPoint = tempBool;
						break;
					case "PDF":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							PDF = tempBool;
						break;
					case "Excel":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Excel = tempBool;
						break;
					case "Word":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Word = tempBool;
						break;
					case "Video":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Video = tempBool;
						break;
					case "Url":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Url = tempBool;
						break;
					case "Network":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Network = tempBool;
						break;
					case "Folder":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Folder = tempBool;
						break;
				}
			}
		}
	}
}
