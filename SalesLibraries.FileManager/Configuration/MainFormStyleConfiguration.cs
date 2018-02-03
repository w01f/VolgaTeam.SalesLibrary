using System.Drawing;
using System.Xml;

namespace SalesLibraries.FileManager.Configuration
{
	class MainFormStyleConfiguration
	{
		public Color? AccentColor { get; set; }
		public Color? StatusBarTextColor { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "AccentColor":
						AccentColor = ColorTranslator.FromHtml(childNode.InnerText);
						break;
					case "StatusBarTextColor":
						StatusBarTextColor = ColorTranslator.FromHtml(childNode.InnerText);
						break;
				}
			}
		}
	}
}
