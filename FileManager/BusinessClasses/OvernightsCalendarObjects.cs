using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FileManager.BusinessClasses
{
    public class OvernightsCalendar
    {
        public Library Parent { get; set; }
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
        public int GrabInterval { get; set; }
        public string InboxSubFolder { get; set; }
        #endregion

        public OvernightsCalendar(Library parent)
        {
            this.Parent = parent;

            this.RootFolder = new DirectoryInfo(@"z:\");
            this.Years = new List<CalendarYear>();
            this.Files = new List<FileInfo>();

            #region Email Grabber Settings
            this.EnableEmailGrabber = false;
            this.GrabInterval = 10;
            this.InboxSubFolder = "Inbox";
            #endregion

            ResetColors();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Enabled>" + this.Enabled.ToString() + @"</Enabled>");
            result.AppendLine(@"<RootFolder>" + this.RootFolder.FullName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RootFolder>");

            #region Color Settings
            result.AppendLine(@"<CalendarBackColor>" + this.CalendarBackColor.ToArgb().ToString() + @"</CalendarBackColor>");
            result.AppendLine(@"<CalendarBorderColor>" + this.CalendarBorderColor.ToArgb().ToString() + @"</CalendarBorderColor>");
            result.AppendLine(@"<CalendarHeaderBackColor>" + this.CalendarHeaderBackColor.ToArgb().ToString() + @"</CalendarHeaderBackColor>");
            result.AppendLine(@"<CalendarHeaderForeColor>" + this.CalendarHeaderForeColor.ToArgb().ToString() + @"</CalendarHeaderForeColor>");
            result.AppendLine(@"<MonthHeaderBackColor>" + this.MonthHeaderBackColor.ToArgb().ToString() + @"</MonthHeaderBackColor>");
            result.AppendLine(@"<MonthHeaderForeColor>" + this.MonthHeaderForeColor.ToArgb().ToString() + @"</MonthHeaderForeColor>");
            result.AppendLine(@"<MonthBodyBackColor>" + this.MonthBodyBackColor.ToArgb().ToString() + @"</MonthBodyBackColor>");
            result.AppendLine(@"<MonthBodyForeColor>" + this.MonthBodyForeColor.ToArgb().ToString() + @"</MonthBodyForeColor>");
            result.AppendLine(@"<SweepBackColor>" + this.SweepBackColor.ToArgb().ToString() + @"</SweepBackColor>");
            result.AppendLine(@"<SweepForeColor>" + this.SweepForeColor.ToArgb().ToString() + @"</SweepForeColor>");
            result.AppendLine(@"<DeadLinksForeColor>" + this.DeadLinksForeColor.ToArgb().ToString() + @"</DeadLinksForeColor>");
            #endregion

            #region Email Grabber Settings
            result.AppendLine(@"<EnableEmailGrabber>" + this.EnableEmailGrabber.ToString() + @"</EnableEmailGrabber>");
            result.AppendLine(@"<GrabInterval>" + this.GrabInterval.ToString() + @"</GrabInterval>");
            result.AppendLine(@"<InboxSubFolder>" + this.InboxSubFolder.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</InboxSubFolder>");
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
                            this.Enabled = tempBool;
                        break;
                    case "RootFolder":
                        if (Directory.Exists(childNode.InnerText))
                            this.RootFolder = new DirectoryInfo(childNode.InnerText);
                        break;
                    #region Color Settings
                    case "CalendarBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarBackColor = Color.FromArgb(tempInt);
                        break;
                    case "CalendarBorderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarBorderColor = Color.FromArgb(tempInt);
                        break;
                    case "CalendarHeaderBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarHeaderBackColor = Color.FromArgb(tempInt);
                        break;
                    case "CalendarHeaderForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarHeaderForeColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthHeaderBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthHeaderBackColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthHeaderForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthHeaderForeColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthBodyBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthBodyBackColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthBodyForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthBodyForeColor = Color.FromArgb(tempInt);
                        break;
                    case "SweepBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SweepBackColor = Color.FromArgb(tempInt);
                        break;
                    case "SweepForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SweepForeColor = Color.FromArgb(tempInt);
                        break;
                    case "DeadLinksForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeadLinksForeColor = Color.FromArgb(tempInt);
                        break;
                    #endregion

                    #region Email Grabber Settings
                    case "EnableEmailGrabber":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableEmailGrabber = tempBool;
                        break;
                    case "GrabInterval":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.GrabInterval = tempInt;
                        break;
                    case "InboxSubFolder":
                        this.InboxSubFolder = childNode.InnerText;
                        break;
                    #endregion
                }
            }
        }

        public void LoadYears()
        {
            this.Years.Clear();
            this.Files.Clear();
            if (this.RootFolder.Exists && this.Enabled)
            {
                foreach (DirectoryInfo yearFolder in this.RootFolder.GetDirectories())
                {
                    int temp;
                    if (int.TryParse(yearFolder.Name, out temp))
                    {
                        CalendarYear year = new CalendarYear(this);
                        year.RootFolder = yearFolder;
                        year.Year = temp;
                        this.Files.AddRange(year.RootFolder.GetFiles());
                        this.Years.Add(year);
                        Application.DoEvents();
                    }
                }
                foreach (CalendarYear year in this.Years)
                {
                    year.LoadSweepPeriods();
                    year.LoadMonths();
                    Application.DoEvents();
                }
            }
        }

        public void ResetColors()
        {
            this.CalendarBackColor = Color.AliceBlue;
            this.CalendarBorderColor = Color.DarkGray;
            this.CalendarHeaderBackColor = Color.Azure;
            this.CalendarHeaderForeColor = Color.Black;
            this.MonthHeaderBackColor = Color.AliceBlue;
            this.MonthHeaderForeColor = Color.Black;
            this.MonthBodyBackColor = Color.AliceBlue;
            this.MonthBodyForeColor = Color.Black;
            this.SweepBackColor = Color.LightGray;
            this.SweepForeColor = Color.Black;
            this.DeadLinksForeColor = Color.Black;
        }
    }

    public class CalendarYear
    {
        private FileSystemWatcher _libraryStorageWatcher = new FileSystemWatcher();

        public OvernightsCalendar Parent { get; private set; }
        public DirectoryInfo RootFolder { get; set; }
        public int Year { get; set; }
        public List<CalendarMonth> Months { get; private set; }
        public List<DateTime> SweepDays { get; private set; }

        public CalendarYear(OvernightsCalendar parent)
        {
            this.Parent = parent;
            this.Months = new List<CalendarMonth>();
            this.SweepDays = new List<DateTime>();
        }

        public void LoadMonths()
        {
            this.Months.Clear();
            for (int i = 1; i <= 12; i++)
            {
                CalendarMonth month = new CalendarMonth(this);
                month.MonthFirstDay = new DateTime(this.Year, i, 1);
                month.LoadDays();
                this.Months.Add(month);
                Application.DoEvents();
            }

            _libraryStorageWatcher.Path = this.RootFolder.FullName;
            _libraryStorageWatcher.Created += new FileSystemEventHandler((sender, e) =>
            {
                try
                {
                    _libraryStorageWatcher.EnableRaisingEvents = false;
                    this.Parent.Files.Add(new FileInfo(e.FullPath));
                }
                finally
                {
                    _libraryStorageWatcher.EnableRaisingEvents = true;
                }
            });
            _libraryStorageWatcher.EnableRaisingEvents = true;
        }

        public void LoadSweepPeriods()
        {
            this.SweepDays.Clear();
            if (this.RootFolder.Exists)
            {
                string sweepPeriodsConnfigFile = Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.SweepPeriodsFileName);
                if (File.Exists(sweepPeriodsConnfigFile))
                {
                    try
                    {
                        XmlDocument document = new XmlDocument();
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
                                            this.SweepDays.Add(dateBegin);
                                            dateBegin = dateBegin.AddDays(1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    public class CalendarMonth
    {
        public CalendarYear Parent { get; private set; }
        public DateTime MonthFirstDay { get; set; }
        public List<CalendarDay> Days { get; private set; }

        public string Name
        {
            get
            {
                return this.MonthFirstDay.ToString("MMMM").ToUpper();
            }
        }

        public CalendarMonth(CalendarYear parent)
        {
            this.Parent = parent;
            this.Days = new List<CalendarDay>();
        }

        public void LoadDays()
        {
            this.Days.Clear();

            DateTime startDay = this.MonthFirstDay;
            while (startDay.DayOfWeek != DayOfWeek.Monday)
                startDay = startDay.AddDays(-1);

            DateTime lastDay = this.MonthFirstDay.AddMonths(1).AddDays(-1);
            while (lastDay.DayOfWeek != DayOfWeek.Sunday)
                lastDay = lastDay.AddDays(-1);

            while (startDay <= lastDay)
            {
                CalendarDay day = new CalendarDay(this);
                day.Date = startDay;
                day.IsSweepDay = this.Parent.SweepDays.Contains(day.Date);
                this.Days.Add(day);
                Application.DoEvents();

                startDay = startDay.AddDays(1);
            }
        }
    }

    public class CalendarDay
    {
        private FileInfo _linkedFile = null;
        private bool _linkedFileReady = false;

        public CalendarMonth Parent { get; private set; }
        public DateTime Date { get; set; }

        public bool IsSweepDay { get; set; }

        public CalendarDay(CalendarMonth parent)
        {
            this.Parent = parent;
        }

        public void Reset()
        {
            _linkedFileReady = false;
        }

        public FileInfo LinkedFile
        {
            get
            {
                if (!_linkedFileReady)
                {
                    _linkedFileReady = true;
                    _linkedFile = this.Parent.Parent.Parent.Files.Where(x => x.Name.Contains(this.Date.ToString("MMddyy"))).FirstOrDefault();
                }
                return _linkedFile;
            }
        }
    }
}
