using System;
using System.Text;
using System.Xml;

namespace SalesLibraries.SalesDepot.Configuration
{
	class CalendarViewSettings
	{
		public string SelectedCalendar { get; set; }
		public int SelectedYear { get; set; }
		public int FontSize { get; set; }

		public CalendarViewSettings()
		{
			SelectedCalendar = String.Empty;
			SelectedYear = 0;
			FontSize = 10;
		}

		public string Serialize()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<SelectedCalendar>" + SelectedCalendar.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedCalendar>");
			xml.AppendLine(@"<SelectedYear>" + SelectedYear + @"</SelectedYear>");
			xml.AppendLine(@"<FontSize>" + FontSize + @"</FontSize>");
			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				int tempInt;
				switch (childNode.Name)
				{
					case "SelectedCalendar":
						SelectedCalendar = childNode.InnerText;
						break;
					case "SelectedYear":
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							SelectedYear = tempInt;
						break;
					case "FontSize":
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							FontSize = tempInt;
						break;
				}
			}
		}
	}
}
