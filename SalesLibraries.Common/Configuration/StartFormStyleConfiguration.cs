using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesLibraries.Common.Configuration
{
	public class StartFormStyleConfiguration
	{
		public Color? SyncBackColor { get; private set; }
		public Color? SyncBorderColor { get; private set; }
		public Color? SyncTextColor { get; private set; }
		public Color? SyncCircleColor { get; private set; }

		public void Load(string settingsFilePath)
		{
			if (!File.Exists(settingsFilePath)) return;
			var document = new XmlDocument();
			document.Load(settingsFilePath);
			var node = document.SelectSingleNode(@"/Config/Sync");
			if (node != null)
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SyncBackColor":
							SyncBackColor = ColorTranslator.FromHtml(childNode.InnerText);
							break;
						case "SyncBorderColor":
							SyncBorderColor = ColorTranslator.FromHtml(childNode.InnerText);
							break;
						case "SyncTextColor":
							SyncTextColor = ColorTranslator.FromHtml(childNode.InnerText);
							break;
						case "SyncCircleColor":
							SyncCircleColor = ColorTranslator.FromHtml(childNode.InnerText);
							break;
					}
				}
		}
	}
}
