using System.Xml;

namespace SalesLibraries.SalesDepot.Configuration
{
	class WallbinButtonsSettings
	{
		public string ClassicTitle { get; set; }
		public string ClassicDescription { get; set; }

		public string ListTitle { get; set; }
		public string ListDescription { get; set; }

		public string AccordionTitle { get; set; }
		public string AccordionDescription { get; set; }

		public void LoadDefault()
		{
			if (!RemoteResourceManager.Instance.DefaultViewFile.ExistsLocal()) return;
			var document = new XmlDocument();
			try
			{
				document.Load(RemoteResourceManager.Instance.DefaultViewFile.LocalPath);
			}
			catch { }

			var node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn1");
			if (node != null)
				ClassicTitle = node.InnerText;
			node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn1tooltip");
			if (node != null)
				ClassicDescription = node.InnerText;
			node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn2");
			if (node != null)
				ListTitle = node.InnerText;
			node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn2tooltip");
			if (node != null)
				ListDescription = node.InnerText;
			node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn4");
			if (node != null)
				AccordionTitle = node.InnerText;
			node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn4tooltip");
			if (node != null)
				AccordionDescription = node.InnerText;
		}
	}
}
