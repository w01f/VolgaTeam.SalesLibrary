using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SalesLibraries.Common.Helpers
{
	public class TabPageConfigurator
	{
		private readonly string _contentPath;

		public TabPageConfigurator(string path)
		{
			_contentPath = path;
			TabPageSettings = new List<TabPageConfig>();
			LoadConfiguration();
		}

		public List<TabPageConfig> TabPageSettings { get; private set; }

		private void LoadConfiguration()
		{
			TabPageSettings.Clear();
			if (!File.Exists(_contentPath)) return;
			var document = new XmlDocument();
			document.Load(_contentPath);
			XmlNode node = document.SelectSingleNode(@"/Root");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var tabPageConfig = new TabPageConfig();
				tabPageConfig.Deserialize(childNode);
				if (tabPageConfig.Visible)
					TabPageSettings.Add(tabPageConfig);
			}
			TabPageSettings.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}

	public class TabPageConfig
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool Visible { get; set; }
		public int Order { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Id":
						Id = childNode.InnerText;
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Visible":
						bool tempBool;
						if (Boolean.TryParse(childNode.InnerText, out tempBool))
							Visible = tempBool;
						break;
					case "Order":
						int tempInt;
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							Order = tempInt;
						break;
				}
			}
		}
	}
}
