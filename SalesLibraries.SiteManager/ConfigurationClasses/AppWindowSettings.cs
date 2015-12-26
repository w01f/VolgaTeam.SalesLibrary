using System;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesLibraries.SiteManager.ConfigurationClasses
{
	public class AppWindowSettings
	{
		private string _filePath;

		public bool Existed { get; set; }

		public bool Maximized { get; set; }
		public int Top { get; set; }
		public int Left { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }

		public AppWindowSettings()
		{
			_filePath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "AppWindowSettings.xml");
		}

		public void Load()
		{
			if (!File.Exists(_filePath)) return;
			var document = new XmlDocument();
			document.Load(_filePath);

			var tempBool = false;
			var tempInt = 0;
			var node = document.SelectSingleNode(@"/Settings/Maximized");
			if (node != null && Boolean.TryParse(node.InnerText, out tempBool))
				Maximized = tempBool;
			node = document.SelectSingleNode(@"/Settings/Top");
			if (node != null && Int32.TryParse(node.InnerText, out tempInt))
				Top = tempInt;
			node = document.SelectSingleNode(@"/Settings/Left");
			if (node != null && Int32.TryParse(node.InnerText, out tempInt))
				Left = tempInt;
			node = document.SelectSingleNode(@"/Settings/Height");
			if (node != null && Int32.TryParse(node.InnerText, out tempInt))
				Height = tempInt;
			node = document.SelectSingleNode(@"/Settings/Width");
			if (node != null && Int32.TryParse(node.InnerText, out tempInt))
				Width = tempInt;

			Existed = true;
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");
			xml.AppendLine(@"<Maximized>" + Maximized + @"</Maximized>");
			xml.AppendLine(@"<Top>" + Top + @"</Top>");
			xml.AppendLine(@"<Left>" + Left + @"</Left>");
			xml.AppendLine(@"<Height>" + Height + @"</Height>");
			xml.AppendLine(@"<Width>" + Width + @"</Width>");
			xml.AppendLine(@"</Settings>");

			using (var sw = new StreamWriter(_filePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
