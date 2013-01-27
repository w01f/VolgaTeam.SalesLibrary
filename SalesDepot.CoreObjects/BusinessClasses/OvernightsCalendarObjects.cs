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
			Years = new List<CalendarYear>();
			Files = new List<FileInfo>();

			#region Email Grabber Settings
			EnableEmailGrabber = false;
			EmailGrabInterval = 10;
			InboxSubFolder = "Inbox";
			#endregion

			#region File Grabber Settings
			EnableFileGrabber = false;
			FileGrabInterval = 10;
			FileGrabSourceFolder = @"c:\Overnights Source";
			#endregion

			ResetColors();
		}

		public ILibrary Parent { get; set; }
		public bool Enabled { get; set; }
		public DirectoryInfo RootFolder { get; set; }
		public List<CalendarYear> Years { get; private set; }
		public List<FileInfo> Files { get; private set; }

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

		#region Email Grabber Settings
		public bool EnableEmailGrabber { get; set; }
		public int EmailGrabInterval { get; set; }
		public string InboxSubFolder { get; set; }
		#endregion

		#region File Grabber Settings
		public bool EnableFileGrabber { get; set; }
		public int FileGrabInterval { get; set; }
		public string FileGrabSourceFolder { get; set; }
		#endregion

		public OvernightsCalendar Clone(ILibrary parent)
		{
			var calendar = new OvernightsCalendar(parent);
			calendar.Enabled = Enabled;
			calendar.RootFolder = RootFolder;

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

			#region Email Grabber Settings
			calendar.EnableEmailGrabber = EnableEmailGrabber;
			calendar.EmailGrabInterval = EmailGrabInterval;
			calendar.InboxSubFolder = InboxSubFolder;
			#endregion

			#region File Grabber Settings
			calendar.EnableFileGrabber = EnableFileGrabber;
			calendar.FileGrabInterval = FileGrabInterval;
			calendar.FileGrabSourceFolder = FileGrabSourceFolder;
			#endregion

			return calendar;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Enabled>" + Enabled.ToString() + @"</Enabled>");
			result.AppendLine(@"<RootFolder>" + RootFolder.FullName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RootFolder>");

			#region Color Settings
			result.AppendLine(@"<CalendarBackColor>" + CalendarBackColor.ToArgb().ToString() + @"</CalendarBackColor>");
			result.AppendLine(@"<CalendarBorderColor>" + CalendarBorderColor.ToArgb().ToString() + @"</CalendarBorderColor>");
			result.AppendLine(@"<CalendarHeaderBackColor>" + CalendarHeaderBackColor.ToArgb().ToString() + @"</CalendarHeaderBackColor>");
			result.AppendLine(@"<CalendarHeaderForeColor>" + CalendarHeaderForeColor.ToArgb().ToString() + @"</CalendarHeaderForeColor>");
			result.AppendLine(@"<MonthHeaderBackColor>" + MonthHeaderBackColor.ToArgb().ToString() + @"</MonthHeaderBackColor>");
			result.AppendLine(@"<MonthHeaderForeColor>" + MonthHeaderForeColor.ToArgb().ToString() + @"</MonthHeaderForeColor>");
			result.AppendLine(@"<MonthBodyBackColor>" + MonthBodyBackColor.ToArgb().ToString() + @"</MonthBodyBackColor>");
			result.AppendLine(@"<MonthBodyForeColor>" + MonthBodyForeColor.ToArgb().ToString() + @"</MonthBodyForeColor>");
			result.AppendLine(@"<SweepBackColor>" + SweepBackColor.ToArgb().ToString() + @"</SweepBackColor>");
			result.AppendLine(@"<SweepForeColor>" + SweepForeColor.ToArgb().ToString() + @"</SweepForeColor>");
			result.AppendLine(@"<DeadLinksForeColor>" + DeadLinksForeColor.ToArgb().ToString() + @"</DeadLinksForeColor>");
			#endregion

			#region Email Grabber Settings
			result.AppendLine(@"<EnableEmailGrabber>" + EnableEmailGrabber.ToString() + @"</EnableEmailGrabber>");
			result.AppendLine(@"<EmailGrabInterval>" + EmailGrabInterval.ToString() + @"</EmailGrabInterval>");
			result.AppendLine(@"<InboxSubFolder>" + InboxSubFolder.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</InboxSubFolder>");
			#endregion

			#region File Grabber Settings
			result.AppendLine(@"<EnableFileGrabber>" + EnableFileGrabber.ToString() + @"</EnableFileGrabber>");
			result.AppendLine(@"<FileGrabInterval>" + FileGrabInterval.ToString() + @"</FileGrabInterval>");
			result.AppendLine(@"<FileGrabSourceFolder>" + FileGrabSourceFolder.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</FileGrabSourceFolder>");
			#endregion

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Enabled":
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

						#region Email Grabber Settings
					case "EnableEmailGrabber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableEmailGrabber = tempBool;
						break;
					case "EmailGrabInterval":
						if (int.TryParse(childNode.InnerText, out tempInt))
							EmailGrabInterval = tempInt;
						break;
					case "InboxSubFolder":
						InboxSubFolder = childNode.InnerText;
						break;
						#endregion

						#region File Grabber Settings
					case "EnableFileGrabber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableFileGrabber = tempBool;
						break;
					case "FileGrabInterval":
						if (int.TryParse(childNode.InnerText, out tempInt))
							FileGrabInterval = tempInt;
						break;
					case "FileGrabSourceFolder":
						FileGrabSourceFolder = childNode.InnerText;
						break;
						#endregion
				}
			}
		}

		public void LoadYears()
		{
			Years.Clear();
			Files.Clear();
			if (RootFolder.Exists && Enabled)
			{
				foreach (DirectoryInfo yearFolder in RootFolder.GetDirectories())
				{
					int temp;
					if (int.TryParse(yearFolder.Name, out temp))
					{
						var year = new CalendarYear(this);
						year.RootFolder = yearFolder;
						year.Year = temp;
						Files.AddRange(year.RootFolder.GetFiles());
						Years.Add(year);
						Application.DoEvents();
					}
				}
				foreach (CalendarYear year in Years)
				{
					year.LoadSweepPeriods();
					year.LoadMonths();
					Application.DoEvents();
				}
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

	public class CalendarYear
	{
		private readonly FileSystemWatcher _libraryStorageWatcher = new FileSystemWatcher();

		public CalendarYear(OvernightsCalendar parent)
		{
			Parent = parent;
			Months = new List<CalendarMonth>();
			SweepDays = new List<DateTime>();
		}

		public OvernightsCalendar Parent { get; private set; }
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
			if (RootFolder.Exists)
			{
				string sweepPeriodsConnfigFile = Path.Combine(RootFolder.FullName, Constants.SweepPeriodsFileName);
				if (File.Exists(sweepPeriodsConnfigFile))
				{
					try
					{
						var document = new XmlDocument();
						document.Load(sweepPeriodsConnfigFile);
						XmlNode node = document.SelectSingleNode(@"/SweepPeriods");
						if (node != null)
						{
							foreach (XmlNode childNode in node.ChildNodes)
							{
								if (childNode.Name.Equals("SweepPeriod"))
								{
									DateTime dateBegin = DateTime.MinValue;
									DateTime dateEnd = DateTime.MinValue;
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
									if (!dateBegin.Equals(DateTime.MinValue) && !dateEnd.Equals(DateTime.MinValue) && dateBegin < dateEnd)
									{
										while (dateBegin <= dateEnd)
										{
											SweepDays.Add(dateBegin);
											dateBegin = dateBegin.AddDays(1);
										}
									}
								}
							}
						}
					}
					catch {}
				}
			}
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
					_linkedFile = Parent.Parent.Parent.Files.Where(x => x.Name.Contains(Date.ToString("MMddyy"))).FirstOrDefault();
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