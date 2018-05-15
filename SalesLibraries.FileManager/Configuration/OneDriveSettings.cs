using System;
using System.Xml;

namespace SalesLibraries.FileManager.Configuration
{
	class OneDriveSettings
	{
		public bool Enabled { get; private set; }
		public string AppId { get; private set; }
		public string AppKey { get; private set; }
		public string Token { get; private set; }
		public string RootPath { get; private set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Enabled":
						Enabled = Boolean.Parse(childNode.InnerText);
						break;
					case "AppId":
						AppId = childNode.InnerText.Trim();
						break;
					case "AppKey":
						AppKey = childNode.InnerText.Trim();
						break;
					case "Token":
						Token = childNode.InnerText.Trim();
						break;
					case "RootPath":
						RootPath = childNode.InnerText.Trim();
						break;
				}
			}
		}
	}
}
