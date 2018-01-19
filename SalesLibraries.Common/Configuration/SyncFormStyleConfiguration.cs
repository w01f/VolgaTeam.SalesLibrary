using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesLibraries.Common.Configuration
{
	public class SyncFormStyleConfiguration
	{
		public Color? SyncBackColor { get; private set; }
		public Color? SyncBorderColor { get; private set; }
		public Color? SyncTextColor { get; private set; }
		public Color? SyncCircleColor { get; private set; }

		public void Load(string settingsFilePath, string sectionName = "Sync")
		{
			if (!File.Exists(settingsFilePath)) return;
			var document = new XmlDocument();
			document.Load(settingsFilePath);
			var node = document.SelectSingleNode(String.Format("/Config/{0}", sectionName));
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
