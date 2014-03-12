using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SalesDepot.BusinessClasses
{
	public class ThemeManager
	{
		public const string RootTemplate = @"{0}\newlocaldirect.com\sync\Incoming\slides\SellerPointThemes\{1}";

		public List<Theme> Themes { get; private set; }

		public ThemeManager(string rootPath)
		{
			Themes = new List<Theme>();
			if (!Directory.Exists(rootPath)) return;
			var approvedThemes = LoadApprovedThemes(rootPath);
			foreach (var themeFolder in Directory.GetDirectories(rootPath))
			{
				var theme = new Theme(themeFolder);
				if (approvedThemes.Any(at => theme.Name == at))
					Themes.Add(theme);
			}
			Themes.Sort((x, y) => x.Order.CompareTo(y.Order));
		}

		private IEnumerable<string> LoadApprovedThemes(string rootPath)
		{
			var approvedThemes = new List<string>();
			var filePath = Path.Combine(Directory.GetParent(rootPath).FullName, "ApprovedThemes.xml");
			if (!File.Exists(filePath)) return approvedThemes;
			var document = new XmlDocument();
			document.Load(filePath);
			foreach (var slideNode in document.SelectNodes(@"//Root/Slide").OfType<XmlNode>())
			{
				var slideAttribute = slideNode.Attributes["Name"];
				if (slideAttribute == null) continue;
				if (slideAttribute.Value != "SalesLibrary") continue;
				foreach (var themeNode in slideNode.SelectNodes("Theme").OfType<XmlNode>())
				{
					if (!approvedThemes.Contains(themeNode.InnerText))
						approvedThemes.Add(themeNode.InnerText);
				}
			}
			return approvedThemes;
		}
	}

	public class Theme
	{
		public string Name { get; private set; }
		public int Order { get; private set; }
		public string PotFilePath { get; private set; }
		public string ThemeFilePath { get; private set; }

		public Theme(string rootPath)
		{
			var titlePath = Path.Combine(rootPath, "title.txt");
			if (File.Exists(titlePath))
				Name = File.ReadAllText(titlePath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(rootPath), out tempInt))
				Order = tempInt;

			PotFilePath = Directory.GetFiles(rootPath, "*.pot").FirstOrDefault();
			ThemeFilePath = Directory.GetFiles(rootPath, "*.thmx").FirstOrDefault();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
