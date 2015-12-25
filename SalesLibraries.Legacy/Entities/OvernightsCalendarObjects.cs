using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class OvernightsCalendar
	{
		public OvernightsCalendar(Library parent)
		{
			Parent = parent;
			ResetColors();
		}
		public Library Parent { get; set; }
		public bool Enabled { get; set; }
		public DirectoryInfo RootFolder { get; set; }

		#region Color Settings
		public Color CalendarBackColor { get; set; }
		public Color CalendarBorderColor { get; set; }
		public Color CalendarHeaderBackColor { get; set; }
		public Color CalendarHeaderForeColor { get; set; }
		public Color MonthHeaderBackColor { get; set; }
		public Color MonthHeaderForeColor { get; set; }
		public Color MonthBodyBackColor { get; set; }
		public Color MonthBodyForeColor { get; set; }
		public Color SweepBackColor { get; set; }
		public Color SweepForeColor { get; set; }
		public Color DeadLinksForeColor { get; set; }
		#endregion

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				int tempInt;
				switch (childNode.Name)
				{
					case "Enabled":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Enabled = tempBool;
						break;
					case "RootFolder":
						if (Directory.Exists(childNode.InnerText))
							RootFolder = new DirectoryInfo(childNode.InnerText);
						break;

					#region Color Settings
					case "CalendarBackColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							CalendarBackColor = Color.FromArgb(tempInt);
						break;
					case "CalendarBorderColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							CalendarBorderColor = Color.FromArgb(tempInt);
						break;
					case "CalendarHeaderBackColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							CalendarHeaderBackColor = Color.FromArgb(tempInt);
						break;
					case "CalendarHeaderForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							CalendarHeaderForeColor = Color.FromArgb(tempInt);
						break;
					case "MonthHeaderBackColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							MonthHeaderBackColor = Color.FromArgb(tempInt);
						break;
					case "MonthHeaderForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							MonthHeaderForeColor = Color.FromArgb(tempInt);
						break;
					case "MonthBodyBackColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							MonthBodyBackColor = Color.FromArgb(tempInt);
						break;
					case "MonthBodyForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							MonthBodyForeColor = Color.FromArgb(tempInt);
						break;
					case "SweepBackColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SweepBackColor = Color.FromArgb(tempInt);
						break;
					case "SweepForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SweepForeColor = Color.FromArgb(tempInt);
						break;
					case "DeadLinksForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DeadLinksForeColor = Color.FromArgb(tempInt);
						break;
					#endregion
				}
			}
		}

		private void ResetColors()
		{
			CalendarBackColor = Color.AliceBlue;
			CalendarBorderColor = Color.DarkGray;
			CalendarHeaderBackColor = Color.Azure;
			CalendarHeaderForeColor = Color.Black;
			MonthHeaderBackColor = Color.AliceBlue;
			MonthHeaderForeColor = Color.Black;
			MonthBodyBackColor = Color.AliceBlue;
			MonthBodyForeColor = Color.Black;
			SweepBackColor = Color.LightGray;
			SweepForeColor = Color.Black;
			DeadLinksForeColor = Color.Black;
		}
	}
}