using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class SuperFilter
	{
		public string Name { get; set; }

		public static IEnumerable<SuperFilter> LoadSuperFilters()
		{
			var result = new List<SuperFilter>();
			var superFilterPath = Path.Combine(Path.GetDirectoryName(typeof(SuperFilter).Assembly.Location), "superfilter.xml");
			if (File.Exists(superFilterPath))
			{
				var document = new XmlDocument();
				document.Load(superFilterPath);
				var node = document.SelectSingleNode(@"/superfilters");
				if (node != null)
					foreach (XmlNode child in node.ChildNodes)
						result.Add(new SuperFilter() { Name = child.InnerText });
			}
			return result;
		}
	}
}