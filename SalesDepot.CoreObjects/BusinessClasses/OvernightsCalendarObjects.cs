using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class OvernightsCalendar
	{
		public OvernightsCalendar(ILibrary parent)
		{
			Parent = parent;
			RootFolder = new DirectoryInfo(Application.StartupPath);
			Parts = new List<CalendarPart>();
			ResetColors();
		}
		public ILibrary Parent { get; set; }
		public bool Enabled { get; set; }
		public DirectoryInfo RootFolder { get; set; }
		public List<CalendarPart> Parts { get; private set; }

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

		public OvernightsCalendar Clone(ILibrary parent)
		{
			var calendar = new OvernightsCalendar(parent);
			calendar.Enabled = Enabled;
			calendar.RootFolder = RootFolder;

			foreach (var calendarPart in Parts)
				calendar.Parts.Add(calendarPart.Clone(calendar));

			#region Color Settings
			calendar.CalendarBackColor = CalendarBackColor;
			calendar.CalendarBorderColor = CalendarBorderColor;
			calendar.CalendarHeaderBackColor = CalendarHeaderBackColor;
			calendar.CalendarHeaderForeColor = CalendarHeaderForeColor;
			calendar.MonthHeaderBackColor = MonthHeaderBackColor;
			calendar.MonthHeaderForeColor = MonthHeaderForeColor;
			calendar.MonthBodyBackColor = MonthBodyBackColor;
			calendar.MonthBodyForeColor = MonthBodyForeColor;
			calendar.SweepBackColor = SweepBackColor;
			calendar.SweepForeColor = SweepForeColor;
			calendar.DeadLinksForeColor = DeadLinksForeColor;
			#endregion

			return calendar;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Enabled>" + Enabled + @"</Enabled>");
			result.AppendLine(@"<RootFolder>" + RootFolder.FullName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RootFolder>");

			#region Color Settings
			result.AppendLine(@"<CalendarBackColor>" + CalendarBackColor.ToArgb() + @"</CalendarBackColor>");
			result.AppendLine(@"<CalendarBorderColor>" + CalendarBorderColor.ToArgb() + @"</CalendarBorderColor>");
			result.AppendLine(@"<CalendarHeaderBackColor>" + CalendarHeaderBackColor.ToArgb() + @"</CalendarHeaderBackColor>");
			result.AppendLine(@"<CalendarHeaderForeColor>" + CalendarHeaderForeColor.ToArgb() + @"</CalendarHeaderForeColor>");
			result.AppendLine(@"<MonthHeaderBackColor>" + MonthHeaderBackColor.ToArgb() + @"</MonthHeaderBackColor>");
			result.AppendLine(@"<MonthHeaderForeColor>" + MonthHeaderForeColor.ToArgb() + @"</MonthHeaderForeColor>");
			result.AppendLine(@"<MonthBodyBackColor>" + MonthBodyBackColor.ToArgb() + @"</MonthBodyBackColor>");
			result.AppendLine(@"<MonthBodyForeColor>" + MonthBodyForeColor.ToArgb() + @"</MonthBodyForeColor>");
			result.AppendLine(@"<SweepBackColor>" + SweepBackColor.ToArgb() + @"</SweepBackColor>");
			result.AppendLine(@"<SweepForeColor>" + SweepForeColor.ToArgb() + @"</SweepForeColor>");
			result.AppendLine(@"<DeadLinksForeColor>" + DeadLinksForeColor.ToArgb() + @"</DeadLinksForeColor>");
			#endregion

			return result.ToString();
		}

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

		public void LoadParts()
		{
			if (!RootFolder.Exists || !Enabled) return;
			Parts.Clear();
			foreach (var directory in RootFolder.GetDirectories())
			{
				var calendarPart = new CalendarPart(this, directory);
				calendarPart.Load();
				if (calendarPart.Configured)
					Parts.Add(calendarPart);
			}
		}

		public void ResetColors()
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

	public class CalendarPart
	{
		public OvernightsCalendar Parent { get; private set; }
		public bool Configured { get; private set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public DirectoryInfo PartFolder { get; private set; }
		public List<CalendarYear> Years { get; private set; }
		public List<FileInfo> Files { get; private set; }

		public CalendarPart(OvernightsCalendar parent, DirectoryInfo partFolder)
		{
			Parent = parent;
			PartFolder = partFolder;
			Years = new List<CalendarYear>();
			Files = new List<FileInfo>();
		}

		public void Load()
		{
			Configured = false;
			Years.Clear();
			Files.Clear();

			if (!PartFolder.Exists) return;

			var configFile = Path.Combine(PartFolder.FullName, Constants.CalendarPartConfigFileName);
			if (!File.Exists(configFile)) return;

			try
			{
				var document = new XmlDocument();
				document.Load(configFile);
				var node = document.SelectSingleNode(@"/Config/Name");
				if (node != null)
				{
					Name = node.InnerText;
					Configured = true;
				}
				node = document.SelectSingleNode(@"/Config/Enabled");
				if (node != null)
				{
					bool temp;
					if (Boolean.TryParse(node.InnerText, out temp))
						Enabled = temp;
				}
			}
			catch { }

			if (!Configured || !Enabled) return;

			foreach (var yearFolder in PartFolder.GetDirectories())
			{
				int temp;
				if (!int.TryParse(yearFolder.Name, out temp)) continue;
				var year = new CalendarYear(this);
				year.RootFolder = yearFolder;
				year.Year = temp;
				Files.AddRange(year.RootFolder.GetFiles());
				Years.Add(year);
				Application.DoEvents();
			}
			foreach (var year in Years)
			{
				year.LoadSweepPeriods();
				year.LoadMonths();
				Application.DoEvents();
			}
		}

		public CalendarPart Clone(OvernightsCalendar parent)
		{
			var calendarPart = new CalendarPart(parent, PartFolder);
			calendarPart.Name = Name;
			calendarPart.Enabled = Enabled;
			return calendarPart;
		}
	}

	public class CalendarYear
	{
		private readonly FileSystemWatcher _libraryStorageWatcher = new FileSystemWatcher();

		public CalendarYear(CalendarPart parent)
		{
			Parent = parent;
			Months = new List<CalendarMonth>();
			SweepDays = new List<DateTime>();
		}

		public CalendarPart Parent { get; private set; }
		public DirectoryInfo RootFolder { get; set; }
		public int Year { get; set; }
		public List<CalendarMonth> Months { get; private set; }
		public List<DateTime> SweepDays { get; private set; }

		public void LoadMonths()
		{
			Months.Clear();
			for (int i = 1; i <= 12; i++)
			{
				var month = new CalendarMonth(this);
				month.MonthFirstDay = new DateTime(Year, i, 1);
				month.LoadDays();
				Months.Add(month);
				Application.DoEvents();
			}

			_libraryStorageWatcher.Path = RootFolder.FullName;
			_libraryStorageWatcher.Created += (sender, e) =>
			{
				if (!Parent.Files.Select(x => x.FullName).Contains(e.FullPath))
					Parent.Files.Add(new FileInfo(e.FullPath));
			};
			_libraryStorageWatcher.EnableRaisingEvents = true;
		}

		public void LoadSweepPeriods()
		{
			SweepDays.Clear();
			if (!RootFolder.Exists) return;
			var sweepPeriodsConnfigFile = Path.Combine(RootFolder.FullName, Constants.SweepPeriodsFileName);
			if (!File.Exists(sweepPeriodsConnfigFile)) return;
			try
			{
				var document = new XmlDocument();
				document.Load(sweepPeriodsConnfigFile);
				var node = document.SelectSingleNode(@"/SweepPeriods");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					if (!childNode.Name.Equals("SweepPeriod")) continue;
					var dateBegin = DateTime.MinValue;
					var dateEnd = DateTime.MinValue;
					foreach (XmlAttribute attribute in childNode.Attributes)
					{
						switch (attribute.Name)
						{
							case "DateBegin":
								DateTime.TryParse(attribute.Value, out dateBegin);
								break;
							case "DateEnd":
								DateTime.TryParse(attribute.Value, out dateEnd);
								break;
						}
					}
					if (dateBegin.Equals(DateTime.MinValue) || dateEnd.Equals(DateTime.MinValue) || dateBegin >= dateEnd) continue;
					while (dateBegin <= dateEnd)
					{
						SweepDays.Add(dateBegin);
						dateBegin = dateBegin.AddDays(1);
					}
				}
			}
			catch { }
		}
	}

	public class CalendarMonth
	{
		public CalendarMonth(CalendarYear parent)
		{
			Parent = parent;
			Days = new List<CalendarDay>();
		}

		public CalendarYear Parent { get; private set; }
		public DateTime MonthFirstDay { get; set; }
		public List<CalendarDay> Days { get; private set; }

		public string Name
		{
			get { return MonthFirstDay.ToString("MMMM").ToUpper(); }
		}

		public void LoadDays()
		{
			Days.Clear();

			DateTime startDay = MonthFirstDay;
			while (startDay.DayOfWeek != DayOfWeek.Monday)
				startDay = startDay.AddDays(-1);

			DateTime lastDay = MonthFirstDay.AddMonths(1).AddDays(-1);
			while (lastDay.DayOfWeek != DayOfWeek.Sunday)
				lastDay = lastDay.AddDays(-1);

			while (startDay <= lastDay)
			{
				var day = new CalendarDay(this);
				day.Date = startDay;
				day.IsSweepDay = Parent.SweepDays.Contains(day.Date);
				Days.Add(day);
				Application.DoEvents();

				startDay = startDay.AddDays(1);
			}
		}
	}

	public class CalendarDay
	{
		private FileInfo _linkedFile;
		private bool _linkedFileReady;

		public CalendarDay(CalendarMonth parent)
		{
			Parent = parent;
		}

		public CalendarMonth Parent { get; private set; }
		public DateTime Date { get; set; }

		public bool IsSweepDay { get; set; }

		public FileInfo LinkedFile
		{
			get
			{
				if (!_linkedFileReady)
				{
					_linkedFileReady = true;
					_linkedFile = Parent.Parent.Parent.Files.FirstOrDefault(x => x.Name.Contains(Date.ToString("MMddyy")));
				}
				return _linkedFile;
			}
		}

		public void Reset()
		{
			_linkedFileReady = false;
		}
	}
}